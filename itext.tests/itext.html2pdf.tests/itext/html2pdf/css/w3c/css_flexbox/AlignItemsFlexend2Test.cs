using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    //TODO DEVSIX-5040 layout: support justify-content and align-items
    public class AlignItemsFlexend2Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "flexbox_align-items-flexend-2.html";
        }
    }
}
