using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css3_selectors {
    [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG)]
    public class Css3ModselD1bTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "css3-modsel-d1b.html";
        }
    }
}
