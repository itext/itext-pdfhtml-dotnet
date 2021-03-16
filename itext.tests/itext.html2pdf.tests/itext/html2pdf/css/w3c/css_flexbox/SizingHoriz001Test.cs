using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    // TODO DEVSIX-4443 improve segment frequency for dashed border
    public class SizingHoriz001Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "flexbox-sizing-horiz-001.xhtml";
        }
    }
}
