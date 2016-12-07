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
    public sealed class WidthHeightApplierUtil {
        private static readonly ILogger logger = LoggerFactory.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.WidthHeightApplierUtil
            ));

        private const String HEIGHT_VALUE_IN_PERCENT_NOT_SUPPORTED = "Height value in percent not supported";

        private WidthHeightApplierUtil() {
        }

        public static void ApplyWidthHeight(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             element) {
            float em = CssUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
            String widthVal = cssProps.Get(CssConstants.WIDTH);
            if (!CssConstants.AUTO.Equals(widthVal) && widthVal != null) {
                UnitValue width = CssUtils.ParseLengthValueToPt(widthVal, em);
                element.SetProperty(Property.WIDTH, width);
            }
            String heightVal = cssProps.Get(CssConstants.HEIGHT);
            if (heightVal != null) {
                if (!CssConstants.AUTO.Equals(heightVal)) {
                    UnitValue height = CssUtils.ParseLengthValueToPt(heightVal, em);
                    if (height.IsPointValue()) {
                        element.SetProperty(Property.HEIGHT, height.GetValue());
                    }
                    else {
                        logger.Error(HEIGHT_VALUE_IN_PERCENT_NOT_SUPPORTED);
                    }
                }
            }
            String maxHeightVal = cssProps.Get(CssConstants.MAX_HEIGHT);
            if (maxHeightVal != null) {
                UnitValue height = CssUtils.ParseLengthValueToPt(maxHeightVal, em);
                if (height.IsPointValue()) {
                    element.SetProperty(Property.MAX_HEIGHT, height.GetValue());
                    if (element is Image) {
                        ((Image)element).SetAutoScale(true);
                    }
                }
                else {
                    logger.Error(HEIGHT_VALUE_IN_PERCENT_NOT_SUPPORTED);
                }
            }
            String minHeightVal = cssProps.Get(CssConstants.MIN_HEIGHT);
            if (minHeightVal != null) {
                UnitValue height = CssUtils.ParseLengthValueToPt(minHeightVal, em);
                if (height.IsPointValue()) {
                    element.SetProperty(Property.MIN_HEIGHT, height.GetValue());
                }
                else {
                    logger.Error(HEIGHT_VALUE_IN_PERCENT_NOT_SUPPORTED);
                }
            }
        }
    }
}
