using System;
using System.Text;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Html;
using iText.StyledXmlParser.Jsoup.Nodes;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>option</c>
    /// element.
    /// </summary>
    public class OptionTagWorker : DivTagWorker {
        private StringBuilder actualOptionTextContent;

        private String labelAttrVal;

        private bool labelReplacedContent;

        private bool fakedContent;

        /// <summary>
        /// Creates a new
        /// <see cref="OptionTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public OptionTagWorker(IElementNode element, ProcessorContext context)
            : base(element, context) {
            // extends DivTagWorker for the sake of pseudo elements
            bool selectedAttr = element.GetAttribute(AttributeConstants.SELECTED) != null;
            GetElementResult().SetProperty(Html2PdfProperty.FORM_FIELD_SELECTED, selectedAttr);
            actualOptionTextContent = new StringBuilder();
            labelAttrVal = element.GetAttribute(AttributeConstants.LABEL);
            if (labelAttrVal != null) {
                if (element.ChildNodes().IsEmpty()) {
                    // workaround to ensure that processContent method is called
                    element.AddChild(new JsoupTextNode(new TextNode("", "")));
                    fakedContent = true;
                }
            }
        }

        public override void ProcessEnd(IElementNode element, ProcessorContext context) {
            base.ProcessEnd(element, context);
            String valueAttr = element.GetAttribute(AttributeConstants.VALUE);
            String labelAttr = element.GetAttribute(AttributeConstants.LABEL);
            String content = actualOptionTextContent.ToString();
            if (labelAttr == null) {
                labelAttr = content;
            }
            if (valueAttr == null) {
                valueAttr = content;
            }
            GetElementResult().SetProperty(Html2PdfProperty.FORM_FIELD_VALUE, valueAttr);
            GetElementResult().SetProperty(Html2PdfProperty.FORM_FIELD_LABEL, labelAttr);
        }

        public override bool ProcessContent(String content, ProcessorContext context) {
            content = content.Trim();
            // white-space:pre is overridden in chrome in user agent css for list-boxes, we don't do this for now
            if (labelAttrVal != null) {
                if (!labelReplacedContent) {
                    labelReplacedContent = true;
                    base.ProcessContent(labelAttrVal, context);
                }
            }
            else {
                base.ProcessContent(content, context);
            }
            if (!fakedContent) {
                actualOptionTextContent.Append(content);
            }
            // TODO DEVSIX-1901: spaces are not collapsed according to white-space property in here
            return true;
        }

        public override bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            // Currently, it seems that jsoup handles all the inner content of options exactly the same way as browsers do,
            // removing all the inner tags and leaving only the text content.
            // This method is meant to handle only pseudo elements (:before and :after).
            return base.ProcessTagChild(childTagWorker, context);
        }
    }
}
