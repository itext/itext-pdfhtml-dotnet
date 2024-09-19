/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
using iText.StyledXmlParser.Css.Validate;
using iText.StyledXmlParser.Css.Validate.Impl;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class BorderTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/BorderTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/BorderTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Border01Test() {
            ConvertToPdfAndCompare("border01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 4)]
        public virtual void Border02Test() {
            ConvertToPdfAndCompare("border02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Border03Test() {
            ConvertToPdfAndCompare("border03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Border04Test() {
            ConvertToPdfAndCompare("border04", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 2)]
        public virtual void Border05Test() {
            ConvertToPdfAndCompare("border05", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Border06Test() {
            ConvertToPdfAndCompare("border06", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Border07Test() {
            ConvertToPdfAndCompare("border07", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Border08Test() {
            ConvertToPdfAndCompare("border08", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 2)]
        public virtual void Border09Test() {
            ConvertToPdfAndCompare("border09", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 2)]
        public virtual void Border10Test() {
            ConvertToPdfAndCompare("border10", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Border3DTest01() {
            ConvertToPdfAndCompare("border3DTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Border3DTest02() {
            ConvertToPdfAndCompare("border3DTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Border3DCmykTest() {
            try {
                CssDeclarationValidationMaster.SetValidator(new CssDeviceCmykAwareValidator());
                ConvertToPdfAndCompare("border3DCmykTest", sourceFolder, destinationFolder);
            }
            finally {
                CssDeclarationValidationMaster.SetValidator(new CssDefaultValidator());
            }
        }

        [NUnit.Framework.Test]
        public virtual void BorderTransparencyTest01() {
            ConvertToPdfAndCompare("borderTransparencyTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderTransparencyTest02() {
            ConvertToPdfAndCompare("borderTransparencyTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleOverlayingInTRTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleOverlayingInTR", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleSolidAndDoubleValueInTRTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleSolidAndDoubleValueInTR", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleSolidAndDottedValueInTRTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleSolidAndDottedValueInTR", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleSolidAndDashedValueInTRTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleSolidAndDashedValueInTR", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleInTrDifferentTypesTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleInTrDifferentTypes", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleTRInsideTheadTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleTRInsideThead", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleTRInsideTbodyTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleTRInsideTbody", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleTRInsideTfootTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleTRInsideTfoot", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleInsideTableElementsTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleInsideTableElements", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleInTRLengthUnitsTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleInTRLengthUnits", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleInTrColorValuesTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleInTrColorValues", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        //TODO DEVSIX-2857 update cmp file after fix
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.LAST_ROW_IS_NOT_COMPLETE)]
        public virtual void BorderStyleInTRwithTHTest() {
            ConvertToPdfAndCompare("borderStyleInTRwithTH", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleInTRSeparateBorderCollapseTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleInTRSeparateBorderCollapse", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TbodyBorderCollapseTest() {
            //TODO DEVSIX-4119 update cmp file after fix
            ConvertToPdfAndCompare("tbodyBorderCollapse", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TheadBorderCollapseTest() {
            //TODO DEVSIX-4119 update cmp file after fix
            ConvertToPdfAndCompare("theadBorderCollapse", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TfootBorderCollapseTest() {
            //TODO DEVSIX-4119 update cmp file after fix
            ConvertToPdfAndCompare("tfootBorderCollapse", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TableBorderStyleHiddenTest() {
            // TODO DEVSIX-5914 Currently border-style: hidden works like border-style: none
            ConvertToPdfAndCompare("tableBorderStyleHidden", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TableBorderStyleNoneTest() {
            // TODO DEVSIX-5914 This test could be used as a reference while testing border-style: hidden
            ConvertToPdfAndCompare("tableBorderStyleNone", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TableBorderStyleCollapsingPriorityTest() {
            // TODO DEVSIX-5915 border-style is not considered while collapsing: in browsers one can see,
            //  that top border of the cell below always wins the bottom border of the cell above
            ConvertToPdfAndCompare("tableBorderStyleCollapsingPriority", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TableWithCellsOfDifferentBorderColorsTest() {
            // TODO DEVSIX-5524 Left border is drawn underneath, but should overlap top and bottom
            ConvertToPdfAndCompare("tableWithCellsOfDifferentBorderColors", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CellDifferentBorderColorsTest() {
            // TODO DEVSIX-5524 Left border is drawn underneath, but should overlap top and bottom
            ConvertToPdfAndCompare("cellDifferentBorderColors", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BorderCollapseWithZeroWidthBorderTest() {
            ConvertToPdfAndCompare("borderCollapseWithZeroWidthBorder", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BigRowspanCollapseTest() {
            ConvertToPdfAndCompare("bigRowspanCollapse", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CellBorderCollapseTest() {
            ConvertToPdfAndCompare("cellBorderCollapse", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeaderBodyFooterTest() {
            ConvertToPdfAndCompare("headerBodyFooter", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BodyCellContentOverlapsBorder2Test() {
            // TODO DEVSIX-5524 Content should be placed over rather than under overlapped border
            ConvertToPdfAndCompare("bodyCellContentOverlapsBorder2", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BordersOfDifferentWidthsTest() {
            ConvertToPdfAndCompare("bordersOfDifferentWidths", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeaderBodyFooterBottomBorderCollapseTest() {
            ConvertToPdfAndCompare("headerBodyFooterBottomBorderCollapse", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BodyCellContentOverlapsBorderTest() {
            // TODO DEVSIX-5524 ?
            ConvertToPdfAndCompare("bodyCellContentOverlapsBorder", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BottomBorderCellAndTableCollapseTest() {
            // TODO DEVSIX-5524 Left border is drawn underneath, but should overlap top and bottom
            ConvertToPdfAndCompare("bottomBorderCellAndTableCollapse", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FooterContentOverlapsFooterBorderTest() {
            ConvertToPdfAndCompare("footerContentOverlapsFooterBorder", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CellBordersDifferentWidthsTest() {
            // TODO DEVSIX-5524 min-width is not respected
            ConvertToPdfAndCompare("cellBordersDifferentWidths", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CornerWidthHorizontalBorderWinsTest() {
            ConvertToPdfAndCompare("cornerWidthHorizontalBorderWins", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CornerWidthVerticalBorderWinsTest() {
            ConvertToPdfAndCompare("cornerWidthVerticalBorderWins", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ShorthandBorderBottomInThTdTest() {
            ConvertToPdfAndCompare("shorthandBorderBottomInThTd", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ShorthandBorderTopInThTdTest() {
            ConvertToPdfAndCompare("shorthandBorderTopInThTd", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ShorthandBorderRightInThTdTest() {
            ConvertToPdfAndCompare("shorthandBorderRightInThTd", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ShorthandBorderLeftInThTdTest() {
            ConvertToPdfAndCompare("shorthandBorderLeftInThTd", sourceFolder, destinationFolder);
        }
    }
}
