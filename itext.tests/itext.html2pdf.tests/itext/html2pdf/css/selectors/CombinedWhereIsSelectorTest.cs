using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css.Selectors {
    [NUnit.Framework.Category("IntegrationTest")]
    public class CombinedWhereIsSelectorTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/selectors/CombinedWhereIsSelectorTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/selectors/CombinedWhereIsSelectorTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsVsWhereTest() {
            ConvertToPdfAndCompare("isVsWhere", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void IsWhereNestedTest() {
            ConvertToPdfAndCompare("isWhereNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NestedSelectorsBasicTest() {
            ConvertToPdfAndCompare("nestedSelectorsBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void WhereIsNestedTest() {
            ConvertToPdfAndCompare("whereIsNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void WhereIsWithJustifyAndAlignContentTest() {
            ConvertToPdfAndCompare("whereIsWithJustifyAndAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CombinedDeepNestingTest() {
            ConvertToPdfAndCompare("combinedDeepNesting", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CombinedOverwriteChainTest() {
            ConvertToPdfAndCompare("combinedOverwriteChain", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CombinedTest() {
            ConvertToPdfAndCompare("combinedTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CombinedTest2() {
            ConvertToPdfAndCompare("combinedTest2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
