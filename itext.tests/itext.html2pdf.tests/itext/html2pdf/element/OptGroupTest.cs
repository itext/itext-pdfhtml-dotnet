using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Element {
    public class OptGroupTest : ExtendedHtmlConversionITextTest {
        private static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/OptGroupTest/";

        private static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/OptGroupTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptGroupBasicTest01() {
            ConvertToPdfAndCompare("optGroupBasicTest01", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptGroupBasicTest02() {
            ConvertToPdfAndCompare("optGroupBasicTest02", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptGroupEmptyTest01() {
            ConvertToPdfAndCompare("optGroupEmptyTest01", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptGroupNestedTest01() {
            ConvertToPdfAndCompare("optGroupNestedTest01", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptGroupNestedTest02() {
            ConvertToPdfAndCompare("optGroupNestedTest02", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptGroupNoSelectTest01() {
            ConvertToPdfAndCompare("optGroupNoSelectTest01", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptGroupStylesTest01() {
            ConvertToPdfAndCompare("optGroupStylesTest01", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptGroupHeightTest01() {
            ConvertToPdfAndCompare("optGroupHeightTest01", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptGroupWidthTest01() {
            // TODO DEVSIX-1896 Support "nowrap" value of "white-space" css property value
            ConvertToPdfAndCompare("optGroupWidthTest01", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptGroupOverflowTest01() {
            ConvertToPdfAndCompare("optGroupOverflowTest01", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptGroupOverflowTest02() {
            // TODO DEVSIX-1896 Support "nowrap" value of "white-space" css property value
            ConvertToPdfAndCompare("optGroupOverflowTest02", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptGroupPseudoTest01() {
            ConvertToPdfAndCompare("optGroupPseudoTest01", sourceFolder, destinationFolder);
        }
    }
}
