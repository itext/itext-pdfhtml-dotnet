using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-2105 support background-clip: padding-box
    public class BackgroundClipPaddingBoxTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-clip-padding-box.html";
        }
    }
}
