using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4442 border-radius properties don't work well together
    public class BorderRadius007RefTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "border-radius-007-ref.xht";
        }
    }
}
