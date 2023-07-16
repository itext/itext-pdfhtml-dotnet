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
using iText.Layout.Logs;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Multicol {
    [NUnit.Framework.Category("IntegrationTest")]
    public class ColumnCountTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/multicol" + "/ColumnCountTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/multicol/ColumnCountTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicArticleTest() {
            RunTest("basicArticleTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicDivTest() {
            RunTest("basicDivTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicDivWithImageTest() {
            RunTest("basicDivWithImageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicPTest() {
            RunTest("basicPTest");
        }

        [NUnit.Framework.Test]
        public virtual void DiffElementsInsidePTest() {
            RunTest("diffElementsInsidePTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicFormTest() {
            RunTest("basicFormTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicUlTest() {
            RunTest("basicUlTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicOlTest() {
            RunTest("basicOlTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicTableTest() {
            RunTest("basicTableTest");
        }

        [NUnit.Framework.Test]
        public virtual void TableColspanTest() {
            RunTest("tableColspanTest");
        }

        [NUnit.Framework.Test]
        public virtual void TableRowspanTest() {
            RunTest("tableRowspanTest");
        }

        [NUnit.Framework.Test]
        public virtual void TableColspanRowspanTest() {
            RunTest("tableColspanRowspanTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicSectionTest() {
            RunTest("basicSectionTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicDivMultiPageDocumentsTest() {
            RunTest("basicDivMultiPageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicFormMultiPageDocumentsTest() {
            RunTest("basicFormMultiPageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBigFormMultiPageDocumentsTest() {
            RunTest("bigFormMultiPageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicDisplayPropertyTest() {
            RunTest("basicDisplayPropertyTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicDisplayPropertyWithNestedColumnsTest() {
            RunTest("basicDisplayPropertyWithNestedColumnsTest");
        }

        //TODO: DEVSIX-7556
        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void ConvertBasicFloatPropertyTest() {
            RunTest("basicFloatPropertyTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicFlexPropertyTest() {
            RunTest("basicFlexPropertyTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertImagesWithDifferentColValuesTest() {
            RunTest("imagesWithDifferentColValuesTest");
        }

        [NUnit.Framework.Test]
        public virtual void PaddingsMarginsBorderBackgrounds() {
            RunTest("paddingsMarginsBorderBackgrounds");
        }

        [NUnit.Framework.Test]
        public virtual void BorderOnlyTest() {
            RunTest("borderOnly");
        }

        [NUnit.Framework.Test]
        public virtual void PaddingOnlyTest() {
            RunTest("paddingOnly");
        }

        [NUnit.Framework.Test]
        public virtual void MarginOnlyTest() {
            RunTest("marginOnly");
        }

        [NUnit.Framework.Test]
        public virtual void SplitInnerParagraphBetweenColumns() {
            RunTest("splitInnerParagraphBetweenColumns");
        }

        [NUnit.Framework.Test]
        public virtual void SplitInnerParagraphWithoutMarginBetweenColumns() {
            RunTest("splitInnerParagraphWithoutMarginBetweenColumns");
        }

        [NUnit.Framework.Test]
        public virtual void SplitEmptyBlockElementsBetweenColumns() {
            RunTest("splitEmptyBlockElementsBetweenColumns");
        }

        [NUnit.Framework.Test]
        public virtual void SplitEmptyParagraphElementsBetweenColumns() {
            RunTest("splitEmptyParagraphElementsBetweenColumns");
        }

        [NUnit.Framework.Test]
        public virtual void SplitEmptyContinuousBlockElementBetweenColumns() {
            RunTest("splitEmptyContinuousBlockElementBetweenColumns");
        }

        [NUnit.Framework.Test]
        public virtual void BasicHiTest() {
            RunTest("basicHiTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicFooterHeaderTest() {
            RunTest("basicFooterHeaderTest");
        }

        [NUnit.Framework.Test]
        public virtual void TripleNestingTest() {
            RunTest("tripleNestingTest");
        }

        [NUnit.Framework.Test]
        public virtual void NestingBetweenPagesTest() {
            RunTest("nestingBetweenPagesTest");
        }

        [NUnit.Framework.Test]
        public virtual void TripleNestingBetweenPagesTest() {
            // TODO DEVSIX-7628 Investigate problem with border near to end of the page in multicol layouting
            RunTest("tripleNestingBetweenPagesTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicDlTest() {
            RunTest("basicDlTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicInlineElementsTest() {
            RunTest("basicInlineElementsTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicBlockquoteTest() {
            RunTest("basicBlockquoteTest");
        }

        [NUnit.Framework.Test]
        public virtual void ImagesMultipageTest() {
            RunTest("imagesMultipageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ImagesWithParagraphMultipageTest() {
            RunTest("imagesWithParagraphMultipageTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicOrphans1Test() {
            RunTest("basicOrphans1Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicOrphans2Test() {
            RunTest("basicOrphans2Test");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.WIDOWS_CONSTRAINT_VIOLATED, LogLevel = LogLevelConstants.WARN
            )]
        public virtual void BasicWidows1Test() {
            RunTest("basicWidows1Test");
        }

        [NUnit.Framework.Test]
        public virtual void BasicWidows2Test() {
            RunTest("basicWidows2Test");
        }

        [NUnit.Framework.Test]
        public virtual void HeightTest() {
            RunTest("height");
        }

        [NUnit.Framework.Test]
        public virtual void HeightToSmallToFitAllClipped() {
            RunTest("heightToSmallToFitAllClipped");
        }

        [NUnit.Framework.Test]
        public virtual void MinHeightToSmallSoEverythingShows() {
            RunTest("minHeightToSmallSoEverythingShows");
        }

        [NUnit.Framework.Test]
        public virtual void MinHeightBiggerSoExtraGap() {
            RunTest("minHeightBiggerSoExtraGap");
        }

        [NUnit.Framework.Test]
        public virtual void WidthTest() {
            RunTest("width");
        }

        [NUnit.Framework.Test]
        public virtual void WidthToBigSoOverflowsOnXAxisIntoInfinity() {
            // TODO DEVSIX-7630 Multicol width&height advanced support
            RunTest("widthToBigSoOverflowsOnXAxisIntoInfinity");
        }

        [NUnit.Framework.Test]
        public virtual void MinWidthBigJustOverflows() {
            RunTest("minWidthBigJustOverflows");
        }

        [NUnit.Framework.Test]
        public virtual void MinWidthContained() {
            RunTest("minWidthToLittleSoJustNormalWidth");
        }

        [NUnit.Framework.Test]
        public virtual void MaxWidthToBig() {
            RunTest("maxWidthToBig");
        }

        [NUnit.Framework.Test]
        public virtual void MaxWidthToSmall() {
            RunTest("maxWidthToSmall");
        }

        [NUnit.Framework.Test]
        public virtual void WidthToBigWrapped() {
            RunTest("widthToBigWrapped");
        }

        [NUnit.Framework.Test]
        public virtual void MaxWidthToBigWrapped() {
            RunTest("maxWidthToBigWrapped");
        }

        [NUnit.Framework.Test]
        public virtual void MultiColLimitedArea() {
            RunTest("multicolLimitedArea");
        }

        [NUnit.Framework.Test]
        public virtual void MultiColLimitedArea2() {
            RunTest("multicolLimitedArea2");
        }

        [NUnit.Framework.Test]
        public virtual void MultiColLimitedArea3() {
            // TODO DEVSIX-7630 Multicol width&height advanced support
            RunTest("multicolLimitedArea3");
        }

        [NUnit.Framework.Test]
        public virtual void MultiColLimitedArea4() {
            RunTest("multicolLimitedArea4");
        }

        [NUnit.Framework.Test]
        public virtual void MultipleAttributes() {
            RunTest("multiple_attributes");
        }

        [NUnit.Framework.Test]
        public virtual void MultipleAttributes1() {
            RunTest("multiple_attributes1");
        }

        [NUnit.Framework.Test]
        public virtual void MultipleAttributes2() {
            RunTest("multiple_attributes2");
        }

        [NUnit.Framework.Test]
        public virtual void HeightMultiPage() {
            // TODO DEVSIX-7630 Multicol width&height advanced support
            RunTest("height_multipage");
        }

        [NUnit.Framework.Test]
        public virtual void BiggerThanColumnDivTest() {
            RunTest("biggerThanColumnDivTest");
        }

        [NUnit.Framework.Test]
        public virtual void BiggerThanColumnImageTest() {
            RunTest("biggerThanColumnImageTest");
        }

        [NUnit.Framework.Test]
        public virtual void BiggerThanColumnImageOverflowHiddenTest() {
            RunTest("biggerThanColumnImageOverflowHiddenTest");
        }

        [NUnit.Framework.Test]
        public virtual void BiggerThanColumnImageOverflowScrollTest() {
            RunTest("biggerThanColumnImageOverflowScrollTest");
        }

        [NUnit.Framework.Test]
        public virtual void OverflowOnMulticolContainerTest() {
            RunTest("overflowOnMulticolContainerTest");
        }

        [NUnit.Framework.Test]
        public virtual void ShortHandResolverTest01() {
            RunTest("shortHandResolverTest01");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetBaseUri
                (SOURCE_FOLDER));
        }
    }
}
