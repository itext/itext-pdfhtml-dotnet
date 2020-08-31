using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4380	support background-position-x
    // TODO DEVSIX-1708	Support background-size CSS property in pdfHTML
    // TODO DEVSIX-4370 support background-repeat
    public class SubpixelRepeatNoRepeatMixTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "subpixel-repeat-no-repeat-mix.html";
        }
    }
}
