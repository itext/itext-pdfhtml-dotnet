using System;
using iText.Html2pdf;
using iText.Test.Attributes;

namespace iText.Html2pdf.Attribute {
    public class StyleDirectionTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/attribute" + "/StyleDirectionTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/attribute/StyleDirectionTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        // TODO DEVSIX-5034 Incorrect direction of dot
        [LogMessage(iText.IO.LogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 8)]
        [LogMessage(iText.IO.LogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH, Count = 1)]
        public virtual void RtlDirectionOfTableElementsTest() {
            ConvertToPdfAndCompare("rtlDirectionOfTableElements", sourceFolder, destinationFolder);
        }
    }
}
