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
