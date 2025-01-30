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

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class ImageTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/ImageTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/ImageTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImagesInBodyTest() {
            ConvertToPdfAndCompare("imagesInBody", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-8869 Percent height is not resolved in fixed size container
        [NUnit.Framework.Test]
        public virtual void RelativeImageInStaticContainerTest() {
            ConvertToPdfAndCompare("relativeImageInStaticContainer", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImagesWithWideBorders() {
            ConvertToPdfAndCompare("imagesWithWideBorders", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImagesWithWideMargins() {
            ConvertToPdfAndCompare("imagesWithWideMargins", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImagesWithWidePaddings() {
            // TODO DEVSIX-2467
            ConvertToPdfAndCompare("imagesWithWidePaddings", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImagesWithWidePaddingsBordersMargins() {
            // TODO DEVSIX-2467
            ConvertToPdfAndCompare("imagesWithWidePaddingsBordersMargins", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CheckImageBorderRadius() {
            ConvertToPdfAndCompare("checkImageBorderRadius", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageFileDocumentTest() {
            ConvertToPdfAndCompare("smallWidthImagePlacement", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageUrlDocumentTest() {
            ConvertToPdfAndCompare("imageUrlDocument", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageWithIncorrectBase64Test() {
            ConvertToPdfAndCompare("imageWithIncorrectBase64", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmbeddedImageBase64Test() {
            //TODO DEVSIX-6190 pdfHtml: Embedded image from html doesn't present in out pdf
            ConvertToPdfAndCompare("embeddedImageBase64", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageBase64DifferentFormatsTest() {
            ConvertToPdfAndCompare("imageBase64DifferentFormats", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SmallImageTest() {
            ConvertToPdfAndCompare("smallImageTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageInSpanTest() {
            // TODO: DEVSIX-2485
            ConvertToPdfAndCompare("imageInSpan", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CaseSensitiveBase64DataInCssNormalizationTest() {
            ConvertToPdfAndCompare("caseSensitiveBase64DataInCssNormalization", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void InlineBlockImageTest() {
            ConvertToPdfAndCompare("inlineBlockImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        /// <summary>Important: the name of the resource in this test is "base64.svg".</summary>
        /// <remarks>
        /// Important: the name of the resource in this test is "base64.svg".
        /// This is done deliberately and tests for a bug that was present before -
        /// image was only fetched as base64 value and not as resource link
        /// </remarks>
        [NUnit.Framework.Test]
        public virtual void SvgExternalResourceCornerCaseTest() {
            ConvertToPdfAndCompare("svgExternalResourceCornerCase", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ImageAltTextTest() {
            ConvertToPdfAndCompare("imageAltText", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void ImageUrlExternalDocumentTest() {
            // Android-Conversion-Ignore-Test (TODO DEVSIX-6459 fix the SecurityException(Permission denied) from UrlUtil method)
            ConvertToPdfAndCompare("externalUrlImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 3)]
        public virtual void SourceMediaTest() {
            //To see the result in html, just increase the size
            ConvertToPdfAndCompare("sourceMedia", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ResolutionInfoStructOf8bimHeaderImageTest() {
            ConvertToPdfAndCompare("resolutionInfoStructOf8bimHeaderImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HtmlImgBase64SVGTest() {
            ConvertToPdfAndCompare("imgTag_base64svg", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg1Test() {
            ConvertToPdfAndCompare("relativeSizeSvg1", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg1_2Test() {
            ConvertToPdfAndCompare("relativeSizeSvg1_2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg1_3Test() {
            ConvertToPdfAndCompare("relativeSizeSvg1_3", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg1_4Test() {
            ConvertToPdfAndCompare("relativeSizeSvg1_4", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg1_5Test() {
            ConvertToPdfAndCompare("relativeSizeSvg1_5", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg1_6Test() {
            ConvertToPdfAndCompare("relativeSizeSvg1_6", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg2Test() {
            ConvertToPdfAndCompare("relativeSizeSvg2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg2_3Test() {
            ConvertToPdfAndCompare("relativeSizeSvg2_3", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg2_4Test() {
            ConvertToPdfAndCompare("relativeSizeSvg2_4", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg2_5Test() {
            ConvertToPdfAndCompare("relativeSizeSvg2_5", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg2_6Test() {
            ConvertToPdfAndCompare("relativeSizeSvg2_6", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg2_7Test() {
            ConvertToPdfAndCompare("relativeSizeSvg2_7", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg2_8Test() {
            ConvertToPdfAndCompare("relativeSizeSvg2_8", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg2_9Test() {
            ConvertToPdfAndCompare("relativeSizeSvg2_9", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_2_2Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_2_2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_3Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_3", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_4Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_4", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_5Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_5", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-8870 improve support of relative sized SVG in img HTML elements
        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_6Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_6", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_7Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_7", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_8Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_8", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_9Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_9", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_10Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_10", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_11Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_11", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_12Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_12", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_13Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_13", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_14Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_14", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg3_15Test() {
            ConvertToPdfAndCompare("relativeSizeSvg3_15", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-8869 Percent height is not resolved in fixed size container
        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_2Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-8869 Percent height is not resolved in fixed size container
        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_3Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_3", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-8869 Percent height is not resolved in fixed size container
        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_4Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_4", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-8869 Percent height is not resolved in fixed size container
        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_5Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_5", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-8869 Percent height is not resolved in fixed size container
        //TODO DEVSIX-8870 improve support of relative sized SVG in img HTML elements
        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_6Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_6", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-8869 Percent height is not resolved in fixed size container
        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_7Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_7", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-8869 Percent height is not resolved in fixed size container
        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_8Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_8", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-8869 Percent height is not resolved in fixed size container
        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_9Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_9", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-8869 Percent height is not resolved in fixed size container
        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_10Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_10", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-8869 Percent height is not resolved in fixed size container
        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_11Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_11", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_12Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_12", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-8869 Percent height is not resolved in fixed size container
        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_13Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_13", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-8869 Percent height is not resolved in fixed size container
        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_14Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_14", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-8869 Percent height is not resolved in fixed size container
        [NUnit.Framework.Test]
        public virtual void FixedImgRelativeSizeSvg3_15Test() {
            ConvertToPdfAndCompare("fixedImgRelativeSizeSvg3_15", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_2Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_3Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_3", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_4Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_4", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_5Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_5", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-8870 improve support of relative sized SVG in img HTML elements
        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_6Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_6", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_7Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_7", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_8Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_8", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_9Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_9", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_10Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_10", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_11Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_11", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_12Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_12", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_13Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_13", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_14Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_14", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeHeightImgRelativeSizeSvg3_15Test() {
            ConvertToPdfAndCompare("relativeHeightImgRelativeSizeSvg3_15", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-8869 Percent height is not resolved in fixed size container
        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg4Test() {
            ConvertToPdfAndCompare("relativeSizeSvg4", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-8869 Percent height is not resolved in fixed size container
        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg4_2Test() {
            ConvertToPdfAndCompare("relativeSizeSvg4_2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        //TODO DEVSIX-8869 Percent height is not resolved in fixed size container
        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg4_4Test() {
            ConvertToPdfAndCompare("relativeSizeSvg4_4", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg5_2Test() {
            ConvertToPdfAndCompare("relativeSizeSvg5_2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvg6_1Test() {
            ConvertToPdfAndCompare("relativeSizeSvg6_1", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void InlineRelativeImageTest() {
            // TODO DEVSIX-1316 make percent width doesn't affect elements min max width
            ConvertToPdfAndCompare("inlineRelativeImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TableRelativeImageTest() {
            // TODO DEVSIX-1316 make percent width doesn't affect elements min max width
            // TODO DEVSIX-7003 Problem with layouting image with relative size in the table
            ConvertToPdfAndCompare("tableRelativeImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeImageInRelativeContainerTest() {
            ConvertToPdfAndCompare("relativeImageInRelativeContainer", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageWithSizeAndDivContent1Test() {
            ConvertToPdfAndCompare("backgroundImageWithSizeAndDivContent1", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageWithSizeAndDivContent2Test() {
            ConvertToPdfAndCompare("backgroundImageWithSizeAndDivContent2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageWithSizeAndDivContent3Test() {
            ConvertToPdfAndCompare("backgroundImageWithSizeAndDivContent3", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageWithSizeAndDivContent4Test() {
            ConvertToPdfAndCompare("backgroundImageWithSizeAndDivContent4", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInFixedImg() {
            ConvertToPdfAndCompare("relativeSizeSvgInFixedImg", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInFixedImg2() {
            ConvertToPdfAndCompare("relativeSizeSvgInFixedImg2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInRelativeImg() {
            ConvertToPdfAndCompare("relativeSizeSvgInRelativeImg", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInRelativeImgWithCustomViewbox2() {
            ConvertToPdfAndCompare("relativeSizeSvgInRelativeImgWithCustomViewbox2", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgInRelativeImgWithCustomViewbox() {
            ConvertToPdfAndCompare("relativeSizeSvgInRelativeImgWithCustomViewbox", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ELEMENT_DOES_NOT_FIT_CURRENT_AREA)]
        public virtual void GiantSvgInRelativeImg() {
            ConvertToPdfAndCompare("giantSvgInRelativeImg", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void InvalidSizeSvgInRelativeImg() {
            ConvertToPdfAndCompare("invalidSizeSvgInRelativeImg", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FixedSizeSvgInRelativeImg() {
            ConvertToPdfAndCompare("fixedSizeSvgInRelativeImg", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSizeSvgFixedInlineBlock() {
            ConvertToPdfAndCompare("relativeSizeSvgFixedInlineBlock", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSvgInSeveralImages() {
            ConvertToPdfAndCompare("relativeSvgInSeveralImages", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RelativeSvgInImgAndBackground() {
            ConvertToPdfAndCompare("relativeSvgInImgAndBackground", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PercentHeightImgContainer() {
            ConvertToPdfAndCompare("percentHeightImgContainer", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void PercentHeightImgContainer2() {
            ConvertToPdfAndCompare("percentHeightImgContainer2", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
