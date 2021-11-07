using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Attribute {
    public class HeightTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/attribute/HeightTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/attribute/HeightTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NegativeHeightValueInImageTest() {
            //TODO DEVSIX-2554 Html2Pdf: Image with negative height is missed in output pdf
            ConvertToPdfAndCompare("negativeHeight", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
