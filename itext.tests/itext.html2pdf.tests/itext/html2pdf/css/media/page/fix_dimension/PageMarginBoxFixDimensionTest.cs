using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css.Media.Page.Fix_dimension {
    public class PageMarginBoxFixDimensionTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/media/page/fix_dimension/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/media/page/fix_dimension/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        // Top margin box tests
        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixPxTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixPx", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixPtTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixPt", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixPercentTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixPercent", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixInTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixIn", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixCmTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixCm", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixMmTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixMm", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixPcTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixPc", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixEmTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixEm", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftFixExTest() {
            ConvertToPdfAndCompare("topOnlyLeftFixEx", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopOnlyRightFixPercentTest() {
            ConvertToPdfAndCompare("topOnlyRightFixPercent", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopOnlyCenterFixPercentTest() {
            ConvertToPdfAndCompare("topOnlyCenterFixPercent", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopFixCenterAndRightTest() {
            ConvertToPdfAndCompare("topFixCenterAndRight", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopFixLeftAndFixCenterTest() {
            ConvertToPdfAndCompare("topFixLeftAndFixCenter", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopFixLeftAndRight() {
            ConvertToPdfAndCompare("topFixLeftAndRight", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopFixLeftAndFixCenterAndFixRight() {
            ConvertToPdfAndCompare("topFixLeftAndFixCenterAndFixRight", sourceFolder, destinationFolder);
        }

        //Left margin box tests
        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftOnlyFixTopTest() {
            ConvertToPdfAndCompare("leftOnlyFixTop", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftOnlyFixCenterTest() {
            ConvertToPdfAndCompare("leftOnlyFixCenter", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftOnlyFixBottomTest() {
            ConvertToPdfAndCompare("leftOnlyFixBottom", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftTopAndFixCenterTest() {
            ConvertToPdfAndCompare("leftTopAndFixCenter", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftTopAndFixBottomTest() {
            ConvertToPdfAndCompare("leftTopAndFixBottom", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftFixCenterAndBottomTest() {
            ConvertToPdfAndCompare("leftFixCenterAndBottom", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftFixTopAndFixCenterAndBottomTest() {
            ConvertToPdfAndCompare("leftFixTopAndFixCenterAndBottom", sourceFolder, destinationFolder);
        }
    }
}
