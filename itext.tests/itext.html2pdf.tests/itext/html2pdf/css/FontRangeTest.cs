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
using iText.Html2pdf;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Pdf;
using iText.Layout.Font;
using iText.Layout.Font.Selectorstrategy;
using iText.StyledXmlParser.Resolver.Font;
using iText.Test;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class FontRangeTest : ExtendedITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FontRangeTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FontRangeTest/";

        public static readonly String FONTS_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/fonts/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FontCharRangeTest() {
            char glyph = '\u00B6';
            String HTML = "<!DOCTYPE html><html lang=en><head></head><body>Hello" + glyph + "World</body></html>";
            String font = FONTS_FOLDER + "Bokor-Regular.ttf";
            String dest = DESTINATION_FOLDER + "fontRangeTest.pdf";
            FontProvider fontProvider = new BasicFontProvider(false, false, false);
            FontProgram fontProgram = FontProgramFactory.CreateFont(font);
            fontProvider.SetFontSelectorStrategyFactory(new BestMatchFontSelectorStrategy.BestMatchFontSelectorStrategyFactory
                ());
            fontProvider.AddFont(fontProgram);
            fontProvider.AddFont(StandardFonts.HELVETICA, PdfEncodings.WINANSI, new RangeBuilder((int)glyph).Create());
            ConverterProperties properties = new ConverterProperties();
            properties.SetFontProvider(fontProvider);
            HtmlConverter.ConvertToPdf(HTML, new PdfWriter(dest), properties);
            PdfDocument pdfDocument = new PdfDocument(new PdfReader(dest));
            String contentStream = iText.Commons.Utils.JavaUtil.GetStringForBytes(pdfDocument.GetPage(1).GetFirstContentStream
                ().GetBytes(), System.Text.Encoding.UTF8);
            //Currently we will find only one mention of our first font in the contentstream.
            //Expected we would find it twice. /F1  for hello - /F2 for the glyph - /F1 again for world.
            int count = iText.Commons.Utils.StringUtil.Split(contentStream, "/F1").Length - 1;
            NUnit.Framework.Assert.AreEqual(2, count, "The result does not find the expected number of occurrences.");
        }
    }
}
