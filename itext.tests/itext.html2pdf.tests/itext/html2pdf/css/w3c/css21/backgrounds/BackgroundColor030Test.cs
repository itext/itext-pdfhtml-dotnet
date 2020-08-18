using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
    [LogMessage(iText.IO.LogMessageConstant.UNKNOWN_COLOR_FORMAT_MUST_BE_RGB_OR_RRGGBB)]
    public class BackgroundColor030Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-color-030.xht";
        }
    }
}
