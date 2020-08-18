using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-4364. There is no filled black square.
    public class BackgroundAppliesTo006Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-applies-to-006.xht";
        }
    }
}
