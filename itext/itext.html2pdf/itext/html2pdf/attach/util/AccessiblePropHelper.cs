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
using iText.Html2pdf.Html;
using iText.Kernel.Pdf.Tagutils;
using iText.Layout.Tagging;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Util {
    /// <summary>Utility class to set lang attribute.</summary>
    public class AccessiblePropHelper {
        /// <summary>Set language attribute in elements accessibility properties if it is not set, does nothing otherwise.
        ///     </summary>
        /// <param name="accessibleElement">pdf element to set language property on</param>
        /// <param name="element">html element from which lang property will be extracted</param>
        public static void TrySetLangAttribute(IAccessibleElement accessibleElement, IElementNode element) {
            String lang = element.GetAttribute(AttributeConstants.LANG);
            TrySetLangAttribute(accessibleElement, lang);
        }

        /// <summary>Set language attribute in elements accessibility properties if it is not set, does nothing otherwise.
        ///     </summary>
        /// <param name="accessibleElement">pdf element to set language property on</param>
        /// <param name="lang">language to set</param>
        public static void TrySetLangAttribute(IAccessibleElement accessibleElement, String lang) {
            if (lang != null) {
                AccessibilityProperties properties = accessibleElement.GetAccessibilityProperties();
                if (properties.GetLanguage() == null) {
                    properties.SetLanguage(lang);
                }
            }
        }
    }
}
