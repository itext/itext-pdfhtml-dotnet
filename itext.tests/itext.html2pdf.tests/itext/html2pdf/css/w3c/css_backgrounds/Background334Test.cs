using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-1708 support background size
    // TODO DEVSIX-1457 support background-position
    [LogMessage(iText.StyledXmlParser.LogMessageConstant.WAS_NOT_ABLE_TO_DEFINE_BACKGROUND_CSS_SHORTHAND_PROPERTIES
        )]
    public class Background334Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-334.html";
        }
    }
}
