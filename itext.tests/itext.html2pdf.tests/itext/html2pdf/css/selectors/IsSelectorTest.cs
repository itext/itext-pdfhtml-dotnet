using System;
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Selectors {
    [NUnit.Framework.Category("IntegrationTest")]
    public class IsSelectorTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/selectors/IsSelectorTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/selectors/IsSelectorTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR, Count = 2)]
        public virtual void IsClassOverridesListTest() {
            ConvertToPdfAndCompare("isClassOverridesList", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void IsDeepNestedTest() {
            ConvertToPdfAndCompare("isDeepNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void IsDeepNested2Test() {
            ConvertToPdfAndCompare("isDeepNested2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void IsFirstVsClassTest() {
            ConvertToPdfAndCompare("isFirstVsClass", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void IsFlexDirTest() {
            ConvertToPdfAndCompare("isFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        [LogMessage("Cannot find pdfCalligraph module, which was implicitly required by one of the layout properties"
            , Count = 48)]
        public virtual void IsFlexDirReadDirTest() {
            ConvertToPdfAndCompare("isFlexDirReadDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void IsFlexWrapTest() {
            ConvertToPdfAndCompare("isFlexWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void IsFlexWrapAlignTest() {
            ConvertToPdfAndCompare("isFlexWrapAlign", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void IsGapJustifyContentTest() {
            ConvertToPdfAndCompare("isGapJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void IsGroupedSelectorsTest() {
            ConvertToPdfAndCompare("isGroupedSelectors", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void IsNestedTest() {
            ConvertToPdfAndCompare("isNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void IsNestedListsTest() {
            ConvertToPdfAndCompare("isNestedLists", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void IsNestedListsFlexTest() {
            ConvertToPdfAndCompare("isNestedListsFlex", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR, Count = 2)]
        public virtual void IsNthChildTest() {
            ConvertToPdfAndCompare("isNthChild", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR, Count = 2)]
        [LogMessage("Cannot find pdfCalligraph module, which was implicitly required by one of the layout properties"
            , Count = 48)]
        public virtual void IsReadDirFlexTest() {
            ConvertToPdfAndCompare("isReadDirFlex", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR, Count = 2)]
        public virtual void IsSelectoreBasicTest() {
            ConvertToPdfAndCompare("isSelectoreBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void IsSpecificOverrideTest() {
            ConvertToPdfAndCompare("isSpecificOverride", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void IsWinsFromClassTest() {
            ConvertToPdfAndCompare("isWinsFromClass", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR, Count = 3)]
        public virtual void IsWithIdTest() {
            ConvertToPdfAndCompare("isWithId", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ERROR_PARSING_CSS_SELECTOR)]
        public virtual void IsWithJustifyAndAlignContentTest() {
            ConvertToPdfAndCompare("isWithJustifyAndAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
