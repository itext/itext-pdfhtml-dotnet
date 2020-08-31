using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-1708 support background-size
    public class BackgroundSize013Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-size-013.html";
        }
    }
}
