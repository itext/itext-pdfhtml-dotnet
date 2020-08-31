using System;
using iText.Html2pdf.Css.W3c;
using iText.Kernel;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4384 box-shadow is not supported
    // TODO DEVSIX-4383 remove expected exception after fixing
    public class BoxShadowBodyTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "box-shadow-body.html";
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
