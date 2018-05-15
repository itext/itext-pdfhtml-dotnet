using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css.Selector.Item {
    public class NonStandardNodesMatchingTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/selector/item/NonStandardNodesMatchingTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/selector/item/NonStandardNodesMatchingTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void PseudoElementMatchingTest01() {
            ConvertToPdfAndCompare("pseudoElementMatchingTest01", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void PseudoElementMatchingTest02() {
            ConvertToPdfAndCompare("pseudoElementMatchingTest02", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DocumentNodeMatchingTest01() {
            ConvertToPdfAndCompare("documentNodeMatchingTest01", sourceFolder, destinationFolder);
        }
    }
}
