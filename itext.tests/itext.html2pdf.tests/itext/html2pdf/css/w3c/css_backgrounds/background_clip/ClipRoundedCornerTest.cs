using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Background_clip {
    // TODO DEVSIX-4394 there is no default body's margin in iText
    public class ClipRoundedCornerTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "clip-rounded-corner.html";
        }
    }
}
