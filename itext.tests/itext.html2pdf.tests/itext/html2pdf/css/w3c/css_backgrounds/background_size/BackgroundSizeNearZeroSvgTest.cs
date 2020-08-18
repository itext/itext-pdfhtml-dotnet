using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Background_size {
    // DEVSIX-1708 TODO background-size is not supported
    public class BackgroundSizeNearZeroSvgTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-size-near-zero-svg.html";
        }
    }
}
