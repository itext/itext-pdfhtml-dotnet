using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class BrTagArtifactTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/BrTagArtifactTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/resources/itext/html2pdf/css/BrTagArtifactTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SimpleBrTagArtifactTest() {
            ConvertToPdfAndCompare("brTagArtifact", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void BrTagArtifactDoubleTagTest() {
            ConvertToPdfAndCompare("brTagArtifactDoubleTag", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void BrTagSimpleTextFieldTest() {
            ConvertToPdfAndCompare("brTagSimpleTextField", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void BrTagSimpleTableTest() {
            ConvertToPdfAndCompare("brTagSimpleTable", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void BrTagSimpleLabelTest() {
            ConvertToPdfAndCompare("brTagSimpleLabel", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }
    }
}
