using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4400 overflow: hidden is not working with border-radius
    public class BorderRadiusClippingTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "border-radius-clipping.html";
        }
    }
}
