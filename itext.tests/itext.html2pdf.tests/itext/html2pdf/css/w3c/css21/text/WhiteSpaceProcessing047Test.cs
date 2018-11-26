using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Text {
    public class WhiteSpaceProcessing047Test : W3CCssAhemFontTest {
        // TODO DEVSIX-2443 Space is not removed at the end of the line when 'white-space' is set to 'pre-wrap'
        protected internal override String GetHtmlFileName() {
            return "white-space-processing-047.xht";
        }
    }
}
