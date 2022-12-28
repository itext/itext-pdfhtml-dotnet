/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 iText Group NV
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
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Commons.Utils;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Logs;
using iText.Kernel.Colors;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Properties;
using iText.Layout.Splitting;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Util;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply font styles.</summary>
    public sealed class FontStyleApplierUtil {
        /// <summary>The logger.</summary>
        private static readonly ILogger logger = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.FontStyleApplierUtil
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
            float em = CssDimensionParsingUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
            float rem = context.GetCssContext().GetRootFontSize();
            if (em != 0) {
                element.SetProperty(Property.FONT_SIZE, UnitValue.CreatePointValue(em));
            }
            if (cssProps.Get(CssConstants.FONT_FAMILY) != null) {
                // TODO DEVSIX-2534
                IList<String> fontFamilies = FontFamilySplitterUtil.SplitFontFamily(cssProps.Get(CssConstants.FONT_FAMILY)
                    );
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
                    TransparentColor tColor = CssDimensionParsingUtils.ParseColor(cssColorPropValue);
                    Color color = tColor.GetColor();
                    float opacity = tColor.GetOpacity();
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
                // For list items default behaviour differs from other elements:
                // only the list symbol should be aligned differently
                if (!CssConstants.LIST_ITEM.Equals(cssProps.Get(CssConstants.DISPLAY))) {
                    element.SetProperty(Property.TEXT_ALIGNMENT, TextAlignment.RIGHT);
                }
            }
            else {
                if (CssConstants.LTR.Equals(direction)) {
                    element.SetProperty(Property.BASE_DIRECTION, BaseDirection.LEFT_TO_RIGHT);
                    if (!CssConstants.LIST_ITEM.Equals(cssProps.Get(CssConstants.DISPLAY))) {
                        element.SetProperty(Property.TEXT_ALIGNMENT, TextAlignment.LEFT);
                    }
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
            bool textWrappingDisabled = CssConstants.NOWRAP.Equals(whiteSpace) || CssConstants.PRE.Equals(whiteSpace);
            element.SetProperty(Property.NO_SOFT_WRAP_INLINE, textWrappingDisabled);
            if (!textWrappingDisabled) {
                String overflowWrap = cssProps.Get(CssConstants.OVERFLOW_WRAP);
                if (CssConstants.ANYWHERE.Equals(overflowWrap)) {
                    element.SetProperty(Property.OVERFLOW_WRAP, OverflowWrapPropertyValue.ANYWHERE);
                }
                else {
                    if (CommonCssConstants.BREAK_WORD.Equals(overflowWrap)) {
                        element.SetProperty(Property.OVERFLOW_WRAP, OverflowWrapPropertyValue.BREAK_WORD);
                    }
                    else {
                        element.SetProperty(Property.OVERFLOW_WRAP, OverflowWrapPropertyValue.NORMAL);
                    }
                }
                String wordBreak = cssProps.Get(CssConstants.WORD_BREAK);
                if (CssConstants.BREAK_ALL.Equals(wordBreak)) {
                    element.SetProperty(Property.SPLIT_CHARACTERS, new BreakAllSplitCharacters());
                }
                else {
                    if (CssConstants.KEEP_ALL.Equals(wordBreak)) {
                        element.SetProperty(Property.SPLIT_CHARACTERS, new KeepAllSplitCharacters());
                    }
                    else {
                        if (CommonCssConstants.BREAK_WORD.Equals(wordBreak)) {
                            // CSS specification cite that describes the reason for overflow-wrap overriding:
                            // "For compatibility with legacy content, the word-break property also supports
                            //  a deprecated break-word keyword. When specified, this has the same effect
                            //  as word-break: normal and overflow-wrap: anywhere, regardless of the actual value
                            //  of the overflow-wrap property."
                            element.SetProperty(Property.OVERFLOW_WRAP, OverflowWrapPropertyValue.ANYWHERE);
                            element.SetProperty(Property.SPLIT_CHARACTERS, new DefaultSplitCharacters());
                        }
                        else {
                            element.SetProperty(Property.SPLIT_CHARACTERS, new DefaultSplitCharacters());
                        }
                    }
                }
            }
            TransparentColor tColor_1;
            Color textDecorationColor;
            float opacity_1 = 1f;
            String textDecorationColorProp = cssProps.Get(CssConstants.TEXT_DECORATION_COLOR);
            if (textDecorationColorProp == null || CssConstants.CURRENTCOLOR.Equals(textDecorationColorProp)) {
                if (element.GetProperty<TransparentColor>(Property.FONT_COLOR) != null) {
                    TransparentColor transparentColor = element.GetProperty<TransparentColor>(Property.FONT_COLOR);
                    textDecorationColor = transparentColor.GetColor();
                    opacity_1 = transparentColor.GetOpacity();
                }
                else {
                    textDecorationColor = ColorConstants.BLACK;
                }
            }
            else {
                if (textDecorationColorProp.StartsWith("hsl")) {
                    logger.LogError(Html2PdfLogMessageConstant.HSL_COLOR_NOT_SUPPORTED);
                    textDecorationColor = ColorConstants.BLACK;
                }
                else {
                    tColor_1 = CssDimensionParsingUtils.ParseColor(textDecorationColorProp);
                    textDecorationColor = tColor_1.GetColor();
                    opacity_1 = tColor_1.GetOpacity();
                }
            }
            String textDecorationLineProp = cssProps.Get(CssConstants.TEXT_DECORATION_LINE);
            if (textDecorationLineProp != null) {
                String[] textDecorationLines = iText.Commons.Utils.StringUtil.Split(textDecorationLineProp, "\\s+");
                IList<Underline> underlineList = new List<Underline>();
                foreach (String textDecorationLine in textDecorationLines) {
                    if (CssConstants.BLINK.Equals(textDecorationLine)) {
                        logger.LogError(Html2PdfLogMessageConstant.TEXT_DECORATION_BLINK_NOT_SUPPORTED);
                    }
                    else {
                        if (CssConstants.LINE_THROUGH.Equals(textDecorationLine)) {
                            underlineList.Add(new Underline(textDecorationColor, opacity_1, .75f, 0, 0, 1 / 4f, PdfCanvasConstants.LineCapStyle
                                .BUTT));
                        }
                        else {
                            if (CssConstants.OVERLINE.Equals(textDecorationLine)) {
                                underlineList.Add(new Underline(textDecorationColor, opacity_1, .75f, 0, 0, 9 / 10f, PdfCanvasConstants.LineCapStyle
                                    .BUTT));
                            }
                            else {
                                if (CssConstants.UNDERLINE.Equals(textDecorationLine)) {
                                    underlineList.Add(new Underline(textDecorationColor, opacity_1, .75f, 0, 0, -1 / 10f, PdfCanvasConstants.LineCapStyle
                                        .BUTT));
                                }
                                else {
                                    if (CssConstants.NONE.Equals(textDecorationLine)) {
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
            String textIndent = cssProps.Get(CommonCssConstants.TEXT_INDENT);
            if (textIndent != null) {
                UnitValue textIndentValue = CssDimensionParsingUtils.ParseLengthValueToPt(textIndent, em, rem);
                if (textIndentValue != null) {
                    if (textIndentValue.IsPointValue()) {
                        element.SetProperty(Property.FIRST_LINE_INDENT, textIndentValue.GetValue());
                    }
                    else {
                        logger.LogError(MessageFormatUtil.Format(Html2PdfLogMessageConstant.CSS_PROPERTY_IN_PERCENTS_NOT_SUPPORTED
                            , CommonCssConstants.TEXT_INDENT));
                    }
                }
            }
            String letterSpacing = cssProps.Get(CssConstants.LETTER_SPACING);
            if (letterSpacing != null && !CssConstants.NORMAL.Equals(letterSpacing)) {
                UnitValue letterSpacingValue = CssDimensionParsingUtils.ParseLengthValueToPt(letterSpacing, em, rem);
                if (letterSpacingValue.IsPointValue()) {
                    element.SetProperty(Property.CHARACTER_SPACING, letterSpacingValue.GetValue());
                }
            }
            // browsers ignore values in percents
            String wordSpacing = cssProps.Get(CssConstants.WORD_SPACING);
            if (wordSpacing != null) {
                UnitValue wordSpacingValue = CssDimensionParsingUtils.ParseLengthValueToPt(wordSpacing, em, rem);
                if (wordSpacingValue != null) {
                    if (wordSpacingValue.IsPointValue()) {
                        element.SetProperty(Property.WORD_SPACING, wordSpacingValue.GetValue());
                    }
                }
            }
            // browsers ignore values in percents
            String lineHeight = cssProps.Get(CssConstants.LINE_HEIGHT);
            SetLineHeight(element, lineHeight, em, rem);
            SetLineHeightByLeading(element, lineHeight, em, rem);
        }

        private static void SetLineHeight(IPropertyContainer elementToSet, String lineHeight, float em, float rem) {
            if (lineHeight != null && !CssConstants.NORMAL.Equals(lineHeight) && !CssConstants.AUTO.Equals(lineHeight)
                ) {
                if (CssTypesValidationUtils.IsNumber(lineHeight)) {
                    float? number = CssDimensionParsingUtils.ParseFloat(lineHeight);
                    if (number != null) {
                        elementToSet.SetProperty(Property.LINE_HEIGHT, LineHeight.CreateMultipliedValue((float)number));
                    }
                    else {
                        elementToSet.SetProperty(Property.LINE_HEIGHT, LineHeight.CreateNormalValue());
                    }
                }
                else {
                    UnitValue lineHeightValue = CssDimensionParsingUtils.ParseLengthValueToPt(lineHeight, em, rem);
                    if (lineHeightValue != null && lineHeightValue.IsPointValue()) {
                        elementToSet.SetProperty(Property.LINE_HEIGHT, LineHeight.CreateFixedValue(lineHeightValue.GetValue()));
                    }
                    else {
                        if (lineHeightValue != null) {
                            elementToSet.SetProperty(Property.LINE_HEIGHT, LineHeight.CreateMultipliedValue(lineHeightValue.GetValue()
                                 / 100f));
                        }
                        else {
                            elementToSet.SetProperty(Property.LINE_HEIGHT, LineHeight.CreateNormalValue());
                        }
                    }
                }
            }
            else {
                elementToSet.SetProperty(Property.LINE_HEIGHT, LineHeight.CreateNormalValue());
            }
        }

        private static void SetLineHeightByLeading(IPropertyContainer element, String lineHeight, float em, float 
            rem) {
            // specification does not give auto as a possible lineHeight value
            // nevertheless some browsers compute it as normal so we apply the same behaviour.
            // What's more, it's basically the same thing as if lineHeight is not set in the first place
            if (lineHeight != null && !CssConstants.NORMAL.Equals(lineHeight) && !CssConstants.AUTO.Equals(lineHeight)
                ) {
                if (CssTypesValidationUtils.IsNumber(lineHeight)) {
                    float? mult = CssDimensionParsingUtils.ParseFloat(lineHeight);
                    if (mult != null) {
                        element.SetProperty(Property.LEADING, new Leading(Leading.MULTIPLIED, (float)mult));
                    }
                }
                else {
                    UnitValue lineHeightValue = CssDimensionParsingUtils.ParseLengthValueToPt(lineHeight, em, rem);
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
    }
}
