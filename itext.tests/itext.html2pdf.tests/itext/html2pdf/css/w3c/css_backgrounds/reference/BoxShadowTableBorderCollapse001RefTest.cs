using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Reference {
    // TODO DEVSIX-4384 box-shadow is not supported
    public class BoxShadowTableBorderCollapse001RefTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "box-shadow-table-border-collapse-001-ref.html";
        }
    }
}
