using System;
using System.Collections.Generic;
using iText.Html2pdf.Css.Page;
using iText.Html2pdf.Html.Node;
using iText.IO.Util;

namespace iText.Html2pdf.Attach.Impl.Layout {
    [System.ObsoleteAttribute(@"Remove this class in 7.2 and use iText.Html2pdf.Css.Page.PageMarginBoxContextNode instead (by making it implement iText.Html2pdf.Html.Node.IElementNode )."
        )]
    internal class PageMarginBoxDummyElement : IElementNode, ICustomElementNode {
        public virtual String Name() {
            return PageMarginBoxContextNode.PAGE_MARGIN_BOX_TAG;
        }

        public virtual IAttributes GetAttributes() {
            throw new NotSupportedException();
        }

        public virtual String GetAttribute(String key) {
            throw new NotSupportedException();
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
            throw new NotSupportedException();
        }

        public virtual IDictionary<String, String> GetStyles() {
            return JavaCollectionsUtil.EmptyMap<String, String>();
        }
    }
}
