using System;
using iText.Html2pdf.Css.W3c;
using iText.Kernel;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4383 html files with empty body are not processed. Remove expected exception after fixing
    public class BackgroundSizePercentageRootTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-size-percentage-root.html";
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
