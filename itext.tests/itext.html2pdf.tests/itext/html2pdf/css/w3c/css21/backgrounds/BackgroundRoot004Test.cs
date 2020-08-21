using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-4413. Background-color's transparent keyword is not supported.
    public class BackgroundRoot004Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-root-004.xht";
        }
    }
}
