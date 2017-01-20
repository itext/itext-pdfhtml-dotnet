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
using iText.Html2pdf.Css.Page;
using iText.Html2pdf.Css.Resolve;
using iText.Html2pdf.Html.Node;

namespace iText.Html2pdf.Attach.Impl.Layout {
    internal class PageContextProperties {
        private static IList<String> pageMarginBoxNames = iText.IO.Util.JavaUtil.ArraysAsList(CssRuleName.TOP_LEFT_CORNER
            , CssRuleName.TOP_LEFT, CssRuleName.TOP_CENTER, CssRuleName.TOP_RIGHT, CssRuleName.TOP_RIGHT_CORNER, CssRuleName
            .RIGHT_TOP, CssRuleName.RIGHT_MIDDLE, CssRuleName.RIGHT_BOTTOM, CssRuleName.BOTTOM_RIGHT_CORNER, CssRuleName
            .BOTTOM_RIGHT, CssRuleName.BOTTOM_CENTER, CssRuleName.BOTTOM_LEFT, CssRuleName.BOTTOM_LEFT_CORNER, CssRuleName
            .LEFT_BOTTOM, CssRuleName.LEFT_MIDDLE, CssRuleName.LEFT_TOP);

        private PageContextNode pageContextNode;

        private IList<PageMarginBoxContextNode> pageMarginBoxes;

        private PageContextProperties(PageContextNode pageProps, IList<PageMarginBoxContextNode> pagesMarginBoxes) {
            this.pageContextNode = pageProps;
            this.pageMarginBoxes = pagesMarginBoxes;
        }

        public static iText.Html2pdf.Attach.Impl.Layout.PageContextProperties Resolve(INode rootNode, ICssResolver
             cssResolver, CssContext context, params String[] pageClasses) {
            PageContextNode pageProps = GetResolvedPageClassNode(rootNode, cssResolver, context, pageClasses);
            IList<PageMarginBoxContextNode> pagesMarginBoxes = GetResolvedMarginBoxes(pageProps, cssResolver, context);
            return new iText.Html2pdf.Attach.Impl.Layout.PageContextProperties(pageProps, pagesMarginBoxes);
        }

        public virtual PageContextNode GetResolvedPageContextNode() {
            return pageContextNode;
        }

        public virtual IList<PageMarginBoxContextNode> GetResolvedPageMarginBoxes() {
            return pageMarginBoxes;
        }

        private static IList<PageMarginBoxContextNode> GetResolvedMarginBoxes(PageContextNode pageClassNode, ICssResolver
             cssResolver, CssContext context) {
            IList<PageMarginBoxContextNode> resolvedMarginBoxes = new List<PageMarginBoxContextNode>();
            foreach (String pageMarginBoxName in pageMarginBoxNames) {
                PageMarginBoxContextNode marginBoxNode = new PageMarginBoxContextNode(pageClassNode, pageMarginBoxName);
                IDictionary<String, String> marginBoxStyles = cssResolver.ResolveStyles(marginBoxNode, context);
                if (ShouldBeDisplayed(marginBoxStyles)) {
                    marginBoxNode.SetStyles(marginBoxStyles);
                    resolvedMarginBoxes.Add(marginBoxNode);
                }
            }
            return resolvedMarginBoxes;
        }

        private static bool ShouldBeDisplayed(IDictionary<String, String> marginBoxStyles) {
            return marginBoxStyles.ContainsKey(CssConstants.CONTENT);
        }

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
    }
}
