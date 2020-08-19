using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Background_attachment_local {
    // TODO DEVSIX-4398 border-radius is not supported for double borders
    // TODO DEVSIX-4400 overflow: hidden is not working with border-radius
    [LogMessage(iText.IO.LogMessageConstant.METHOD_IS_NOT_IMPLEMENTED_BY_DEFAULT_OTHER_METHOD_WILL_BE_USED, Count
         = 4)]
    public class AttachmentLocalClippingImage6RefTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "attachment-local-clipping-image-6-ref.html";
        }
    }
}
