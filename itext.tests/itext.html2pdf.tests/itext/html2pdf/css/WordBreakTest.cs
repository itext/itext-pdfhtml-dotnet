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
            // TODO: update cmp file after implementing DEVSIX-1438
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
            // TODO: update cmp file after implementing DEVSIX-1438
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
            // TODO: update cmp file after implementing DEVSIX-1438
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "whiteSpaceAndWordBreak.html"), new FileInfo(destinationFolder
                 + "whiteSpaceAndWordBreak.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "whiteSpaceAndWordBreak.pdf"
                , sourceFolder + "cmp_whiteSpaceAndWordBreak.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void WordBreakMidNumbersTest() {
            // TODO: update cmp file after implementing DEVSIX-1438
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
            // TODO: update cmp file after implementing DEVSIX-1438
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "wordBreakMidPunctuation.html"), new FileInfo(destinationFolder
                 + "wordBreakMidPunctuation.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "wordBreakMidPunctuation.pdf"
                , sourceFolder + "cmp_wordBreakMidPunctuation.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void WordBreakAllAndFloatTest() {
            // TODO: update cmp file after implementing DEVSIX-1438
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "wordBreakAllAndFloat.html"), new FileInfo(destinationFolder
                 + "wordBreakAllAndFloat.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "wordBreakAllAndFloat.pdf"
                , sourceFolder + "cmp_wordBreakAllAndFloat.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH, Count = 3)]
        public virtual void WordBreakTableScenarioTest() {
            // TODO: update cmp file after implementing DEVSIX-1438
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "wordBreakTableScenario.html"), new FileInfo(destinationFolder
                 + "wordBreakTableScenario.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "wordBreakTableScenario.pdf"
                , sourceFolder + "cmp_wordBreakTableScenario.pdf", destinationFolder));
        }
    }
}
