using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-1948, DEVSIX-4370. Failed. blue stripe inside the hollow black square, positioned at the top (instead of bottom) of the black square.
    public class Background060Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-060.xht";
        }
    }
}
