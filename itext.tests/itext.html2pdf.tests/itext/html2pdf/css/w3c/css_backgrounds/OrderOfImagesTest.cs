using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-2027 process multiple backgrounds
    // TODO DEVSIX-1457	support background-position
    public class OrderOfImagesTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "order-of-images.htm";
        }
    }
}
