using System;
using System.Collections.Generic;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html.Node;

namespace iText.Html2pdf.Css.Selector.Item {
    internal class CssPseudoClassNthOfTypeSelectorItem : CssPseudoClassNthSelectorItem {
        public CssPseudoClassNthOfTypeSelectorItem(String arguments)
            : base(CssConstants.NTH_OF_TYPE, arguments) {
        }

        public override bool Matches(INode node) {
            if (!(node is IElementNode) || node is ICustomElementNode || node is IDocumentNode) {
                return false;
            }
            IList<INode> children = GetAllSiblingsOfNodeType(node);
            return !children.IsEmpty() && ResolveNth(node, children);
        }
    }
}
