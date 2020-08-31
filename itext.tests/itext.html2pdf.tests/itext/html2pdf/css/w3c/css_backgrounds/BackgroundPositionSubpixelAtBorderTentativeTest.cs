using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-1457 support background-position
    // PDF and HTML look pretty the same but background-position is not supported
    public class BackgroundPositionSubpixelAtBorderTentativeTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-position-subpixel-at-border.tentative.html";
        }
    }
}
