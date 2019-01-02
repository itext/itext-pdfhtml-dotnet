using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css.Media.Page {
    public class PageMarginBoxIntegrationTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/media/page/PageMarginBoxIntegrationTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/media/page/PageMarginBoxIntegrationTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeaderFooterTest() {
            ConvertToPdfAndCompare("headerFooter", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void PageWidthContentTest() {
            ConvertToPdfAndCompare("css-page-width-content", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void PageWidthElementTest() {
            ConvertToPdfAndCompare("css-page-width-element", sourceFolder, destinationFolder);
        }

        // Top margin box tests
        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftTest() {
            ConvertToPdfAndCompare("topOnlyLeft", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopOnlyRightTest() {
            ConvertToPdfAndCompare("topOnlyRight", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopOnlyCenterTest() {
            ConvertToPdfAndCompare("topOnlyCenter", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopCenterAndRight() {
            ConvertToPdfAndCompare("topCenterAndRight", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopLeftAndCenter() {
            ConvertToPdfAndCompare("topLeftAndCenter", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopLeftAndRight() {
            ConvertToPdfAndCompare("topLeftAndRight", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void TopLeftAndCenterAndRight() {
            ConvertToPdfAndCompare("topLeftAndCenterAndRight", sourceFolder, destinationFolder);
        }

        //Bottom margin box tests
        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BottomOnlyLeftTest() {
            ConvertToPdfAndCompare("bottomOnlyLeft", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BottomOnlyRightTest() {
            ConvertToPdfAndCompare("bottomOnlyRight", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BottomOnlyCenterTest() {
            ConvertToPdfAndCompare("bottomOnlyCenter", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BottomCenterAndRight() {
            ConvertToPdfAndCompare("bottomCenterAndRight", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BottomLeftAndCenter() {
            ConvertToPdfAndCompare("bottomLeftAndCenter", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BottomLeftAndRight() {
            ConvertToPdfAndCompare("bottomLeftAndRight", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BottomLeftAndCenterAndRight() {
            ConvertToPdfAndCompare("bottomLeftAndCenterAndRight", sourceFolder, destinationFolder);
        }

        //Left margin box tests
        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftOnlyTopTest() {
            ConvertToPdfAndCompare("leftOnlyTop", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftOnlyCenterTest() {
            ConvertToPdfAndCompare("leftOnlyCenter", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftOnlyBottomTest() {
            ConvertToPdfAndCompare("leftOnlyBottom", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftTopAndCenterTest() {
            ConvertToPdfAndCompare("leftTopAndCenter", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftTopAndBottomTest() {
            ConvertToPdfAndCompare("leftTopAndBottom", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftCenterAndBottomTest() {
            ConvertToPdfAndCompare("leftCenterAndBottom", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LeftTopAndCenterAndBottomTest() {
            ConvertToPdfAndCompare("leftTopAndCenterAndBottom", sourceFolder, destinationFolder);
        }

        //Right margin box tests
        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void RightOnlyTopTest() {
            ConvertToPdfAndCompare("rightOnlyTop", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void RightOnlyCenterTest() {
            ConvertToPdfAndCompare("rightOnlyCenter", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void RightOnlyBottomTest() {
            ConvertToPdfAndCompare("rightOnlyBottom", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void RightTopAndCenterTest() {
            ConvertToPdfAndCompare("rightTopAndCenter", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void RightTopAndBottomTest() {
            ConvertToPdfAndCompare("rightTopAndBottom", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void RightCenterAndBottomTest() {
            ConvertToPdfAndCompare("rightCenterAndBottom", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void RightTopAndCenterAndBottomTest() {
            ConvertToPdfAndCompare("rightTopAndCenterAndBottom", sourceFolder, destinationFolder);
        }

        //Edge-case test
        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LargeAutoLeftRegularCenterTopBottomTest() {
            ConvertToPdfAndCompare("largeAutoLeftRegularCenterTopBottomTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HugeAutoLeftTopBottomTest() {
            ConvertToPdfAndCompare("hugeAutoLeftTopBottomTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LargeAutoCenterRegularSidesTopBottomTest() {
            ConvertToPdfAndCompare("largeAutoCenterRegularSidesTopBottomTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LargeFixedLeftRegularCenterTopBottomTest() {
            ConvertToPdfAndCompare("largeFixedLeftRegularCenterTopBottomTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HugeFixedLeftTopBottomTest() {
            ConvertToPdfAndCompare("hugeFixedLeftTopBottomTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SmallFixedLeftTopBottomTest() {
            ConvertToPdfAndCompare("smallFixedLeftTopBottomTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LargeFixedCenterRegularSidesTopBottomTest() {
            ConvertToPdfAndCompare("largeFixedCenterRegularSidesTopBottomTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LargeAutoTopRegularCenterLeftRightTest() {
            ConvertToPdfAndCompare("largeAutoTopRegularCenterLeftRightTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HugeAutoTopLeftRightTest() {
            ConvertToPdfAndCompare("hugeAutoTopLeftRightTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LargeAutoCenterRegularSidesLeftRightTest() {
            ConvertToPdfAndCompare("largeAutoCenterRegularSidesLeftRightTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LargeFixedTopRegularCenterLeftRightTest() {
            ConvertToPdfAndCompare("largeFixedTopRegularCenterLeftRightTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HugeFixedTopLeftRightTest() {
            ConvertToPdfAndCompare("hugeFixedTopLeftRightTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SmallFixedTopLeftRightTest() {
            ConvertToPdfAndCompare("smallFixedTopLeftRightTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LargeFixedCenterRegularSidesLeftRightTest() {
            ConvertToPdfAndCompare("largeFixedCenterRegularSidesLeftRightTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LargeFixedAllLeftRightTest() {
            ConvertToPdfAndCompare("largeFixedAllLeftRightTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LargeFixedAllTopBottomTest() {
            ConvertToPdfAndCompare("largeFixedAllTopBottomTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LargeFixedSidesAutoMiddleTest() {
            ConvertToPdfAndCompare("largeFixedSidesAutoMiddleTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LargeFixedSidesFixedMiddleTest() {
            ConvertToPdfAndCompare("largeFixedSidesFixedMiddleTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void LargeFixedSidesNoMiddleTest() {
            ConvertToPdfAndCompare("largeFixedSidesNoMiddleTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void AllFixedWithMBPTest() {
            ConvertToPdfAndCompare("allFixedWithMBPTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void PercentageVerticalTest() {
            ConvertToPdfAndCompare("percentageVerticalTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CornerPrecisionTest() {
            ConvertToPdfAndCompare("cornerPrecisionTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void NegativeMarginsTest() {
            ConvertToPdfAndCompare("negativeMarginsTest", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HugeImageTest() {
            ConvertToPdfAndCompare("hugeImageTest", sourceFolder, destinationFolder);
        }
    }
}
