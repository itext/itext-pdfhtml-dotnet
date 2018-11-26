using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Text {
    public class WhiteSpaceMixed002Test : W3CCssAhemFontTest {
        // TODO DEVSIX-2443: collapsing doesn't work exactly as needed
        protected internal override String GetHtmlFileName() {
            return "white-space-mixed-002.xht";
        }
    }
}
