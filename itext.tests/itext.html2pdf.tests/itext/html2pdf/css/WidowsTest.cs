using System;
using iText.Html2pdf;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class WidowsTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/WidowsTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/WidowsTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Widows5LinesTest() {
            RunTest("widows5LinesTest");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.WIDOWS_CONSTRAINT_VIOLATED)]
        public virtual void Widows5LinesViolationTest() {
            RunTest("widows5LinesViolationTest");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, sourceFolder, destinationFolder);
        }
    }
}
