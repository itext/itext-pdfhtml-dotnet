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
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Attach.Wrapelement;
using iText.Html2pdf.Html;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>tr</c>
    /// element.
    /// </summary>
    public class TrTagWorker : ITagWorker {
        /// <summary>The row wrapper.</summary>
        private TableRowWrapper rowWrapper;

        /// <summary>The parent tag worker.</summary>
        private ITagWorker parentTagWorker;

        /// <summary>The lang attribute value.</summary>
        private String lang;

        /// <summary>
        /// Creates a new
        /// <see cref="TrTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public TrTagWorker(IElementNode element, ProcessorContext context) {
            rowWrapper = new TableRowWrapper();
            parentTagWorker = context.GetState().Empty() ? null : context.GetState().Top();
            if (parentTagWorker is TableTagWorker) {
                ((TableTagWorker)parentTagWorker).ApplyColStyles();
            }
            lang = element.GetAttribute(AttributeConstants.LANG);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processContent(java.lang.String, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual bool ProcessContent(String content, ProcessorContext context) {
            return parentTagWorker != null && parentTagWorker.ProcessContent(content, context);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processTagChild(com.itextpdf.html2pdf.attach.ITagWorker, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            if (childTagWorker.GetElementResult() is Cell) {
                Cell cell = (Cell)childTagWorker.GetElementResult();
                AccessiblePropHelper.TrySetLangAttribute(cell, lang);
                rowWrapper.AddCell(cell);
                return true;
            }
            return false;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#getElementResult()
        */
        public virtual IPropertyContainer GetElementResult() {
            return null;
        }

//\cond DO_NOT_DOCUMENT
        /// <summary>Gets the table row wrapper.</summary>
        /// <returns>the table row wrapper</returns>
        internal virtual TableRowWrapper GetTableRowWrapper() {
            return rowWrapper;
        }
//\endcond
    }
}
