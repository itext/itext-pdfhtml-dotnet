using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-1948, DEVSIX-4370. Test passes if there is a green rectangle on top of a blue stripe, actually it's below
    public class Background090Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-090.xht";
        }
    }
}
