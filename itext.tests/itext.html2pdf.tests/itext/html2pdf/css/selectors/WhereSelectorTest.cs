using System;
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Selectors {
    [NUnit.Framework.Category("IntegrationTest")]
    public class WhereSelectorTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/selectors/WhereSelectorTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/selectors/WhereSelectorTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void WhereDeepNestedTest() {
            ConvertToPdfAndCompare("whereDeepNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void WhereDeepNested2Test() {
            ConvertToPdfAndCompare("whereDeepNested2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void WhereFlexDirTest() {
            ConvertToPdfAndCompare("whereFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        [LogMessage("Cannot find pdfCalligraph module, which was implicitly required by one of the layout properties"
            , Count = 48)]
        public virtual void WhereFlexDirReadDirTest() {
            ConvertToPdfAndCompare("whereFlexDirReadDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void WhereFlexWrapTest() {
            ConvertToPdfAndCompare("whereFlexWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void WhereFlexWrapAlignTest() {
            ConvertToPdfAndCompare("whereFlexWrapAlign", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void WhereGapJustifyContentTest() {
            ConvertToPdfAndCompare("whereGapJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void WhereLosesFromClassTest() {
            ConvertToPdfAndCompare("whereLosesFromClass", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void WhereNestedTest() {
            ConvertToPdfAndCompare("whereNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void WhereNestedListsTest() {
            ConvertToPdfAndCompare("whereNestedLists", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void WhereNestedListsFlexTest() {
            ConvertToPdfAndCompare("whereNestedListsFlex", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR, Count = 2)]
        public virtual void WhereNthChildTest() {
            ConvertToPdfAndCompare("whereNthChild", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR, Count = 2)]
        [LogMessage("Cannot find pdfCalligraph module, which was implicitly required by one of the layout properties"
            , Count = 48)]
        public virtual void WhereReadDirFlexTest() {
            ConvertToPdfAndCompare("whereReadDirFlex", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void WhereSelectorBasicTest() {
            ConvertToPdfAndCompare("whereSelectorBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void WhereSpecificOverrideTest() {
            ConvertToPdfAndCompare("whereSpecificOverride", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR, Count = 3)]
        public virtual void WhereWithIdTest() {
            ConvertToPdfAndCompare("whereWithId", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
