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
using System.Collections.Generic;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach {
    /// <summary>
    /// Interface for classes that can process HTML to PDF in the form of a
    /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
    /// or a list of
    /// <see cref="iText.Layout.Element.IElement"/>
    /// objects.
    /// </summary>
    public interface IHtmlProcessor {
        /// <summary>
        /// Parses HTML to add the content to a
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>.
        /// </summary>
        /// <param name="root">the root node of the HTML that needs to be parsed</param>
        /// <param name="pdfDocument">
        /// the
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// instance
        /// </param>
        /// <returns>
        /// a
        /// <see cref="iText.Layout.Document"/>
        /// instance
        /// </returns>
        Document ProcessDocument(INode root, PdfDocument pdfDocument);

        /// <summary>
        /// Parses HTML to add the content to a list of
        /// <see cref="iText.Layout.Element.IElement"/>
        /// objects.
        /// </summary>
        /// <param name="root">the root node of the HTML that needs to be parsed</param>
        /// <returns>the resulting list</returns>
        IList<IElement> ProcessElements(INode root);
    }
}
