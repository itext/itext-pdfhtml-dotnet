using iText.Html2pdf.Attach;
using iText.Html2pdf.Html.Node;
using iText.Kernel.Pdf.Tagging;
using iText.Layout.Tagging;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>TagWorker class for the page margin box.</summary>
    public class PageMarginBoxWorker : DivTagWorker {
        /// <summary>
        /// Creates a new
        /// <see cref="PageMarginBoxWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public PageMarginBoxWorker(IElementNode element, ProcessorContext context)
            : base(element, context) {
        }

        public override void ProcessEnd(IElementNode element, ProcessorContext context) {
            base.ProcessEnd(element, context);
            if (GetElementResult() is IAccessibleElement) {
                ((IAccessibleElement)GetElementResult()).GetAccessibilityProperties().SetRole(StandardRoles.ARTIFACT);
            }
        }
    }
}
