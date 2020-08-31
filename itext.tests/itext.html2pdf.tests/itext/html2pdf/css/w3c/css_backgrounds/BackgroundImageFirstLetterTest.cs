using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4425 suppot first-letter
    public class BackgroundImageFirstLetterTest : W3CCssAhemFontTest {
        protected internal override String GetHtmlFileName() {
            return "background-image-first-letter.html";
        }
    }
}
