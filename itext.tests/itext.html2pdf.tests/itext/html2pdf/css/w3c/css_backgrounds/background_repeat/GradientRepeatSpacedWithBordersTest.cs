using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Background_repeat {
    // TODO DEVSIX-4396 background: radial-gradient is not supported
    [LogMessage(iText.StyledXmlParser.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
    public class GradientRepeatSpacedWithBordersTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "gradient-repeat-spaced-with-borders.html";
        }
    }
}
