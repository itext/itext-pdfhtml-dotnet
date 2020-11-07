using iText.Test;

namespace iText.Html2pdf.Attach.Impl.Layout {
    public class PageTargetCountRendererTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void GetNextRendererTest() {
            PageTargetCountRenderer renderer = new PageTargetCountRenderer(new PageTargetCountElement("target"));
            NUnit.Framework.Assert.AreEqual(renderer, renderer.GetNextRenderer());
        }
    }
}
