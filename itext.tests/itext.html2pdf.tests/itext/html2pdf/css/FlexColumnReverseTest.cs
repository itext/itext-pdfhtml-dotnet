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

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class FlexColumnReverseTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FlexColumnReverseTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FlexColumnReverseTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignIItemsCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignIItemsCenter", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsCenterJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsCenterJustifyContentCenter", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsCenterJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsCenterJustifyContentEnd", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsCenterJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsCenterJustifyContentFlexEnd", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsCenterJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsCenterJustifyContentFlexStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsCenterJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsCenterJustifyContentStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsEndJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsEndJustifyContentCenter", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsEndJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsEndJustifyContentEnd", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsEndJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsEndJustifyContentFlexEnd", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsEndJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsEndJustifyContentFlexStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsEndJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsEndJustifyContentStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexEndJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexEndJustifyContentCenter", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexEndJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexEndJustifyContentEnd", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexEndJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexEndJustifyContentFlexEnd", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexEndJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexEndJustifyContentFlexStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexEndJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexEndJustifyContentStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexStart", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexStartJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexStartJustifyContentCenter", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexStartJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexStartJustifyContentEnd", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexStartJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexStartJustifyContentFlexEnd", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexStartJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexStartJustifyContentFlexStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsFlexStartJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsFlexStartJustifyContentStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsStart", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsStartJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsStartJustifyContentCenter", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsStartJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsStartJustifyContentEnd", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsStartJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsStartJustifyContentFlexEnd", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsStartJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsStartJustifyContentFlexStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseAlignItemsStartJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseAlignItemsStartJustifyContentStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseJustifyContentCenterTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseJustifyContentCenter", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseJustifyContentEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseJustifyContentEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseJustifyContentFlexEndTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseJustifyContentFlexEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseJustifyContentFlexStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseJustifyContentFlexStart", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseJustifyContentStartTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseJustifyContentStart", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseJustifyContentStartMaxSizeTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseJustifyContentStartMaxSize", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseJustifyContentStartMinSizeTest() {
            ConvertToPdfAndCompare("FlexDirColumnReverseJustifyContentStartMinSize", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 51)]
        public virtual void FlexDirColumnReverseAlignContentBaselineTest() {
            ConvertToPdfAndCompare("flex-dir-column-reverse-align-content-baseline", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnReverseAlignContentCenterTest() {
            ConvertToPdfAndCompare("flex-dir-column-reverse-align-content-center", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnReverseAlignContentEndTest() {
            ConvertToPdfAndCompare("flex-dir-column-reverse-align-content-end", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void FlexDirColumnReverseAlignContentFirstBaselineTest() {
            ConvertToPdfAndCompare("flex-dir-column-reverse-align-content-first-baseline", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnReverseAlignContentFlexEndTest() {
            ConvertToPdfAndCompare("flex-dir-column-reverse-align-content-flex-end", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnReverseAlignContentFlexStartTest() {
            ConvertToPdfAndCompare("flex-dir-column-reverse-align-content-flex-start", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnReverseAlignContentNormalTest() {
            ConvertToPdfAndCompare("flex-dir-column-reverse-align-content-normal", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnReverseAlignContentSpaceAroundTest() {
            ConvertToPdfAndCompare("flex-dir-column-reverse-align-content-space-around", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnReverseAlignContentSpaceBetweenTest() {
            ConvertToPdfAndCompare("flex-dir-column-reverse-align-content-space-between", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnReverseAlignContentFlexStartWrapReverseTest() {
            ConvertToPdfAndCompare("flex-dir-column-reverse-align-content-flex-start-wrap-reverse", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnReverseAlignContentFlexEndWrapReverseTest() {
            ConvertToPdfAndCompare("flex-dir-column-reverse-align-content-flex-end-wrap-reverse", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnReverseAlignContentFlexCenterWrapReverseTest() {
            ConvertToPdfAndCompare("flex-dir-column-reverse-align-content-flex-center-wrap-reverse", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnReverseAlignContentSpaceAroundWrapReverseTest() {
            ConvertToPdfAndCompare("flex-dir-column-reverse-align-content-space-around-wrap-reverse", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnReverseAlignContentSpaceEvenlyWrapReverseTest() {
            ConvertToPdfAndCompare("flex-dir-column-reverse-align-content-space-evenly-wrap-reverse", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnReverseAlignContentSpaceBetweenWrapReverseTest() {
            ConvertToPdfAndCompare("flex-dir-column-reverse-align-content-space-between-wrap-reverse", SOURCE_FOLDER, 
                DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnReverseAlignContentSpaceEvenlyTest() {
            ConvertToPdfAndCompare("flex-dir-column-reverse-align-content-space-evenly", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirColumnReverseAlignContentStartTest() {
            ConvertToPdfAndCompare("flex-dir-column-reverse-align-content-start", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ELEMENT_DOES_NOT_FIT_CURRENT_AREA)]
        public virtual void FlexDirColumnReverseAlignContentStretchTest() {
            ConvertToPdfAndCompare("flex-dir-column-reverse-align-content-stretch", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseNonPagingTest() {
            ConvertToPdfAndCompare("column-reverse-non-paging", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReversePagingTest() {
            ConvertToPdfAndCompare("column-reverse-paging", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReversePagingMultiColumnTest() {
            ConvertToPdfAndCompare("column-reverse-paging-multi-column", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReversePagingLargeElementTest() {
            ConvertToPdfAndCompare("column-reverse-paging-large-element", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
