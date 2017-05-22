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
using Org.Jsoup.Nodes;
using iText.Html2pdf.Html;
using iText.Html2pdf.Html.Node;

namespace iText.Html2pdf.Html.Impl.Jsoup.Node {
    public class JsoupElementNode : JsoupNode, IElementNode {
        private Element element;

        private IAttributes attributes;

        private IDictionary<String, String> elementResolvedStyles;

        private IList<IDictionary<String, String>> customDefaultStyles;

        private String lang = null;

        public JsoupElementNode(Element element)
            : base(element) {
            this.element = element;
            this.attributes = new JsoupAttributes(element.Attributes());
            this.lang = GetAttribute(AttributeConstants.LANG);
        }

        public virtual String Name() {
            return element.NodeName();
        }

        public virtual IAttributes GetAttributes() {
            return attributes;
        }

        public virtual String GetAttribute(String key) {
            return attributes.GetAttribute(key);
        }

        public virtual void SetStyles(IDictionary<String, String> elementResolvedStyles) {
            this.elementResolvedStyles = elementResolvedStyles;
        }

        public virtual IDictionary<String, String> GetStyles() {
            return this.elementResolvedStyles;
        }

        public virtual IList<IDictionary<String, String>> GetAdditionalHtmlStyles() {
            return customDefaultStyles;
        }

        public virtual void AddAdditionalHtmlStyles(IDictionary<String, String> styles) {
            if (customDefaultStyles == null) {
                customDefaultStyles = new List<IDictionary<String, String>>();
            }
            customDefaultStyles.Add(styles);
        }

        public virtual String GetLang() {
            if (lang != null) {
                return lang;
            }
            else {
                INode parent = parentNode;
                lang = parent is IElementNode ? ((IElementNode)parent).GetLang() : null;
                if (lang == null) {
                    // Set to empty string to "cache", i.e. not to traverse parent chain each time the method is called for
                    // documents with no "lang" attribute
                    lang = "";
                }
                return lang;
            }
        }

        public virtual String Text() {
            return element.Text();
        }
    }
}
