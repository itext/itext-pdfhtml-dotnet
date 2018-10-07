using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css {
    public class FontSelectorTimesFontTest : ExtendedFontPropertiesTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FontSelectorTimesFontTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FontSelectorTimesFontTest/";

        private static String[] FONT_WEIGHTS = new String[] { "normal", "bold", "100", "300", "500", "600", "700", 
            "900" };

        private static String[] FONT_STYLES = new String[] { "normal", "italic", "oblique" };

        // TODO DEVSIX-2114 Add bolder/lighter font-weights once they are supported
        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TimesFontFamilyTest() {
            String text = "quick brown fox jumps over the lazy dog";
            String[] fontFamilies = new String[] { "times", "roman", "times roman", "times-roman", "times bold", "times-bold"
                , "times-italic", "times-italic", "times roman italic", "times-roman italic", "times roman bold", "times-roman bold"
                 };
            String fileName = "timesFontFamilyTest";
            String htmlString = BuildDocumentTree(fontFamilies, FONT_WEIGHTS, FONT_STYLES, null, text);
            RunTest(htmlString, sourceFolder, destinationFolder, fileName, fileName);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TimesFontFamilyTest02() {
            String text = "quick brown fox jumps over the lazy dog";
            String[] fontFamilies = new String[] { "times", "new", "roman", "times new", "times roman", "new roman", "times new roman"
                , "times new toman", "times mew roman", "mimes new roman" };
            String fileName = "timesFontFamilyTest02";
            String htmlString = BuildDocumentTree(fontFamilies, new String[] { "normal" }, new String[] { "normal" }, 
                null, text);
            RunTest(htmlString, sourceFolder, destinationFolder, fileName, fileName);
        }
    }
}
