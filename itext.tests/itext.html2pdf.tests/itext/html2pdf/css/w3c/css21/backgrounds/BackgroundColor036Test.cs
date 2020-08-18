using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
    public class BackgroundColor036Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-color-036.xht";
        }
    }
}
