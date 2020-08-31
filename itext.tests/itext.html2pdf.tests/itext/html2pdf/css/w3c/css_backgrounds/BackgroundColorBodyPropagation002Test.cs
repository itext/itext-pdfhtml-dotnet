using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4390 background of the body element overrides the html background
    public class BackgroundColorBodyPropagation002Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-color-body-propagation-002.html";
        }
    }
}
