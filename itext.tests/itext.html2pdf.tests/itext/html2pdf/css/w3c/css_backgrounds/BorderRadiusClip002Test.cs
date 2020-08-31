using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-2105	support background-clip
    public class BorderRadiusClip002Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "border-radius-clip-002.htm";
        }
    }
}
