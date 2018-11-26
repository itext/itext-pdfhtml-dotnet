using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Text {
    public class WhiteSpaceProcessing049Test : W3CCssAhemFontTest {
        // TODO DEVSIX-2459: absolutely positioned blue box width is not set correctly to its intrinsic max-width
        protected internal override String GetHtmlFileName() {
            return "white-space-processing-049.xht";
        }
    }
}
