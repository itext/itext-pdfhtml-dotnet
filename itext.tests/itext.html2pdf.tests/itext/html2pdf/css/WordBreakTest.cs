/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
Authors: iText Software.

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
using iText.Html2pdf.Resolver.Font;
using iText.Kernel.Utils;
using iText.Layout.Font;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class WordBreakTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/WordBreakTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/WordBreakTest/";

        public static readonly String fontsFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/CJKFonts/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void WordBreakCommonScenarioTest() {
            FontProvider fontProvider = new DefaultFontProvider();
            fontProvider.AddFont(fontsFolder + "NotoSansCJKjp-Regular.otf");
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetFontProvider(fontProvider);
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "wordBreakCommonScenario.html"), new FileInfo(destinationFolder
                 + "wordBreakCommonScenario.pdf"), converterProperties);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "wordBreakCommonScenario.pdf"
                , sourceFolder + "cmp_wordBreakCommonScenario.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void OverflowXWordBreakTest() {
            FontProvider fontProvider = new DefaultFontProvider();
            fontProvider.AddFont(fontsFolder + "NotoSansCJKjp-Regular.otf");
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetFontProvider(fontProvider);
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "overflowXWordBreak.html"), new FileInfo(destinationFolder
                 + "overflowXWordBreak.pdf"), converterProperties);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "overflowXWordBreak.pdf"
                , sourceFolder + "cmp_overflowXWordBreak.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceAndWordBreakTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "whiteSpaceAndWordBreak.html"), new FileInfo(destinationFolder
                 + "whiteSpaceAndWordBreak.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "whiteSpaceAndWordBreak.pdf"
                , sourceFolder + "cmp_whiteSpaceAndWordBreak.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void WordBreakMidNumbersTest() {
            FontProvider fontProvider = new DefaultFontProvider();
            fontProvider.AddFont(fontsFolder + "NotoSansCJKjp-Regular.otf");
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetFontProvider(fontProvider);
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "wordBreakMidNumbers.html"), new FileInfo(destinationFolder
                 + "wordBreakMidNumbers.pdf"), converterProperties);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "wordBreakMidNumbers.pdf"
                , sourceFolder + "cmp_wordBreakMidNumbers.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void WordBreakMidPunctuationTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "wordBreakMidPunctuation.html"), new FileInfo(destinationFolder
                 + "wordBreakMidPunctuation.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "wordBreakMidPunctuation.pdf"
                , sourceFolder + "cmp_wordBreakMidPunctuation.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void WordBreakAllAndFloatTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "wordBreakAllAndFloat.html"), new FileInfo(destinationFolder
                 + "wordBreakAllAndFloat.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "wordBreakAllAndFloat.pdf"
                , sourceFolder + "cmp_wordBreakAllAndFloat.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH, Count = 
            3)]
        public virtual void WordBreakTableScenarioTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "wordBreakTableScenario.html"), new FileInfo(destinationFolder
                 + "wordBreakTableScenario.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "wordBreakTableScenario.pdf"
                , sourceFolder + "cmp_wordBreakTableScenario.pdf", destinationFolder));
        }
    }
}
