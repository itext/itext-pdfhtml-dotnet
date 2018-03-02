using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css3_selectors {
    [LogMessage(iText.Html2pdf.LogMessageConstant.ERROR_PARSING_CSS_SELECTOR, Count = 6)]
    public class Css3Modsel27aTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "css3-modsel-27a.html";
        }
    }
}
