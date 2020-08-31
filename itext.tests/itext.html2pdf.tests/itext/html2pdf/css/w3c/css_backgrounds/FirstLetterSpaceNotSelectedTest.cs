using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // HTML doesn't correspond its description at least in google chrome.
    // There are red rectangles behind the letters. But the resulting PDF looks correct.
    public class FirstLetterSpaceNotSelectedTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "first-letter-space-not-selected.html";
        }
    }
}
