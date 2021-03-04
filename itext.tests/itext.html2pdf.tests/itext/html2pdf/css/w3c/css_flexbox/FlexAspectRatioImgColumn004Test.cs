using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    //TODO DEVSIX-5096 support flex-direction: column
    //TODO DEVSIX-5004 pdfHTML: improve support of flex-items with intrinsic aspect ratio
    //TODO DEVSIX-5137 support margin collapse
    public class FlexAspectRatioImgColumn004Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "flex-aspect-ratio-img-column-004.html";
        }
    }
}
