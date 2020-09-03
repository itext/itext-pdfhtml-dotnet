/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
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
using iText.IO.Util;
using iText.Kernel.Colors;
using iText.Kernel.Colors.Gradients;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Properties;
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
            ApplyBackgroundColor(backgroundColorStr, element);
            String backgroundImagesStr = cssProps.Get(CssConstants.BACKGROUND_IMAGE);
            String backgroundRepeatStr = cssProps.Get(CssConstants.BACKGROUND_REPEAT);
            IList<BackgroundImage> backgroundImagesList = new List<BackgroundImage>();
            String[] backgroundImagesArray = SplitStringWithComma(backgroundImagesStr);
            String[] backgroundRepeatArray = SplitStringWithComma(backgroundRepeatStr);
            for (int i = 0; i < backgroundImagesArray.Length; ++i) {
                if (backgroundImagesArray[i] == null || CssConstants.NONE.Equals(backgroundImagesArray[i])) {
                    continue;
                }
                if (CssGradientUtil.IsCssLinearGradientValue(backgroundImagesArray[i])) {
                    ApplyLinearGradient(cssProps, context, backgroundImagesArray, backgroundImagesList, i);
                }
                else {
                    BackgroundRepeat repeat = ApplyBackgroundRepeat(backgroundRepeatArray, i);
                    ApplyBackgroundImage(context, backgroundImagesArray, backgroundImagesList, i, repeat);
                }
            }
            if (!backgroundImagesList.IsEmpty()) {
                element.SetProperty(Property.BACKGROUND_IMAGE, backgroundImagesList);
            }
        }

        internal static String[] SplitStringWithComma(String value) {
            if (value == null) {
                return new String[0];
            }
            IList<String> resultList = new List<String>();
            int lastComma = 0;
            int notClosedBrackets = 0;
            for (int i = 0; i < value.Length; ++i) {
                if (value[i] == ',' && notClosedBrackets == 0) {
                    resultList.Add(value.JSubstring(lastComma, i));
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
                resultList.Add(lastToken);
            }
            return resultList.ToArray(new String[0]);
        }

        private static BackgroundRepeat ApplyBackgroundRepeat(String[] backgroundRepeatArray, int iteration) {
            int index = GetBackgroundSidePropertyIndex(backgroundRepeatArray, iteration);
            if (index != -1) {
                bool repeatX = CssConstants.REPEAT.Equals(backgroundRepeatArray[index]) || CssConstants.REPEAT_X.Equals(backgroundRepeatArray
                    [index]);
                bool repeatY = CssConstants.REPEAT.Equals(backgroundRepeatArray[index]) || CssConstants.REPEAT_Y.Equals(backgroundRepeatArray
                    [index]);
                return new BackgroundRepeat(repeatX, repeatY);
            }
            return new BackgroundRepeat(true, true);
        }

        private static int GetBackgroundSidePropertyIndex(String[] backgroundPropertyArray, int iteration) {
            if (backgroundPropertyArray.Length > 0) {
                if (backgroundPropertyArray.Length > iteration) {
                    return iteration;
                }
                else {
                    return 0;
                }
            }
            return -1;
        }

        private static void ApplyBackgroundColor(String backgroundColorStr, IPropertyContainer element) {
            if (backgroundColorStr != null && !CssConstants.TRANSPARENT.Equals(backgroundColorStr)) {
                float[] rgbaColor = CssUtils.ParseRgbaColor(backgroundColorStr);
                Color color = new DeviceRgb(rgbaColor[0], rgbaColor[1], rgbaColor[2]);
                float opacity = rgbaColor[3];
                Background backgroundColor = new Background(color, opacity);
                element.SetProperty(Property.BACKGROUND, backgroundColor);
            }
        }

        private static void ApplyBackgroundImage(ProcessorContext context, String[] backgroundImagesArray, IList<BackgroundImage
            > backgroundImagesList, int i, BackgroundRepeat repeat) {
            PdfXObject image = context.GetResourceResolver().RetrieveImageExtended(CssUtils.ExtractUrl(backgroundImagesArray
                [i]));
            if (image != null) {
                if (image is PdfImageXObject) {
                    backgroundImagesList.Add(new BackgroundApplierUtil.HtmlBackgroundImage((PdfImageXObject)image, repeat));
                }
                else {
                    if (image is PdfFormXObject) {
                        backgroundImagesList.Add(new BackgroundApplierUtil.HtmlBackgroundImage((PdfFormXObject)image, repeat));
                    }
                    else {
                        throw new InvalidOperationException();
                    }
                }
            }
        }

        private static void ApplyLinearGradient(IDictionary<String, String> cssProps, ProcessorContext context, String
            [] backgroundImagesArray, IList<BackgroundImage> backgroundImagesList, int i) {
            float em = CssUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
            float rem = context.GetCssContext().GetRootFontSize();
            try {
                StrategyBasedLinearGradientBuilder gradientBuilder = CssGradientUtil.ParseCssLinearGradient(backgroundImagesArray
                    [i], em, rem);
                if (gradientBuilder != null) {
                    backgroundImagesList.Add(new BackgroundImage(gradientBuilder));
                }
            }
            catch (StyledXMLParserException) {
                LOGGER.Warn(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, backgroundImagesArray
                    [i]));
            }
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
            /// background image property.
            /// <see cref="iText.Kernel.Pdf.Xobject.PdfImageXObject"/>
            /// instance.
            /// </param>
            /// <param name="repeat">
            /// background repeat property.
            /// <see cref="iText.Layout.Properties.BackgroundRepeat"/>
            /// instance.
            /// </param>
            public HtmlBackgroundImage(PdfImageXObject xObject, BackgroundRepeat repeat)
                : base(xObject, repeat) {
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
            public HtmlBackgroundImage(PdfFormXObject xObject, BackgroundRepeat repeat)
                : base(xObject, repeat) {
            }

            /// <summary>
            /// Creates a new
            /// <see cref="HtmlBackgroundImage"/>
            /// instance.
            /// </summary>
            /// <param name="xObject">
            /// background image property.
            /// <see cref="iText.Kernel.Pdf.Xobject.PdfImageXObject"/>
            /// instance.
            /// </param>
            /// <param name="repeatX">is background is repeated in x dimension.</param>
            /// <param name="repeatY">is background is repeated in y dimension.</param>
            [System.ObsoleteAttribute(@"Remove this constructor in 7.2.")]
            public HtmlBackgroundImage(PdfImageXObject xObject, bool repeatX, bool repeatY)
                : this(xObject, new BackgroundRepeat(repeatX, repeatY)) {
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
            /// <param name="repeatX">is background is repeated in x dimension.</param>
            /// <param name="repeatY">is background is repeated in y dimension.</param>
            [System.ObsoleteAttribute(@"Remove this constructor in 7.2.")]
            public HtmlBackgroundImage(PdfFormXObject xObject, bool repeatX, bool repeatY)
                : this(xObject, new BackgroundRepeat(repeatX, repeatY)) {
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
