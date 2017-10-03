/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2017 iText Group NV
    Authors: iText Software.

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
    address: sales@itextpdf.com */
using System;
using System.Collections.Generic;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Resolve;
using iText.Html2pdf.Css.Util;
using iText.IO.Log;
using iText.Kernel.Colors;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Properties;

namespace iText.Html2pdf.Css.Apply.Util {
    public class OutlineApplierUtil {
        /// <summary>The logger.</summary>
        private static readonly ILogger LOGGER = LoggerFactory.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.OutlineApplierUtil
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
            float em = CssUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
            float rem = context.GetCssContext().GetRootFontSize();
            Border outline = GetCertainBorder(cssProps.Get(CssConstants.OUTLINE_WIDTH), cssProps.Get(CssConstants.OUTLINE_STYLE
                ), GetSpecificOutlineColorOrDefaultColor(cssProps, CssConstants.OUTLINE_COLOR), em, rem);
            if (outline != null) {
                element.SetProperty(Property.OUTLINE, outline);
            }
            if (cssProps.Get(CssConstants.OUTLINE_OFFSET) != null && element.GetProperty<Border>(Property.OUTLINE) != 
                null) {
                UnitValue unitValue = CssUtils.ParseLengthValueToPt(cssProps.Get(CssConstants.OUTLINE_OFFSET), em, rem);
                if (unitValue != null) {
                    if (unitValue.IsPercentValue()) {
                        LOGGER.Error("outline-width in percents is not supported");
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
            UnitValue unitValue = CssUtils.ParseLengthValueToPt(outlineWidth, em, rem);
            if (unitValue == null) {
                return null;
            }
            if (unitValue.IsPercentValue()) {
                LOGGER.Error("outline-width in percents is not supported");
                return null;
            }
            outlineWidthValue = unitValue.GetValue();
            Border outline = null;
            if (outlineWidthValue > 0) {
                DeviceRgb color = (DeviceRgb)Color.BLACK;
                float opacity = 1f;
                if (outlineColor != null) {
                    if (!CssConstants.TRANSPARENT.Equals(outlineColor)) {
                        float[] rgbaColor = CssUtils.ParseRgbaColor(outlineColor);
                        color = new DeviceRgb(rgbaColor[0], rgbaColor[1], rgbaColor[2]);
                        opacity = rgbaColor[3];
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
                        outline = new GrooveBorder(color, outlineWidthValue, opacity);
                        break;
                    }

                    case CssConstants.RIDGE: {
                        outline = new RidgeBorder(color, outlineWidthValue, opacity);
                        break;
                    }

                    case CssConstants.INSET: {
                        outline = new InsetBorder(color, outlineWidthValue, opacity);
                        break;
                    }

                    case CssConstants.OUTSET: {
                        outline = new OutsetBorder(color, outlineWidthValue, opacity);
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
            if (outlineColor == null || outlineColor.Equals("currentcolor")) {
                outlineColor = styles.Get(CssConstants.COLOR);
            }
            else {
                if (CssConstants.INVERT.Equals(outlineColor)) {
                    LOGGER.Warn("Invert color for outline is not supported");
                    outlineColor = styles.Get(CssConstants.COLOR);
                }
            }
            return outlineColor;
        }
    }
}
