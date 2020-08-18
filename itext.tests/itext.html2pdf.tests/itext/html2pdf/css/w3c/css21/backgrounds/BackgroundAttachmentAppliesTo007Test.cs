using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-2027, DEVSIX-4371. Expected: a blue stripe should be short
    public class BackgroundAttachmentAppliesTo007Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-attachment-applies-to-007.xht";
        }
    }
}
