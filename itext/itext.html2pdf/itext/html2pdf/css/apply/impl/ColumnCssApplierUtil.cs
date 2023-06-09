using System;
using System.Collections.Generic;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Layout;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Util;

namespace iText.Html2pdf.Css.Apply.Impl {
    /// <summary>Utility class to apply column-count values.</summary>
    public class ColumnCssApplierUtil {
        private ColumnCssApplierUtil() {
        }

        /// <summary>Apply column-count to an element.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the Processor context</param>
        /// <param name="element">the styles container</param>
        public static void ApplyColumnCount(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             element) {
            if (context.IsMulticolEnabled()) {
                int? columnCount = CssDimensionParsingUtils.ParseInteger(cssProps.Get(CssConstants.COLUMN_COUNT));
                if (columnCount != null) {
                    element.SetProperty(Property.COLUMN_COUNT, columnCount);
                }
            }
        }
    }
}
