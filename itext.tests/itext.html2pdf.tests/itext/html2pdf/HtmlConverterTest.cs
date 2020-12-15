using System.IO;
using iText.Html2pdf.Exceptions;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Test;

namespace iText.Html2pdf {
    public class HtmlConverterTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void CannotConvertHtmlToDocumentInReadingModeTest() {
            NUnit.Framework.Assert.That(() =>  {
                PdfDocument pdfDocument = CreateTempDoc();
                ConverterProperties properties = new ConverterProperties();
                Document document = HtmlConverter.ConvertToDocument("", pdfDocument, properties);
            }
            , NUnit.Framework.Throws.InstanceOf<Html2PdfException>().With.Message.EqualTo(Html2PdfException.PDF_DOCUMENT_SHOULD_BE_IN_WRITING_MODE))
;
        }

        private static PdfDocument CreateTempDoc() {
            MemoryStream outputStream = new MemoryStream();
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outputStream));
            pdfDocument.AddNewPage();
            pdfDocument.Close();
            pdfDocument = new PdfDocument(new PdfReader(new MemoryStream(outputStream.ToArray())));
            return pdfDocument;
        }
    }
}
