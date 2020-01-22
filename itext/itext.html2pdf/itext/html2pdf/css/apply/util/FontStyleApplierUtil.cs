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
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Font;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Exceptions;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply font styles.</summary>
    public sealed class FontStyleApplierUtil {
        /// <summary>The logger.</summary>
        private static readonly ILog logger = LogManager.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.FontStyleApplierUtil
            ));

        private const float DEFAULT_LINE_HEIGHT = 1.2f;

        /// <summary>
        /// Creates a
        /// <see cref="FontStyleApplierUtil"/>
        /// instance.
        /// </summary>
        private FontStyleApplierUtil() {
        }

        /// <summary>Applies font styles to an element.</summary>
        /// <param name="cssProps">the CSS props</param>
        /// <param name="context">the processor context</param>
        /// <param name="stylesContainer">the styles container</param>
        /// <param name="element">the element</param>
        public static void ApplyFontStyles(IDictionary<String, String> cssProps, ProcessorContext context, IStylesContainer
             stylesContainer, IPropertyContainer element) {
            float em = CssUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
            float rem = context.GetCssContext().GetRootFontSize();
            if (em != 0) {
                element.SetProperty(Property.FONT_SIZE, UnitValue.CreatePointValue(em));
            }
            if (cssProps.Get(CssConstants.FONT_FAMILY) != null) {
                // TODO DEVSIX-2534
                IList<String> fontFamilies = FontFamilySplitter.SplitFontFamily(cssProps.Get(CssConstants.FONT_FAMILY));
                element.SetProperty(Property.FONT, fontFamilies.ToArray(new String[fontFamilies.Count]));
            }
            if (cssProps.Get(CssConstants.FONT_WEIGHT) != null) {
                element.SetProperty(Property.FONT_WEIGHT, cssProps.Get(CssConstants.FONT_WEIGHT));
            }
            if (cssProps.Get(CssConstants.FONT_STYLE) != null) {
                element.SetProperty(Property.FONT_STYLE, cssProps.Get(CssConstants.FONT_STYLE));
            }
            String cssColorPropValue = cssProps.Get(CssConstants.COLOR);
            if (cssColorPropValue != null) {
                TransparentColor transparentColor;
                if (!CssConstants.TRANSPARENT.Equals(cssColorPropValue)) {
                    float[] rgbaColor = CssUtils.ParseRgbaColor(cssColorPropValue);
                    Color color = new DeviceRgb(rgbaColor[0], rgbaColor[1], rgbaColor[2]);
                    float opacity = rgbaColor[3];
                    transparentColor = new TransparentColor(color, opacity);
                }
                else {
                    transparentColor = new TransparentColor(ColorConstants.BLACK, 0f);
                }
                element.SetProperty(Property.FONT_COLOR, transparentColor);
            }
            // Make sure to place that before text-align applier
            String direction = cssProps.Get(CssConstants.DIRECTION);
            if (CssConstants.RTL.Equals(direction)) {
                element.SetProperty(Property.BASE_DIRECTION, BaseDirection.RIGHT_TO_LEFT);
                element.SetProperty(Property.TEXT_ALIGNMENT, TextAlignment.RIGHT);
            }
            else {
                if (CssConstants.LTR.Equals(direction)) {
                    element.SetProperty(Property.BASE_DIRECTION, BaseDirection.LEFT_TO_RIGHT);
                    element.SetProperty(Property.TEXT_ALIGNMENT, TextAlignment.LEFT);
                }
            }
            if (stylesContainer is IElementNode && ((IElementNode)stylesContainer).ParentNode() is IElementNode && CssConstants
                .RTL.Equals(((IElementNode)((IElementNode)stylesContainer).ParentNode()).GetStyles().Get(CssConstants.
                DIRECTION)) && !element.HasProperty(Property.HORIZONTAL_ALIGNMENT)) {
                // We should only apply horizontal alignment if parent has dir attribute or direction property
                element.SetProperty(Property.HORIZONTAL_ALIGNMENT, HorizontalAlignment.RIGHT);
            }
            // Make sure to place that after direction applier
            String align = cssProps.Get(CssConstants.TEXT_ALIGN);
            if (CssConstants.LEFT.Equals(align)) {
                element.SetProperty(Property.TEXT_ALIGNMENT, TextAlignment.LEFT);
            }
            else {
                if (CssConstants.RIGHT.Equals(align)) {
                    element.SetProperty(Property.TEXT_ALIGNMENT, TextAlignment.RIGHT);
                }
                else {
                    if (CssConstants.CENTER.Equals(align)) {
                        element.SetProperty(Property.TEXT_ALIGNMENT, TextAlignment.CENTER);
                    }
                    else {
                        if (CssConstants.JUSTIFY.Equals(align)) {
                            element.SetProperty(Property.TEXT_ALIGNMENT, TextAlignment.JUSTIFIED);
                            element.SetProperty(Property.SPACING_RATIO, 1f);
                        }
                    }
                }
            }
            String whiteSpace = cssProps.Get(CssConstants.WHITE_SPACE);
            element.SetProperty(Property.NO_SOFT_WRAP_INLINE, CssConstants.NOWRAP.Equals(whiteSpace) || CssConstants.PRE
                .Equals(whiteSpace));
            String textDecorationProp = cssProps.Get(CssConstants.TEXT_DECORATION);
            if (textDecorationProp != null) {
                String[] textDecorations = iText.IO.Util.StringUtil.Split(textDecorationProp, "\\s+");
                IList<Underline> underlineList = new List<Underline>();
                foreach (String textDecoration in textDecorations) {
                    if (CssConstants.BLINK.Equals(textDecoration)) {
                        logger.Error(iText.Html2pdf.LogMessageConstant.TEXT_DECORATION_BLINK_NOT_SUPPORTED);
                    }
                    else {
                        if (CssConstants.LINE_THROUGH.Equals(textDecoration)) {
                            underlineList.Add(new Underline(null, .75f, 0, 0, 1 / 4f, PdfCanvasConstants.LineCapStyle.BUTT));
                        }
                        else {
                            if (CssConstants.OVERLINE.Equals(textDecoration)) {
                                underlineList.Add(new Underline(null, .75f, 0, 0, 9 / 10f, PdfCanvasConstants.LineCapStyle.BUTT));
                            }
                            else {
                                if (CssConstants.UNDERLINE.Equals(textDecoration)) {
                                    underlineList.Add(new Underline(null, .75f, 0, 0, -1 / 10f, PdfCanvasConstants.LineCapStyle.BUTT));
                                }
                                else {
                                    if (CssConstants.NONE.Equals(textDecoration)) {
                                        underlineList = null;
                                        // if none and any other decoration are used together, none is displayed
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                element.SetProperty(Property.UNDERLINE, underlineList);
            }
            String textIndent = cssProps.Get(CssConstants.TEXT_INDENT);
            if (textIndent != null) {
                UnitValue textIndentValue = CssUtils.ParseLengthValueToPt(textIndent, em, rem);
                if (textIndentValue != null) {
                    if (textIndentValue.IsPointValue()) {
                        element.SetProperty(Property.FIRST_LINE_INDENT, textIndentValue.GetValue());
                    }
                    else {
                        logger.Error(MessageFormatUtil.Format(iText.Html2pdf.LogMessageConstant.CSS_PROPERTY_IN_PERCENTS_NOT_SUPPORTED
                            , CssConstants.TEXT_INDENT));
                    }
                }
            }
            String letterSpacing = cssProps.Get(CssConstants.LETTER_SPACING);
            if (letterSpacing != null && !CssConstants.NORMAL.Equals(letterSpacing)) {
                UnitValue letterSpacingValue = CssUtils.ParseLengthValueToPt(letterSpacing, em, rem);
                if (letterSpacingValue.IsPointValue()) {
                    element.SetProperty(Property.CHARACTER_SPACING, letterSpacingValue.GetValue());
                }
            }
            // browsers ignore values in percents
            String wordSpacing = cssProps.Get(CssConstants.WORD_SPACING);
            if (wordSpacing != null) {
                UnitValue wordSpacingValue = CssUtils.ParseLengthValueToPt(wordSpacing, em, rem);
                if (wordSpacingValue != null) {
                    if (wordSpacingValue.IsPointValue()) {
                        element.SetProperty(Property.WORD_SPACING, wordSpacingValue.GetValue());
                    }
                }
            }
            // browsers ignore values in percents
            String lineHeight = cssProps.Get(CssConstants.LINE_HEIGHT);
            // specification does not give auto as a possible lineHeight value
            // nevertheless some browsers compute it as normal so we apply the same behaviour.
            // What's more, it's basically the same thing as if lineHeight is not set in the first place
            if (lineHeight != null && !CssConstants.NORMAL.Equals(lineHeight) && !CssConstants.AUTO.Equals(lineHeight)
                ) {
                if (CssUtils.IsNumericValue(lineHeight)) {
                    float? mult = CssUtils.ParseFloat(lineHeight);
                    if (mult != null) {
                        element.SetProperty(Property.LEADING, new Leading(Leading.MULTIPLIED, (float)mult));
                    }
                }
                else {
                    UnitValue lineHeightValue = CssUtils.ParseLengthValueToPt(lineHeight, em, rem);
                    if (lineHeightValue != null && lineHeightValue.IsPointValue()) {
                        element.SetProperty(Property.LEADING, new Leading(Leading.FIXED, lineHeightValue.GetValue()));
                    }
                    else {
                        if (lineHeightValue != null) {
                            element.SetProperty(Property.LEADING, new Leading(Leading.MULTIPLIED, lineHeightValue.GetValue() / 100));
                        }
                    }
                }
            }
            else {
                element.SetProperty(Property.LEADING, new Leading(Leading.MULTIPLIED, DEFAULT_LINE_HEIGHT));
            }
        }

        /// <summary>Parses the absolute font size.</summary>
        /// <param name="fontSizeValue">
        /// the font size value as a
        /// <see cref="System.String"/>
        /// </param>
        /// <returns>
        /// the font size value as a
        /// <c>float</c>
        /// </returns>
        [System.ObsoleteAttribute(@"Use iText.StyledXmlParser.Css.Util.CssUtils.ParseAbsoluteFontSize(System.String) instead."
            )]
        public static float ParseAbsoluteFontSize(String fontSizeValue) {
            if (CssConstants.FONT_ABSOLUTE_SIZE_KEYWORDS.Contains(fontSizeValue)) {
                switch (fontSizeValue) {
                    case CssConstants.XX_SMALL: {
                        fontSizeValue = "9px";
                        break;
                    }

                    case CssConstants.X_SMALL: {
                        fontSizeValue = "10px";
                        break;
                    }

                    case CssConstants.SMALL: {
                        fontSizeValue = "13px";
                        break;
                    }

                    case CssConstants.MEDIUM: {
                        fontSizeValue = "16px";
                        break;
                    }

                    case CssConstants.LARGE: {
                        fontSizeValue = "18px";
                        break;
                    }

                    case CssConstants.X_LARGE: {
                        fontSizeValue = "24px";
                        break;
                    }

                    case CssConstants.XX_LARGE: {
                        fontSizeValue = "32px";
                        break;
                    }

                    default: {
                        fontSizeValue = "16px";
                        break;
                    }
                }
            }
            try {
                /* Styled XML Parser will throw an exception when it can't parse the given value
                but in html2pdf, we want to fall back to the default value of 0
                */
                return CssUtils.ParseAbsoluteLength(fontSizeValue);
            }
            catch (StyledXMLParserException) {
                return 0f;
            }
        }

        /// <summary>Parses the relative font size.</summary>
        /// <param name="relativeFontSizeValue">
        /// the relative font size value as a
        /// <see cref="System.String"/>
        /// </param>
        /// <param name="baseValue">the base value</param>
        /// <returns>
        /// the relative font size value as a
        /// <c>float</c>
        /// </returns>
        [System.ObsoleteAttribute(@"Use iText.StyledXmlParser.Css.Util.CssUtils.ParseRelativeFontSize(System.String, float) instead."
            )]
        public static float ParseRelativeFontSize(String relativeFontSizeValue, float baseValue) {
            if (CssConstants.SMALLER.Equals(relativeFontSizeValue)) {
                return (float)(baseValue / 1.2);
            }
            else {
                if (CssConstants.LARGER.Equals(relativeFontSizeValue)) {
                    return (float)(baseValue * 1.2);
                }
            }
            return CssUtils.ParseRelativeValue(relativeFontSizeValue, baseValue);
        }
    }
}
