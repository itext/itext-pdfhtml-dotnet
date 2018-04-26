/*
This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using System.Collections.Generic;
using Common.Logging;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Apply.Impl;
using iText.Html2pdf.Css.Apply.Util;
using iText.Html2pdf.Css.Page;
using iText.Html2pdf.Css.Util;
using iText.Html2pdf.Html.Node;
using iText.IO.Util;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Tagging;
using iText.Kernel.Pdf.Tagutils;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using iText.Layout.Tagging;

namespace iText.Html2pdf.Attach.Impl.Layout {
    /// <summary>Context processor for specific types of pages: first, left, or right page.</summary>
    internal class PageContextProcessor {
        /// <summary>The page size.</summary>
        private PageSize pageSize;

        /// <summary>Marks for page boundaries.</summary>
        private ICollection<String> marks;

        /// <summary>The bleed value for the margin.</summary>
        private float? bleed;

        /// <summary>The margins.</summary>
        private float[] margins;

        /// <summary>The borders.</summary>
        private Border[] borders;

        /// <summary>The paddings.</summary>
        private float[] paddings;

        /// <summary>Page background simulation.</summary>
        private Div pageBackgroundSimulation;

        /// <summary>Page borders simulation.</summary>
        private Div pageBordersSimulation;

        private PageContextProperties properties;

        private ProcessorContext context;

        /// <summary>Instantiates a new page context processor.</summary>
        /// <param name="properties">the page context properties</param>
        /// <param name="context">the processor context</param>
        /// <param name="defaultPageSize">the default page size</param>
        /// <param name="defaultPageMargins">the default page margins</param>
        internal PageContextProcessor(PageContextProperties properties, ProcessorContext context, PageSize defaultPageSize
            , float[] defaultPageMargins) {
            this.properties = properties;
            this.context = context;
            Reset(defaultPageSize, defaultPageMargins);
        }

        /// <summary>
        /// Re-initializes page context processor based on default current page size and page margins
        /// and on properties from css page at-rules.
        /// </summary>
        /// <remarks>
        /// Re-initializes page context processor based on default current page size and page margins
        /// and on properties from css page at-rules. Css properties priority is higher than default document values.
        /// </remarks>
        /// <param name="defaultPageSize">current default page size to be used if it is not defined in css</param>
        /// <param name="defaultPageMargins">current default page margins to be used if they are not defined in css</param>
        /// <returns>
        /// this
        /// <see cref="PageContextProcessor"/>
        /// instance
        /// </returns>
        internal virtual iText.Html2pdf.Attach.Impl.Layout.PageContextProcessor Reset(PageSize defaultPageSize, float
            [] defaultPageMargins) {
            IDictionary<String, String> styles = properties.GetResolvedPageContextNode().GetStyles();
            float em = CssUtils.ParseAbsoluteLength(styles.Get(CssConstants.FONT_SIZE));
            float rem = context.GetCssContext().GetRootFontSize();
            pageSize = PageSizeParser.FetchPageSize(styles.Get(CssConstants.SIZE), em, rem, defaultPageSize);
            UnitValue bleedValue = CssUtils.ParseLengthValueToPt(styles.Get(CssConstants.BLEED), em, rem);
            if (bleedValue != null && bleedValue.IsPointValue()) {
                bleed = bleedValue.GetValue();
            }
            marks = ParseMarks(styles.Get(CssConstants.MARKS));
            ParseMargins(styles, em, rem, defaultPageMargins);
            ParseBorders(styles, em, rem);
            ParsePaddings(styles, em, rem);
            CreatePageSimulationElements(styles, context);
            PrepareMarginBoxesSizing(properties.GetResolvedPageMarginBoxes());
            return this;
        }

        /// <summary>Gets the page size.</summary>
        /// <returns>the page size</returns>
        internal virtual PageSize GetPageSize() {
            return pageSize;
        }

        /// <summary>Compute layout margins.</summary>
        /// <returns>the float values of the margins</returns>
        internal virtual float[] ComputeLayoutMargins() {
            float[] layoutMargins = JavaUtil.ArraysCopyOf(margins, margins.Length);
            for (int i = 0; i < borders.Length; ++i) {
                float width = borders[i] != null ? borders[i].GetWidth() : 0;
                layoutMargins[i] += width;
            }
            for (int i = 0; i < paddings.Length; ++i) {
                layoutMargins[i] += paddings[i];
            }
            return layoutMargins;
        }

        /// <summary>Finalizes page processing by drawing margins if necessary.</summary>
        /// <param name="pageNum">the page to process</param>
        /// <param name="pdfDocument">
        /// the
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// to which content is written
        /// </param>
        /// <param name="documentRenderer">the document renderer</param>
        internal virtual void ProcessPageEnd(int pageNum, PdfDocument pdfDocument, DocumentRenderer documentRenderer
            ) {
            DrawMarginBoxes(pageNum, pdfDocument, documentRenderer);
        }

        /// <summary>
        /// Processes a new page by setting the bleed value, adding marks, drawing
        /// page backgrounds and borders.
        /// </summary>
        /// <param name="page">the page to process</param>
        internal virtual void ProcessNewPage(PdfPage page) {
            SetBleed(page);
            DrawMarks(page);
            DrawPageBackgroundAndBorders(page);
        }

        /// <summary>Sets the bleed value for a page.</summary>
        /// <param name="page">the new bleed</param>
        private void SetBleed(PdfPage page) {
            if (bleed == null && !marks.IsEmpty()) {
                bleed = 6f;
            }
            if (bleed != null) {
                Rectangle box = page.GetMediaBox();
                box.IncreaseHeight((float)bleed * 2);
                box.SetWidth(box.GetWidth() + (float)bleed * 2);
                page.SetMediaBox(box).SetBleedBox(box);
                Rectangle trimBox = page.GetTrimBox();
                trimBox.MoveUp((float)bleed);
                trimBox.MoveRight((float)bleed);
                page.SetTrimBox(trimBox);
            }
        }

        /// <summary>
        /// Sets the different page boundaries and draws printer marks on the page
        /// (if necessary).
        /// </summary>
        /// <param name="page">the page</param>
        private void DrawMarks(PdfPage page) {
            if (marks.IsEmpty()) {
                return;
            }
            float horizontalIndent = 48;
            float verticalIndent = 57;
            Rectangle mediaBox = page.GetMediaBox();
            mediaBox.IncreaseHeight(verticalIndent * 2);
            mediaBox.SetWidth(mediaBox.GetWidth() + horizontalIndent * 2);
            page.SetMediaBox(mediaBox);
            Rectangle bleedBox = page.GetBleedBox();
            bleedBox.MoveUp(verticalIndent);
            bleedBox.MoveRight(horizontalIndent);
            page.SetBleedBox(bleedBox);
            Rectangle trimBox = page.GetTrimBox();
            trimBox.MoveUp(verticalIndent);
            trimBox.MoveRight(horizontalIndent);
            page.SetTrimBox(trimBox);
            PdfCanvas canvas = new PdfCanvas(page);
            if (page.GetDocument().IsTagged()) {
                canvas.OpenTag(new CanvasArtifact());
            }
            if (marks.Contains(CssConstants.CROP)) {
                float cropLineLength = 24;
                float verticalCropStartIndent = verticalIndent - cropLineLength;
                float horizontalCropStartIndent = horizontalIndent - cropLineLength;
                canvas.SaveState().SetLineWidth(0.1f).MoveTo(trimBox.GetLeft(), verticalCropStartIndent).LineTo(trimBox.GetLeft
                    (), verticalIndent).MoveTo(horizontalCropStartIndent, trimBox.GetTop()).LineTo(horizontalIndent, trimBox
                    .GetTop()).MoveTo(trimBox.GetRight(), verticalCropStartIndent).LineTo(trimBox.GetRight(), verticalIndent
                    ).MoveTo(mediaBox.GetWidth() - horizontalCropStartIndent, trimBox.GetTop()).LineTo(mediaBox.GetWidth()
                     - horizontalIndent, trimBox.GetTop()).MoveTo(trimBox.GetLeft(), mediaBox.GetHeight() - verticalCropStartIndent
                    ).LineTo(trimBox.GetLeft(), mediaBox.GetHeight() - verticalIndent).MoveTo(mediaBox.GetWidth() - horizontalCropStartIndent
                    , trimBox.GetBottom()).LineTo(mediaBox.GetWidth() - horizontalIndent, trimBox.GetBottom()).MoveTo(trimBox
                    .GetRight(), mediaBox.GetHeight() - verticalCropStartIndent).LineTo(trimBox.GetRight(), mediaBox.GetHeight
                    () - verticalIndent).MoveTo(horizontalCropStartIndent, trimBox.GetBottom()).LineTo(horizontalIndent, trimBox
                    .GetBottom()).Stroke().RestoreState();
            }
            if (marks.Contains(CssConstants.CROSS)) {
                float horCrossCenterIndent = verticalIndent - 12;
                float verCrossCenterIndent = horizontalIndent - 12;
                float x;
                float y;
                canvas.SaveState().SetLineWidth(0.1f);
                x = mediaBox.GetWidth() / 2;
                y = mediaBox.GetHeight() - horCrossCenterIndent;
                DrawCross(canvas, x, y, true);
                x = mediaBox.GetWidth() / 2;
                y = horCrossCenterIndent;
                DrawCross(canvas, x, y, true);
                x = verCrossCenterIndent;
                y = mediaBox.GetHeight() / 2;
                DrawCross(canvas, x, y, false);
                x = mediaBox.GetWidth() - verCrossCenterIndent;
                y = mediaBox.GetHeight() / 2;
                DrawCross(canvas, x, y, false);
                canvas.RestoreState();
            }
            if (page.GetDocument().IsTagged()) {
                canvas.CloseTag();
            }
        }

        /// <summary>
        /// Draws a cross (used in the
        /// <see cref="DrawMarks(iText.Kernel.Pdf.PdfPage)"/>
        /// method).
        /// </summary>
        /// <param name="canvas">the canvas to draw on</param>
        /// <param name="x">the x value</param>
        /// <param name="y">the y value</param>
        /// <param name="horizontalCross">true if horizontal</param>
        private void DrawCross(PdfCanvas canvas, float x, float y, bool horizontalCross) {
            float xLineHalf;
            float yLineHalf;
            float circleR = 6;
            if (horizontalCross) {
                xLineHalf = 30;
                yLineHalf = 12;
            }
            else {
                xLineHalf = 12;
                yLineHalf = 30;
            }
            canvas.MoveTo(x - xLineHalf, y).LineTo(x + xLineHalf, y).MoveTo(x, y - yLineHalf).LineTo(x, y + yLineHalf);
            canvas.Circle(x, y, circleR);
            canvas.Stroke();
        }

        /// <summary>Draws page background and borders.</summary>
        /// <param name="page">the page</param>
        private void DrawPageBackgroundAndBorders(PdfPage page) {
            iText.Layout.Canvas canvas = new iText.Layout.Canvas(new PdfCanvas(page), page.GetDocument(), page.GetBleedBox
                ());
            canvas.EnableAutoTagging(page);
            canvas.Add(pageBackgroundSimulation);
            canvas.Close();
            canvas = new iText.Layout.Canvas(new PdfCanvas(page), page.GetDocument(), page.GetTrimBox());
            canvas.EnableAutoTagging(page);
            canvas.Add(pageBordersSimulation);
            canvas.Close();
        }

        /// <summary>Draws margin boxes.</summary>
        /// <param name="pageNumber">the page</param>
        /// <param name="pdfDocument">
        /// the
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// to which content is written
        /// </param>
        /// <param name="documentRenderer">the document renderer</param>
        private void DrawMarginBoxes(int pageNumber, PdfDocument pdfDocument, DocumentRenderer documentRenderer) {
            if (properties.GetResolvedPageMarginBoxes().IsEmpty()) {
                return;
            }
            PdfPage page = pdfDocument.GetPage(pageNumber);
            foreach (PageMarginBoxContextNode marginBoxContentNode in properties.GetResolvedPageMarginBoxes()) {
                IElement curBoxElement = ProcessMarginBoxContent(marginBoxContentNode, pageNumber, context);
                IRenderer renderer = curBoxElement.CreateRendererSubTree();
                RemoveAreaBreaks(renderer);
                renderer.SetParent(documentRenderer);
                bool isTagged = pdfDocument.IsTagged();
                if (isTagged) {
                    LayoutTaggingHelper taggingHelper = renderer.GetProperty<LayoutTaggingHelper>(Property.TAGGING_HELPER);
                    LayoutTaggingHelper.AddTreeHints(taggingHelper, renderer);
                }
                LayoutResult result = renderer.Layout(new LayoutContext(new LayoutArea(pageNumber, marginBoxContentNode.GetPageMarginBoxRectangle
                    ())));
                IRenderer rendererToDraw = result.GetStatus() == LayoutResult.FULL ? renderer : result.GetSplitRenderer();
                if (rendererToDraw != null) {
                    TagTreePointer tagPointer = null;
                    TagTreePointer backupPointer = null;
                    PdfPage backupPage = null;
                    if (isTagged) {
                        tagPointer = pdfDocument.GetTagStructureContext().GetAutoTaggingPointer();
                        backupPage = tagPointer.GetCurrentPage();
                        backupPointer = new TagTreePointer(tagPointer);
                        tagPointer.MoveToRoot();
                        tagPointer.SetPageForTagging(page);
                    }
                    rendererToDraw.SetParent(documentRenderer).Draw(new DrawContext(page.GetDocument(), new PdfCanvas(page), isTagged
                        ));
                    if (isTagged) {
                        tagPointer.SetPageForTagging(backupPage);
                        tagPointer.MoveToPointer(backupPointer);
                    }
                }
                else {
                    // marginBoxElements have overflow property set to HIDDEN, therefore it is not expected to neither get
                    // LayoutResult other than FULL nor get no split renderer (result NOTHING) even if result is not FULL
                    ILog logger = LogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Layout.PageContextProcessor));
                    logger.Error(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.PAGE_MARGIN_BOX_CONTENT_CANNOT_BE_DRAWN
                        , marginBoxContentNode.GetMarginBoxName()));
                }
            }
        }

        /// <summary>Parses the marks.</summary>
        /// <param name="marksStr">
        /// a
        /// <see cref="System.String"/>
        /// value defining the marks
        /// </param>
        /// <returns>
        /// a
        /// <see cref="Java.Util.Set{E}"/>
        /// of mark values
        /// </returns>
        private static ICollection<String> ParseMarks(String marksStr) {
            ICollection<String> marks = new HashSet<String>();
            if (marksStr == null) {
                return marks;
            }
            String[] split = iText.IO.Util.StringUtil.Split(marksStr, " ");
            foreach (String mark in split) {
                if (CssConstants.CROP.Equals(mark) || CssConstants.CROSS.Equals(mark)) {
                    marks.Add(mark);
                }
                else {
                    marks.Clear();
                    break;
                }
            }
            return marks;
        }

        /// <summary>Parses the margins.</summary>
        /// <param name="styles">
        /// a
        /// <see cref="System.Collections.IDictionary{K, V}"/>
        /// containing the styles
        /// </param>
        /// <param name="em">a measurement expressed in em</param>
        /// <param name="rem">a measurement expressed in rem (root em)</param>
        private void ParseMargins(IDictionary<String, String> styles, float em, float rem, float[] defaultMarginValues
            ) {
            PageSize pageSize = GetPageSize();
            margins = PageMarginBoxCssApplier.ParseBoxProps(styles, em, rem, defaultMarginValues, pageSize, CssConstants
                .MARGIN_TOP, CssConstants.MARGIN_RIGHT, CssConstants.MARGIN_BOTTOM, CssConstants.MARGIN_LEFT);
        }

        /// <summary>Parses the paddings.</summary>
        /// <param name="styles">
        /// a
        /// <see cref="System.Collections.IDictionary{K, V}"/>
        /// containing the styles
        /// </param>
        /// <param name="em">a measurement expressed in em</param>
        /// <param name="rem">a measurement expressed in rem (root em)</param>
        private void ParsePaddings(IDictionary<String, String> styles, float em, float rem) {
            float defaultPadding = 0;
            PageSize pageSize = GetPageSize();
            paddings = PageMarginBoxCssApplier.ParseBoxProps(styles, em, rem, new float[] { defaultPadding, defaultPadding
                , defaultPadding, defaultPadding }, pageSize, CssConstants.PADDING_TOP, CssConstants.PADDING_RIGHT, CssConstants
                .PADDING_BOTTOM, CssConstants.PADDING_LEFT);
        }

        /// <summary>Parses the borders.</summary>
        /// <param name="styles">
        /// a
        /// <see cref="System.Collections.IDictionary{K, V}"/>
        /// containing the styles
        /// </param>
        /// <param name="em">a measurement expressed in em</param>
        /// <param name="rem">a measurement expressed in rem (root em)</param>
        private void ParseBorders(IDictionary<String, String> styles, float em, float rem) {
            borders = BorderStyleApplierUtil.GetBordersArray(styles, em, rem);
        }

        /// <summary>Creates the page simulation elements.</summary>
        /// <param name="styles">
        /// a
        /// <see cref="System.Collections.IDictionary{K, V}"/>
        /// containing the styles
        /// </param>
        /// <param name="context">the processor context</param>
        private void CreatePageSimulationElements(IDictionary<String, String> styles, ProcessorContext context) {
            pageBackgroundSimulation = new Div().SetFillAvailableArea(true);
            BackgroundApplierUtil.ApplyBackground(styles, context, pageBackgroundSimulation);
            pageBackgroundSimulation.GetAccessibilityProperties().SetRole(StandardRoles.ARTIFACT);
            pageBordersSimulation = new Div().SetFillAvailableArea(true);
            pageBordersSimulation.SetMargins(margins[0], margins[1], margins[2], margins[3]);
            pageBordersSimulation.SetBorderTop(borders[0]);
            pageBordersSimulation.SetBorderRight(borders[1]);
            pageBordersSimulation.SetBorderBottom(borders[2]);
            pageBordersSimulation.SetBorderLeft(borders[3]);
            pageBordersSimulation.GetAccessibilityProperties().SetRole(StandardRoles.ARTIFACT);
        }

        /// <summary>Creates the margin boxes elements.</summary>
        /// <param name="resolvedPageMarginBoxes">the resolved page margin boxes</param>
        private void PrepareMarginBoxesSizing(IList<PageMarginBoxContextNode> resolvedPageMarginBoxes) {
            Rectangle[] marginBoxRectangles = CalculateMarginBoxRectangles(resolvedPageMarginBoxes);
            foreach (PageMarginBoxContextNode marginBoxContentNode in resolvedPageMarginBoxes) {
                if (marginBoxContentNode.ChildNodes().IsEmpty()) {
                    // margin box node shall not be added to resolvedPageMarginBoxes if it's kids were not resolved from content
                    throw new InvalidOperationException();
                }
                int marginBoxInd = MapMarginBoxNameToIndex(marginBoxContentNode.GetMarginBoxName());
                marginBoxContentNode.SetPageMarginBoxRectangle(marginBoxRectangles[marginBoxInd]);
                marginBoxContentNode.SetContainingBlockForMarginBox(CalculateContainingBlockSizesForMarginBox(marginBoxInd
                    , marginBoxRectangles[marginBoxInd]));
            }
        }

        private IElement ProcessMarginBoxContent(PageMarginBoxContextNode marginBoxContentNode, int pageNumber, ProcessorContext
             context) {
            IElementNode dummyMarginBoxNode = new PageMarginBoxDummyElement();
            dummyMarginBoxNode.SetStyles(marginBoxContentNode.GetStyles());
            ITagWorker marginBoxWorker = context.GetTagWorkerFactory().GetTagWorker(dummyMarginBoxNode, context);
            for (int i = 0; i < marginBoxContentNode.ChildNodes().Count; i++) {
                INode childNode = marginBoxContentNode.ChildNodes()[i];
                if (childNode is ITextNode) {
                    String text = ((ITextNode)marginBoxContentNode.ChildNodes()[i]).WholeText();
                    marginBoxWorker.ProcessContent(text, context);
                }
                else {
                    if (childNode is IElementNode) {
                        ITagWorker childTagWorker = context.GetTagWorkerFactory().GetTagWorker((IElementNode)childNode, context);
                        if (childTagWorker != null) {
                            childTagWorker.ProcessEnd((IElementNode)childNode, context);
                            marginBoxWorker.ProcessTagChild(childTagWorker, context);
                        }
                    }
                    else {
                        if (childNode is PageMarginRunningElementNode) {
                            PageMarginRunningElementNode runningElementNode = (PageMarginRunningElementNode)childNode;
                            RunningElementContainer runningElement = context.GetCssContext().GetRunningManager().GetRunningElement(runningElementNode
                                .GetRunningElementName(), runningElementNode.GetRunningElementOccurrence(), pageNumber);
                            if (runningElement != null) {
                                marginBoxWorker.ProcessTagChild(runningElement.GetProcessedElementWorker(), context);
                            }
                        }
                        else {
                            LogManager.GetLogger(GetType()).Error(iText.Html2pdf.LogMessageConstant.UNKNOWN_MARGIN_BOX_CHILD);
                        }
                    }
                }
            }
            marginBoxWorker.ProcessEnd(dummyMarginBoxNode, context);
            if (!(marginBoxWorker.GetElementResult() is IElement)) {
                throw new InvalidOperationException("Custom tag worker implementation for margin boxes shall return IElement for #getElementResult() call."
                    );
            }
            ICssApplier cssApplier = context.GetCssApplierFactory().GetCssApplier(dummyMarginBoxNode);
            cssApplier.Apply(context, marginBoxContentNode, marginBoxWorker);
            return (IElement)marginBoxWorker.GetElementResult();
        }

        /// <summary>Calculate margin box rectangles.</summary>
        /// <param name="resolvedPageMarginBoxes">the resolved page margin boxes</param>
        /// <returns>
        /// an array of
        /// <see cref="iText.Kernel.Geom.Rectangle"/>
        /// values
        /// </returns>
        private Rectangle[] CalculateMarginBoxRectangles(IList<PageMarginBoxContextNode> resolvedPageMarginBoxes) {
            // TODO It's a very basic implementation for now. In future resolve rectangles based on presence of certain margin boxes,
            //      also height and width properties should be taken into account.
            float topMargin = margins[0];
            float rightMargin = margins[1];
            float bottomMargin = margins[2];
            float leftMargin = margins[3];
            Rectangle withoutMargins = pageSize.Clone().ApplyMargins(topMargin, rightMargin, bottomMargin, leftMargin, 
                false);
            float topBottomMarginWidth = withoutMargins.GetWidth() / 3;
            float leftRightMarginHeight = withoutMargins.GetHeight() / 3;
            Rectangle[] hardcodedBoxRectangles = new Rectangle[] { new Rectangle(0, withoutMargins.GetTop(), leftMargin
                , topMargin), new Rectangle(rightMargin, withoutMargins.GetTop(), topBottomMarginWidth, topMargin), new 
                Rectangle(rightMargin + topBottomMarginWidth, withoutMargins.GetTop(), topBottomMarginWidth, topMargin
                ), new Rectangle(withoutMargins.GetRight() - topBottomMarginWidth, withoutMargins.GetTop(), topBottomMarginWidth
                , topMargin), new Rectangle(withoutMargins.GetRight(), withoutMargins.GetTop(), topBottomMarginWidth, 
                topMargin), new Rectangle(withoutMargins.GetRight(), withoutMargins.GetTop() - leftRightMarginHeight, 
                rightMargin, leftRightMarginHeight), new Rectangle(withoutMargins.GetRight(), withoutMargins.GetBottom
                () + leftRightMarginHeight, rightMargin, leftRightMarginHeight), new Rectangle(withoutMargins.GetRight
                (), withoutMargins.GetBottom(), rightMargin, leftRightMarginHeight), new Rectangle(withoutMargins.GetRight
                (), 0, rightMargin, bottomMargin), new Rectangle(withoutMargins.GetRight() - topBottomMarginWidth, 0, 
                topBottomMarginWidth, bottomMargin), new Rectangle(rightMargin + topBottomMarginWidth, 0, topBottomMarginWidth
                , bottomMargin), new Rectangle(rightMargin, 0, topBottomMarginWidth, bottomMargin), new Rectangle(0, 0
                , leftMargin, bottomMargin), new Rectangle(0, withoutMargins.GetBottom(), leftMargin, leftRightMarginHeight
                ), new Rectangle(0, withoutMargins.GetBottom() + leftRightMarginHeight, leftMargin, leftRightMarginHeight
                ), new Rectangle(0, withoutMargins.GetTop() - leftRightMarginHeight, leftMargin, leftRightMarginHeight
                ) };
            return hardcodedBoxRectangles;
        }

        /// <summary>Calculate containing block sizes for margin box.</summary>
        /// <param name="marginBoxInd">the margin box index</param>
        /// <param name="pageMarginBoxRectangle">
        /// a
        /// <see cref="iText.Kernel.Geom.Rectangle"/>
        /// defining dimensions of the page margin box corresponding to the given index
        /// </param>
        /// <returns>the corresponding rectangle</returns>
        private Rectangle CalculateContainingBlockSizesForMarginBox(int marginBoxInd, Rectangle pageMarginBoxRectangle
            ) {
            if (marginBoxInd == 0 || marginBoxInd == 4 || marginBoxInd == 8 || marginBoxInd == 12) {
                return pageMarginBoxRectangle;
            }
            Rectangle withoutMargins = pageSize.Clone().ApplyMargins(margins[0], margins[1], margins[2], margins[3], false
                );
            if (marginBoxInd < 4) {
                return new Rectangle(withoutMargins.GetWidth(), margins[0]);
            }
            else {
                if (marginBoxInd < 8) {
                    return new Rectangle(margins[1], withoutMargins.GetHeight());
                }
                else {
                    if (marginBoxInd < 12) {
                        return new Rectangle(withoutMargins.GetWidth(), margins[2]);
                    }
                    else {
                        return new Rectangle(margins[3], withoutMargins.GetWidth());
                    }
                }
            }
        }

        /// <summary>Maps a margin box name to an index.</summary>
        /// <param name="marginBoxName">the margin box name</param>
        /// <returns>the index corresponding with the margin box name</returns>
        private int MapMarginBoxNameToIndex(String marginBoxName) {
            switch (marginBoxName) {
                case CssRuleName.TOP_LEFT_CORNER: {
                    return 0;
                }

                case CssRuleName.TOP_LEFT: {
                    return 1;
                }

                case CssRuleName.TOP_CENTER: {
                    return 2;
                }

                case CssRuleName.TOP_RIGHT: {
                    return 3;
                }

                case CssRuleName.TOP_RIGHT_CORNER: {
                    return 4;
                }

                case CssRuleName.RIGHT_TOP: {
                    return 5;
                }

                case CssRuleName.RIGHT_MIDDLE: {
                    return 6;
                }

                case CssRuleName.RIGHT_BOTTOM: {
                    return 7;
                }

                case CssRuleName.BOTTOM_RIGHT_CORNER: {
                    return 8;
                }

                case CssRuleName.BOTTOM_RIGHT: {
                    return 9;
                }

                case CssRuleName.BOTTOM_CENTER: {
                    return 10;
                }

                case CssRuleName.BOTTOM_LEFT: {
                    return 11;
                }

                case CssRuleName.BOTTOM_LEFT_CORNER: {
                    return 12;
                }

                case CssRuleName.LEFT_BOTTOM: {
                    return 13;
                }

                case CssRuleName.LEFT_MIDDLE: {
                    return 14;
                }

                case CssRuleName.LEFT_TOP: {
                    return 15;
                }
            }
            return -1;
        }

        /// <summary>Gets rid of all page breaks that might have occurred inside page margin boxes because of the running elements.
        ///     </summary>
        /// <param name="renderer">root renderer of renderers subtree</param>
        private static void RemoveAreaBreaks(IRenderer renderer) {
            IList<IRenderer> areaBreaks = null;
            foreach (IRenderer child in renderer.GetChildRenderers()) {
                if (child is AreaBreakRenderer) {
                    if (areaBreaks == null) {
                        areaBreaks = new List<IRenderer>();
                    }
                    areaBreaks.Add(child);
                }
                else {
                    RemoveAreaBreaks(child);
                }
            }
            if (areaBreaks != null) {
                renderer.GetChildRenderers().RemoveAll(areaBreaks);
            }
        }
    }
}
