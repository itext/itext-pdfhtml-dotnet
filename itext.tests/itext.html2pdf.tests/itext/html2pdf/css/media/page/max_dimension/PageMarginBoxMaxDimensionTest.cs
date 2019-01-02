using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css.Media.Page.Max_dimension {
    public class PageMarginBoxMaxDimensionTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/media/page/max_dimension/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/media/page/max_dimension/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        // Top margin box tests
        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopOnlyMaxLeftTest() {
            ConvertToPdfAndCompare("topOnlyMaxLeft", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopOnlyMaxRightTest() {
            ConvertToPdfAndCompare("topOnlyMaxRight", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopOnlyMaxCenterTest() {
            ConvertToPdfAndCompare("topOnlyMaxCenter", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopMaxCenterAndRightTest() {
            ConvertToPdfAndCompare("topMaxCenterAndRight", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopMaxLeftAndCenterTest() {
            ConvertToPdfAndCompare("topMaxLeftAndCenter", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopMaxLeftAndRight() {
            ConvertToPdfAndCompare("topMaxLeftAndRight", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopMaxLeftAndMaxCenterAndMaxRight() {
            ConvertToPdfAndCompare("topMaxLeftAndMaxCenterAndMaxRight", sourceFolder, destinationFolder);
        }

        //Left margin box tests
        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftOnlyMaxTopTest() {
            ConvertToPdfAndCompare("leftOnlyMaxTop", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftOnlyMaxCenterTest() {
            ConvertToPdfAndCompare("leftOnlyMaxCenter", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftOnlyMaxBottomTest() {
            ConvertToPdfAndCompare("leftOnlyMaxBottom", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftTopAndMaxCenterTest() {
            ConvertToPdfAndCompare("leftTopAndMaxCenter", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftTopAndMaxBottomTest() {
            ConvertToPdfAndCompare("leftTopAndMaxBottom", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftMaxCenterAndBottomTest() {
            ConvertToPdfAndCompare("leftMaxCenterAndBottom", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftMaxTopAndMaxCenterAndBottomTest() {
            ConvertToPdfAndCompare("leftMaxTopAndMaxCenterAndBottom", sourceFolder, destinationFolder);
        }
    }
}
