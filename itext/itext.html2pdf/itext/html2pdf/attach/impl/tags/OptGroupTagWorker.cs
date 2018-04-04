using System;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Html;
using iText.Html2pdf.Html.Node;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>optgroup</c>
    /// element.
    /// </summary>
    public class OptGroupTagWorker : DivTagWorker {
        /// <summary>
        /// Creates a new
        /// <see cref="OptGroupTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public OptGroupTagWorker(IElementNode element, ProcessorContext context)
            : base(element, context) {
            String label = element.GetAttribute(AttributeConstants.LABEL);
            if (label == null || String.IsNullOrEmpty(label)) {
                label = "\u00A0";
            }
            GetElementResult().SetProperty(Html2PdfProperty.FORM_FIELD_LABEL, label);
            Paragraph p = new Paragraph(label).SetMargin(0);
            p.SetProperty(Property.OVERFLOW_X, OverflowPropertyValue.VISIBLE);
            p.SetProperty(Property.OVERFLOW_Y, OverflowPropertyValue.VISIBLE);
            ((Div)GetElementResult()).Add(p);
        }

        public override bool ProcessContent(String content, ProcessorContext context) {
            return content == null || String.IsNullOrEmpty(content.Trim());
        }
    }
}
