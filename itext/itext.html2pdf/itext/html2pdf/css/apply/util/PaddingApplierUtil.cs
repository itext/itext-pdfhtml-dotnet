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
using iText.Layout;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Util;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply a padding.</summary>
    public sealed class PaddingApplierUtil {
        /// <summary>The logger.</summary>
        private static readonly ILog logger = LogManager.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.PaddingApplierUtil
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
                        logger.Error(iText.Html2pdf.LogMessageConstant.PADDING_VALUE_IN_PERCENT_NOT_SUPPORTED);
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
                        logger.Error(iText.Html2pdf.LogMessageConstant.PADDING_VALUE_IN_PERCENT_NOT_SUPPORTED);
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
                        logger.Error(iText.Html2pdf.LogMessageConstant.PADDING_VALUE_IN_PERCENT_NOT_SUPPORTED);
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
                        logger.Error(iText.Html2pdf.LogMessageConstant.PADDING_VALUE_IN_PERCENT_NOT_SUPPORTED);
                    }
                }
            }
        }
    }
}
