using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-1457. Background-position is not supported: currently we apply background from the top
    //  and not from the bottom of the area
    public class Background184Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-184.xht";
        }
    }
}
