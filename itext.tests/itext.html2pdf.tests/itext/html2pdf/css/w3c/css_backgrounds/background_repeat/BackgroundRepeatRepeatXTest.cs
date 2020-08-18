using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Background_repeat {
    [LogMessage(iText.IO.LogMessageConstant.OCCUPIED_AREA_HAS_NOT_BEEN_INITIALIZED, Count = 3)]
    public class BackgroundRepeatRepeatXTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-repeat-repeat-x.xht";
        }
    }
}
