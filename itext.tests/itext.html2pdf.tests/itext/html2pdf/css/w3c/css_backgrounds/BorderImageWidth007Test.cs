using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4437 support border-image-outset
    // TODO DEVSIX-4435 support border-image-width
    // TODO DEVSIX-4434 support border-image-slice
    // TODO DEVSIX-4382 support border-image-source
    // Note that html and cmp_pdf look pretty the same, but html has some properties that are currently not supported.
    // It seems that the same result is due to coincidence.
    public class BorderImageWidth007Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "border-image-width-007.xht";
        }
    }
}
