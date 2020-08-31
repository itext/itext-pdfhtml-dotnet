using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4435 support border-image-width
    // TODO DEVSIX-4434 support border-image-slice
    // TODO DEVSIX-4382 support border-image-source
    // TODO DEVSIX-4436 support border-image-repeat
    // Note that html and cmp_pdf look pretty the same, but html has some properties that are currently not supported.
    // It seems that the same result is due to coincidence.
    public class BorderImageSlice003Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "border-image-slice-003.xht";
        }
    }
}
