using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Background_size.Vector {
    // TODO DEVSIX-1708 background-size is not supported
    [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNKNOWN_ABSOLUTE_METRIC_LENGTH_PARSED, Count = 9)]
    public class TallAuto32PxPWPHTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "tall--auto-32px--percent-width-percent-height.html";
        }
    }
}
