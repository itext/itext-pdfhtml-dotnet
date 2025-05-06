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
    public class PseudoElementsTest : ExtendedHtmlConversionITextTest {
        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/PseudoElementsTest/";

        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/PseudoElementsTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PseudoContentWithWidthAndHeightTest() {
            ConvertToPdfAndCompare("pseudoContentWithWidthAndHeightTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PseudoContentWithPercentWidthAndHeightTest() {
            ConvertToPdfAndCompare("pseudoContentWithPercentWidthAndHeightTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PseudoContentDisplayNoneTest() {
            ConvertToPdfAndCompare("pseudoContentDisplayNoneTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PseudoContentWithAutoWidthAndHeightTest() {
            ConvertToPdfAndCompare("pseudoContentWithAutoWidthAndHeightTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssWithListOfSelectorsInPseudoClassTest() {
            ConvertToPdfAndCompare("pseudoClassTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void InvalidPseudoClassWithCommaSelectorTest() {
            ConvertToPdfAndCompare("invalidPseudoClassWithCommaSelector", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DifferentArgumentsInNotClassTest() {
            ConvertToPdfAndCompare("invalidPseudoClassWithCommaSelector", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NotFirstPseudoClassTest() {
            ConvertToPdfAndCompare("notFirstPseudoClassTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AddingNewContentInPseudoClassTest() {
            ConvertToPdfAndCompare("addingNewContentInPseudoClassTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ComplexPseudoClassTest() {
            ConvertToPdfAndCompare("complexPseudoClassTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest01() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest02() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest03() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest03", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest04() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest04", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest05() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest05", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest06() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest06", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest07() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest07", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest08() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest08", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 2)]
        public virtual void BeforeAfterPseudoTest09() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest09", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest10() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest10", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest11() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest11", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest12() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest12", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest13() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest13", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BeforeAfterPseudoTest14() {
            ConvertToPdfAndCompare("beforeAfterPseudoTest14", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsBeforeAfterPseudo01() {
            ConvertToPdfAndCompare("collapsingMarginsBeforeAfterPseudo01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsBeforeAfterPseudo02() {
            ConvertToPdfAndCompare("collapsingMarginsBeforeAfterPseudo02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsBeforeAfterPseudo03() {
            ConvertToPdfAndCompare("collapsingMarginsBeforeAfterPseudo03", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsBeforeAfterPseudo04() {
            ConvertToPdfAndCompare("collapsingMarginsBeforeAfterPseudo04", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsBeforeAfterPseudo05() {
            ConvertToPdfAndCompare("collapsingMarginsBeforeAfterPseudo05", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsBeforeAfterPseudo06() {
            ConvertToPdfAndCompare("collapsingMarginsBeforeAfterPseudo06", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EscapedStringTest01() {
            ConvertToPdfAndCompare("escapedStringTest01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EscapedStringTest02() {
            ConvertToPdfAndCompare("escapedStringTest02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EscapedStringTest03() {
            ConvertToPdfAndCompare("escapedStringTest03", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EscapedStringTest04() {
            ConvertToPdfAndCompare("escapedStringTest04", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EscapedStringTest05() {
            ConvertToPdfAndCompare("escapedStringTest05", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.CONTENT_PROPERTY_INVALID, Count = 5)]
        public virtual void AttrTest01() {
            ConvertToPdfAndCompare("attrTest01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.CONTENT_PROPERTY_INVALID, Count = 3)]
        public virtual void AttrTest02() {
            ConvertToPdfAndCompare("attrTest02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStillShownPseudoTest01() {
            ConvertToPdfAndCompare("emptyStillShownPseudoTest01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStillShownPseudoTest02() {
            // TODO DEVSIX-1393 position property is not supported for inline elements.
            ConvertToPdfAndCompare("emptyStillShownPseudoTest02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStillShownPseudoTest03() {
            ConvertToPdfAndCompare("emptyStillShownPseudoTest03", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStillShownPseudoTest04() {
            // TODO DEVSIX-1393 position property is not supported for inline elements.
            ConvertToPdfAndCompare("emptyStillShownPseudoTest04", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStillShownPseudoTest05() {
            // TODO DEVSIX-1393 position property is not supported for inline elements.
            ConvertToPdfAndCompare("emptyStillShownPseudoTest05", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStillShownPseudoTest06() {
            ConvertToPdfAndCompare("emptyStillShownPseudoTest06", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStillShownPseudoTest07() {
            ConvertToPdfAndCompare("emptyStillShownPseudoTest07", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStillShownPseudoTest08() {
            ConvertToPdfAndCompare("emptyStillShownPseudoTest08", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyStillShownPseudoTest09() {
            ConvertToPdfAndCompare("emptyStillShownPseudoTest09", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PseudoDisplayTable01Test() {
            ConvertToPdfAndCompare("pseudoDisplayTable01", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PseudoDisplayTable02Test() {
            ConvertToPdfAndCompare("pseudoDisplayTable02", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImgPseudoBeforeDivTest() {
            ConvertToPdfAndCompare("imgPseudoBeforeDiv", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImgPseudoBeforeDivDisplayBlockTest() {
            ConvertToPdfAndCompare("imgPseudoBeforeDivDisplayBlock", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER)]
        public virtual void ImgPseudoBeforeImgTest() {
            ConvertToPdfAndCompare("imgPseudoBeforeImg", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImgPseudoBeforeWithTextTest() {
            ConvertToPdfAndCompare("imgPseudoBeforeWithText", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImgPseudoBeforeInSeveralDivsTest() {
            ConvertToPdfAndCompare("imgPseudoBeforeInSeveralDivs", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImgPseudoWithPageRuleTest() {
            ConvertToPdfAndCompare("imgPseudoWithPageRule", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NonNormalizedAfterBeforeTest() {
            ConvertToPdfAndCompare("nonNormalizedAfterBefore", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PseudoElementsWithMarginTest() {
            // TODO: update cmp file after DEVSIX-6192 will be fixed
            ConvertToPdfAndCompare("pseudoElementsWithMargin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
