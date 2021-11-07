using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Attribute {
    public class WidthTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/attribute/WidthTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/attribute/WidthTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NegativeWidthValueInImageTest() {
            //TODO DEVSIX-2554 Html2Pdf: Image with negative width is missed in output pdf
            ConvertToPdfAndCompare("negativeWidth", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
