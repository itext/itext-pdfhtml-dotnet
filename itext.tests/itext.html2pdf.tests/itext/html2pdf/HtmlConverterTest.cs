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
using System.IO;
using iText.Html2pdf.Exceptions;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Test;
using iText.Test.Pdfa;

namespace iText.Html2pdf {
    [NUnit.Framework.Category("IntegrationTest")]
    public class HtmlConverterTest : ExtendedITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/HtmlConverterTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/HtmlConverterTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA2SimpleTest() {
            String sourceHtml = SOURCE_FOLDER + "simple.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_simple.pdf";
            String destinationPdf = DESTINATION_FOLDER + "simple.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformance(PdfAConformance.PDF_A_2B);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, new PdfWriter(destinationPdf), converterProperties);
            }
            CompareAndCheckCompliance(destinationPdf, cmpPdf);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfASimpleTest() {
            String sourceHtml = SOURCE_FOLDER + "simple.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_simple.pdf";
            String destinationPdf = DESTINATION_FOLDER + "simple.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformance(PdfAConformance.PDF_A_1B);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, new PdfWriter(destinationPdf), converterProperties);
            }
            CompareAndCheckCompliance(destinationPdf, cmpPdf);
        }

        [NUnit.Framework.Test]
        public virtual void CannotConvertHtmlToDocumentInReadingModeTest() {
            Exception exception = NUnit.Framework.Assert.Catch(typeof(Html2PdfException), () => {
                PdfDocument pdfDocument = CreateTempDoc();
                ConverterProperties properties = new ConverterProperties();
                Document document = HtmlConverter.ConvertToDocument("", pdfDocument, properties);
            }
            );
            NUnit.Framework.Assert.AreEqual(Html2PdfException.PDF_DOCUMENT_SHOULD_BE_IN_WRITING_MODE, exception.Message
                );
        }

        [NUnit.Framework.Test]
        public virtual void InputsWithTitleTagsAsAltDescTest() {
            String sourceHtml = SOURCE_FOLDER + "inputsWithTitleTagsAsAltDesc.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_inputsWithTitleTagsAsAltDesc.pdf";
            String destinationPdf = DESTINATION_FOLDER + "inputsWithTitleTagsAsAltDesc.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetCreateAcroForm(true);
            PdfDocument document = new PdfDocument(new PdfWriter(destinationPdf));
            document.SetTagged();
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, document, converterProperties);
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationPdf, cmpPdf, DESTINATION_FOLDER
                , "diff_simple_"));
        }

        private static PdfDocument CreateTempDoc() {
            MemoryStream outputStream = new MemoryStream();
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outputStream));
            pdfDocument.AddNewPage();
            pdfDocument.Close();
            pdfDocument = new PdfDocument(new PdfReader(new MemoryStream(outputStream.ToArray())));
            return pdfDocument;
        }

        protected internal static void CompareAndCheckCompliance(String destinationPdf, String cmpPdf) {
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationPdf, cmpPdf, DESTINATION_FOLDER
                , "diff_simple_"));
            VeraPdfValidator veraPdfValidator = new VeraPdfValidator();
            NUnit.Framework.Assert.IsNull(veraPdfValidator.Validate(destinationPdf));
        }
    }
}
