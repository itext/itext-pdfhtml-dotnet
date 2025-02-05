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

        private const float TEXT_DECORATION_LINE_DEFAULT_THICKNESS = .75F;

        private const float TEXT_DECORATION_LINE_THROUGH_Y_POS = 1 / 4F;

        private const float TEXT_DECORATION_LINE_OVER_Y_POS = 9 / 10F;

        private const float TEXT_DECORATION_LIN_UNDER_Y_POS = -1 / 10F;

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
            SetTextDecoration(element, cssProps);
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

        private static void SetTextDecoration(IPropertyContainer element, IDictionary<String, String> cssProps) {
            String[] props = new String[] { null };
            String unparsedProps = cssProps.Get(CommonCssConstants.TEXT_DECORATION_COLOR);
            if (unparsedProps != null && !String.IsNullOrEmpty(unparsedProps.Trim())) {
                props = iText.Commons.Utils.StringUtil.Split(cssProps.Get(CommonCssConstants.TEXT_DECORATION_COLOR), "\\s+"
                    );
            }
            IList<float> opacityList = new List<float>(props.Length);
            IList<Color> colorList = new List<Color>(props.Length);
            foreach (String textDecorationColorProp in props) {
                TransparentColor tColor;
                Color textDecorationColor;
                float opacity = 1f;
                if (textDecorationColorProp == null || CommonCssConstants.CURRENTCOLOR.Equals(textDecorationColorProp)) {
                    if (element.GetProperty<TransparentColor>(Property.FONT_COLOR) != null) {
                        TransparentColor transparentColor = element.GetProperty<TransparentColor>(Property.FONT_COLOR);
                        textDecorationColor = transparentColor.GetColor();
                        opacity = transparentColor.GetOpacity();
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
                        tColor = CssDimensionParsingUtils.ParseColor(textDecorationColorProp);
                        textDecorationColor = tColor.GetColor();
                        opacity = tColor.GetOpacity();
                    }
                }
                opacityList.Add(opacity);
                colorList.Add(textDecorationColor);
            }
            String textDecorationLineProp = cssProps.Get(CommonCssConstants.TEXT_DECORATION_LINE);
            if (textDecorationLineProp == null) {
                return;
            }
            String[] textDecorationArray = iText.Commons.Utils.StringUtil.Split(textDecorationLineProp, "\\s+");
            IList<Underline> underlineList = new List<Underline>();
            for (int currentIndex = 0; currentIndex < textDecorationArray.Length; currentIndex++) {
                float opacity = opacityList.Count - 1 > currentIndex ? opacityList[currentIndex] : opacityList[opacityList
                    .Count - 1];
                Color color = colorList.Count - 1 > currentIndex ? colorList[currentIndex] : colorList[colorList.Count - 1
                    ];
                String line = textDecorationArray[currentIndex];
                if (CommonCssConstants.BLINK.Equals(line)) {
                    logger.LogError(Html2PdfLogMessageConstant.TEXT_DECORATION_BLINK_NOT_SUPPORTED);
                }
                else {
                    if (CommonCssConstants.LINE_THROUGH.Equals(line)) {
                        underlineList.Add(new Underline(color, opacity, TEXT_DECORATION_LINE_DEFAULT_THICKNESS, 0, 0, TEXT_DECORATION_LINE_THROUGH_Y_POS
                            , PdfCanvasConstants.LineCapStyle.BUTT));
                    }
                    else {
                        if (CommonCssConstants.OVERLINE.Equals(line)) {
                            underlineList.Add(new Underline(color, opacity, TEXT_DECORATION_LINE_DEFAULT_THICKNESS, 0, 0, TEXT_DECORATION_LINE_OVER_Y_POS
                                , PdfCanvasConstants.LineCapStyle.BUTT));
                        }
                        else {
                            if (CommonCssConstants.UNDERLINE.Equals(line)) {
                                underlineList.Add(new Underline(color, opacity, TEXT_DECORATION_LINE_DEFAULT_THICKNESS, 0, 0, TEXT_DECORATION_LIN_UNDER_Y_POS
                                    , PdfCanvasConstants.LineCapStyle.BUTT));
                            }
                            else {
                                if (CommonCssConstants.NONE.Equals(line)) {
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
