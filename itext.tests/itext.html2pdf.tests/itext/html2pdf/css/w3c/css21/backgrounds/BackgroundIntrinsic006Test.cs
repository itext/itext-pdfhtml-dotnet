using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-2654. Svg width, height values in percents aren't supported
    [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNKNOWN_ABSOLUTE_METRIC_LENGTH_PARSED, Count = 12)]
    public class BackgroundIntrinsic006Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-intrinsic-006.xht";
        }
    }
}
