using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css.Grid {
    [NUnit.Framework.Category("IntegrationTest")]
    public class GridTemplateRowTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/grid/GridTemplateRowTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/grid/GridTemplateRowTest/";

        //TODO DEVSIX-3340 change cmp files when GRID LAYOUT is supported
        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowAutoTest() {
            RunTest("template-rows-auto");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowBordersTest() {
            RunTest("template-rows-borders");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowStartEndTest() {
            RunTest("template-rows-start-end");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowWidthUnitsTest() {
            RunTest("template-rows-different-width-units");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowFitContentTest() {
            RunTest("template-rows-fit-content");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowFitContentAutoTest() {
            RunTest("template-rows-fit-content-auto");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowFrTest() {
            RunTest("template-rows-fr");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowGridGapTest() {
            RunTest("template-rows-grid-gap");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowHeightWidthTest() {
            RunTest("template-rows-height-width");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowMarginTest() {
            RunTest("template-rows-margin");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowMinMaxTest() {
            RunTest("template-rows-minmax");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowMixedTest() {
            RunTest("template-rows-mixed");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowMultiPageTest() {
            RunTest("template-rows-multipage");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowNestedTest() {
            RunTest("template-rows-nested");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowPaddingTest() {
            RunTest("template-rows-padding");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowRepeatTest() {
            RunTest("template-rows-repeat");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowRepeatMinMaxTest() {
            RunTest("template-rows-repeat-minmax");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowRowGapTest() {
            RunTest("template-rows-row-gap");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowBasicTest() {
            RunTest("template-rows-without-other-props");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetBaseUri
                (SOURCE_FOLDER));
        }
    }
}
