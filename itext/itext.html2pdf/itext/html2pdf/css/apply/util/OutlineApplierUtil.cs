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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Kernel.Colors;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Resolve;
using iText.StyledXmlParser.Css.Util;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utility class to apply outline properties.</summary>
    public class OutlineApplierUtil {
        /// <summary>The logger.</summary>
        private static readonly ILogger LOGGER = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.OutlineApplierUtil
            ));

        /// <summary>
        /// Creates a new
        /// <see cref="OutlineApplierUtil"/>
        /// instance.
        /// </summary>
        private OutlineApplierUtil() {
        }

        /// <summary>Applies outlines to an element.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the Processor context</param>
        /// <param name="element">the element</param>
        public static void ApplyOutlines(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             element) {
            float em = CssDimensionParsingUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
            float rem = context.GetCssContext().GetRootFontSize();
            Border outline = GetCertainBorder(cssProps.Get(CssConstants.OUTLINE_WIDTH), cssProps.Get(CssConstants.OUTLINE_STYLE
                ), GetSpecificOutlineColorOrDefaultColor(cssProps, CssConstants.OUTLINE_COLOR), em, rem);
            if (outline != null) {
                element.SetProperty(Property.OUTLINE, outline);
            }
            if (cssProps.Get(CssConstants.OUTLINE_OFFSET) != null && element.GetProperty<Border>(Property.OUTLINE) != 
                null) {
                UnitValue unitValue = CssDimensionParsingUtils.ParseLengthValueToPt(cssProps.Get(CssConstants.OUTLINE_OFFSET
                    ), em, rem);
                if (unitValue != null) {
                    if (unitValue.IsPercentValue()) {
                        LOGGER.LogError("outline-width in percents is not supported");
                    }
                    else {
                        if (unitValue.GetValue() != 0) {
                            element.SetProperty(Property.OUTLINE_OFFSET, unitValue.GetValue());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates a
        /// <see cref="iText.Layout.Borders.Border"/>
        /// instance based on specific properties.
        /// </summary>
        /// <param name="outlineWidth">the outline width</param>
        /// <param name="outlineStyle">the outline style</param>
        /// <param name="outlineColor">the outline color</param>
        /// <param name="em">the em value</param>
        /// <param name="rem">the root em value</param>
        /// <returns>the border</returns>
        public static Border GetCertainBorder(String outlineWidth, String outlineStyle, String outlineColor, float
             em, float rem) {
            if (outlineStyle == null || CssConstants.NONE.Equals(outlineStyle)) {
                return null;
            }
            if (outlineWidth == null) {
                outlineWidth = CssDefaults.GetDefaultValue(CssConstants.OUTLINE_WIDTH);
            }
            float outlineWidthValue;
            if (CssConstants.BORDER_WIDTH_VALUES.Contains(outlineWidth)) {
                if (CssConstants.THIN.Equals(outlineWidth)) {
                    outlineWidth = "1px";
                }
                else {
                    if (CssConstants.MEDIUM.Equals(outlineWidth)) {
                        outlineWidth = "2px";
                    }
                    else {
                        if (CssConstants.THICK.Equals(outlineWidth)) {
                            outlineWidth = "3px";
                        }
                    }
                }
            }
            UnitValue unitValue = CssDimensionParsingUtils.ParseLengthValueToPt(outlineWidth, em, rem);
            if (unitValue == null) {
                return null;
            }
            if (unitValue.IsPercentValue()) {
                LOGGER.LogError("outline-width in percents is not supported");
                return null;
            }
            outlineWidthValue = unitValue.GetValue();
            Border outline = null;
            if (outlineWidthValue > 0) {
                Color color = ColorConstants.BLACK;
                float opacity = 1f;
                if (outlineColor != null) {
                    if (!CssConstants.TRANSPARENT.Equals(outlineColor)) {
                        TransparentColor tColor = CssDimensionParsingUtils.ParseColor(outlineColor);
                        color = tColor.GetColor();
                        opacity = tColor.GetOpacity();
                    }
                    else {
                        opacity = 0f;
                    }
                }
                else {
                    if (CssConstants.GROOVE.Equals(outlineStyle) || CssConstants.RIDGE.Equals(outlineStyle) || CssConstants.INSET
                        .Equals(outlineStyle) || CssConstants.OUTSET.Equals(outlineStyle)) {
                        color = new DeviceRgb(212, 208, 200);
                    }
                }
                switch (outlineStyle) {
                    case CssConstants.SOLID:
                    case CssConstants.AUTO: {
                        outline = new SolidBorder(color, outlineWidthValue, opacity);
                        break;
                    }

                    case CssConstants.DASHED: {
                        outline = new DashedBorder(color, outlineWidthValue, opacity);
                        break;
                    }

                    case CssConstants.DOTTED: {
                        outline = new DottedBorder(color, outlineWidthValue, opacity);
                        break;
                    }

                    case CssConstants.DOUBLE: {
                        outline = new DoubleBorder(color, outlineWidthValue, opacity);
                        break;
                    }

                    case CssConstants.GROOVE: {
                        if (color is DeviceRgb) {
                            outline = new GrooveBorder((DeviceRgb)color, outlineWidthValue, opacity);
                        }
                        if (color is DeviceCmyk) {
                            outline = new GrooveBorder((DeviceCmyk)color, outlineWidthValue, opacity);
                        }
                        break;
                    }

                    case CssConstants.RIDGE: {
                        if (color is DeviceRgb) {
                            outline = new RidgeBorder((DeviceRgb)color, outlineWidthValue, opacity);
                        }
                        if (color is DeviceCmyk) {
                            outline = new RidgeBorder((DeviceCmyk)color, outlineWidthValue, opacity);
                        }
                        break;
                    }

                    case CssConstants.INSET: {
                        if (color is DeviceRgb) {
                            outline = new InsetBorder((DeviceRgb)color, outlineWidthValue, opacity);
                        }
                        if (color is DeviceCmyk) {
                            outline = new InsetBorder((DeviceCmyk)color, outlineWidthValue, opacity);
                        }
                        break;
                    }

                    case CssConstants.OUTSET: {
                        if (color is DeviceRgb) {
                            outline = new OutsetBorder((DeviceRgb)color, outlineWidthValue, opacity);
                        }
                        if (color is DeviceCmyk) {
                            outline = new OutsetBorder((DeviceCmyk)color, outlineWidthValue, opacity);
                        }
                        break;
                    }

                    default: {
                        outline = null;
                        break;
                    }
                }
            }
            return outline;
        }

        private static String GetSpecificOutlineColorOrDefaultColor(IDictionary<String, String> styles, String specificOutlineColorProperty
            ) {
            String outlineColor = styles.Get(specificOutlineColorProperty);
            if (outlineColor == null || CssConstants.CURRENTCOLOR.Equals(outlineColor)) {
                outlineColor = styles.Get(CssConstants.COLOR);
            }
            else {
                if (CssConstants.INVERT.Equals(outlineColor)) {
                    LOGGER.LogWarning("Invert color for outline is not supported");
                    outlineColor = styles.Get(CssConstants.COLOR);
                }
            }
            return outlineColor;
        }
    }
}
