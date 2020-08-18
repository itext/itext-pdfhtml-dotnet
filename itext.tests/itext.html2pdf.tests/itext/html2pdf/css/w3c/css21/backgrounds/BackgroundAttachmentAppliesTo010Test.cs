using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-2027, DEVSIX-4371. 1) blue stripe should be short 2) there is NO a marker bullet on the left-hand side of the boxes.
    public class BackgroundAttachmentAppliesTo010Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-attachment-applies-to-010.xht";
        }
    }
}
