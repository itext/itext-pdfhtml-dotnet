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
using iText.Kernel.Pdf;
using iText.Pdfa;
using iText.Pdfa.Exceptions;
using iText.StyledXmlParser.Resolver.Font;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf {
    [NUnit.Framework.Category("IntegrationTest")]
    public class HtmlConverterPdfA3Test : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/HtmlConverterPdfA3Test/";

        public static readonly String RESOURCES_SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/fonts/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/HtmlConverterPdfA3Test/";

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
            converterProperties.SetPdfAConformance(PdfAConformance.PDF_A_3U);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, new PdfWriter(destinationPdf), converterProperties);
            }
            HtmlConverterTest.CompareAndCheckCompliance(destinationPdf, cmpPdf);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA3USimpleFromStringTest() {
            String sourceHtml = SOURCE_FOLDER + "simple.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_simple.pdf";
            String destinationPdf = DESTINATION_FOLDER + "simple.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformance(PdfAConformance.PDF_A_3U);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            HtmlConverter.ConvertToPdf("<html>\n" + "<head><title>Test</title></head>\n" + "<body >\n" + "<form>\n" + 
                "    <p>Hello world!</p>\n" + "</form>\n" + "</body>\n" + "</html>", new PdfWriter(destinationPdf), converterProperties
                );
            HtmlConverterTest.CompareAndCheckCompliance(destinationPdf, cmpPdf);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA3ASimpleTest() {
            String sourceHtml = SOURCE_FOLDER + "simple.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_simple_a.pdf";
            String destinationPdf = DESTINATION_FOLDER + "simple_a.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformance(PdfAConformance.PDF_A_3U);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, new PdfWriter(destinationPdf), converterProperties);
            }
            HtmlConverterTest.CompareAndCheckCompliance(destinationPdf, cmpPdf);
        }

        //TODO DEVSIX-4201 adapt test when property is added
        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 2, LogLevel = LogLevelConstants.WARN)]
        public virtual void ConvertToPdfA3ColorsTest() {
            String sourceHtml = SOURCE_FOLDER + "colors.html";
            String destinationPdf = DESTINATION_FOLDER + "pdfA3ColorTest.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_pdfA3ColorTest.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformance(PdfAConformance.PDF_A_3U);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, new FileStream(destinationPdf, FileMode.Create), converterProperties
                    );
            }
            HtmlConverterTest.CompareAndCheckCompliance(destinationPdf, cmpPdf);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA3UWithCustomFontProviderTest() {
            String sourceHtml = SOURCE_FOLDER + "simple.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_simple_custom_font.pdf";
            String destinationPdf = DESTINATION_FOLDER + "simple_custom_font.pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformance(PdfAConformance.PDF_A_3U);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            BasicFontProvider fontProvider = new BasicFontProvider(false, false, false);
            fontProvider.AddFont(RESOURCES_SOURCE_FOLDER + "NotoSans-Regular.ttf");
            converterProperties.SetFontProvider(fontProvider);
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, new PdfWriter(destinationPdf), converterProperties);
            }
            HtmlConverterTest.CompareAndCheckCompliance(destinationPdf, cmpPdf);
        }

        //TODO DEVSIX-7925 - Adapt cmp file after img filter is supported
        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA3TaggedTest() {
            String sourceHtml = SOURCE_FOLDER + "mixedContent.html";
            String cmpPdf = SOURCE_FOLDER + "cmp_pdfA3TaggedTest.pdf";
            String destinationPdf = DESTINATION_FOLDER + "pdfA3TaggedTest.pdf";
            ConverterProperties converterProperties = new ConverterProperties().SetBaseUri(SOURCE_FOLDER);
            PdfADocument pdfADocument = new PdfADocument(new PdfWriter(destinationPdf), PdfAConformance.PDF_A_3U, new 
                PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", new FileStream(SOURCE_FOLDER
                 + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            pdfADocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(sourceHtml, FileMode.Open, FileAccess.Read), pdfADocument, converterProperties
                );
            HtmlConverterTest.CompareAndCheckCompliance(destinationPdf, cmpPdf);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA3UnreferencedGlyphsTest() {
            String destinationPdf = DESTINATION_FOLDER + "pdfA3FontTest.pdf";
            String html = "<html>\n" + "<head>" + "<title>Test</title></head>\n" + "<body >\n" + "<p>أميرة</p>" + "</body>\n"
                 + "</html>";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformance(PdfAConformance.PDF_A_3U);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            BasicFontProvider fontProvider = new BasicFontProvider(false, false, false);
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
        public virtual void ConvertToPdfA3ArabicFontTest() {
            String cmpPdf = SOURCE_FOLDER + "cmp_pdfA3ArabicFontTest.pdf";
            String destinationPdf = DESTINATION_FOLDER + "pdfA3ArabicFontTest.pdf";
            String html = "<html>\n" + "<head>" + "<title>Test</title></head>\n" + "<body >\n" + "<p>أميرة</p>" + "</body>\n"
                 + "</html>";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformance(PdfAConformance.PDF_A_3U);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            BasicFontProvider fontProvider = new BasicFontProvider(false, false, false);
            fontProvider.AddFont(RESOURCES_SOURCE_FOLDER + "NotoNaskhArabic-Regular.ttf");
            converterProperties.SetFontProvider(fontProvider);
            FileStream fOutput = new FileStream(destinationPdf, FileMode.Create);
            HtmlConverter.ConvertToPdf(html, fOutput, converterProperties);
            HtmlConverterTest.CompareAndCheckCompliance(destinationPdf, cmpPdf);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToPdfA3UnreferencedEmojiTest() {
            String destinationPdf = DESTINATION_FOLDER + "pdfA3UnrefEmojiTest.pdf";
            String html = "<html>\n" + "<head>" + "<title>Test</title></head>\n" + "<body >\n" + "<p>\uD83D\uDE09</p>"
                 + "</body>\n" + "</html>";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformance(PdfAConformance.PDF_A_3U);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            BasicFontProvider fontProvider = new BasicFontProvider(false, false, false);
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
        public virtual void ConvertToPdfA3EmojiTest() {
            String destinationPdf = DESTINATION_FOLDER + "pdfA3EmojiTest.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_pdfA3EmojiTest.pdf";
            String html = "<html>\n" + "<head>" + "<title>Test</title></head>\n" + "<body>\n" + "<p>\uD83D\uDE09</p>" 
                + "</body>\n" + "</html>";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetPdfAConformance(PdfAConformance.PDF_A_3U);
            converterProperties.SetDocumentOutputIntent(new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1"
                , new FileStream(SOURCE_FOLDER + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read)));
            BasicFontProvider fontProvider = new BasicFontProvider(false, false, false);
            fontProvider.AddFont(RESOURCES_SOURCE_FOLDER + "NotoEmoji-Regular.ttf");
            converterProperties.SetFontProvider(fontProvider);
            FileStream fOutput = new FileStream(destinationPdf, FileMode.Create);
            HtmlConverter.ConvertToPdf(html, fOutput, converterProperties);
            HtmlConverterTest.CompareAndCheckCompliance(destinationPdf, cmpPdf);
        }
    }
}
