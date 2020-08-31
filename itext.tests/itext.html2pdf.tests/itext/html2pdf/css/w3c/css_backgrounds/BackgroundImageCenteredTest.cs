using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4423 support repeating-radial-gradient, change cmp and remove log message
    // TODO DEVSIX-1457 support background-position
    [LogMessage(iText.StyledXmlParser.LogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI)]
    public class BackgroundImageCenteredTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-image-centered.html";
        }
    }
}
