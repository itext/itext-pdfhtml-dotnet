using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_flexbox {
    //TODO DEVSIX-5163 Support more complex justify-content values
    public class BasicFieldsetHoriz001Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "flexbox-basic-fieldset-horiz-001.xhtml";
        }
    }
}
