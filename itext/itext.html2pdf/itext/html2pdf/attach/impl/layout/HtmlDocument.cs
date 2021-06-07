/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
Authors: iText Software.

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
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;

namespace iText.Html2pdf.Attach.Impl.Layout {
    /// <summary>
    /// This class is a wrapper on
    /// <see cref="iText.Layout.Document"/>
    /// , which is the default root element while creating a self-sufficient PDF.
    /// </summary>
    /// <remarks>
    /// This class is a wrapper on
    /// <see cref="iText.Layout.Document"/>
    /// , which is the default root element while creating a self-sufficient PDF.
    /// It contains several html-specific customizations.
    /// </remarks>
    public class HtmlDocument : Document {
        /// <summary>
        /// Creates a html document from a
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// with a manually set
        /// <see cref="iText.Kernel.Geom.PageSize"/>.
        /// </summary>
        /// <param name="pdfDoc">the in-memory representation of the PDF document</param>
        /// <param name="pageSize">the page size</param>
        /// <param name="immediateFlush">
        /// if true, write pages and page-related instructions
        /// to the
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// as soon as possible.
        /// </param>
        public HtmlDocument(PdfDocument pdfDoc, PageSize pageSize, bool immediateFlush)
            : base(pdfDoc, pageSize, immediateFlush) {
        }

        /// <summary><inheritDoc/></summary>
        public override void Relayout() {
            if (rootRenderer is HtmlDocumentRenderer) {
                ((HtmlDocumentRenderer)rootRenderer).RemoveEventHandlers();
                base.Relayout();
                ((HtmlDocumentRenderer)rootRenderer).ProcessWaitingElement();
            }
        }
    }
}
