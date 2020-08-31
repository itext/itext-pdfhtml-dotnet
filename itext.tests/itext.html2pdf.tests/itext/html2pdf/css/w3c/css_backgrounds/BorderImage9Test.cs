using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4382 border-image-source is not supported
    public class BorderImage9Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "border-image-9.html";
        }
    }
}
