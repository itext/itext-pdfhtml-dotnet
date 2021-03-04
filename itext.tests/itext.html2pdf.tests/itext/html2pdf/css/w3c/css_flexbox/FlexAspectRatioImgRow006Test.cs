using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    //TODO DEVSIX-5004 improve support of flex-items with intrinsic aspect ratio
    public class FlexAspectRatioImgRow006Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "flex-aspect-ratio-img-row-006.html";
        }
    }
}
