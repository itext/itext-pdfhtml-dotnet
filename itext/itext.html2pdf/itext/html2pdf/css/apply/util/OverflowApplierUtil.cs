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
using iText.Html2pdf.Css;
using iText.Layout;
using iText.Layout.Properties;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply overflow.</summary>
    public class OverflowApplierUtil {
        /// <summary>Creates a new <c>OverflowApplierUtil</c> instance.</summary>
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
