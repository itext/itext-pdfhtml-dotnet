using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Reference {
    // TODO DEVSIX-4382 border-image-source is not supported
    public class BorderImageShorthand001RefTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "border-image-shorthand-001-ref.html";
        }
    }
}
