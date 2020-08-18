using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-1948, DEVSIX-4370. A blue stripe should be positioned at the bottom of the black square.
    public class Background055Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-055.xht";
        }
    }
}
