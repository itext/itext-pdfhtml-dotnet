using System;
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class VerticalTextMinMaxWidthTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/VerticalTextMinMaxWidthTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/VerticalTextMinMaxWidthTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxTableAutoLayoutTest() {
            ConvertToPdfAndCompare("vertMinMaxTableAutoLayout", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxTableExplicitCellMinMaxTest() {
            ConvertToPdfAndCompare("vertMinMaxTableExplicitCellMinMax", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxTableColgroupWidthsTest() {
            ConvertToPdfAndCompare("vertMinMaxTableColgroupWidths", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxTableFixedLayoutTest() {
            ConvertToPdfAndCompare("vertMinMaxTableFixedLayout", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxTableDisplayTableCellTest() {
            ConvertToPdfAndCompare("vertMinMaxTableDisplayTableCell", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxTableColspanVerticalTest() {
            ConvertToPdfAndCompare("vertMinMaxTableColspanVertical", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxTableRowspanVerticalTest() {
            ConvertToPdfAndCompare("vertMinMaxTableRowspanVertical", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxTableMultipleVerticalCellsSameRowTest() {
            ConvertToPdfAndCompare("vertMinMaxTableMultipleVerticalCellsSameRow", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxTableAllVerticalCellsTest() {
            ConvertToPdfAndCompare("vertMinMaxTableAllVerticalCells", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxTableNowrapCellTest() {
            ConvertToPdfAndCompare("vertMinMaxTableNowrapCell", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxFlexNestedInTableCellTest() {
            ConvertToPdfAndCompare("vertMinMaxFlexNestedInTableCell", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxTableNestedInTableCellTest() {
            ConvertToPdfAndCompare("vertMinMaxTableNestedInTableCell", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxFlexBasisAutoMinWidthAutoTest() {
            ConvertToPdfAndCompare("vertMinMaxFlexBasisAutoMinWidthAuto", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxFlexMinWidthZeroOverrideTest() {
            ConvertToPdfAndCompare("vertMinMaxFlexMinWidthZeroOverride", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxFlexShrinkBelowContentTest() {
            ConvertToPdfAndCompare("vertMinMaxFlexShrinkBelowContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxFlexGrowWithMaxWidthTest() {
            ConvertToPdfAndCompare("vertMinMaxFlexGrowWithMaxWidth", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxFlexBasisZeroPercentTest() {
            ConvertToPdfAndCompare("vertMinMaxFlexBasisZeroPercent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxFlexProportionalGrowTest() {
            ConvertToPdfAndCompare("vertMinMaxFlexProportionalGrow", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxFlexStretchCrossAxisTest() {
            ConvertToPdfAndCompare("vertMinMaxFlexStretchCrossAxis", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.FONT_PROPERTY_MUST_BE_PDF_FONT_OBJECT)]
        public virtual void VertMinMaxFlexColumnDirectionTest() {
            ConvertToPdfAndCompare("vertMinMaxFlexColumnDirection", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxFlexWrapMultipleItemsTest() {
            ConvertToPdfAndCompare("vertMinMaxFlexWrapMultipleItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxFlexNestedContainersTest() {
            ConvertToPdfAndCompare("vertMinMaxFlexNestedContainers", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxFlexLogicalBlockSizeTest() {
            ConvertToPdfAndCompare("vertMinMaxFlexLogicalBlockSize", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxTableNestedInFlexItemTest() {
            ConvertToPdfAndCompare("vertMinMaxTableNestedInFlexItem", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxInlineBlockWrapperAutoWidthTest() {
            ConvertToPdfAndCompare("vertMinMaxInlineBlockWrapperAutoWidth", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxInlineBlockWrapperExplicitMinMaxTest() {
            ConvertToPdfAndCompare("vertMinMaxInlineBlockWrapperExplicitMinMax", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxInlineBlockElementItselfTest() {
            ConvertToPdfAndCompare("vertMinMaxInlineBlockElementItself", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxInlineBlockPercentageWidthDefiniteParentTest() {
            ConvertToPdfAndCompare("vertMinMaxInlineBlockPercentageWidthDefiniteParent", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxInlineBlockDifferentFontSizesTest() {
            ConvertToPdfAndCompare("vertMinMaxInlineBlockDifferentFontSizes", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxInlineBlockMultipleSideBySideTest() {
            ConvertToPdfAndCompare("vertMinMaxInlineBlockMultipleSideBySide", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxInlineBlockInTableCellTest() {
            ConvertToPdfAndCompare("vertMinMaxInlineBlockInTableCell", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxInlineBlockOverflowHiddenTest() {
            ConvertToPdfAndCompare("vertMinMaxInlineBlockOverflowHidden", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxInlineBlockDoubleNestedWrappersTest() {
            ConvertToPdfAndCompare("vertMinMaxInlineBlockDoubleNestedWrappers", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxGridAutoTracksTest() {
            ConvertToPdfAndCompare("vertMinMaxGridAutoTracks", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxGridMinmaxFunctionTest() {
            ConvertToPdfAndCompare("vertMinMaxGridMinmaxFunction", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxGridMinWidthZeroTest() {
            ConvertToPdfAndCompare("vertMinMaxGridMinWidthZero", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxGridNestedInGridItemTest() {
            ConvertToPdfAndCompare("vertMinMaxGridNestedInGridItem", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBoxSizingContentBoxTest() {
            ConvertToPdfAndCompare("vertMinMaxBoxSizingContentBox", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBoxSizingBorderBoxTest() {
            ConvertToPdfAndCompare("vertMinMaxBoxSizingBorderBox", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakLogicalSizeInTableTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakLogicalSizeInTable", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakTableConflictingColumnWidthsAcrossRowsTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakTableConflictingColumnWidthsAcrossRows", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ELEMENT_DOES_NOT_FIT_CURRENT_AREA)]
        public virtual void VertMinMaxBreakTableFixedTinyWidthUnbreakableTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakTableFixedTinyWidthUnbreakable", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ELEMENT_DOES_NOT_FIT_CURRENT_AREA)]
        public virtual void VertMinMaxBreakTableZeroWidthCellOverflowHiddenTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakTableZeroWidthCellOverflowHidden", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakTableEverythingConflictsAtOnceTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakTableEverythingConflictsAtOnce", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakZeroWidthFlexBasisTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakZeroWidthFlexBasis", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakUnbreakableMinContentTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakUnbreakableMinContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakFlexShrinkZeroOverflowTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakFlexShrinkZeroOverflow", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakFlexMinWidthOverridesShrinkTargetTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakFlexMinWidthOverridesShrinkTarget", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void VertMinMaxBreakFlexInvalidNegativeBasisTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakFlexInvalidNegativeBasis", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakFlexNegativeMarginTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakFlexNegativeMargin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakPercentageOnIndefiniteAncestorTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakPercentageOnIndefiniteAncestor", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakNestedConflictingConstraintsTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakNestedConflictingConstraints", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakInlineBlockZeroWidthNowrapAncestorTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakInlineBlockZeroWidthNowrapAncestor", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakInlineBlockDoubleNowrapTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakInlineBlockDoubleNowrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakInlineBlockNegativePaddingTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakInlineBlockNegativePadding", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakInlineBlockFloatOverridesDisplayTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakInlineBlockFloatOverridesDisplay", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakGridMinmaxZeroToFrTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakGridMinmaxZeroToFr", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakGridAutoFillMinmaxTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakGridAutoFillMinmax", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakGridMinmaxInvertedTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakGridMinmaxInverted", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakConflictingMinGreaterThanMaxTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakConflictingMinGreaterThanMax", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakMinExceedsContainerTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakMinExceedsContainer", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakEmptyContentWithConstraintsTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakEmptyContentWithConstraints", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakExtremeLargeMinWidthTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakExtremeLargeMinWidth", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertMinMaxBreakExtremeTinyMaxWidthTest() {
            ConvertToPdfAndCompare("vertMinMaxBreakExtremeTinyMaxWidth", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
