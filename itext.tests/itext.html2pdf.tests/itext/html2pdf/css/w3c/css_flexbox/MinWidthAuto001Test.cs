using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    //TODO DEVSIX-4443 improve segment frequency for dashed border
    //TODO DEVSIX-5198 Fix a bug with incorrect processing of the width property in the flex algorithm
    //TODO DEVSIX-5181 Support calc function for width
    public class MinWidthAuto001Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "flexbox-min-width-auto-001.html";
        }
    }
}
