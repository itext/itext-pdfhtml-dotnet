using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    //TODO DEVSIX-5135 Flex item with nested floating element processed incorrectly
    public class SizingVert002Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "flexbox-sizing-vert-002.xhtml";
        }
    }
}
