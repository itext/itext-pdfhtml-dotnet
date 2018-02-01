using System;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Html.Node;
using iText.Layout;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>TagWorker class for the running elements taken out of the normal flow.</summary>
    public class RunningElementTagWorker : ITagWorker {
        private RunningElement runningElement;

        public RunningElementTagWorker(RunningElementContainer runningElementContainer) {
            runningElement = new RunningElement(runningElementContainer);
        }

        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
        }

        public virtual bool ProcessContent(String content, ProcessorContext context) {
            return false;
        }

        public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            return false;
        }

        public virtual IPropertyContainer GetElementResult() {
            return runningElement;
        }
    }
}
