using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Background_clip {
    // TODO DEVSIX-2105 background-clip is not supported; content-box is not supported (padding for background is not supported)
    // TODO DEVSIX-1708 background-size is not supported
    public class ClipContentBoxWithSizeTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "clip-content-box_with_size.html";
        }
    }
}
