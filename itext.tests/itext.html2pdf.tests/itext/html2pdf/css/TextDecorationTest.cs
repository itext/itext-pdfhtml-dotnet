/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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
    public class TextDecorationTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/TextDecorationTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/TextDecorationTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextDecoration01Test() {
            ConvertToPdfAndCompare("textDecorationTest01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextDecoration02Test() {
            ConvertToPdfAndCompare("textDecorationTest02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextDecoration03Test() {
            ConvertToPdfAndCompare("textDecorationTest03", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //Text decoration property is in defaults.css for a[href], should be replaced by css.
        [NUnit.Framework.Test]
        public virtual void TextDecoration04Test() {
            ConvertToPdfAndCompare("textDecorationTest04", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextDecoration05Test() {
            // TODO DEVSIX-2532
            ConvertToPdfAndCompare("textDecorationTest05", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationShorthandAllValuesTest() {
            //TODO update after DEVSIX-4063 is closed
            ConvertToPdfAndCompare("textDecorationShorthandAllValues", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationShorthandOneValueTest() {
            ConvertToPdfAndCompare("textDecorationShorthandOneValue", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationShorthandTwoValuesTest() {
            //TODO update after DEVSIX-4063 is closed
            ConvertToPdfAndCompare("textDecorationShorthandTwoValues", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationWithChildElementTest() {
            ConvertToPdfAndCompare("textDecorationWithChildElement", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        //TODO: DEVSIX-4201
        [LogMessage(Html2PdfLogMessageConstant.HSL_COLOR_NOT_SUPPORTED)]
        public virtual void TextDecorationColorTest() {
            ConvertToPdfAndCompare("textDecorationColor", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationColorWithTransparencyTest() {
            ConvertToPdfAndCompare("textDecorationColorWithTransparency", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationLineTest() {
            ConvertToPdfAndCompare("textDecorationLine", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationLineNoneAndUnderlineTogetherTest() {
            ConvertToPdfAndCompare("textDecorationLineNoneAndUnderlineTogether", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationStyleTest() {
            //TODO update after DEVSIX-4063 is closed
            ConvertToPdfAndCompare("textDecorationStyle", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ShorthandAndSpecificTextDecorPropsTest() {
            //TODO update after DEVSIX-4063 is closed
            ConvertToPdfAndCompare("shorthandAndSpecificTextDecorProps", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CombinationOfLinesInTextDecorationTest() {
            ConvertToPdfAndCompare("combinationOfLinesInTextDecoration", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationColorEffectOnNestedElements01Test() {
            ConvertToPdfAndCompare("textDecorationColorEffectOnNestedElements01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationColorEffectOnNestedElements02Test() {
            ConvertToPdfAndCompare("textDecorationColorEffectOnNestedElements02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationColorEffectOnNestedElements03Test() {
            ConvertToPdfAndCompare("textDecorationColorEffectOnNestedElements03", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationColorEffectOnNestedElements04Test() {
            ConvertToPdfAndCompare("textDecorationColorEffectOnNestedElements04", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationNoneOnNestedElementsTest() {
            ConvertToPdfAndCompare("textDecorationNoneOnNestedElements", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationWithDisplayInlineBlockTest() {
            ConvertToPdfAndCompare("textDecorationWithDisplayInlineBlock", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationInNodeStyleAttributeVsStyleTest() {
            ConvertToPdfAndCompare("textDecorationInNodeStyleAttributeVsStyle", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
