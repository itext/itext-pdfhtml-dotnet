using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-1457 support background-position
    public class BackgroundPropertiesGreaterThanImagesTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background_properties_greater_than_images.htm";
        }
    }
}
