using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Reference {
    // TODO DEVSIX-4380 background-position-x and background-position-y are not supported
    public class SubpixelRepeatNoRepeatMixRefTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "subpixel-repeat-no-repeat-mix-ref.html";
        }
    }
}
