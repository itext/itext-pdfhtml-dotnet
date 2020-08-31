using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-1457 support background-position
    // TODO DEVSIX-2105 support background-clip
    // TODO DEVSIX-4389 support background-color
    public class BackgroundColorPaddingBoxTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background_color_padding_box.htm";
        }
    }
}
