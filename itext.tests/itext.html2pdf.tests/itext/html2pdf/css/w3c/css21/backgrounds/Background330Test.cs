using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-1457. Background-position is not supported
    public class Background330Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-330.xht";
        }
    }
}
