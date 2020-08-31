using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-2105 support background-origin
    public class Css3BackgroundOriginContentBoxTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "css3-background-origin-content-box.html";
        }
    }
}
