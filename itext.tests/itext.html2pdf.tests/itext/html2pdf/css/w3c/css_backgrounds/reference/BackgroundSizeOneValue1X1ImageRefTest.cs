using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Reference {
    // TODO DEVSIX-1708 background-size is not supported
    public class BackgroundSizeOneValue1X1ImageRefTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-size-one-value-1x1-image-ref.html";
        }
    }
}
