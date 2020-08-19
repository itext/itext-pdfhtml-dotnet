using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Background_attachment_local {
    // TODO DEVSIX-4401 background: no-repeat is aligned to the bottom-right corner instead of the top-left one
    public class AttachmentLocalPositioning3RefTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "attachment-local-positioning-3-ref.html";
        }
    }
}
