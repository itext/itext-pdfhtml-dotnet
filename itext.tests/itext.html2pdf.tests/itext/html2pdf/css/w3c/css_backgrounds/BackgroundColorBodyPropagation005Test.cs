using System;
using iText.Html2pdf.Css.W3c;

namespace iText.Html2pdf.Css.W3c.Css_backgrounds {
    // TODO DEVSIX-4440 html with display: none throws IndexOutOfBoundsException
    public class BackgroundColorBodyPropagation005Test : W3CCssTest {
        protected internal override String GetHtmlFileName() {
            return "background-color-body-propagation-005.html";
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
