using System;
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Selectors {
    [NUnit.Framework.Category("IntegrationTest")]
    public class NotSelectorTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/selectors/NotSelectorTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/selectors/NotSelectorTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsFirstVsClassTest() {
            ConvertToPdfAndCompare("notFirstVsClass", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NotNestedTest() {
            ConvertToPdfAndCompare("notNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NotSelectorBasicTest() {
            ConvertToPdfAndCompare("notSelectorBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR, Count = 3)]
        public virtual void NotInvalidTest() {
            ConvertToPdfAndCompare("notInvalid", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NotSpecificityGreaterTest() {
            ConvertToPdfAndCompare("notSpecificityGreater", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ComplexNotSpecificityGreaterTest() {
            ConvertToPdfAndCompare("complexNotSpecificityGreater", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NotSpecificityLowerTest() {
            ConvertToPdfAndCompare("notSpecificityLower", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
