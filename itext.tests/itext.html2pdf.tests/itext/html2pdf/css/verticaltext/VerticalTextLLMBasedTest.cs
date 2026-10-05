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

namespace iText.Html2pdf.Css.Verticaltext {
    [NUnit.Framework.Category("IntegrationTest")]
    public class VerticalTextLLMBasedTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/verticaltext/VerticalTextLLMBasedTest/";

        public static readonly String DEST_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory + "/test/itext/html2pdf/css/verticaltext/VerticalTextLLMBasedTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DEST_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VerticalJapaneseKinsokuLineBreakingTest() {
            ConvertToPdfAndCompare("verticalJapaneseKinsokuLineBreaking", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VerticalJapaneseHangingPunctuationTest() {
            ConvertToPdfAndCompare("verticalJapaneseHangingPunctuation", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VerticalJapaneseCjkLatinAutospaceTest() {
            ConvertToPdfAndCompare("verticalJapaneseCjkLatinAutospace", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VerticalJapanesePunctuationSpacingPaltTest() {
            ConvertToPdfAndCompare("verticalJapanesePunctuationSpacingPalt", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VerticalJapaneseWidowsOrphansPaginationTest() {
            ConvertToPdfAndCompare("verticalJapaneseWidowsOrphansPagination", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VerticalJapaneseUnbreakableAlphanumericSequenceTest() {
            ConvertToPdfAndCompare("verticalJapaneseUnbreakableAlphanumericSequence", SOURCE_FOLDER, DEST_FOLDER, false
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 6)]
        public virtual void VerticalJapaneseMonoRubyVsGroupRubyTest() {
            ConvertToPdfAndCompare("verticalJapaneseMonoRubyVsGroupRuby", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VerticalKoreanWordBreakKeepAllTest() {
            ConvertToPdfAndCompare("verticalKoreanWordBreakKeepAll", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 12)]
        public virtual void VerticalChineseBopomofoRubyPositionTest() {
            ConvertToPdfAndCompare("verticalChineseBopomofoRubyPosition", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VerticalChineseEmphasisMarkPositionTest() {
            ConvertToPdfAndCompare("verticalChineseEmphasisMarkPosition", SOURCE_FOLDER, DEST_FOLDER, false);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.VERTICAL_WRITING_MODE_NOT_SUPPORTED_FOR_ELEMENT)]
        public virtual void VerticalJapaneseConsecutivePunctuationCompressionTest() {
            ConvertToPdfAndCompare("verticalJapaneseConsecutivePunctuationCompression", SOURCE_FOLDER, DEST_FOLDER, false
                );
        }
    }
}
