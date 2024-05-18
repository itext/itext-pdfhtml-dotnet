/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
using iText.StyledXmlParser.Css.Util;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply css grid properties and styles.</summary>
    public sealed class GridApplierUtil {
        private GridApplierUtil() {
        }

        // empty constructor
        /// <summary>Applies grid properties to an element.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="element">the element</param>
        public static void ApplyGridItemProperties(IDictionary<String, String> cssProps, IPropertyContainer element
            ) {
            int? columnStart = CssDimensionParsingUtils.ParseInteger(cssProps.Get(CssConstants.GRID_COLUMN_START));
            if (columnStart != null) {
                element.SetProperty(Property.GRID_COLUMN_START, columnStart);
            }
            int? columnEnd = CssDimensionParsingUtils.ParseInteger(cssProps.Get(CssConstants.GRID_COLUMN_END));
            if (columnEnd != null) {
                element.SetProperty(Property.GRID_COLUMN_END, columnEnd);
            }
            int? rowStart = CssDimensionParsingUtils.ParseInteger(cssProps.Get(CssConstants.GRID_ROW_START));
            if (rowStart != null) {
                element.SetProperty(Property.GRID_ROW_START, rowStart);
            }
            int? rowEnd = CssDimensionParsingUtils.ParseInteger(cssProps.Get(CssConstants.GRID_ROW_END));
            if (rowEnd != null) {
                element.SetProperty(Property.GRID_ROW_END, rowEnd);
            }
        }
    }
}
