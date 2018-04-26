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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css.Page;
using iText.Html2pdf.Css.Resolve;
using iText.Html2pdf.Html.Node;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Tagging;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout {
    /// <summary>The DocumentRenderer class for HTML.</summary>
    public class HtmlDocumentRenderer : DocumentRenderer {
        /// <summary>The Constant TRIM_LAST_BLANK_PAGE.</summary>
        /// <remarks>
        /// The Constant TRIM_LAST_BLANK_PAGE.
        /// In a future version, we might want to expose this value to the users,
        /// or make it a public setting of the HTML renderer.
        /// </remarks>
        private const bool TRIM_LAST_BLANK_PAGE = true;

        /// <summary>The page context processor for the first page.</summary>
        private PageContextProcessor firstPageProc;

        /// <summary>The page context processor for all left pages.</summary>
        private PageContextProcessor leftPageProc;

        /// <summary>The page context processor for all right pages.</summary>
        private PageContextProcessor rightPageProc;

        /// <summary>Indicates if even pages are considered as left or right.</summary>
        /// <remarks>
        /// Indicates if even pages are considered as left or right.
        /// Important: this value might differ depending on page progression direction,
        /// as well as because the first page break-before might change right page to left (in ltr cases),
        /// but a blank page will not be added.
        /// </remarks>
        private bool evenPagesAreLeft = true;

        private HtmlDocumentRenderer.PageMarginBoxesDrawingHandler handler;

        /// <summary>
        /// The waiting element, an child element is kept waiting for the
        /// next element to process the "keep with previous" property.
        /// </summary>
        private IRenderer waitingElement;

        /// <summary>
        /// Indicates if the first blank pages caused by a break-before-first
        /// element should be trimmed.
        /// </summary>
        private bool shouldTrimFirstBlankPagesCausedByBreakBeforeFirstElement = true;

        /// <summary>Indicates if anything was added to the current area.</summary>
        private bool anythingAddedToCurrentArea = false;

        /// <summary>The estimated number of pages.</summary>
        private int estimatedNumberOfPages;

        /// <summary>
        /// Instantiates a new
        /// <see cref="HtmlDocumentRenderer"/>
        /// instance.
        /// </summary>
        /// <param name="document">
        /// an iText
        /// <see cref="iText.Layout.Document"/>
        /// instance
        /// </param>
        /// <param name="immediateFlush">the immediate flush indicator</param>
        public HtmlDocumentRenderer(Document document, bool immediateFlush)
            : base(document, immediateFlush) {
        }

        /// <summary>Processes the page rules.</summary>
        /// <param name="rootNode">the root node</param>
        /// <param name="cssResolver">the CSS resolver</param>
        /// <param name="context">the processor context</param>
        public virtual void ProcessPageRules(INode rootNode, ICssResolver cssResolver, ProcessorContext context) {
            PageContextProperties firstPageProps = PageContextProperties.Resolve(rootNode, cssResolver, context.GetCssContext
                (), PageContextConstants.FIRST, PageContextConstants.RIGHT);
            // TODO in documents with set to rtl on root document, first page is considered as left
            PageContextProperties leftPageProps = PageContextProperties.Resolve(rootNode, cssResolver, context.GetCssContext
                (), PageContextConstants.LEFT);
            PageContextProperties rightPageProps = PageContextProperties.Resolve(rootNode, cssResolver, context.GetCssContext
                (), PageContextConstants.RIGHT);
            PageSize defaultPageSize = document.GetPdfDocument().GetDefaultPageSize();
            float[] defaultPageMargins = new float[] { document.GetTopMargin(), document.GetRightMargin(), document.GetBottomMargin
                (), document.GetRightMargin() };
            firstPageProc = new PageContextProcessor(firstPageProps, context, defaultPageSize, defaultPageMargins);
            leftPageProc = new PageContextProcessor(leftPageProps, context, defaultPageSize, defaultPageMargins);
            rightPageProc = new PageContextProcessor(rightPageProps, context, defaultPageSize, defaultPageMargins);
            handler = new HtmlDocumentRenderer.PageMarginBoxesDrawingHandler().SetHtmlDocumentRenderer(this);
            document.GetPdfDocument().AddEventHandler(PdfDocumentEvent.END_PAGE, handler);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.layout.renderer.RootRenderer#addChild(com.itextpdf.layout.renderer.IRenderer)
        */
        public override void AddChild(IRenderer renderer) {
            if (waitingElement != null) {
                if (true.Equals(renderer.GetProperty<bool?>(Html2PdfProperty.KEEP_WITH_PREVIOUS))) {
                    waitingElement.SetProperty(Property.KEEP_WITH_NEXT, true);
                }
                IRenderer element = waitingElement;
                waitingElement = null;
                base.AddChild(element);
                if (!IsRunningElementsOnly(element)) {
                    // After we have added any child, we should not trim first pages because of break before element, even if the added child had zero height
                    shouldTrimFirstBlankPagesCausedByBreakBeforeFirstElement = false;
                }
            }
            waitingElement = renderer;
            FloatPropertyValue? floatPropertyValue = renderer.GetProperty<FloatPropertyValue?>(Property.FLOAT);
            int? position = renderer.GetProperty<int?>(Property.POSITION);
            if ((position != null && position == LayoutPosition.ABSOLUTE) || (floatPropertyValue != null && !floatPropertyValue
                .Equals(FloatPropertyValue.NONE))) {
                waitingElement = null;
                base.AddChild(renderer);
            }
        }

        /* (non-Javadoc)
        * @see com.itextpdf.layout.renderer.DocumentRenderer#close()
        */
        public override void Close() {
            if (waitingElement != null) {
                IRenderer r = this.waitingElement;
                waitingElement = null;
                base.AddChild(r);
            }
            base.Close();
            PdfDocument pdfDocument = document.GetPdfDocument();
            if (pdfDocument.GetNumberOfPages() > 1) {
                PdfPage lastPage = pdfDocument.GetLastPage();
                if (lastPage.GetContentStreamCount() == 1 && lastPage.GetContentStream(0).GetOutputStream().GetCurrentPos(
                    ) <= 0) {
                    // Remove last empty page
                    pdfDocument.RemovePage(pdfDocument.GetNumberOfPages());
                }
            }
            document.GetPdfDocument().RemoveEventHandler(PdfDocumentEvent.END_PAGE, handler);
            for (int i = 1; i <= document.GetPdfDocument().GetNumberOfPages(); ++i) {
                PdfPage page = document.GetPdfDocument().GetPage(i);
                if (!page.IsFlushed()) {
                    handler.ProcessPage(document.GetPdfDocument(), i);
                }
            }
        }

        /* (non-Javadoc)
        * @see com.itextpdf.layout.renderer.DocumentRenderer#getNextRenderer()
        */
        public override IRenderer GetNextRenderer() {
            // Process waiting element to get the correct number of pages
            if (waitingElement != null) {
                base.AddChild(waitingElement);
                waitingElement = null;
            }
            iText.Html2pdf.Attach.Impl.Layout.HtmlDocumentRenderer relayoutRenderer = new iText.Html2pdf.Attach.Impl.Layout.HtmlDocumentRenderer
                (document, immediateFlush);
            PageSize defaultPageSize = document.GetPdfDocument().GetDefaultPageSize();
            float[] defaultPageMargins = new float[] { document.GetTopMargin(), document.GetRightMargin(), document.GetBottomMargin
                (), document.GetRightMargin() };
            relayoutRenderer.firstPageProc = firstPageProc.Reset(defaultPageSize, defaultPageMargins);
            relayoutRenderer.leftPageProc = leftPageProc.Reset(defaultPageSize, defaultPageMargins);
            relayoutRenderer.rightPageProc = rightPageProc.Reset(defaultPageSize, defaultPageMargins);
            relayoutRenderer.estimatedNumberOfPages = currentPageNumber;
            relayoutRenderer.handler = handler.SetHtmlDocumentRenderer(relayoutRenderer);
            return relayoutRenderer;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.layout.renderer.DocumentRenderer#updateCurrentArea(com.itextpdf.layout.layout.LayoutResult)
        */
        protected override LayoutArea UpdateCurrentArea(LayoutResult overflowResult) {
            AreaBreak areaBreak = overflowResult != null ? overflowResult.GetAreaBreak() : null;
            if (areaBreak is HtmlPageBreak) {
                HtmlPageBreakType htmlPageBreakType = ((HtmlPageBreak)areaBreak).GetBreakType();
                if (shouldTrimFirstBlankPagesCausedByBreakBeforeFirstElement && currentArea != null && overflowResult.GetStatus
                    () == LayoutResult.NOTHING && currentArea.IsEmptyArea() && currentArea.GetPageNumber() == 1) {
                    // Remove blank page that was added just to have area for elements to layout on.
                    // Now we will add a page with dimensions and all the stuff that is requested by page-break-before
                    document.GetPdfDocument().RemovePage(1);
                    currentPageNumber = 0;
                    overflowResult = null;
                    currentArea = null;
                    shouldTrimFirstBlankPagesCausedByBreakBeforeFirstElement = false;
                    if (HtmlPageBreakType.LEFT.Equals(htmlPageBreakType) && !IsPageLeft(1) || HtmlPageBreakType.RIGHT.Equals(htmlPageBreakType
                        ) && !IsPageRight(1)) {
                        evenPagesAreLeft = !evenPagesAreLeft;
                    }
                }
                // hack to change the "evenness" of the first page without adding an unnecessary blank page
                anythingAddedToCurrentArea = anythingAddedToCurrentArea || overflowResult != null && overflowResult.GetStatus
                    () == LayoutResult.PARTIAL;
                if (HtmlPageBreakType.ALWAYS.Equals(htmlPageBreakType)) {
                    LayoutArea nextArea = currentArea;
                    if (anythingAddedToCurrentArea || currentArea == null) {
                        nextArea = base.UpdateCurrentArea(overflowResult);
                    }
                    anythingAddedToCurrentArea = false;
                    return nextArea;
                }
                else {
                    if (HtmlPageBreakType.LEFT.Equals(htmlPageBreakType)) {
                        LayoutArea nextArea = currentArea;
                        if (anythingAddedToCurrentArea || currentArea == null || !IsPageLeft(currentPageNumber)) {
                            do {
                                nextArea = base.UpdateCurrentArea(overflowResult);
                            }
                            while (!IsPageLeft(currentPageNumber));
                        }
                        anythingAddedToCurrentArea = false;
                        return nextArea;
                    }
                    else {
                        if (HtmlPageBreakType.RIGHT.Equals(htmlPageBreakType)) {
                            LayoutArea nextArea = currentArea;
                            if (anythingAddedToCurrentArea || currentArea == null || !IsPageRight(currentPageNumber)) {
                                do {
                                    nextArea = base.UpdateCurrentArea(overflowResult);
                                }
                                while (!IsPageRight(currentPageNumber));
                            }
                            anythingAddedToCurrentArea = false;
                            return nextArea;
                        }
                    }
                }
            }
            anythingAddedToCurrentArea = false;
            return base.UpdateCurrentArea(overflowResult);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.layout.renderer.RootRenderer#shrinkCurrentAreaAndProcessRenderer(com.itextpdf.layout.renderer.IRenderer, java.util.List, com.itextpdf.layout.layout.LayoutResult)
        */
        protected override void ShrinkCurrentAreaAndProcessRenderer(IRenderer renderer, IList<IRenderer> resultRenderers
            , LayoutResult result) {
            if (renderer != null) {
                anythingAddedToCurrentArea = true;
            }
            base.ShrinkCurrentAreaAndProcessRenderer(renderer, resultRenderers, result);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.layout.renderer.DocumentRenderer#addNewPage(com.itextpdf.kernel.geom.PageSize)
        */
        protected override PageSize AddNewPage(PageSize customPageSize) {
            PdfPage addedPage;
            int numberOfPages = document.GetPdfDocument().GetNumberOfPages();
            PageContextProcessor nextProcessor = GetPageProcessor(numberOfPages + 1);
            if (customPageSize != null) {
                addedPage = document.GetPdfDocument().AddNewPage(customPageSize);
            }
            else {
                addedPage = document.GetPdfDocument().AddNewPage(nextProcessor.GetPageSize());
            }
            nextProcessor.ProcessNewPage(addedPage);
            float[] margins = nextProcessor.ComputeLayoutMargins();
            ApplyHtmlBodyStyles(addedPage, margins);
            SetProperty(Property.MARGIN_TOP, margins[0]);
            SetProperty(Property.MARGIN_RIGHT, margins[1]);
            SetProperty(Property.MARGIN_BOTTOM, margins[2]);
            SetProperty(Property.MARGIN_LEFT, margins[3]);
            return new PageSize(addedPage.GetTrimBox());
        }

        private void ApplyHtmlBodyStyles(PdfPage page, float[] defaultMargins) {
            BodyHtmlStylesContainer[] styles = new BodyHtmlStylesContainer[2];
            styles[0] = ((IPropertyContainer)document).GetProperty<BodyHtmlStylesContainer>(Html2PdfProperty.HTML_STYLING
                );
            styles[1] = ((IPropertyContainer)document).GetProperty<BodyHtmlStylesContainer>(Html2PdfProperty.BODY_STYLING
                );
            int firstBackground = ApplyFirstBackground(page, defaultMargins, styles);
            for (int i = 0; i < 2; i++) {
                if (styles[i] != null) {
                    if (styles[i].HasContentToDraw()) {
                        DrawSimulatedDiv(page, styles[i].properties, defaultMargins, firstBackground != i);
                    }
                    for (int j = 0; j < 4; j++) {
                        defaultMargins[j] += styles[i].GetTotalWidth()[j];
                    }
                }
            }
        }

        private int ApplyFirstBackground(PdfPage page, float[] defaultMargins, BodyHtmlStylesContainer[] styles) {
            int firstBackground = -1;
            if (styles[0] != null && (styles[0].GetOwnProperty<Background>(Property.BACKGROUND) != null || styles[0].GetOwnProperty
                <BackgroundImage>(Property.BACKGROUND_IMAGE) != null)) {
                firstBackground = 0;
            }
            else {
                if (styles[1] != null && (styles[1].GetOwnProperty<Background>(Property.BACKGROUND) != null || styles[1].GetOwnProperty
                    <BackgroundImage>(Property.BACKGROUND_IMAGE) != null)) {
                    firstBackground = 1;
                }
            }
            if (firstBackground != -1) {
                Dictionary<int, Object> background = new Dictionary<int, Object>();
                background.Put(Property.BACKGROUND, styles[firstBackground].GetProperty<Background>(Property.BACKGROUND));
                background.Put(Property.BACKGROUND_IMAGE, styles[firstBackground].GetProperty<BackgroundImage>(Property.BACKGROUND_IMAGE
                    ));
                DrawSimulatedDiv(page, background, defaultMargins, true);
            }
            return firstBackground;
        }

        private void DrawSimulatedDiv(PdfPage page, IDictionary<int, Object> styles, float[] margins, bool drawBackground
            ) {
            Div pageBordersSimulation;
            pageBordersSimulation = new Div().SetFillAvailableArea(true);
            foreach (KeyValuePair<int, Object> entry in styles) {
                if ((entry.Key == Property.BACKGROUND || entry.Key == Property.BACKGROUND_IMAGE) && !drawBackground) {
                    continue;
                }
                pageBordersSimulation.SetProperty(entry.Key, entry.Value);
            }
            pageBordersSimulation.GetAccessibilityProperties().SetRole(StandardRoles.ARTIFACT);
            iText.Layout.Canvas canvas = new Canvas(new PdfCanvas(page), page.GetDocument(), page.GetTrimBox().ApplyMargins
                (margins[0], margins[1], margins[2], margins[3], false));
            canvas.EnableAutoTagging(page);
            canvas.Add(pageBordersSimulation);
            canvas.Close();
        }

        /// <summary>Gets the estimated number of pages.</summary>
        /// <returns>the estimated number of pages</returns>
        internal virtual int GetEstimatedNumberOfPages() {
            return estimatedNumberOfPages;
        }

        /// <summary>Gets a page processor for the page.</summary>
        /// <param name="pageNum">
        /// the number of the page for which the
        /// <see cref="PageContextProcessor"/>
        /// shall be obtained
        /// </param>
        /// <returns>a page processor</returns>
        private PageContextProcessor GetPageProcessor(int pageNum) {
            // If first page, but break-before: left for ltr is present, we should use left page instead of first
            if (pageNum == 1 && evenPagesAreLeft) {
                return firstPageProc;
            }
            else {
                if (IsPageLeft(pageNum)) {
                    return leftPageProc;
                }
                else {
                    return rightPageProc;
                }
            }
        }

        /// <summary>Checks if the current page is a left page.</summary>
        /// <returns>true, if is current page left</returns>
        private bool IsPageLeft(int pageNum) {
            // TODO rtl
            bool pageIsEven = pageNum % 2 == 0;
            return evenPagesAreLeft == pageIsEven;
        }

        /// <summary>Checks if the current page is a right page.</summary>
        /// <returns>true, if is current page right</returns>
        private bool IsPageRight(int pageNum) {
            return !IsPageLeft(pageNum);
        }

        private static bool IsRunningElementsOnly(IRenderer waitingElement) {
            bool res;
            if (res = waitingElement is ParagraphRenderer && !waitingElement.GetChildRenderers().IsEmpty()) {
                IList<IRenderer> childRenderers = waitingElement.GetChildRenderers();
                int i = 0;
                while (res && i < childRenderers.Count) {
                    res = childRenderers[i++] is RunningElement.RunningElementRenderer;
                }
            }
            return res;
        }

        private class PageMarginBoxesDrawingHandler : IEventHandler {
            private HtmlDocumentRenderer htmlDocumentRenderer;

            internal virtual HtmlDocumentRenderer.PageMarginBoxesDrawingHandler SetHtmlDocumentRenderer(HtmlDocumentRenderer
                 htmlDocumentRenderer) {
                this.htmlDocumentRenderer = htmlDocumentRenderer;
                return this;
            }

            public virtual void HandleEvent(Event @event) {
                if (@event is PdfDocumentEvent) {
                    PdfPage page = ((PdfDocumentEvent)@event).GetPage();
                    PdfDocument pdfDoc = ((PdfDocumentEvent)@event).GetDocument();
                    int pageNumber = pdfDoc.GetPageNumber(page);
                    ProcessPage(pdfDoc, pageNumber);
                }
            }

            internal virtual void ProcessPage(PdfDocument pdfDoc, int pageNumber) {
                PageContextProcessor pageProcessor = htmlDocumentRenderer.GetPageProcessor(pageNumber);
                pageProcessor.ProcessPageEnd(pageNumber, pdfDoc, htmlDocumentRenderer);
            }
        }
    }
}
