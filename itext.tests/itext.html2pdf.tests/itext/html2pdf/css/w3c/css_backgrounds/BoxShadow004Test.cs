using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4384 box-shadow is not supported
    // Note that html and cmp_pdf look pretty the same, but html has some properties that are currently not supported.
    // It seems that the same result is due to coincidence.
    public class BoxShadow004Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "box-shadow-004.htm";
        }
    }
}
