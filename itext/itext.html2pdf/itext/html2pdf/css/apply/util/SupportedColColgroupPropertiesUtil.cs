/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
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
using iText.IO.Util;
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
        private static readonly ICollection<String> CELL_CSS_PROPERTIES = new HashSet<String>(JavaUtil.ArraysAsList
            (CssConstants.BACKGROUND_COLOR, CssConstants.BACKGROUND_IMAGE, CssConstants.BACKGROUND_POSITION, CssConstants
            .BACKGROUND_SIZE, CssConstants.BACKGROUND_REPEAT, CssConstants.BACKGROUND_ORIGIN, CssConstants.BACKGROUND_CLIP
            , CssConstants.BACKGROUND_ATTACHMENT));

        /// <summary>These properties don't need to be transferred from &lt;colgroup&gt; to &lt;col&gt;.</summary>
        private static readonly ICollection<String> OWN_CSS_PROPERTIES = /*TODO DEVSIX-2090 visibility doesn't work on "chrome" or "safari" and it works on "firefox" but the results differ, 
            The supported values are 'collapse' and 'visible'. The expected behaviour for 'collapse' is not to render those cols 
            (the table layout should change ann the width should be diminished), and to clip cells that are spaned to none-collapsed one. 
            The state of the content in clipped cells is not specified*/ new HashSet<String>(JavaUtil.ArraysAsList
            (CssConstants.BORDER_BOTTOM_COLOR, CssConstants.BORDER_BOTTOM_STYLE, CssConstants.BORDER_BOTTOM_WIDTH, 
            CssConstants.BORDER_LEFT_COLOR, CssConstants.BORDER_LEFT_STYLE, CssConstants.BORDER_LEFT_WIDTH, CssConstants
            .BORDER_RIGHT_COLOR, CssConstants.BORDER_RIGHT_STYLE, CssConstants.BORDER_RIGHT_WIDTH, CssConstants.BORDER_TOP_COLOR
            , CssConstants.BORDER_TOP_STYLE, CssConstants.BORDER_TOP_WIDTH, CssConstants.VISIBILITY));

        /// <summary>Gets the width.</summary>
        /// <param name="resolvedCssProps">the resolved CSS properties</param>
        /// <param name="context">the processor context</param>
        /// <returns>the width</returns>
        public static UnitValue GetWidth(IDictionary<String, String> resolvedCssProps, ProcessorContext context) {
            //The Width is a special case, casue it should be transferred from <colgroup> to <col> but it not applied to <td> or <th>
            float em = CssUtils.ParseAbsoluteLength(resolvedCssProps.Get(CssConstants.FONT_SIZE));
            String width = resolvedCssProps.Get(CssConstants.WIDTH);
            return width != null ? CssUtils.ParseLengthValueToPt(width, em, context.GetCssContext().GetRootFontSize())
                 : null;
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
