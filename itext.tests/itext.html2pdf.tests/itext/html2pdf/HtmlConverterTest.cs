/*
This file is part of the iText (R) project.
Copyright (c) 1998-2022 iText Group NV
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
using System.IO;
using iText.Html2pdf.Exceptions;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Test;

namespace iText.Html2pdf {
    [NUnit.Framework.Category("IntegrationTest")]
    public class HtmlConverterTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void CannotConvertHtmlToDocumentInReadingModeTest() {
            NUnit.Framework.Assert.That(() =>  {
                PdfDocument pdfDocument = CreateTempDoc();
                ConverterProperties properties = new ConverterProperties();
                Document document = HtmlConverter.ConvertToDocument("", pdfDocument, properties);
            }
            , NUnit.Framework.Throws.InstanceOf<Html2PdfException>().With.Message.EqualTo(Html2PdfException.PDF_DOCUMENT_SHOULD_BE_IN_WRITING_MODE))
;
        }

        private static PdfDocument CreateTempDoc() {
            MemoryStream outputStream = new MemoryStream();
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outputStream));
            pdfDocument.AddNewPage();
            pdfDocument.Close();
            pdfDocument = new PdfDocument(new PdfReader(new MemoryStream(outputStream.ToArray())));
            return pdfDocument;
        }
    }
}
