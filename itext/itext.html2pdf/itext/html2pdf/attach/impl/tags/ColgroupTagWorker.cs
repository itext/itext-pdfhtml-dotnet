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
using iText.Html2pdf.Attach.Wrapelement;
using iText.Html2pdf.Html;
using iText.Layout;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>TagWorker class for a column group.</summary>
    public class ColgroupTagWorker : ITagWorker {
        /// <summary>The column group.</summary>
        private ColgroupWrapper colgroup;

        /// <summary>
        /// Creates a new
        /// <see cref="ColgroupTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public ColgroupTagWorker(IElementNode element, ProcessorContext context) {
            int? span = CssDimensionParsingUtils.ParseInteger(element.GetAttribute(AttributeConstants.SPAN));
            colgroup = new ColgroupWrapper(span != null ? (int)span : 1);
            colgroup.SetLang(element.GetAttribute(AttributeConstants.LANG));
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
            return content == null || String.IsNullOrEmpty(content.Trim());
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processTagChild(com.itextpdf.html2pdf.attach.ITagWorker, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            if (childTagWorker is ColTagWorker) {
                colgroup.GetColumns().Add(((ColTagWorker)childTagWorker).GetColumn());
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

        /// <summary>Gets the column group.</summary>
        /// <returns>the column group</returns>
        public virtual ColgroupWrapper GetColgroup() {
            return colgroup;
        }
    }
}
