using System;
using iText.Html2pdf.Css.W3c;
using iText.Kernel;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4391 support iframe tag
    public class BackgroundMarginIframeRootTest : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-margin-iframe-root.html";
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG)]
        public override void Test() {
            NUnit.Framework.Assert.That(() =>  {
                base.Test();
            }
            , NUnit.Framework.Throws.InstanceOf<PdfException>().With.Message.EqualTo(PdfException.DocumentHasNoPages))
;
        }
    }
}
