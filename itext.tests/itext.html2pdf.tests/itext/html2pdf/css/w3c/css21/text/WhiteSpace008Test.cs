using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Text {
    public class WhiteSpace008Test : W3CCssAhemFontTest {
        // TODO  DEVSIX-2449 z-index property not supported
        protected internal override String GetHtmlFileName() {
            return "white-space-008.xht";
        }
    }
}
