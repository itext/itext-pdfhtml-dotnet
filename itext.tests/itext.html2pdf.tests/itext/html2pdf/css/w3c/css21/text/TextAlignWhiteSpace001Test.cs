using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Text {
    public class TextAlignWhiteSpace001Test : W3CCssAhemFontTest {
        // TODO DEVSIX-2446 text-align: justify + non-collapsible spaces whitespace property
        protected internal override String GetHtmlFileName() {
            return "text-align-white-space-001.xht";
        }
    }
}
