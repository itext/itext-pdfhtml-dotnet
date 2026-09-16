using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class VerticalTextMixedDirectionsTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/VerticalTextMixedDirectionsTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/VerticalTextMixedDirectionsTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphMixedTextTest() {
            ConvertToPdfAndCompare("paragraphMixedTextTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphMixedTextWithLineBreaksTest() {
            ConvertToPdfAndCompare("paragraphMixedTextWithLineBreaksTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphMixedTextNoEnoughHorizontalSpaceTest() {
            ConvertToPdfAndCompare("paragraphMixedTextNoEnoughHorizontalSpaceTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphMixedTextWithPageBreakTest() {
            ConvertToPdfAndCompare("paragraphMixedTextWithPageBreakTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalParagraphMixedTest() {
            ConvertToPdfAndCompare("verticalParagraphMixedTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalParagraphMixedWithLineBreaksTest() {
            ConvertToPdfAndCompare("verticalParagraphMixedWithLineBreaksTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalWritingAtTextLevelTest() {
            ConvertToPdfAndCompare("verticalWritingAtTextLevelTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalWritingAtTextLevelTwoLinesTest() {
            ConvertToPdfAndCompare("verticalWritingAtTextLevelTwoLinesTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalWritingAtTextLevelPageBreakTest() {
            ConvertToPdfAndCompare("verticalWritingAtTextLevelPageBreakTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalWritingAtTextLevelLongTextTest() {
            ConvertToPdfAndCompare("verticalWritingAtTextLevelLongTextTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalParagraphWithHorizontalTextTest() {
            ConvertToPdfAndCompare("verticalParagraphWithHorizontalTextTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
