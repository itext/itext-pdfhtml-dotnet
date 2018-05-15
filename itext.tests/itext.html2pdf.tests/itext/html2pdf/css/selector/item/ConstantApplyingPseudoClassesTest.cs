using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css.Selector.Item {
    public class ConstantApplyingPseudoClassesTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/selector/item/ConstantApplyingPseudoClassesTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/selector/item/ConstantApplyingPseudoClassesTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void AlwaysApplyPseudoClassesTest01() {
            ConvertToPdfAndCompare("alwaysApplyPseudoClassesTest01", sourceFolder, destinationFolder);
        }
    }
}
