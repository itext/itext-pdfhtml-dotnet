using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Reference {
    // NOTE a link to a stylesheet files with a custom font was removed, since it's not needed
    public class BackgroundImageFirstLetterRefTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-image-first-letter-ref.html";
        }
    }
}
