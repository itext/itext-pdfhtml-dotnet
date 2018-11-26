using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css21.Text {
    [LogMessage(iText.IO.LogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH)]
    public class WhiteSpaceAppliesTo007Test : W3CCssAhemFontTest {
        // TODO DEVSIX-2444 in tables, non-collapsing spaces are not affecting table cell contents..
        protected internal override String GetHtmlFileName() {
            return "white-space-applies-to-007.xht";
        }
    }
}
