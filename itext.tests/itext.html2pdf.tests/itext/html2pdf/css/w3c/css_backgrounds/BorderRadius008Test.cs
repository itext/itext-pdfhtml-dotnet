using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4438 validate border-radius properly
    public class BorderRadius008Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "border-radius-008.xht";
        }
    }
}
