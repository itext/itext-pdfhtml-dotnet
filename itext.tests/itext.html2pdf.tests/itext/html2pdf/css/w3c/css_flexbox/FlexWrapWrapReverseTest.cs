using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    //TODO DEVSIX-1315 Initial support for flex display:flex CSS property
    [LogMessage(iText.IO.LogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 18)]
    public class FlexWrapWrapReverseTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "flexbox-flex-wrap-wrap-reverse.htm";
        }
    }
}
