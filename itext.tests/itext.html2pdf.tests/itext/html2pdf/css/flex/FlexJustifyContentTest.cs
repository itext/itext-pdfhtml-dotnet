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
    public class FlexJustifyContentTest : ExtendedHtmlConversionITextTest {
        //TODO DEVSIX-5163: Update cmp files
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/flex/FlexJustifyContentTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/flex/FlexJustifyContentTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void SelfEndTest() {
            ConvertToPdfAndCompare("selfEnd", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 8)]
        public virtual void InheritFlexDirTest() {
            ConvertToPdfAndCompare("inheritFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 12)]
        public virtual void InheritSpaceValuesTest() {
            ConvertToPdfAndCompare("inheritSpaceValues", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 6)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void InheritWrapTest() {
            ConvertToPdfAndCompare("inheritWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void InitialSimpleTest() {
            ConvertToPdfAndCompare("initialSimple", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void InitialFlexDirTest() {
            ConvertToPdfAndCompare("initialFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void InitialWrapTest() {
            ConvertToPdfAndCompare("initialWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage("Cannot find pdfCalligraph module, which was implicitly required by one of the layout properties"
            , Count = 24)]
        public virtual void LeftValueTest() {
            ConvertToPdfAndCompare("leftValue", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 3)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 2)]
        public virtual void MultipleValuesTest() {
            ConvertToPdfAndCompare("otherValues", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 3)]
        public virtual void SimpleCombinedTest() {
            ConvertToPdfAndCompare("simpleCombined", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 4)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 2)]
        public virtual void MultipleValuesMarginAutoOverridesTest() {
            ConvertToPdfAndCompare("otherValuesMarginAutoOverrides", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StartEndCenterValuesAlignItemsTest() {
            ConvertToPdfAndCompare("startEndCenterValuesAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StartEndCenterValuesWrapTest() {
            ConvertToPdfAndCompare("startEndCenterValuesWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 7)]
        public virtual void StartEndCenterValuesNoWrapTest() {
            ConvertToPdfAndCompare("startEndCenterValuesNoWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StartEndCenterValuesWrapReverseTest() {
            ConvertToPdfAndCompare("startEndCenterValuesWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StartEndCenterValuesWrapAlignItemsTest() {
            ConvertToPdfAndCompare("startEndCenterValuesWrapAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CenterAlignContentTest() {
            ConvertToPdfAndCompare("centerAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CenterAlignItemsTest() {
            ConvertToPdfAndCompare("centerAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CenterRowWrapLongTest() {
            ConvertToPdfAndCompare("centerRowWrapLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StartColumnWrapLongTest() {
            ConvertToPdfAndCompare("startColumnWrapLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexEndColumnReverseWrapLongTest() {
            ConvertToPdfAndCompare("flexEndColumnReverseWrapLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexStartRowReverseWrapLongTest() {
            ConvertToPdfAndCompare("flexStartRowReverseWrapLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 5)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void RevertSimpleTest() {
            ConvertToPdfAndCompare("revertSimple", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 6)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 8)]
        public virtual void RevertFlexDirTest() {
            ConvertToPdfAndCompare("revertFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 5)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void RevertLayerTest() {
            ConvertToPdfAndCompare("revertLayer", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 6)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 8)]
        public virtual void RevertWrapTest() {
            ConvertToPdfAndCompare("revertWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 5)]
        public virtual void SafeCenterTest() {
            ConvertToPdfAndCompare("safeCenter", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 12)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 2)]
        public virtual void SafeCenterWrapTest() {
            ConvertToPdfAndCompare("safeCenterWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 5)]
        public virtual void UnsafeCenterTest() {
            ConvertToPdfAndCompare("unSafeCenter", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 15)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 3)]
        public virtual void UnsafeCenterWrapTest() {
            ConvertToPdfAndCompare("unsafeCenterWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void StretchTest() {
            ConvertToPdfAndCompare("stretch", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 8)]
        public virtual void StretchFlexDirTest() {
            ConvertToPdfAndCompare("stretchFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 6)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void StretchWrapTest() {
            ConvertToPdfAndCompare("stretchWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void StretchAlignItemsTest() {
            ConvertToPdfAndCompare("stretchAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StretchAlignContentTest() {
            ConvertToPdfAndCompare("stretchAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void StretchAlignSelfTest() {
            ConvertToPdfAndCompare("stretchAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void UnsetTest() {
            ConvertToPdfAndCompare("unset", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 8)]
        public virtual void UnsetFlexDirTest() {
            ConvertToPdfAndCompare("unsetFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 6)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void UnsetWrapTest() {
            ConvertToPdfAndCompare("unsetWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void SpaceAroundSimpleTest() {
            ConvertToPdfAndCompare("spaceAroundSimple", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 4)]
        public virtual void SpaceAroundFlexDirTest() {
            ConvertToPdfAndCompare("spaceAroundFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 1)]
        public virtual void SpaceAroundMarginAndAlignSelfTest() {
            ConvertToPdfAndCompare("spaceAroundMarginAndAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void SpaceAroundMarginAutoTest() {
            ConvertToPdfAndCompare("spaceAroundMarginAuto", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void SpaceAroundMarginAuto2Test() {
            ConvertToPdfAndCompare("spaceAroundMarginAuto2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void SpaceAroundMarginAutoOverrideTest() {
            ConvertToPdfAndCompare("spaceAroundMarginAutoOverride", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 6)]
        public virtual void SpaceAroundAlignItemsTest() {
            ConvertToPdfAndCompare("spaceAroundAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 6)]
        public virtual void SpaceAroundAlignContentTest() {
            ConvertToPdfAndCompare("spaceAroundAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void SpaceAroundAlignSelfTest() {
            ConvertToPdfAndCompare("spaceAroundAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void SpaceAroundWrapLongTest() {
            ConvertToPdfAndCompare("spaceAroundWrapLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void SpaceAroundColumnWrapLongTest() {
            ConvertToPdfAndCompare("spaceAroundColumnWrapLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void SpaceBetweenSimpleTest() {
            ConvertToPdfAndCompare("spaceBetweenSimple", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 4)]
        public virtual void SpaceBetweenFlexDirTest() {
            ConvertToPdfAndCompare("spaceBetweenFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void SpaceBetweenMarginAndAlignSelfTest() {
            ConvertToPdfAndCompare("spaceBetweenMarginAndAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void SpaceBetweenMarginAutoOverrideTest() {
            ConvertToPdfAndCompare("spaceBetweenMarginAutoOverride", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 4)]
        public virtual void SpaceBetweenAllMarginOptionsTest() {
            ConvertToPdfAndCompare("spaceBetweenAllMarginOptions", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 6)]
        public virtual void SpaceBetweenAlignItemsTest() {
            ConvertToPdfAndCompare("spaceBetweenAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 6)]
        public virtual void SpaceBetweenAlignContentTest() {
            ConvertToPdfAndCompare("spaceBetweenAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void SpaceBetweenAlignSelfTest() {
            ConvertToPdfAndCompare("spaceBetweenAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void SpaceBetweenColumnReverseWrapLongTest() {
            ConvertToPdfAndCompare("spaceBetweenColumnReverseWrapLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void SpaceEvenlySimpleTest() {
            ConvertToPdfAndCompare("spaceEvenlySimple", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 6)]
        public virtual void SpaceEvenlyAlignItemsTest() {
            ConvertToPdfAndCompare("spaceEvenlyAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 6)]
        public virtual void SpaceEvenlyAlignContentTest() {
            ConvertToPdfAndCompare("spaceEvenlyAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void SpaceEvenlyAlignSelfTest() {
            ConvertToPdfAndCompare("spaceEvenlyAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 4)]
        public virtual void SpaceEvenlyFlexDirTest() {
            ConvertToPdfAndCompare("spaceEvenlyFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void SpaceEvenlyMarginAndAlignSelfTest() {
            ConvertToPdfAndCompare("spaceEvenlyMarginAndAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void SpaceEvenlyMarginAutoOverrideTest() {
            ConvertToPdfAndCompare("spaceEvenlyMarginAutoOverride", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 1)]
        public virtual void SpaceEvenlyColumnReverseWrapLongTest() {
            ConvertToPdfAndCompare("spaceEvenlyColumnReverseWrapLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 3)]
        public virtual void SpaceValuesBorderAndMarginsTest() {
            ConvertToPdfAndCompare("spaceValuesBorderAndMargins", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 9)]
        public virtual void SpaceValuesFlexGrowTest() {
            ConvertToPdfAndCompare("spaceValuesFlexGrow", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 3)]
        public virtual void SpaceValuesPaddingBordersMarginTest() {
            ConvertToPdfAndCompare("spaceValuesPaddingBordersMargin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 3)]
        public virtual void SpaceValuesSingleElementTest() {
            ConvertToPdfAndCompare("spaceValuesSingleElement", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 3)]
        public virtual void SpaceValuesWithBordersTest() {
            ConvertToPdfAndCompare("spaceValuesWithBorders", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 3)]
        public virtual void SpaceValuesWithMarginsTest() {
            ConvertToPdfAndCompare("spaceValuesWithMargins", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 3)]
        public virtual void SpaceValuesWithPaddingTest() {
            ConvertToPdfAndCompare("spaceValuesWithPadding", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 9)]
        public virtual void SpaceValuesFlexShrinkTest() {
            ConvertToPdfAndCompare("spaceValuesFlexShrink", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 3)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 3)]
        public virtual void SpaceValuesNoWrapTest() {
            ConvertToPdfAndCompare("spaceValuesNoWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 3)]
        public virtual void SpaceValuesWrapTest() {
            ConvertToPdfAndCompare("spaceValuesWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 12)]
        public virtual void SpaceValuesWrapAlignItemsTest() {
            ConvertToPdfAndCompare("spaceValuesWrapAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 3)]
        public virtual void SpaceValuesWrapReverseTest() {
            ConvertToPdfAndCompare("spaceValuesWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
