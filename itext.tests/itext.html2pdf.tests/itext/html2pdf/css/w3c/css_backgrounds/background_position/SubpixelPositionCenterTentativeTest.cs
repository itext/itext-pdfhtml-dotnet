using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Background_position {
    // TODO DEVSIX-1708 background-size is not supported
    // TODO DEVSIX-1457 background-position is not supported
    // TODO DEVSIX-4395 absolute positioning: the order in which absolute-positioned elements are placed in the html is not respected: the one which is higher and/or lefter is written to the pdf first
    public class SubpixelPositionCenterTentativeTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "subpixel-position-center.tentative.html";
        }
    }
}
