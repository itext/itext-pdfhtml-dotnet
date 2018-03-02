using System.Collections.Generic;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html.Node;

namespace iText.Html2pdf.Css.Selector.Item {
    internal class CssPseudoClassFirstOfTypeSelectorItem : CssPseudoClassChildSelectorItem {
        private static readonly iText.Html2pdf.Css.Selector.Item.CssPseudoClassFirstOfTypeSelectorItem instance = 
            new iText.Html2pdf.Css.Selector.Item.CssPseudoClassFirstOfTypeSelectorItem();

        private CssPseudoClassFirstOfTypeSelectorItem()
            : base(CssConstants.FIRST_OF_TYPE) {
        }

        public static iText.Html2pdf.Css.Selector.Item.CssPseudoClassFirstOfTypeSelectorItem GetInstance() {
            return instance;
        }

        public override bool Matches(INode node) {
            if (!(node is IElementNode) || node is ICustomElementNode) {
                return false;
            }
            IList<INode> children = GetAllSiblingsOfNodeType(node);
            return !children.IsEmpty() && node.Equals(children[0]);
        }
    }
}
