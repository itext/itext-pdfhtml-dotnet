/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
Authors: Apryse Software.

This program is offered under a commercial and under the AGPL license.
For commercial licensing, contact us at https://itextpdf.com/sales.  For AGPL licensing, see below.

AGPL licensing:
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using iText.Html2pdf;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Media.Page {
    [NUnit.Framework.Category("IntegrationTest")]
    public class PageMarginBoxIntegrationTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/media/page/PageMarginBoxIntegrationTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/media/page/PageMarginBoxIntegrationTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeaderFooterTest() {
            ConvertToPdfAndCompare("headerFooter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void PageWidthContentTest() {
            ConvertToPdfAndCompare("css-page-width-content", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void PageWidthElementTest() {
            ConvertToPdfAndCompare("css-page-width-element", sourceFolder, destinationFolder);
        }

        // Top margin box tests
        [NUnit.Framework.Test]
        public virtual void TopOnlyLeftTest() {
            ConvertToPdfAndCompare("topOnlyLeft", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyRightTest() {
            ConvertToPdfAndCompare("topOnlyRight", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopOnlyCenterTest() {
            ConvertToPdfAndCompare("topOnlyCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopCenterAndRight() {
            ConvertToPdfAndCompare("topCenterAndRight", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopLeftAndCenter() {
            ConvertToPdfAndCompare("topLeftAndCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopLeftAndRight() {
            ConvertToPdfAndCompare("topLeftAndRight", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TopLeftAndCenterAndRight() {
            ConvertToPdfAndCompare("topLeftAndCenterAndRight", sourceFolder, destinationFolder);
        }

        //Bottom margin box tests
        [NUnit.Framework.Test]
        public virtual void BottomOnlyLeftTest() {
            ConvertToPdfAndCompare("bottomOnlyLeft", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BottomOnlyRightTest() {
            ConvertToPdfAndCompare("bottomOnlyRight", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BottomOnlyCenterTest() {
            ConvertToPdfAndCompare("bottomOnlyCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BottomCenterAndRight() {
            ConvertToPdfAndCompare("bottomCenterAndRight", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BottomLeftAndCenter() {
            ConvertToPdfAndCompare("bottomLeftAndCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BottomLeftAndRight() {
            ConvertToPdfAndCompare("bottomLeftAndRight", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BottomLeftAndCenterAndRight() {
            ConvertToPdfAndCompare("bottomLeftAndCenterAndRight", sourceFolder, destinationFolder);
        }

        //Left margin box tests
        [NUnit.Framework.Test]
        public virtual void LeftOnlyTopTest() {
            ConvertToPdfAndCompare("leftOnlyTop", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftOnlyCenterTest() {
            ConvertToPdfAndCompare("leftOnlyCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftOnlyBottomTest() {
            ConvertToPdfAndCompare("leftOnlyBottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftTopAndCenterTest() {
            ConvertToPdfAndCompare("leftTopAndCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftTopAndBottomTest() {
            ConvertToPdfAndCompare("leftTopAndBottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftCenterAndBottomTest() {
            ConvertToPdfAndCompare("leftCenterAndBottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LeftTopAndCenterAndBottomTest() {
            ConvertToPdfAndCompare("leftTopAndCenterAndBottom", sourceFolder, destinationFolder);
        }

        //Right margin box tests
        [NUnit.Framework.Test]
        public virtual void RightOnlyTopTest() {
            ConvertToPdfAndCompare("rightOnlyTop", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RightOnlyCenterTest() {
            ConvertToPdfAndCompare("rightOnlyCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RightOnlyBottomTest() {
            ConvertToPdfAndCompare("rightOnlyBottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RightTopAndCenterTest() {
            ConvertToPdfAndCompare("rightTopAndCenter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RightTopAndBottomTest() {
            ConvertToPdfAndCompare("rightTopAndBottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RightCenterAndBottomTest() {
            ConvertToPdfAndCompare("rightCenterAndBottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RightTopAndCenterAndBottomTest() {
            ConvertToPdfAndCompare("rightTopAndCenterAndBottom", sourceFolder, destinationFolder);
        }

        //Edge-case test
        [NUnit.Framework.Test]
        public virtual void LargeAutoLeftRegularCenterTopBottomTest() {
            ConvertToPdfAndCompare("largeAutoLeftRegularCenterTopBottomTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HugeAutoLeftTopBottomTest() {
            ConvertToPdfAndCompare("hugeAutoLeftTopBottomTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LargeAutoCenterRegularSidesTopBottomTest() {
            ConvertToPdfAndCompare("largeAutoCenterRegularSidesTopBottomTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LargeFixedLeftRegularCenterTopBottomTest() {
            ConvertToPdfAndCompare("largeFixedLeftRegularCenterTopBottomTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HugeFixedLeftTopBottomTest() {
            ConvertToPdfAndCompare("hugeFixedLeftTopBottomTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void SmallFixedLeftTopBottomTest() {
            ConvertToPdfAndCompare("smallFixedLeftTopBottomTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LargeFixedCenterRegularSidesTopBottomTest() {
            ConvertToPdfAndCompare("largeFixedCenterRegularSidesTopBottomTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LargeAutoTopRegularCenterLeftRightTest() {
            ConvertToPdfAndCompare("largeAutoTopRegularCenterLeftRightTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HugeAutoTopLeftRightTest() {
            ConvertToPdfAndCompare("hugeAutoTopLeftRightTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LargeAutoCenterRegularSidesLeftRightTest() {
            ConvertToPdfAndCompare("largeAutoCenterRegularSidesLeftRightTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LargeFixedTopRegularCenterLeftRightTest() {
            ConvertToPdfAndCompare("largeFixedTopRegularCenterLeftRightTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HugeFixedTopLeftRightTest() {
            ConvertToPdfAndCompare("hugeFixedTopLeftRightTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void SmallFixedTopLeftRightTest() {
            ConvertToPdfAndCompare("smallFixedTopLeftRightTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LargeFixedCenterRegularSidesLeftRightTest() {
            ConvertToPdfAndCompare("largeFixedCenterRegularSidesLeftRightTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LargeFixedAllLeftRightTest() {
            ConvertToPdfAndCompare("largeFixedAllLeftRightTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LargeFixedAllTopBottomTest() {
            ConvertToPdfAndCompare("largeFixedAllTopBottomTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LargeFixedSidesAutoMiddleTest() {
            ConvertToPdfAndCompare("largeFixedSidesAutoMiddleTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LargeFixedSidesFixedMiddleTest() {
            ConvertToPdfAndCompare("largeFixedSidesFixedMiddleTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LargeFixedSidesNoMiddleTest() {
            ConvertToPdfAndCompare("largeFixedSidesNoMiddleTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AllFixedWithMBPTest() {
            ConvertToPdfAndCompare("allFixedWithMBPTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void PercentageVerticalTest() {
            ConvertToPdfAndCompare("percentageVerticalTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CornerPrecisionTest() {
            ConvertToPdfAndCompare("cornerPrecisionTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void NegativeMarginsTest() {
            ConvertToPdfAndCompare("negativeMarginsTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HugeImageTest() {
            ConvertToPdfAndCompare("hugeImageTest", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void PageMarginFont() {
            ConvertToPdfAndCompare("pageMarginFont", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT)]
        public virtual void TableInsideOfPageMarginNotFittingIntoDedicatedSpace() {
            NUnit.Framework.Assert.That(() =>  {
                ConvertToPdfAndCompare("tableInsideOfPageMarginNotFittingIntoDedicatedSpace", sourceFolder, destinationFolder
                    );
            }
            , NUnit.Framework.Throws.InstanceOf<NullReferenceException>())
;
        }

        [NUnit.Framework.Test]
        public virtual void PageSizeLetterMarginZeroTest() {
            ConvertToPdfAndCompare("pageSizeLetterMarginZero", sourceFolder, destinationFolder);
        }
    }
}
