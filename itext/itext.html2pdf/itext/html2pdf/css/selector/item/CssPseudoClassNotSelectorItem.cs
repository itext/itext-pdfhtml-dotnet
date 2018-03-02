using System.Collections.Generic;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Parse;
using iText.Html2pdf.Css.Selector;
using iText.Html2pdf.Html.Node;

namespace iText.Html2pdf.Css.Selector.Item {
    internal class CssPseudoClassNotSelectorItem : CssPseudoClassSelectorItem {
        private ICssSelector argumentsSelector;

        internal CssPseudoClassNotSelectorItem(ICssSelector argumentsSelector)
            : base(CssConstants.NOT, argumentsSelector.ToString()) {
            this.argumentsSelector = argumentsSelector;
        }

        public virtual IList<ICssSelectorItem> GetArgumentsSelector() {
            return CssSelectorParser.ParseSelectorItems(arguments);
        }

        public override bool Matches(INode node) {
            return !argumentsSelector.Matches(node);
        }
    }
}
