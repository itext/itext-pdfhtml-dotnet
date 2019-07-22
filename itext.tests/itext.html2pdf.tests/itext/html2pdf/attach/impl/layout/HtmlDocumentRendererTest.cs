using iText.IO.Source;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;

namespace iText.Html2pdf.Attach.Impl.Layout {
    public class HtmlDocumentRendererTest {
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
    }
}
