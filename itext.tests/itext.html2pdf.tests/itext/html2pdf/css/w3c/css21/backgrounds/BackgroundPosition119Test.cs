using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-1457. Background-position isn't supported
    public class BackgroundPosition119Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-position-119.xht";
        }
    }
}
