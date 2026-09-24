/*
This file is part of the iText (R) project.
Copyright (c) 1998-2026 Apryse Group NV
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
using iText.Commons.Internal.Runtime;
using iText.Layout;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>Applies the CSS Level 3 text-combine-upright property.</summary>
    public sealed class TextCombineUprightApplierUtil {
        private TextCombineUprightApplierUtil() {
        }

        /// <summary>Maps resolved CSS values to layout properties.</summary>
        /// <remarks>Maps resolved CSS values to layout properties. The renderer ignores combination in horizontal writing.
        ///     </remarks>
        /// <param name="cssProps">resolved CSS properties</param>
        /// <param name="element">target layout element</param>
        public static void ApplyTextCombineUpright(IDictionary<String, String> cssProps, IPropertyContainer element
            ) {
            String value = cssProps.Get(CommonCssConstants.TEXT_COMBINE_UPRIGHT);
            if (CommonCssConstants.ALL.Equals(value)) {
                element.SetProperty(Property.TEXT_COMBINE_UPRIGHT, TextCombineUpright.ALL);
            }
            else {
                if (CommonCssConstants.NONE.Equals(value)) {
                    element.SetProperty(Property.TEXT_COMBINE_UPRIGHT, TextCombineUpright.NONE);
                }
            }
        }
    }
}
