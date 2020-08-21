using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-2445. Display property (list-item) lacking support
    public class BackgroundColorAppliesTo010Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-color-applies-to-010.xht";
        }
    }
}
