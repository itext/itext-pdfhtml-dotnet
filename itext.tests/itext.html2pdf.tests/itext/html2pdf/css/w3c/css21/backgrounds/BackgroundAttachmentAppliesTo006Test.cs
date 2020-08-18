using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-2027, DEVSIX-4371. Expected: there is a short blue stripe above a tall orange rectangle. Actual: nothing.
    public class BackgroundAttachmentAppliesTo006Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-attachment-applies-to-006.xht";
        }
    }
}
