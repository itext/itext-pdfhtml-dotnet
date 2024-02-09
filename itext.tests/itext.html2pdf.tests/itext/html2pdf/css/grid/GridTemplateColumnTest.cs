using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css.Grid {
    [NUnit.Framework.Category("IntegrationTest")]
    public class GridTemplateColumnTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/grid/GridTemplateColumnTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/grid/GridTemplateColumnTest/";

        //TODO DEVSIX-3340 change cmp files when GRID LAYOUT is supported
        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnBordersTest() {
            RunTest("template-cols-borders");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnStartEndTest() {
            RunTest("template-cols-column-start-end");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnWidthUnitsTest() {
            RunTest("template-cols-different-width-units");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnFitContentTest() {
            RunTest("template-cols-fit-content");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnFitContentAutoTest() {
            RunTest("template-cols-fit-content-auto");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnFrTest() {
            RunTest("template-cols-fr");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnGridGapTest() {
            RunTest("template-cols-grid-gap");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnHeightWidthTest() {
            RunTest("template-cols-height-width");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnMarginTest() {
            RunTest("template-cols-margin");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnMinMaxTest() {
            RunTest("template-cols-minmax");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnMixedTest() {
            RunTest("template-cols-mixed");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnMultiPageTest() {
            RunTest("template-cols-enormous-size");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnNestedTest() {
            RunTest("template-cols-nested");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnPaddingTest() {
            RunTest("template-cols-padding");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnRepeatTest() {
            RunTest("template-cols-repeat");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnRepeatMinMaxTest() {
            RunTest("template-cols-repeat-minmax");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnColumnGapTest() {
            RunTest("template-cols-column-gap");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnBasicTest() {
            RunTest("template-cols-without-other-props");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetBaseUri
                (SOURCE_FOLDER));
        }
    }
}
