/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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
using iText.Html2pdf;
using iText.Html2pdf.Attach.Impl;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach {
    /// <summary>
    /// Helper class to add parsed HTML content to an existing iText document,
    /// or to parse HTML to a list of iText elements.
    /// </summary>
    public class Attacher {
        /// <summary>
        /// Instantiates a new
        /// <see cref="Attacher"/>
        /// instance.
        /// </summary>
        private Attacher() {
        }

        /// <summary>
        /// Attaches the HTML content stored in a document node to
        /// an existing PDF document, using specific converter properties,
        /// and returning an iText
        /// <see cref="iText.Layout.Document"/>
        /// object.
        /// </summary>
        /// <param name="documentNode">the document node with the HTML</param>
        /// <param name="pdfDocument">
        /// the
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// instance
        /// </param>
        /// <param name="converterProperties">
        /// the
        /// <see cref="iText.Html2pdf.ConverterProperties"/>
        /// instance
        /// </param>
        /// <returns>
        /// an iText
        /// <see cref="iText.Layout.Document"/>
        /// object
        /// </returns>
        public static Document Attach(IDocumentNode documentNode, PdfDocument pdfDocument, ConverterProperties converterProperties
            ) {
            IHtmlProcessor processor = new DefaultHtmlProcessor(converterProperties);
            return processor.ProcessDocument(documentNode, pdfDocument);
        }

        /// <summary>
        /// Attaches the HTML content stored in a document node to
        /// a list of
        /// <see cref="iText.Layout.Element.IElement"/>
        /// objects.
        /// </summary>
        /// <param name="documentNode">the document node with the HTML</param>
        /// <param name="converterProperties">
        /// the
        /// <see cref="iText.Html2pdf.ConverterProperties"/>
        /// instance
        /// </param>
        /// <returns>
        /// the list of
        /// <see cref="iText.Layout.Element.IElement"/>
        /// objects
        /// </returns>
        public static IList<IElement> Attach(IDocumentNode documentNode, ConverterProperties converterProperties) {
            IHtmlProcessor processor = new DefaultHtmlProcessor(converterProperties);
            return processor.ProcessElements(documentNode);
        }
    }
}
