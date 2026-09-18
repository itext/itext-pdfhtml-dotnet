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

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class VerticalTextMixedDirectionsTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/VerticalTextMixedDirectionsTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/VerticalTextMixedDirectionsTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphMixedTextTest() {
            ConvertToPdfAndCompare("paragraphMixedTextTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphMixedVerticalTextTest() {
            // TODO DEVSIX-10200 Consider text elements with different writing-mode as inline-blocks,
            //  after that vertical RTL text chunks in vertical LTR paragraphs and vice versa will be fixed.
            ConvertToPdfAndCompare("paragraphMixedVerticalText", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphMixedVerticalTextRtlTest() {
            // TODO DEVSIX-10200 Consider text elements with different writing-mode as inline-blocks
            ConvertToPdfAndCompare("paragraphMixedVerticalTextRtl", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphMixedVerticalTextHorizontalTest() {
            // TODO DEVSIX-10200 Consider text elements with different writing-mode as inline-blocks
            ConvertToPdfAndCompare("paragraphMixedVerticalTextHorizontal", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphMixedVerticalTextInlineBlockTest() {
            // TODO DEVSIX-10200 Consider text elements with different writing-mode as inline-blocks
            ConvertToPdfAndCompare("paragraphMixedVerticalTextInlineBlock", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphMixedTextWithLineBreaksTest() {
            ConvertToPdfAndCompare("paragraphMixedTextWithLineBreaksTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphMixedTextNoEnoughHorizontalSpaceTest() {
            ConvertToPdfAndCompare("paragraphMixedTextNoEnoughHorizontalSpaceTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphMixedTextWithPageBreakTest() {
            ConvertToPdfAndCompare("paragraphMixedTextWithPageBreakTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalParagraphMixedTest() {
            ConvertToPdfAndCompare("verticalParagraphMixedTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalParagraphMixedWithLineBreaksTest() {
            ConvertToPdfAndCompare("verticalParagraphMixedWithLineBreaksTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalWritingAtTextLevelTest() {
            ConvertToPdfAndCompare("verticalWritingAtTextLevelTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalWritingAtTextLevelTwoLinesTest() {
            ConvertToPdfAndCompare("verticalWritingAtTextLevelTwoLinesTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalWritingAtTextLevelPageBreakTest() {
            ConvertToPdfAndCompare("verticalWritingAtTextLevelPageBreakTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalWritingAtTextLevelLongTextTest() {
            ConvertToPdfAndCompare("verticalWritingAtTextLevelLongTextTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VerticalParagraphWithHorizontalTextTest() {
            ConvertToPdfAndCompare("verticalParagraphWithHorizontalTextTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
