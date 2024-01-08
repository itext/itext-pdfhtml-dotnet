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
using System.IO;
using iText.Commons.Utils;
using iText.Html2pdf.Logs;
using iText.Html2pdf.Resolver.Font;
using iText.Kernel.Pdf;
using iText.Pdfa;
using iText.Pdfa.Checker;
using iText.Pdfa.Exceptions;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf {
    [NUnit.Framework.Category("IntegrationTest")]
    public class HtmlConverterPdfA4Test : ExtendedITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/HtmlConverterPdfA4Test/";

        public static readonly String RESOURCES_SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/fonts/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/HtmlConverterPdfA4Test/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA4SimpleTest() {
            String sourceHtml = SOURCE_FOLDER + "simple.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_simpleA4.pdf";
            String destinationPdf = DESTINATION_FOLDER + "simpleA4.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformanceLevel(PdfAConformanceLevel.PDF_A_4);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            FileStream fOutputDest = new FileStream(destinationPdf, FileMode.Create);
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, fOutputDest, converterProperties);
            }
            fOutputDest.Dispose();
            HtmlConverterTest.CompareAndCheckCompliance(destinationPdf, cmpPdf);
        }

        //TODO DEVSIX-4201 adapt test when property is added
        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, Count = 2, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void ConvertToPdfA4ColorsTest() {
            String sourceHtml = SOURCE_FOLDER + "colors.html";
            String destinationPdf = DESTINATION_FOLDER + "pdfA4ColorTest.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_pdfA4ColorTest.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformanceLevel(PdfAConformanceLevel.PDF_A_4);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, new FileStream(destinationPdf, FileMode.Create), converterProperties
                    );
            }
            HtmlConverterTest.CompareAndCheckCompliance(destinationPdf, cmpPdf);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA4MetaDataInvalidTest() {
            String sourceHtml = SOURCE_FOLDER + "simple.html";
            String destinationPdf = DESTINATION_FOLDER + "pdfA4InvalidMetadataTest.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformanceLevel(PdfAConformanceLevel.PDF_A_4);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            converterProperties.SetBaseUri("no_link");
            using (FileStream fOutput = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                using (PdfWriter pdfWriter = new PdfWriter(destinationPdf)) {
                    Exception e = NUnit.Framework.Assert.Catch(typeof(PdfAConformanceException), () => {
                        HtmlConverter.ConvertToPdf(fOutput, pdfWriter, converterProperties);
                    }
                    );
                    NUnit.Framework.Assert.AreEqual(MessageFormatUtil.Format(PdfaExceptionMessageConstant.THE_FILE_HEADER_SHALL_CONTAIN_RIGHT_PDF_VERSION
                        , "2"), e.Message);
                }
            }
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA4ArabicFontTest() {
            String cmpPdf = SOURCE_FOLDER + "cmp_pdfA4ArabicFontTest.pdf";
            String destinationPdf = DESTINATION_FOLDER + "pdfA4ArabicFontTest.pdf";
            String html = "<html>\n" + "<head>" + "<title>Test</title></head>\n" + "<body >\n" + "<p>أميرة</p>" + "</body>\n"
                 + "</html>";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformanceLevel(PdfAConformanceLevel.PDF_A_4);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            DefaultFontProvider fontProvider = new DefaultFontProvider(false, false, false);
            fontProvider.AddFont(RESOURCES_SOURCE_FOLDER + "NotoNaskhArabic-Regular.ttf");
            converterProperties.SetFontProvider(fontProvider);
            FileStream fOutput = new FileStream(destinationPdf, FileMode.Create);
            HtmlConverter.ConvertToPdf(html, fOutput, converterProperties);
            HtmlConverterTest.CompareAndCheckCompliance(destinationPdf, cmpPdf);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA4UnreferencedGlyphsTest() {
            String destinationPdf = DESTINATION_FOLDER + "pdfA4UnrefFontTest.pdf";
            String html = "<html>\n" + "<head>" + "<title>Test</title></head>\n" + "<body >\n" + "<p>أميرة</p>" + "</body>\n"
                 + "</html>";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformanceLevel(PdfAConformanceLevel.PDF_A_4);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            DefaultFontProvider fontProvider = new DefaultFontProvider(false, false, false);
            fontProvider.AddFont(RESOURCES_SOURCE_FOLDER + "NotoSans-Regular.ttf");
            converterProperties.SetFontProvider(fontProvider);
            using (FileStream fOutput = new FileStream(destinationPdf, FileMode.Create)) {
                Exception e = NUnit.Framework.Assert.Catch(typeof(PdfAConformanceException), () => {
                    HtmlConverter.ConvertToPdf(html, fOutput, converterProperties);
                }
                );
                NUnit.Framework.Assert.AreEqual(MessageFormatUtil.Format(PdfaExceptionMessageConstant.EMBEDDED_FONTS_SHALL_DEFINE_ALL_REFERENCED_GLYPHS
                    ), e.Message);
            }
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA4UnreferencedEmojiTest() {
            String destinationPdf = DESTINATION_FOLDER + "pdfA4UnrefEmojiTest.pdf";
            String html = "<html>\n" + "<head>" + "<title>Test</title></head>\n" + "<body >\n" + "<p>\uD83D\uDE09</p>"
                 + "</body>\n" + "</html>";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformanceLevel(PdfAConformanceLevel.PDF_A_4);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            DefaultFontProvider fontProvider = new DefaultFontProvider(false, false, false);
            fontProvider.AddFont(RESOURCES_SOURCE_FOLDER + "NotoSans-Regular.ttf");
            converterProperties.SetFontProvider(fontProvider);
            using (FileStream fOutput = new FileStream(destinationPdf, FileMode.Create)) {
                Exception e = NUnit.Framework.Assert.Catch(typeof(PdfAConformanceException), () => {
                    HtmlConverter.ConvertToPdf(html, fOutput, converterProperties);
                }
                );
                NUnit.Framework.Assert.AreEqual(MessageFormatUtil.Format(PdfaExceptionMessageConstant.EMBEDDED_FONTS_SHALL_DEFINE_ALL_REFERENCED_GLYPHS
                    ), e.Message);
            }
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA4EmojiTest() {
            String destinationPdf = DESTINATION_FOLDER + "pdfA4EmojiTest.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_pdfA4EmojiTest.pdf";
            String html = "<html>\n" + "<head>" + "<title>Test</title></head>\n" + "<body>\n" + "<p>\uD83D\uDE09</p>" 
                + "</body>\n" + "</html>";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformanceLevel(PdfAConformanceLevel.PDF_A_4);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            DefaultFontProvider fontProvider = new DefaultFontProvider(false, false, false);
            fontProvider.AddFont(RESOURCES_SOURCE_FOLDER + "NotoEmoji-Regular.ttf");
            converterProperties.SetFontProvider(fontProvider);
            FileStream fOutput = new FileStream(destinationPdf, FileMode.Create);
            HtmlConverter.ConvertToPdf(html, fOutput, converterProperties);
            HtmlConverterTest.CompareAndCheckCompliance(destinationPdf, cmpPdf);
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
            HtmlConverterTest.CompareAndCheckCompliance(destinationPdf, cmpPdf);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfAWithoutIcmProfileTest() {
            String sourceHtml = SOURCE_FOLDER + "simple.html";
            String destinationPdf = DESTINATION_FOLDER + "simple_without_icm.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformanceLevel(PdfAConformanceLevel.PDF_A_4);
            using (FileStream fOutputHtml = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                using (FileStream fOutputDest = new FileStream(destinationPdf, FileMode.Create)) {
                    Exception e = NUnit.Framework.Assert.Catch(typeof(PdfAConformanceException), () => HtmlConverter.ConvertToPdf
                        (fOutputHtml, fOutputDest, converterProperties));
                    NUnit.Framework.Assert.AreEqual(MessageFormatUtil.Format(PdfaExceptionMessageConstant.DEVICEGRAY_SHALL_ONLY_BE_USED_IF_CURRENT_PDFA_OUTPUT_INTENT_OR_DEFAULTGRAY_IN_USAGE_CONTEXT
                        ), e.Message);
                }
            }
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
            HtmlConverterTest.CompareAndCheckCompliance(destinationPdf, cmpPdf);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfAWithProvidingPdADocumentInstanceWithDefinedConformanceTest() {
            String sourceHtml = SOURCE_FOLDER + "simple.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_simple_doc.pdf";
            String destinationPdf = DESTINATION_FOLDER + "simple_doc.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformanceLevel(PdfAConformanceLevel.PDF_A_3U);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            PdfWriter writer = new PdfWriter(destinationPdf, new WriterProperties().SetPdfVersion(PdfVersion.PDF_2_0));
            PdfADocument pdfDocument = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_4E, new PdfOutputIntent("Custom"
                , "", "http://www.color.org", "sRGB IEC61966-2.1", new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm"
                , FileMode.Open, FileAccess.Read)));
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, pdfDocument, converterProperties);
            }
            using (PdfADocument pdfADocument = new PdfADocument(new PdfReader(destinationPdf), new PdfWriter(new MemoryStream
                ()))) {
                new PdfA4Checker(PdfAConformanceLevel.PDF_A_4E).CheckDocument(pdfADocument.GetCatalog());
            }
            HtmlConverterTest.CompareAndCheckCompliance(destinationPdf, cmpPdf);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA4LinearGradientTest() {
            String sourceHtml = SOURCE_FOLDER + "gradient.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_pdfA4LinGradient.pdf";
            String destinationPdf = DESTINATION_FOLDER + "pdfA4LinGradient.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformanceLevel(PdfAConformanceLevel.PDF_A_4);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, new FileStream(destinationPdf, FileMode.Create), converterProperties
                    );
            }
            HtmlConverterTest.CompareAndCheckCompliance(destinationPdf, cmpPdf);
        }
    }
}
