using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Background_origin {
    // TODO DEVSIX-2105 background-origin: content-box is not supported (padding for background is not supported)
    // TODO DEVSIX-1457 background-position is not supported
    public class OriginContentBoxWithPositionTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "origin-content-box_with_position.html";
        }
    }
}
