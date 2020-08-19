using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Background_attachment_local {
    // TODO DEVSIX-4402 background's position is not supported
    public class AttachmentScrollPositioning1RefTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "attachment-scroll-positioning-1-ref.html";
        }
    }
}
