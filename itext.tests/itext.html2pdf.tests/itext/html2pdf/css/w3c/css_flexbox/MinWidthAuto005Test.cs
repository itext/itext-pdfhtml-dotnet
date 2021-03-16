using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    //TODO DEVSIX-5004 pdfHTML: improve support of flex-items with intrinsic aspect ratio
    //TODO DEVSIX-5087 Support layout properties for FlexContainerRenderer
    public class MinWidthAuto005Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "flexbox-min-width-auto-005.html";
        }
    }
}
