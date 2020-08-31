using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-2027 process multiple backgrounds
    // TODO DEVSIX-1457	support background-position
    // TODO DEVSIX-4370 support background-repeat
    public class NoneAsImageLayerTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "none-as-image-layer.htm";
        }
    }
}
