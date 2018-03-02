using System.Collections.Generic;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html.Node;

namespace iText.Html2pdf.Css.Selector.Item {
    internal class CssPseudoClassFirstChildSelectorItem : CssPseudoClassChildSelectorItem {
        private static readonly iText.Html2pdf.Css.Selector.Item.CssPseudoClassFirstChildSelectorItem instance = new 
            iText.Html2pdf.Css.Selector.Item.CssPseudoClassFirstChildSelectorItem();

        private CssPseudoClassFirstChildSelectorItem()
            : base(CssConstants.FIRST_CHILD) {
        }

        public static iText.Html2pdf.Css.Selector.Item.CssPseudoClassFirstChildSelectorItem GetInstance() {
            return instance;
        }

        public override bool Matches(INode node) {
            if (!(node is IElementNode) || node is ICustomElementNode || node is IDocumentNode) {
                return false;
            }
            IList<INode> children = GetAllSiblings(node);
            return !children.IsEmpty() && node.Equals(children[0]);
        }
    }
}
