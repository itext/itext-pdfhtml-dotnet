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
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Resolve.Shorthand;
using iText.IO.Util;

namespace iText.Html2pdf.Css.Resolve.Shorthand.Impl {
    /// <summary>
    /// <see cref="iText.Html2pdf.Css.Resolve.Shorthand.IShorthandResolver"/>
    /// implementation for list styles.
    /// </summary>
    public class ListStyleShorthandResolver : IShorthandResolver {
        /// <summary>The list style types (disc, decimal,...).</summary>
        private static readonly ICollection<String> LIST_STYLE_TYPE_VALUES = new HashSet<String>(JavaUtil.ArraysAsList
            (CssConstants.DISC, CssConstants.ARMENIAN, CssConstants.CIRCLE, CssConstants.CJK_IDEOGRAPHIC, CssConstants
            .DECIMAL, CssConstants.DECIMAL_LEADING_ZERO, CssConstants.GEORGIAN, CssConstants.HEBREW, CssConstants.
            HIRAGANA, CssConstants.HIRAGANA_IROHA, CssConstants.LOWER_ALPHA, CssConstants.LOWER_GREEK, CssConstants
            .LOWER_LATIN, CssConstants.LOWER_ROMAN, CssConstants.NONE, CssConstants.SQUARE, CssConstants.UPPER_ALPHA
            , CssConstants.UPPER_LATIN, CssConstants.UPPER_ROMAN));

        /// <summary>The list stype positions (inside, outside).</summary>
        private static readonly ICollection<String> LIST_STYLE_POSITION_VALUES = new HashSet<String>(JavaUtil.ArraysAsList
            (CssConstants.INSIDE, CssConstants.OUTSIDE));

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.css.resolve.shorthand.IShorthandResolver#resolveShorthand(java.lang.String)
        */
        public virtual IList<CssDeclaration> ResolveShorthand(String shorthandExpression) {
            if (CssConstants.INITIAL.Equals(shorthandExpression) || CssConstants.INHERIT.Equals(shorthandExpression)) {
                return JavaUtil.ArraysAsList(new CssDeclaration(CssConstants.LIST_STYLE_TYPE, shorthandExpression), new CssDeclaration
                    (CssConstants.LIST_STYLE_POSITION, shorthandExpression), new CssDeclaration(CssConstants.LIST_STYLE_IMAGE
                    , shorthandExpression));
            }
            String[] props = iText.IO.Util.StringUtil.Split(shorthandExpression, "\\s+");
            String listStyleTypeValue = null;
            String listStylePositionValue = null;
            String listStyleImageValue = null;
            foreach (String value in props) {
                if (value.Contains("url(") || CssConstants.NONE.Equals(value) && listStyleTypeValue != null) {
                    listStyleImageValue = value;
                }
                else {
                    if (LIST_STYLE_TYPE_VALUES.Contains(value)) {
                        listStyleTypeValue = value;
                    }
                    else {
                        if (LIST_STYLE_POSITION_VALUES.Contains(value)) {
                            listStylePositionValue = value;
                        }
                    }
                }
            }
            IList<CssDeclaration> resolvedDecl = new List<CssDeclaration>();
            resolvedDecl.Add(new CssDeclaration(CssConstants.LIST_STYLE_TYPE, listStyleTypeValue == null ? CssConstants
                .INITIAL : listStyleTypeValue));
            resolvedDecl.Add(new CssDeclaration(CssConstants.LIST_STYLE_POSITION, listStylePositionValue == null ? CssConstants
                .INITIAL : listStylePositionValue));
            resolvedDecl.Add(new CssDeclaration(CssConstants.LIST_STYLE_IMAGE, listStyleImageValue == null ? CssConstants
                .INITIAL : listStyleImageValue));
            return resolvedDecl;
        }
    }
}
