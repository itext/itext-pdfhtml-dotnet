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
using iText.Html2pdf;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Grid {
    [NUnit.Framework.Category("IntegrationTest")]
    public class GridGapTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/grid/GridGapTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/grid/GridGapTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColsGapTest() {
            RunTest("template_cols_gap");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColsColGapTest() {
            RunTest("template_cols_col_gap");
        }

        [NUnit.Framework.Test]
        public virtual void Template_rowsRowGapTest() {
            RunTest("template_rows_row_gap");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowsGapTest() {
            RunTest("template_rows_gap");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowsColsGapTest() {
            RunTest("template_rows_cols_gap");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowsColsAutoRowsGapTest() {
            RunTest("template_rows_cols_auto_rows_gap");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowsColsAutoColsGapTest() {
            RunTest("template_rows_cols_auto_cols_gap");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowsColsBigCellGapTest1() {
            RunTest("template_rows_cols_big_cell_gap_1");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowsColsBigCellGapTest2() {
            RunTest("template_rows_cols_big_cell_gap_2");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowsColsVertCellGapTest1() {
            RunTest("template_rows_cols_vert_cell_gap_1");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowsColsVertCellGapTest2() {
            RunTest("template_rows_cols_vert_cell_gap_2");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowsColsHorzCellGapTest1() {
            RunTest("template_rows_cols_horz_cell_gap_1");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowsColsHorzCellGapTest2() {
            RunTest("template_rows_cols_horz_cell_gap_2");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowsColsFewBigCellGapTest1() {
            RunTest("template_rows_cols_few_big_cell_gap_1");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowsColsFewBigCellGapTest2() {
            RunTest("template_rows_cols_few_big_cell_gap_2");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 2)]
        public virtual void DiffUnitsTest() {
            RunTest("diff_units");
        }

        [NUnit.Framework.Test]
        public virtual void FloatGapValueTest() {
            RunTest("float_gap_value");
        }

        [NUnit.Framework.Test]
        public virtual void LargeColGapValueTest() {
            RunTest("large_col_gap_value");
        }

        [NUnit.Framework.Test]
        public virtual void LargeRowGapValueTest() {
            RunTest("large_row_gap_value");
        }

        [NUnit.Framework.Test]
        public virtual void LargeGapValueTest() {
            RunTest("large_gap_value");
        }

        [NUnit.Framework.Test]
        public virtual void MarginTest() {
            RunTest("margin");
        }

        [NUnit.Framework.Test]
        public virtual void ColGapLargeMarginItemTest() {
            ConvertToPdfAndCompare("colGapLargeMarginItem", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColGapLargeMarginsTest() {
            ConvertToPdfAndCompare("colGapLargeMargins", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PaddingTest() {
            RunTest("padding");
        }

        [NUnit.Framework.Test]
        public virtual void ColGapLargePaddingTest() {
            ConvertToPdfAndCompare("colGapLargePadding", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColGapLargePaddingItemTest() {
            ConvertToPdfAndCompare("colGapLargePaddingItem", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColGapLargePaddingOverflowItemTest() {
            ConvertToPdfAndCompare("colGapLargePaddingOverflowItem", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapLargeMarginItemTest() {
            ConvertToPdfAndCompare("rowGapLargeMarginItem", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapLargeMarginsTest() {
            ConvertToPdfAndCompare("rowGapLargeMargins", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapLargePaddingTest() {
            ConvertToPdfAndCompare("rowGapLargePadding", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapLargePaddingItemTest() {
            ConvertToPdfAndCompare("rowGapLargePaddingItem", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapLargePaddingOverflowItemTest() {
            ConvertToPdfAndCompare("rowGapLargePaddingOverflowItem", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void NegativeColGapValueTest() {
            RunTest("negative_col_gap_value");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void NegativeRowGapValueTest() {
            RunTest("negative_row_gap_value");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void NegativeGapValueTest() {
            RunTest("negative_gap_value");
        }

        [NUnit.Framework.Test]
        public virtual void SmallValuesGapTest() {
            RunTest("small_values_gap");
        }

        [NUnit.Framework.Test]
        public virtual void DifferentRowsColsGapTest() {
            RunTest("different_rows_cols_gap");
        }

        [NUnit.Framework.Test]
        public virtual void GridGapTest1() {
            RunTest("gridGapTest1");
        }

        [NUnit.Framework.Test]
        public virtual void GridGapTest2() {
            RunTest("gridGapTest2");
        }

        [NUnit.Framework.Test]
        public virtual void GridColumnGapTest() {
            RunTest("gridColumnGapTest");
        }

        [NUnit.Framework.Test]
        public virtual void GridRowGapTest() {
            RunTest("gridRowGapTest");
        }

        [NUnit.Framework.Test]
        public virtual void AutoColumnsLargeGapTest() {
            ConvertToPdfAndCompare("autoColumnsLargeGap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AutoRowsLargeGapTest() {
            ConvertToPdfAndCompare("autoRowsLargeGap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage("Cannot find pdfCalligraph module, which was implicitly required by one of the layout properties"
            , Count = 32)]
        public virtual void ColGapRtlDirectionTest() {
            ConvertToPdfAndCompare("colGapRtlDirection", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage("Cannot find pdfCalligraph module, which was implicitly required by one of the layout properties"
            , Count = 32)]
        public virtual void RowGapRtlDirectionTest() {
            ConvertToPdfAndCompare("rowGapRtlDirection", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColGapLongGridTest() {
            ConvertToPdfAndCompare("colGapLongGrid", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LargeColGapTest() {
            ConvertToPdfAndCompare("largeColGap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapLongGridTest() {
            ConvertToPdfAndCompare("rowGapLongGrid", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LargeRowGapTest() {
            ConvertToPdfAndCompare("largeRowGap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void LongGridTest() {
            ConvertToPdfAndCompare("longGrid", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void OrderOnItemsTest() {
            ConvertToPdfAndCompare("orderOnItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PercentageColGapTest() {
            ConvertToPdfAndCompare("percentageColGap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PercentageRowGapTest() {
            ConvertToPdfAndCompare("percentageRowGap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ZeroSpaceColGapTest() {
            ConvertToPdfAndCompare("zeroSpaceColGap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ZeroSpaceRowGapTest() {
            ConvertToPdfAndCompare("zeroSpaceRowGap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetBaseUri
                (SOURCE_FOLDER));
        }
    }
}
