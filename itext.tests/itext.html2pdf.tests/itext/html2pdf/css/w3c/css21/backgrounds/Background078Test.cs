using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-1948, DEVSIX-4370. The blue stripe inside the hollow black square, positioned at the TOP of the black square.
    public class Background078Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-078.xht";
        }
    }
}
