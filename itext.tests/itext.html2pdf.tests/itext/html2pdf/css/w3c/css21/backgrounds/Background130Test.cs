using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-1948, DEVSIX-4370. Test passes if there is a filled green rectangle above a short blue stripe, actually it's bellow
    public class Background130Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-130.xht";
        }
    }
}
