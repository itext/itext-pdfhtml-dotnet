using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Background_size {
    // TODO DEVSIX-1708 background-size is not supported
    public class BackgroundSizeCoverTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-size-cover.xht";
        }
    }
}
