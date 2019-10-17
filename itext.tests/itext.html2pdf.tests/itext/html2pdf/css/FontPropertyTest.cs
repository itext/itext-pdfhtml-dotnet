using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css {
    public class FontPropertyTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FontPropertyTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FontPropertyTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FontShorthandContainingOnlyFontFamilyTest() {
            // TODO DEVSIX-3373: Fix cmp file after the bug is fixed. Currently, Helvetica font is picked up while the default one should be used
            ConvertToPdfAndCompare("fontShorthandContainingOnlyFontFamily", sourceFolder, destinationFolder);
        }
    }
}
