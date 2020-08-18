using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Background_clip {
    // TODO DEVSIX-2105 background-clip is not supported
    public class ClipPaddingBoxWithRadiusTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "clip-padding-box_with_radius.html";
        }
    }
}
