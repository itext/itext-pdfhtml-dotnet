/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2017 iText Group NV
    Authors: iText Software.

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
    address: sales@itextpdf.com */
using System;
using System.Collections.Generic;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Resolve.Shorthand;
using iText.Html2pdf.Css.Util;

namespace iText.Html2pdf.Css.Resolve.Shorthand.Impl {
    public abstract class AbstractBorderShorthandResolver : IShorthandResolver {
        private const String _0_WIDTH = "{0}-width";

        private const String _0_STYLE = "{0}-style";

        private const String _0_COLOR = "{0}-color";

        protected internal abstract String GetPrefix();

        public virtual IList<CssDeclaration> ResolveShorthand(String shorthandExpression) {
            String widthPropName = String.Format(_0_WIDTH, GetPrefix());
            String stylePropName = String.Format(_0_STYLE, GetPrefix());
            String colorPropName = String.Format(_0_COLOR, GetPrefix());
            if (CssConstants.INITIAL.Equals(shorthandExpression) || CssConstants.INHERIT.Equals(shorthandExpression)) {
                return iText.IO.Util.JavaUtil.ArraysAsList(new CssDeclaration(widthPropName, shorthandExpression), new CssDeclaration
                    (stylePropName, shorthandExpression), new CssDeclaration(colorPropName, shorthandExpression));
            }
            String[] props = iText.IO.Util.StringUtil.Split(shorthandExpression, "\\s+");
            String borderColorValue = null;
            String borderStyleValue = null;
            String borderWidthValue = null;
            foreach (String value in props) {
                if (CssConstants.BORDER_WIDTH_VALUES.Contains(value) || CssUtils.IsNumericValue(value) || CssUtils.IsMetricValue
                    (value) || CssUtils.IsRelativeValue(value)) {
                    borderWidthValue = value;
                }
                else {
                    if (CssConstants.BORDER_STYLE_VALUES.Contains(value)) {
                        borderStyleValue = value;
                    }
                    else {
                        if (CssUtils.IsColorProperty(value)) {
                            borderColorValue = value;
                        }
                    }
                }
            }
            IList<CssDeclaration> resolvedDecl = new List<CssDeclaration>();
            resolvedDecl.Add(new CssDeclaration(widthPropName, borderWidthValue == null ? CssConstants.INITIAL : borderWidthValue
                ));
            resolvedDecl.Add(new CssDeclaration(stylePropName, borderStyleValue == null ? CssConstants.INITIAL : borderStyleValue
                ));
            resolvedDecl.Add(new CssDeclaration(colorPropName, borderColorValue == null ? CssConstants.INITIAL : borderColorValue
                ));
            return resolvedDecl;
        }
    }
}
