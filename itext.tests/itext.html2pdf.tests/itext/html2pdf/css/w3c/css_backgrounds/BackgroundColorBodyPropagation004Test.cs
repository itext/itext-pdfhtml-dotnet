using System;
using iText.Html2pdf.Css.W3c;
using iText.Kernel;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4440 html with display: none throws exceptions. Remove expected exception after fixing
    public class BackgroundColorBodyPropagation004Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-color-body-propagation-004.html";
        }

        [NUnit.Framework.Test]
        public override void Test() {
            NUnit.Framework.Assert.That(() =>  {
                base.Test();
            }
            , NUnit.Framework.Throws.InstanceOf<PdfException>().With.Message.EqualTo(PdfException.DocumentHasNoPages))
;
        }
    }
}
