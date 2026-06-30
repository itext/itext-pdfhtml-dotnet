/*
This file is part of the iText (R) project.
Copyright (c) 1998-2026 Apryse Group NV
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
    public class AbsolutePositionTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/AbsolutePositionTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/AbsolutePositionTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition01Test() {
            ConvertToPdfAndCompare("absolutePositionTest01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition02Test() {
            ConvertToPdfAndCompare("absolutePositionTest02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition03Test() {
            ConvertToPdfAndCompare("absolutePositionTest03", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition04Test() {
            ConvertToPdfAndCompare("absolutePositionTest04", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition05Test() {
            ConvertToPdfAndCompare("absolutePositionTest05", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition06Test() {
            ConvertToPdfAndCompare("absolutePositionTest06", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition07Test() {
            ConvertToPdfAndCompare("absolutePositionTest07", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition08Test() {
            ConvertToPdfAndCompare("absolutePositionTest08", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.OCCUPIED_AREA_HAS_NOT_BEEN_INITIALIZED)]
        public virtual void AbsolutePosition09Test() {
            ConvertToPdfAndCompare("absolutePositionTest09", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition10Test() {
            ConvertToPdfAndCompare("absolutePositionTest10", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition11Test() {
            ConvertToPdfAndCompare("absolutePositionTest11", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition12Test() {
            ConvertToPdfAndCompare("absolutePositionTest12", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition13Test() {
            ConvertToPdfAndCompare("absolutePositionTest13", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition14Test() {
            ConvertToPdfAndCompare("absolutePositionTest14", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePosition15Test() {
            ConvertToPdfAndCompare("absolutePositionTest15", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePositionTest16() {
            ConvertToPdfAndCompare("absolutePositionTest16", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePositionTest17() {
            ConvertToPdfAndCompare("absolutePositionTest17", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Ignore("DEVSIX-1818")]
        [NUnit.Framework.Test]
        public virtual void AbsolutePositionTest18() {
            ConvertToPdfAndCompare("absolutePositionTest18", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosNoTopBottomTest01() {
            ConvertToPdfAndCompare("absPosNoTopBottomTest01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePositionSplitPagesBeforeTextTest() {
            ConvertToPdfAndCompare("absolutePositionSplitPagesBeforeText", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePositionSplitPagesAfterTextTest() {
            ConvertToPdfAndCompare("absolutePositionSplitPagesAfterText", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsolutePositionSplitPagesAfterTextInImageBlockTest() {
            ConvertToPdfAndCompare("absolutePositionSplitPagesAfterTextInImageBlock", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosTopOnlyTest() {
            ConvertToPdfAndCompare("absPosTopOnly", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosLeftOnlyTest() {
            ConvertToPdfAndCompare("absPosLeftOnly", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosBottomOnlyTest() {
            ConvertToPdfAndCompare("absPosBottomOnly", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosRightOnlyTest() {
            ConvertToPdfAndCompare("absPosRightOnly", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosRightBottomOnlyTest() {
            ConvertToPdfAndCompare("absPosRightBottomOnly", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosTopRightCornerTest() {
            ConvertToPdfAndCompare("absPosTopRightCorner", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosBottomLeftCornerTest() {
            ConvertToPdfAndCompare("absPosBottomLeftCorner", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosNoOffsetsTest() {
            ConvertToPdfAndCompare("absPosNoOffsets", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosLeftRightOverconstrainedTest() {
            ConvertToPdfAndCompare("absPosLeftRightOverconstrained", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosMinMaxWidthClashTest() {
            ConvertToPdfAndCompare("absPosMinMaxWidthClash", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosInsetShorthandTest() {
            ConvertToPdfAndCompare("absPosInsetShorthand", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosCenteringMarginAutoTest() {
            ConvertToPdfAndCompare("absPosCenteringMarginAuto", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.CSS_PROPERTY_IN_PERCENTS_NOT_SUPPORTED, Count = 4)]
        public virtual void AbsPosPercentageAutoHeightTest() {
            ConvertToPdfAndCompare("absPosPercentageAutoHeight", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosZeroNegativeDimensionsTest() {
            ConvertToPdfAndCompare("absPosZeroNegativeDimensions", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosExtremeOffsetsTest() {
            ConvertToPdfAndCompare("absPosExtremeOffsets", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosExtremelyOversizedTest() {
            ConvertToPdfAndCompare("absPosExtremelyOversized", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosRtlLeftRightTest() {
            ConvertToPdfAndCompare("absPosRtlLeftRight", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosRtlLeftRightBothTest() {
            ConvertToPdfAndCompare("absPosRtlLeftRightBoth", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosWritingModeVerticalTest() {
            ConvertToPdfAndCompare("absPosWritingModeVertical", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsoluteInsideRelativeInsideAbsoluteTest() {
            ConvertToPdfAndCompare("absoluteInsideRelativeInsideAbsolute", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsoluteNestedFourLevelsTest() {
            ConvertToPdfAndCompare("absoluteNestedFourLevels", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsoluteInsideStaticAncestorChainTest() {
            ConvertToPdfAndCompare("absoluteInsideStaticAncestorChain", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.OCCUPIED_AREA_HAS_NOT_BEEN_INITIALIZED, Count = 3)]
        public virtual void AbsPosOnImageTest() {
            ConvertToPdfAndCompare("absPosOnImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.OCCUPIED_AREA_HAS_NOT_BEEN_INITIALIZED)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.FONT_PROPERTY_MUST_BE_PDF_FONT_OBJECT, Count = 2)]
        public virtual void AbsPosOnInlineTextTest() {
            ConvertToPdfAndCompare("absPosOnInlineText", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosOnListTest() {
            ConvertToPdfAndCompare("absPosOnList", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosOnTableTest() {
            ConvertToPdfAndCompare("absPosOnTable", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.OCCUPIED_AREA_HAS_NOT_BEEN_INITIALIZED)]
        public virtual void AbsPosOnFormControlsTest() {
            ConvertToPdfAndCompare("absPosOnFormControls", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.CSS_PROPERTY_IN_PERCENTS_NOT_SUPPORTED, Count = 2)]
        public virtual void AbsPosComboFlexNestedMissingAxisTest() {
            ConvertToPdfAndCompare("absPosComboFlexNestedMissingAxis", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosComboRtlWritingModeNestedTest() {
            ConvertToPdfAndCompare("absPosComboRtlWritingModeNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosComboStaticChainOversizedMinWidthTest() {
            ConvertToPdfAndCompare("absPosComboStaticChainOversizedMinWidth", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AbsPosContainerSpansAcrossMultiplePagesTest() {
            ConvertToPdfAndCompare("absPosContainerSpansAcrossMultiplePages", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
