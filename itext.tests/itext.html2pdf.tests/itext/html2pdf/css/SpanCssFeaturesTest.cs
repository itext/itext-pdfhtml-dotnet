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
    public class SpanCssFeaturesTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/SpanCssFeaturesTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/SpanCssFeaturesTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanBackgroundGradientParentAndChildTest() {
            ConvertToPdfAndCompare("spanBackgroundGradientParentAndChild", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanBackgroundTranslucentOverlapTest() {
            ConvertToPdfAndCompare("spanBackgroundTranslucentOverlap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanBorderCurrentColorPerSpanTest() {
            ConvertToPdfAndCompare("spanBorderCurrentColorPerSpan", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanBorderRadiusSizesParentAndChildTest() {
            ConvertToPdfAndCompare("spanBorderRadiusSizesParentAndChild", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanBorderStylesPerSideMixTest() {
            ConvertToPdfAndCompare("spanBorderStylesPerSideMix", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanBrInsideNestedBorderedSpansTest() {
            ConvertToPdfAndCompare("spanBrInsideNestedBorderedSpans", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 2)]
        public virtual void SpanBreakOpportunitiesAtSpanEdgesTest() {
            ConvertToPdfAndCompare("spanBreakOpportunitiesAtSpanEdges", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanCascadeSpecificityParentAndChildTest() {
            ConvertToPdfAndCompare("spanCascadeSpecificityParentAndChild", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.DEFAULT_VALUE_OF_CSS_PROPERTY_UNKNOWN
            )]
        public virtual void SpanCssWideKeywordsOnNonInheritedPropertiesTest() {
            ConvertToPdfAndCompare("spanCssWideKeywordsOnNonInheritedProperties", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanDecorationAcrossChildPaddingAndBorderTest() {
            ConvertToPdfAndCompare("spanDecorationAcrossChildPaddingAndBorder", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanDecorationLargerChildFontSizeTest() {
            ConvertToPdfAndCompare("spanDecorationLargerChildFontSize", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanDecorationOnBlockChildOfInlineParentTest() {
            ConvertToPdfAndCompare("spanDecorationOnBlockChildOfInlineParent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanDecorationPropagationCannotBeUndoneTest() {
            ConvertToPdfAndCompare("spanDecorationPropagationCannotBeUndone", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanDecorationStyleAndThicknessPerLevelTest() {
            ConvertToPdfAndCompare("spanDecorationStyleAndThicknessPerLevel", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanDecorationThicknessAndOffsetInEmTest() {
            ConvertToPdfAndCompare("spanDecorationThicknessAndOffsetInEm", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanDecorationUnderlineOffsetAndPositionTest() {
            ConvertToPdfAndCompare("spanDecorationUnderlineOffsetAndPosition", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanDecorationWithRelativeAndRaisedChildTest() {
            ConvertToPdfAndCompare("spanDecorationWithRelativeAndRaisedChild", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanDecorationWithSpacingAndWhitespaceTest() {
            ConvertToPdfAndCompare("spanDecorationWithSpacingAndWhitespace", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanDisplayBlockNestedMarginCollapseTest() {
            ConvertToPdfAndCompare("spanDisplayBlockNestedMarginCollapse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanDisplayContentsOnSpanTest() {
            ConvertToPdfAndCompare("spanDisplayContentsOnSpan", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanDisplayFlexInBorderedInlineParentTest() {
            ConvertToPdfAndCompare("spanDisplayFlexInBorderedInlineParent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanDisplayFlexWrapAndGapsTest() {
            ConvertToPdfAndCompare("spanDisplayFlexWrapAndGaps", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanDisplayInlineBlockChildMarginsTest() {
            ConvertToPdfAndCompare("spanDisplayInlineBlockChildMargins", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanDisplayInlineFlexBaselineAlignmentTest() {
            ConvertToPdfAndCompare("spanDisplayInlineFlexBaselineAlignment", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanEmptySpanOccupiesInlineAreaTest() {
            ConvertToPdfAndCompare("spanEmptySpanOccupiesInlineArea", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanFontSizeRelativeCompoundingTest() {
            ConvertToPdfAndCompare("spanFontSizeRelativeCompounding", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanInheritedAndNonInheritedPropertyMixTest() {
            ConvertToPdfAndCompare("spanInheritedAndNonInheritedPropertyMix", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanLetterWordSpacingComputedValueInheritedTest() {
            ConvertToPdfAndCompare("spanLetterWordSpacingComputedValueInherited", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanLineBoxHeightMixedChildrenTest() {
            ConvertToPdfAndCompare("spanLineBoxHeightMixedChildren", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanMarginCollapseBarriersTest() {
            ConvertToPdfAndCompare("spanMarginCollapseBarriers", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanOneSidedPaddingAndBorderMixTest() {
            ConvertToPdfAndCompare("spanOneSidedPaddingAndBorderMix", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanOpacityMultipliesAndIsNotInheritedTest() {
            ConvertToPdfAndCompare("spanOpacityMultipliesAndIsNotInherited", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.MARGIN_VALUE_IN_PERCENT_NOT_SUPPORTED, Count = 2)]
        [LogMessage(Html2PdfLogMessageConstant.PADDING_VALUE_IN_PERCENT_NOT_SUPPORTED, Count = 8)]
        public virtual void SpanPercentAndEmPaddingResolutionTest() {
            ConvertToPdfAndCompare("spanPercentAndEmPaddingResolution", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanPositionRelativeNestedTest() {
            ConvertToPdfAndCompare("spanPositionRelativeNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanTextShadowInheritedAndOverriddenTest() {
            ConvertToPdfAndCompare("spanTextShadowInheritedAndOverridden", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanTextTransformInheritedAndOverriddenTest() {
            ConvertToPdfAndCompare("spanTextTransformInheritedAndOverridden", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanVerticalAlignKeywordsAndNestingTest() {
            ConvertToPdfAndCompare("spanVerticalAlignKeywordsAndNesting", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanVerticalPaddingBorderDoNotAffectLineBoxTest() {
            ConvertToPdfAndCompare("spanVerticalPaddingBorderDoNotAffectLineBox", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanVisibilityParentAndChildTest() {
            ConvertToPdfAndCompare("spanVisibilityParentAndChild", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanWrappedParentWithMarginsAndPaddingFragmentsTest() {
            ConvertToPdfAndCompare("spanWrappedParentWithMarginsAndPaddingFragments", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }
    }
}
