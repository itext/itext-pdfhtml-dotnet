using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4439 border-radius works incorectly in the inner div
    public class BorderRadiusDynamicFromNoRadiusRefTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "border-radius-dynamic-from-no-radius-ref.html";
        }
    }
}
