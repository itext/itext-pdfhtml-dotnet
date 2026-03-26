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
    public class FlexJustifyContentTest : ExtendedHtmlConversionITextTest {
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
        public virtual void InheritFlexDirTest() {
            ConvertToPdfAndCompare("inheritFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void InheritSpaceValuesTest() {
            ConvertToPdfAndCompare("inheritSpaceValues", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void InheritWrapTest() {
            ConvertToPdfAndCompare("inheritWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void InitialSimpleTest() {
            ConvertToPdfAndCompare("initialSimple", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void InitialFlexDirTest() {
            ConvertToPdfAndCompare("initialFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void InitialWrapTest() {
            ConvertToPdfAndCompare("initialWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage("Cannot find pdfCalligraph module, which was implicitly required by one of the layout properties"
            , Count = 24)]
        public virtual void LeftValueTest() {
            // TODO DEVSIX-9436 Flex: alignment/justify-content doesn't work correctly with direction: rtl
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
        public virtual void SimpleCombinedTest() {
            ConvertToPdfAndCompare("simpleCombined", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 3)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 2)]
        public virtual void MultipleValuesMarginAutoOverridesTest() {
            // TODO DEVSIX-5002 pdfHTML: support 'margin: auto'
            ConvertToPdfAndCompare("otherValuesMarginAutoOverrides", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StartEndCenterValuesAlignItemsTest() {
            ConvertToPdfAndCompare("startEndCenterValuesAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StartEndCenterValuesWrapTest() {
            // TODO DEVSIX-9446 Support continuous container logic for flex
            ConvertToPdfAndCompare("startEndCenterValuesWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StartEndCenterValuesNoWrapTest() {
            ConvertToPdfAndCompare("startEndCenterValuesNoWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StartEndCenterValuesWrapReverseTest() {
            // TODO DEVSIX-9446 Support continuous container logic for flex
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
        public virtual void CenterOnPageSplitTest() {
            ConvertToPdfAndCompare("centerOnPageSplit", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StartColumnWrapLongTest() {
            // TODO DEVSIX-9446 Support continuous container logic for flex
            ConvertToPdfAndCompare("startColumnWrapLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexEndColumnReverseWrapLongTest() {
            // TODO DEVSIX-9446 Support continuous container logic for flex
            ConvertToPdfAndCompare("flexEndColumnReverseWrapLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexEndFlexWrapOnPageSplitTest() {
            ConvertToPdfAndCompare("flexEndFlexWrapOnPageSplit", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexEndFlexWrapFlexDirOnPageSplitTest() {
            ConvertToPdfAndCompare("flexEndFlexWrapFlexDirOnPageSplit", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexEndDirColumnWrapLongTest() {
            ConvertToPdfAndCompare("flexEndDirColumnWrapLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexStartRowReverseWrapLongTest() {
            ConvertToPdfAndCompare("flexStartRowReverseWrapLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void RevertSimpleTest() {
            ConvertToPdfAndCompare("revertSimple", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 8)]
        public virtual void RevertFlexDirTest() {
            ConvertToPdfAndCompare("revertFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void RevertLayerTest() {
            ConvertToPdfAndCompare("revertLayer", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 6)]
        public virtual void RevertWrapTest() {
            ConvertToPdfAndCompare("revertWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 5)]
        public virtual void NestedSplitOverflowRevertWrap() {
            ConvertToPdfAndCompare("nestedSplitOverflowRevertWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 5)]
        public virtual void SafeCenterTest() {
            ConvertToPdfAndCompare("safeCenter", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 12)]
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
        public virtual void UnsafeCenterWrapTest() {
            ConvertToPdfAndCompare("unsafeCenterWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StretchTest() {
            ConvertToPdfAndCompare("stretch", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StretchFlexDirTest() {
            ConvertToPdfAndCompare("stretchFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StretchWrapTest() {
            ConvertToPdfAndCompare("stretchWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void StretchAlignItemsTest() {
            // TODO DEVSIX-5167 Support baseline value for align-items and align-self
            ConvertToPdfAndCompare("stretchAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StretchAlignContentTest() {
            // TODO DEVSIX-9446 Support continuous container logic for flex
            ConvertToPdfAndCompare("stretchAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void StretchAlignSelfTest() {
            // TODO DEVSIX-5167 Support baseline value for align-items and align-self
            ConvertToPdfAndCompare("stretchAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 1)]
        public virtual void UnsetTest() {
            ConvertToPdfAndCompare("unset", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 4)]
        public virtual void UnsetFlexDirTest() {
            ConvertToPdfAndCompare("unsetFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 3)]
        public virtual void UnsetWrapTest() {
            ConvertToPdfAndCompare("unsetWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceAroundSimpleTest() {
            ConvertToPdfAndCompare("spaceAroundSimple", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceAroundFlexDirTest() {
            ConvertToPdfAndCompare("spaceAroundFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceAroundMarginAndAlignSelfTest() {
            // TODO DEVSIX-5002 pdfHTML: support 'margin: auto'
            ConvertToPdfAndCompare("spaceAroundMarginAndAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceAroundMarginAutoTest() {
            // TODO DEVSIX-5002 pdfHTML: support 'margin: auto'
            ConvertToPdfAndCompare("spaceAroundMarginAuto", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceAroundMarginAuto2Test() {
            // TODO DEVSIX-5002 pdfHTML: support 'margin: auto'
            ConvertToPdfAndCompare("spaceAroundMarginAuto2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceAroundMarginAutoOverrideTest() {
            // TODO DEVSIX-5002 pdfHTML: support 'margin: auto'
            ConvertToPdfAndCompare("spaceAroundMarginAutoOverride", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void SpaceAroundAlignItemsTest() {
            // TODO DEVSIX-5167 Support baseline value for align-items and align-self
            ConvertToPdfAndCompare("spaceAroundAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceAroundAlignContentTest() {
            ConvertToPdfAndCompare("spaceAroundAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void SpaceAroundAlignSelfTest() {
            // TODO DEVSIX-5167 Support baseline value for align-items and align-self
            ConvertToPdfAndCompare("spaceAroundAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceAroundWrapLongTest() {
            // TODO DEVSIX-9446 Support continuous container logic for flex
            ConvertToPdfAndCompare("spaceAroundWrapLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceAroundColumnWrapLongTest() {
            // TODO DEVSIX-9446 Support continuous container logic for flex
            ConvertToPdfAndCompare("spaceAroundColumnWrapLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceAroundOnPageSplitTest() {
            ConvertToPdfAndCompare("spaceAroundOnPageSplit", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceAroundFlexWrapFlexDirOnPageSplitTest() {
            ConvertToPdfAndCompare("spaceAroundFlexWrapFlexDirOnPageSplit", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceAroundFlexWrapFlexDirOnPageSplit2Test() {
            ConvertToPdfAndCompare("spaceAroundFlexWrapFlexDirOnPageSplit2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceBetweenSimpleTest() {
            ConvertToPdfAndCompare("spaceBetweenSimple", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceBetweenFlexDirTest() {
            ConvertToPdfAndCompare("spaceBetweenFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceBetweenMarginAndAlignSelfTest() {
            // TODO DEVSIX-5002 pdfHTML: support 'margin: auto'
            ConvertToPdfAndCompare("spaceBetweenMarginAndAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceBetweenMarginAutoOverrideTest() {
            // TODO DEVSIX-5002 pdfHTML: support 'margin: auto'
            ConvertToPdfAndCompare("spaceBetweenMarginAutoOverride", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceBetweenAllMarginOptionsTest() {
            // TODO DEVSIX-5002 pdfHTML: support 'margin: auto'
            ConvertToPdfAndCompare("spaceBetweenAllMarginOptions", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 1)]
        public virtual void SpaceBetweenAlignItemsTest() {
            // TODO DEVSIX-5167 Support baseline value for align-items and align-self
            ConvertToPdfAndCompare("spaceBetweenAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceBetweenAlignContentTest() {
            ConvertToPdfAndCompare("spaceBetweenAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void SpaceBetweenAlignSelfTest() {
            // TODO DEVSIX-5167 Support baseline value for align-items and align-self
            ConvertToPdfAndCompare("spaceBetweenAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceBetweenColumnReverseWrapLongTest() {
            // TODO DEVSIX-9446 Support continuous container logic for flex
            ConvertToPdfAndCompare("spaceBetweenColumnReverseWrapLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceBetweenOnPageSplitTest() {
            ConvertToPdfAndCompare("spaceBetweenOnPageSplit", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceEvenlySimpleTest() {
            ConvertToPdfAndCompare("spaceEvenlySimple", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 1)]
        public virtual void SpaceEvenlyAlignItemsTest() {
            // TODO DEVSIX-5167 Support baseline value for align-items and align-self
            ConvertToPdfAndCompare("spaceEvenlyAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceEvenlyAlignContentTest() {
            ConvertToPdfAndCompare("spaceEvenlyAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void SpaceEvenlyAlignSelfTest() {
            ConvertToPdfAndCompare("spaceEvenlyAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceEvenlyFlexDirTest() {
            ConvertToPdfAndCompare("spaceEvenlyFlexDir", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceEvenlyMarginAndAlignSelfTest() {
            // TODO DEVSIX-5002 pdfHTML: support 'margin: auto'
            ConvertToPdfAndCompare("spaceEvenlyMarginAndAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceEvenlyMarginAutoOverrideTest() {
            // TODO DEVSIX-5002 pdfHTML: support 'margin: auto'
            ConvertToPdfAndCompare("spaceEvenlyMarginAutoOverride", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceEvenlyColumnReverseWrapLongTest() {
            // TODO DEVSIX-9446 Support continuous container logic for flex
            ConvertToPdfAndCompare("spaceEvenlyColumnReverseWrapLong", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceEvenlyOnPageSplitTest() {
            ConvertToPdfAndCompare("spaceEvenlyOnPageSplit", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceValuesBorderAndMarginsTest() {
            // TODO DEVSIX-9446 Support continuous container logic for flex
            ConvertToPdfAndCompare("spaceValuesBorderAndMargins", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceValuesFlexGrowTest() {
            ConvertToPdfAndCompare("spaceValuesFlexGrow", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceValuesPaddingBordersMarginTest() {
            // TODO DEVSIX-9446 Support continuous container logic for flex
            ConvertToPdfAndCompare("spaceValuesPaddingBordersMargin", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceValuesSingleElementTest() {
            ConvertToPdfAndCompare("spaceValuesSingleElement", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceValuesOneElemDirRowReverseTest() {
            ConvertToPdfAndCompare("spaceValuesOneElemDirRowReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceValuesOneElemDirColumnReverseTest() {
            ConvertToPdfAndCompare("spaceValuesOneElemDirColumnReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceValuesOneElemDirRowTest() {
            ConvertToPdfAndCompare("spaceValuesOneElemDirRow", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceValuesWithBordersTest() {
            ConvertToPdfAndCompare("spaceValuesWithBorders", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceValuesWithMarginsTest() {
            // TODO DEVSIX-9446 Support continuous container logic for flex
            ConvertToPdfAndCompare("spaceValuesWithMargins", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceValuesWithPaddingTest() {
            ConvertToPdfAndCompare("spaceValuesWithPadding", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceValuesFlexShrinkTest() {
            ConvertToPdfAndCompare("spaceValuesFlexShrink", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceValuesNoWrapTest() {
            ConvertToPdfAndCompare("spaceValuesNoWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceValuesWrapTest() {
            // TODO DEVSIX-9446 Support continuous container logic for flex
            ConvertToPdfAndCompare("spaceValuesWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceValuesWrapAlignItemsTest() {
            ConvertToPdfAndCompare("spaceValuesWrapAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpaceValuesWrapReverseTest() {
            // TODO DEVSIX-9446 Support continuous container logic for flex
            ConvertToPdfAndCompare("spaceValuesWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
