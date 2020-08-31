using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4384 box-shadow is not supported
    // Note that the font Ahem must be installed on your system to view valid HTML.
    public class BoxShadowOverlapping004Test : W3CCssAhemFontTest {
        protected internal override String GetHtmlFileName() {
            return "box-shadow-overlapping-004.html";
        }
    }
}
