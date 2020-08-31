using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4443 improve segment frequency for dashed border
    // TODO DEVSIX-2105 check that PDF is correct after supporting of background-origin
    // Note that currently PDF looks correct cause background-origin: padding-box is default value
    // and it matches to our default implementation
    public class Css3BackgroundOriginPaddingBoxTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "css3-background-origin-padding-box.html";
        }
    }
}
