/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
Authors: Apryse Software.

This program is offered under a commercial and under the AGPL license.
For commercial licensing, contact us at https://itextpdf.com/sales.  For AGPL licensing, see below.

AGPL licensing:
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.Text;
using iText.Forms.Form;
using iText.Html2pdf.Attach;
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
        // extends DivTagWorker for the sake of pseudo elements
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
            bool selectedAttr = element.GetAttribute(AttributeConstants.SELECTED) != null;
            GetElementResult().SetProperty(FormProperty.FORM_FIELD_SELECTED, selectedAttr);
            actualOptionTextContent = new StringBuilder();
            labelAttrVal = element.GetAttribute(AttributeConstants.LABEL);
            if (labelAttrVal != null) {
                if (element.ChildNodes().IsEmpty()) {
                    // workaround to ensure that processContent method is called
                    element.AddChild(new JsoupTextNode(new TextNode("")));
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
            GetElementResult().SetProperty(FormProperty.FORM_FIELD_VALUE, valueAttr);
            GetElementResult().SetProperty(FormProperty.FORM_FIELD_LABEL, labelAttr);
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
                // TODO DEVSIX-2443: spaces are not collapsed according to white-space property in here
                actualOptionTextContent.Append(content);
            }
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
