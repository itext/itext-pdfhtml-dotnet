using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4436 support border-image-repeat
    public class BorderImageRepeatRoundTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "border-image-repeat-round.html";
        }
    }
}
