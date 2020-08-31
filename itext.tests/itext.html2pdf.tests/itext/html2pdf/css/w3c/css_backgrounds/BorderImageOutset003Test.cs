using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4437 support border-image-outset
    // TODO DEVSIX-4434 support border-image-slice
    public class BorderImageOutset003Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "border-image-outset-003.html";
        }
    }
}
