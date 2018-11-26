using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Text {
    public class WhiteSpaceProcessing042Test : W3CCssAhemFontTest {
        // TODO tab stops are not processed when tab characters are encountered
        protected internal override String GetHtmlFileName() {
            return "white-space-processing-042.xht";
        }
    }
}
