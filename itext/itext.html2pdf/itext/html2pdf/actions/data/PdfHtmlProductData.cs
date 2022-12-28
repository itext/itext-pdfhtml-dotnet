/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 iText Group NV
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
using System;
using iText.Commons.Actions;
using iText.Commons.Actions.Data;

namespace iText.Html2pdf.Actions.Data {
    /// <summary>
    /// Stores an instance of
    /// <see cref="iText.Commons.Actions.Data.ProductData"/>
    /// related to iText pdfHTML module.
    /// </summary>
    public sealed class PdfHtmlProductData {
        private const String PDF_HTML_PUBLIC_PRODUCT_NAME = "pdfHTML";

        private const String PDF_HTML_VERSION = "4.0.5-SNAPSHOT";

        private const int PDF_HTML_COPYRIGHT_SINCE = 2000;

        private const int PDF_HTML_COPYRIGHT_TO = 2023;

        private static readonly ProductData PDF_HTML_PRODUCT_DATA = new ProductData(PDF_HTML_PUBLIC_PRODUCT_NAME, 
            ProductNameConstant.PDF_HTML, PDF_HTML_VERSION, PDF_HTML_COPYRIGHT_SINCE, PDF_HTML_COPYRIGHT_TO);

        private PdfHtmlProductData() {
        }

        // Empty constructor.
        /// <summary>
        /// Getter for an instance of
        /// <see cref="iText.Commons.Actions.Data.ProductData"/>
        /// related to iText pdfHTML module.
        /// </summary>
        /// <returns>iText pdfHTML product description</returns>
        public static ProductData GetInstance() {
            return PDF_HTML_PRODUCT_DATA;
        }
    }
}
