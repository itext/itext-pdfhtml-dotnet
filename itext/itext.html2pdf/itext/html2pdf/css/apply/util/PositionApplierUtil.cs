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
using iText.Layout;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Util;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply a position.</summary>
    public sealed class PositionApplierUtil {
        /// <summary>The logger.</summary>
        private static readonly ILogger logger = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.PositionApplierUtil
            ));

        /// <summary>
        /// Creates a new
        /// <see cref="PositionApplierUtil"/>
        /// instance.
        /// </summary>
        private PositionApplierUtil() {
        }

        /// <summary>Applies a position to an element.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the propertiescontext</param>
        /// <param name="element">the element</param>
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
        //            float em = CssUtils.parseAbsoluteLength(cssProps.get(CommonCssConstants.FONT_SIZE));
        //            applyLeftProperty(cssProps, element, em, Property.X);
        //            applyTopProperty(cssProps, element, em, Property.Y);
        // TODO DEVSIX-4104 support "fixed" value of position property
        /// <summary>Applies left, right, top, and bottom properties.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the processor context</param>
        /// <param name="element">the element</param>
        /// <param name="position">the position</param>
        private static void ApplyLeftRightTopBottom(IDictionary<String, String> cssProps, ProcessorContext context
            , IPropertyContainer element, String position) {
            float em = CssDimensionParsingUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
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

        /// <summary>Applies the "left" property.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="element">the element</param>
        /// <param name="em">the em value</param>
        /// <param name="rem">the root em value</param>
        /// <param name="layoutPropertyMapping">the layout property mapping</param>
        private static void ApplyLeftProperty(IDictionary<String, String> cssProps, IPropertyContainer element, float
             em, float rem, int layoutPropertyMapping) {
            String left = cssProps.Get(CssConstants.LEFT);
            UnitValue leftVal = CssDimensionParsingUtils.ParseLengthValueToPt(left, em, rem);
            if (leftVal != null) {
                if (leftVal.IsPointValue()) {
                    element.SetProperty(layoutPropertyMapping, leftVal.GetValue());
                }
                else {
                    logger.LogError(MessageFormatUtil.Format(Html2PdfLogMessageConstant.CSS_PROPERTY_IN_PERCENTS_NOT_SUPPORTED
                        , CommonCssConstants.LEFT));
                }
            }
        }

        /// <summary>Applies the "right" property.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="element">the element</param>
        /// <param name="em">the em value</param>
        /// <param name="rem">the root em value</param>
        /// <param name="layoutPropertyMapping">the layout property mapping</param>
        private static void ApplyRightProperty(IDictionary<String, String> cssProps, IPropertyContainer element, float
             em, float rem, int layoutPropertyMapping) {
            String right = cssProps.Get(CssConstants.RIGHT);
            UnitValue rightVal = CssDimensionParsingUtils.ParseLengthValueToPt(right, em, rem);
            if (rightVal != null) {
                if (rightVal.IsPointValue()) {
                    element.SetProperty(layoutPropertyMapping, rightVal.GetValue());
                }
                else {
                    logger.LogError(MessageFormatUtil.Format(Html2PdfLogMessageConstant.CSS_PROPERTY_IN_PERCENTS_NOT_SUPPORTED
                        , CommonCssConstants.RIGHT));
                }
            }
        }

        /// <summary>Applies the "top" property.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="element">the element</param>
        /// <param name="em">the em value</param>
        /// <param name="rem">the root em value</param>
        /// <param name="layoutPropertyMapping">the layout property mapping</param>
        private static void ApplyTopProperty(IDictionary<String, String> cssProps, IPropertyContainer element, float
             em, float rem, int layoutPropertyMapping) {
            String top = cssProps.Get(CssConstants.TOP);
            UnitValue topVal = CssDimensionParsingUtils.ParseLengthValueToPt(top, em, rem);
            if (topVal != null) {
                if (topVal.IsPointValue()) {
                    element.SetProperty(layoutPropertyMapping, topVal.GetValue());
                }
                else {
                    logger.LogError(MessageFormatUtil.Format(Html2PdfLogMessageConstant.CSS_PROPERTY_IN_PERCENTS_NOT_SUPPORTED
                        , CommonCssConstants.TOP));
                }
            }
        }

        /// <summary>Applies the "bottom" property.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="element">the element</param>
        /// <param name="em">the em value</param>
        /// <param name="rem">the root em value</param>
        /// <param name="layoutPropertyMapping">the layout property mapping</param>
        private static void ApplyBottomProperty(IDictionary<String, String> cssProps, IPropertyContainer element, 
            float em, float rem, int layoutPropertyMapping) {
            String bottom = cssProps.Get(CssConstants.BOTTOM);
            UnitValue bottomVal = CssDimensionParsingUtils.ParseLengthValueToPt(bottom, em, rem);
            if (bottomVal != null) {
                if (bottomVal.IsPointValue()) {
                    element.SetProperty(layoutPropertyMapping, bottomVal.GetValue());
                }
                else {
                    logger.LogError(MessageFormatUtil.Format(Html2PdfLogMessageConstant.CSS_PROPERTY_IN_PERCENTS_NOT_SUPPORTED
                        , CommonCssConstants.BOTTOM));
                }
            }
        }
    }
}
