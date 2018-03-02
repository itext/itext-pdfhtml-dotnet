using System.Collections.Generic;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html.Node;

namespace iText.Html2pdf.Css.Selector.Item {
    internal class CssPseudoClassLastChildSelectorItem : CssPseudoClassChildSelectorItem {
        private static readonly iText.Html2pdf.Css.Selector.Item.CssPseudoClassLastChildSelectorItem instance = new 
            iText.Html2pdf.Css.Selector.Item.CssPseudoClassLastChildSelectorItem();

        private CssPseudoClassLastChildSelectorItem()
            : base(CssConstants.LAST_CHILD) {
        }

        public static iText.Html2pdf.Css.Selector.Item.CssPseudoClassLastChildSelectorItem GetInstance() {
            return instance;
        }

        public override bool Matches(INode node) {
            if (!(node is IElementNode) || node is ICustomElementNode || node is IDocumentNode) {
                return false;
            }
            IList<INode> children = GetAllSiblings(node);
            return !children.IsEmpty() && node.Equals(children[children.Count - 1]);
        }
    }
}
