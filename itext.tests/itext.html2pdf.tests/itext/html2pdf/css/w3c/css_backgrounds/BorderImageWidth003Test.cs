using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4435 support border-image-width
    public class BorderImageWidth003Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "border-image-width-003.htm";
        }
    }
}
