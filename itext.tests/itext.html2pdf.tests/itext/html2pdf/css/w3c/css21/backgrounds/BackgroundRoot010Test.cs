using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-4417. For iText body takes the whole page area, but for browsers body takes just the number
    //  of space which is needed for its elements.
    public class BackgroundRoot010Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-root-010.xht";
        }
    }
}
