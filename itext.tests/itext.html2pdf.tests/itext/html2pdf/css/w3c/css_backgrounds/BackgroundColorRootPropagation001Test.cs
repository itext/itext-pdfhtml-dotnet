using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4440 html with display: none throws IndexOutOfBoundsException
    public class BackgroundColorRootPropagation001Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-color-root-propagation-001.html";
        }

        [NUnit.Framework.Test]
        public override void Test() {
            NUnit.Framework.Assert.That(() =>  {
                base.Test();
            }
            , NUnit.Framework.Throws.InstanceOf<Exception>())
;
        }
    }
}
