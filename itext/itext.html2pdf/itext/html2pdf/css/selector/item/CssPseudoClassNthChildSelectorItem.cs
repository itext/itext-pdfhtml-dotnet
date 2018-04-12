using System;
using iText.Html2pdf.Css;

namespace iText.Html2pdf.Css.Selector.Item {
    internal class CssPseudoClassNthChildSelectorItem : CssPseudoClassNthSelectorItem {
        internal CssPseudoClassNthChildSelectorItem(String arguments)
            : base(CssConstants.NTH_CHILD, arguments) {
        }
    }
}
