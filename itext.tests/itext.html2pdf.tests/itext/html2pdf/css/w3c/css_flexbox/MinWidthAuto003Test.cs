using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    //TODO DEVSIX-5087 Support layout properties for FlexContainerRenderer
    public class MinWidthAuto003Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "flexbox-min-width-auto-003.html";
        }
    }
}
