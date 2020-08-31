using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-2105 support background-clip
    public class Css3BackgroundClipContentBoxTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "css3-background-clip-content-box.html";
        }
    }
}
