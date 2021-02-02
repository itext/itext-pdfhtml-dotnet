/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
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
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class DisplayTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/DisplayTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/DisplayTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayTable01Test() {
            ConvertToPdfAndCompare("display_table01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayTable01ATest() {
            ConvertToPdfAndCompare("display_table01A", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.LAST_ROW_IS_NOT_COMPLETE)]
        [LogMessage(iText.IO.LogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        public virtual void DisplayTable02Test() {
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(destinationFolder + "display_table02.pdf"));
            pdfDoc.SetDefaultPageSize(new PageSize(1500, 842));
            HtmlConverter.ConvertToPdf(new FileStream(sourceFolder + "display_table02.html", FileMode.Open, FileAccess.Read
                ), pdfDoc);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "display_table02.pdf"
                , sourceFolder + "cmp_display_table02.pdf", destinationFolder, "diff20_"));
        }

        [NUnit.Framework.Test]
        public virtual void DisplayTable03Test() {
            ConvertToPdfAndCompare("display_table03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayTable04Test() {
            // TODO DEVSIX-2445
            ConvertToPdfAndCompare("display_table04", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.LAST_ROW_IS_NOT_COMPLETE)]
        [LogMessage(iText.IO.LogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        public virtual void DisplayTable05Test() {
            ConvertToPdfAndCompare("display_table05", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        public virtual void DisplayTable05aTest() {
            ConvertToPdfAndCompare("display_table05a", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayTable06Test() {
            ConvertToPdfAndCompare("display_table06", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.LAST_ROW_IS_NOT_COMPLETE)]
        [LogMessage(iText.IO.LogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        [LogMessage(iText.Html2pdf.LogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 1)]
        public virtual void DisplayTable07Test() {
            ConvertToPdfAndCompare("display_table07", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayTable08Test() {
            ConvertToPdfAndCompare("display_table08", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        [LogMessage(iText.IO.LogMessageConstant.SUM_OF_TABLE_COLUMNS_IS_GREATER_THAN_100)]
        public virtual void DisplayTable09Test() {
            ConvertToPdfAndCompare("display_table09", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayTable10Test() {
            ConvertToPdfAndCompare("display_table10", sourceFolder, destinationFolder);
        }

        [LogMessage(iText.IO.LogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        [NUnit.Framework.Test]
        public virtual void DisplayTable11Test() {
            ConvertToPdfAndCompare("display_table11", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        //TODO: update after DEVSIX-2445 fix
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 6)]
        public virtual void DisplayBlockInsideParagraphTest() {
            ConvertToPdfAndCompare("displayBlockInsideParagraph", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInline01Test() {
            ConvertToPdfAndCompare("display_inline01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock01Test() {
            ConvertToPdfAndCompare("display_inline-block01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.INLINE_BLOCK_ELEMENT_WILL_BE_CLIPPED)]
        public virtual void DisplayInlineBlock02Test() {
            ConvertToPdfAndCompare("display_inline-block02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock03Test() {
            ConvertToPdfAndCompare("display_inline-block03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock04Test() {
            ConvertToPdfAndCompare("display_inline-block04", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock05Test() {
            ConvertToPdfAndCompare("display_inline-block05", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock06Test() {
            ConvertToPdfAndCompare("display_inline-block06", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock07Test() {
            ConvertToPdfAndCompare("display_inline-block07", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock08Test() {
            ConvertToPdfAndCompare("display_inline-block08", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock09Test() {
            ConvertToPdfAndCompare("display_inline-block09", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock10Test() {
            ConvertToPdfAndCompare("display_inline-block10", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock11Test() {
            ConvertToPdfAndCompare("display_inline-block11", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock12Test() {
            ConvertToPdfAndCompare("display_inline-block12", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock13Test() {
            ConvertToPdfAndCompare("display_inline-block13", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock14Test() {
            ConvertToPdfAndCompare("display_inline-block14", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock15Test() {
            ConvertToPdfAndCompare("display_inline-block15", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock16Test() {
            ConvertToPdfAndCompare("display_inline-block16", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock17Test() {
            ConvertToPdfAndCompare("display_inline-block17", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlock18Test() {
            ConvertToPdfAndCompare("display_inline-block18", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlockJustified01Test() {
            ConvertToPdfAndCompare("display_inline-block_justified01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlockJustified02Test() {
            ConvertToPdfAndCompare("display_inline-block_justified02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlockYLineTest01() {
            ConvertToPdfAndCompare("displayInlineBlockYLineTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlockYLineTest02() {
            ConvertToPdfAndCompare("displayInlineBlockYLineTest02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlockYLineTest03() {
            ConvertToPdfAndCompare("displayInlineBlockYLineTest03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlockYLineTest04() {
            ConvertToPdfAndCompare("displayInlineBlockYLineTest04", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlockYLineTest05() {
            ConvertToPdfAndCompare("displayInlineBlockYLineTest05", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlockYLineTest06() {
            ConvertToPdfAndCompare("displayInlineBlockYLineTest06", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayInlineBlockYLineTest07() {
            // TODO Fix cmp after DEVSIX-3429 is implemented. The checkbox should become aligned with the baseline of the text
            ConvertToPdfAndCompare("displayInlineBlockYLineTest07", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayNoneImportant01() {
            ConvertToPdfAndCompare("displayNoneImportant01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayNoneImportant02() {
            ConvertToPdfAndCompare("displayNoneImportant02", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayNoneImportant03() {
            ConvertToPdfAndCompare("displayNoneImportant03", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayDivInlineWithStyle() {
            ConvertToPdfAndCompare("displayDivInlineWithStyle", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, Count = 7)]
        public virtual void InlineBlockInsideTableCellTest() {
            // IO setup
            PdfWriter writer = new PdfWriter(new FileInfo(destinationFolder + "inlineBlockInsideTableCellTest.pdf"));
            PdfDocument pdfDocument = new PdfDocument(writer);
            pdfDocument.SetDefaultPageSize(new PageSize(1000f, 1450f));
            // properties
            ConverterProperties props = new ConverterProperties();
            props.SetBaseUri(sourceFolder);
            HtmlConverter.ConvertToPdf(new FileStream(sourceFolder + "inlineBlockInsideTableCellTest.html", FileMode.Open
                , FileAccess.Read), pdfDocument, props);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "inlineBlockInsideTableCellTest.pdf"
                , sourceFolder + "cmp_inlineBlockInsideTableCell.pdf", destinationFolder, "diffinlineBlockInsideTableCellTest_"
                ));
        }

        [NUnit.Framework.Test]
        public virtual void DisplayValuesInsideImageTest() {
            ConvertToPdfAndCompare("displayValuesInsideImage", sourceFolder, destinationFolder);
        }
    }
}
