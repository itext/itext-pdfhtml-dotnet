using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-2025 check that border-radius: 50% processed correctly and change cmp
    public class CssBorderRadius001Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "css-border-radius-001.html";
        }
    }
}
