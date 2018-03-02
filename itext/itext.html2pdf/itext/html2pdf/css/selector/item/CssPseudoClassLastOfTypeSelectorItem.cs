using System.Collections.Generic;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html.Node;

namespace iText.Html2pdf.Css.Selector.Item {
    internal class CssPseudoClassLastOfTypeSelectorItem : CssPseudoClassChildSelectorItem {
        private static readonly iText.Html2pdf.Css.Selector.Item.CssPseudoClassLastOfTypeSelectorItem instance = new 
            iText.Html2pdf.Css.Selector.Item.CssPseudoClassLastOfTypeSelectorItem();

        private CssPseudoClassLastOfTypeSelectorItem()
            : base(CssConstants.LAST_OF_TYPE) {
        }

        public static iText.Html2pdf.Css.Selector.Item.CssPseudoClassLastOfTypeSelectorItem GetInstance() {
            return instance;
        }

        public override bool Matches(INode node) {
            if (!(node is IElementNode) || node is ICustomElementNode) {
                return false;
            }
            IList<INode> children = GetAllSiblingsOfNodeType(node);
            return !children.IsEmpty() && node.Equals(children[children.Count - 1]);
        }
    }
}
