using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Box_shadow {
    // TODO DEVSIX-4384 box-shadow is not supported
    public class BoxShadowBlurDefinition001Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "box-shadow-blur-definition-001.xht";
        }
    }
}
