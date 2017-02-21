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

        private WidthHeightApplierUtil() {
        }

        public static void ApplyWidthHeight(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             element) {
            float em = CssUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
            float rem = context.GetCssContext().GetRootFontSize();
            String widthVal = cssProps.Get(CssConstants.WIDTH);
            if (!CssConstants.AUTO.Equals(widthVal) && widthVal != null) {
                UnitValue width = CssUtils.ParseLengthValueToPt(widthVal, em, rem);
                element.SetProperty(Property.WIDTH, width);
            }
            // TODO consider display css property
            bool applyToTable = element is Table;
            UnitValue height = null;
            String heightVal = cssProps.Get(CssConstants.HEIGHT);
            if (heightVal != null) {
                if (!CssConstants.AUTO.Equals(heightVal)) {
                    height = CssUtils.ParseLengthValueToPt(heightVal, em, rem);
                    if (height != null) {
                        if (height.IsPointValue()) {
                            // For tables, max height does not have any effect. The height value will be used when
                            // calculating effective min height value below
                            if (!applyToTable) {
                                element.SetProperty(Property.HEIGHT, height.GetValue());
                            }
                        }
                        else {
                            logger.Error(iText.Html2pdf.LogMessageConstant.HEIGHT_VALUE_IN_PERCENT_NOT_SUPPORTED);
                        }
                    }
                }
            }
            String maxHeightVal = cssProps.Get(CssConstants.MAX_HEIGHT);
            float maxHeightToApply = 0;
            if (maxHeightVal != null) {
                UnitValue maxHeight = CssUtils.ParseLengthValueToPt(maxHeightVal, em, rem);
                if (maxHeight != null) {
                    if (maxHeight.IsPointValue()) {
                        // For tables, max height does not have any effect. See also comments below when MIN_HEIGHT is applied.
                        if (!applyToTable) {
                            maxHeightToApply = maxHeight.GetValue();
                        }
                    }
                    else {
                        logger.Error(iText.Html2pdf.LogMessageConstant.HEIGHT_VALUE_IN_PERCENT_NOT_SUPPORTED);
                    }
                }
            }
            if (maxHeightToApply > 0) {
                element.SetProperty(Property.MAX_HEIGHT, maxHeightToApply);
            }
            String minHeightVal = cssProps.Get(CssConstants.MIN_HEIGHT);
            float minHeightToApply = 0;
            if (minHeightVal != null) {
                UnitValue minHeight = CssUtils.ParseLengthValueToPt(minHeightVal, em, rem);
                if (minHeight != null) {
                    if (minHeight.IsPointValue()) {
                        minHeightToApply = minHeight.GetValue();
                    }
                    else {
                        logger.Error(iText.Html2pdf.LogMessageConstant.HEIGHT_VALUE_IN_PERCENT_NOT_SUPPORTED);
                    }
                }
            }
            // The height of a table is given by the 'height' property for the 'table' or 'inline-table' element.
            // A value of 'auto' means that the height is the sum of the row heights plus any cell spacing or borders.
            // Any other value is treated as a minimum height. CSS 2.1 does not define how extra space is distributed when
            // the 'height' property causes the table to be taller than it otherwise would be.
            if (applyToTable && height != null && height.IsPointValue() && height.GetValue() > minHeightToApply) {
                minHeightToApply = height.GetValue();
            }
            if (minHeightToApply > 0) {
                element.SetProperty(Property.MIN_HEIGHT, minHeightToApply);
            }
        }
    }
}
