using System;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Css.Resolve.Func.Counter;
using iText.Layout;
using iText.Test;

namespace iText.Html2pdf.Attach.Impl.Tags {
    public class PageCountWorkerTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void PageTargetCountElementNodeTest() {
            String target = "target";
            PageCountWorker worker = new PageCountWorker(new PageTargetCountElementNode(null, target), null);
            IPropertyContainer container = worker.GetElementResult();
            NUnit.Framework.Assert.IsTrue(container is PageTargetCountElement);
            NUnit.Framework.Assert.AreEqual(target, ((PageTargetCountElement)container).GetTarget());
        }

        [NUnit.Framework.Test]
        public virtual void PageCountElementNodeTest() {
            PageCountWorker worker = new PageCountWorker(new PageCountElementNode(false, null), null);
            IPropertyContainer container = worker.GetElementResult();
            NUnit.Framework.Assert.IsTrue(container is PageCountElement);
            NUnit.Framework.Assert.AreEqual(PageCountType.CURRENT_PAGE_NUMBER, container.GetProperty<PageCountType?>(Html2PdfProperty
                .PAGE_COUNT_TYPE));
        }

        [NUnit.Framework.Test]
        public virtual void PagesCountElementNodeTest() {
            PageCountWorker worker = new PageCountWorker(new PageCountElementNode(true, null), null);
            IPropertyContainer container = worker.GetElementResult();
            NUnit.Framework.Assert.IsTrue(container is PageCountElement);
            NUnit.Framework.Assert.AreEqual(PageCountType.TOTAL_PAGE_COUNT, container.GetProperty<PageCountType?>(Html2PdfProperty
                .PAGE_COUNT_TYPE));
        }
    }
}
