using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    //TODO DEVSIX-5096 support flex-direction: column
    //TODO DEVSIX-5135 Flex item with nested floating element processed incorrectly
    //TODO DEVSIX-5178 Incorrect handling of min-height and max-height
    public class SizingVert001Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "flexbox-sizing-vert-001.xhtml";
        }
    }
}
