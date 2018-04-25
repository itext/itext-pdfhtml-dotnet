using System;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Html.Node;
using iText.Layout;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>input</c>
    /// 's placeholder.
    /// </summary>
    public class PlaceholderTagWorker : ITagWorker {
        public PlaceholderTagWorker(IElementNode element, ProcessorContext context) {
        }

        // Do nothing here
        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
        }

        // Do nothing here
        public virtual bool ProcessContent(String content, ProcessorContext context) {
            return false;
        }

        public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            return false;
        }

        public virtual IPropertyContainer GetElementResult() {
            return null;
        }
    }
}
