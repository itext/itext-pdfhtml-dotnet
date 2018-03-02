using System;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html.Node;

namespace iText.Html2pdf.Css.Selector.Item {
    internal class CssPseudoClassEmptySelectorItem : CssPseudoClassSelectorItem {
        private static readonly iText.Html2pdf.Css.Selector.Item.CssPseudoClassEmptySelectorItem instance = new iText.Html2pdf.Css.Selector.Item.CssPseudoClassEmptySelectorItem
            ();

        private CssPseudoClassEmptySelectorItem()
            : base(CssConstants.EMPTY) {
        }

        public static iText.Html2pdf.Css.Selector.Item.CssPseudoClassEmptySelectorItem GetInstance() {
            return instance;
        }

        public override bool Matches(INode node) {
            if (!(node is IElementNode) || node is ICustomElementNode) {
                return false;
            }
            if (node.ChildNodes().IsEmpty()) {
                return true;
            }
            foreach (INode childNode in node.ChildNodes()) {
                if (!(childNode is ITextNode) || !String.IsNullOrEmpty(((ITextNode)childNode).WholeText())) {
                    return false;
                }
            }
            return true;
        }
    }
}
