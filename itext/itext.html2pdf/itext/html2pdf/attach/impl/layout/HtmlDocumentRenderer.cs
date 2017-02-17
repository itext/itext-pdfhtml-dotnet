/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2017 iText Group NV
    Authors: iText Software.

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
    address: sales@itextpdf.com */
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
    public class HtmlDocumentRenderer : DocumentRenderer {
        private const bool TRIM_LAST_BLANK_PAGE = true;

        private PageContextProcessor firstPageProc;

        private PageContextProcessor leftPageProc;

        private PageContextProcessor rightPageProc;

        private bool currentPageEven = true;

        private IRenderer waitingElement;

        private bool shouldTrimFirstBlankPagesCausedByBreakBeforeFirstElement = true;

        private bool anythingAddedToCurrentArea = false;

        public HtmlDocumentRenderer(Document document, bool immediateFlush)
            : base(document, immediateFlush) {
        }

        // Maybe later we will want to expose this to the users or make it a public setting of this renderer
        // NOTE! This number may differ from checking the number of pages in the document on whether it is an even number or not, because
        // first page break-before might change right page to left (in ltr cases), but a blank page will not be added
        // An child element is kept waiting for the next one to process "keep with previous" property
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
        }

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

        protected override void ShrinkCurrentAreaAndProcessRenderer(IRenderer renderer, IList<IRenderer> resultRenderers
            , LayoutResult result) {
            if (renderer != null) {
                anythingAddedToCurrentArea = true;
            }
            base.ShrinkCurrentAreaAndProcessRenderer(renderer, resultRenderers, result);
        }

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
            nextProcessor.ProcessNewPage(addedPage);
            float[] margins = nextProcessor.ComputeLayoutMargins();
            document.SetMargins(margins[0], margins[1], margins[2], margins[3]);
            return new PageSize(addedPage.GetTrimBox());
        }

        public override void Close() {
            if (waitingElement != null) {
                base.AddChild(waitingElement);
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

        private bool IsCurrentPageLeft() {
            // TODO rtl
            return currentPageEven;
        }

        private bool IsCurrentPageRight() {
            return !IsCurrentPageLeft();
        }
    }
}
