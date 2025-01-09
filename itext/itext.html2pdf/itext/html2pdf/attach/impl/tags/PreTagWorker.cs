/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
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
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>pre</c>
    /// element.
    /// </summary>
    public class PreTagWorker : DivTagWorker {
        /// <summary>Keeps track to see if any content was processed.</summary>
        private bool anyContentProcessed = false;

        /// <summary>
        /// Creates a new
        /// <see cref="PreTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public PreTagWorker(IElementNode element, ProcessorContext context)
            : base(element, context) {
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.tags.DivTagWorker#processContent(java.lang.String, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public override bool ProcessContent(String content, ProcessorContext context) {
            // It seems that browsers just skip first newline symbol, if any
            if (!anyContentProcessed) {
                if (content.StartsWith("\r\n")) {
                    content = content.Substring(2);
                }
            }
            anyContentProcessed = true;
            return base.ProcessContent(content, context);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.tags.DivTagWorker#processTagChild(com.itextpdf.html2pdf.attach.ITagWorker, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public override bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            anyContentProcessed = true;
            return base.ProcessTagChild(childTagWorker, context);
        }
    }
}
