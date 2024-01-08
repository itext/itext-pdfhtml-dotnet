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
using iText.Html2pdf.Css.Resolve;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Page;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Layout {
    /// <summary>
    /// Properties class for the
    /// <see cref="PageContextProcessor"/>.
    /// </summary>
    internal class PageContextProperties {
        /// <summary>List containing possible names for page margin boxes.</summary>
        private static readonly IList<String> pageMarginBoxNames = JavaUtil.ArraysAsList(CssRuleName.TOP_LEFT_CORNER
            , CssRuleName.TOP_LEFT, CssRuleName.TOP_CENTER, CssRuleName.TOP_RIGHT, CssRuleName.TOP_RIGHT_CORNER, CssRuleName
            .RIGHT_TOP, CssRuleName.RIGHT_MIDDLE, CssRuleName.RIGHT_BOTTOM, CssRuleName.BOTTOM_RIGHT_CORNER, CssRuleName
            .BOTTOM_RIGHT, CssRuleName.BOTTOM_CENTER, CssRuleName.BOTTOM_LEFT, CssRuleName.BOTTOM_LEFT_CORNER, CssRuleName
            .LEFT_BOTTOM, CssRuleName.LEFT_MIDDLE, CssRuleName.LEFT_TOP);

        /// <summary>The page context node.</summary>
        private PageContextNode pageContextNode;

        /// <summary>The page margin boxes.</summary>
        private IList<PageMarginBoxContextNode> pageMarginBoxes;

        /// <summary>
        /// Instantiates a new
        /// <see cref="PageContextProperties"/>
        /// instance.
        /// </summary>
        /// <param name="pageProps">the page context node</param>
        /// <param name="pagesMarginBoxes">the page margin boxes</param>
        private PageContextProperties(PageContextNode pageProps, IList<PageMarginBoxContextNode> pagesMarginBoxes) {
            this.pageContextNode = pageProps;
            this.pageMarginBoxes = pagesMarginBoxes;
        }

        /// <summary>
        /// Resolves a node with a
        /// <see cref="PageContextProperties"/>
        /// instance as result.
        /// </summary>
        /// <param name="rootNode">the root node to resolve</param>
        /// <param name="cssResolver">the CSS resolver</param>
        /// <param name="context">the CSS context</param>
        /// <param name="pageClasses">the page classes</param>
        /// <returns>
        /// the
        /// <see cref="PageContextProperties"/>
        /// for a specific node
        /// </returns>
        public static iText.Html2pdf.Attach.Impl.Layout.PageContextProperties Resolve(INode rootNode, ICssResolver
             cssResolver, CssContext context, params String[] pageClasses) {
            PageContextNode pageProps = GetResolvedPageClassNode(rootNode, cssResolver, context, pageClasses);
            IList<PageMarginBoxContextNode> pagesMarginBoxes = GetResolvedMarginBoxes(pageProps, cssResolver, context);
            return new iText.Html2pdf.Attach.Impl.Layout.PageContextProperties(pageProps, pagesMarginBoxes);
        }

        /// <summary>Gets the resolved margin boxes.</summary>
        /// <param name="pageClassNode">the page contex node</param>
        /// <param name="cssResolver">the CSS resolver</param>
        /// <param name="context">the CSS context</param>
        /// <returns>the resolved margin boxes</returns>
        private static IList<PageMarginBoxContextNode> GetResolvedMarginBoxes(PageContextNode pageClassNode, ICssResolver
             cssResolver, CssContext context) {
            IList<PageMarginBoxContextNode> resolvedMarginBoxes = new List<PageMarginBoxContextNode>();
            foreach (String pageMarginBoxName in pageMarginBoxNames) {
                PageMarginBoxContextNode marginBoxNode = new PageMarginBoxContextNode(pageClassNode, pageMarginBoxName);
                IDictionary<String, String> marginBoxStyles = cssResolver.ResolveStyles(marginBoxNode, context);
                if (!marginBoxNode.ChildNodes().IsEmpty()) {
                    marginBoxNode.SetStyles(marginBoxStyles);
                    resolvedMarginBoxes.Add(marginBoxNode);
                }
                context.SetQuotesDepth(0);
            }
            return resolvedMarginBoxes;
        }

        /// <summary>Gets the resolved page class node.</summary>
        /// <param name="rootNode">the root node</param>
        /// <param name="cssResolver">the CSS resolver</param>
        /// <param name="context">the CSS context</param>
        /// <param name="pageClasses">the page classes</param>
        /// <returns>the resolved page class node</returns>
        private static PageContextNode GetResolvedPageClassNode(INode rootNode, ICssResolver cssResolver, CssContext
             context, params String[] pageClasses) {
            PageContextNode pagesClassNode = new PageContextNode(rootNode);
            foreach (String pageClass in pageClasses) {
                pagesClassNode.AddPageClass(pageClass);
            }
            IDictionary<String, String> pageClassStyles = cssResolver.ResolveStyles(pagesClassNode, context);
            pagesClassNode.SetStyles(pageClassStyles);
            return pagesClassNode;
        }

        /// <summary>Gets the resolved page context node.</summary>
        /// <returns>the resolved page context node</returns>
        internal virtual PageContextNode GetResolvedPageContextNode() {
            return pageContextNode;
        }

        /// <summary>Gets the resolved page margin boxes.</summary>
        /// <returns>the resolved page margin boxes</returns>
        internal virtual IList<PageMarginBoxContextNode> GetResolvedPageMarginBoxes() {
            return pageMarginBoxes;
        }
    }
}
