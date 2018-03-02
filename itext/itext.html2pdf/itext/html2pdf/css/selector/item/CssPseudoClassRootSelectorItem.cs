using iText.Html2pdf.Css;
using iText.Html2pdf.Html.Node;

namespace iText.Html2pdf.Css.Selector.Item {
    internal class CssPseudoClassRootSelectorItem : CssPseudoClassSelectorItem {
        private static readonly iText.Html2pdf.Css.Selector.Item.CssPseudoClassRootSelectorItem instance = new iText.Html2pdf.Css.Selector.Item.CssPseudoClassRootSelectorItem
            ();

        private CssPseudoClassRootSelectorItem()
            : base(CssConstants.ROOT) {
        }

        public static iText.Html2pdf.Css.Selector.Item.CssPseudoClassRootSelectorItem GetInstance() {
            return instance;
        }

        public override bool Matches(INode node) {
            if (!(node is IElementNode) || node is ICustomElementNode) {
                return false;
            }
            return node.ParentNode() is IDocumentNode;
        }
    }
}
