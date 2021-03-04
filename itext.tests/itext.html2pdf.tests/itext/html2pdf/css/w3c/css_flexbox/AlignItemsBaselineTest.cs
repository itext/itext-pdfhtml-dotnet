using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    //TODO DEVSIX-5040 layout: support justify-content and align-items
    public class AlignItemsBaselineTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "flexbox_align-items-baseline.html";
        }
    }
}
