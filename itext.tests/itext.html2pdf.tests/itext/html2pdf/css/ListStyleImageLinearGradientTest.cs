using System;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Css {
    public class ListStyleImageLinearGradientTest : ExtendedITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/ListStyleImageLinearGradientTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/ListStyleImageLinearGradientTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientInListStyleTest() {
            RunTest("linearGradientInListStyle");
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientTypeTest() {
            RunTest("linearGradientType");
        }

        [NUnit.Framework.Test]
        public virtual void RepeatingLinearGradientTypeTest() {
            RunTest("repeatingLinearGradientType");
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientWithEmRemValuesTest() {
            RunTest("linearGradientWithEmRemValues");
        }

        [NUnit.Framework.Test]
        public virtual void DifferentLinearGradientsInElementsTest() {
            RunTest("differentLinearGradientsInElements");
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientInDifferentElementsTest() {
            RunTest("linearGradientInDifferentElements");
        }

        [NUnit.Framework.Test]
        public virtual void LinearGradientDifferentFontSizeTest() {
            RunTest("linearGradientDifferentFontSize");
        }

        private void RunTest(String testName) {
            HtmlConverter.ConvertToPdf(new FileInfo(SOURCE_FOLDER + testName + ".html"), new FileInfo(DESTINATION_FOLDER
                 + testName + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + testName + ".pdf", SOURCE_FOLDER
                 + "cmp_" + testName + ".pdf", DESTINATION_FOLDER, "diff_" + testName));
        }
    }
}
