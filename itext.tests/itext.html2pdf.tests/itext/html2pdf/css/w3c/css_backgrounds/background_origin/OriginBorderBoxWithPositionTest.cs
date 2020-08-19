using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Background_origin {
    // TODO DEVSIX-2105 background-origin: border-box is not supported
    // TODO DEVSIX-1457 background-position is not supported
    public class OriginBorderBoxWithPositionTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "origin-border-box_with_position.html";
        }
    }
}
