/*
This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Util;
using iText.IO.Log;
using iText.Layout;
using iText.Layout.Layout;
using iText.Layout.Properties;

namespace iText.Html2pdf.Css.Apply.Util {
    public sealed class PositionApplierUtil {
        private static readonly ILogger logger = LoggerFactory.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.PositionApplierUtil
            ));

        private PositionApplierUtil() {
        }

        public static void ApplyPosition(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             element) {
            String position = cssProps.Get(CssConstants.POSITION);
            if (CssConstants.ABSOLUTE.Equals(position)) {
                element.SetProperty(Property.POSITION, LayoutPosition.ABSOLUTE);
                ApplyLeftRightTopBottom(cssProps, context, element, position);
            }
            else {
                if (CssConstants.RELATIVE.Equals(position)) {
                    element.SetProperty(Property.POSITION, LayoutPosition.RELATIVE);
                    ApplyLeftRightTopBottom(cssProps, context, element, position);
                }
                else {
                    if (CssConstants.FIXED.Equals(position)) {
                    }
                }
            }
        }

        //            element.setProperty(Property.POSITION, LayoutPosition.FIXED);
        //            float em = CssUtils.parseAbsoluteLength(cssProps.get(CssConstants.FONT_SIZE));
        //            applyLeftProperty(cssProps, element, em, Property.X);
        //            applyTopProperty(cssProps, element, em, Property.Y);
        // TODO
        private static void ApplyLeftRightTopBottom(IDictionary<String, String> cssProps, ProcessorContext context
            , IPropertyContainer element, String position) {
            float em = CssUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
            float rem = context.GetCssContext().GetRootFontSize();
            if (CssConstants.RELATIVE.Equals(position) && cssProps.ContainsKey(CssConstants.LEFT) && cssProps.ContainsKey
                (CssConstants.RIGHT)) {
                // When both the right CSS property and the left CSS property are defined, the position of the element is overspecified.
                // In that case, the left value has precedence when the container is left-to-right (that is that the right computed value is set to -left),
                // and the right value has precedence when the container is right-to-left (that is that the left computed value is set to -right).
                bool isRtl = CssConstants.RTL.Equals(cssProps.Get(CssConstants.DIRECTION));
                if (isRtl) {
                    ApplyRightProperty(cssProps, element, em, rem, Property.RIGHT);
                }
                else {
                    ApplyLeftProperty(cssProps, element, em, rem, Property.LEFT);
                }
            }
            else {
                ApplyLeftProperty(cssProps, element, em, rem, Property.LEFT);
                ApplyRightProperty(cssProps, element, em, rem, Property.RIGHT);
            }
            ApplyTopProperty(cssProps, element, em, rem, Property.TOP);
            ApplyBottomProperty(cssProps, element, em, rem, Property.BOTTOM);
        }

        private static void ApplyLeftProperty(IDictionary<String, String> cssProps, IPropertyContainer element, float
             em, float rem, int layoutPropertyMapping) {
            String left = cssProps.Get(CssConstants.LEFT);
            UnitValue leftVal = CssUtils.ParseLengthValueToPt(left, em, rem);
            if (leftVal != null) {
                if (leftVal.IsPointValue()) {
                    element.SetProperty(layoutPropertyMapping, leftVal.GetValue());
                }
                else {
                    logger.Error(String.Format(iText.Html2pdf.LogMessageConstant.CSS_PROPERTY_IN_PERCENTS_NOT_SUPPORTED, CssConstants
                        .LEFT));
                }
            }
        }

        private static void ApplyRightProperty(IDictionary<String, String> cssProps, IPropertyContainer element, float
             em, float rem, int layoutPropertyMapping) {
            String right = cssProps.Get(CssConstants.RIGHT);
            UnitValue rightVal = CssUtils.ParseLengthValueToPt(right, em, rem);
            if (rightVal != null) {
                if (rightVal.IsPointValue()) {
                    element.SetProperty(layoutPropertyMapping, rightVal.GetValue());
                }
                else {
                    logger.Error(String.Format(iText.Html2pdf.LogMessageConstant.CSS_PROPERTY_IN_PERCENTS_NOT_SUPPORTED, CssConstants
                        .RIGHT));
                }
            }
        }

        private static void ApplyTopProperty(IDictionary<String, String> cssProps, IPropertyContainer element, float
             em, float rem, int layoutPropertyMapping) {
            String top = cssProps.Get(CssConstants.TOP);
            UnitValue topVal = CssUtils.ParseLengthValueToPt(top, em, rem);
            if (topVal != null) {
                if (topVal.IsPointValue()) {
                    element.SetProperty(layoutPropertyMapping, topVal.GetValue());
                }
                else {
                    logger.Error(String.Format(iText.Html2pdf.LogMessageConstant.CSS_PROPERTY_IN_PERCENTS_NOT_SUPPORTED, CssConstants
                        .TOP));
                }
            }
        }

        private static void ApplyBottomProperty(IDictionary<String, String> cssProps, IPropertyContainer element, 
            float em, float rem, int layoutPropertyMapping) {
            String bottom = cssProps.Get(CssConstants.BOTTOM);
            UnitValue bottomVal = CssUtils.ParseLengthValueToPt(bottom, em, rem);
            if (bottomVal != null) {
                if (bottomVal.IsPointValue()) {
                    element.SetProperty(layoutPropertyMapping, bottomVal.GetValue());
                }
                else {
                    logger.Error(String.Format(iText.Html2pdf.LogMessageConstant.CSS_PROPERTY_IN_PERCENTS_NOT_SUPPORTED, CssConstants
                        .BOTTOM));
                }
            }
        }
    }
}
