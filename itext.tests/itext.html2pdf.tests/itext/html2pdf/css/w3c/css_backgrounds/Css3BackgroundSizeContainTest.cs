using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-1708 support background-size
    public class Css3BackgroundSizeContainTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "css3-background-size-contain.html";
        }
    }
}
