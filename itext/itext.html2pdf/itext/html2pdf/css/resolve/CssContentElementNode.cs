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
using System.Collections;
using System.Collections.Generic;
using iText.Html2pdf.Css.Pseudo;
using iText.Html2pdf.Html.Node;

namespace iText.Html2pdf.Css.Resolve {
    internal class CssContentElementNode : CssContextNode, IElementNode, ICustomElementNode {
        private CssContentElementNode.Attributes attributes;

        private String tagName;

        public CssContentElementNode(INode parentNode, String pseudoElementName, IDictionary<String, String> attributes)
            : base(parentNode) {
            this.tagName = CssPseudoElementUtil.CreatePseudoElementTagName(pseudoElementName);
            this.attributes = new CssContentElementNode.Attributes(attributes);
        }

        public virtual String Name() {
            return tagName;
        }

        public virtual IAttributes GetAttributes() {
            return attributes;
        }

        public virtual String GetAttribute(String key) {
            return attributes.GetAttribute(key);
        }

        public virtual IList<IDictionary<String, String>> GetAdditionalHtmlStyles() {
            return null;
        }

        public virtual void AddAdditionalHtmlStyles(IDictionary<String, String> styles) {
            throw new NotSupportedException("addAdditionalHtmlStyles");
        }

        public virtual String GetLang() {
            return null;
        }

        private class Attributes : IAttributes {
            private IDictionary<String, String> attributes;

            public Attributes(IDictionary<String, String> attributes) {
                this.attributes = attributes;
            }

            public virtual String GetAttribute(String key) {
                return this.attributes.Get(key);
            }

            public virtual void SetAttribute(String key, String value) {
                throw new NotSupportedException("setAttribute");
            }

            public virtual int Size() {
                return this.attributes.Count;
            }
            
            public IEnumerator<IAttribute> GetEnumerator() {
                return new CssContentElementNode.AttributeIterator(this.attributes.GetEnumerator());
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return GetEnumerator();
            }
        }

        private class Attribute : IAttribute {
            private KeyValuePair<String, String> entry;

            public Attribute(KeyValuePair<String, String> entry) {
                this.entry = entry;
            }

            public virtual String GetKey() {
                return this.entry.Key;
            }

            public virtual String GetValue() {
                return this.entry.Value;
            }
        }

        private class AttributeIterator : IEnumerator<IAttribute> {
            private IEnumerator<KeyValuePair<String, String>> iterator;

            public AttributeIterator(IEnumerator<KeyValuePair<String, String>> iterator) {
                this.iterator = iterator;
            }

            public void Dispose() {
                iterator.Dispose();
            }
            
            public bool MoveNext() {
                return iterator.MoveNext();
            }
            
            public void Reset() {
                iterator.Reset();
            }

            public IAttribute Current {
                get { return new Attribute(iterator.Current); }
            }

            object IEnumerator.Current {
                get { return Current; }
            }
        }
    }
}
