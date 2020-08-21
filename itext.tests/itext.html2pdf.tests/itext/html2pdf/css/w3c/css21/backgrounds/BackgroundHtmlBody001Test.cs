using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-4390. Background of the body element overrides the html background
    public class BackgroundHtmlBody001Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-html-body-001.xht";
        }
    }
}
