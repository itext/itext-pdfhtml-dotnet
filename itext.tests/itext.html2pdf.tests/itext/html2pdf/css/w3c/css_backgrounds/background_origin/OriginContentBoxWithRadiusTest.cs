using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Background_origin {
    // TODO DEVSIX-2105 background-origin: content-box is not supported (padding for background is not supported)
    public class OriginContentBoxWithRadiusTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "origin-content-box_with_radius.html";
        }
    }
}
