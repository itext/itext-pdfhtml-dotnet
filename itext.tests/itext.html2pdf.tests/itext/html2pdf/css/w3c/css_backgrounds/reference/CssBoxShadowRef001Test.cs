using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Reference {
    [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG)]
    public class CssBoxShadowRef001Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "css-box-shadow-ref-001.html";
        }
    }
}
