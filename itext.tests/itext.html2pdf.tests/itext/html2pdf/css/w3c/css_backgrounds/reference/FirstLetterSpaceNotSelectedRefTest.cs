using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Reference {
    // TODO DEVSIX-4387 &nbsp; &ensp; &emsp; &thinsp; are not processed correctly
    public class FirstLetterSpaceNotSelectedRefTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "first-letter-space-not-selected-ref.html";
        }
    }
}
