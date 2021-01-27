using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    //TODO DEVSIX-1315 Initial support for flex display:flex CSS property
    public class JustifyContentVert002Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "flexbox-justify-content-vert-002.xhtml";
        }
    }
}
