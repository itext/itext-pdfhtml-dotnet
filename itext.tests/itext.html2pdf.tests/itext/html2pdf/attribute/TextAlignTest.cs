using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Attribute {
    public class TextAlignTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/attribute/TextAlignTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/attribute/TextAlignTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TextAlignLeftTest() {
            ConvertToPdfAndCompare("textAlignLeft", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TextAlignRightTest() {
            ConvertToPdfAndCompare("textAlignRight", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TextAlignCenterTest() {
            ConvertToPdfAndCompare("textAlignCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TextAlignJustifyTest() {
            ConvertToPdfAndCompare("textAlignJustify", sourceFolder, destinationFolder);
        }
    }
}
