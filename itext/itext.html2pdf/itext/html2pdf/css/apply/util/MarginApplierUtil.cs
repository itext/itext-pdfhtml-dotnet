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
using iText.Html2pdf.Css.Util;
using iText.IO.Log;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Css.Apply.Util {
    public sealed class MarginApplierUtil {
        private static readonly ILogger logger = LoggerFactory.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.MarginApplierUtil
            ));

        private MarginApplierUtil() {
        }

        public static void ApplyMargins(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             element) {
            String marginTop = cssProps.Get(CssConstants.MARGIN_TOP);
            String marginBottom = cssProps.Get(CssConstants.MARGIN_BOTTOM);
            String marginLeft = cssProps.Get(CssConstants.MARGIN_LEFT);
            String marginRight = cssProps.Get(CssConstants.MARGIN_RIGHT);
            bool isBlock = element is IBlockElement;
            bool isImage = element is Image;
            float em = CssUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
            float rem = context.GetCssContext().GetRootFontSize();
            if (isBlock || isImage) {
                TrySetMarginIfNotAuto(Property.MARGIN_TOP, marginTop, element, em, rem);
                TrySetMarginIfNotAuto(Property.MARGIN_BOTTOM, marginBottom, element, em, rem);
            }
            bool isLeftAuto = !TrySetMarginIfNotAuto(Property.MARGIN_LEFT, marginLeft, element, em, rem);
            bool isRightAuto = !TrySetMarginIfNotAuto(Property.MARGIN_RIGHT, marginRight, element, em, rem);
            if (isBlock) {
                if (isLeftAuto && isRightAuto) {
                    element.SetProperty(Property.HORIZONTAL_ALIGNMENT, HorizontalAlignment.CENTER);
                }
                else {
                    if (isLeftAuto) {
                        element.SetProperty(Property.HORIZONTAL_ALIGNMENT, HorizontalAlignment.RIGHT);
                    }
                    else {
                        if (isRightAuto) {
                            element.SetProperty(Property.HORIZONTAL_ALIGNMENT, HorizontalAlignment.LEFT);
                        }
                    }
                }
            }
        }

        private static bool TrySetMarginIfNotAuto(int marginProperty, String marginValue, IPropertyContainer element
            , float em, float rem) {
            bool isAuto = CssConstants.AUTO.Equals(marginValue);
            if (isAuto) {
                return false;
            }
            float? marginTopVal = ParseMarginValue(marginValue, em, rem);
            if (marginTopVal != null) {
                element.SetProperty(marginProperty, marginTopVal);
            }
            return true;
        }

        private static float? ParseMarginValue(String marginValString, float em, float rem) {
            UnitValue marginTopUnitVal = CssUtils.ParseLengthValueToPt(marginValString, em, rem);
            if (marginTopUnitVal != null) {
                if (!marginTopUnitVal.IsPointValue()) {
                    logger.Error(iText.Html2pdf.LogMessageConstant.MARGIN_VALUE_IN_PERCENT_NOT_SUPPORTED);
                    return null;
                }
                return marginTopUnitVal.GetValue();
            }
            else {
                return null;
            }
        }
    }
}
