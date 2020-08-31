using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4370 support background repeat
    public class BackgroundImageLargeWithAutoTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-image-large-with-auto.html";
        }
    }
}
