using iText.Layout.Renderer;
using iText.Test;

namespace iText.Html2pdf.Attach.Impl.Layout {
    public class PageTargetCountElementTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void ConstructorTest() {
            PageTargetCountElement element = new PageTargetCountElement("'aadad''''adad#####'''aaa");
            NUnit.Framework.Assert.AreEqual("aadadadadaaa", element.GetTarget());
        }

        [NUnit.Framework.Test]
        public virtual void MakeNewRendererTest() {
            PageTargetCountElement element = new PageTargetCountElement("'#target'");
            IRenderer renderer = element.GetRenderer();
            NUnit.Framework.Assert.IsTrue(renderer is PageTargetCountRenderer);
        }
    }
}
