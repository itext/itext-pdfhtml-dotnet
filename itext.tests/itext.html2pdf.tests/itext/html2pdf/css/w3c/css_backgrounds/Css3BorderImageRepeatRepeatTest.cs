using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4382 border-image-source is not supported
    // TODO DEVSIX-4434 support border-image-slice
    // TODO DEVSIX-4436 support border-image-repeat
    public class Css3BorderImageRepeatRepeatTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "css3-border-image-repeat-repeat.html";
        }
    }
}
