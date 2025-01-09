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

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class FlexPagingTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FlexPagingTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FlexPagingTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RowNonPagingTest() {
            ConvertToPdfAndCompare("row-non-paging", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnNonPagingTest() {
            ConvertToPdfAndCompare("column-non-paging", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnPagingTest() {
            ConvertToPdfAndCompare("column-paging", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnPagingMultiColumnTest() {
            ConvertToPdfAndCompare("column-paging-multi-column", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReverseNonPagingTest() {
            ConvertToPdfAndCompare("column-reverse-non-paging", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReversePagingTest() {
            ConvertToPdfAndCompare("column-reverse-paging", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReversePagingMultiColumnTest() {
            ConvertToPdfAndCompare("column-reverse-paging-multi-column", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnPagingLargeElementTest() {
            ConvertToPdfAndCompare("column-paging-large-element", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnPagingLargeElementFlexEndJustificationTest() {
            ConvertToPdfAndCompare("column-paging-large-element-flex-end-justification", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnPagingLargeElementCenterJustificationTest() {
            ConvertToPdfAndCompare("column-paging-large-element-center-justification", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ColumnPagingLargeElementFixedHeightTest() {
            ConvertToPdfAndCompare("column-paging-large-element-fixed-height", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnReversePagingLargeElementTest() {
            ConvertToPdfAndCompare("column-reverse-paging-large-element", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnWrapReverseNonPagingTest() {
            ConvertToPdfAndCompare("column-wrap-reverse-non-paging", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT)]
        public virtual void ColumnPagingInDivTest() {
            ConvertToPdfAndCompare("column-paging-in-div", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnPagingFixedHeightTest() {
            ConvertToPdfAndCompare("column-paging-fixed-height", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnNoWrapPagingTest() {
            ConvertToPdfAndCompare("column-nowrap-paging", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnFlexShrinkPagingTest() {
            ConvertToPdfAndCompare("column-flex-shrink-paging", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnFlexGrowPagingTest() {
            ConvertToPdfAndCompare("column-flex-grow-paging", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ColumnFlexGrowPaging2Test() {
            ConvertToPdfAndCompare("column-flex-grow-paging-2", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TableInFlexOnSplitTest() {
            ConvertToPdfAndCompare("table-in-flex-on-split", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH, Count = 
            4)]
        public virtual void TableInFlexOnSplit2Test() {
            ConvertToPdfAndCompare("table-in-flex-on-split2", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TableInFlexColumnOnSplitTest() {
            ConvertToPdfAndCompare("table-in-flex-column-on-split", sourceFolder, destinationFolder);
        }
    }
}
