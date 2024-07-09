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
using iText.Html2pdf.Logs;
using iText.Layout.Exceptions;
using iText.Layout.Logs;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Grid {
    [NUnit.Framework.Category("IntegrationTest")]
    public class GridTemplatesTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/grid/GridTemplatesTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/grid/GridTemplatesTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnOneDivTest() {
            RunTest("basicColumnOneDivTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnFewDivsTest() {
            RunTest("basicColumnFewDivsTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnFewDivsWithFrTest() {
            RunTest("basicColumnFewDivsWithFrTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnFewDivs2Test() {
            RunTest("basicColumnFewDivs2Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnMultiPageTest() {
            // TODO DEVSIX-8331
            RunTest("basicColumnMultiPageTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnStartEndTest() {
            RunTest("basicColumnStartEndTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnStartEnd2Test() {
            RunTest("basicColumnStartEnd2Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnStartEnd3Test() {
            // We need to add a "dry run" for cell balancing without layouting to determine final grid size
            RunTest("basicColumnStartEnd3Test");
        }

        //--------------- without grid-templates-columns and grid-templates-rows ---------------
        [NUnit.Framework.Test]
        public virtual void BasicOnlyGridDisplayTest() {
            RunTest("basicOnlyGridDisplayTest");
        }

        //--------------- grid-templates-rows ---------------
        [NUnit.Framework.Test]
        public virtual void BasicRowOneDivTest() {
            RunTest("basicRowOneDivTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicRowFewDivsTest() {
            RunTest("basicRowFewDivsTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicRowStartEndTest() {
            RunTest("basicRowStartEndTest");
        }

        //--------------- grid-templates-columns + grid-templates-rows ---------------
        [NUnit.Framework.Test]
        public virtual void BasicColumnRowFewDivs1Test() {
            RunTest("basicColumnRowFewDivs1Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowFewDivs2Test() {
            RunTest("basicColumnRowFewDivs2Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowFewDivs3Test() {
            RunTest("basicColumnRowFewDivs3Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowFewDivs4Test() {
            RunTest("basicColumnRowFewDivs4Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEndTest() {
            RunTest("basicColumnRowStartEndTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEnd2Test() {
            RunTest("basicColumnRowStartEnd2Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEnd3Test() {
            RunTest("basicColumnRowStartEnd3Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEnd4Test() {
            RunTest("basicColumnRowStartEnd4Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEnd5Test() {
            RunTest("basicColumnRowStartEnd5Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEnd6Test() {
            RunTest("basicColumnRowStartEnd6Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEnd7Test() {
            RunTest("basicColumnRowStartEnd7Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEnd8Test() {
            RunTest("basicColumnRowStartEnd8Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEnd9Test() {
            RunTest("basicColumnRowStartEnd9Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEnd10Test() {
            RunTest("basicColumnRowStartEnd10Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEnd11Test() {
            RunTest("basicColumnRowStartEnd11Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEnd12Test() {
            RunTest("basicColumnRowStartEnd12Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEnd13Test() {
            RunTest("basicColumnRowStartEnd13Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEnd14Test() {
            RunTest("basicColumnRowStartEnd14Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEnd15Test() {
            RunTest("basicColumnRowStartEnd15Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEnd16Test() {
            RunTest("basicColumnRowStartEnd16Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEnd17Test() {
            RunTest("basicColumnRowStartEnd17Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEnd18Test() {
            RunTest("basicColumnRowStartEnd18Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEnd19Test() {
            RunTest("basicColumnRowStartEnd19Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnRowStartEndWithInlineTextTest() {
            RunTest("basicColumnRowStartEndWithInlineTextTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicGridAfterParagraphTest() {
            RunTest("basicGridAfterParagraphTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicRowFlowTest() {
            RunTest("basicRowFlowTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicRowDenseFlowTest() {
            RunTest("basicRowDenseFlowTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnFlowTest() {
            RunTest("basicColumnFlowTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicColumnDenseFlowTest() {
            RunTest("basicColumnDenseFlowTest");
        }

        [NUnit.Framework.Test]
        public virtual void FixedTemplatesAndCellDoesNotHaveDirectNeighborTest() {
            RunTest("fixedTemplatesAndCellDoesNotHaveDirectNeighborTest");
        }

        [NUnit.Framework.Test]
        public virtual void GridInsideGridTest() {
            RunTest("gridInsideGridTest");
        }

        [NUnit.Framework.Test]
        public virtual void GridInsideGridOnPageBreakTest() {
            RunTest("gridInsideGridOnPageBreakTest");
        }

        [NUnit.Framework.Test]
        public virtual void ElementDoesntFitContentTest() {
            RunTest("elementDoesntFitContentTest");
        }

        [NUnit.Framework.Test]
        public virtual void ElementDoesntFitTest() {
            RunTest("elementDoesntFitTest");
        }

        [NUnit.Framework.Test]
        public virtual void ElementDoesntFitHorizontallyTest() {
            RunTest("elementDoesntFitHorizontallyTest");
        }

        [NUnit.Framework.Test]
        public virtual void ElementDoesntFitOverflowingToNextPageTest() {
            RunTest("elementDoesntFitOverflowingToNextPageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ElementDoesntFitContentOverflowingToNextPageTest() {
            RunTest("elementDoesntFitContentOverflowingToNextPageTest");
        }

        [NUnit.Framework.Test]
        public virtual void TextsWithOverflowTest() {
            RunTest("textsWithOverflowTest");
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, LogLevel = LogLevelConstants.WARN)]
        public virtual void ImageElementDoesntFitTest() {
            RunTest("imageElementDoesntFitTest");
        }

        [NUnit.Framework.Test]
        public virtual void ManyImageElementsTest() {
            RunTest("manyImageElementsTest");
        }

        [NUnit.Framework.Test]
        public virtual void ImageElementsOn2ndPageTest() {
            RunTest("imageElementsOn2ndPageTest");
        }

        [NUnit.Framework.Test]
        public virtual void GridWithBrTest() {
            RunTest("gridWithBrTest");
        }

        [NUnit.Framework.Test]
        public virtual void GridWithPageBreakTest() {
            RunTest("gridWithPageBreakTest");
        }

        [NUnit.Framework.Test]
        public virtual void GridWithTableTest() {
            RunTest("gridWithTableTest");
        }

        [NUnit.Framework.Test]
        public virtual void ColumnFlowOnSplitTest() {
            RunTest("columnFlowOnSplitTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicGridRemValuesTest() {
            RunTest("grid-layout-rem");
        }

        [NUnit.Framework.Test]
        public virtual void BasicGridEmValuesTest() {
            RunTest("grid-layout-em");
        }

        [NUnit.Framework.Test]
        public virtual void PercentageTemplateHeightTest() {
            RunTest("percentageTemplateHeightTest");
        }

        [NUnit.Framework.Test]
        public virtual void PercentageTemplateHeightWithFixedHeightTest() {
            RunTest("percentageTemplateHeightWithFixedHeightTest");
        }

        [NUnit.Framework.Test]
        public virtual void PercentageFitContentWithFrTest() {
            RunTest("percentageFitContentWithFrTest");
        }

        [NUnit.Framework.Test]
        public virtual void AutoFillRepeatWithGapsTest() {
            RunTest("autoFillRepeatWithGapsTest");
        }

        [NUnit.Framework.Test]
        public virtual void AutoFitWithSingleCellTest() {
            RunTest("autoFitWithSingleCellTest");
        }

        [NUnit.Framework.Test]
        public virtual void ColumnFlowAutoFillTest() {
            RunTest("columnFlowAutoFillTest");
        }

        [NUnit.Framework.Test]
        public virtual void FitContentAndFrTest() {
            RunTest("fitContentAndFrTest");
        }

        [NUnit.Framework.Test]
        public virtual void FixedFitContentTest() {
            RunTest("fixedFitContentTest");
        }

        [NUnit.Framework.Test]
        public virtual void FixedRepeatWithGapsTest() {
            RunTest("fixedRepeatWithGapsTest");
        }

        [NUnit.Framework.Test]
        public virtual void InlineAutoFillTest() {
            RunTest("inlineAutoFillTest");
        }

        [LogMessage(LayoutExceptionMessageConstant.GRID_AUTO_REPEAT_CANNOT_BE_COMBINED_WITH_INDEFINITE_SIZES)]
        [NUnit.Framework.Test]
        public virtual void InvalidAutoRepeatTest() {
            RunTest("invalidAutoRepeatTest");
        }

        [NUnit.Framework.Test]
        public virtual void InvalidParameterRepeatTest() {
            RunTest("invalidParameterRepeatTest");
        }

        [NUnit.Framework.Test]
        public virtual void MinMaxAutoFillTest() {
            RunTest("minMaxAutoFillTest");
        }

        [NUnit.Framework.Test]
        public virtual void MinMaxAutoFillWithHeightTest() {
            RunTest("minMaxAutoFillWithHeightTest");
        }

        [NUnit.Framework.Test]
        public virtual void MinMaxAutoFillWithMaxHeightTest() {
            RunTest("minMaxAutoFillWithMaxHeightTest");
        }

        [NUnit.Framework.Test]
        public virtual void MixedRepeatsTest() {
            RunTest("mixedRepeatsTest");
        }

        [NUnit.Framework.Test]
        public virtual void ResolvableAutoFillSimpleTest() {
            RunTest("resolvableAutoFillSimpleTest");
        }

        [NUnit.Framework.Test]
        public virtual void ResolvableAutoFitWithMinMaxTest() {
            RunTest("resolvableAutoFitWithMinMaxTest");
        }

        [NUnit.Framework.Test]
        public virtual void SeveralValuesAutoFillTest() {
            RunTest("severalValuesAutoFillTest");
        }

        [NUnit.Framework.Test]
        public virtual void AutoFitOnIntrinsicAreaTest() {
            RunTest("autoFitOnIntrinsicAreaTest");
        }

        [NUnit.Framework.Test]
        public virtual void AutoFillWithDefiniteMinMaxTest() {
            RunTest("autoFillWithDefiniteMinMaxTest");
        }

        [NUnit.Framework.Test]
        public virtual void AutoFillWithIndefiniteMinMaxTest() {
            RunTest("autoFillWithIndefiniteMinMaxTest");
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutExceptionMessageConstant.FLEXIBLE_ARENT_ALLOWED_AS_MINIMUM_IN_MINMAX)]
        public virtual void MinmaxWithMinFrTest() {
            RunTest("minmaxWithMinFrTest");
        }

        [NUnit.Framework.Test]
        public virtual void MinmaxWithMaxFrTest() {
            RunTest("minmaxWithMaxFrTest");
        }

        [NUnit.Framework.Test]
        public virtual void MinMaxWithIndefiniteMinTest() {
            RunTest("minMaxWithIndefiniteMinTest");
        }

        [NUnit.Framework.Test]
        public virtual void PointZeroFlexTest() {
            RunTest("pointZeroFlexTest");
        }

        [NUnit.Framework.Test]
        public virtual void PointZeroFlexTest2() {
            RunTest("pointZeroFlexTest2");
        }

        [NUnit.Framework.Test]
        public virtual void PointZeroFlexTest3() {
            RunTest("pointZeroFlexTest3");
        }

        [NUnit.Framework.Test]
        public virtual void PointZeroFlexTest4() {
            RunTest("pointZeroFlexTest4");
        }

        [NUnit.Framework.Test]
        public virtual void PointZeroFlexTest5() {
            RunTest("pointZeroFlexTest5");
        }

        [NUnit.Framework.Test]
        public virtual void PointZeroFlexTest6() {
            RunTest("pointZeroFlexTest6");
        }

        [NUnit.Framework.Test]
        public virtual void SpanOnlyFrTest() {
            RunTest("spanOnlyFrTest");
        }

        [NUnit.Framework.Test]
        public virtual void AutoFitOnIntrinsicAreaWithLargeBorderTest() {
            RunTest("autoFitOnIntrinsicAreaWithLargeBorderTest");
        }

        [NUnit.Framework.Test]
        public virtual void AutoFitWithLargeBorderTest() {
            RunTest("autoFitWithLargeBorderTest");
        }

        [NUnit.Framework.Test]
        public virtual void AutoFitOnIntrinsicAreaWithLargeMarginPaddingTest() {
            RunTest("autoFitOnIntrinsicAreaWithLargeMarginPaddingTest");
        }

        [NUnit.Framework.Test]
        public virtual void AutoRepeatOnIntrinsicAreaTest() {
            RunTest("autoRepeatOnIntrinsicAreaTest");
        }

        [LogMessage(LayoutExceptionMessageConstant.GRID_AUTO_REPEAT_CANNOT_BE_COMBINED_WITH_INDEFINITE_SIZES)]
        [NUnit.Framework.Test]
        public virtual void AutoRepeatWithIntrinsicArgumentTest() {
            RunTest("autoRepeatWithIntrinsicArgumentTest");
        }

        [LogMessage(LayoutExceptionMessageConstant.GRID_AUTO_REPEAT_CAN_BE_USED_ONLY_ONCE)]
        [NUnit.Framework.Test]
        public virtual void TwoAutoRepeatsTest() {
            RunTest("twoAutoRepeatsTest");
        }

        [NUnit.Framework.Test]
        public virtual void AutoFillRepeatWithFlexMinMaxTest() {
            RunTest("autoFillRepeatWithFlexMinMaxTest");
        }

        [NUnit.Framework.Test]
        public virtual void AutoFitRepeatWithFlexMinMaxTest() {
            RunTest("autoFitRepeatWithFlexMinMaxTest");
        }

        [NUnit.Framework.Test]
        public virtual void RepeatInsideMinMaxTest() {
            RunTest("repeatInsideMinMaxTest");
        }

        [LogMessage(LayoutExceptionMessageConstant.GRID_AUTO_REPEAT_CANNOT_BE_COMBINED_WITH_INDEFINITE_SIZES)]
        [NUnit.Framework.Test]
        public virtual void AutoRepeatWithFitContentTest() {
            RunTest("autoRepeatWithFitContentTest");
        }

        [NUnit.Framework.Test]
        public virtual void FixedRepeatWithFitContentTest() {
            RunTest("fixedRepeatWithFitContentTest");
        }

        [NUnit.Framework.Test]
        public virtual void FixedRepeatWithMinMaxContentTest() {
            RunTest("fixedRepeatWithMinMaxContentTest");
        }

        [LogMessage(LayoutExceptionMessageConstant.GRID_AUTO_REPEAT_CANNOT_BE_COMBINED_WITH_INDEFINITE_SIZES)]
        [NUnit.Framework.Test]
        public virtual void AutoRepeatWithLeadingMaxContentTest() {
            RunTest("autoRepeatWithLeadingMaxContentTest");
        }

        [NUnit.Framework.Test]
        public virtual void AutoFitWithGapsTest() {
            RunTest("autoFitWithGapsTest");
        }

        [NUnit.Framework.Test]
        public virtual void RowColumnShorthandSimpleTest() {
            RunTest("rowColumnShorthandSimpleTest");
        }

        [NUnit.Framework.Test]
        public virtual void GridShorthandColumnAutoFlowTest() {
            RunTest("gridShorthandColumnAutoFlowTest");
        }

        [NUnit.Framework.Test]
        public virtual void GridShorthandRowAutoFlowTest() {
            RunTest("gridShorthandRowAutoFlowTest");
        }

        [NUnit.Framework.Test]
        public virtual void ShrankTemplateAfterAutoFitTest() {
            RunTest("shrankTemplateAfterAutoFitTest");
        }

        [NUnit.Framework.Test]
        public virtual void MinHeightTest() {
            RunTest("minHeightTest");
        }

        [NUnit.Framework.Test]
        public virtual void MinHeightFlexRowsTest() {
            RunTest("minHeightFlexRowsTest");
        }

        [NUnit.Framework.Test]
        public virtual void MaxHeightTest() {
            // TODO DEVSIX-8426 Fix working with min\max-height\width on grid container
            RunTest("maxHeightTest");
        }

        [NUnit.Framework.Test]
        public virtual void MaxHeightFlexRowsTest() {
            // TODO DEVSIX-8426 Fix working with min\max-height\width on grid container
            RunTest("maxHeightFlexRowsTest");
        }

        [NUnit.Framework.Test]
        public virtual void MaxHeightFlexRowsTest2() {
            RunTest("maxHeightFlexRowsTest2");
        }

        [LogMessage(Html2PdfLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, LogLevel = LogLevelConstants.WARN
            )]
        public virtual void DivNestingTest() {
            RunTest("divNestingTest");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetBaseUri
                (SOURCE_FOLDER));
        }
    }
}
