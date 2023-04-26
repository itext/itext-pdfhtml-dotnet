/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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
using iText.Html2pdf.Logs;
using iText.Layout;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Util;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply a padding.</summary>
    public sealed class PaddingApplierUtil {
        /// <summary>The logger.</summary>
        private static readonly ILogger logger = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.PaddingApplierUtil
            ));

        /// <summary>
        /// Creates a new
        /// <see cref="PaddingApplierUtil"/>
        /// instance.
        /// </summary>
        private PaddingApplierUtil() {
        }

        /// <summary>Applies paddings to an element.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the processor context</param>
        /// <param name="element">the element</param>
        public static void ApplyPaddings(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             element) {
            ApplyPaddings(cssProps, context, element, 0.0f, 0.0f);
        }

        /// <summary>Applies paddings to an element.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the processor context</param>
        /// <param name="element">the element</param>
        /// <param name="baseValueHorizontal">value used by default for horizontal dimension</param>
        /// <param name="baseValueVertical">value used by default for vertical dimension</param>
        public static void ApplyPaddings(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             element, float baseValueVertical, float baseValueHorizontal) {
            String paddingTop = cssProps.Get(CssConstants.PADDING_TOP);
            String paddingBottom = cssProps.Get(CssConstants.PADDING_BOTTOM);
            String paddingLeft = cssProps.Get(CssConstants.PADDING_LEFT);
            String paddingRight = cssProps.Get(CssConstants.PADDING_RIGHT);
            float em = CssDimensionParsingUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
            float rem = context.GetCssContext().GetRootFontSize();
            UnitValue paddingTopVal = CssDimensionParsingUtils.ParseLengthValueToPt(paddingTop, em, rem);
            UnitValue paddingBottomVal = CssDimensionParsingUtils.ParseLengthValueToPt(paddingBottom, em, rem);
            UnitValue paddingLeftVal = CssDimensionParsingUtils.ParseLengthValueToPt(paddingLeft, em, rem);
            UnitValue paddingRightVal = CssDimensionParsingUtils.ParseLengthValueToPt(paddingRight, em, rem);
            if (paddingTopVal != null) {
                if (paddingTopVal.IsPointValue()) {
                    element.SetProperty(Property.PADDING_TOP, paddingTopVal);
                }
                else {
                    if (baseValueVertical != 0.0f) {
                        element.SetProperty(Property.PADDING_TOP, new UnitValue(UnitValue.POINT, baseValueVertical * paddingTopVal
                            .GetValue() * 0.01f));
                    }
                    else {
                        logger.LogError(Html2PdfLogMessageConstant.PADDING_VALUE_IN_PERCENT_NOT_SUPPORTED);
                    }
                }
            }
            if (paddingBottomVal != null) {
                if (paddingBottomVal.IsPointValue()) {
                    element.SetProperty(Property.PADDING_BOTTOM, paddingBottomVal);
                }
                else {
                    if (baseValueVertical != 0.0f) {
                        element.SetProperty(Property.PADDING_BOTTOM, new UnitValue(UnitValue.POINT, baseValueVertical * paddingBottomVal
                            .GetValue() * 0.01f));
                    }
                    else {
                        logger.LogError(Html2PdfLogMessageConstant.PADDING_VALUE_IN_PERCENT_NOT_SUPPORTED);
                    }
                }
            }
            if (paddingLeftVal != null) {
                if (paddingLeftVal.IsPointValue()) {
                    element.SetProperty(Property.PADDING_LEFT, paddingLeftVal);
                }
                else {
                    if (baseValueHorizontal != 0.0f) {
                        element.SetProperty(Property.PADDING_LEFT, new UnitValue(UnitValue.POINT, baseValueHorizontal * paddingLeftVal
                            .GetValue() * 0.01f));
                    }
                    else {
                        logger.LogError(Html2PdfLogMessageConstant.PADDING_VALUE_IN_PERCENT_NOT_SUPPORTED);
                    }
                }
            }
            if (paddingRightVal != null) {
                if (paddingRightVal.IsPointValue()) {
                    element.SetProperty(Property.PADDING_RIGHT, paddingRightVal);
                }
                else {
                    if (baseValueHorizontal != 0.0f) {
                        element.SetProperty(Property.PADDING_RIGHT, new UnitValue(UnitValue.POINT, baseValueHorizontal * paddingRightVal
                            .GetValue() * 0.01f));
                    }
                    else {
                        logger.LogError(Html2PdfLogMessageConstant.PADDING_VALUE_IN_PERCENT_NOT_SUPPORTED);
                    }
                }
            }
        }
    }
}
