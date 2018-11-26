using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Text {
    public class TextIndent012Test : W3CCssAhemFontTest {
        // TODO DEVSIX-2450: max width for div with float and inline-block is not calculated properly
        protected internal override String GetHtmlFileName() {
            return "text-indent-012.xht";
        }
    }
}
