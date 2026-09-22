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
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Verticaltext {
    [NUnit.Framework.Category("IntegrationTest")]
    public class VerticalTextCornerCasesTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/verticaltext/VerticalTextCornerCasesTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/verticaltext/VerticalTextCornerCasesTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerCombiningMarkAloneTest() {
            ConvertToPdfAndCompare("vertCornerCombiningMarkAlone", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerExtremeEverythingAtOnceTest() {
            ConvertToPdfAndCompare("vertCornerExtremeEverythingAtOnce", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerGeneratedContentPseudoElementTest() {
            ConvertToPdfAndCompare("vertCornerGeneratedContentPseudoElement", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerHugeBorderRadiusTest() {
            ConvertToPdfAndCompare("vertCornerHugeBorderRadius", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerHugeFontTinyBoxTest() {
            ConvertToPdfAndCompare("vertCornerHugeFontTinyBox", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerHugeLineHeightTest() {
            ConvertToPdfAndCompare("vertCornerHugeLineHeight", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerHugeMarginAllSidesTest() {
            ConvertToPdfAndCompare("vertCornerHugeMarginAllSides", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerHugeMarginOneSideAsymmetricTest() {
            ConvertToPdfAndCompare("vertCornerHugeMarginOneSideAsymmetric", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerHugeNegativeMarginTest() {
            ConvertToPdfAndCompare("vertCornerHugeNegativeMargin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerExtremeMarginPageEdgeOverflowTest() {
            ConvertToPdfAndCompare("vertCornerExtremeMarginPageEdgeOverflow", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerHugePaddingTest() {
            ConvertToPdfAndCompare("vertCornerHugePadding", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerIdeographicSpaceAloneTest() {
            ConvertToPdfAndCompare("vertCornerIdeographicSpaceAlone", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerMultiPageBackgroundColorTest() {
            ConvertToPdfAndCompare("vertCornerMultiPageBackgroundColor", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerMultiPageBackgroundColorDecorationCloneTest() {
            ConvertToPdfAndCompare("vertCornerMultiPageBackgroundColorDecorationClone", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerMultiPageBackgroundColorDecorationSliceTest() {
            ConvertToPdfAndCompare("vertCornerMultiPageBackgroundColorDecorationSlice", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerMultiPageBorderColorTest() {
            ConvertToPdfAndCompare("vertCornerMultiPageBorderColor", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerMultiPageBoxShadowTest() {
            // TODO DEVSIX-4384 box-shadow is not supported
            ConvertToPdfAndCompare("vertCornerMultiPageBoxShadow", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerMultiPageBreakInsideAvoidTest() {
            ConvertToPdfAndCompare("vertCornerMultiPageBreakInsideAvoid", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        //TODO DEVSIX-10180 Support text rise in html mode for vertical text
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.WIDOWS_CONSTRAINT_VIOLATED)]
        public virtual void VertCornerMultiPageOrphansWidowsTest() {
            ConvertToPdfAndCompare("vertCornerMultiPageOrphansWidows", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerMultiPageOutlineTest() {
            ConvertToPdfAndCompare("vertCornerMultiPageOutline", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerMultiPageUnbreakableWordWithBackgroundTest() {
            ConvertToPdfAndCompare("vertCornerMultiPageUnbreakableWordWithBackground", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerNearInvisibleOpacityTest() {
            ConvertToPdfAndCompare("vertCornerNearInvisibleOpacity", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerPerfectFitSingleCharacterTest() {
            ConvertToPdfAndCompare("vertCornerPerfectFitSingleCharacter", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerRootElementVerticalTest() {
            ConvertToPdfAndCompare("vertCornerRootElementVertical", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerSingleCharacterHugeLetterSpacingTest() {
            ConvertToPdfAndCompare("vertCornerSingleCharacterHugeLetterSpacing", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerSingleSpaceCharacterTest() {
            ConvertToPdfAndCompare("vertCornerSingleSpaceCharacter", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerTinyFontSizeTest() {
            ConvertToPdfAndCompare("vertCornerTinyFontSize", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerTinyLineHeightTest() {
            //TODO DEVSIX-10180 Support text rise in html mode for vertical text
            ConvertToPdfAndCompare("vertCornerTinyLineHeight", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerUnusualAbsoluteUnitsTest() {
            ConvertToPdfAndCompare("vertCornerUnusualAbsoluteUnits", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertCornerZeroFontSizeTest() {
            ConvertToPdfAndCompare("vertCornerZeroFontSize", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
