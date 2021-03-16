using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    //TODO DEVSIX-4443 improve segment frequency for dashed border
    //TODO DEVSIX-5178 Incorrect handling of min-height and max-height
    public class SizingHoriz002Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "flexbox-sizing-horiz-002.xhtml";
        }
    }
}
