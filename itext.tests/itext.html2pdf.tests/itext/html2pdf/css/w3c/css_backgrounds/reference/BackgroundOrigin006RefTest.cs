using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Reference {
    // TODO DEVSIX-4381 padding-left is supported incorrectly: it's applied on element with absolute positioning
    public class BackgroundOrigin006RefTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-origin-006-ref.html";
        }
    }
}
