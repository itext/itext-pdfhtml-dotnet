using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-1948. The image on the right is cut off.
    public class Background040Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-040.xht";
        }
    }
}
