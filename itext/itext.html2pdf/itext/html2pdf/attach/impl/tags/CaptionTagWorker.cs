using iText.Html2pdf.Attach;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>caption</c>
    /// element.
    /// </summary>
    public class CaptionTagWorker : DivTagWorker {
        /// <summary>
        /// Creates a new
        /// <see cref="CaptionTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public CaptionTagWorker(IElementNode element, ProcessorContext context)
            : base(element, context) {
        }
    }
}
