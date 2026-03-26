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
using iText.Kernel.Utils;
using iText.Layout.Font;
using iText.StyledXmlParser.Resolver.Font;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class WordBreakTest : ExtendedITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/WordBreakTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/WordBreakTest/";

        private static readonly String FONTS_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/fonts/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void WordBreakCommonScenarioTest() {
            FontProvider fontProvider = new BasicFontProvider();
            fontProvider.AddFont(FONTS_FOLDER + "NotoSansCJKjp-Regular.otf");
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetFontProvider(fontProvider);
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "wordBreakCommonScenario.html"), new FileInfo(DESTINATION_FOLDER
                 + "wordBreakCommonScenario.pdf"), converterProperties);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "wordBreakCommonScenario.pdf"
                , SOURCE_FOLDER + "cmp_wordBreakCommonScenario.pdf", DESTINATION_FOLDER));
        }

        [NUnit.Framework.Test]
        public virtual void OverflowXWordBreakTest() {
            FontProvider fontProvider = new BasicFontProvider();
            fontProvider.AddFont(FONTS_FOLDER + "NotoSansCJKjp-Regular.otf");
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetFontProvider(fontProvider);
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "overflowXWordBreak.html"), new FileInfo(DESTINATION_FOLDER
                 + "overflowXWordBreak.pdf"), converterProperties);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "overflowXWordBreak.pdf"
                , SOURCE_FOLDER + "cmp_overflowXWordBreak.pdf", DESTINATION_FOLDER));
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceAndWordBreakTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "whiteSpaceAndWordBreak.html"), new FileInfo(DESTINATION_FOLDER
                 + "whiteSpaceAndWordBreak.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "whiteSpaceAndWordBreak.pdf"
                , SOURCE_FOLDER + "cmp_whiteSpaceAndWordBreak.pdf", DESTINATION_FOLDER));
        }

        [NUnit.Framework.Test]
        public virtual void WordBreakMidNumbersTest() {
            FontProvider fontProvider = new BasicFontProvider();
            fontProvider.AddFont(FONTS_FOLDER + "NotoSansCJKjp-Regular.otf");
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetFontProvider(fontProvider);
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "wordBreakMidNumbers.html"), new FileInfo(DESTINATION_FOLDER
                 + "wordBreakMidNumbers.pdf"), converterProperties);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "wordBreakMidNumbers.pdf"
                , SOURCE_FOLDER + "cmp_wordBreakMidNumbers.pdf", DESTINATION_FOLDER));
        }

        [NUnit.Framework.Test]
        public virtual void WordBreakMidPunctuationTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "wordBreakMidPunctuation.html"), new FileInfo(DESTINATION_FOLDER
                 + "wordBreakMidPunctuation.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "wordBreakMidPunctuation.pdf"
                , SOURCE_FOLDER + "cmp_wordBreakMidPunctuation.pdf", DESTINATION_FOLDER));
        }

        [NUnit.Framework.Test]
        public virtual void WordBreakAllAndFloatTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "wordBreakAllAndFloat.html"), new FileInfo(DESTINATION_FOLDER
                 + "wordBreakAllAndFloat.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "wordBreakAllAndFloat.pdf"
                , SOURCE_FOLDER + "cmp_wordBreakAllAndFloat.pdf", DESTINATION_FOLDER));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH, Count = 
            3)]
        public virtual void WordBreakTableScenarioTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + "wordBreakTableScenario.html"), new FileInfo(DESTINATION_FOLDER
                 + "wordBreakTableScenario.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "wordBreakTableScenario.pdf"
                , SOURCE_FOLDER + "cmp_wordBreakTableScenario.pdf", DESTINATION_FOLDER));
        }
    }
}
