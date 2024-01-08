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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Html;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// Tag worker class for the
    /// <c>abbr</c>
    /// element.
    /// </summary>
    public class AbbrTagWorker : SpanTagWorker {
        /// <summary>
        /// Creates a new
        /// <see cref="AbbrTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the tag</param>
        /// <param name="context">the context</param>
        public AbbrTagWorker(IElementNode element, ProcessorContext context)
            : base(element, context) {
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.tags.SpanTagWorker#processContent(java.lang.String, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public override bool ProcessContent(String content, ProcessorContext context) {
            return base.ProcessContent(content, context);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.tags.SpanTagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public override void ProcessEnd(IElementNode element, ProcessorContext context) {
            base.ProcessEnd(element, context);
            EnrichSpan(element.GetAttribute(AttributeConstants.TITLE));
        }

        /// <summary>Enrich the span with accessibility features, more specifically the expansion text.</summary>
        /// <param name="expansionText">the expansion text</param>
        private void EnrichSpan(String expansionText) {
            foreach (IPropertyContainer container in GetAllElements()) {
                if (container is Text) {
                    Text txt = (Text)container;
                    if (expansionText != null) {
                        txt.GetAccessibilityProperties().SetExpansion(expansionText);
                        break;
                    }
                }
            }
        }
    }
}
