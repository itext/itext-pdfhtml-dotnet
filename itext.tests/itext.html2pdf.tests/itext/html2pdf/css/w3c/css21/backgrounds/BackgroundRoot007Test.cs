using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-4413. Background-color special keyword values isn't supported
    public class BackgroundRoot007Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-root-007.xht";
        }
    }
}
