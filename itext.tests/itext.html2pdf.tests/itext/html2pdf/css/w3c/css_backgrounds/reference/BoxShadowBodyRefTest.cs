using System;
using iText.Html2pdf.Css.W3c;
using iText.Kernel;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds.Reference {
    // TODO DEVSIX-4383 html files with empty body (and css properties on body and html tags) are not processed
    // TODO DEVSIX-4384 box-shadow is not supported
    public class BoxShadowBodyRefTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "box-shadow-body-ref.html";
        }

        [NUnit.Framework.Test]
        public override void Test() {
            NUnit.Framework.Assert.That(() =>  {
                base.Test();
            }
            , NUnit.Framework.Throws.InstanceOf<PdfException>())
;
        }
    }
}
