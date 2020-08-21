using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-1903. Margin is not processed correctly
    public class BackgroundRoot002Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-root-002.xht";
        }
    }
}
