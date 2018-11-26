using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Text {
    public class TextIndentIntrinsic001Test : W3CCssAhemFontTest {
        // TODO 5th and 6th blocks min width is still different from what's in browsers. -- their min-width is bigger than required.
        protected internal override String GetHtmlFileName() {
            return "text-indent-intrinsic-001.xht";
        }
    }
}
