using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    //TODO DEVSIX-1315 Initial support for flex display:flex CSS property
    public class AlignItemsBaselineOverflowNonVisibleTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "align-items-baseline-overflow-non-visible.html";
        }
    }
}
