using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css {
    public class CssEmptySelectorTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/CssEmptySelectorTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/CssEmptySelectorTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CssEmptyNotEmptyNestedNodesTest() {
            ConvertToPdfAndCompare("cssEmptyNotEmptyNestedNodesTest", sourceFolder, destinationFolder);
        }
    }
}
