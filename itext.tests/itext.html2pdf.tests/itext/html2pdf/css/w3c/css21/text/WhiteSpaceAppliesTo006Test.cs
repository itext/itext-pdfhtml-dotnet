using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Text {
    public class WhiteSpaceAppliesTo006Test : W3CCssAhemFontTest {
        // TODO DEVSIX-2444 in tables, non-collapsing spaces are not affecting table cell contents..
        protected internal override String GetHtmlFileName() {
            return "white-space-applies-to-006.xht";
        }
    }
}
