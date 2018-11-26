using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css21.Text {
    [LogMessage(iText.Html2pdf.LogMessageConstant.CSS_PROPERTY_IN_PERCENTS_NOT_SUPPORTED)]
    [LogMessage(iText.Html2pdf.LogMessageConstant.MARGIN_VALUE_IN_PERCENT_NOT_SUPPORTED)]
    public class TextIndent104Test : W3CCssAhemFontTest {
        // TODO DEVSIX-1101, DEVSIX-1989: margins and text-indent in percents not supported
        // TODO DEVSIX-1880: negative margins are poorly supported in some cases
        protected internal override String GetHtmlFileName() {
            return "text-indent-104.xht";
        }
    }
}
