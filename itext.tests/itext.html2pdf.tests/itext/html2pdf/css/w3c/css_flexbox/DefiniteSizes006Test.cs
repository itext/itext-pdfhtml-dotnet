using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    //TODO DEVSIX-5096 support flex-direction: column
    public class DefiniteSizes006Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "flexbox-definite-sizes-006.html";
        }
    }
}
