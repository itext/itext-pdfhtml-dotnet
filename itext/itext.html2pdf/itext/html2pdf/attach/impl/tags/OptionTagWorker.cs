/*
This file is part of the iText (R) project.
Copyright (c) 1998-2022 iText Group NV
Authors: iText Software.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
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
            GetElementResult().SetProperty(Html2PdfProperty.FORM_FIELD_SELECTED, selectedAttr);
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
