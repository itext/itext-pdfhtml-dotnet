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
using System.IO;
using iText.Forms.Logs;
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class DisplayTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/DisplayTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/DisplayTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayTable01Test() {
            ConvertToPdfAndCompare("display_table01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayTable01ATest() {
            ConvertToPdfAndCompare("display_table01A", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.LAST_ROW_IS_NOT_COMPLETE)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        public virtual void DisplayTable02Test() {
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(DESTINATION_FOLDER + "display_table02.pdf"));
            pdfDoc.SetDefaultPageSize(new PageSize(1500, 842));
            HtmlConverter.ConvertToPdf(new FileStream(SOURCE_FOLDER + "display_table02.html", FileMode.Open, FileAccess.Read
                ), pdfDoc);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "display_table02.pdf"
                , SOURCE_FOLDER + "cmp_display_table02.pdf", DESTINATION_FOLDER, "diff20_"));
        }

        [NUnit.Framework.Test]
        public virtual void DisplayTable03Test() {
            ConvertToPdfAndCompare("display_table03", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayTable04Test() {
            // TODO DEVSIX-2445
            ConvertToPdfAndCompare("display_table04", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.LAST_ROW_IS_NOT_COMPLETE)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        public virtual void DisplayTable05Test() {
            ConvertToPdfAndCompare("display_table05", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        public virtual void DisplayTable05aTest() {
            ConvertToPdfAndCompare("display_table05a", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayTable06Test() {
            ConvertToPdfAndCompare("display_table06", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.LAST_ROW_IS_NOT_COMPLETE)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 1)]
        public virtual void DisplayTable07Test() {
            ConvertToPdfAndCompare("display_table07", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayTable08Test() {
            ConvertToPdfAndCompare("display_table08", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.SUM_OF_TABLE_COLUMNS_IS_GREATER_THAN_100)]
        public virtual void DisplayTable09Test() {
            ConvertToPdfAndCompare("display_table09", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayTable10Test() {
            ConvertToPdfAndCompare("display_table10", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        [NUnit.Framework.Test]
        public virtual void DisplayTable11Test() {
            ConvertToPdfAndCompare("display_table11", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        //TODO: update after DEVSIX-2445 fix
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 6)]
        [LogMessage(FormsLogMessageConstants.DUPLICATE_EXPORT_VALUE, Count = 2)]
        public virtual void DisplayBlockInsideParagraphTest() {
            ConvertToPdfAndCompare("displayBlockInsideParagraph", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayBlockInsideImageTest() {
            //TODO DEVSIX-6163 Image is converted in outPdf as inline element when display: block is set
            ConvertToPdfAndCompare("displayBlockInsideImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInline01Test() {
            ConvertToPdfAndCompare("display_inline01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock01Test() {
            ConvertToPdfAndCompare("display_inline-block01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.INLINE_BLOCK_ELEMENT_WILL_BE_CLIPPED)]
        public virtual void DisplayInlineBlock02Test() {
            ConvertToPdfAndCompare("display_inline-block02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock03Test() {
            ConvertToPdfAndCompare("display_inline-block03", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock04Test() {
            ConvertToPdfAndCompare("display_inline-block04", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock05Test() {
            ConvertToPdfAndCompare("display_inline-block05", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock06Test() {
            ConvertToPdfAndCompare("display_inline-block06", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock07Test() {
            ConvertToPdfAndCompare("display_inline-block07", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock08Test() {
            ConvertToPdfAndCompare("display_inline-block08", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock09Test() {
            ConvertToPdfAndCompare("display_inline-block09", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock10Test() {
            ConvertToPdfAndCompare("display_inline-block10", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock11Test() {
            ConvertToPdfAndCompare("display_inline-block11", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock12Test() {
            ConvertToPdfAndCompare("display_inline-block12", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock13Test() {
            ConvertToPdfAndCompare("display_inline-block13", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock14Test() {
            ConvertToPdfAndCompare("display_inline-block14", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock15Test() {
            ConvertToPdfAndCompare("display_inline-block15", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock16Test() {
            ConvertToPdfAndCompare("display_inline-block16", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock17Test() {
            ConvertToPdfAndCompare("display_inline-block17", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock18Test() {
            ConvertToPdfAndCompare("display_inline-block18", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlockAndWidthInDivTest() {
            ConvertToPdfAndCompare("displayInlineBlockAndWidthInDiv", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlockJustified01Test() {
            ConvertToPdfAndCompare("display_inline-block_justified01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlockJustified02Test() {
            ConvertToPdfAndCompare("display_inline-block_justified02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlockYLineTest01() {
            ConvertToPdfAndCompare("displayInlineBlockYLineTest01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlockYLineTest02() {
            ConvertToPdfAndCompare("displayInlineBlockYLineTest02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlockYLineTest03() {
            ConvertToPdfAndCompare("displayInlineBlockYLineTest03", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlockYLineTest04() {
            ConvertToPdfAndCompare("displayInlineBlockYLineTest04", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlockYLineTest05() {
            ConvertToPdfAndCompare("displayInlineBlockYLineTest05", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlockYLineTest06() {
            ConvertToPdfAndCompare("displayInlineBlockYLineTest06", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlockYLineTest07() {
            // TODO Fix cmp after DEVSIX-3429 is implemented. The checkbox should become aligned with the baseline of the text
            ConvertToPdfAndCompare("displayInlineBlockYLineTest07", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayNoneImportant01() {
            ConvertToPdfAndCompare("displayNoneImportant01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayNoneImportant02() {
            ConvertToPdfAndCompare("displayNoneImportant02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayNoneImportant03() {
            ConvertToPdfAndCompare("displayNoneImportant03", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayDivInlineWithStyle() {
            ConvertToPdfAndCompare("displayDivInlineWithStyle", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 7)]
        public virtual void InlineBlockInsideTableCellTest() {
            // IO setup
            PdfWriter writer = new PdfWriter(new FileInfo(DESTINATION_FOLDER + "inlineBlockInsideTableCellTest.pdf"));
            PdfDocument pdfDocument = new PdfDocument(writer);
            pdfDocument.SetDefaultPageSize(new PageSize(1000f, 1450f));
            // properties
            ConverterProperties props = new ConverterProperties();
            props.SetBaseUri(SOURCE_FOLDER);
            HtmlConverter.ConvertToPdf(new FileStream(SOURCE_FOLDER + "inlineBlockInsideTableCellTest.html", FileMode.Open
                , FileAccess.Read), pdfDocument, props);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(DESTINATION_FOLDER + "inlineBlockInsideTableCellTest.pdf"
                , SOURCE_FOLDER + "cmp_inlineBlockInsideTableCell.pdf", DESTINATION_FOLDER, "diffinlineBlockInsideTableCellTest_"
                ));
        }

        [NUnit.Framework.Test]
        public virtual void DisplayValuesInsideImageTest() {
            ConvertToPdfAndCompare("displayValuesInsideImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
