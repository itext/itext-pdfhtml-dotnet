using System;
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Grid {
    [NUnit.Framework.Category("IntegrationTest")]
    public class GridAreaTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/grid/GridAreaTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/grid/GridAreaTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BasicGridArea1Test() {
            RunTest("basicGridArea1");
        }

        [NUnit.Framework.Test]
        public virtual void BasicGridArea2Test() {
            RunTest("basicGridArea2");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateAreasBasicTest() {
            RunTest("templateAreasBasic");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateAreasInvalidNameTest() {
            RunTest("templateAreasInvalidName");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateAreasWithDotsTest() {
            RunTest("templateAreasWithDots");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.GRID_TEMPLATE_AREAS_IS_INVALID)]
        public virtual void InvalidTemplateAreasTest() {
            RunTest("invalidTemplateAreas");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetBaseUri
                (SOURCE_FOLDER).SetCssGridEnabled(true));
        }
    }
}
