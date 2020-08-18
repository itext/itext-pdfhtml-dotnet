using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Background_size {
    // TODO DEVSIX-1708 background-size is not supported
    // TODO DEVSIX-1708 the height of the background in browsers is less than in pdf (as if the svg is shorten on some level)
    public class BackgroundSizeCoverSvgTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-size-cover-svg.html";
        }
    }
}
