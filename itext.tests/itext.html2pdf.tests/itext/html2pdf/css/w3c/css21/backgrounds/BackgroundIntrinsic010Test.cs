using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-1457. Absolute position is applied incorrectly
    public class BackgroundIntrinsic010Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-intrinsic-010.xht";
        }
    }
}
