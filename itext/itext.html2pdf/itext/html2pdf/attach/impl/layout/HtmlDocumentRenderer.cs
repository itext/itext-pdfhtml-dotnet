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
using System.Collections.Generic;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css.Page;
using iText.Html2pdf.Css.Resolve;
using iText.Html2pdf.Html.Node;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
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

        /// <summary>Indicates if the current page is even.</summary>
        /// <remarks>
        /// Indicates if the current page is even.
        /// Important: this number may differ from the result you get checking
        /// if the number of pages in the document is even or not, because
        /// the first page break-before might change right page to left (in ltr cases),
        /// but a blank page will not be added.
        /// </remarks>
        private bool currentPageEven = true;

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

        /// <summary>Instantiates a new <code>HtmlDocumentRenderer</code> instance.</summary>
        /// <param name="document">an iText <code>Document</code> instance</param>
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
            firstPageProc = new PageContextProcessor(firstPageProps, context, defaultPageSize);
            leftPageProc = new PageContextProcessor(leftPageProps, context, defaultPageSize);
            rightPageProc = new PageContextProcessor(rightPageProps, context, defaultPageSize);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.layout.renderer.RootRenderer#addChild(com.itextpdf.layout.renderer.IRenderer)
        */
        public override void AddChild(IRenderer renderer) {
            if (waitingElement != null) {
                if (true.Equals(renderer.GetProperty<bool?>(Html2PdfProperty.KEEP_WITH_PREVIOUS))) {
                    waitingElement.SetProperty(Property.KEEP_WITH_NEXT, true);
                }
                base.AddChild(waitingElement);
                // After we have added any child, we should not trim first pages because of break before element, even if the added child had zero height
                shouldTrimFirstBlankPagesCausedByBreakBeforeFirstElement = false;
                waitingElement = null;
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
            relayoutRenderer.firstPageProc = firstPageProc;
            relayoutRenderer.leftPageProc = leftPageProc;
            relayoutRenderer.rightPageProc = rightPageProc;
            relayoutRenderer.estimatedNumberOfPages = currentPageNumber;
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
                    if (HtmlPageBreakType.LEFT.Equals(htmlPageBreakType) && IsCurrentPageLeft() || HtmlPageBreakType.RIGHT.Equals
                        (htmlPageBreakType) && IsCurrentPageRight()) {
                        currentPageEven = !currentPageEven;
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
                        if (anythingAddedToCurrentArea || !IsCurrentPageLeft() || currentArea == null) {
                            do {
                                nextArea = base.UpdateCurrentArea(overflowResult);
                            }
                            while (!IsCurrentPageLeft());
                        }
                        anythingAddedToCurrentArea = false;
                        return nextArea;
                    }
                    else {
                        if (HtmlPageBreakType.RIGHT.Equals(htmlPageBreakType)) {
                            LayoutArea nextArea = currentArea;
                            if (anythingAddedToCurrentArea || !IsCurrentPageRight() || currentArea == null) {
                                do {
                                    nextArea = base.UpdateCurrentArea(overflowResult);
                                }
                                while (!IsCurrentPageRight());
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
            PageContextProcessor nextProcessor = GetNextPageProcessor(numberOfPages == 0);
            if (customPageSize != null) {
                addedPage = document.GetPdfDocument().AddNewPage(customPageSize);
            }
            else {
                addedPage = document.GetPdfDocument().AddNewPage(nextProcessor.GetPageSize());
            }
            currentPageEven = !currentPageEven;
            nextProcessor.ProcessNewPage(addedPage, this);
            float[] margins = nextProcessor.ComputeLayoutMargins();
            document.SetMargins(margins[0], margins[1], margins[2], margins[3]);
            return new PageSize(addedPage.GetTrimBox());
        }

        /// <summary>Gets the estimated number of pages.</summary>
        /// <returns>the estimated number of pages</returns>
        internal virtual int GetEstimatedNumberOfPages() {
            return estimatedNumberOfPages;
        }

        /// <summary>Gets the next page processor.</summary>
        /// <param name="firstPage">the first page</param>
        /// <returns>the next page processor</returns>
        private PageContextProcessor GetNextPageProcessor(bool firstPage) {
            // If first page, but break-before: left for ltr is present, we should use left page instead of first
            if (firstPage && currentPageEven) {
                return firstPageProc;
            }
            else {
                if (IsCurrentPageRight()) {
                    return leftPageProc;
                }
                else {
                    return rightPageProc;
                }
            }
        }

        /// <summary>Checks if the current page is a left page.</summary>
        /// <returns>true, if is current page left</returns>
        private bool IsCurrentPageLeft() {
            // TODO rtl
            return currentPageEven;
        }

        /// <summary>Checks if the current page is a right page.</summary>
        /// <returns>true, if is current page right</returns>
        private bool IsCurrentPageRight() {
            return !IsCurrentPageLeft();
        }
    }
}
