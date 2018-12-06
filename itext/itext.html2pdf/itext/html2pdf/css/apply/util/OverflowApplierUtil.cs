/*
This file is part of the iText (R) project.
Copyright (c) 1998-2018 iText Group NV
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
using iText.Html2pdf.Css;
using iText.Layout;
using iText.Layout.Properties;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply overflow.</summary>
    public class OverflowApplierUtil {
        /// <summary>Creates a new <code>OverflowApplierUtil</code> instance.</summary>
        private OverflowApplierUtil() {
        }

        /// <summary>Applies overflow to an element.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="element">the element</param>
        public static void ApplyOverflow(IDictionary<String, String> cssProps, IPropertyContainer element) {
            String overflow = null != cssProps && CssConstants.OVERFLOW_VALUES.Contains(cssProps.Get(CssConstants.OVERFLOW
                )) ? cssProps.Get(CssConstants.OVERFLOW) : null;
            String overflowX = null != cssProps && CssConstants.OVERFLOW_VALUES.Contains(cssProps.Get(CssConstants.OVERFLOW_X
                )) ? cssProps.Get(CssConstants.OVERFLOW_X) : overflow;
            if (CssConstants.HIDDEN.Equals(overflowX) || CssConstants.AUTO.Equals(overflowX) || CssConstants.SCROLL.Equals
                (overflowX)) {
                element.SetProperty(Property.OVERFLOW_X, OverflowPropertyValue.HIDDEN);
            }
            else {
                element.SetProperty(Property.OVERFLOW_X, OverflowPropertyValue.VISIBLE);
            }
            String overflowY = null != cssProps && CssConstants.OVERFLOW_VALUES.Contains(cssProps.Get(CssConstants.OVERFLOW_Y
                )) ? cssProps.Get(CssConstants.OVERFLOW_Y) : overflow;
            if (CssConstants.HIDDEN.Equals(overflowY) || CssConstants.AUTO.Equals(overflowY) || CssConstants.SCROLL.Equals
                (overflowY)) {
                element.SetProperty(Property.OVERFLOW_Y, OverflowPropertyValue.HIDDEN);
            }
            else {
                element.SetProperty(Property.OVERFLOW_Y, OverflowPropertyValue.VISIBLE);
            }
        }
    }
}
