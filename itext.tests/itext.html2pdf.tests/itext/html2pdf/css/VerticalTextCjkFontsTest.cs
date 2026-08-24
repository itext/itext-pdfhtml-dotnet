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
    public class VerticalTextCjkFontsTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/VerticalTextCjkFontsTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/VerticalTextCjkFontsTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CjkFontMetricsComparisonTest() {
            ConvertToPdfAndCompare("cjkFontMetricsComparison", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CjkFontNotoSansJpTest() {
            ConvertToPdfAndCompare("cjkFontNotoSansJp", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CjkFontNotoSansKrTest() {
            ConvertToPdfAndCompare("cjkFontNotoSansKr", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CjkFontNotoSansMongolianTest() {
            ConvertToPdfAndCompare("cjkFontNotoSansMongolian", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CjkFontNotoSansScTest() {
            ConvertToPdfAndCompare("cjkFontNotoSansSc", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CjkFontNotoSansScBoldTest() {
            ConvertToPdfAndCompare("cjkFontNotoSansScBold", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CjkFontNotoSerifScTest() {
            ConvertToPdfAndCompare("cjkFontNotoSerifSc", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CjkLineBreakWordBreakTest() {
            ConvertToPdfAndCompare("cjkLineBreakWordBreak", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CjkMixedAllScriptsTest() {
            ConvertToPdfAndCompare("cjkMixedAllScripts", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CjkMixedBoldRegularChineseTest() {
            ConvertToPdfAndCompare("cjkMixedBoldRegularChinese", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CjkMixedChineseJapaneseKoreanTest() {
            ConvertToPdfAndCompare("cjkMixedChineseJapaneseKorean", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CjkMixedMongolianChineseLatinTest() {
            ConvertToPdfAndCompare("cjkMixedMongolianChineseLatin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CjkMixedSansSerifChineseTest() {
            ConvertToPdfAndCompare("cjkMixedSansSerifChinese", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CjkMongolianEmbeddedLatinSidewaysTest() {
            ConvertToPdfAndCompare("cjkMongolianEmbeddedLatinSideways", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CjkPunctuationOrientationTest() {
            ConvertToPdfAndCompare("cjkPunctuationOrientation", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 4)]
        public virtual void CjkRubyWithFontTest() {
            ConvertToPdfAndCompare("cjkRubyWithFont", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CjkTextCombineUprightWithFontTest() {
            ConvertToPdfAndCompare("cjkTextCombineUprightWithFont", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CjkTextEmphasisMarksTest() {
            ConvertToPdfAndCompare("cjkTextEmphasisMarks", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CjkTextOrientationMixedVsUprightTest() {
            ConvertToPdfAndCompare("cjkTextOrientationMixedVsUpright", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
