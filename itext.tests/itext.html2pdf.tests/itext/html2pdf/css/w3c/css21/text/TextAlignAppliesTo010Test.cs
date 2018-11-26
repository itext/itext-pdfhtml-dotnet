using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Text {
    public class TextAlignAppliesTo010Test : W3CCssAhemFontTest {
        // TODO DEVSIX-2445: display: table-footer-group; is not supported
        protected internal override String GetHtmlFileName() {
            return "text-align-applies-to-010.xht";
        }
    }
}
