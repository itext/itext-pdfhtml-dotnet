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
using iText.Layout.Properties;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply a padding.</summary>
    public sealed class PaddingApplierUtil {
        /// <summary>The logger.</summary>
        private static readonly ILogger logger = LoggerFactory.GetLogger(typeof(iText.Html2pdf.Css.Apply.Util.PaddingApplierUtil
            ));

        /// <summary>Creates a new <code>PaddingApplierUtil</code> instance.</summary>
        private PaddingApplierUtil() {
        }

        /// <summary>Applies paddings to an element.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the processor context</param>
        /// <param name="element">the element</param>
        public static void ApplyPaddings(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             element) {
            String paddingTop = cssProps.Get(CssConstants.PADDING_TOP);
            String paddingBottom = cssProps.Get(CssConstants.PADDING_BOTTOM);
            String paddingLeft = cssProps.Get(CssConstants.PADDING_LEFT);
            String paddingRight = cssProps.Get(CssConstants.PADDING_RIGHT);
            float em = CssUtils.ParseAbsoluteLength(cssProps.Get(CssConstants.FONT_SIZE));
            float rem = context.GetCssContext().GetRootFontSize();
            UnitValue marginTopVal = CssUtils.ParseLengthValueToPt(paddingTop, em, rem);
            UnitValue marginBottomVal = CssUtils.ParseLengthValueToPt(paddingBottom, em, rem);
            UnitValue marginLeftVal = CssUtils.ParseLengthValueToPt(paddingLeft, em, rem);
            UnitValue marginRightVal = CssUtils.ParseLengthValueToPt(paddingRight, em, rem);
            if (marginTopVal != null) {
                if (marginTopVal.IsPointValue()) {
                    element.SetProperty(Property.PADDING_TOP, marginTopVal.GetValue());
                }
                else {
                    logger.Error(iText.Html2pdf.LogMessageConstant.PADDING_VALUE_IN_PERCENT_NOT_SUPPORTED);
                }
            }
            if (marginBottomVal != null) {
                if (marginBottomVal.IsPointValue()) {
                    element.SetProperty(Property.PADDING_BOTTOM, marginBottomVal.GetValue());
                }
                else {
                    logger.Error(iText.Html2pdf.LogMessageConstant.PADDING_VALUE_IN_PERCENT_NOT_SUPPORTED);
                }
            }
            if (marginLeftVal != null) {
                if (marginLeftVal.IsPointValue()) {
                    element.SetProperty(Property.PADDING_LEFT, marginLeftVal.GetValue());
                }
                else {
                    logger.Error(iText.Html2pdf.LogMessageConstant.PADDING_VALUE_IN_PERCENT_NOT_SUPPORTED);
                }
            }
            if (marginRightVal != null) {
                if (marginRightVal.IsPointValue()) {
                    element.SetProperty(Property.PADDING_RIGHT, marginRightVal.GetValue());
                }
                else {
                    logger.Error(iText.Html2pdf.LogMessageConstant.PADDING_VALUE_IN_PERCENT_NOT_SUPPORTED);
                }
            }
        }
    }
}
