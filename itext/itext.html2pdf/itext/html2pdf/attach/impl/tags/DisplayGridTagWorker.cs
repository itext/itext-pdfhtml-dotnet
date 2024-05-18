using iText.Html2pdf.Attach;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// <see cref="iText.Html2pdf.Attach.ITagWorker"/>
    /// implementation for elements with
    /// <c>display: grid</c>.
    /// </summary>
    public class DisplayGridTagWorker : DivTagWorker {
        public DisplayGridTagWorker(IElementNode element, ProcessorContext context)
            : base(element, context, new GridContainer()) {
        }
    }
}
