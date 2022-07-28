/*
This file is part of the iText (R) project.
Copyright (c) 1998-2022 iText Group NV
Authors: iText Software.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using iText.Html2pdf;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Media.Page {
    [NUnit.Framework.Category("Integration test")]
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
