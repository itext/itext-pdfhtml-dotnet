using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css21.Positioning {
    [LogMessage(iText.IO.LogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 2)]
    public class PositionRelative009Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "position-relative-009.xht";
        }
    }
}
