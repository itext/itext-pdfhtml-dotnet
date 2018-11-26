using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Text {
    public class TextTransformAppliesTo001Test : W3CCssAhemFontTest {
        // TODO DEVSIX-2448: transform: capitalize not supported
        protected internal override String GetHtmlFileName() {
            return "text-transform-applies-to-001.xht";
        }
    }
}
