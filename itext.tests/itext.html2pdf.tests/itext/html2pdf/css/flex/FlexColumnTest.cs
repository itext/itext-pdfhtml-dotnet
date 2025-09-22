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
using iText.Html2pdf.Logs;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Flex {
    [NUnit.Framework.Category("IntegrationTest")]
    public class FlexColumnTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/flex/FlexColumnTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/flex/FlexColumnTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignIItemsCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignIItemsCenter", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsCenterJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsCenterJustifyContentCenter", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsCenterJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsCenterJustifyContentEnd", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsCenterJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsCenterJustifyContentFlexEnd", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsCenterJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsCenterJustifyContentFlexStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnAlignContentFlexStartWrapReverseTest() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-flex-start-wrap-reverse", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnAlignContentFlexEndWrapReverseTest() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-flex-end-wrap-reverse", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnAlignContentFlexCenterWrapReverseTest() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-flex-center-wrap-reverse", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnAlignContentSpaceAroundWrapReverseTest() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-space-around-wrap-reverse", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnAlignContentSpaceEvenlyWrapReverseTest() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-space-evenly-wrap-reverse", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnAlignContentSpaceBetweenWrapReverseTest() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-space-between-wrap-reverse", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 3)]
        public virtual void FlexDirColumnAlignContentWrapReverseTest() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-wrap-reverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsCenterJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsCenterJustifyContentStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsEndJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsEndJustifyContentCenter", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsEndJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsEndJustifyContentEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsEndJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsEndJustifyContentFlexEnd", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsEndJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsEndJustifyContentFlexStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsEndJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsEndJustifyContentStart", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexEndJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexEndJustifyContentCenter", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexEndJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexEndJustifyContentEnd", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexEndJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexEndJustifyContentFlexEnd", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexEndJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexEndJustifyContentFlexStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexEndJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexEndJustifyContentStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexStart", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexStartJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexStartJustifyContentCenter", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexStartJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexStartJustifyContentEnd", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexStartJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexStartJustifyContentFlexEnd", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexStartJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexStartJustifyContentFlexStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsFlexStartJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsFlexStartJustifyContentStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsStart", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsStartJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsStartJustifyContentCenter", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsStartJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsStartJustifyContentEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsStartJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsStartJustifyContentFlexEnd", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsStartJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsStartJustifyContentFlexStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnAlignItemsStartJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnAlignItemsStartJustifyContentStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnJustifyContentCenter", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnJustifyContentEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnJustifyContentFlexEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnJustifyContentFlexStart", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnJustifyContentStart", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnJustifyContentStartMaxSizeTest() {
            ConvertToPdfAndCompare("FlexDirColumnJustifyContentStartMaxSize", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnJustifyContentStartMinSizeTest() {
            ConvertToPdfAndCompare("FlexDirColumnJustifyContentStartMinSize", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnRelativeWidthHrChildTest() {
            ConvertToPdfAndCompare("ColumnRelativeWidthHrChild", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnRelativeWidthInInlineBlockHrChildTest() {
            ConvertToPdfAndCompare("ColumnRelativeWidthInInlineBlockHrChild", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnRelativeWidthDivWithContentChildTest() {
            ConvertToPdfAndCompare("ColumnRelativeWidthDivWithContentChild", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnFixedWidthDivWithContentChildTest() {
            ConvertToPdfAndCompare("ColumnFixedWidthDivWithContentChild", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 51)]
        public virtual void FlexDirColumnAlignContentBaselineTest() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-baseline", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnAlignContentCenterTest() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-center", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnAlignContentEndTest() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-end", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void FlexDirColumnAlignContentFirstBaselineTest() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-first-baseline", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnAlignContentFlexEndTest() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-flex-end", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnAlignContentFlexEndTest2() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-flex-end-2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnAlignContentFlexStartTest() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-flex-start", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnAlignContentNormalTest() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-normal", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnAlignContentSpaceAroundTest() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-space-around", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnAlignContentSpaceAroundTest2() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-space-around-2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnAlignContentSpaceBetweenTest() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-space-between", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnAlignContentSpaceBetweenTest2() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-space-between-2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnAlignContentSpaceEvenlyTest() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-space-evenly", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnAlignContentSpaceEvenlyTest2() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-space-evenly-2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnAlignContentStartTest() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-start", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ELEMENT_DOES_NOT_FIT_CURRENT_AREA)]
        public virtual void FlexDirColumnAlignContentStretchTest() {
            ConvertToPdfAndCompare("flex-dir-column-align-content-stretch", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnNonPagingTest() {
            ConvertToPdfAndCompare("column-non-paging", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnPagingTest() {
            ConvertToPdfAndCompare("column-paging", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnPagingMultiColumnTest() {
            ConvertToPdfAndCompare("column-paging-multi-column", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnPagingLargeElementTest() {
            ConvertToPdfAndCompare("column-paging-large-element", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnPagingLargeElementFlexEndJustificationTest() {
            ConvertToPdfAndCompare("column-paging-large-element-flex-end-justification", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnPagingLargeElementCenterJustificationTest() {
            ConvertToPdfAndCompare("column-paging-large-element-center-justification", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnPagingLargeElementFixedHeightTest() {
            ConvertToPdfAndCompare("column-paging-large-element-fixed-height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnWrapReverseNonPagingTest() {
            ConvertToPdfAndCompare("column-wrap-reverse-non-paging", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT)]
        public virtual void ColumnPagingInDivTest() {
            ConvertToPdfAndCompare("column-paging-in-div", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnPagingFixedHeightTest() {
            ConvertToPdfAndCompare("column-paging-fixed-height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnNoWrapPagingTest() {
            ConvertToPdfAndCompare("column-nowrap-paging", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnFlexShrinkPagingTest() {
            ConvertToPdfAndCompare("column-flex-shrink-paging", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnFlexGrowPagingTest() {
            ConvertToPdfAndCompare("column-flex-grow-paging", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnFlexGrowPaging2Test() {
            ConvertToPdfAndCompare("column-flex-grow-paging-2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TableInFlexOnSplitTest() {
            ConvertToPdfAndCompare("table-in-flex-on-split", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH, Count = 
            4)]
        public virtual void TableInFlexOnSplit2Test() {
            ConvertToPdfAndCompare("table-in-flex-on-split2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TableInFlexColumnOnSplitTest() {
            ConvertToPdfAndCompare("table-in-flex-column-on-split", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
