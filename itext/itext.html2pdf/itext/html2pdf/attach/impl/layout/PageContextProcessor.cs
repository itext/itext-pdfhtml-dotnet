/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
Authors: Apryse Software.

This program is offered under a commercial and under the AGPL license.
For commercial licensing, contact us at https://itextpdf.com/sales.  For AGPL licensing, see below.

AGPL licensing:
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Commons.Utils;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply.Impl;
using iText.Html2pdf.Css.Apply.Util;
using iText.Html2pdf.Logs;
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
using iText.StyledXmlParser.Css.Page;
using iText.StyledXmlParser.Css.Util;

namespace iText.Html2pdf.Attach.Impl.Layout {
//\cond DO_NOT_DOCUMENT
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

        /// <summary>page background simulation.</summary>
        private Div pageBackgroundSimulation;

        /// <summary>page borders simulation.</summary>
        private Div pageBordersSimulation;

        private PageContextProperties properties;

        private ProcessorContext context;

        private PageMarginBoxBuilder pageMarginBoxHelper;

        /// <summary>The logger.</summary>
        private static readonly ILogger LOGGER = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Layout.PageContextProcessor
            ));

//\cond DO_NOT_DOCUMENT
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
//\endcond

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
            String[] split = iText.Commons.Utils.StringUtil.Split(marksStr, " ");
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

//\cond DO_NOT_DOCUMENT
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
            float em = CssDimensionParsingUtils.ParseAbsoluteLength(styles.Get(CssConstants.FONT_SIZE));
            float rem = context.GetCssContext().GetRootFontSize();
            pageSize = PageSizeParser.FetchPageSize(styles.Get(CssConstants.SIZE), em, rem, defaultPageSize);
            UnitValue bleedValue = CssDimensionParsingUtils.ParseLengthValueToPt(styles.Get(CssConstants.BLEED), em, rem
                );
            if (bleedValue != null && bleedValue.IsPointValue()) {
                bleed = bleedValue.GetValue();
            }
            marks = ParseMarks(styles.Get(CssConstants.MARKS));
            ParseMargins(styles, em, rem, defaultPageMargins);
            ParseBorders(styles, em, rem);
            ParsePaddings(styles, em, rem);
            CreatePageSimulationElements(styles, context);
            pageMarginBoxHelper = new PageMarginBoxBuilder(properties.GetResolvedPageMarginBoxes(), margins, pageSize);
            return this;
        }
//\endcond

//\cond DO_NOT_DOCUMENT
        /// <summary>Gets the page size.</summary>
        /// <returns>the page size</returns>
        internal virtual PageSize GetPageSize() {
            return pageSize;
        }
//\endcond

//\cond DO_NOT_DOCUMENT
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
//\endcond

//\cond DO_NOT_DOCUMENT
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
//\endcond

//\cond DO_NOT_DOCUMENT
        /// <summary>
        /// Processes a new page by setting the bleed value, adding marks, drawing
        /// page backgrounds and borders.
        /// </summary>
        /// <param name="page">the page to process</param>
        internal virtual void ProcessNewPage(PdfPage page) {
            SetBleed(page);
            DrawMarks(page);
            DrawPageBorders(page);
        }
//\endcond

//\cond DO_NOT_DOCUMENT
        /// <summary>Draws page background.</summary>
        /// <param name="page">the page</param>
        /// <returns>pdfCanvas instance if there was a background to draw, otherwise returns null</returns>
        internal virtual PdfCanvas DrawPageBackground(PdfPage page) {
            PdfCanvas pdfCanvas = null;
            if (pageBackgroundSimulation != null) {
                pdfCanvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), page.GetDocument());
                iText.Layout.Canvas canvas = new iText.Layout.Canvas(pdfCanvas, page.GetBleedBox());
                canvas.EnableAutoTagging(page);
                canvas.Add(pageBackgroundSimulation);
                canvas.Close();
            }
            return pdfCanvas;
        }
//\endcond

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

        /// <summary>Draws page border.</summary>
        /// <param name="page">the page</param>
        private void DrawPageBorders(PdfPage page) {
            if (pageBordersSimulation == null) {
                return;
            }
            iText.Layout.Canvas canvas = new iText.Layout.Canvas(new PdfCanvas(page), page.GetTrimBox());
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
            pageMarginBoxHelper.BuildForSinglePage(pageNumber, pdfDocument, documentRenderer, context);
            if (pageMarginBoxHelper.GetRenderers() != null) {
                for (int i = 0; i < 16; i++) {
                    if (pageMarginBoxHelper.GetRenderers()[i] != null) {
                        Draw(pageMarginBoxHelper.GetRenderers()[i], pageMarginBoxHelper.GetNodes()[i], pdfDocument, pdfDocument.GetPage
                            (pageNumber), documentRenderer, pageNumber);
                    }
                }
            }
        }

        private void Draw(IRenderer renderer, PageMarginBoxContextNode node, PdfDocument pdfDocument, PdfPage page
            , DocumentRenderer documentRenderer, int pageNumber) {
            LayoutResult result = renderer.Layout(new LayoutContext(new LayoutArea(pageNumber, node.GetPageMarginBoxRectangle
                ())));
            IRenderer rendererToDraw = result.GetStatus() == LayoutResult.FULL ? renderer : result.GetSplitRenderer();
            if (rendererToDraw != null) {
                TagTreePointer tagPointer = null;
                TagTreePointer backupPointer = null;
                PdfPage backupPage = null;
                if (pdfDocument.IsTagged()) {
                    tagPointer = pdfDocument.GetTagStructureContext().GetAutoTaggingPointer();
                    backupPage = tagPointer.GetCurrentPage();
                    backupPointer = new TagTreePointer(tagPointer);
                    tagPointer.MoveToRoot();
                    tagPointer.SetPageForTagging(page);
                }
                rendererToDraw.SetParent(documentRenderer).Draw(new DrawContext(page.GetDocument(), new PdfCanvas(page), pdfDocument
                    .IsTagged()));
                if (pdfDocument.IsTagged()) {
                    tagPointer.SetPageForTagging(backupPage);
                    tagPointer.MoveToPointer(backupPointer);
                }
            }
            else {
                // marginBoxElements have overflow property set to HIDDEN, therefore it is not expected to neither get
                // LayoutResult other than FULL nor get no split renderer (result NOTHING) even if result is not FULL
                LOGGER.LogError(MessageFormatUtil.Format(Html2PdfLogMessageConstant.PAGE_MARGIN_BOX_CONTENT_CANNOT_BE_DRAWN
                    , node.GetMarginBoxName()));
            }
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
            if (!pageBackgroundSimulation.HasOwnProperty(Property.BACKGROUND) && !pageBackgroundSimulation.HasOwnProperty
                (Property.BACKGROUND_IMAGE)) {
                pageBackgroundSimulation = null;
            }
            if (borders[0] == null && borders[1] == null && borders[2] == null && borders[3] == null) {
                pageBordersSimulation = null;
                return;
            }
            pageBordersSimulation = new Div().SetFillAvailableArea(true);
            pageBordersSimulation.SetMargins(margins[0], margins[1], margins[2], margins[3]);
            pageBordersSimulation.SetBorderTop(borders[0]);
            pageBordersSimulation.SetBorderRight(borders[1]);
            pageBordersSimulation.SetBorderBottom(borders[2]);
            pageBordersSimulation.SetBorderLeft(borders[3]);
            pageBordersSimulation.GetAccessibilityProperties().SetRole(StandardRoles.ARTIFACT);
        }
    }
//\endcond
}
