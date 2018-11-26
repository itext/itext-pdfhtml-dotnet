using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css21.Text {
    [LogMessage(iText.IO.LogMessageConstant.RECTANGLE_HAS_NEGATIVE_OR_ZERO_SIZES, Count = 8)]
    public class WhiteSpaceNormal001Test : W3CCssAhemFontTest {
        protected internal override String GetHtmlFileName() {
            return "white-space-normal-001.xht";
        }
    }
}
