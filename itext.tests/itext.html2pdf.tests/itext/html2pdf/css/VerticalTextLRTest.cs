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
using iText.Layout.Logs;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class VerticalTextLRTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/VerticalTextLRTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/VerticalTextLRTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrAbsolutePositioningTest() {
            //TODO DEVSIX-10183 fixed width ignored
            ConvertToPdfAndCompare("vertLrAbsolutePositioning", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrBackgroundDecorationTest() {
            //TODO DEVSIX-10168 Border and background sizing in flex container
            ConvertToPdfAndCompare("vertLrBackgroundDecoration", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrBasicTest() {
            ConvertToPdfAndCompare("vertLrBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrBlockquoteTest() {
            // Difference with browser due to tag "cite" not being supported
            ConvertToPdfAndCompare("vertLrBlockquote", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrCjkTextTest() {
            //TODO DEVSIX-10168 flex box borders
            ConvertToPdfAndCompare("vertLrCjkText", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrComboComplexTest() {
            //TODO DEVSIX-10168 paragraph positioning in flex container
            //TODO DEVSIX-10180 Support text rise in html mode for vertical text
            ConvertToPdfAndCompare("vertLrComboComplex", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrComboFlexMixedTest() {
            //TODO DEVSIX-10168 horizontal positioning of paragraph content is wrong
            ConvertToPdfAndCompare("vertLrComboFlexMixed", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrComboSpacingDecorationOverflowTest() {
            //TODO DEVSIX-10180 Support text rise in html mode for vertical text
            ConvertToPdfAndCompare("vertLrComboSpacingDecorationOverflow", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrComboWideDecoratedTest() {
            //TODO DEVSIX-10180 Strike-through positioning is off.
            ConvertToPdfAndCompare("vertLrComboWideDecorated", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrCssColumnsTest() {
            //TODO DEVSIX-10168 flex box borders div positioning
            ConvertToPdfAndCompare("vertLrCssColumns", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrFlexMinMaxTest() {
            //TODO DEVSIX-10168 Occupied area for flex container is incorrect.
            ConvertToPdfAndCompare("vertLrFlexMinMax", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrFloatTest() {
            // Floating paragraph positioning and sizing is not supported with vertical writing.
            ConvertToPdfAndCompare("vertLrFloat", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrHeadingsTest() {
            //TODO DEVSIX-10168 header border in flex container too narrow
            ConvertToPdfAndCompare("vertLrHeadings", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrImageInlineBlockTest() {
            //TODO DEVSIX-10188 inline svg color is not working
            ConvertToPdfAndCompare("vertLrImageInlineBlock", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        //TODO DEVSIX-10168 last paragraph too small in flex container
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT, Count = 4)]
        public virtual void VertLrLetterSpacingTest() {
            ConvertToPdfAndCompare("vertLrLetterSpacing", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        //TODO DEVSIX-10168 flex borders misaligned
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.RECTANGLE_HAS_NEGATIVE_SIZE)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT, Count = 8)]
        public virtual void VertLrLineHeightTest() {
            ConvertToPdfAndCompare("vertLrLineHeight", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        //TODO DEVSIX-10186 Lists with vertical writing.
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT, Count = 4)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.OCCUPIED_AREA_HAS_NOT_BEEN_INITIALIZED)]
        public virtual void VertLrListsTest() {
            ConvertToPdfAndCompare("vertLrLists", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrLongContainerTest() {
            //TODO DEVSIX-10168 When height > Page length run over pages before going to next line
            ConvertToPdfAndCompare("vertLrLongContainer", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrLongTextTest() {
            //TODO DEVSIX-10168 borders incorrect when page overflows
            ConvertToPdfAndCompare("vertLrLongText", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrMixedFontsTest() {
            // Symbol height calculations differ slightly depending on the font in use.
            ConvertToPdfAndCompare("vertLrMixedFonts", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrNoSoftWrapTest() {
            //TODO DEVSIX-10168 paragraph positioning in flex container + border sizing
            ConvertToPdfAndCompare("vertLrNoSoftWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        //TODO DEVSIX-10168 paragraph positioning in flex container
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT, Count = 9)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.RECTANGLE_HAS_NEGATIVE_SIZE, Count = 3)]
        public virtual void VertLrOverflowTest() {
            ConvertToPdfAndCompare("vertLrOverflow", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrSpacingRatioTest() {
            ConvertToPdfAndCompare("vertLrSpacingRatio", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrTableCellTest() {
            //TODO DEVSIX-10183 Cells vertically oversized
            ConvertToPdfAndCompare("vertLrTableCell", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrTextAlignTest() {
            //TODO DEVSIX-10180 Support text rise in html mode for vertical text
            //TODO DEVSIX-10168 paragraph border in flex container too narrow
            ConvertToPdfAndCompare("vertLrTextAlign", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrTextCombineUprightTest() {
            //TODO DEVSIX-10167 text-combine-upright all not supported
            //TODO DEVSIX-10168 Border sizing and positioning in flex container
            ConvertToPdfAndCompare("vertLrTextCombineUpright", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrTextDecorationTest() {
            //TODO DEVSIX-10180 Support text rise in html mode for vertical text
            //TODO DEVSIX-10168 paragraph border in flex container too narrow
            ConvertToPdfAndCompare("vertLrTextDecoration", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrUnderlinePositionTest() {
            //TODO DEVSIX-10168 paragraph border in flex container too narrow
            ConvertToPdfAndCompare("vertLrUnderlinePosition", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrVerticalAlignTest() {
            //TODO DEVSIX-10180 Support text rise in html mode for vertical text
            ConvertToPdfAndCompare("vertLrVerticalAlign", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrWideMulticolumnTest() {
            ConvertToPdfAndCompare("vertLrWideMulticolumn", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertLrWordSpacingTest() {
            //TODO DEVSIX-10168 last paragraph too small in flex container
            ConvertToPdfAndCompare("vertLrWordSpacing", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT, LogLevel = LogLevelConstants.WARN, Count = 9)]
        [LogMessage(LayoutLogMessageConstant.FLEX_ITEM_LAYOUT_RESULT_IS_NOT_FULL, LogLevel = LogLevelConstants.ERROR
            , Count = 1)]
        public virtual void VertLrZeroNegativeDimensionsTest() {
            // Vertical text with extreme values doesn't work normal, but it doesn't throw or results in infinite loop, which is
            // good enough already.
            ConvertToPdfAndCompare("vertLrZeroNegativeDimensions", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT, Count = 7)]
        public virtual void MixedUprightSidewaysTest() {
            // TODO DEVSIX-10176 Text-orientation sideways and mixed are not supported.
            ConvertToPdfAndCompare("mixedUprightSideways", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void OccupiedAreaTest() {
            ConvertToPdfAndCompare("occupiedArea", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void InlineBlockAndTextRiseTest() {
            //TODO DEVSIX-10180 Support text rise in html mode for vertical text
            ConvertToPdfAndCompare("inline_block_and_text_rise", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
