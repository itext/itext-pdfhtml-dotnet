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
    public class BorderStyleApplierUtil {
        private static readonly ILogger LOGGER = LoggerFactory.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.BorderStyleApplierUtil
            ));

        private BorderStyleApplierUtil() {
        }

        public static void ApplyBorders(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             element) {
            float em = CssUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
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
        }

        public static Border[] GetBordersArray(IDictionary<String, String> styles, float em, float rem) {
            Border[] borders = new Border[4];
            Border topBorder = GetCertainBorder(styles.Get(CssConstants.BORDER_TOP_WIDTH), styles.Get(CssConstants.BORDER_TOP_STYLE
                ), styles.Get(CssConstants.BORDER_TOP_COLOR), em, rem);
            borders[0] = topBorder;
            Border rightBorder = GetCertainBorder(styles.Get(CssConstants.BORDER_RIGHT_WIDTH), styles.Get(CssConstants
                .BORDER_RIGHT_STYLE), styles.Get(CssConstants.BORDER_RIGHT_COLOR), em, rem);
            borders[1] = rightBorder;
            Border bottomBorder = GetCertainBorder(styles.Get(CssConstants.BORDER_BOTTOM_WIDTH), styles.Get(CssConstants
                .BORDER_BOTTOM_STYLE), styles.Get(CssConstants.BORDER_BOTTOM_COLOR), em, rem);
            borders[2] = bottomBorder;
            Border leftBorder = GetCertainBorder(styles.Get(CssConstants.BORDER_LEFT_WIDTH), styles.Get(CssConstants.BORDER_LEFT_STYLE
                ), styles.Get(CssConstants.BORDER_LEFT_COLOR), em, rem);
            borders[3] = leftBorder;
            return borders;
        }

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
            UnitValue unitValue = CssUtils.ParseLengthValueToPt(borderWidth, em, rem);
            if (unitValue == null) {
                return null;
            }
            if (unitValue.IsPercentValue()) {
                LOGGER.Error("border-width in percents is not supported");
                return null;
            }
            borderWidthValue = unitValue.GetValue();
            Border border = null;
            if (borderWidthValue > 0) {
                DeviceRgb color = (DeviceRgb)Color.BLACK;
                float opacity = 1f;
                if (borderColor != null) {
                    if (!CssConstants.TRANSPARENT.Equals(borderColor)) {
                        float[] rgbaColor = CssUtils.ParseRgbaColor(borderColor);
                        color = new DeviceRgb(rgbaColor[0], rgbaColor[1], rgbaColor[2]);
                        opacity = rgbaColor[3];
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
                        border = new DottedBorder(color, borderWidthValue, opacity);
                        break;
                    }

                    case CssConstants.DOUBLE: {
                        border = new DoubleBorder(color, borderWidthValue, opacity);
                        break;
                    }

                    case CssConstants.GROOVE: {
                        border = new GrooveBorder(color, borderWidthValue, opacity);
                        break;
                    }

                    case CssConstants.RIDGE: {
                        border = new RidgeBorder(color, borderWidthValue, opacity);
                        break;
                    }

                    case CssConstants.INSET: {
                        border = new InsetBorder(color, borderWidthValue, opacity);
                        break;
                    }

                    case CssConstants.OUTSET: {
                        border = new OutsetBorder(color, borderWidthValue, opacity);
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
    }
}
