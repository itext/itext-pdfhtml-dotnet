/*
This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
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
using iText.Html2pdf.Html.Impl.Jsoup.Node;
using iText.Html2pdf.Html.Node;
using iText.IO.Util;

namespace iText.Html2pdf.Css.Resolve.Func.Counter {
    /// <summary>
    /// <see cref="iText.Html2pdf.Html.Node.ICustomElementNode"/>
    /// implementation for a page count element node.
    /// </summary>
    public class PageCountElementNode : JsoupElementNode, ICustomElementNode {
        /// <summary>The Constant PAGE_COUNTER_TAG.</summary>
        public const String PAGE_COUNTER_TAG = "_e0d00a6_page-counter";

        /// <summary>The parent.</summary>
        private readonly INode parent;

        /// <summary>Indicates if the node represents the total page count.</summary>
        private bool totalPageCount = false;

        /// <summary>
        /// Creates a new
        /// <see cref="PageCountElementNode"/>
        /// instance.
        /// </summary>
        /// <param name="totalPageCount">indicates if the node represents the total page count</param>
        [System.ObsoleteAttribute(@"Will be removed in 3.0. Use PageCountElementNode(bool, iText.Html2pdf.Html.Node.INode) instead"
            )]
        public PageCountElementNode(bool totalPageCount)
            : this(totalPageCount, null) {
        }

        public PageCountElementNode(bool totalPageCount, INode parent)
            : base(new iText.Html2pdf.Jsoup.Nodes.Element(iText.Html2pdf.Jsoup.Parser.Tag.ValueOf(PAGE_COUNTER_TAG), ""
                )) {
            this.totalPageCount = totalPageCount;
            this.parent = parent;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.html.node.INode#childNodes()
        */
        public override IList<INode> ChildNodes() {
            return JavaCollectionsUtil.EmptyList<INode>();
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.html.node.INode#addChild(com.itextpdf.html2pdf.html.node.INode)
        */
        public override void AddChild(INode node) {
            throw new NotSupportedException();
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.html.node.INode#parentNode()
        */
        public override INode ParentNode() {
            return parent;
        }

        /// <summary>Checks if the node represents the total page count.</summary>
        /// <returns>true, if the node represents the total page count</returns>
        public virtual bool IsTotalPageCount() {
            return totalPageCount;
        }
    }
}
