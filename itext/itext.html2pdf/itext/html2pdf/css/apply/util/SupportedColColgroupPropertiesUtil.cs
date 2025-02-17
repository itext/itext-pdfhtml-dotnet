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
using iText.Commons.Utils;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Util;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>
    /// Utilities class to get widths and mapping related to columns and column groups
    /// as stated in paragraph 17.3 of https://www.w3.org/TR/CSS21/tables.html#q4.
    /// </summary>
    public class SupportedColColgroupPropertiesUtil {
        /// <summary>
        /// These inheritable properties should be transferred from &lt;colgroup&gt;
        /// to &lt;col&gt; and then to &lt;td&gt; or &lt;th&gt;.
        /// </summary>
        private static readonly ICollection<String> CELL_CSS_PROPERTIES = JavaCollectionsUtil.UnmodifiableSet(new 
            HashSet<String>(JavaUtil.ArraysAsList(CssConstants.BACKGROUND_COLOR, CssConstants.BACKGROUND_IMAGE, CssConstants
            .BACKGROUND_POSITION_X, CssConstants.BACKGROUND_POSITION_Y, CssConstants.BACKGROUND_SIZE, CssConstants
            .BACKGROUND_REPEAT, CssConstants.BACKGROUND_ORIGIN, CssConstants.BACKGROUND_CLIP, CssConstants.BACKGROUND_ATTACHMENT
            )));

        /// <summary>These properties don't need to be transferred from &lt;colgroup&gt; to &lt;col&gt;.</summary>
        private static readonly ICollection<String> OWN_CSS_PROPERTIES = JavaCollectionsUtil/*TODO DEVSIX-2090 visibility doesn't work on "chrome" or "safari" and it works on "firefox" but the results differ, 
            The supported values are 'collapse' and 'visible'. The expected behaviour for 'collapse' is not to render those cols 
            (the table layout should change ann the width should be diminished), and to clip cells that are spaned to none-collapsed one. 
            The state of the content in clipped cells is not specified*/ .UnmodifiableSet(new HashSet<String>(JavaUtil.ArraysAsList
            (CssConstants.BORDER_BOTTOM_COLOR, CssConstants.BORDER_BOTTOM_STYLE, CssConstants.BORDER_BOTTOM_WIDTH, 
            CssConstants.BORDER_LEFT_COLOR, CssConstants.BORDER_LEFT_STYLE, CssConstants.BORDER_LEFT_WIDTH, CssConstants
            .BORDER_RIGHT_COLOR, CssConstants.BORDER_RIGHT_STYLE, CssConstants.BORDER_RIGHT_WIDTH, CssConstants.BORDER_TOP_COLOR
            , CssConstants.BORDER_TOP_STYLE, CssConstants.BORDER_TOP_WIDTH, CssConstants.VISIBILITY)));

        /// <summary>Gets the width.</summary>
        /// <param name="resolvedCssProps">the resolved CSS properties</param>
        /// <param name="context">the processor context</param>
        /// <returns>the width</returns>
        public static UnitValue GetWidth(IDictionary<String, String> resolvedCssProps, ProcessorContext context) {
            //The Width is a special case, casue it should be transferred from <colgroup> to <col> but it not applied to <td> or <th>
            float em = CssDimensionParsingUtils.ParseAbsoluteLength(resolvedCssProps.Get(CssConstants.FONT_SIZE));
            String width = resolvedCssProps.Get(CssConstants.WIDTH);
            return width != null ? CssDimensionParsingUtils.ParseLengthValueToPt(width, em, context.GetCssContext().GetRootFontSize
                ()) : null;
        }

        /// <summary>Gets the cell properties.</summary>
        /// <param name="resolvedCssProps">the resolved CSS properties</param>
        /// <returns>the cell properties</returns>
        public static IDictionary<String, String> GetCellProperties(IDictionary<String, String> resolvedCssProps) {
            return GetFilteredMap(resolvedCssProps, CELL_CSS_PROPERTIES);
        }

        /// <summary>Gets the own properties.</summary>
        /// <param name="resolvedCssProps">the resolved css props</param>
        /// <returns>the own properties</returns>
        public static IDictionary<String, String> GetOwnProperties(IDictionary<String, String> resolvedCssProps) {
            return GetFilteredMap(resolvedCssProps, OWN_CSS_PROPERTIES);
        }

        /// <summary>Filters a given map so that it only contains supported keys.</summary>
        /// <param name="map">the map</param>
        /// <param name="supportedKeys">the supported keys</param>
        /// <returns>the filtered map</returns>
        private static IDictionary<String, String> GetFilteredMap(IDictionary<String, String> map, ICollection<String
            > supportedKeys) {
            IDictionary<String, String> result = new Dictionary<String, String>();
            if (map != null) {
                foreach (String key in supportedKeys) {
                    String value = map.Get(key);
                    if (value != null) {
                        result.Put(key, value);
                    }
                }
            }
            return result.Count > 0 ? result : null;
        }
    }
}
