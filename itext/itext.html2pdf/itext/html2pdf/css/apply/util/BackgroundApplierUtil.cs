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
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Commons.Utils;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Logs;
using iText.Kernel.Colors.Gradients;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Exceptions;
using iText.Svg;
using iText.Svg.Renderers;
using iText.Svg.Utils;
using iText.Svg.Xobject;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply backgrounds.</summary>
    public sealed class BackgroundApplierUtil {
        private static readonly ILogger LOGGER = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.BackgroundApplierUtil
            ));

        /// <summary>
        /// Creates a new
        /// <see cref="BackgroundApplierUtil"/>
        /// instance.
        /// </summary>
        private BackgroundApplierUtil() {
        }

        /// <summary>Applies background to an element.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the processor context</param>
        /// <param name="element">the element</param>
        public static void ApplyBackground(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             element) {
            String backgroundColorStr = cssProps.Get(CssConstants.BACKGROUND_COLOR);
            String backgroundImagesStr = cssProps.Get(CssConstants.BACKGROUND_IMAGE);
            String backgroundRepeatStr = cssProps.Get(CssConstants.BACKGROUND_REPEAT);
            String backgroundSizeStr = cssProps.Get(CssConstants.BACKGROUND_SIZE);
            String backgroundPositionXStr = cssProps.Get(CssConstants.BACKGROUND_POSITION_X);
            String backgroundPositionYStr = cssProps.Get(CssConstants.BACKGROUND_POSITION_Y);
            String backgroundBlendModeStr = cssProps.Get(CssConstants.BACKGROUND_BLEND_MODE);
            String backgroundClipStr = cssProps.Get(CssConstants.BACKGROUND_CLIP);
            String backgroundOriginStr = cssProps.Get(CssConstants.BACKGROUND_ORIGIN);
            IList<String> backgroundImagesArray = CssUtils.SplitStringWithComma(backgroundImagesStr);
            IList<String> backgroundRepeatArray = CssUtils.SplitStringWithComma(backgroundRepeatStr);
            IList<IList<String>> backgroundSizeArray = backgroundSizeStr == null ? null : CssUtils.ExtractShorthandProperties
                (backgroundSizeStr);
            IList<String> backgroundPositionXArray = CssUtils.SplitStringWithComma(backgroundPositionXStr);
            IList<String> backgroundPositionYArray = CssUtils.SplitStringWithComma(backgroundPositionYStr);
            IList<String> backgroundBlendModeArray = CssUtils.SplitStringWithComma(backgroundBlendModeStr);
            String fontSize = cssProps.Get(CssConstants.FONT_SIZE);
            float em = fontSize == null ? 0 : CssDimensionParsingUtils.ParseAbsoluteLength(fontSize);
            float rem = context.GetCssContext().GetRootFontSize();
            IList<String> backgroundClipArray = CssUtils.SplitStringWithComma(backgroundClipStr);
            IList<String> backgroundOriginArray = CssUtils.SplitStringWithComma(backgroundOriginStr);
            BackgroundBox clipForColor = GetBackgroundBoxProperty(backgroundClipArray, backgroundImagesArray.IsEmpty()
                 ? 0 : (backgroundImagesArray.Count - 1), BackgroundBox.BORDER_BOX);
            ApplyBackgroundColor(backgroundColorStr, element, clipForColor);
            IList<BackgroundImage> backgroundImagesList = GetBackgroundImagesList(backgroundImagesArray, context, em, 
                rem, backgroundPositionXArray, backgroundPositionYArray, backgroundSizeArray, backgroundBlendModeArray
                , backgroundRepeatArray, backgroundClipArray, backgroundOriginArray);
            if (!backgroundImagesList.IsEmpty()) {
                element.SetProperty(Property.BACKGROUND_IMAGE, backgroundImagesList);
            }
        }

        private static IList<BackgroundImage> GetBackgroundImagesList(IList<String> backgroundImagesArray, ProcessorContext
             context, float em, float rem, IList<String> backgroundPositionXArray, IList<String> backgroundPositionYArray
            , IList<IList<String>> backgroundSizeArray, IList<String> backgroundBlendModeArray, IList<String> backgroundRepeatArray
            , IList<String> backgroundClipArray, IList<String> backgroundOriginArray) {
            IList<BackgroundImage> backgroundImagesList = new List<BackgroundImage>();
            for (int i = 0; i < backgroundImagesArray.Count; ++i) {
                String backgroundImage = backgroundImagesArray[i];
                if (backgroundImage == null || CssConstants.NONE.Equals(backgroundImage)) {
                    continue;
                }
                BackgroundPosition position = ApplyBackgroundPosition(backgroundPositionXArray, backgroundPositionYArray, 
                    i, em, rem);
                BlendMode blendMode = ApplyBackgroundBlendMode(backgroundBlendModeArray, i);
                bool imageApplied = false;
                BackgroundRepeat repeat = ApplyBackgroundRepeat(backgroundRepeatArray, i);
                BackgroundBox clip = GetBackgroundBoxProperty(backgroundClipArray, i, BackgroundBox.BORDER_BOX);
                BackgroundBox origin = GetBackgroundBoxProperty(backgroundOriginArray, i, BackgroundBox.PADDING_BOX);
                if (CssGradientUtil.IsCssLinearGradientValue(backgroundImage)) {
                    imageApplied = ApplyLinearGradient(backgroundImage, backgroundImagesList, blendMode, position, em, rem, repeat
                        , clip, origin);
                }
                else {
                    PdfXObject image = context.GetResourceResolver().RetrieveImage(CssUtils.ExtractUrl(backgroundImage));
                    imageApplied = ApplyBackgroundImage(image, backgroundImagesList, repeat, blendMode, position, clip, origin
                        );
                }
                if (imageApplied) {
                    ApplyBackgroundSize(backgroundSizeArray, em, rem, i, backgroundImagesList[backgroundImagesList.Count - 1]);
                }
            }
            return backgroundImagesList;
        }

        private static BackgroundBox GetBackgroundBoxProperty(IList<String> propertyArray, int iteration, BackgroundBox
             defaultValue) {
            int index = GetBackgroundSidePropertyIndex(propertyArray.Count, iteration);
            if (index == -1) {
                return defaultValue;
            }
            else {
                return GetBackgroundBoxPropertyByString(propertyArray[index]);
            }
        }

        private static BackgroundBox GetBackgroundBoxPropertyByString(String box) {
            if (CommonCssConstants.PADDING_BOX.Equals(box)) {
                return BackgroundBox.PADDING_BOX;
            }
            else {
                if (CommonCssConstants.CONTENT_BOX.Equals(box)) {
                    return BackgroundBox.CONTENT_BOX;
                }
                else {
                    return BackgroundBox.BORDER_BOX;
                }
            }
        }

        private static BlendMode ApplyBackgroundBlendMode(IList<String> backgroundBlendModeArray, int iteration) {
            String cssValue = null;
            if (backgroundBlendModeArray != null && !backgroundBlendModeArray.IsEmpty()) {
                int actualValueIteration = Math.Min(iteration, backgroundBlendModeArray.Count - 1);
                cssValue = backgroundBlendModeArray[actualValueIteration];
            }
            return CssUtils.ParseBlendMode(cssValue);
        }

        private static BackgroundPosition ApplyBackgroundPosition(IList<String> backgroundPositionXArray, IList<String
            > backgroundPositionYArray, int i, float em, float rem) {
            BackgroundPosition position = new BackgroundPosition();
            int indexX = GetBackgroundSidePropertyIndex(backgroundPositionXArray.Count, i);
            if (indexX != -1) {
                ApplyBackgroundPositionX(position, backgroundPositionXArray[indexX], em, rem);
            }
            int indexY = GetBackgroundSidePropertyIndex(backgroundPositionYArray.Count, i);
            if (indexY != -1) {
                ApplyBackgroundPositionY(position, backgroundPositionYArray[indexY], em, rem);
            }
            return position;
        }

        private static void ApplyBackgroundPositionX(BackgroundPosition position, String xPosition, float em, float
             rem) {
            foreach (String value in iText.Commons.Utils.StringUtil.Split(xPosition, " ")) {
                switch (value) {
                    case CommonCssConstants.LEFT: {
                        position.SetPositionX(BackgroundPosition.PositionX.LEFT);
                        break;
                    }

                    case CommonCssConstants.RIGHT: {
                        position.SetPositionX(BackgroundPosition.PositionX.RIGHT);
                        break;
                    }

                    case CommonCssConstants.CENTER: {
                        position.SetPositionX(BackgroundPosition.PositionX.CENTER);
                        break;
                    }

                    default: {
                        UnitValue unitValue = CssDimensionParsingUtils.ParseLengthValueToPt(value, em, rem);
                        if (unitValue != null) {
                            position.SetXShift(unitValue);
                        }
                        break;
                    }
                }
            }
        }

        private static void ApplyBackgroundPositionY(BackgroundPosition position, String yPosition, float em, float
             rem) {
            foreach (String value in iText.Commons.Utils.StringUtil.Split(yPosition, " ")) {
                switch (value) {
                    case CommonCssConstants.TOP: {
                        position.SetPositionY(BackgroundPosition.PositionY.TOP);
                        break;
                    }

                    case CommonCssConstants.BOTTOM: {
                        position.SetPositionY(BackgroundPosition.PositionY.BOTTOM);
                        break;
                    }

                    case CommonCssConstants.CENTER: {
                        position.SetPositionY(BackgroundPosition.PositionY.CENTER);
                        break;
                    }

                    default: {
                        UnitValue unitValue = CssDimensionParsingUtils.ParseLengthValueToPt(value, em, rem);
                        if (unitValue != null) {
                            position.SetYShift(unitValue);
                        }
                        break;
                    }
                }
            }
        }

        private static BackgroundRepeat ApplyBackgroundRepeat(IList<String> backgroundRepeatArray, int iteration) {
            int index = GetBackgroundSidePropertyIndex(backgroundRepeatArray.Count, iteration);
            if (index != -1) {
                String[] repeatProps = iText.Commons.Utils.StringUtil.Split(backgroundRepeatArray[index], " ");
                if (repeatProps.Length == 1) {
                    if (CommonCssConstants.REPEAT_X.Equals(repeatProps[0])) {
                        return new BackgroundRepeat(BackgroundRepeat.BackgroundRepeatValue.REPEAT, BackgroundRepeat.BackgroundRepeatValue
                            .NO_REPEAT);
                    }
                    else {
                        if (CommonCssConstants.REPEAT_Y.Equals(repeatProps[0])) {
                            return new BackgroundRepeat(BackgroundRepeat.BackgroundRepeatValue.NO_REPEAT, BackgroundRepeat.BackgroundRepeatValue
                                .REPEAT);
                        }
                        else {
                            BackgroundRepeat.BackgroundRepeatValue value = CssBackgroundUtils.ParseBackgroundRepeat(repeatProps[0]);
                            return new BackgroundRepeat(value);
                        }
                    }
                }
                else {
                    if (repeatProps.Length == 2) {
                        return new BackgroundRepeat(CssBackgroundUtils.ParseBackgroundRepeat(repeatProps[0]), CssBackgroundUtils.ParseBackgroundRepeat
                            (repeatProps[1]));
                    }
                }
            }
            // Valid cases are processed by the block above, and in invalid
            // situations, the REPEAT property on both axes is used by default
            return new BackgroundRepeat();
        }

        private static int GetBackgroundSidePropertyIndex(int propertiesNumber, int iteration) {
            if (propertiesNumber > 0) {
                return iteration % propertiesNumber;
            }
            else {
                return -1;
            }
        }

        private static void ApplyBackgroundColor(String backgroundColorStr, IPropertyContainer element, BackgroundBox
             clip) {
            if (backgroundColorStr != null && !CssConstants.TRANSPARENT.Equals(backgroundColorStr)) {
                TransparentColor color = CssDimensionParsingUtils.ParseColor(backgroundColorStr);
                Background backgroundColor = new Background(color.GetColor(), color.GetOpacity(), clip);
                element.SetProperty(Property.BACKGROUND, backgroundColor);
            }
        }

        private static bool ApplyBackgroundImage(PdfXObject image, IList<BackgroundImage> backgroundImagesList, BackgroundRepeat
             repeat, BlendMode backgroundBlendMode, BackgroundPosition position, BackgroundBox clip, BackgroundBox
             origin) {
            if (image == null) {
                return false;
            }
            if (image is PdfImageXObject) {
                backgroundImagesList.Add(new BackgroundApplierUtil.HtmlBackgroundImage((PdfImageXObject)image, repeat, position
                    , backgroundBlendMode, clip, origin));
                return true;
            }
            else {
                if (image is SvgImageXObject) {
                    SvgImageXObject svgImageXObject = (SvgImageXObject)image;
                    UnitValue width = svgImageXObject.GetElementWidth();
                    bool isRelativeWidthSvg = width == null || width.IsPercentValue();
                    UnitValue height = svgImageXObject.GetElementHeight();
                    bool isRelativeHeightSvg = height == null || height.IsPercentValue();
                    backgroundImagesList.Add(new BackgroundApplierUtil.HtmlBackgroundImage((PdfFormXObject)image, repeat, position
                        , backgroundBlendMode, clip, origin, isRelativeWidthSvg || isRelativeHeightSvg));
                    return true;
                }
                else {
                    if (image is PdfFormXObject) {
                        backgroundImagesList.Add(new BackgroundApplierUtil.HtmlBackgroundImage((PdfFormXObject)image, repeat, position
                            , backgroundBlendMode, clip, origin));
                        return true;
                    }
                    else {
                        throw new InvalidOperationException();
                    }
                }
            }
        }

        private static bool ApplyLinearGradient(String image, IList<BackgroundImage> backgroundImagesList, BlendMode
             blendMode, BackgroundPosition position, float em, float rem, BackgroundRepeat repeat, BackgroundBox clip
            , BackgroundBox origin) {
            try {
                StrategyBasedLinearGradientBuilder gradientBuilder = CssGradientUtil.ParseCssLinearGradient(image, em, rem
                    );
                if (gradientBuilder != null) {
                    backgroundImagesList.Add(new BackgroundImage.Builder().SetLinearGradientBuilder(gradientBuilder).SetBackgroundBlendMode
                        (blendMode).SetBackgroundPosition(position).SetBackgroundRepeat(repeat).SetBackgroundClip(clip).SetBackgroundOrigin
                        (origin).Build());
                    return true;
                }
            }
            catch (StyledXMLParserException) {
                LOGGER.LogWarning(MessageFormatUtil.Format(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, image)
                    );
            }
            return false;
        }

        private static void ApplyBackgroundSize(IList<IList<String>> backgroundProperties, float em, float rem, int
             imageIndex, BackgroundImage image) {
            if (backgroundProperties == null || backgroundProperties.IsEmpty()) {
                return;
            }
            if (image.GetForm() != null && (image.GetImageHeight() == 0f || image.GetImageWidth() == 0f)) {
                if (!(image.GetForm() is SvgImageXObject && ((SvgImageXObject)image.GetForm()).IsRelativeSized())) {
                    // For relative sized SVG images it is expected that getImageWidth and
                    // getImageHeight can be null, they will be resolved later on drawing
                    return;
                }
            }
            IList<String> backgroundSizeValues = backgroundProperties[GetBackgroundSidePropertyIndex(backgroundProperties
                .Count, imageIndex)];
            if (backgroundSizeValues.Count == 2 && CommonCssConstants.AUTO.Equals(backgroundSizeValues[1])) {
                backgroundSizeValues.JRemoveAt(1);
            }
            if (backgroundSizeValues.Count == 1) {
                String widthValue = backgroundSizeValues[0];
                ApplyBackgroundWidth(widthValue, image, em, rem);
            }
            if (backgroundSizeValues.Count == 2) {
                ApplyBackgroundWidthHeight(backgroundSizeValues, image, em, rem);
            }
        }

        private static void ApplyBackgroundWidth(String widthValue, BackgroundImage image, float em, float rem) {
            if (CommonCssConstants.BACKGROUND_SIZE_VALUES.Contains(widthValue)) {
                if (widthValue.Equals(CommonCssConstants.CONTAIN)) {
                    image.GetBackgroundSize().SetBackgroundSizeToContain();
                }
                if (widthValue.Equals(CommonCssConstants.COVER)) {
                    image.GetBackgroundSize().SetBackgroundSizeToCover();
                }
                return;
            }
            image.GetBackgroundSize().SetBackgroundSizeToValues(CssDimensionParsingUtils.ParseLengthValueToPt(widthValue
                , em, rem), null);
        }

        private static void ApplyBackgroundWidthHeight(IList<String> backgroundSizeValues, BackgroundImage image, 
            float em, float rem) {
            String widthValue = backgroundSizeValues[0];
            if (CommonCssConstants.BACKGROUND_SIZE_VALUES.Contains(widthValue)) {
                if (widthValue.Equals(CommonCssConstants.AUTO)) {
                    UnitValue height = CssDimensionParsingUtils.ParseLengthValueToPt(backgroundSizeValues[1], em, rem);
                    if (height != null) {
                        image.GetBackgroundSize().SetBackgroundSizeToValues(null, height);
                    }
                }
                return;
            }
            image.GetBackgroundSize().SetBackgroundSizeToValues(CssDimensionParsingUtils.ParseLengthValueToPt(backgroundSizeValues
                [0], em, rem), CssDimensionParsingUtils.ParseLengthValueToPt(backgroundSizeValues[1], em, rem));
        }

        /// <summary>Implementation of the Image class when used in the context of HTML to PDF conversion.</summary>
        private class HtmlBackgroundImage : BackgroundImage {
            private const double PX_TO_PT_MULTIPLIER = 0.75;

            /// <summary>
            /// In iText, we use user unit for the image sizes (and by default
            /// one user unit = one point), whereas images are usually measured
            /// in pixels.
            /// </summary>
            private double dimensionMultiplier = 1;

            private bool isRelativeSizedSvg = false;

            /// <summary>
            /// Creates a new
            /// <see cref="HtmlBackgroundImage"/>
            /// instance.
            /// </summary>
            /// <param name="xObject">
            /// background-image property.
            /// <see cref="iText.Kernel.Pdf.Xobject.PdfImageXObject"/>
            /// instance.
            /// </param>
            /// <param name="repeat">
            /// background-repeat property.
            /// <see cref="iText.Layout.Properties.BackgroundRepeat"/>
            /// instance.
            /// </param>
            /// <param name="position">
            /// background-position property.
            /// <see cref="iText.Layout.Properties.BackgroundPosition"/>
            /// instance.
            /// </param>
            /// <param name="blendMode">
            /// background-blend-mode property.
            /// <see cref="iText.Layout.Properties.BlendMode"/>
            /// instance.
            /// </param>
            /// <param name="clip">
            /// background-clip property.
            /// <see cref="iText.Layout.Properties.BackgroundBox"/>
            /// instance.
            /// </param>
            /// <param name="origin">
            /// background-origin property.
            /// <see cref="iText.Layout.Properties.BackgroundBox"/>
            /// instance.
            /// </param>
            public HtmlBackgroundImage(PdfImageXObject xObject, BackgroundRepeat repeat, BackgroundPosition position, 
                BlendMode blendMode, BackgroundBox clip, BackgroundBox origin)
                : base(new BackgroundImage.Builder().SetImage(xObject).SetBackgroundRepeat(repeat).SetBackgroundPosition(position
                    ).SetBackgroundBlendMode(blendMode).SetBackgroundClip(clip).SetBackgroundOrigin(origin).Build()) {
                dimensionMultiplier = PX_TO_PT_MULTIPLIER;
            }

            /// <summary>
            /// Creates a new
            /// <see cref="HtmlBackgroundImage"/>
            /// instance.
            /// </summary>
            /// <param name="xObject">
            /// background-image property.
            /// <see cref="iText.Kernel.Pdf.Xobject.PdfFormXObject"/>
            /// instance.
            /// </param>
            /// <param name="repeat">
            /// background-repeat property.
            /// <see cref="iText.Layout.Properties.BackgroundRepeat"/>
            /// instance.
            /// </param>
            /// <param name="position">
            /// background-position property.
            /// <see cref="iText.Layout.Properties.BackgroundPosition"/>
            /// instance.
            /// </param>
            /// <param name="blendMode">
            /// background-blend-mode property.
            /// <see cref="iText.Layout.Properties.BlendMode"/>
            /// instance.
            /// </param>
            /// <param name="clip">
            /// background-clip property.
            /// <see cref="iText.Layout.Properties.BackgroundBox"/>
            /// instance.
            /// </param>
            /// <param name="origin">
            /// background-origin property.
            /// <see cref="iText.Layout.Properties.BackgroundBox"/>
            /// instance.
            /// </param>
            public HtmlBackgroundImage(PdfFormXObject xObject, BackgroundRepeat repeat, BackgroundPosition position, BlendMode
                 blendMode, BackgroundBox clip, BackgroundBox origin)
                : base(new BackgroundImage.Builder().SetImage(xObject).SetBackgroundRepeat(repeat).SetBackgroundPosition(position
                    ).SetBackgroundBlendMode(blendMode).SetBackgroundClip(clip).SetBackgroundOrigin(origin).Build()) {
            }

            public HtmlBackgroundImage(PdfFormXObject xObject, BackgroundRepeat repeat, BackgroundPosition position, BlendMode
                 blendMode, BackgroundBox clip, BackgroundBox origin, bool isRelativeSizedSvg)
                : base(new BackgroundImage.Builder().SetImage(xObject).SetBackgroundRepeat(repeat).SetBackgroundPosition(position
                    ).SetBackgroundBlendMode(blendMode).SetBackgroundClip(clip).SetBackgroundOrigin(origin).Build()) {
                this.isRelativeSizedSvg = isRelativeSizedSvg;
            }

            protected override float[] ResolveWidthAndHeight(float? width, float? height, float areaWidth, float areaHeight
                ) {
                if (!isRelativeSizedSvg) {
                    return base.ResolveWidthAndHeight(width, height, areaWidth, areaHeight);
                }
                SvgImageXObject svgImageXObject = (SvgImageXObject)image;
                ISvgNodeRenderer svgRootRenderer = svgImageXObject.GetResult().GetRootRenderer();
                float? aspectRatio = null;
                bool isAspectRatioNone = false;
                float[] viewBoxValues = SvgCssUtils.ParseViewBox(svgRootRenderer);
                String preserveAspectRatio = svgRootRenderer.GetAttribute(SvgConstants.Attributes.PRESERVE_ASPECT_RATIO);
                if (SvgConstants.Values.NONE.Equals(preserveAspectRatio)) {
                    isAspectRatioNone = true;
                }
                else {
                    if (viewBoxValues != null && viewBoxValues.Length == SvgConstants.Values.VIEWBOX_VALUES_NUMBER) {
                        // aspectRatio can also be specified by absolute height and width,
                        // but in that case SVG isn't relative and processed as usual image
                        aspectRatio = viewBoxValues[2] / viewBoxValues[3];
                    }
                }
                // The code below is based on the following algorithm (with modifications to follow real browsers behavior)
                // https://developer.mozilla.org/en-US/docs/Web/CSS/Scaling_of_SVG_backgrounds
                float? finalWidth = null;
                float? finalHeight = null;
                if (GetBackgroundSize().IsSpecificSize()) {
                    if (aspectRatio == null) {
                        finalWidth = areaWidth;
                        finalHeight = areaHeight;
                    }
                    else {
                        if (GetBackgroundSize().IsCover()) {
                            if (aspectRatio < areaWidth / areaHeight) {
                                finalWidth = areaWidth;
                                finalHeight = finalWidth / aspectRatio;
                            }
                            else {
                                finalHeight = areaHeight;
                                finalWidth = finalHeight * aspectRatio;
                            }
                        }
                        else {
                            // isContain
                            if (aspectRatio > areaWidth / areaHeight) {
                                finalWidth = areaWidth;
                                finalHeight = finalWidth / aspectRatio;
                            }
                            else {
                                finalHeight = areaHeight;
                                finalWidth = finalHeight * aspectRatio;
                            }
                        }
                    }
                }
                else {
                    UnitValue svgWidthUV = svgImageXObject.GetElementWidth();
                    UnitValue svgHeightUV = svgImageXObject.GetElementHeight();
                    if (width != null) {
                        finalWidth = width;
                    }
                    else {
                        if (svgWidthUV != null && svgWidthUV.IsPointValue()) {
                            finalWidth = svgWidthUV.GetValue();
                        }
                    }
                    if (height != null) {
                        finalHeight = height;
                    }
                    else {
                        if (svgHeightUV != null && svgHeightUV.IsPointValue()) {
                            finalHeight = svgHeightUV.GetValue();
                        }
                    }
                    if (isAspectRatioNone) {
                        // if aspect ratio is none, then if the svg specifies the final size, ignore it and use the whole area size
                        if (width == null && finalWidth != null) {
                            finalWidth = areaWidth;
                        }
                        if (height == null && finalHeight != null) {
                            finalHeight = areaHeight;
                        }
                    }
                    else {
                        if (aspectRatio != null && (width == null || height == null)) {
                            // svg aspectRatio affects only if there is at least one background size dimension which isn't defined
                            if (finalWidth == null && finalHeight == null) {
                                if (aspectRatio < areaWidth / areaHeight) {
                                    finalHeight = areaHeight;
                                    finalWidth = finalHeight * aspectRatio;
                                }
                                else {
                                    finalWidth = areaWidth;
                                    finalHeight = finalWidth / aspectRatio;
                                }
                            }
                            else {
                                if (finalWidth != null && finalHeight != null) {
                                    if (aspectRatio < finalWidth / finalHeight) {
                                        finalHeight = finalWidth / aspectRatio;
                                    }
                                    else {
                                        finalWidth = finalHeight / aspectRatio;
                                    }
                                }
                                else {
                                    if (finalWidth == null) {
                                        finalWidth = finalHeight * aspectRatio;
                                    }
                                    else {
                                        // finalHeight == null
                                        finalHeight = finalWidth / aspectRatio;
                                    }
                                }
                            }
                        }
                    }
                    // if no background or svg size, no aspect ratio or ratio=none, then just get the whole available area
                    if (finalWidth == null) {
                        finalWidth = areaWidth;
                    }
                    if (finalHeight == null) {
                        finalHeight = areaHeight;
                    }
                }
                if (aspectRatio != null) {
                    svgRootRenderer.SetAttribute(SvgConstants.Attributes.WIDTH, null);
                    svgRootRenderer.SetAttribute(SvgConstants.Attributes.HEIGHT, null);
                }
                svgImageXObject.UpdateBBox((float)finalWidth, (float)finalHeight);
                svgImageXObject.Generate(null);
                return new float[] { (float)finalWidth, (float)finalHeight };
            }

            public override float GetImageWidth() {
                return (float)(image.GetWidth() * dimensionMultiplier);
            }

            public override float GetImageHeight() {
                return (float)(image.GetHeight() * dimensionMultiplier);
            }
        }
    }
}
