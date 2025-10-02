using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css.Flex {
    [NUnit.Framework.Category("IntegrationTest")]
    public class NestedFlexTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/flex/NestedFlexTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/flex/NestedFlexTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NestedFlexWithTable() {
            ConvertToPdfAndCompare("nestedFlexWithTable", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NestedFlexWithDiv() {
            ConvertToPdfAndCompare("nestedFlexWithDiv", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NestedFlexWithParagraph() {
            ConvertToPdfAndCompare("nestedFlexWithParagraph", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
