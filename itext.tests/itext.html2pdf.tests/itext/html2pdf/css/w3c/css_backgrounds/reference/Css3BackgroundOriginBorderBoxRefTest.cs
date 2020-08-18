using System;
using iText.Html2pdf.Css.W3c;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Reference {
    // TODO DEVSIX-2449 z-index is not supported
    [LogMessage(iText.IO.LogMessageConstant.OCCUPIED_AREA_HAS_NOT_BEEN_INITIALIZED)]
    public class Css3BackgroundOriginBorderBoxRefTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "css3-background-origin-border-box-ref.html";
        }
    }
}
