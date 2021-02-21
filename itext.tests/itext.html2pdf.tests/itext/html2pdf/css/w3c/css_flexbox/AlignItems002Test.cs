using System;
using iText.Html2pdf.Css.W3c;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN
        )]
    public class AlignItems002Test : W3CCssTest {
        //TODO DEVSIX-1315 Initial support for flex display:flex CSS property
        protected internal override String GetHtmlFileName() {
            return "align-items-002.htm";
        }
    }
}
