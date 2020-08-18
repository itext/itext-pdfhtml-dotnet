using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Reference {
    // TODO DEVSIX-3757 bottom border is two times wider than needed
    public class Background334RefTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-334-ref.xht";
        }
    }
}
