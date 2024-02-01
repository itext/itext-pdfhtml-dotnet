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
using iText.Commons.Utils;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;

namespace iText.Html2pdf.Css.Resolve.Func.Counter {
    /// <summary>
    /// <see cref="iText.StyledXmlParser.Node.ICustomElementNode"/>
    /// implementation for a page count element node.
    /// </summary>
    public class PageCountElementNode : JsoupElementNode, ICustomElementNode {
        /// <summary>The Constant PAGE_COUNTER_TAG.</summary>
        public const String PAGE_COUNTER_TAG = "_e0d00a6_page-counter";

        /// <summary>The parent.</summary>
        private readonly INode parent;

        private CounterDigitsGlyphStyle digitsGlyphStyle;

        /// <summary>Indicates if the node represents the total page count.</summary>
        private bool totalPageCount = false;

        public PageCountElementNode(bool totalPageCount, INode parent)
            : base(new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag.ValueOf(PAGE_COUNTER_TAG
                ), "")) {
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

        /// <summary>Gets glyph style for digits.</summary>
        /// <returns>name of the glyph style</returns>
        public virtual CounterDigitsGlyphStyle GetDigitsGlyphStyle() {
            return digitsGlyphStyle;
        }

        /// <summary>Sets glyph style for digits.</summary>
        /// <param name="digitsGlyphStyle">name of the glyph style</param>
        /// <returns>
        /// this
        /// <see cref="PageCountElementNode"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.Css.Resolve.Func.Counter.PageCountElementNode SetDigitsGlyphStyle(CounterDigitsGlyphStyle
             digitsGlyphStyle) {
            this.digitsGlyphStyle = digitsGlyphStyle;
            return this;
        }
    }
}
