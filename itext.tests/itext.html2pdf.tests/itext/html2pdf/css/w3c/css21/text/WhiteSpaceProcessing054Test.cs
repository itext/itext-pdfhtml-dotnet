using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Text {
    public class WhiteSpaceProcessing054Test : W3CCssAhemFontTest {
        // TODO DEVSIX-2443 An 'en quad' characters is not collapsed by the white space processing model
        protected internal override String GetHtmlFileName() {
            return "white-space-processing-054.xht";
        }
    }
}
