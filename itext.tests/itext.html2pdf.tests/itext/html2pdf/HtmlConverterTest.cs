/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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
using iText.Commons.Utils;
using iText.Html2pdf.Exceptions;
using iText.Html2pdf.Resolver.Font;
using iText.IO.Util;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Pdfa;
using iText.Pdfa.Exceptions;
using iText.Test;
using iText.Test.Pdfa;

namespace iText.Html2pdf {
    [NUnit.Framework.Category("IntegrationTest")]
    public class HtmlConverterTest : ExtendedITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/HtmlConverterTest/";

        public static readonly String RESOURCES_SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/fonts/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/HtmlConverterTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA3USimpleTest() {
            String sourceHtml = SOURCE_FOLDER + "simple.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_simple.pdf";
            String destinationPdf = DESTINATION_FOLDER + "simple.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformanceLevel(PdfAConformanceLevel.PDF_A_3U);
            converterProperties.SetOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, new PdfWriter(destinationPdf), converterProperties);
            }
            CompareAndCheckCompliance(sourceHtml, destinationPdf, cmpPdf);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA3USimpleFromStringTest() {
            String sourceHtml = SOURCE_FOLDER + "simple.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_simple.pdf";
            String destinationPdf = DESTINATION_FOLDER + "simple.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformanceLevel(PdfAConformanceLevel.PDF_A_3U);
            converterProperties.SetOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            HtmlConverter.ConvertToPdf("<html>\n" + "<head><title>Test</title></head>\n" + "<body >\n" + "<form>\n" + 
                "    <p>Hello world!</p>\n" + "</form>\n" + "</body>\n" + "</html>", new PdfWriter(destinationPdf), converterProperties
                );
            CompareAndCheckCompliance(sourceHtml, destinationPdf, cmpPdf);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA3ASimpleTest() {
            String sourceHtml = SOURCE_FOLDER + "simple.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_simple_a.pdf";
            String destinationPdf = DESTINATION_FOLDER + "simple_a.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformanceLevel(PdfAConformanceLevel.PDF_A_3U);
            converterProperties.SetOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, new PdfWriter(destinationPdf), converterProperties);
            }
            CompareAndCheckCompliance(sourceHtml, destinationPdf, cmpPdf);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA3UWithCustomFontProviderTest() {
            String sourceHtml = SOURCE_FOLDER + "simple.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_simple_custom_font.pdf";
            String destinationPdf = DESTINATION_FOLDER + "simple_custom_font.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformanceLevel(PdfAConformanceLevel.PDF_A_3U);
            converterProperties.SetOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            DefaultFontProvider fontProvider = new DefaultFontProvider(false, false, false);
            fontProvider.AddFont(RESOURCES_SOURCE_FOLDER + "NotoSans-Regular.ttf");
            converterProperties.SetFontProvider(fontProvider);
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, new PdfWriter(destinationPdf), converterProperties);
            }
            CompareAndCheckCompliance(sourceHtml, destinationPdf, cmpPdf);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA4SimpleTest() {
            String sourceHtml = SOURCE_FOLDER + "simple.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_simpleA4.pdf";
            String destinationPdf = DESTINATION_FOLDER + "simpleA4.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformanceLevel(PdfAConformanceLevel.PDF_A_4);
            converterProperties.SetOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, new FileStream(destinationPdf, FileMode.Create), converterProperties
                    );
            }
            CompareAndCheckCompliance(sourceHtml, destinationPdf, cmpPdf);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfAWithProvidingPdADocumentInstanceTest() {
            String sourceHtml = SOURCE_FOLDER + "simple.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_simple_doc.pdf";
            String destinationPdf = DESTINATION_FOLDER + "simple_doc.pdf";
            PdfWriter writer = new PdfWriter(destinationPdf, new WriterProperties().SetPdfVersion(PdfVersion.PDF_2_0));
            PdfADocument pdfDocument = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_4E, new PdfOutputIntent("Custom"
                , "", "http://www.color.org", "sRGB IEC61966-2.1", new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm"
                , FileMode.Open, FileAccess.Read)));
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, pdfDocument);
            }
            CompareAndCheckCompliance(sourceHtml, destinationPdf, cmpPdf);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfAWithoutIcmProfileTest() {
            String sourceHtml = SOURCE_FOLDER + "simple.html";
            String destinationPdf = DESTINATION_FOLDER + "simple_without_icm.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformanceLevel(PdfAConformanceLevel.PDF_A_4);
            Exception e = NUnit.Framework.Assert.Catch(typeof(PdfAConformanceException), () => HtmlConverter.ConvertToPdf
                (new FileStream(sourceHtml, FileMode.Open, FileAccess.Read), new FileStream(destinationPdf, FileMode.Create
                ), converterProperties));
            NUnit.Framework.Assert.AreEqual(MessageFormatUtil.Format(PdfaExceptionMessageConstant.DEVICEGRAY_SHALL_ONLY_BE_USED_IF_CURRENT_PDFA_OUTPUT_INTENT_OR_DEFAULTGRAY_IN_USAGE_CONTEXT
                ), e.Message);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfAWithProvidingPdADocumentAndCustomFontProviderTest() {
            String sourceHtml = SOURCE_FOLDER + "simple.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_simple_doc_custom_font.pdf";
            String destinationPdf = DESTINATION_FOLDER + "simple_doc_custom_font.pdf";
            ConverterProperties properties = new ConverterProperties();
            DefaultFontProvider fontProvider = new DefaultFontProvider(false, false, false);
            fontProvider.AddFont(RESOURCES_SOURCE_FOLDER + "NotoSans-Regular.ttf");
            properties.SetFontProvider(fontProvider);
            PdfWriter writer = new PdfWriter(destinationPdf, new WriterProperties().SetPdfVersion(PdfVersion.PDF_2_0));
            PdfADocument pdfDocument = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_4E, new PdfOutputIntent("Custom"
                , "", "http://www.color.org", "sRGB IEC61966-2.1", new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm"
                , FileMode.Open, FileAccess.Read)));
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, pdfDocument, properties);
            }
            CompareAndCheckCompliance(sourceHtml, destinationPdf, cmpPdf);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfAWithProvidingPdADocumentInstanceWithDefinedConformanceTest() {
            String sourceHtml = SOURCE_FOLDER + "simple.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_simple_doc.pdf";
            String destinationPdf = DESTINATION_FOLDER + "simple_doc.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformanceLevel(PdfAConformanceLevel.PDF_A_4);
            converterProperties.SetOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            PdfWriter writer = new PdfWriter(destinationPdf, new WriterProperties().SetPdfVersion(PdfVersion.PDF_2_0));
            PdfADocument pdfDocument = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_4E, new PdfOutputIntent("Custom"
                , "", "http://www.color.org", "sRGB IEC61966-2.1", new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm"
                , FileMode.Open, FileAccess.Read)));
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, pdfDocument, converterProperties);
            }
            CompareAndCheckCompliance(sourceHtml, destinationPdf, cmpPdf);
        }

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

        private static void CompareAndCheckCompliance(String sourceHtml, String destinationPdf, String cmpPdf) {
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(sourceHtml) + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationPdf, cmpPdf, DESTINATION_FOLDER
                , "diff_simple_"));
            VeraPdfValidator veraPdfValidator = new VeraPdfValidator();
            NUnit.Framework.Assert.IsNull(veraPdfValidator.Validate(destinationPdf));
        }
    }
}
