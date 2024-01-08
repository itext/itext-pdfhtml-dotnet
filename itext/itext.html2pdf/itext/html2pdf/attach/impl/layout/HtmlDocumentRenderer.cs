/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
using iText.Html2pdf.Attach;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Page;
using iText.StyledXmlParser.Node;

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

        private HtmlDocumentRenderer.PageMarginBoxesDrawingHandler marginBoxesHandler;

        private HtmlBodyStylesApplierHandler htmlBodyHandler;

        private IDictionary<int, HtmlBodyStylesApplierHandler.PageStylesProperties> pageStylesPropertiesMap = new 
            Dictionary<int, HtmlBodyStylesApplierHandler.PageStylesProperties>();

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
            htmlBodyHandler = new HtmlBodyStylesApplierHandler(this, pageStylesPropertiesMap);
            document.GetPdfDocument().AddEventHandler(PdfDocumentEvent.END_PAGE, htmlBodyHandler);
        }

        /// <summary>Processes the page rules.</summary>
        /// <param name="rootNode">the root node</param>
        /// <param name="cssResolver">the CSS resolver</param>
        /// <param name="context">the processor context</param>
        public virtual void ProcessPageRules(INode rootNode, ICssResolver cssResolver, ProcessorContext context) {
            PageContextProperties firstPageProps = PageContextProperties.Resolve(rootNode, cssResolver, context.GetCssContext
                (), PageContextConstants.FIRST, PageContextConstants.RIGHT);
            // TODO DEVSIX-4118 in documents with set to rtl on root document, first page is considered as left
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
            marginBoxesHandler = new HtmlDocumentRenderer.PageMarginBoxesDrawingHandler().SetHtmlDocumentRenderer(this
                );
            document.GetPdfDocument().AddEventHandler(PdfDocumentEvent.END_PAGE, marginBoxesHandler);
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
            ProcessWaitingElement();
            base.Close();
            TrimLastPageIfNecessary();
            document.GetPdfDocument().RemoveEventHandler(PdfDocumentEvent.END_PAGE, marginBoxesHandler);
            document.GetPdfDocument().RemoveEventHandler(PdfDocumentEvent.END_PAGE, htmlBodyHandler);
            for (int i = 1; i <= document.GetPdfDocument().GetNumberOfPages(); ++i) {
                PdfPage page = document.GetPdfDocument().GetPage(i);
                if (!page.IsFlushed()) {
                    marginBoxesHandler.ProcessPage(document.GetPdfDocument(), i);
                    htmlBodyHandler.ProcessPage(page, i);
                }
            }
        }

        /// <summary>
        /// Removes event handlers that were added to pdf document when this
        /// <see cref="HtmlDocumentRenderer"/>
        /// was created.
        /// </summary>
        internal virtual void RemoveEventHandlers() {
            document.GetPdfDocument().RemoveEventHandler(PdfDocumentEvent.END_PAGE, htmlBodyHandler);
            // This handler is added in processPageRules method.
            document.GetPdfDocument().RemoveEventHandler(PdfDocumentEvent.END_PAGE, marginBoxesHandler);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.layout.renderer.DocumentRenderer#getNextRenderer()
        */
        public override IRenderer GetNextRenderer() {
            // Process waiting element to get the correct number of pages
            ProcessWaitingElement();
            iText.Html2pdf.Attach.Impl.Layout.HtmlDocumentRenderer relayoutRenderer = new iText.Html2pdf.Attach.Impl.Layout.HtmlDocumentRenderer
                (document, immediateFlush);
            PageSize defaultPageSize = document.GetPdfDocument().GetDefaultPageSize();
            float[] defaultPageMargins = new float[] { document.GetTopMargin(), document.GetRightMargin(), document.GetBottomMargin
                (), document.GetRightMargin() };
            relayoutRenderer.firstPageProc = firstPageProc.Reset(defaultPageSize, defaultPageMargins);
            relayoutRenderer.leftPageProc = leftPageProc.Reset(defaultPageSize, defaultPageMargins);
            relayoutRenderer.rightPageProc = rightPageProc.Reset(defaultPageSize, defaultPageMargins);
            relayoutRenderer.estimatedNumberOfPages = currentArea == null ? estimatedNumberOfPages : currentArea.GetPageNumber
                () - SimulateTrimLastPage();
            relayoutRenderer.marginBoxesHandler = marginBoxesHandler.SetHtmlDocumentRenderer(relayoutRenderer);
            relayoutRenderer.targetCounterHandler = new TargetCounterHandler(targetCounterHandler);
            return relayoutRenderer;
        }

        public override void Flush() {
            ProcessWaitingElement();
            base.Flush();
        }

        /// <summary>Layouts waiting element.</summary>
        public virtual void ProcessWaitingElement() {
            if (waitingElement != null) {
                IRenderer r = this.waitingElement;
                waitingElement = null;
                base.AddChild(r);
            }
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
                        if (anythingAddedToCurrentArea || currentArea == null || !IsPageLeft(currentArea.GetPageNumber())) {
                            do {
                                nextArea = base.UpdateCurrentArea(overflowResult);
                            }
                            while (!IsPageLeft(currentArea.GetPageNumber()));
                        }
                        anythingAddedToCurrentArea = false;
                        return nextArea;
                    }
                    else {
                        if (HtmlPageBreakType.RIGHT.Equals(htmlPageBreakType)) {
                            LayoutArea nextArea = currentArea;
                            if (anythingAddedToCurrentArea || currentArea == null || !IsPageRight(currentArea.GetPageNumber())) {
                                do {
                                    nextArea = base.UpdateCurrentArea(overflowResult);
                                }
                                while (!IsPageRight(currentArea.GetPageNumber()));
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

        protected override void FlushSingleRenderer(IRenderer resultRenderer) {
            if (!IsElementOnNonStaticLayout(resultRenderer)) {
                LayoutArea area = resultRenderer.GetOccupiedArea();
                UpdateLowestAndHighestPoints(area.GetBBox(), area.GetPageNumber());
            }
            base.FlushSingleRenderer(resultRenderer);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.layout.renderer.DocumentRenderer#addNewPage(com.itextpdf.kernel.geom.PageSize)
        */
        protected override PageSize AddNewPage(PageSize customPageSize) {
            PdfPage addedPage;
            int pageNumber = document.GetPdfDocument().GetNumberOfPages() + 1;
            PageContextProcessor nextProcessor = GetPageProcessor(pageNumber);
            if (customPageSize != null) {
                addedPage = document.GetPdfDocument().AddNewPage(customPageSize);
            }
            else {
                addedPage = document.GetPdfDocument().AddNewPage(nextProcessor.GetPageSize());
            }
            nextProcessor.ProcessNewPage(addedPage);
            float[] margins = nextProcessor.ComputeLayoutMargins();
            BodyHtmlStylesContainer[] styles = new BodyHtmlStylesContainer[] { ((IPropertyContainer)document).GetProperty
                <BodyHtmlStylesContainer>(Html2PdfProperty.HTML_STYLING), ((IPropertyContainer)document).GetProperty<BodyHtmlStylesContainer
                >(Html2PdfProperty.BODY_STYLING) };
            pageStylesPropertiesMap.Put(pageNumber, new HtmlBodyStylesApplierHandler.PageStylesProperties(styles));
            UpdateDefaultMargins(styles, margins);
            SetProperty(Property.MARGIN_TOP, margins[0]);
            SetProperty(Property.MARGIN_RIGHT, margins[1]);
            SetProperty(Property.MARGIN_BOTTOM, margins[2]);
            SetProperty(Property.MARGIN_LEFT, margins[3]);
            return new PageSize(addedPage.GetTrimBox());
        }

        internal virtual bool ShouldAttemptTrimLastPage() {
            return TRIM_LAST_BLANK_PAGE && document.GetPdfDocument().GetNumberOfPages() > 1;
        }

        internal virtual void TrimLastPageIfNecessary() {
            if (ShouldAttemptTrimLastPage()) {
                PdfDocument pdfDocument = document.GetPdfDocument();
                PdfPage lastPage = pdfDocument.GetLastPage();
                if (lastPage.GetContentStreamCount() == 1 && lastPage.GetContentStream(0).GetOutputStream().GetCurrentPos(
                    ) <= 0) {
                    // Remove last empty page
                    pdfDocument.RemovePage(pdfDocument.GetNumberOfPages());
                }
            }
        }

        /// <summary>
        /// Returns the number of pages that will be trimmed on
        /// <see cref="Close()"/>
        /// </summary>
        /// <returns>0 if no pages will be trimmed, or positive number of trimmed pages in case any are trimmed</returns>
        internal virtual int SimulateTrimLastPage() {
            if (ShouldAttemptTrimLastPage()) {
                int lastPageNumber = document.GetPdfDocument().GetNumberOfPages();
                // At the moment we only check if some element was positioned on this page
                // However, there might theoretically be an inconsistency with the method that
                // actually does the trimming because that method checks the canvas output only.
                // We might want to simulate drawing on canvas here in the future, or possibly
                // consider invisible elements in the method that actually does the trimming
                bool willAnyContentBeDrawnOnLastPage = false;
                foreach (IRenderer renderer in childRenderers) {
                    if (renderer.GetOccupiedArea().GetPageNumber() == lastPageNumber) {
                        willAnyContentBeDrawnOnLastPage = true;
                    }
                }
                foreach (IRenderer renderer in positionedRenderers) {
                    if (renderer.GetOccupiedArea().GetPageNumber() == lastPageNumber) {
                        willAnyContentBeDrawnOnLastPage = true;
                    }
                }
                return willAnyContentBeDrawnOnLastPage ? 0 : 1;
            }
            else {
                return 0;
            }
        }

        /// <summary>Gets a page processor for the page.</summary>
        /// <param name="pageNum">
        /// the number of the page for which the
        /// <see cref="PageContextProcessor"/>
        /// shall be obtained
        /// </param>
        /// <returns>a page processor</returns>
        internal virtual PageContextProcessor GetPageProcessor(int pageNum) {
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

        /// <summary>Gets the estimated number of pages.</summary>
        /// <returns>the estimated number of pages</returns>
        internal virtual int GetEstimatedNumberOfPages() {
            return estimatedNumberOfPages;
        }

        private void UpdateDefaultMargins(BodyHtmlStylesContainer[] styles, float[] defaultMargins) {
            for (int i = 0; i < 2; i++) {
                if (styles[i] != null) {
                    for (int j = 0; j < 4; j++) {
                        defaultMargins[j] += styles[i].GetTotalWidth()[j];
                    }
                }
            }
        }

        private bool IsElementOnNonStaticLayout(IRenderer resultRenderer) {
            bool nonStaticLayout = false;
            if (resultRenderer.HasProperty(Property.POSITION)) {
                int positionProperty = (int)resultRenderer.GetProperty<int?>(Property.POSITION);
                nonStaticLayout = positionProperty == LayoutPosition.ABSOLUTE || positionProperty == LayoutPosition.FIXED;
            }
            if (!nonStaticLayout && resultRenderer.HasProperty(Property.FLOAT)) {
                FloatPropertyValue? floatProperty = resultRenderer.GetProperty<FloatPropertyValue?>(Property.FLOAT);
                nonStaticLayout = floatProperty == FloatPropertyValue.LEFT || floatProperty == FloatPropertyValue.RIGHT;
            }
            return nonStaticLayout;
        }

        private void UpdateLowestAndHighestPoints(Rectangle rectangle, int page) {
            if (!pageStylesPropertiesMap.ContainsKey(page)) {
                return;
            }
            HtmlBodyStylesApplierHandler.LowestAndHighest currentPagePoints = pageStylesPropertiesMap.Get(page).lowestAndHighest;
            if (currentPagePoints == null) {
                pageStylesPropertiesMap.Get(page).lowestAndHighest = new HtmlBodyStylesApplierHandler.LowestAndHighest(rectangle
                    .GetY(), rectangle.GetY() + rectangle.GetHeight());
            }
            else {
                float newLowestPoint = rectangle.GetY();
                float newHighestPoint = rectangle.GetY() + rectangle.GetHeight();
                currentPagePoints.lowest = Math.Min(newLowestPoint, currentPagePoints.lowest);
                currentPagePoints.highest = Math.Max(newHighestPoint, currentPagePoints.highest);
            }
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

        /// <summary>Checks if the current page is a left page.</summary>
        /// <returns>true, if is current page left</returns>
        private bool IsPageLeft(int pageNum) {
            // TODO DEVSIX-4118 rtl
            bool pageIsEven = pageNum % 2 == 0;
            return evenPagesAreLeft == pageIsEven;
        }

        /// <summary>Checks if the current page is a right page.</summary>
        /// <returns>true, if is current page right</returns>
        private bool IsPageRight(int pageNum) {
            return !IsPageLeft(pageNum);
        }

        private class PageMarginBoxesDrawingHandler : iText.Kernel.Events.IEventHandler {
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
