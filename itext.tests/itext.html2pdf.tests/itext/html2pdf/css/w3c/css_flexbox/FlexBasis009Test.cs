using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    //TODO DEVSIX-1315 Initial support for flex display:flex CSS property
    [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 10)]
    public class FlexBasis009Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "flex-basis-009.html";
        }
    }
}
