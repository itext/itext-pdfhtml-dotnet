using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-4364. There is NO a marker bullet on the left-hand side of the box.
    public class BackgroundAppliesTo010Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-applies-to-010.xht";
        }
    }
}
