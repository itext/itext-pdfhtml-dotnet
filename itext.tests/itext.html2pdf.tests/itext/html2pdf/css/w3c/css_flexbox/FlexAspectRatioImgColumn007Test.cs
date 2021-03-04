using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    [LogMessage(iText.IO.LogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Count = 1)]
    public class FlexAspectRatioImgColumn007Test : W3CCssTest {
        //TODO DEVSIX-5096 support flex-direction: column
        //TODO DEVSIX-5040 layout: support justify-content and align-items
        //TODO DEVSIX-5004 improve support of flex-items with intrinsic aspect ratio
        protected internal override String GetHtmlFileName() {
            return "flex-aspect-ratio-img-column-007.html";
        }
    }
}
