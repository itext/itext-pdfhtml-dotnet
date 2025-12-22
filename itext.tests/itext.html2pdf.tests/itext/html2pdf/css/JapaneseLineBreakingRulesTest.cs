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
using iText.Layout.Font;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class JapaneseLineBreakingRulesTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/JapaneseLineBreakingRulesTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/JapaneseLineBreakingRulesTest/";

        private static readonly String FONTS_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/fonts/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        // See https://www.w3.org/TR/jlreq/?lang=en#characters_not_starting_a_line
        [NUnit.Framework.Test]
        public virtual void CharsNotStartingLineTest() {
            FontProvider fontProvider = new FontProvider();
            fontProvider.AddFont(FONTS_FOLDER + "NotoSansJP-Regular.ttf");
            ConverterProperties props = new ConverterProperties();
            props.SetFontProvider(fontProvider);
            ConvertToPdfAndCompare("charsNotStartingLine", SOURCE_FOLDER, DESTINATION_FOLDER, false, props);
        }

        // See https://www.w3.org/TR/jlreq/?lang=en#characters_not_ending_a_line
        [NUnit.Framework.Test]
        public virtual void CharsNotEndingLineTest() {
            FontProvider fontProvider = new FontProvider();
            fontProvider.AddFont(FONTS_FOLDER + "NotoSansJP-Regular.ttf");
            ConverterProperties props = new ConverterProperties();
            props.SetFontProvider(fontProvider);
            // There is a bug in browsers that they handle left and right (double) quotes as same chars.
            // It's why browsers don't allow to start a line from '“' (Left Double Quotation Mark)
            ConvertToPdfAndCompare("charsNotEndingLine", SOURCE_FOLDER, DESTINATION_FOLDER, false, props);
        }

        // See https://www.w3.org/TR/jlreq/?lang=en#unbreakable_character_sequences
        [NUnit.Framework.Test]
        public virtual void UnseparableSequenceTest() {
            // TODO DEVSIX-4863 Layout splitting logic handles negative values incorrectly if they are not in the very beginning of Text element
            FontProvider fontProvider = new FontProvider();
            fontProvider.AddFont(FONTS_FOLDER + "NotoSansJP-Regular.ttf");
            ConverterProperties props = new ConverterProperties();
            props.SetFontProvider(fontProvider);
            ConvertToPdfAndCompare("unseparableSequence", SOURCE_FOLDER, DESTINATION_FOLDER, false, props);
        }
    }
}
