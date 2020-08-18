using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-1948, DEVSIX-4370. There is a green rectangle on bottom of a blue stripe.
    public class Background114Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-114.xht";
        }
    }
}
