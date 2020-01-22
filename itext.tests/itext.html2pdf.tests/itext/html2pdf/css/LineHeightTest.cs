using System.Collections.Generic;
using iText.Html2pdf;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Css {
    public class LineHeightTest {
        [NUnit.Framework.Test]
        public virtual void DefaultLineHeightTest() {
            IList<IElement> elements = HtmlConverter.ConvertToElements("<p>Lorem Ipsum</p>");
            NUnit.Framework.Assert.AreEqual(1.2f, elements[0].GetProperty<Leading>(Property.LEADING).GetValue(), 1e-10
                );
        }
    }
}
