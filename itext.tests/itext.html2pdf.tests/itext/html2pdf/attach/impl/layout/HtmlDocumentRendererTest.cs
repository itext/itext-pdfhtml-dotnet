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
using iText.Html2pdf;
using iText.IO.Source;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Test;

namespace iText.Html2pdf.Attach.Impl.Layout {
    [NUnit.Framework.Category("UnitTest")]
    public class HtmlDocumentRendererTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void ShouldAttemptTrimLastPageTest() {
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(new ByteArrayOutputStream()));
            Document document = new Document(pdfDocument);
            HtmlDocumentRenderer documentRenderer = new HtmlDocumentRenderer(document, false);
            document.SetRenderer(documentRenderer);
            pdfDocument.AddNewPage();
            NUnit.Framework.Assert.AreEqual(1, pdfDocument.GetNumberOfPages());
            // For one-page documents it does not make sense to attempt to trim last page
            NUnit.Framework.Assert.IsFalse(documentRenderer.ShouldAttemptTrimLastPage());
            pdfDocument.AddNewPage();
            NUnit.Framework.Assert.AreEqual(2, pdfDocument.GetNumberOfPages());
            // If there are more than one page, we try to trim last page
            NUnit.Framework.Assert.IsTrue(documentRenderer.ShouldAttemptTrimLastPage());
        }

        [NUnit.Framework.Test]
        public virtual void TrimLastPageIfNecessaryTest() {
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(new ByteArrayOutputStream()));
            Document document = new Document(pdfDocument);
            HtmlDocumentRenderer documentRenderer = new HtmlDocumentRenderer(document, false);
            document.SetRenderer(documentRenderer);
            pdfDocument.AddNewPage();
            pdfDocument.AddNewPage();
            new PdfCanvas(pdfDocument.GetLastPage()).MoveTo(10, 10).LineTo(20, 20).Stroke();
            pdfDocument.AddNewPage();
            NUnit.Framework.Assert.AreEqual(3, pdfDocument.GetNumberOfPages());
            documentRenderer.TrimLastPageIfNecessary();
            NUnit.Framework.Assert.AreEqual(2, pdfDocument.GetNumberOfPages());
            documentRenderer.TrimLastPageIfNecessary();
            NUnit.Framework.Assert.AreEqual(2, pdfDocument.GetNumberOfPages());
        }

        [NUnit.Framework.Test]
        public virtual void EstimatedNumberOfPagesInNextRendererEmptyDocumentTest() {
            Document document = HtmlConverter.ConvertToDocument("<html></html>", new PdfWriter(new ByteArrayOutputStream
                ()));
            HtmlDocumentRenderer documentRenderer = (HtmlDocumentRenderer)document.GetRenderer();
            HtmlDocumentRenderer nextRenderer = (HtmlDocumentRenderer)documentRenderer.GetNextRenderer();
            NUnit.Framework.Assert.AreEqual(0, nextRenderer.GetEstimatedNumberOfPages());
        }

        [NUnit.Framework.Test]
        public virtual void EstimatedNumberOfPagesInNextRendererDocumentWithTextChunkTest() {
            Document document = HtmlConverter.ConvertToDocument("<html>text</html>", new PdfWriter(new ByteArrayOutputStream
                ()));
            HtmlDocumentRenderer documentRenderer = (HtmlDocumentRenderer)document.GetRenderer();
            HtmlDocumentRenderer nextRenderer = (HtmlDocumentRenderer)documentRenderer.GetNextRenderer();
            NUnit.Framework.Assert.AreEqual(1, nextRenderer.GetEstimatedNumberOfPages());
        }
    }
}
