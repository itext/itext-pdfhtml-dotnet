/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
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
address: sales@itextpdf.com
*/
using System;
using System.Collections.Generic;
using iText.IO.Util;
using iText.StyledXmlParser.Css.Page;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Layout {
    [System.ObsoleteAttribute(@"To be removed in next major pdfHTML version once iText.StyledXmlParser.Css.Page.PageMarginBoxContextNode implementsiText.StyledXmlParser.Node.IElementNode so that it can be used directly instead of creating dummy node."
        )]
    internal class PageMarginBoxDummyElement : IElementNode, ICustomElementNode {
        /// <summary>The resolved styles.</summary>
        private IDictionary<String, String> elementResolvedStyles;

        public virtual String Name() {
            return PageMarginBoxContextNode.PAGE_MARGIN_BOX_TAG;
        }

        public virtual IAttributes GetAttributes() {
            throw new NotSupportedException();
        }

        public virtual String GetAttribute(String key) {
            return null;
        }

        public virtual IList<IDictionary<String, String>> GetAdditionalHtmlStyles() {
            throw new NotSupportedException();
        }

        public virtual void AddAdditionalHtmlStyles(IDictionary<String, String> styles) {
            throw new NotSupportedException();
        }

        public virtual String GetLang() {
            throw new NotSupportedException();
        }

        public virtual IList<INode> ChildNodes() {
            throw new NotSupportedException();
        }

        public virtual void AddChild(INode node) {
            throw new NotSupportedException();
        }

        public virtual INode ParentNode() {
            throw new NotSupportedException();
        }

        public virtual void SetStyles(IDictionary<String, String> stringStringMap) {
            elementResolvedStyles = stringStringMap;
        }

        public virtual IDictionary<String, String> GetStyles() {
            return elementResolvedStyles == null ? JavaCollectionsUtil.EmptyMap<String, String>() : elementResolvedStyles;
        }
    }
}
