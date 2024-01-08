/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Kernel.Colors;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Resolve;
using iText.StyledXmlParser.Css.Util;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply border styles.</summary>
    public class BorderStyleApplierUtil {
        /// <summary>
        /// Creates a new
        /// <see cref="BorderStyleApplierUtil"/>
        /// instance.
        /// </summary>
        private BorderStyleApplierUtil() {
        }

        /// <summary>Applies borders to an element.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the Processor context</param>
        /// <param name="element">the element</param>
        public static void ApplyBorders(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             element) {
            float em = CssDimensionParsingUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
            float rem = context.GetCssContext().GetRootFontSize();
            Border[] bordersArray = GetBordersArray(cssProps, em, rem);
            if (bordersArray[0] != null) {
                element.SetProperty(Property.BORDER_TOP, bordersArray[0]);
            }
            if (bordersArray[1] != null) {
                element.SetProperty(Property.BORDER_RIGHT, bordersArray[1]);
            }
            if (bordersArray[2] != null) {
                element.SetProperty(Property.BORDER_BOTTOM, bordersArray[2]);
            }
            if (bordersArray[3] != null) {
                element.SetProperty(Property.BORDER_LEFT, bordersArray[3]);
            }
            BorderRadius[] borderRadii = GetBorderRadiiArray(cssProps, em, rem);
            if (borderRadii[0] != null) {
                element.SetProperty(Property.BORDER_TOP_LEFT_RADIUS, borderRadii[0]);
            }
            if (borderRadii[1] != null) {
                element.SetProperty(Property.BORDER_TOP_RIGHT_RADIUS, borderRadii[1]);
            }
            if (borderRadii[2] != null) {
                element.SetProperty(Property.BORDER_BOTTOM_RIGHT_RADIUS, borderRadii[2]);
            }
            if (borderRadii[3] != null) {
                element.SetProperty(Property.BORDER_BOTTOM_LEFT_RADIUS, borderRadii[3]);
            }
        }

        /// <summary>Gets the array that defines the borders.</summary>
        /// <param name="styles">the styles mapping</param>
        /// <param name="em">the em value</param>
        /// <param name="rem">the root em value</param>
        /// <returns>the borders array</returns>
        public static Border[] GetBordersArray(IDictionary<String, String> styles, float em, float rem) {
            Border[] borders = new Border[4];
            Border topBorder = GetCertainBorder(styles.Get(CssConstants.BORDER_TOP_WIDTH), styles.Get(CssConstants.BORDER_TOP_STYLE
                ), GetSpecificBorderColorOrDefaultColor(styles, CssConstants.BORDER_TOP_COLOR), em, rem);
            borders[0] = topBorder;
            Border rightBorder = GetCertainBorder(styles.Get(CssConstants.BORDER_RIGHT_WIDTH), styles.Get(CssConstants
                .BORDER_RIGHT_STYLE), GetSpecificBorderColorOrDefaultColor(styles, CssConstants.BORDER_RIGHT_COLOR), em
                , rem);
            borders[1] = rightBorder;
            Border bottomBorder = GetCertainBorder(styles.Get(CssConstants.BORDER_BOTTOM_WIDTH), styles.Get(CssConstants
                .BORDER_BOTTOM_STYLE), GetSpecificBorderColorOrDefaultColor(styles, CssConstants.BORDER_BOTTOM_COLOR), 
                em, rem);
            borders[2] = bottomBorder;
            Border leftBorder = GetCertainBorder(styles.Get(CssConstants.BORDER_LEFT_WIDTH), styles.Get(CssConstants.BORDER_LEFT_STYLE
                ), GetSpecificBorderColorOrDefaultColor(styles, CssConstants.BORDER_LEFT_COLOR), em, rem);
            borders[3] = leftBorder;
            return borders;
        }

        /// <summary>
        /// Creates a
        /// <see cref="iText.Layout.Borders.Border"/>
        /// instance based on specific properties.
        /// </summary>
        /// <param name="borderWidth">the border width</param>
        /// <param name="borderStyle">the border style</param>
        /// <param name="borderColor">the border color</param>
        /// <param name="em">the em value</param>
        /// <param name="rem">the root em value</param>
        /// <returns>the border</returns>
        public static Border GetCertainBorder(String borderWidth, String borderStyle, String borderColor, float em
            , float rem) {
            if (borderStyle == null || CssConstants.NONE.Equals(borderStyle)) {
                return null;
            }
            if (borderWidth == null) {
                borderWidth = CssDefaults.GetDefaultValue(CssConstants.BORDER_WIDTH);
            }
            float borderWidthValue;
            if (CssConstants.BORDER_WIDTH_VALUES.Contains(borderWidth)) {
                if (CssConstants.THIN.Equals(borderWidth)) {
                    borderWidth = "1px";
                }
                else {
                    if (CssConstants.MEDIUM.Equals(borderWidth)) {
                        borderWidth = "2px";
                    }
                    else {
                        if (CssConstants.THICK.Equals(borderWidth)) {
                            borderWidth = "3px";
                        }
                    }
                }
            }
            UnitValue unitValue = CssDimensionParsingUtils.ParseLengthValueToPt(borderWidth, em, rem);
            if (unitValue == null) {
                return null;
            }
            if (unitValue.IsPercentValue()) {
                return null;
            }
            borderWidthValue = unitValue.GetValue();
            Border border = null;
            if (borderWidthValue > 0) {
                Color color = ColorConstants.BLACK;
                float opacity = 1f;
                if (borderColor != null) {
                    if (!CssConstants.TRANSPARENT.Equals(borderColor)) {
                        TransparentColor tColor = CssDimensionParsingUtils.ParseColor(borderColor);
                        color = tColor.GetColor();
                        opacity = tColor.GetOpacity();
                    }
                    else {
                        opacity = 0f;
                    }
                }
                else {
                    if (CssConstants.GROOVE.Equals(borderStyle) || CssConstants.RIDGE.Equals(borderStyle) || CssConstants.INSET
                        .Equals(borderStyle) || CssConstants.OUTSET.Equals(borderStyle)) {
                        color = new DeviceRgb(212, 208, 200);
                    }
                }
                switch (borderStyle) {
                    case CssConstants.SOLID: {
                        border = new SolidBorder(color, borderWidthValue, opacity);
                        break;
                    }

                    case CssConstants.DASHED: {
                        border = new DashedBorder(color, borderWidthValue, opacity);
                        break;
                    }

                    case CssConstants.DOTTED: {
                        border = new RoundDotsBorder(color, borderWidthValue, opacity);
                        break;
                    }

                    case CssConstants.DOUBLE: {
                        border = new DoubleBorder(color, borderWidthValue, opacity);
                        break;
                    }

                    case CssConstants.GROOVE: {
                        if (color is DeviceRgb) {
                            border = new GrooveBorder((DeviceRgb)color, borderWidthValue, opacity);
                        }
                        if (color is DeviceCmyk) {
                            border = new GrooveBorder((DeviceCmyk)color, borderWidthValue, opacity);
                        }
                        break;
                    }

                    case CssConstants.RIDGE: {
                        if (color is DeviceRgb) {
                            border = new RidgeBorder((DeviceRgb)color, borderWidthValue, opacity);
                        }
                        if (color is DeviceCmyk) {
                            border = new RidgeBorder((DeviceCmyk)color, borderWidthValue, opacity);
                        }
                        break;
                    }

                    case CssConstants.INSET: {
                        if (color is DeviceRgb) {
                            border = new InsetBorder((DeviceRgb)color, borderWidthValue, opacity);
                        }
                        if (color is DeviceCmyk) {
                            border = new InsetBorder((DeviceCmyk)color, borderWidthValue, opacity);
                        }
                        break;
                    }

                    case CssConstants.OUTSET: {
                        if (color is DeviceRgb) {
                            border = new OutsetBorder((DeviceRgb)color, borderWidthValue, opacity);
                        }
                        if (color is DeviceCmyk) {
                            border = new OutsetBorder((DeviceCmyk)color, borderWidthValue, opacity);
                        }
                        break;
                    }

                    default: {
                        border = null;
                        break;
                    }
                }
            }
            return border;
        }

        /// <summary>Gets the array that defines the borders.</summary>
        /// <param name="styles">the styles mapping</param>
        /// <param name="em">the em value</param>
        /// <param name="rem">the root em value</param>
        /// <returns>the borders array</returns>
        public static BorderRadius[] GetBorderRadiiArray(IDictionary<String, String> styles, float em, float rem) {
            BorderRadius[] borderRadii = new BorderRadius[4];
            BorderRadius borderRadius = null;
            UnitValue borderRadiusUV = CssDimensionParsingUtils.ParseLengthValueToPt(styles.Get(CssConstants.BORDER_RADIUS
                ), em, rem);
            if (null != borderRadiusUV) {
                borderRadius = new BorderRadius(borderRadiusUV);
            }
            UnitValue[] borderTopLeftRadiusUV = CssDimensionParsingUtils.ParseSpecificCornerBorderRadius(styles.Get(CssConstants
                .BORDER_TOP_LEFT_RADIUS), em, rem);
            borderRadii[0] = null == borderTopLeftRadiusUV ? borderRadius : new BorderRadius(borderTopLeftRadiusUV[0], 
                borderTopLeftRadiusUV[1]);
            UnitValue[] borderTopRightRadiusUV = CssDimensionParsingUtils.ParseSpecificCornerBorderRadius(styles.Get(CssConstants
                .BORDER_TOP_RIGHT_RADIUS), em, rem);
            borderRadii[1] = null == borderTopRightRadiusUV ? borderRadius : new BorderRadius(borderTopRightRadiusUV[0
                ], borderTopRightRadiusUV[1]);
            UnitValue[] borderBottomRightRadiusUV = CssDimensionParsingUtils.ParseSpecificCornerBorderRadius(styles.Get
                (CssConstants.BORDER_BOTTOM_RIGHT_RADIUS), em, rem);
            borderRadii[2] = null == borderBottomRightRadiusUV ? borderRadius : new BorderRadius(borderBottomRightRadiusUV
                [0], borderBottomRightRadiusUV[1]);
            UnitValue[] borderBottomLeftRadiusUV = CssDimensionParsingUtils.ParseSpecificCornerBorderRadius(styles.Get
                (CssConstants.BORDER_BOTTOM_LEFT_RADIUS), em, rem);
            borderRadii[3] = null == borderBottomLeftRadiusUV ? borderRadius : new BorderRadius(borderBottomLeftRadiusUV
                [0], borderBottomLeftRadiusUV[1]);
            return borderRadii;
        }

        private static String GetSpecificBorderColorOrDefaultColor(IDictionary<String, String> styles, String specificBorderColorProperty
            ) {
            String borderColor = styles.Get(specificBorderColorProperty);
            if (borderColor == null || CssConstants.CURRENTCOLOR.Equals(borderColor)) {
                borderColor = styles.Get(CssConstants.COLOR);
            }
            return borderColor;
        }
    }
}
