/*
This file is part of the iText (R) project.
Copyright (c) 1998-2018 iText Group NV
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
using iText.Layout;
using iText.Layout.Hyphenation;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Resolve;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Apply.Util {
    /// <summary>The Class HyphenationApplierUtil.</summary>
    public sealed class HyphenationApplierUtil {
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

        // TODO these are css properties actually, but it is not supported by the browsers currently
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
