using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css {
    public class OrphansTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/OrphansTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/OrphansTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Orphans5LinesTest() {
            RunTest("orphans5LinesTest");
        }

        [NUnit.Framework.Test]
        public virtual void Orphans5LinesOverflowTest() {
            RunTest("orphans5LinesOverflowTest");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, sourceFolder, destinationFolder);
        }
    }
}
