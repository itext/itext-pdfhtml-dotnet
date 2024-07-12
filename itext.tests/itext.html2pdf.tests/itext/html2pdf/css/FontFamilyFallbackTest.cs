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
using iText.Html2pdf;
using iText.IO.Font;
using iText.Kernel.Pdf;
using iText.Layout.Font;
using iText.Test;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class FontFamilyFallbackTest : ExtendedITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FontFamilyFallbackTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FontFamilyFallbackTest/";

        public static readonly String FONTSFOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/fonts/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NoJapaneseGlyphsTest() {
            String htmlPath = SOURCE_FOLDER + "glyphsNotFound.html";
            String pdfPath = DESTINATION_FOLDER + "glyphsNotFound.pdf";
            FontProgram font = FontProgramFactory.CreateFont(FONTSFOLDER + "Bokor-Regular.ttf");
            FontProgram backUpFont = FontProgramFactory.CreateFont(FONTSFOLDER + "NotoSansJP-Bold.ttf");
            FontProvider dfp = new FontProvider();
            dfp.AddFont(font);
            dfp.AddFont(backUpFont);
            ConverterProperties props = new ConverterProperties();
            props.SetFontProvider(dfp);
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(pdfPath), props);
            String basefontName = "";
            int fontDictionarySize = 0;
            using (PdfDocument resultPdf = new PdfDocument(new PdfReader(pdfPath))) {
                PdfDictionary resources = resultPdf.GetPage(1).GetResources().GetPdfObject();
                PdfDictionary fontDictionary = resources.GetAsDictionary(PdfName.Font);
                fontDictionarySize = fontDictionary.Size();
                basefontName = fontDictionary.GetAsDictionary(new PdfName("F1")).GetAsName(PdfName.BaseFont).GetValue();
            }
            NUnit.Framework.Assert.AreEqual(2, fontDictionarySize, "PDF contains a number of fonts different from expected."
                );
            NUnit.Framework.Assert.IsTrue(basefontName.Contains("NotoSansJP-Bold"), "Base font name is different from expected."
                );
        }

        [NUnit.Framework.Test]
        public virtual void MixedEnglishJapaneseTest() {
            String htmlPath = SOURCE_FOLDER + "mixedJapaneseEnglish.html";
            String pdfPath = DESTINATION_FOLDER + "mixedJapaneseEnglish.pdf";
            FontProgram font = FontProgramFactory.CreateFont(FONTSFOLDER + "Bokor-Regular.ttf");
            FontProgram backUpFont = FontProgramFactory.CreateFont(FONTSFOLDER + "NotoSansJP-Bold.ttf");
            FontProvider dfp = new FontProvider();
            dfp.AddFont(font);
            dfp.AddFont(backUpFont);
            ConverterProperties props = new ConverterProperties();
            props.SetFontProvider(dfp);
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(pdfPath), props);
            int fontDictionarySize = 0;
            using (PdfDocument resultPdf = new PdfDocument(new PdfReader(pdfPath))) {
                PdfDictionary resources = resultPdf.GetPage(1).GetResources().GetPdfObject();
                PdfDictionary fontDictionary = resources.GetAsDictionary(PdfName.Font);
                fontDictionarySize = fontDictionary.Size();
            }
            NUnit.Framework.Assert.AreEqual(2, fontDictionarySize, "PDF contains " + fontDictionarySize + " and not the expected 2."
                );
        }
    }
}
