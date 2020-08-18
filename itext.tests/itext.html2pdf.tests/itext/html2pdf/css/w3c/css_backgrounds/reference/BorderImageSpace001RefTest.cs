using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Reference {
    // TODO DEVSIX-1457 background-position is not supported
    // TODO DEVSIX-1708 background-size is not supported
    public class BorderImageSpace001RefTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "border-image-space-001-ref.html";
        }
    }
}
