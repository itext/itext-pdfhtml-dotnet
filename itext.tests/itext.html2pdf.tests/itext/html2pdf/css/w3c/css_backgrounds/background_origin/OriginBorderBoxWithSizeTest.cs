using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Background_origin {
    // TODO DEVSIX-2105 background-origin: border-box is not supported
    // TODO DEVSIX-1708 background-size is not supported
    public class OriginBorderBoxWithSizeTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "origin-border-box_with_size.html";
        }
    }
}
