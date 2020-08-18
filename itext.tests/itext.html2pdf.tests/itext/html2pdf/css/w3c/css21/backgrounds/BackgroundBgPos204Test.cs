using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-1948. A single fuchsia diamond should be at the bottom right of the viewport. Now at the top left
    public class BackgroundBgPos204Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-bg-pos-204.xht";
        }
    }
}
