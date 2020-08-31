using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-2449 z-index is not supported
    public class BorderImage017Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "border-image-017.xht";
        }
    }
}
