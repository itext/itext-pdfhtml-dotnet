using iText.Html2pdf.Attach;
using iText.Kernel.Pdf.Tagging;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Tagging;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>TagWorker class for the page margin box.</summary>
    public class PageMarginBoxWorker : DivTagWorker {
        private Div wrappedElementResult;

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
            GetElementResult().SetProperty(Property.COLLAPSING_MARGINS, true);
            if (base.GetElementResult() is IBlockElement) {
                wrappedElementResult = new Div().Add((IBlockElement)base.GetElementResult());
                wrappedElementResult.SetProperty(Property.COLLAPSING_MARGINS, false);
            }
            if (GetElementResult() is IAccessibleElement) {
                ((IAccessibleElement)GetElementResult()).GetAccessibilityProperties().SetRole(StandardRoles.ARTIFACT);
            }
        }

        public override IPropertyContainer GetElementResult() {
            return wrappedElementResult == null ? base.GetElementResult() : wrappedElementResult;
        }
    }
}
