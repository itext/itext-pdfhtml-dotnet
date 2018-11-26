using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Text {
    public class WhiteSpaceMixed001Test : W3CCssAhemFontTest {
        // TODO https://wiki.itextsupport.com/display/IT7/HTML-CSS+inline+context+limitations: ignores nowrap on inline elements
        protected internal override String GetHtmlFileName() {
            return "white-space-mixed-001.xht";
        }
    }
}
