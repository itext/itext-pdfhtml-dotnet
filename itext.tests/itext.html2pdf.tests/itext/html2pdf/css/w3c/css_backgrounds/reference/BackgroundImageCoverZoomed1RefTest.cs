using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Reference {
    // TODO DEVSIX-1708 background-size is not supported
    public class BackgroundImageCoverZoomed1RefTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-image-cover-zoomed-1-ref.html";
        }
    }
}
