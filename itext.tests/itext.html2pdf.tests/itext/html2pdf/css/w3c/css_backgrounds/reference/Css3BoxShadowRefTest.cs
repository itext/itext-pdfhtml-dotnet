using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Reference {
    // TODO DEVSIX-2449 z-index is not supported
    public class Css3BoxShadowRefTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "css3-box-shadow-ref.html";
        }
    }
}
