/*
This file is part of the iText (R) project.
Copyright (c) 1998-2018 iText Group NV
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
using System.Text.RegularExpressions;
using Common.Logging;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Apply.Impl;
using iText.Html2pdf.Css.Apply.Util;
using iText.Html2pdf.Css.Page;
using iText.IO.Util;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Tagging;
using iText.Kernel.Pdf.Tagutils;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Font;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using iText.Layout.Splitting;
using iText.Layout.Tagging;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Page;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

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
            Rectangle[] marginBoxRectanglesSpec = CalculateMarginBoxRectanglesSpec(resolvedPageMarginBoxes);
            foreach (PageMarginBoxContextNode marginBoxContentNode in resolvedPageMarginBoxes) {
                if (marginBoxContentNode.ChildNodes().IsEmpty()) {
                    // margin box node shall not be added to resolvedPageMarginBoxes if it's kids were not resolved from content
                    throw new InvalidOperationException();
                }
                int marginBoxInd = MapMarginBoxNameToIndex(marginBoxContentNode.GetMarginBoxName());
                marginBoxContentNode.SetPageMarginBoxRectangle(marginBoxRectanglesSpec[marginBoxInd]);
                marginBoxContentNode.SetContainingBlockForMarginBox(CalculateContainingBlockSizesForMarginBox(marginBoxInd
                    , marginBoxRectanglesSpec[marginBoxInd]));
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

        internal virtual Rectangle[] CalculateMarginBoxRectanglesSpec(IList<PageMarginBoxContextNode> resolvedPageMarginBoxes
            ) {
            float topMargin = margins[0];
            float rightMargin = margins[1];
            float bottomMargin = margins[2];
            float leftMargin = margins[3];
            Rectangle withoutMargins = pageSize.Clone().ApplyMargins(topMargin, rightMargin, bottomMargin, leftMargin, 
                false);
            IDictionary<String, PageMarginBoxContextNode> resolvedPMBMap = new Dictionary<String, PageMarginBoxContextNode
                >();
            foreach (PageMarginBoxContextNode node in resolvedPageMarginBoxes) {
                resolvedPMBMap.Put(node.GetMarginBoxName(), node);
            }
            //Define corner boxes
            Rectangle tlc = new Rectangle(0, withoutMargins.GetTop(), leftMargin, topMargin);
            Rectangle trc = new Rectangle(withoutMargins.GetRight(), withoutMargins.GetTop(), rightMargin, topMargin);
            Rectangle blc = new Rectangle(0, 0, leftMargin, bottomMargin);
            Rectangle brc = new Rectangle(withoutMargins.GetRight(), 0, rightMargin, bottomMargin);
            //Top calculation
            //Gather necessary input
            float[] topWidthResults = CalculatePageMarginBoxDimensions(RetrievePageMarginBoxWidths(resolvedPMBMap.Get(
                CssRuleName.TOP_LEFT), withoutMargins.GetWidth()), RetrievePageMarginBoxWidths(resolvedPMBMap.Get(CssRuleName
                .TOP_CENTER), withoutMargins.GetWidth()), RetrievePageMarginBoxWidths(resolvedPMBMap.Get(CssRuleName.TOP_RIGHT
                ), withoutMargins.GetWidth()), withoutMargins.GetWidth());
            float centerOrMiddleCoord = GetStartCoordForCenterOrMiddleBox(withoutMargins.GetWidth(), topWidthResults, 
                withoutMargins.GetLeft());
            Rectangle[] topResults = new Rectangle[] { new Rectangle(withoutMargins.GetLeft(), withoutMargins.GetTop()
                , topWidthResults[0], topMargin), new Rectangle(centerOrMiddleCoord, withoutMargins.GetTop(), topWidthResults
                [1], topMargin), new Rectangle(withoutMargins.GetRight() - topWidthResults[2], withoutMargins.GetTop()
                , topWidthResults[2], topMargin) };
            //Right calculation
            float[] rightHeightResults = CalculatePageMarginBoxDimensions(RetrievePageMarginBoxHeights(resolvedPMBMap.
                Get(CssRuleName.RIGHT_TOP), rightMargin, withoutMargins.GetHeight()), RetrievePageMarginBoxHeights(resolvedPMBMap
                .Get(CssRuleName.RIGHT_MIDDLE), rightMargin, withoutMargins.GetHeight()), RetrievePageMarginBoxHeights
                (resolvedPMBMap.Get(CssRuleName.RIGHT_BOTTOM), rightMargin, withoutMargins.GetHeight()), withoutMargins
                .GetHeight());
            centerOrMiddleCoord = GetStartCoordForCenterOrMiddleBox(withoutMargins.GetHeight(), rightHeightResults, withoutMargins
                .GetBottom());
            Rectangle[] rightResults = new Rectangle[] { new Rectangle(withoutMargins.GetRight(), withoutMargins.GetTop
                () - rightHeightResults[0], rightMargin, rightHeightResults[0]), new Rectangle(withoutMargins.GetRight
                (), centerOrMiddleCoord, rightMargin, rightHeightResults[1]), new Rectangle(withoutMargins.GetRight(), 
                withoutMargins.GetBottom(), rightMargin, rightHeightResults[2]) };
            //Bottom calculation
            float[] bottomWidthResults = CalculatePageMarginBoxDimensions(RetrievePageMarginBoxWidths(resolvedPMBMap.Get
                (CssRuleName.BOTTOM_LEFT), withoutMargins.GetWidth()), RetrievePageMarginBoxWidths(resolvedPMBMap.Get(
                CssRuleName.BOTTOM_CENTER), withoutMargins.GetWidth()), RetrievePageMarginBoxWidths(resolvedPMBMap.Get
                (CssRuleName.BOTTOM_RIGHT), withoutMargins.GetWidth()), withoutMargins.GetWidth());
            centerOrMiddleCoord = GetStartCoordForCenterOrMiddleBox(withoutMargins.GetWidth(), bottomWidthResults, withoutMargins
                .GetLeft());
            Rectangle[] bottomResults = new Rectangle[] { new Rectangle(withoutMargins.GetRight() - bottomWidthResults
                [2], 0, bottomWidthResults[2], bottomMargin), new Rectangle(centerOrMiddleCoord, 0, bottomWidthResults
                [1], bottomMargin), new Rectangle(withoutMargins.GetLeft(), 0, bottomWidthResults[0], bottomMargin) };
            //Left calculation
            float[] leftHeightResults = CalculatePageMarginBoxDimensions(RetrievePageMarginBoxHeights(resolvedPMBMap.Get
                (CssRuleName.LEFT_TOP), leftMargin, withoutMargins.GetHeight()), RetrievePageMarginBoxHeights(resolvedPMBMap
                .Get(CssRuleName.LEFT_MIDDLE), leftMargin, withoutMargins.GetHeight()), RetrievePageMarginBoxHeights(resolvedPMBMap
                .Get(CssRuleName.LEFT_BOTTOM), leftMargin, withoutMargins.GetHeight()), withoutMargins.GetHeight());
            centerOrMiddleCoord = GetStartCoordForCenterOrMiddleBox(withoutMargins.GetHeight(), leftHeightResults, withoutMargins
                .GetBottom());
            Rectangle[] leftResults = new Rectangle[] { new Rectangle(0, withoutMargins.GetTop() - leftHeightResults[0
                ], leftMargin, leftHeightResults[0]), new Rectangle(0, centerOrMiddleCoord, leftMargin, leftHeightResults
                [1]), new Rectangle(0, withoutMargins.GetBottom(), leftMargin, leftHeightResults[2]) };
            //Group & return results
            Rectangle[] groupedRectangles = new Rectangle[] { tlc, topResults[0], topResults[1], topResults[2], trc, rightResults
                [0], rightResults[1], rightResults[2], brc, bottomResults[0], bottomResults[1], bottomResults[2], blc, 
                leftResults[2], leftResults[1], leftResults[0] };
            return groupedRectangles;
        }

        private float GetStartCoordForCenterOrMiddleBox(float availableDimension, float[] dimensionResults, float 
            offset) {
            return offset + (availableDimension - dimensionResults[1]) / 2;
        }

        /// <summary>See the algorithm detailed at https://www.w3.org/TR/css3-page/#margin-dimension</summary>
        /// <param name="dimA"/>
        /// <param name="dimB"/>
        /// <param name="dimC"/>
        /// <param name="availableDimension"/>
        /// <returns/>
        internal virtual float[] CalculatePageMarginBoxDimensions(PageContextProcessor.Dimensions dimA, PageContextProcessor.Dimensions
             dimB, PageContextProcessor.Dimensions dimC, float availableDimension) {
            float maxContentDimensionA;
            float minContentDimensionA;
            float maxContentDimensionB;
            float minContentDimensionB;
            float maxContentDimensionC;
            float minContentDimensionC;
            float[] dimensions = new float[3];
            if (dimA == null && dimB == null && dimC == null) {
                return dimensions;
            }
            //Calculate widths
            //Check if B is present
            if (dimB == null) {
                //Single box present
                if (dimA == null) {
                    if (dimC.IsAutoDimension()) {
                        //Allocate everything to C
                        return new float[] { 0, 0, availableDimension };
                    }
                    else {
                        return new float[] { 0, 0, dimC.dimension };
                    }
                }
                if (dimC == null) {
                    if (dimA.IsAutoDimension()) {
                        //Allocate everything to A
                        return new float[] { availableDimension, 0, 0 };
                    }
                    else {
                        return new float[] { dimA.dimension, 0, 0 };
                    }
                }
                if (dimA.IsAutoDimension() && dimC.IsAutoDimension()) {
                    //Gather input
                    maxContentDimensionA = dimA.maxContentDimension;
                    minContentDimensionA = dimA.minContentDimension;
                    maxContentDimensionC = dimC.maxContentDimension;
                    minContentDimensionC = dimC.minContentDimension;
                    float[] distributedWidths = DistributeDimensionBetweenTwoBoxes(maxContentDimensionA, minContentDimensionA, 
                        maxContentDimensionC, minContentDimensionC, availableDimension);
                    dimensions = new float[] { distributedWidths[0], 0f, distributedWidths[1] };
                }
                else {
                    if (!dimA.IsAutoDimension()) {
                        dimensions[0] = dimA.dimension;
                    }
                    else {
                        dimensions[0] = availableDimension - dimC.dimension;
                    }
                    if (!dimC.IsAutoDimension()) {
                        dimensions[2] = dimC.dimension;
                    }
                    else {
                        dimensions[2] = availableDimension - dimA.dimension;
                    }
                }
            }
            else {
                //Check for edge cases
                if (dimA != null) {
                    maxContentDimensionA = dimA.maxContentDimension;
                    minContentDimensionA = dimA.minContentDimension;
                }
                else {
                    maxContentDimensionA = 0;
                    minContentDimensionA = 0;
                }
                if (dimC != null) {
                    maxContentDimensionC = dimC.maxContentDimension;
                    minContentDimensionC = dimC.minContentDimension;
                }
                else {
                    maxContentDimensionC = 0;
                    minContentDimensionC = 0;
                }
                //Construct box AC
                float maxContentWidthAC = maxContentDimensionA + maxContentDimensionC;
                float minContentWidthAC = minContentDimensionA + minContentDimensionC;
                //Determine width box B
                maxContentDimensionB = dimB.maxContentDimension;
                minContentDimensionB = dimB.minContentDimension;
                float[] distributedDimensions = DistributeDimensionBetweenTwoBoxes(maxContentDimensionB, minContentDimensionB
                    , maxContentWidthAC, minContentWidthAC, availableDimension);
                //Determine width boxes A & C
                float newAvailableDimension = (availableDimension - distributedDimensions[0]) / 2;
                float[] distributedWidthsAC = new float[] { Math.Min(minContentDimensionA, newAvailableDimension), Math.Min
                    (minContentDimensionC, newAvailableDimension) };
                dimensions = new float[] { distributedWidthsAC[0], distributedDimensions[0], distributedWidthsAC[1] };
                SetManualDimension(dimA, dimensions, 0);
                SetManualDimension(dimB, dimensions, 1);
                SetManualDimension(dimC, dimensions, 2);
            }
            if (RecalculateIfNecessary(dimA, dimensions, 0) || RecalculateIfNecessary(dimB, dimensions, 1) || RecalculateIfNecessary
                (dimC, dimensions, 2)) {
                return CalculatePageMarginBoxDimensions(dimA, dimB, dimC, availableDimension);
            }
            return dimensions;
        }

        private void SetManualDimension(PageContextProcessor.Dimensions dim, float[] dimensions, int index) {
            if (dim != null && !dim.IsAutoDimension()) {
                dimensions[index] = dim.dimension;
            }
        }

        private bool RecalculateIfNecessary(PageContextProcessor.Dimensions dim, float[] dimensions, int index) {
            if (dim != null) {
                if (dimensions[index] < dim.minDimension && dim.IsAutoDimension()) {
                    dim.dimension = dim.minDimension;
                    return true;
                }
                if (dimensions[index] > dim.maxDimension && dim.IsAutoDimension()) {
                    dim.dimension = dim.maxDimension;
                    return true;
                }
            }
            return false;
        }

        internal virtual PageContextProcessor.Dimensions RetrievePageMarginBoxWidths(PageMarginBoxContextNode pmbcNode
            , float maxWidth) {
            if (pmbcNode == null) {
                return null;
            }
            else {
                return new PageContextProcessor.WidthDimensions(this, pmbcNode, maxWidth);
            }
        }

        internal virtual PageContextProcessor.Dimensions RetrievePageMarginBoxHeights(PageMarginBoxContextNode pmbcNode
            , float marginWidth, float maxHeight) {
            if (pmbcNode == null) {
                return null;
            }
            else {
                return new PageContextProcessor.HeightDimensions(this, pmbcNode, marginWidth, maxHeight);
            }
        }

        internal class Dimensions {
            internal float dimension;

            internal float minDimension;

            internal float maxDimension;

            internal float minContentDimension;

            internal float maxContentDimension;

            internal Dimensions(PageContextProcessor _enclosing) {
                this._enclosing = _enclosing;
                this.dimension = -1;
                this.minDimension = 0;
                this.minContentDimension = 0;
                this.maxDimension = float.MaxValue;
                this.maxContentDimension = float.MaxValue;
            }

            internal virtual bool IsAutoDimension() {
                return this.dimension == -1;
            }

            internal virtual float ParseDimension(CssContextNode node, String content, float maxAvailableDimension) {
                String numberRegex = "(\\d+(\\.\\d*)?)";
                String units = "(in|cm|mm|pt|pc|px|%|em|ex)";
                Match matcher = iText.IO.Util.StringUtil.Match(iText.IO.Util.StringUtil.RegexCompile(numberRegex + units), 
                    content);
                if (matcher.Find()) {
                    float value = float.Parse(iText.IO.Util.StringUtil.Group(matcher, 1), System.Globalization.CultureInfo.InvariantCulture
                        );
                    String unit = iText.IO.Util.StringUtil.Group(matcher, 3);
                    switch (unit) {
                        case "pt": {
                            break;
                        }

                        case "%": {
                            value *= maxAvailableDimension / 100;
                            break;
                        }

                        case "pc": {
                            value *= 12;
                            break;
                        }

                        case "px": {
                            value *= 0.75;
                            break;
                        }

                        case "in": {
                            value *= 72;
                            break;
                        }

                        case "cm": {
                            value *= 28.3465;
                            break;
                        }

                        case "mm": {
                            value *= 2.83465;
                            break;
                        }

                        case "em": {
                            float fontSize = this._enclosing.GetFontSize(node);
                            value *= fontSize;
                            break;
                        }

                        case "ex": {
                            // Use 0.5em as heuristic of x-height. Look CSS 2.1 Spec
                            float fontSize = this._enclosing.GetFontSize(node);
                            value *= 0.5 * fontSize;
                            break;
                        }

                        default: {
                            value = 0;
                            break;
                        }
                    }
                    return value;
                }
                return 0;
            }

            private readonly PageContextProcessor _enclosing;
        }

        internal class WidthDimensions : PageContextProcessor.Dimensions {
            internal WidthDimensions(PageContextProcessor _enclosing, CssContextNode node, float maxWidth)
                : base(_enclosing) {
                this._enclosing = _enclosing;
                String width = node.GetStyles().Get(CssConstants.WIDTH);
                if (width != null && !width.Equals("auto")) {
                    this.dimension = this.ParseDimension(node, width, maxWidth);
                }
                this.minDimension = this.GetMinWidth(node, maxWidth);
                this.maxDimension = this.GetMaxWidth(node, maxWidth);
                this.minContentDimension = this._enclosing.GetMinContentWidth((PageMarginBoxContextNode)node);
                this.maxContentDimension = this._enclosing.GetMaxContentWidth((PageMarginBoxContextNode)node);
            }

            internal virtual float GetMinWidth(CssContextNode node, float maxAvailableWidth) {
                String content = node.GetStyles().Get(CssConstants.MIN_WIDTH);
                if (content == null) {
                    return 0;
                }
                content = content.ToLowerInvariant().Trim();
                if (content.Equals("inherit")) {
                    if (node.ParentNode() is CssContextNode) {
                        return this.GetMinWidth((CssContextNode)node.ParentNode(), maxAvailableWidth);
                    }
                    return 0;
                }
                return this.ParseDimension(node, content, maxAvailableWidth);
            }

            internal virtual float GetMaxWidth(CssContextNode node, float maxAvailableWidth) {
                String content = node.GetStyles().Get(CssConstants.MAX_WIDTH);
                if (content == null) {
                    return float.MaxValue;
                }
                content = content.ToLowerInvariant().Trim();
                if (content.Equals("inherit")) {
                    if (node.ParentNode() is CssContextNode) {
                        return this.GetMaxWidth((CssContextNode)node.ParentNode(), maxAvailableWidth);
                    }
                    return float.MaxValue;
                }
                float dim = this.ParseDimension(node, content, maxAvailableWidth);
                if (dim == 0) {
                    return float.MaxValue;
                }
                return dim;
            }

            private readonly PageContextProcessor _enclosing;
        }

        internal class HeightDimensions : PageContextProcessor.Dimensions {
            internal HeightDimensions(PageContextProcessor _enclosing, CssContextNode pmbcNode, float width, float maxHeight
                )
                : base(_enclosing) {
                this._enclosing = _enclosing;
                String height = pmbcNode.GetStyles().Get(CssConstants.HEIGHT);
                if (height != null && !height.Equals("auto")) {
                    this.dimension = this.ParseDimension(pmbcNode, height, maxHeight);
                }
                this.minDimension = this.GetMinHeight(pmbcNode, maxHeight);
                this.maxDimension = this.GetMaxHeight(pmbcNode, maxHeight);
                this.minContentDimension = this._enclosing.GetMinContentHeight((PageMarginBoxContextNode)pmbcNode, width, 
                    maxHeight);
                this.maxContentDimension = this._enclosing.GetMaxContentHeight((PageMarginBoxContextNode)pmbcNode, width, 
                    maxHeight);
            }

            internal virtual float GetMinHeight(CssContextNode node, float maxAvailableHeight) {
                String content = node.GetStyles().Get(CssConstants.MIN_HEIGHT);
                if (content == null) {
                    return 0;
                }
                content = content.ToLowerInvariant().Trim();
                if (content.Equals("inherit")) {
                    if (node.ParentNode() is CssContextNode) {
                        return this.GetMinHeight((CssContextNode)node.ParentNode(), maxAvailableHeight);
                    }
                    return 0;
                }
                return this.ParseDimension(node, content, maxAvailableHeight);
            }

            internal virtual float GetMaxHeight(CssContextNode node, float maxAvailableHeight) {
                String content = node.GetStyles().Get(CssConstants.MIN_HEIGHT);
                if (content == null) {
                    return float.MaxValue;
                }
                content = content.ToLowerInvariant().Trim();
                if (content.Equals("inherit")) {
                    if (node.ParentNode() is CssContextNode) {
                        return this.GetMaxHeight((CssContextNode)node.ParentNode(), maxAvailableHeight);
                    }
                    return float.MaxValue;
                }
                float dim = this.ParseDimension(node, content, maxAvailableHeight);
                if (dim == 0) {
                    return float.MaxValue;
                }
                return dim;
            }

            private readonly PageContextProcessor _enclosing;
        }

        internal virtual float[] DistributeDimensionBetweenTwoBoxes(float maxContentDimensionA, float minContentDimensionA
            , float maxContentDimensionC, float minContentDimensionC, float availableDimension) {
            //calculate based on flex space
            //Determine flex factor
            float flexRatioA;
            float flexRatioC;
            float flexSpace;
            float distributedWidthA;
            float distributedWidthC;
            if (maxContentDimensionA + maxContentDimensionC < availableDimension) {
                if (maxContentDimensionA == 0 && maxContentDimensionC == 0) {
                    //TODO(DEVSIX-1050) float comparison to zero, revisit
                    flexRatioA = 1;
                    flexRatioC = 1;
                }
                else {
                    flexRatioA = maxContentDimensionA / (maxContentDimensionA + maxContentDimensionC);
                    flexRatioC = maxContentDimensionC / (maxContentDimensionA + maxContentDimensionC);
                }
                flexSpace = availableDimension - (maxContentDimensionA + maxContentDimensionC);
                distributedWidthA = maxContentDimensionA + flexRatioA * flexSpace;
                distributedWidthC = maxContentDimensionC + flexRatioC * flexSpace;
            }
            else {
                if (minContentDimensionA + minContentDimensionC < availableDimension) {
                    float factorSum = (maxContentDimensionA - minContentDimensionA) + (maxContentDimensionC - minContentDimensionC
                        );
                    if (factorSum == 0) {
                        //TODO(DEVSIX-1050) float comparison to zero, revisit
                        flexRatioA = 1;
                        flexRatioC = 1;
                    }
                    else {
                        flexRatioA = (maxContentDimensionA - minContentDimensionA) / factorSum;
                        flexRatioC = (maxContentDimensionC - minContentDimensionC) / factorSum;
                    }
                    flexSpace = availableDimension - (minContentDimensionA + minContentDimensionC);
                    distributedWidthA = minContentDimensionA + flexRatioA * flexSpace;
                    distributedWidthC = minContentDimensionC + flexRatioC * flexSpace;
                }
                else {
                    if (minContentDimensionA == 0 && minContentDimensionC == 0) {
                        //TODO(DEVSIX-1050) float comparison to zero, revisit
                        flexRatioA = 1;
                        flexRatioC = 1;
                    }
                    else {
                        flexRatioA = minContentDimensionA / (minContentDimensionA + minContentDimensionC);
                        flexRatioC = minContentDimensionC / (minContentDimensionA + minContentDimensionC);
                    }
                    flexSpace = availableDimension - (minContentDimensionA + minContentDimensionC);
                    distributedWidthA = minContentDimensionA + flexRatioA * flexSpace;
                    distributedWidthC = minContentDimensionC + flexRatioC * flexSpace;
                }
            }
            return new float[] { distributedWidthA, distributedWidthC };
        }

        internal virtual float GetMaxContentWidth(PageMarginBoxContextNode pmbcNode) {
            //Check styles?
            //Simulate contents?
            //TODO(DEVSIX-1050): Consider complex non-purely text based contents
            String content = pmbcNode.GetStyles().Get(CssConstants.CONTENT);
            //Resolve font using context
            String fontFamilyName = pmbcNode.GetStyles().Get(CssConstants.FONT_FAMILY);
            float fontSize = GetFontSize(pmbcNode);
            FontProvider provider = this.context.GetFontProvider();
            FontCharacteristics fc = new FontCharacteristics();
            FontSelectorStrategy strategy = provider.GetStrategy(content, FontFamilySplitter.SplitFontFamily(fontFamilyName
                ), fc);
            strategy.NextGlyphs();
            PdfFont currentFont = strategy.GetCurrentFont();
            if (currentFont == null) {
                //TODO(DEVSIX-1050) Warn and use pdf default
                try {
                    currentFont = PdfFontFactory.CreateFont();
                }
                catch (System.IO.IOException) {
                }
            }
            //TODO throw exception further?
            return currentFont.GetWidth(content, fontSize);
        }

        internal virtual float GetMinContentWidth(PageMarginBoxContextNode node) {
            //TODO(DEVSIX-1050): reread spec to be certain that min-content-size does in fact mean the same as max content
            return GetMaxContentWidth(node);
        }

        internal virtual float GetMaxContentHeight(PageMarginBoxContextNode pmbcNode, float width, float maxAvailableHeight
            ) {
            //TODO(DEVSIX-1050): Consider complex non-purely text based contents
            String content = pmbcNode.GetStyles().Get(CssConstants.CONTENT);
            //Use iText layout engine to simulate
            //Resolve font using context
            String fontFamilyName = pmbcNode.GetStyles().Get(CssConstants.FONT_FAMILY);
            float fontSize = GetFontSize(pmbcNode);
            FontProvider provider = this.context.GetFontProvider();
            FontCharacteristics fc = new FontCharacteristics();
            FontSelectorStrategy strategy = provider.GetStrategy(content, FontFamilySplitter.SplitFontFamily(fontFamilyName
                ), fc);
            strategy.NextGlyphs();
            PdfFont currentFont = strategy.GetCurrentFont();
            iText.Layout.Element.Text text = new iText.Layout.Element.Text(content);
            text.SetFont(currentFont);
            text.SetFontSize(fontSize);
            text.SetProperty(Property.TEXT_RISE, 0f);
            text.SetProperty(Property.TEXT_RENDERING_MODE, PdfCanvasConstants.TextRenderingMode.FILL);
            text.SetProperty(Property.SPLIT_CHARACTERS, new DefaultSplitCharacters());
            Paragraph p = new Paragraph(text);
            p.SetMargin(0f);
            p.SetPadding(0f);
            IRenderer pRend = p.CreateRendererSubTree();
            LayoutArea layoutArea = new LayoutArea(1, new Rectangle(0, 0, width, maxAvailableHeight));
            LayoutContext minimalContext = new LayoutContext(layoutArea);
            LayoutResult quickLayout = pRend.Layout(minimalContext);
            return quickLayout.GetOccupiedArea().GetBBox().GetHeight();
        }

        private float GetFontSize(CssContextNode pmbcNode) {
            return FontStyleApplierUtil.ParseAbsoluteFontSize(pmbcNode.GetStyles().Get(CssConstants.FONT_SIZE));
        }

        internal virtual float GetMinContentHeight(PageMarginBoxContextNode node, float width, float maxAvailableHeight
            ) {
            return GetMaxContentHeight(node, width, maxAvailableHeight);
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
