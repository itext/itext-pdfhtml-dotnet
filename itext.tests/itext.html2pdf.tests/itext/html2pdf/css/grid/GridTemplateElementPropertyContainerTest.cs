using System;
using iText.Html2pdf;
using iText.Layout.Logs;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Grid {
    [NUnit.Framework.Category("IntegrationTest")]
    public class GridTemplateElementPropertyContainerTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/grid/GridTemplateElementPropertyContainerTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/grid/GridTemplateElementPropertyContainerTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundItemsOnTopOfBackgroundGridTest() {
            RunTest("backgroundItemsOnTopOfBackgroundGrid");
        }

        [NUnit.Framework.Test]
        public virtual void PaddingAllSidesTest() {
            RunTest("paddingAll");
        }

        [NUnit.Framework.Test]
        public virtual void PaddingAllSidesEmptyDivTest() {
            RunTest("padding_all_with_empty_div");
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Count = 1)]
        public virtual void PaddingAllToBigForWidthTest() {
            RunTest("paddingAllToBigForWidth");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-TODO")]
        public virtual void PaddingOverflowX() {
            RunTest("padding_overflow_x");
        }

        [NUnit.Framework.Test]
        public virtual void BasicBorderTest() {
            RunTest("basic_border");
        }

        [NUnit.Framework.Test]
        public virtual void BorderBigTest() {
            RunTest("border_big");
        }

        [NUnit.Framework.Test]
        public virtual void BoderWithOverflowXTest() {
            RunTest("border_big_with_overflow_x");
        }

        [NUnit.Framework.Test]
        public virtual void MarginAllTest() {
            RunTest("margin_all");
        }

        [NUnit.Framework.Test]
        public virtual void MarginAllEmptyTest() {
            RunTest("margin_all_empty");
        }

        [NUnit.Framework.Test]
        public virtual void MarginAllWithEmptyDIV() {
            RunTest("margin_all_with_empty_div");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetBaseUri
                (SOURCE_FOLDER).SetCssGridEnabled(true));
        }
    }
}
