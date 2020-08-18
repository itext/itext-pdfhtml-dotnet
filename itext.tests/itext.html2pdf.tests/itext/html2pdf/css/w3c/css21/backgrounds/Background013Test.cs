using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-1948. Failed, cat image should not cut off at the bottom
    public class Background013Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-013.xht";
        }
    }
}
