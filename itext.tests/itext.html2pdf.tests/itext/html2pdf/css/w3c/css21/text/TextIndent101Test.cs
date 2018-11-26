using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css21.Text {
    [LogMessage(iText.Html2pdf.LogMessageConstant.CSS_PROPERTY_IN_PERCENTS_NOT_SUPPORTED)]
    public class TextIndent101Test : W3CCssAhemFontTest {
        protected internal override String GetHtmlFileName() {
            return "text-indent-101.xht";
        }
    }
}
