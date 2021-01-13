using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    //TODO DEVSIX-1315 Initial support for flex display:flex CSS property
    [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG, Count = 16)]
    public class FlexBasisContent004BTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "flexbox-flex-basis-content-004b.html";
        }
    }
}
