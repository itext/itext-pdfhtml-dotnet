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
using System.Text;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html.Node;
using iText.IO.Log;
using iText.IO.Util;

namespace iText.Html2pdf.Css.Resolve {
    internal class CssContentPropertyResolver {
        internal static INode ResolveContent(String contentStr, INode contentContainer, CssContext context) {
            if (contentStr == null || CssConstants.NONE.Equals(contentStr) || CssConstants.NORMAL.Equals(contentStr)) {
                return null;
            }
            StringBuilder content = new StringBuilder();
            StringBuilder nonDirectContent = new StringBuilder();
            bool insideQuotes = false;
            bool insideDoubleQuotes = false;
            for (int i = 0; i < contentStr.Length; ++i) {
                if (contentStr[i] == '"' && (!insideQuotes || insideDoubleQuotes) || contentStr[i] == '\'' && (!insideQuotes
                     || !insideDoubleQuotes)) {
                    if (!insideQuotes) {
                        // TODO in future, try to resolve if counter() or smth like that encountered
                        if (!String.IsNullOrEmpty(nonDirectContent.ToString().Trim())) {
                            break;
                        }
                        nonDirectContent.Length = 0;
                        insideDoubleQuotes = contentStr[i] == '"';
                    }
                    insideQuotes = !insideQuotes;
                }
                else {
                    if (insideQuotes) {
                        content.Append(contentStr[i]);
                    }
                    else {
                        nonDirectContent.Append(contentStr[i]);
                    }
                }
            }
            if (!String.IsNullOrEmpty(nonDirectContent.ToString().Trim())) {
                ILogger logger = LoggerFactory.GetLogger(typeof(CssContentPropertyResolver));
                logger.Error(String.Format(iText.Html2pdf.LogMessageConstant.CONTENT_PROPERTY_INVALID, contentStr));
                return null;
            }
            // TODO resolve unicode sequences. see PseudoElementsTest#collapsingMarginsBeforeAfterPseudo03
            String resolvedContent = content.ToString();
            // TODO in future, when img content values will be supported, some specific IElementNode might be returned with 
            // correct src attribute, however this element shall not get img styles from css style sheet 
            // and also the one that implements it should be aware of possible infinite loop (see PseudoElementsTest#imgPseudoTest02)
            return new CssContentPropertyResolver.ContentTextNode(contentContainer, resolvedContent);
        }

        private class ContentTextNode : ITextNode {
            private readonly INode parent;

            private String content;

            public ContentTextNode(INode parent, String content) {
                this.parent = parent;
                this.content = content;
            }

            public virtual IList<INode> ChildNodes() {
                return JavaCollectionsUtil.EmptyList<INode>();
            }

            public virtual void AddChild(INode node) {
                throw new NotSupportedException();
            }

            public virtual INode ParentNode() {
                return parent;
            }

            public virtual String WholeText() {
                return content;
            }
        }
    }
}
