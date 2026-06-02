using System;
using iText.Html2pdf;

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
        public virtual void WhereDeepNestedTest() {
            // TODO DEVSIX-9519 display:flex is not supported with ul/ol elements.
            ConvertToPdfAndCompare("whereDeepNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void WhereDeepNested2Test() {
            ConvertToPdfAndCompare("whereDeepNested2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void WhereFlexDirTest() {
            ConvertToPdfAndCompare("whereFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void WhereFlexDirReadDirTest() {
            ConvertToPdfAndCompare("whereFlexDirReadDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void WhereFlexWrapTest() {
            ConvertToPdfAndCompare("whereFlexWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void WhereFlexWrapAlignTest() {
            ConvertToPdfAndCompare("whereFlexWrapAlign", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void WhereGapJustifyContentTest() {
            ConvertToPdfAndCompare("whereGapJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void WhereLosesFromClassTest() {
            ConvertToPdfAndCompare("whereLosesFromClass", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void WhereNestedTest() {
            ConvertToPdfAndCompare("whereNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void WhereNestedListsTest() {
            // TODO DEVSIX-9519 display:flex is not supported with ul/ol elements.
            ConvertToPdfAndCompare("whereNestedLists", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void WhereNestedListsFlexTest() {
            // TODO DEVSIX-9519 display:flex is not supported with ul/ol elements.
            ConvertToPdfAndCompare("whereNestedListsFlex", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void WhereNthChildTest() {
            // TODO DEVSIX-9519 display:flex is not supported with ul/ol elements.
            ConvertToPdfAndCompare("whereNthChild", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void WhereReadDirFlexTest() {
            ConvertToPdfAndCompare("whereReadDirFlex", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void WhereSelectorBasicTest() {
            ConvertToPdfAndCompare("whereSelectorBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void WhereSpecificOverrideTest() {
            ConvertToPdfAndCompare("whereSpecificOverride", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void WhereWithIdTest() {
            ConvertToPdfAndCompare("whereWithId", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
