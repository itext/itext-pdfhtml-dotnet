using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4434 support border-image-slice
    // TODO DEVSIX-4435 support border-image-width
    // TODO DEVSIX-4436 support border-image-repeat
    public class BorderImageSpace001Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "border-image-space-001.html";
        }
    }
}
