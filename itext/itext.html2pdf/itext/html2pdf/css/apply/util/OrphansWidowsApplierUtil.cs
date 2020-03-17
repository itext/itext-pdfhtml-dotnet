/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
Authors: iText Software.

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
using iText.StyledXmlParser.Css.Util;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply orphans and widows properties.</summary>
    public class OrphansWidowsApplierUtil {
        public const int MAX_LINES_TO_MOVE = 2;

        public const bool OVERFLOW_PARAGRAPH_ON_VIOLATION = false;

        /// <summary>
        /// Creates a
        /// <see cref="OrphansWidowsApplierUtil"/>
        /// instance.
        /// </summary>
        private OrphansWidowsApplierUtil() {
        }

        /// <summary>Applies orphans and widows properties to an element.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="element">to which the property will be applied.</param>
        public static void ApplyOrphansAndWidows(IDictionary<String, String> cssProps, IPropertyContainer element) {
            if (cssProps != null) {
                if (cssProps.ContainsKey(CssConstants.WIDOWS)) {
                    int? minWidows = CssUtils.ParseInteger(cssProps.Get(CssConstants.WIDOWS));
                    if (minWidows != null && minWidows > 0) {
                        element.SetProperty(Property.WIDOWS_CONTROL, new ParagraphWidowsControl(minWidows.Value, MAX_LINES_TO_MOVE
                            , OVERFLOW_PARAGRAPH_ON_VIOLATION));
                    }
                }
                if (cssProps.ContainsKey(CssConstants.ORPHANS)) {
                    int? minOrphans = CssUtils.ParseInteger(cssProps.Get(CssConstants.ORPHANS));
                    if (minOrphans != null && minOrphans > 0) {
                        element.SetProperty(Property.ORPHANS_CONTROL, new ParagraphOrphansControl(minOrphans.Value));
                    }
                }
            }
        }
    }
}
