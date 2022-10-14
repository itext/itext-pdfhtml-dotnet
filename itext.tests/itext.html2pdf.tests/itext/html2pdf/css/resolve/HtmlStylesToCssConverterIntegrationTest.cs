using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css.Resolve {
    [NUnit.Framework.Category("Integration test")]
    public class HtmlStylesToCssConverterIntegrationTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css" + "/HtmlStylesToCssConverterIntegrationTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css" + "/HtmlStylesToCssConverterIntegrationTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ObjectTagWidthAndHeightTest() {
            ConvertToPdfAndCompare("objectTagWidthAndHeightTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
