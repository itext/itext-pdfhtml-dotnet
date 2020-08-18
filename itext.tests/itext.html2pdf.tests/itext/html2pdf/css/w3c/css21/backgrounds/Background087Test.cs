using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-1948, DEVSIX-4370. Test passes if there is an orange rectangle above a blue stripe, actually BELOW
    public class Background087Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-087.xht";
        }
    }
}
