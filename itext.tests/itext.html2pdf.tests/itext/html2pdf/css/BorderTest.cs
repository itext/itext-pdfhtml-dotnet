/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
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
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.StyledXmlParser.Css.Validate;
using iText.StyledXmlParser.Css.Validate.Impl;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class BorderTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/BorderTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/BorderTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Border01Test() {
            ConvertToPdfAndCompare("border01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 4)]
        public virtual void Border02Test() {
            ConvertToPdfAndCompare("border02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Border03Test() {
            ConvertToPdfAndCompare("border03", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Border04Test() {
            ConvertToPdfAndCompare("border04", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 2)]
        public virtual void Border05Test() {
            ConvertToPdfAndCompare("border05", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Border06Test() {
            ConvertToPdfAndCompare("border06", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Border07Test() {
            ConvertToPdfAndCompare("border07", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Border08Test() {
            ConvertToPdfAndCompare("border08", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 2)]
        public virtual void Border09Test() {
            ConvertToPdfAndCompare("border09", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 2)]
        public virtual void Border10Test() {
            ConvertToPdfAndCompare("border10", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Border3DTest01() {
            ConvertToPdfAndCompare("border3DTest01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Border3DTest02() {
            ConvertToPdfAndCompare("border3DTest02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void Border3DCmykTest() {
            try {
                CssDeclarationValidationMaster.SetValidator(new CssDeviceCmykAwareValidator());
                ConvertToPdfAndCompare("border3DCmykTest", SOURCE_FOLDER, DESTINATION_FOLDER);
            }
            finally {
                CssDeclarationValidationMaster.SetValidator(new CssDefaultValidator());
            }
        }

        [NUnit.Framework.Test]
        public virtual void BorderTransparencyTest01() {
            ConvertToPdfAndCompare("borderTransparencyTest01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BorderTransparencyTest02() {
            ConvertToPdfAndCompare("borderTransparencyTest02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleOverlayingInTRTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleOverlayingInTR", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleSolidAndDoubleValueInTRTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleSolidAndDoubleValueInTR", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleSolidAndDottedValueInTRTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleSolidAndDottedValueInTR", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleSolidAndDashedValueInTRTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleSolidAndDashedValueInTR", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleInTrDifferentTypesTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleInTrDifferentTypes", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleTRInsideTheadTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleTRInsideThead", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleTRInsideTbodyTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleTRInsideTbody", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleTRInsideTfootTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleTRInsideTfoot", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleInsideTableElementsTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleInsideTableElements", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleInTRLengthUnitsTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleInTRLengthUnits", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleInTrColorValuesTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleInTrColorValues", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        //TODO DEVSIX-2857 update cmp file after fix
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.LAST_ROW_IS_NOT_COMPLETE)]
        public virtual void BorderStyleInTRwithTHTest() {
            ConvertToPdfAndCompare("borderStyleInTRwithTH", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BorderStyleInTRSeparateBorderCollapseTest() {
            //TODO DEVSIX-2857 update cmp file after fix
            ConvertToPdfAndCompare("borderStyleInTRSeparateBorderCollapse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TbodyBorderCollapseTest() {
            //TODO DEVSIX-4119 update cmp file after fix
            ConvertToPdfAndCompare("tbodyBorderCollapse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TheadBorderCollapseTest() {
            //TODO DEVSIX-4119 update cmp file after fix
            ConvertToPdfAndCompare("theadBorderCollapse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TfootBorderCollapseTest() {
            //TODO DEVSIX-4119 update cmp file after fix
            ConvertToPdfAndCompare("tfootBorderCollapse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TableBorderStyleHiddenTest() {
            // TODO DEVSIX-5914 Currently border-style: hidden works like border-style: none
            ConvertToPdfAndCompare("tableBorderStyleHidden", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TableBorderStyleNoneTest() {
            // TODO DEVSIX-5914 This test could be used as a reference while testing border-style: hidden
            ConvertToPdfAndCompare("tableBorderStyleNone", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TableBorderStyleCollapsingPriorityTest() {
            // TODO DEVSIX-5915 border-style is not considered while collapsing: in browsers one can see,
            //  that top border of the cell below always wins the bottom border of the cell above
            ConvertToPdfAndCompare("tableBorderStyleCollapsingPriority", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TableWithCellsOfDifferentBorderColorsTest() {
            ConvertToPdfAndCompare("tableWithCellsOfDifferentBorderColors", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CellDifferentBorderColorsTest() {
            ConvertToPdfAndCompare("cellDifferentBorderColors", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BorderCollapseWithZeroWidthBorderTest() {
            ConvertToPdfAndCompare("borderCollapseWithZeroWidthBorder", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BigRowspanCollapseTest() {
            ConvertToPdfAndCompare("bigRowspanCollapse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CellBorderCollapseTest() {
            ConvertToPdfAndCompare("cellBorderCollapse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HeaderBodyFooterTest() {
            ConvertToPdfAndCompare("headerBodyFooter", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BodyCellContentOverlapsBorder2Test() {
            // TODO DEVSIX-5962 Content should be placed over rather than under overlapped border
            ConvertToPdfAndCompare("bodyCellContentOverlapsBorder2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BordersOfDifferentWidthsTest() {
            ConvertToPdfAndCompare("bordersOfDifferentWidths", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HeaderBodyFooterBottomBorderCollapseTest() {
            ConvertToPdfAndCompare("headerBodyFooterBottomBorderCollapse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BodyCellContentOverlapsBorderTest() {
            // TODO DEVSIX-5962 Content should be placed over rather than under overlapped border, red should overlap yellow
            ConvertToPdfAndCompare("bodyCellContentOverlapsBorder", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BottomBorderCellAndTableCollapseTest() {
            ConvertToPdfAndCompare("bottomBorderCellAndTableCollapse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FooterContentOverlapsFooterBorderTest() {
            ConvertToPdfAndCompare("footerContentOverlapsFooterBorder", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CellBordersDifferentWidthsTest() {
            // TODO DEVSIX-5962 min-width is not respected
            ConvertToPdfAndCompare("cellBordersDifferentWidths", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CornerWidthHorizontalBorderWinsTest() {
            ConvertToPdfAndCompare("cornerWidthHorizontalBorderWins", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CornerWidthVerticalBorderWinsTest() {
            ConvertToPdfAndCompare("cornerWidthVerticalBorderWins", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ShorthandBorderBottomInThTdTest() {
            ConvertToPdfAndCompare("shorthandBorderBottomInThTd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ShorthandBorderTopInThTdTest() {
            ConvertToPdfAndCompare("shorthandBorderTopInThTd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ShorthandBorderRightInThTdTest() {
            ConvertToPdfAndCompare("shorthandBorderRightInThTd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ShorthandBorderLeftInThTdTest() {
            ConvertToPdfAndCompare("shorthandBorderLeftInThTd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void WidthLessThanWordInSentenceTest() {
            ConvertToPdfAndCompare("borderWidthLessThanWordInSentence", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BorderWidthLessThanWordInSentenceTaggedTest() {
            // TODO DEVSIX-8998 When border is less than content and document is tagged seems to break copy pasting in adobe
            String sourceHtml = SOURCE_FOLDER + "borderWidthLessThanWordInSentence.html";
            String destinationPdf = DESTINATION_FOLDER + "borderWidthLessThanWordInSentenceTagged.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_borderWidthLessThanWordInSentenceTagged.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationPdf));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(sourceHtml, FileMode.Open, FileAccess.Read), pdfDocument);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationPdf, cmpPdf, DESTINATION_FOLDER
                , "diff_"));
        }

        [NUnit.Framework.Test]
        public virtual void TextCrossesBorderTest() {
            ConvertToPdfAndCompare("textCrossesBorder", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextCrossesBorderTaggedTest() {
            // TODO DEVSIX-8998 When border is less than content and document is tagged seems to break copy pasting in adobe
            String sourceHtml = SOURCE_FOLDER + "borderWidthLessThanWordInSentence.html";
            String destinationPdf = DESTINATION_FOLDER + "textCrossesBorderTagged.pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_textCrossesBorderTagged.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationPdf));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(sourceHtml, FileMode.Open, FileAccess.Read), pdfDocument);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationPdf, cmpPdf, DESTINATION_FOLDER
                , "diff_"));
        }
    }
}
