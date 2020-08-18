using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Background_size.Vector {
    // TODO DEVSIX-4388 percentage is not supported for rect's x, y, width and height
    [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNKNOWN_ABSOLUTE_METRIC_LENGTH_PARSED, Count = 5)]
    public class TallAuto32PxOWOHVbTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "tall--auto-32px--omitted-width-omitted-height-viewbox.html";
        }
    }
}
