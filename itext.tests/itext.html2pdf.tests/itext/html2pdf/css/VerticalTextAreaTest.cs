using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class VerticalTextAreaTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/VerticalTextAreaTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/VerticalTextAreaTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DivTest() {
            ConvertToPdfAndCompare("div", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DivsTest() {
            ConvertToPdfAndCompare("divs", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DivDisplayModesTest() {
            //TODO DEVSIX-10168 Inline mode positioning
            //TODO DEVSIX-10168 Grid mode sizing
            //TODO DEVSIX-10168 Table mode sizing
            ConvertToPdfAndCompare("divDisplayModes", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PTest() {
            ConvertToPdfAndCompare("p", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PsTest() {
            ConvertToPdfAndCompare("ps", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PsInDivTest() {
            ConvertToPdfAndCompare("psInDiv", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpansInDivTest() {
            //TODO DEVSIX-10168 positioning problems like with inline mode
            ConvertToPdfAndCompare("spansInDiv", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpansInDiv2Test() {
            //TODO minor borders should not be closed on bottom and top for line splited spans
            ConvertToPdfAndCompare("spansInDiv2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BodyTest() {
            // Height of body is ignored in horizontal and vertical modes.
            ConvertToPdfAndCompare("body", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexPsTest() {
            //TODO DEVSIX-10168 paragraphs are not wide enough
            ConvertToPdfAndCompare("flexPs", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDivsTest() {
            //TODO DEVSIX-10168 Sizing of divs and flex container
            ConvertToPdfAndCompare("flexDivs", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDivs2Test() {
            ConvertToPdfAndCompare("flexDivs2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
