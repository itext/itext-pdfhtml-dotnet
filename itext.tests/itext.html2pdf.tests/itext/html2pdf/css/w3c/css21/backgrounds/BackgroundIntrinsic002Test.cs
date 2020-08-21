using System;
using iText.Html2pdf.Css.W3c;
using iText.Svg.Exceptions;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-2654. Svg height values in percents aren't supported
    [LogMessage(SvgLogMessageConstant.MISSING_HEIGHT, Count = 2)]
    [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNKNOWN_ABSOLUTE_METRIC_LENGTH_PARSED, Count = 4)]
    public class BackgroundIntrinsic002Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-intrinsic-002.xht";
        }
    }
}
