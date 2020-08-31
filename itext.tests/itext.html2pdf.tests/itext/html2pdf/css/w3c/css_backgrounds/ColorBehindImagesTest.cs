using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-1457	support background-position
    // TODO DEVSIX-4370 support background repeat
    // TODO DEVSIX-2027 process multiple backgrounds
    public class ColorBehindImagesTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "color-behind-images.htm";
        }
    }
}
