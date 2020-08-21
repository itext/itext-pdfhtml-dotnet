using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-2445. Display property (table-column) lacking support
    public class BackgroundColorAppliesTo006Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-color-applies-to-006.xht";
        }
    }
}
