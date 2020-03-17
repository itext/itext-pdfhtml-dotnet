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
