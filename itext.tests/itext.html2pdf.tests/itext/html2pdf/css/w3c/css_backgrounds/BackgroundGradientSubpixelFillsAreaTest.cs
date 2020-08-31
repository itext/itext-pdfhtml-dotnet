using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4433 process horizontal padding correctly
    public class BackgroundGradientSubpixelFillsAreaTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-gradient-subpixel-fills-area.html";
        }
    }
}
