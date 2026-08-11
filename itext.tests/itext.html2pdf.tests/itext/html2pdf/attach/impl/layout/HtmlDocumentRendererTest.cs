/*
This file is part of the iText (R) project.
Copyright (c) 1998-2026 Apryse Group NV
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
using System.IO;
using iText.Html2pdf;
using iText.IO.Source;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Event;
using iText.Layout;
using iText.Test;

namespace iText.Html2pdf.Attach.Impl.Layout {
    [NUnit.Framework.Category("UnitTest")]
    public class HtmlDocumentRendererTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void ShouldAttemptTrimLastPageTest() {
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(new ByteArrayOutputStream()));
            Document document = new Document(pdfDocument);
            HtmlDocumentRenderer documentRenderer = new HtmlDocumentRenderer(document, false);
            document.SetRenderer(documentRenderer);
            pdfDocument.AddNewPage();
            NUnit.Framework.Assert.AreEqual(1, pdfDocument.GetNumberOfPages());
            // For one-page documents it does not make sense to attempt to trim last page
            NUnit.Framework.Assert.IsFalse(documentRenderer.ShouldAttemptTrimLastPage());
            pdfDocument.AddNewPage();
            NUnit.Framework.Assert.AreEqual(2, pdfDocument.GetNumberOfPages());
            // If there are more than one page, we try to trim last page
            NUnit.Framework.Assert.IsTrue(documentRenderer.ShouldAttemptTrimLastPage());
        }

        [NUnit.Framework.Test]
        public virtual void TrimLastPageIfNecessaryTest() {
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(new ByteArrayOutputStream()));
            Document document = new Document(pdfDocument);
            HtmlDocumentRenderer documentRenderer = new HtmlDocumentRenderer(document, false);
            document.SetRenderer(documentRenderer);
            pdfDocument.AddNewPage();
            pdfDocument.AddNewPage();
            new PdfCanvas(pdfDocument.GetLastPage()).MoveTo(10, 10).LineTo(20, 20).Stroke();
            pdfDocument.AddNewPage();
            NUnit.Framework.Assert.AreEqual(3, pdfDocument.GetNumberOfPages());
            documentRenderer.TrimLastPageIfNecessary();
            NUnit.Framework.Assert.AreEqual(2, pdfDocument.GetNumberOfPages());
            documentRenderer.TrimLastPageIfNecessary();
            NUnit.Framework.Assert.AreEqual(2, pdfDocument.GetNumberOfPages());
        }

        [NUnit.Framework.Test]
        public virtual void EstimatedNumberOfPagesInNextRendererEmptyDocumentTest() {
            Document document = HtmlConverter.ConvertToDocument("<html></html>", new PdfWriter(new ByteArrayOutputStream
                ()));
            HtmlDocumentRenderer documentRenderer = (HtmlDocumentRenderer)document.GetRenderer();
            HtmlDocumentRenderer nextRenderer = (HtmlDocumentRenderer)documentRenderer.GetNextRenderer();
            NUnit.Framework.Assert.AreEqual(0, nextRenderer.GetEstimatedNumberOfPages());
        }

        [NUnit.Framework.Test]
        public virtual void EstimatedNumberOfPagesInNextRendererDocumentWithTextChunkTest() {
            Document document = HtmlConverter.ConvertToDocument("<html>text</html>", new PdfWriter(new ByteArrayOutputStream
                ()));
            HtmlDocumentRenderer documentRenderer = (HtmlDocumentRenderer)document.GetRenderer();
            HtmlDocumentRenderer nextRenderer = (HtmlDocumentRenderer)documentRenderer.GetNextRenderer();
            NUnit.Framework.Assert.AreEqual(1, nextRenderer.GetEstimatedNumberOfPages());
        }

        [NUnit.Framework.Test]
        public virtual void TrimLastPageWithTrailingBlankPageTest() {
            ConverterProperties converterProperties = new ConverterProperties().SetImmediateFlush(false);
            using (Document document = HtmlConverter.ConvertToDocument("<html><body><div style='page-break-after: always'>text</div></body></html>"
                , new PdfWriter(new ByteArrayOutputStream()), converterProperties)) {
                HtmlDocumentRenderer documentRenderer = (HtmlDocumentRenderer)document.GetRenderer();
                document.GetPdfDocument().AddNewPage();
                NUnit.Framework.Assert.AreEqual(2, document.GetPdfDocument().GetNumberOfPages());
                NUnit.Framework.Assert.AreEqual(1, documentRenderer.SimulateTrimLastPage());
            }
        }

        [NUnit.Framework.Test]
        public virtual void RelayoutProcessesWaitingElementTest() {
            ConverterProperties converterProperties = new ConverterProperties().SetImmediateFlush(false);
            ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
            using (Document document = HtmlConverter.ConvertToDocument("<html><body><span>first</span><span>second</span></body></html>"
                , new PdfWriter(outputStream), converterProperties)) {
                document.Relayout();
            }
            using (PdfDocument resultDocument = new PdfDocument(new PdfReader(new MemoryStream(outputStream.ToArray())
                ))) {
                NUnit.Framework.Assert.AreEqual(1, resultDocument.GetNumberOfPages());
                String pageText = PdfTextExtractor.GetTextFromPage(resultDocument.GetPage(1));
                NUnit.Framework.Assert.IsTrue(pageText.Contains("first"));
                NUnit.Framework.Assert.IsTrue(pageText.Contains("second"));
            }
        }

        [NUnit.Framework.Test]
        public virtual void RelayoutDoesNotLeaveWrongEventHandlersHtmlDocumentRendererTest() {
            ConverterProperties converterProperties = new ConverterProperties().SetImmediateFlush(false);
            HtmlDocumentRendererTest.ThrowOnTooManyGetPagePdfDocument pdfDocument = new HtmlDocumentRendererTest.ThrowOnTooManyGetPagePdfDocument
                (new PdfWriter(new ByteArrayOutputStream()));
            pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new HtmlDocumentRendererTest.GetPageProbeOnEndPageEventHandler
                ());
            Document document = HtmlConverter.ConvertToDocument("<html><head><style>@page { @bottom-center { content: counter(page); } }"
                 + "</style></head><body><span>first</span><span>second</span></body></html>", pdfDocument, converterProperties
                );
            try {
                document.Relayout();
                pdfDocument.ResetGetPageCalls();
                pdfDocument.SetMaxGetPageCalls(7);
                NUnit.Framework.Assert.DoesNotThrow(() => document.Close());
                NUnit.Framework.Assert.AreEqual(7, pdfDocument.GetPageCalls());
            }
            finally {
                if (!pdfDocument.IsClosed()) {
                    document.Close();
                }
            }
        }

        [NUnit.Framework.Test]
        public virtual void RemoveEventHandlersBeforeRelayoutTest() {
            HtmlDocumentRendererTest.CountRemoveEventHandlerPdfDocument pdfDocument = new HtmlDocumentRendererTest.CountRemoveEventHandlerPdfDocument
                (new PdfWriter(new ByteArrayOutputStream()));
            using (Document document = new Document(pdfDocument)) {
                HtmlDocumentRenderer documentRenderer = new HtmlDocumentRenderer(document, false);
                document.SetRenderer(documentRenderer);
                pdfDocument.ResetRemoveEventHandlerCalls();
                documentRenderer.RemoveEventHandlersForRelayout();
                NUnit.Framework.Assert.AreEqual(2, pdfDocument.GetRemoveEventHandlerCalls());
            }
        }

        private sealed class ThrowOnTooManyGetPagePdfDocument : PdfDocument {
            private int pageCalls = 0;

            private int maxGetPageCalls = int.MaxValue;

            public ThrowOnTooManyGetPagePdfDocument(PdfWriter writer)
                : base(writer) {
            }

            public override PdfPage GetPage(int pageNum) {
                ++pageCalls;
                if (pageCalls > maxGetPageCalls) {
                    throw new InvalidOperationException("getPage(int) called too many times: " + pageCalls + " (max " + maxGetPageCalls
                         + ")");
                }
                return base.GetPage(pageNum);
            }

            public void ResetGetPageCalls() {
                pageCalls = 0;
            }

            public void SetMaxGetPageCalls(int maxGetPageCalls) {
                this.maxGetPageCalls = maxGetPageCalls;
            }

            public int GetPageCalls() {
                return pageCalls;
            }
        }

        private sealed class CountRemoveEventHandlerPdfDocument : PdfDocument {
            private int removeEventHandlerCalls = 0;

            public CountRemoveEventHandlerPdfDocument(PdfWriter writer)
                : base(writer) {
            }

            public override void RemoveEventHandler(AbstractPdfDocumentEventHandler handler) {
                ++removeEventHandlerCalls;
                base.RemoveEventHandler(handler);
            }

            public void ResetRemoveEventHandlerCalls() {
                removeEventHandlerCalls = 0;
            }

            public int GetRemoveEventHandlerCalls() {
                return removeEventHandlerCalls;
            }
        }

        private sealed class GetPageProbeOnEndPageEventHandler : AbstractPdfDocumentEventHandler {
            protected override void OnAcceptedEvent(AbstractPdfDocumentEvent @event) {
                if (@event is PdfDocumentEvent) {
                    PdfDocumentEvent pageEvent = (PdfDocumentEvent)@event;
                    int pageNumber = @event.GetDocument().GetPageNumber(pageEvent.GetPage());
                    @event.GetDocument().GetPage(pageNumber);
                }
            }
        }
    }
}
