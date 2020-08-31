using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4370 support background repeat
    // TODO DEVSIX-2105	support background-origin
    public class BackgroundRepeatBpaceBontentBoxTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background_repeat_space_content_box.htm";
        }
    }
}
