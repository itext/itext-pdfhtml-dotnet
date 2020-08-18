using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css21.Backgrounds {
    // TODO DEVSIX-4370. A red square exists on the page.
    public class BackgroundAttachment009Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-attachment-009.xht";
        }
    }
}
