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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Layout;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Util;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Utilities class to apply opacity.</summary>
    public class OpacityApplierUtil {
        /// <summary>
        /// Creates a new
        /// <see cref="OpacityApplierUtil"/>
        /// instance.
        /// </summary>
        private OpacityApplierUtil() {
        }

        /// <summary>Applies opacity to an element.</summary>
        /// <param name="cssProps">the CSS properties</param>
        /// <param name="context">the processor context</param>
        /// <param name="container">the container element</param>
        public static void ApplyOpacity(IDictionary<String, String> cssProps, ProcessorContext context, IPropertyContainer
             container) {
            float? opacity = CssDimensionParsingUtils.ParseFloat(cssProps.Get(CssConstants.OPACITY));
            if (opacity != null) {
                container.SetProperty(Property.OPACITY, opacity);
            }
        }
    }
}
