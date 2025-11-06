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
using iText.Layout.Logs;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Flex {
    [NUnit.Framework.Category("IntegrationTest")]
    public class FlexBaseSizeTest : ExtendedHtmlConversionITextTest {
        // TODO DEVSIX-5091 Support flex-basis: content
        // TODO DEVSIX-5001 Support content, min-content, max-content as width values
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/flex/FlexBaseSizeTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/flex/FlexBaseSizeTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        // A and E – most common cases from https://www.w3.org/TR/css-flexbox-1/#algo-main-item (flex base size algorithm)
        [NUnit.Framework.Test]
        public virtual void FlexBasisAutoWidthNumTest() {
            ConvertToPdfAndCompare("flexBasisAutoWidthNum", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexBasisAutoHeightNumTest() {
            ConvertToPdfAndCompare("flexBasisAutoHeightNum", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexBasisAutoWidthPercentageTest() {
            ConvertToPdfAndCompare("flexBasisAutoWidthPercentage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexBasisAutoHeightPercentageTest() {
            ConvertToPdfAndCompare("flexBasisAutoHeightPercentage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexBasisAutoWidthContentTest() {
            ConvertToPdfAndCompare("flexBasisAutoWidthContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexBasisAutoHeightContentTest() {
            ConvertToPdfAndCompare("flexBasisAutoHeightContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexBasisAutoWidthMinContentTest() {
            // TODO DEVSIX-5001 min-content and max-content as width are not supported
            ConvertToPdfAndCompare("flexBasisAutoWidthMinContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexBasisAutoWidthMaxContentTest() {
            ConvertToPdfAndCompare("flexBasisAutoWidthMaxContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 12)]
        public virtual void FlexBasisContentTest() {
            ConvertToPdfAndCompare("flexBasisContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 9)]
        public virtual void FlexBasisContentAspectRatioTest() {
            // TODO DEVSIX-5255 Support aspect-ratio property
            ConvertToPdfAndCompare("flexBasisContentAspectRatio", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 9)]
        [LogMessage(LayoutLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void FlexBasisContentAspectRatioImageTest() {
            // Result for image width differs from browser, although min width and flexible lengths are determined according
            // to the CSS specification algorithms. Not sure why browser behaves like this: min main size is calculated based on
            // transferred size suggestion instead of content size suggestion and main size property is ignored.
            ConvertToPdfAndCompare("flexBasisContentAspectRatioImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 9)]
        public virtual void FlexBasisContentAspectRatioImageBrowserTest() {
            // flexBasisContentAspectRatioImageTest alternative
            ConvertToPdfAndCompare("flexBasisContentAspectRatioImageBrowser", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SplitMaxSizesTest() {
            ConvertToPdfAndCompare("splitMaxSizes", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SplitMinSizesTest() {
            ConvertToPdfAndCompare("splitMinSizes", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 10)]
        public virtual void FlexBasisContentIgnoreMinMaxWidthTest() {
            ConvertToPdfAndCompare("flexBasisContentIgnoreMinMaxWidth", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 5)]
        public virtual void FlexBasisContentIgnoreMinMaxHeightTest() {
            ConvertToPdfAndCompare("flexBasisContentIgnoreMinMaxHeight", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexBasisContentMaxWidthTest() {
            ConvertToPdfAndCompare("flexBasisContentMaxWidth", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexItemWithPercentageMaxWidthTest() {
            ConvertToPdfAndCompare("flexItemWithPercentageMaxWidth", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        // B from https://www.w3.org/TR/css-flexbox-1/#algo-main-item (Determine the flex base size algorithm)
        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void FlexBasisContentAspectRatioDefiniteCrossSizeTest() {
            // TODO DEVSIX-5255 Support aspect-ratio property
            ConvertToPdfAndCompare("flexBasisContentAspectRatioDefiniteCrossSize", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void FlexBasisContentAspectRatioDefiniteCrossSizeColumnTest() {
            // TODO DEVSIX-5255 Support aspect-ratio property
            ConvertToPdfAndCompare("flexBasisContentAspectRatioDefiniteCrossSizeColumn", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        // C from https://www.w3.org/TR/css-flexbox-1/#algo-main-item (Determine the flex base size algorithm)
        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void FlexBasisContentInsideMinContentTest() {
            // TODO DEVSIX-5001 min-content and max-content as width are not supported
            ConvertToPdfAndCompare("flexBasisContentInsideMinContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 4)]
        public virtual void FlexBasisContentInsideMaxContentTest() {
            // TODO DEVSIX-5001 min-content and max-content as width are not supported
            ConvertToPdfAndCompare("flexBasisContentInsideMaxContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void FlexBasisContentInsideMinContentColumnTest() {
            // TODO DEVSIX-5001 min-content and max-content as width are not supported
            ConvertToPdfAndCompare("flexBasisContentInsideMinContentColumn", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void FlexBasisContentInsideMaxContentColumnTest() {
            // TODO DEVSIX-5001 min-content and max-content as width are not supported
            ConvertToPdfAndCompare("flexBasisContentInsideMaxContentColumn", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        // D from https://www.w3.org/TR/css-flexbox-1/#algo-main-item (Determine the flex base size algorithm)
        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void FlexBasisContentOrthogonalFlowTest() {
            // TODO DEVSIX-5182 Support writing-mode property
            // E.g. infinite height + vertical main axe for flex container (column) + vertical-writing-mode flex item
            ConvertToPdfAndCompare("flexBasisContentOrthogonalFlow", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
