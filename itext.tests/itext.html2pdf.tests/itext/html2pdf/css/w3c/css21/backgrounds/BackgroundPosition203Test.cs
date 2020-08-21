using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-1457. Background-position isn't supported
    [LogMessage(iText.StyledXmlParser.LogMessageConstant.WAS_NOT_ABLE_TO_DEFINE_BACKGROUND_CSS_SHORTHAND_PROPERTIES
        )]
    public class BackgroundPosition203Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-position-203.xht";
        }
    }
}
