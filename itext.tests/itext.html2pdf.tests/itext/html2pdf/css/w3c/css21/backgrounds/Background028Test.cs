using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-1948. Failed - the one cat in the right top corner is cut off.
    public class Background028Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-028.xht";
        }
    }
}
