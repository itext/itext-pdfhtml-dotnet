using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css.Media.Page.Min_dimension {
    public class PageMarginBoxMinDimensionTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/media/page/min_dimension/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/media/page/min_dimension/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        // Top margin box tests
        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopOnlyMinLeftTest() {
            ConvertToPdfAndCompare("topOnlyMinLeft", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopMinCenterAndRightTest() {
            ConvertToPdfAndCompare("topMinCenterAndRight", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("TODO DEVSIX-2389: Change test files after decision")]
        public virtual void TopMinLeftAndCenterTest() {
            ConvertToPdfAndCompare("topMinLeftAndCenter", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopMinLeftAndRight() {
            ConvertToPdfAndCompare("topMinLeftAndRight", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopMinLeftAndMinCenterAndMinRight() {
            ConvertToPdfAndCompare("topMinLeftAndMinCenterAndMinRight", sourceFolder, destinationFolder);
        }

        //Left margin box tests
        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftOnlyMinTopTest() {
            ConvertToPdfAndCompare("leftOnlyMinTop", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftTopAndMinCenterTest() {
            ConvertToPdfAndCompare("leftTopAndMinCenter", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftTopAndMinBottomTest() {
            ConvertToPdfAndCompare("leftTopAndMinBottom", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftMinCenterAndBottomTest() {
            ConvertToPdfAndCompare("leftMinCenterAndBottom", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftMinTopAndMinCenterAndBottomTest() {
            ConvertToPdfAndCompare("leftMinTopAndMinCenterAndBottom", sourceFolder, destinationFolder);
        }
    }
}
