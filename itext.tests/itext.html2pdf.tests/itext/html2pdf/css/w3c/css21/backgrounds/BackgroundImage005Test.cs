using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-4416. Background-image shorthand with repeat is not supported
    public class BackgroundImage005Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-image-005.xht";
        }
    }
}
