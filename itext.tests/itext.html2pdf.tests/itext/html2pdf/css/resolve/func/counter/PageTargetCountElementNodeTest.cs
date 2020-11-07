using System;
using iText.Test;

namespace iText.Html2pdf.Css.Resolve.Func.Counter {
    public class PageTargetCountElementNodeTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void ConstructorTest() {
            String target = "tarGet";
            PageTargetCountElementNode node = new PageTargetCountElementNode(null, target);
            NUnit.Framework.Assert.AreEqual("_e0d00a6_page-counter", node.Name());
            NUnit.Framework.Assert.AreEqual(target, node.GetTarget());
            NUnit.Framework.Assert.IsNull(node.ParentNode());
        }
    }
}
