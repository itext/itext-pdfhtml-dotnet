using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Text {
    public class TextAlignAppliesTo003Test : W3CCssAhemFontTest {
        // TODO DEVSIX-2445 display: list-item is not supported for spans
        protected internal override String GetHtmlFileName() {
            return "text-align-applies-to-003.xht";
        }
    }
}
