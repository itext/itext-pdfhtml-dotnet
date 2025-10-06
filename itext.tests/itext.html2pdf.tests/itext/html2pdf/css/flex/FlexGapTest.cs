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
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void GapDecimalsTest() {
            ConvertToPdfAndCompare("gapDecimals", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void GapDecimalsDoubleDigitsTest() {
            ConvertToPdfAndCompare("gapDecimalsDoubleDigits", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void GapEmTest() {
            ConvertToPdfAndCompare("gapEm", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 12)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.DEFAULT_VALUE_OF_CSS_PROPERTY_UNKNOWN
            , Count = 2)]
        public virtual void GapGlobalTest() {
            ConvertToPdfAndCompare("gapGlobal", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 4)]
        public virtual void GapInheritTest() {
            ConvertToPdfAndCompare("gapInherit", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.DEFAULT_VALUE_OF_CSS_PROPERTY_UNKNOWN
            , Count = 2)]
        public virtual void GapInitialTest() {
            ConvertToPdfAndCompare("gapInitial", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void Gap3vminTest() {
            ConvertToPdfAndCompare("gap3vmin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void GapCalcTest() {
            ConvertToPdfAndCompare("gapCalc", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void GapZeroTest() {
            ConvertToPdfAndCompare("gapZero", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void GapZeroColDecimalsTest() {
            ConvertToPdfAndCompare("gapZeroColDecimals", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void GapZeroRowDecimalsTest() {
            ConvertToPdfAndCompare("gapZeroRowDecimals", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void GapZeroWidthHeightTest() {
            ConvertToPdfAndCompare("gapZeroWidthHeight", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void GapPercentageTest() {
            ConvertToPdfAndCompare("gapPercentage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void GapPhysUnitsTest() {
            ConvertToPdfAndCompare("gapPhysUnits", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void GapRemTest() {
            ConvertToPdfAndCompare("gapRem", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 4)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void GapRevertTest() {
            ConvertToPdfAndCompare("gapRevert", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void GapRevertLayerTest() {
            ConvertToPdfAndCompare("gapRevertLayer", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 4)]
        public virtual void GapUnsetTest() {
            ConvertToPdfAndCompare("gapUnset", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 12)]
        public virtual void GapAlignContentTest() {
            ConvertToPdfAndCompare("gapAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 9)]
        public virtual void GapAlignItemsTest() {
            ConvertToPdfAndCompare("gapAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 3)]
        public virtual void GapAlignSelfTest() {
            ConvertToPdfAndCompare("gapAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void GapGrowTest() {
            ConvertToPdfAndCompare("gapGrow", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 15)]
        public virtual void GapJustifyContentTest() {
            ConvertToPdfAndCompare("gapJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void GapLongTest() {
            ConvertToPdfAndCompare("gapLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void GapLongMarginTest() {
            ConvertToPdfAndCompare("gapLongMargin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 3)]
        public virtual void GapLongMixedPropertiesTest() {
            ConvertToPdfAndCompare("gapLongMixedProperties", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void GapLongPaddingTest() {
            ConvertToPdfAndCompare("gapLongPadding", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void GapMarginLongTest() {
            ConvertToPdfAndCompare("gapMarginLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 14)]
        public virtual void GapMixedTest() {
            ConvertToPdfAndCompare("gapMixed", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 8)]
        public virtual void GapMixedSizesTest() {
            ConvertToPdfAndCompare("gapMixedSizes", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void GapNegativeMarginsTest() {
            ConvertToPdfAndCompare("gapNegativeMargins", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void GapPaddingLongTest() {
            ConvertToPdfAndCompare("gapPaddingLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void GapShrinkTest() {
            ConvertToPdfAndCompare("gapShrink", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 6)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void GapWrapTest() {
            ConvertToPdfAndCompare("gapWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 4)]
        public virtual void NestedGapTest() {
            ConvertToPdfAndCompare("nestedGap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void ColGap3vminTest() {
            ConvertToPdfAndCompare("colGap3vmin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void ColGapCalcTest() {
            ConvertToPdfAndCompare("colGapCalc", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void ColGapDecimalsTest() {
            ConvertToPdfAndCompare("colGapDecimals", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void ColGapEmTest() {
            ConvertToPdfAndCompare("colGapEm", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void ColGapPercentageTest() {
            ConvertToPdfAndCompare("colGapPercentage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void ColGapPhysUnitsTest() {
            ConvertToPdfAndCompare("colGapPhysUnits", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void ColGapRemTest() {
            ConvertToPdfAndCompare("colGapRem", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 6)]
        public virtual void ColGapAlignContentTest() {
            ConvertToPdfAndCompare("colGapAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 5)]
        public virtual void ColGapAlignItemsTest() {
            ConvertToPdfAndCompare("colGapAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void ColGapAlignSelfTest() {
            ConvertToPdfAndCompare("colGapAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 9)]
        public virtual void ColGapJustifyContentTest() {
            ConvertToPdfAndCompare("colGapJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void ColGapLargeTest() {
            ConvertToPdfAndCompare("colGapLarge", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void ColGapMarginTest() {
            ConvertToPdfAndCompare("colGapMargin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 4)]
        public virtual void ColGapMixedSizesTest() {
            ConvertToPdfAndCompare("colGapMixedSizes", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void ColGapNegativeMarginsTest() {
            ConvertToPdfAndCompare("colGapNegativeMargins", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void ColGapPaddingTest() {
            ConvertToPdfAndCompare("colGapPadding", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void ColGapTooBigTest() {
            ConvertToPdfAndCompare("colGapTooBig", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 3)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void ColGapWrapTest() {
            ConvertToPdfAndCompare("colGapWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void RowGap3vminTest() {
            ConvertToPdfAndCompare("rowGap3vmin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void RowGapCalcTest() {
            ConvertToPdfAndCompare("rowGapCalc", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void RowGapDecimalsTest() {
            ConvertToPdfAndCompare("rowGapDecimals", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void RowGapEmTest() {
            ConvertToPdfAndCompare("rowGapEm", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void RowGapPercentageTest() {
            ConvertToPdfAndCompare("rowGapPercentage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void RowGapPhysUnitsTest() {
            ConvertToPdfAndCompare("rowGapPhysUnits", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void RowGapRemTest() {
            ConvertToPdfAndCompare("rowGapRem", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void RowGapTooBigTest() {
            ConvertToPdfAndCompare("rowGapTooBig", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 3)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void RowGapWrapTest() {
            ConvertToPdfAndCompare("rowGapWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 6)]
        public virtual void RowGapAlignContentTest() {
            ConvertToPdfAndCompare("rowGapAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 5)]
        public virtual void RowGapAlignItemsTest() {
            ConvertToPdfAndCompare("rowGapAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void RowGapAlignSelfTest() {
            ConvertToPdfAndCompare("rowGapAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 9)]
        public virtual void RowGapJustifyContentTest() {
            ConvertToPdfAndCompare("rowGapJustifyContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void RowGapLargeTest() {
            ConvertToPdfAndCompare("rowGapLarge", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void RowGapMarginTest() {
            ConvertToPdfAndCompare("rowGapMargin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 4)]
        public virtual void RowGapMixedSizesTest() {
            ConvertToPdfAndCompare("rowGapMixedSizes", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void RowGapNegativeMarginsTest() {
            ConvertToPdfAndCompare("rowGapNegativeMargins", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void RowGapPaddingTest() {
            ConvertToPdfAndCompare("rowGapPadding", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
