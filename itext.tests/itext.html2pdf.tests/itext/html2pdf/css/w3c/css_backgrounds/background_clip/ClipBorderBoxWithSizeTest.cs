using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Background_clip {
    // TODO DEVSIX-1708 background-size is not supported
    public class ClipBorderBoxWithSizeTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "clip-border-box_with_size.html";
        }
    }
}
