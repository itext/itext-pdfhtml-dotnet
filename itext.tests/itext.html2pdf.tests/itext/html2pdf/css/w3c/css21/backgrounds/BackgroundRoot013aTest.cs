using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-4413. Background-color special keyword values isn't supported
    [LogMessage(iText.Html2pdf.LogMessageConstant.MARGIN_VALUE_IN_PERCENT_NOT_SUPPORTED, Count = 6)]
    public class BackgroundRoot013aTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-root-013a.xht";
        }
    }
}
