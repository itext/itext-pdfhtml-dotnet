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
using iText.Layout.Logs;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Verticaltext {
    [NUnit.Framework.Category("IntegrationTest")]
    public class VerticalTextLRTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/verticaltext/VerticalTextLRTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/verticaltext/VerticalTextLRTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrAbsolutePositioningTest() {
            ConvertToPdfAndCompare("vertLrAbsolutePositioning", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrBackgroundDecorationTest() {
            ConvertToPdfAndCompare("vertLrBackgroundDecoration", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrBasicTest() {
            ConvertToPdfAndCompare("vertLrBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VertLrBlockquoteTest() {
            // Difference with browser due to tag "cite" not being supported
            ConvertToPdfAndCompare("vertLrBlockquote", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrCjkTextTest() {
            ConvertToPdfAndCompare("vertLrCjkText", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrComboComplexTest() {
            ConvertToPdfAndCompare("vertLrComboComplex", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VertLrComboFlexMixedTest() {
            ConvertToPdfAndCompare("vertLrComboFlexMixed", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrComboSpacingDecorationOverflowTest() {
            ConvertToPdfAndCompare("vertLrComboSpacingDecorationOverflow", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrComboWideDecoratedTest() {
            ConvertToPdfAndCompare("vertLrComboWideDecorated", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrCssColumnsTest() {
            ConvertToPdfAndCompare("vertLrCssColumns", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrFlexMinMaxTest() {
            ConvertToPdfAndCompare("vertLrFlexMinMax", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.UNSUPPORTED_PROPERTY, LogLevel = LogLevelConstants.WARN, Count = 2)]
        public virtual void VertLrFloatTest() {
            // Floating paragraph positioning and sizing is not supported with vertical writing.
            ConvertToPdfAndCompare("vertLrFloat", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VertLrHeadingsTest() {
            ConvertToPdfAndCompare("vertLrHeadings", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        // TODO DEVSIX-10188 inline svg color is not working
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT, LogLevel = LogLevelConstants
            .WARN, Count = 2)]
        public virtual void VertLrImageInlineBlockTest() {
            ConvertToPdfAndCompare("vertLrImageInlineBlock", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VertLrLetterSpacingTest() {
            ConvertToPdfAndCompare("vertLrLetterSpacing", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrLineHeightTest() {
            ConvertToPdfAndCompare("vertLrLineHeight", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT, LogLevel = LogLevelConstants
            .WARN, Count = 2)]
        public virtual void VertLrListsTest() {
            //TODO DEVSIX-10186 Lists with vertical writing.
            ConvertToPdfAndCompare("vertLrLists", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrLongContainerTest() {
            ConvertToPdfAndCompare("vertLrLongContainer", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrLongTextTest() {
            ConvertToPdfAndCompare("vertLrLongText", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrMixedFontsTest() {
            // Symbol height calculations differ slightly depending on the font in use.
            ConvertToPdfAndCompare("vertLrMixedFonts", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrNoSoftWrapTest() {
            ConvertToPdfAndCompare("vertLrNoSoftWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrOverflowTest() {
            ConvertToPdfAndCompare("vertLrOverflow", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrSpacingRatioTest() {
            ConvertToPdfAndCompare("vertLrSpacingRatio", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VertLrTableCellTest() {
            ConvertToPdfAndCompare("vertLrTableCell", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrTextAlignTest() {
            ConvertToPdfAndCompare("vertLrTextAlign", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrTextCombineUprightTest() {
            //TODO DEVSIX-10167 text-combine-upright all not supported
            ConvertToPdfAndCompare("vertLrTextCombineUpright", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrTextDecorationTest() {
            ConvertToPdfAndCompare("vertLrTextDecoration", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrUnderlinePositionTest() {
            ConvertToPdfAndCompare("vertLrUnderlinePosition", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrVerticalAlignTest() {
            ConvertToPdfAndCompare("vertLrVerticalAlign", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrVerticalAlignShiftsTest() {
            ConvertToPdfAndCompare("vertLrVerticalAlignShifts", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextRiseWithInlineVerticalAlignmentTest() {
            ConvertToPdfAndCompare("textRiseWithInlineVerticalAlignment", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NestedSpansVerticalAlignmentTest() {
            ConvertToPdfAndCompare("nestedSpansVerticalAlignment", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrWideMulticolumnTest() {
            ConvertToPdfAndCompare("vertLrWideMulticolumn", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrWordSpacingTest() {
            ConvertToPdfAndCompare("vertLrWordSpacing", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.FLEX_ITEM_LAYOUT_RESULT_IS_NOT_FULL)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.RECTANGLE_HAS_NEGATIVE_SIZE)]
        public virtual void VertLrZeroNegativeDimensionsTest() {
            // Vertical text with extreme values doesn't work normal, but it doesn't throw or results in infinite loop, which is
            // good enough already.
            ConvertToPdfAndCompare("vertLrZeroNegativeDimensions", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void MixedUprightSidewaysTest() {
            // TODO DEVSIX-10176 Text-orientation sideways and mixed are not supported.
            ConvertToPdfAndCompare("mixedUprightSideways", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void OccupiedAreaTest() {
            ConvertToPdfAndCompare("occupiedArea", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void InlineBlockAndTextRiseTest() {
            ConvertToPdfAndCompare("inline_block_and_text_rise", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void OverflowTest() {
            ConvertToPdfAndCompare("overflow", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void OverflowWrapTest() {
            ConvertToPdfAndCompare("overflowWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NoWrapTest() {
            ConvertToPdfAndCompare("noWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void OccupiedAreaSmallerThanTextTest() {
            ConvertToPdfAndCompare("occupiedAreaSmallerThanTextTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
