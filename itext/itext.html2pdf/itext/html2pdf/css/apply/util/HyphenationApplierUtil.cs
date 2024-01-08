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
using iText.Layout.Hyphenation;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Resolve;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>The Class HyphenationApplierUtil.</summary>
    public sealed class HyphenationApplierUtil {
        // These are css properties actually, but it is not supported by the browsers currently
        /// <summary>The Constant HYPHENATE_BEFORE.</summary>
        private const int HYPHENATE_BEFORE = 2;

        /// <summary>The Constant HYPHENATE_AFTER.</summary>
        private const int HYPHENATE_AFTER = 3;

        /// <summary>
        /// Creates a new
        /// <see cref="HyphenationApplierUtil"/>
        /// instance.
        /// </summary>
        private HyphenationApplierUtil() {
        }

        /// <summary>Applies hyphenation to an element.</summary>
        /// <param name="cssProps">the CSS props</param>
        /// <param name="context">the processor context</param>
        /// <param name="stylesContainer">the styles container</param>
        /// <param name="element">the element</param>
        public static void ApplyHyphenation(IDictionary<String, String> cssProps, ProcessorContext context, IStylesContainer
             stylesContainer, IPropertyContainer element) {
            String value = cssProps.Get(CssConstants.HYPHENS);
            if (value == null) {
                value = CssDefaults.GetDefaultValue(CssConstants.HYPHENS);
            }
            if (CssConstants.NONE.Equals(value)) {
                element.SetProperty(Property.HYPHENATION, null);
            }
            else {
                if (CssConstants.MANUAL.Equals(value)) {
                    element.SetProperty(Property.HYPHENATION, new HyphenationConfig(HYPHENATE_BEFORE, HYPHENATE_AFTER));
                }
                else {
                    if (CssConstants.AUTO.Equals(value) && stylesContainer is IElementNode) {
                        String lang = ((IElementNode)stylesContainer).GetLang();
                        if (lang != null && lang.Length > 0) {
                            element.SetProperty(Property.HYPHENATION, new HyphenationConfig(lang.JSubstring(0, 2), "", HYPHENATE_BEFORE
                                , HYPHENATE_AFTER));
                        }
                    }
                }
            }
        }
    }
}
