using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-4391. Iframe is not supported.
    [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG)]
    public class BackgroundIframes001Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-iframes-001.xht";
        }
    }
}
