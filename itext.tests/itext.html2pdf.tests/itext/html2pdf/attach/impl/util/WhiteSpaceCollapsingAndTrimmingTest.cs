using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Attach.Impl.Util {
    public class WhiteSpaceCollapsingAndTrimmingTest : ExtendedHtmlConversionITextTest {
        private static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/attacher/impl/WhiteSpaceCollapsingAndTrimmingTest/";

        private static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/attacher/impl/WhiteSpaceCollapsingAndTrimmingTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void EmptyElementsTest01() {
            ConvertToPdfAndCompare("emptyElementsTest01", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void EmptyElementsTest02() {
            ConvertToPdfAndCompare("emptyElementsTest02", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void EmptyElementsTest03() {
            ConvertToPdfAndCompare("emptyElementsTest03", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void FloatingInlineBlockInsideLinkTest01() {
            ConvertToPdfAndCompare("floatingInlineBlockInsideLinkTest01", sourceFolder, destinationFolder);
        }
    }
}
