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
    public class FlexRowTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FlexRowTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FlexRowTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 51)]
        public virtual void FlexDirRowAlignContentBaselineTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-baseline", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentCenterTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-center", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void FlexDirRowAlignContentEndTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-end", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void FlexDirRowAlignContentFirstBaselineTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-first-baseline", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentFlexEndTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-flex-end", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentFlexEndTest2() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-flex-end-2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentFlexStartTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-flex-start", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentNormalTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-normal", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentSpaceAroundTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-space-around", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentSpaceAroundTest2() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-space-around-2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentSpaceBetweenTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-space-between", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentSpaceBetweenTest2() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-space-between-2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentSpaceEvenlyTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-space-evenly", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentSpaceEvenlyTest2() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-space-evenly-2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentSpaceEvenlySplittingTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-space-evenly-splitting", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentSpaceEvenlySplittingWithOneElementTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-space-evenly-splitting-one-element", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentSpaceBetweenSplittingTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-space-between-splitting", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentSpaceBetweenSplittingOneElementTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-space-between-splitting-one-element", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentSpaceAroundSplittingTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-space-around-splitting", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentSpaceAroundSplittingOneElementTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-space-around-splitting-one-element", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentFlexEndSplittingTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-flex-end-splitting", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentFlexEndSplittingOneElementTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-flex-end-splitting-one-element", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentCenterSplittingTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-center-splitting", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentCenterSplittingOneElementTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-center-splitting-one-element", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 3)]
        public virtual void FlexDirRowAlignContentWrapReverseTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-wrap-reverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void FlexDirRowAlignContentStartTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-start", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignContentStretchTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-content-stretch", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 51)]
        public virtual void FlexDirRowAlignItemsBaselineTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-items-baseline", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 51)]
        public virtual void FlexDirRowAlignItemsBaseline2Test() {
            ConvertToPdfAndCompare("flex-dir-row-align-items-baseline2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignItemsCenterTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-items-center", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignItemsCenter2Test() {
            ConvertToPdfAndCompare("flex-dir-row-align-items-center2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignItemsEndTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-items-end", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignItemsFlexEndTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-items-flex-end", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignItemsFlexEnd2Test() {
            ConvertToPdfAndCompare("flex-dir-row-align-items-flex-end2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignItemsFlexStartTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-items-flex-start", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignItemsFlexStart2Test() {
            ConvertToPdfAndCompare("flex-dir-row-align-items-flex-start2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignItemsSelfEndTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-items-self-end", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignItemsSelfStartTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-items-self-start", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignItemsStartTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-items-start", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignItemsStretchTest() {
            ConvertToPdfAndCompare("flex-dir-row-align-items-stretch", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowAlignItemsStretch2Test() {
            ConvertToPdfAndCompare("flex-dir-row-align-items-stretch2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowNonPagingTest() {
            ConvertToPdfAndCompare("row-non-paging", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
