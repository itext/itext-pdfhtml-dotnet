using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    //TODO DEVSIX-5096 support flex-direction: column
    //TODO DEVSIX-5040 layout: support justify-content and align-items
    //TODO DEVSIX-5004 improve support of flex-items with intrinsic aspect ratio
    public class FlexAspectRatioImgColumn009Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "flex-aspect-ratio-img-column-009.html";
        }
    }
}
