/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using System.Collections.Generic;
using Common.Logging;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Logs;
using iText.IO.Util;
using iText.Kernel.Colors;
using iText.Kernel.Colors.Gradients;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Exceptions;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply backgrounds.</summary>
    public sealed class BackgroundApplierUtil {
        private static readonly ILog LOGGER = LogManager.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.BackgroundApplierUtil
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

        /// <summary>
        /// Splits the provided
        /// <see cref="System.String"/>
        /// by comma with respect of brackets.
        /// </summary>
        /// <param name="value">to split</param>
        /// <returns>the split result</returns>
        [System.ObsoleteAttribute(@"use iText.StyledXmlParser.Css.Util.CssUtils.SplitStringWithComma(System.String)"
            )]
        internal static String[] SplitStringWithComma(String value) {
            if (value == null) {
                return new String[0];
            }
            IList<String> resultList = new List<String>();
            int lastComma = 0;
            int notClosedBrackets = 0;
            for (int i = 0; i < value.Length; ++i) {
                if (value[i] == ',' && notClosedBrackets == 0) {
                    resultList.Add(value.JSubstring(lastComma, i).Trim());
                    lastComma = i + 1;
                }
                if (value[i] == '(') {
                    ++notClosedBrackets;
                }
                if (value[i] == ')') {
                    --notClosedBrackets;
                    notClosedBrackets = Math.Max(notClosedBrackets, 0);
                }
            }
            String lastToken = value.Substring(lastComma);
            if (!String.IsNullOrEmpty(lastToken)) {
                resultList.Add(lastToken.Trim());
            }
            return resultList.ToArray(new String[0]);
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
                    PdfXObject image = context.GetResourceResolver().RetrieveImageExtended(CssUtils.ExtractUrl(backgroundImage
                        ));
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
            foreach (String value in iText.IO.Util.StringUtil.Split(xPosition, " ")) {
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
            foreach (String value in iText.IO.Util.StringUtil.Split(yPosition, " ")) {
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
                String[] repeatProps = iText.IO.Util.StringUtil.Split(backgroundRepeatArray[index], " ");
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
                float[] rgbaColor = CssDimensionParsingUtils.ParseRgbaColor(backgroundColorStr);
                Color color = new DeviceRgb(rgbaColor[0], rgbaColor[1], rgbaColor[2]);
                float opacity = rgbaColor[3];
                Background backgroundColor = new Background(color, opacity, clip);
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
                LOGGER.Warn(MessageFormatUtil.Format(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, image));
            }
            return false;
        }

        private static void ApplyBackgroundSize(IList<IList<String>> backgroundProperties, float em, float rem, int
             imageIndex, BackgroundImage image) {
            if (backgroundProperties == null || backgroundProperties.IsEmpty()) {
                return;
            }
            if (image.GetForm() != null && (image.GetImageHeight() == 0f || image.GetImageWidth() == 0f)) {
                return;
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

            public override float GetImageWidth() {
                return (float)(image.GetWidth() * dimensionMultiplier);
            }

            public override float GetImageHeight() {
                return (float)(image.GetHeight() * dimensionMultiplier);
            }

            public override float GetWidth() {
                return (float)(image.GetWidth() * dimensionMultiplier);
            }

            public override float GetHeight() {
                return (float)(image.GetHeight() * dimensionMultiplier);
            }
        }
    }
}
