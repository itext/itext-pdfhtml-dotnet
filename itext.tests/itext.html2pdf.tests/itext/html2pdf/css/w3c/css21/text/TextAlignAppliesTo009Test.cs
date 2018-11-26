using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Text {
    public class TextAlignAppliesTo009Test : W3CCssAhemFontTest {
        // TODO DEVSIX-2445: display: table-header-group; is not supported
        protected internal override String GetHtmlFileName() {
            return "text-align-applies-to-009.xht";
        }
    }
}
