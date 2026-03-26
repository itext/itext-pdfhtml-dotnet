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

namespace iText.Html2pdf.Css.Flex {
    [NUnit.Framework.Category("IntegrationTest")]
    public class FlexGapTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/flex/FlexGapTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/flex/FlexGapTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapDecimalsTest() {
            ConvertToPdfAndCompare("gapDecimals", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapDecimalsDoubleDigitsTest() {
            ConvertToPdfAndCompare("gapDecimalsDoubleDigits", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapEmTest() {
            ConvertToPdfAndCompare("gapEm", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapGlobalTest() {
            // TODO DEVSIX-9472 Support global values for column/row-gap property
            ConvertToPdfAndCompare("gapGlobal", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapInheritTest() {
            ConvertToPdfAndCompare("gapInherit", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapInitialTest() {
            ConvertToPdfAndCompare("gapInitial", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void Gap3vminTest() {
            // TODO DEVSIX-9472 Support vmin/vmax values for column/row-gap property
            ConvertToPdfAndCompare("gap3vmin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void GapCalcTest() {
            // TODO DEVSIX-9472 Support calc values for column/row-gap property
            ConvertToPdfAndCompare("gapCalc", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapZeroTest() {
            ConvertToPdfAndCompare("gapZero", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapZeroColDecimalsTest() {
            ConvertToPdfAndCompare("gapZeroColDecimals", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapZeroRowDecimalsTest() {
            ConvertToPdfAndCompare("gapZeroRowDecimals", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapZeroWidthHeightTest() {
            ConvertToPdfAndCompare("gapZeroWidthHeight", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapPercentageTest() {
            // TODO DEVSIX-9472 Support percentage values for column/row-gap property
            ConvertToPdfAndCompare("gapPercentage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapPhysUnitsTest() {
            ConvertToPdfAndCompare("gapPhysUnits", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapRemTest() {
            ConvertToPdfAndCompare("gapRem", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void GapRevertTest() {
            // TODO DEVSIX-9472 Support revert value for column/row-gap property
            ConvertToPdfAndCompare("gapRevert", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void GapRevertLayerTest() {
            // TODO DEVSIX-9472 Support revert-layer value for column/row-gap property
            ConvertToPdfAndCompare("gapRevertLayer", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapUnsetTest() {
            ConvertToPdfAndCompare("gapUnset", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapNormalTest() {
            ConvertToPdfAndCompare("gapNormal", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapNestedFlexContainerTest() {
            ConvertToPdfAndCompare("gapNestedFlexContainer", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapNestedFlexContainerColumnTest() {
            ConvertToPdfAndCompare("gapNestedFlexContainerColumn", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapAlignContentTest() {
            ConvertToPdfAndCompare("gapAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapAlignContentRowRevDirTest() {
            ConvertToPdfAndCompare("gapAlignContentRowRevDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapAlignContentColumnDirTest() {
            ConvertToPdfAndCompare("gapAlignContentColumnDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapAlignContentColumnDirPageSplitTest() {
            ConvertToPdfAndCompare("gapAlignContentColumnDirPageSplit", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapAlignContentColumnDirSmallHeightTest() {
            // TODO DEVSIX-9559 Fix align-content in case free space is negative
            ConvertToPdfAndCompare("gapAlignContentColumnDirSmallHeight", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapAlignContentColumnDirSmallHeightWrapRevTest() {
            // TODO DEVSIX-9559 Fix align-content in case free space is negative
            ConvertToPdfAndCompare("gapAlignContentColumnDirSmallHeightWrapRev", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapAlignContentColumnRevDirTest() {
            ConvertToPdfAndCompare("gapAlignContentColumnRevDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapAlignContentWrapRevTest() {
            ConvertToPdfAndCompare("gapAlignContentWrapRev", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void GapAlignItemsTest() {
            ConvertToPdfAndCompare("gapAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void GapAlignSelfTest() {
            ConvertToPdfAndCompare("gapAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapGrowTest() {
            ConvertToPdfAndCompare("gapGrow", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapJustifyContentTest() {
            ConvertToPdfAndCompare("gapJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapJustifyContentRowRevTest() {
            ConvertToPdfAndCompare("gapJustifyContentRowRev", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapJustifyContentColumnTest() {
            ConvertToPdfAndCompare("gapJustifyContentColumn", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapJustifyContentColumnRevTest() {
            ConvertToPdfAndCompare("gapJustifyContentColumnRev", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapJustifyContentWrapRevTest() {
            ConvertToPdfAndCompare("gapJustifyContentWrapRev", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapLongTest() {
            ConvertToPdfAndCompare("gapLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapLongMarginTest() {
            ConvertToPdfAndCompare("gapLongMargin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapLongMixedPropertiesTest() {
            ConvertToPdfAndCompare("gapLongMixedProperties", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapLongPaddingTest() {
            ConvertToPdfAndCompare("gapLongPadding", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapMarginLongTest() {
            ConvertToPdfAndCompare("gapMarginLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapMixedTest() {
            // TODO DEVSIX-9472 Support percentage values for column/row-gap property
            ConvertToPdfAndCompare("gapMixed", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapMixedSizesTest() {
            ConvertToPdfAndCompare("gapMixedSizes", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapNegativeMarginsTest() {
            ConvertToPdfAndCompare("gapNegativeMargins", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapPaddingLongTest() {
            ConvertToPdfAndCompare("gapPaddingLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapShrinkTest() {
            ConvertToPdfAndCompare("gapShrink", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapWrapTest() {
            ConvertToPdfAndCompare("gapWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void GapFlexDirWrapTest() {
            ConvertToPdfAndCompare("gapFlexDirWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NestedGapTest() {
            ConvertToPdfAndCompare("nestedGap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void ColGap3vminTest() {
            // TODO DEVSIX-9472 Support vmin/vmax values for column/row-gap property
            ConvertToPdfAndCompare("colGap3vmin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void ColGapCalcTest() {
            // TODO DEVSIX-9472 Support calc values for column/row-gap property
            ConvertToPdfAndCompare("colGapCalc", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColGapDecimalsTest() {
            ConvertToPdfAndCompare("colGapDecimals", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColGapEmTest() {
            ConvertToPdfAndCompare("colGapEm", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColGapPercentageTest() {
            // TODO DEVSIX-9472 Support percentage values for column/row-gap property
            ConvertToPdfAndCompare("colGapPercentage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColGapPhysUnitsTest() {
            ConvertToPdfAndCompare("colGapPhysUnits", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColGapRemTest() {
            ConvertToPdfAndCompare("colGapRem", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColGapAlignContentTest() {
            ConvertToPdfAndCompare("colGapAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void ColGapAlignItemsTest() {
            ConvertToPdfAndCompare("colGapAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void ColGapAlignSelfTest() {
            ConvertToPdfAndCompare("colGapAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColGapJustifyContentTest() {
            ConvertToPdfAndCompare("colGapJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColGapLargeTest() {
            ConvertToPdfAndCompare("colGapLarge", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColGapMarginTest() {
            ConvertToPdfAndCompare("colGapMargin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColGapMixedSizesTest() {
            ConvertToPdfAndCompare("colGapMixedSizes", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColGapNegativeMarginsTest() {
            ConvertToPdfAndCompare("colGapNegativeMargins", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColGapPaddingTest() {
            ConvertToPdfAndCompare("colGapPadding", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColGapTooBigTest() {
            ConvertToPdfAndCompare("colGapTooBig", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColGapWrapTest() {
            ConvertToPdfAndCompare("colGapWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void RowGap3vminTest() {
            // TODO DEVSIX-9472 Support vmin/vmax values for column/row-gap property
            ConvertToPdfAndCompare("rowGap3vmin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void RowGapCalcTest() {
            // TODO DEVSIX-9472 Support calc values for column/row-gap property
            ConvertToPdfAndCompare("rowGapCalc", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapDecimalsTest() {
            ConvertToPdfAndCompare("rowGapDecimals", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapEmTest() {
            ConvertToPdfAndCompare("rowGapEm", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapPercentageTest() {
            // TODO DEVSIX-9472 Support percentage values for column/row-gap property
            ConvertToPdfAndCompare("rowGapPercentage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapPhysUnitsTest() {
            ConvertToPdfAndCompare("rowGapPhysUnits", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapRemTest() {
            ConvertToPdfAndCompare("rowGapRem", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapTooBigTest() {
            ConvertToPdfAndCompare("rowGapTooBig", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapWrapTest() {
            ConvertToPdfAndCompare("rowGapWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapAlignContentTest() {
            ConvertToPdfAndCompare("rowGapAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void RowGapAlignItemsTest() {
            ConvertToPdfAndCompare("rowGapAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void RowGapAlignSelfTest() {
            ConvertToPdfAndCompare("rowGapAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapJustifyContentTest() {
            ConvertToPdfAndCompare("rowGapJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapLargeTest() {
            ConvertToPdfAndCompare("rowGapLarge", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapMarginTest() {
            ConvertToPdfAndCompare("rowGapMargin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapMixedSizesTest() {
            ConvertToPdfAndCompare("rowGapMixedSizes", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapNegativeMarginsTest() {
            ConvertToPdfAndCompare("rowGapNegativeMargins", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RowGapPaddingTest() {
            ConvertToPdfAndCompare("rowGapPadding", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
