using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-4370. Backround repeat property isn't supported properly
    public class BackgroundRepeatAppliesTo010Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-repeat-applies-to-010.xht";
        }
    }
}
