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
using System.Collections.Generic;
using System.IO;
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.IO.Util;
using iText.Kernel.Exceptions;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Element;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    public class TableTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/TableTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/TableTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableDocumentTest() {
            RunTest("hello_table");
        }

        [NUnit.Framework.Test]
        public virtual void CheckBasicTableFeatures() {
            RunTest("checkBasicTableFeatures");
        }

        [NUnit.Framework.Test]
        public virtual void Check_th_align() {
            //TODO update after DEVSIX-2908
            RunTest("th_align");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableFixed1DocumentTest() {
            RunTest("hello_table_fixed1");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableFixed2DocumentTest() {
            RunTest("hello_table_fixed2");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableFixed3DocumentTest() {
            RunTest("hello_table_fixed3");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableFixed4DocumentTest() {
            RunTest("hello_table_fixed4");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableFixed5DocumentTest() {
            RunTest("hello_table_fixed5");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableFixed6DocumentTest() {
            //TODO: DEVSIX-5967 Incorrect cell content layout for 'table-layout: fixed' tag.
            RunTest("hello_table_fixed6");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableFixed7DocumentTest() {
            RunTest("hello_table_fixed7");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableFixed8DocumentTest() {
            RunTest("hello_table_fixed8");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableFixedLackOfTableWidthTest01() {
            RunTest("helloTableFixedLackOfTableWidthTest01", false, new PageSize(PageSize.A3).Rotate());
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableFixedLackOfTableWidthTest01A() {
            RunTest("helloTableFixedLackOfTableWidthTest01A", false, new PageSize(PageSize.A3).Rotate());
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableFixedLackOfTableWidthTest02() {
            RunTest("helloTableFixedLackOfTableWidthTest02", false, new PageSize(PageSize.A3).Rotate());
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableFixedLackOfTableWidthTest02A() {
            RunTest("helloTableFixedLackOfTableWidthTest02A", false, new PageSize(PageSize.A3).Rotate());
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.SUM_OF_TABLE_COLUMNS_IS_GREATER_THAN_100, Count = 3)]
        public virtual void HelloTableFixedLackOfTableWidthTest03() {
            RunTest("helloTableFixedLackOfTableWidthTest03", false, new PageSize(PageSize.A3).Rotate());
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.SUM_OF_TABLE_COLUMNS_IS_GREATER_THAN_100, Count = 3)]
        public virtual void HelloTableFixedLackOfTableWidthTest03A() {
            RunTest("helloTableFixedLackOfTableWidthTest03A", false, new PageSize(PageSize.A3).Rotate());
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableAutoDocumentTest() {
            RunTest("hello_table_auto");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableAuto2DocumentTest() {
            RunTest("hello_table_auto2");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableAuto3DocumentTest() {
            RunTest("hello_table_auto3");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableAuto4DocumentTest() {
            RunTest("hello_table_auto4");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableAuto5DocumentTest() {
            //TODO: DEVSIX-5969 Incorrect text wrapping for 'table-layout: auto' tag.
            RunTest("hello_table_auto5");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableAuto6DocumentTest() {
            RunTest("hello_table_auto6");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableAuto7DocumentTest() {
            RunTest("hello_table_auto7");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableAuto8DocumentTest() {
            RunTest("hello_table_auto8");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableAuto9DocumentTest() {
            RunTest("hello_table_auto9");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableAuto10DocumentTest() {
            RunTest("hello_table_auto10");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableAuto11DocumentTest() {
            RunTest("hello_table_auto11");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableAuto12DocumentTest() {
            RunTest("hello_table_auto12");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1370")]
        public virtual void HelloTableAuto13DocumentTest() {
            RunTest("hello_table_auto13");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableAuto14DocumentTest() {
            RunTest("hello_table_auto14");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableAuto15DocumentTest() {
            RunTest("hello_table_auto15");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableAuto16DocumentTest() {
            RunTest("hello_table_auto16");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableAuto17DocumentTest() {
            RunTest("hello_table_auto17");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableHeaderFooterDocumentTest() {
            RunTest("hello_table_header_footer");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.LAST_ROW_IS_NOT_COMPLETE)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.RECTANGLE_HAS_NEGATIVE_SIZE)]
        public virtual void CheckHeaderFooterTaggedTables() {
            //TODO update after DEVSIX-2395 and DEVSIX-2399
            RunTest("checkHeaderFooterTaggedTables");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.RECTANGLE_HAS_NEGATIVE_SIZE)]
        public virtual void CheckFloatInTdTagged() {
            //TODO update after DEVSIX-2395 and DEVSIX-2399
            RunTest("checkFloatInTdTagged");
        }

        [NUnit.Framework.Test]
        public virtual void CheckDisplayInTableTagged() {
            //TODO update after DEVSIX-2399
            RunTest("checkDisplayInTableTagged");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH, Count = 
            3)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Count = 2)]
        public virtual void CheckLargeImagesInTable() {
            //TODO update after DEVSIX-2382
            RunTest("checkLargeImagesInTable");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableColspanDocumentTest() {
            RunTest("hello_table_colspan");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableRowspanDocumentTest() {
            RunTest("hello_table_rowspan");
        }

        [NUnit.Framework.Test]
        public virtual void HelloTableColspanRowspanDocumentTest() {
            RunTest("hello_table_colspan_rowspan");
        }

        [NUnit.Framework.Test]
        public virtual void TableCssPropsTest01() {
            RunTest("tableCssPropsTest01");
        }

        [NUnit.Framework.Test]
        public virtual void TableCssPropsTest02() {
            RunTest("tableCssPropsTest02");
        }

        [NUnit.Framework.Test]
        public virtual void TableCssPropsTest03() {
            RunTest("tableCssPropsTest03");
        }

        [NUnit.Framework.Test]
        public virtual void DefaultTableTest() {
            RunTest("defaultTable");
        }

        [NUnit.Framework.Test]
        public virtual void TextInTableAndRowTest() {
            RunTest("textInTableAndRow");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.NOT_SUPPORTED_TH_SCOPE_TYPE, Count = 2)]
        public virtual void ThTagTest() {
            RunTest("thTag", true);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.NOT_SUPPORTED_TH_SCOPE_TYPE, Count = 2)]
        public virtual void TheadTagTest() {
            RunTest("theadTagTest", true);
        }

        [NUnit.Framework.Test]
        public virtual void TfootTagTest() {
            RunTest("tfootTagTest", true);
        }

        [NUnit.Framework.Test]
        public virtual void BrInTdTest() {
            RunTest("brInTd");
        }

        [NUnit.Framework.Test]
        public virtual void TableBorderAttributeTest01() {
            RunTest("tableBorderAttributeTest01");
        }

        [NUnit.Framework.Test]
        public virtual void TableBorderAttributeTest02() {
            RunTest("tableBorderAttributeTest02");
        }

        [NUnit.Framework.Test]
        public virtual void TableBorderAttributeTest03() {
            RunTest("tableBorderAttributeTest03");
        }

        [NUnit.Framework.Test]
        public virtual void TableBorderAttributeTest04() {
            RunTest("tableBorderAttributeTest04");
        }

        [NUnit.Framework.Test]
        public virtual void TableBorderAttributeTest05() {
            RunTest("tableBorderAttributeTest05");
        }

        [NUnit.Framework.Test]
        public virtual void TableBorderAttributeTest06() {
            RunTest("tableBorderAttributeTest06");
        }

        [NUnit.Framework.Test]
        public virtual void TableCellHeightsExpansionTest01() {
            // TODO this test currently does not work like in browsers. Cell heights are treated in a very special way in browsers,
            // but they are considered when deciding whether to expand the table.
            // Due to the mechanism layout currently works, we do not pass heights from html to layout for cells because otherwise
            // the content would be clipped if it does not fit, whereas the cell height should be expanted in html in this case.
            // This is the reason why we do not know on layout level if a height was set to an html cell.
            // There is a possibility to work around this problem by extending from TableRenderer for case of thml tables.
            // but this problem seems really not that important and a very narrow use case for now.
            // For related ticket, see DEVSIX-1072
            RunTest("tableCellHeightsExpansion01");
        }

        [NUnit.Framework.Test]
        public virtual void TableCellHeightsExpansionTest02() {
            RunTest("tableCellHeightsExpansion02");
        }

        [NUnit.Framework.Test]
        public virtual void TableCellHeightsExpansionTest03() {
            // Cells max-height property should not affect layout, just like in browsers.
            RunTest("tableCellHeightsExpansion03");
        }

        [NUnit.Framework.Test]
        public virtual void TableMaxHeightTest01() {
            RunTest("tableMaxHeight01");
        }

        [NUnit.Framework.Test]
        public virtual void TableMaxHeightTest02() {
            RunTest("tableMaxHeight02");
        }

        [NUnit.Framework.Test]
        public virtual void MultipleRowsInHeade01() {
            RunTest("multipleRowsInHeader01");
        }

        [NUnit.Framework.Test]
        public virtual void TableCollapseColCellBoxSizingWidthDifference() {
            RunTest("table_collapse_col_cell_box_sizing_width_difference");
        }

        [NUnit.Framework.Test]
        public virtual void ColspanInHeaderFooterTest() {
            RunTest("table_header_footer_colspan");
        }

        [NUnit.Framework.Test]
        public virtual void SeparateBorder01() {
            RunTest("separateBorder01");
        }

        [NUnit.Framework.Test]
        public virtual void ThScopeTaggedTest() {
            RunTest("thTagScopeTagged", true);
        }

        [NUnit.Framework.Test]
        public virtual void ThScopeTaggedDifferentTablesTest() {
            RunConvertToElements("thTagScopeTaggedDifferentTables", true);
        }

        [NUnit.Framework.Test]
        public virtual void ThScopeNotTaggedDifferentTablesTest() {
            RunConvertToElements("thTagScopeNotTaggedDifferentTables", false);
        }

        [NUnit.Framework.Test]
        public virtual void PlainTextTest() {
            // TODO DEVSIX-2092
            RunConvertToElements("plainTextTest", false);
        }

        [NUnit.Framework.Test]
        public virtual void SeparatedTablesWithDifferentCaptionsTest01() {
            RunTest("separatedTableWithDifferentCaptionsTest01", false);
        }

        [NUnit.Framework.Test]
        public virtual void CollapsedTablesWithDifferentCaptionsTest01() {
            RunTest("collapsedTablesWithDifferentCaptionsTest01", false);
        }

        [NUnit.Framework.Test]
        public virtual void CaptionWithTextAlignTest01() {
            RunTest("captionWithTextAlignTest01", false);
        }

        [NUnit.Framework.Test]
        public virtual void WideCaptionTest01() {
            RunTest("wideCaptionTest01", false);
        }

        [NUnit.Framework.Test]
        public virtual void WideCaptionTest02() {
            RunTest("wideCaptionTest02", false);
        }

        [NUnit.Framework.Test]
        public virtual void WideTableWithCaptionTest01() {
            RunTest("wideTableWithCaptionTest01", false);
        }

        [NUnit.Framework.Test]
        public virtual void WideTableWithCaptionTest02() {
            RunTest("wideTableWithCaptionTest02", false);
        }

        [NUnit.Framework.Test]
        public virtual void CaptionSideTest01() {
            RunTest("captionSideTest01", false);
        }

        [NUnit.Framework.Test]
        public virtual void CaptionSideSetAsAlignTest01() {
            RunTest("captionSideSetAsAlignTest01", false);
        }

        [NUnit.Framework.Test]
        public virtual void TableCellMinWidthRightAlignmentTest() {
            RunConvertToElements("tableCellMinWidthRightAlignmentTest", false);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.SUM_OF_TABLE_COLUMNS_IS_GREATER_THAN_100, Count = 4)]
        public virtual void TableWidthMoreThan100PercentTest() {
            //TODO: DEVSIX-2895 - inconsistency in table width between pdf and html
            RunTest("tableWidthMoreThan100Percent");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.PADDING_VALUE_IN_PERCENT_NOT_SUPPORTED, Count = 63)]
        public virtual void CheckResponsiveTableExample() {
            //https://codepen.io/heypablete/pen/qdIsm
            //TODO: update after DEVSIX-1101
            RunTest("checkResponsiveTableExample");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Count = 2)]
        [LogMessage(Html2PdfLogMessageConstant.INPUT_FIELD_DOES_NOT_FIT, Count = 2)]
        public virtual void TableWithChildrenBiggerThanCellTest() {
            //TODO: DEVSIX-3022 - Inputs bigger than enclosing cell force table to split
            RunTest("tableWithChildrenBiggerThanCell");
        }

        [NUnit.Framework.Test]
        public virtual void TableRowAndCellBackgroundColorConflictTest() {
            // TODO DEVSIX-4247
            RunTest("tableRowAndCellBackgroundColorConflictTest");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsedBorderWithWrongRowspanTableTest() {
            NUnit.Framework.Assert.That(() =>  {
                // TODO DEVSIX-5036
                RunTest("collapsedBorderWithWrongRowspanTable", false, new PageSize(PageSize.A5).Rotate());
            }
            , NUnit.Framework.Throws.InstanceOf<Exception>())
;
        }

        [NUnit.Framework.Test]
        public virtual void CellWithRowspanShouldBeConsideredWhileCalculatingColumnWidths() {
            // TODO DEVSIX-4806
            RunTest("cellWithRowspanShouldBeConsideredWhileCalculatingColumnWidths");
        }

        [NUnit.Framework.Test]
        public virtual void EmptyTrRowspanBorderCollapsingTest() {
            // TODO DEVSIX-5290 change cmp after the correction
            RunTest("emptyTrRowspanBorderCollapsing");
        }

        [NUnit.Framework.Test]
        public virtual void EmptyTdTest() {
            // TODO DEVSIX-6068 support empty td tag
            RunTest("emptyTd");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.UNEXPECTED_BEHAVIOUR_DURING_TABLE_ROW_COLLAPSING, Count = 2
            )]
        public virtual void EmptyTrTest() {
            RunTest("emptyTr");
        }

        [NUnit.Framework.Test]
        public virtual void TagsFlushingErrorWhenConvertedFromHtmlTest() {
            String file = sourceFolder + "tagsFlushingErrorWhenConvertedFromHtml.html";
            IList<IElement> elements = HtmlConverter.ConvertToElements(new FileStream(file, FileMode.Open, FileAccess.Read
                ), new ConverterProperties().SetCreateAcroForm(true));
            PdfDocument pdf = new PdfDocument(new PdfWriter(new FileStream(destinationFolder + "tagsFlushingErrorWhenConvertedFromHtml.pdf"
                , FileMode.Create)));
            pdf.SetTagged();
            Document document = new Document(pdf);
            document.SetMargins(10, 50, 10, 50);
            foreach (IElement element in elements) {
                document.Add((IBlockElement)element);
            }
            NUnit.Framework.Assert.That(() =>  {
                document.Close();
            }
            , NUnit.Framework.Throws.InstanceOf<PdfException>().With.Message.EqualTo("Tag structure flushing failed: it might be corrupted."))
;
        }

        [NUnit.Framework.Test]
        public virtual void ImageScaleTest() {
            RunTest("imageScale");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.LAST_ROW_IS_NOT_COMPLETE)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
        public virtual void TableSplitAndNotInitializedAreaTest() {
            // TODO This test should be considered during DEVSIX-1655. After the ticket is fixed, the cmp might get updated
            RunTest("tableSplitAndNotInitializedArea");
        }

        [NUnit.Framework.Test]
        public virtual void RepeatFooterHeaderInComplexTableTest() {
            RunTest("repeatFooterHeaderInComplexTable");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.NOT_SUPPORTED_TH_SCOPE_TYPE, Count = 2)]
        public virtual void ThTagConvertToElementTest() {
            RunConvertToElements("thTagConvertToElement", false);
        }

        [NUnit.Framework.Test]
        public virtual void ThTagConvertToPdfTest() {
            RunTest("thTagConvertToPdf");
        }

        private void RunTest(String testName) {
            RunTest(testName, false);
        }

        private void RunTest(String testName, bool tagged) {
            RunTest(testName, tagged, null);
        }

        private void RunTest(String testName, bool tagged, PageSize pageSize) {
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationFolder + testName + ".pdf"));
            if (null != pageSize) {
                pdfDocument.SetDefaultPageSize(pageSize);
            }
            if (tagged) {
                pdfDocument.SetTagged();
            }
            HtmlConverter.ConvertToPdf(new FileStream(sourceFolder + testName + ".html", FileMode.Open, FileAccess.Read
                ), pdfDocument, new ConverterProperties().SetBaseUri(sourceFolder));
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(sourceFolder + testName + ".html"
                ) + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + testName + ".pdf", sourceFolder
                 + "cmp_" + testName + ".pdf", destinationFolder, "diff_" + testName));
        }

        private void RunConvertToElements(String testName, bool tagged) {
            FileStream source = new FileStream(sourceFolder + testName + ".html", FileMode.Open, FileAccess.Read);
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationFolder + testName + ".pdf"));
            if (tagged) {
                pdfDocument.SetTagged();
            }
            Document layoutDocument = new Document(pdfDocument);
            ConverterProperties props = new ConverterProperties();
            foreach (IElement element in HtmlConverter.ConvertToElements(source, props)) {
                if (element is IBlockElement) {
                    layoutDocument.Add((IBlockElement)element);
                }
            }
            layoutDocument.Close();
            pdfDocument.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + testName + ".pdf", sourceFolder
                 + "cmp_" + testName + ".pdf", destinationFolder, "diff01_"));
        }
    }
}
