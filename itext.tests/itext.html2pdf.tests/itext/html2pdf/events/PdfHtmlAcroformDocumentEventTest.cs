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
using System.Collections.Generic;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Event;
using iText.Kernel.Utils;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Test;

namespace iText.Html2pdf.Events {
    [NUnit.Framework.Category("IntegrationTest")]
    public class PdfHtmlAcroformDocumentEventTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/events/PdfHtmlAcroformDocumentEventTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/events/PdfHtmlAcroformDocumentEventTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void InitDestinationFolder() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void EndPageEventWithFieldTest() {
            String name = "endPageEventWithFieldTest";
            String pdfOutput = destinationFolder + name + ".pdf";
            String pdfComparison = sourceFolder + "cmp_" + name + ".pdf";
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetCreateAcroForm(true);
            PdfDocument pdfDocument = AddEventHandlersToPdfDocument(pdfOutput, converterProperties);
            String htmlString = "<html><head><title>pdfHtml header and footer example</title></head><body><span>test</span>"
                 + "<div style='page-break-before:always;'></div><span>test</span></body></html>";
            HtmlConverter.ConvertToPdf(htmlString, pdfDocument, new ConverterProperties());
            pdfDocument.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(pdfOutput, pdfComparison, destinationFolder
                ));
        }

        private PdfDocument AddEventHandlersToPdfDocument(String pdfOutput, ConverterProperties converterProperties
            ) {
            FileStream pdfStream = new FileStream(pdfOutput, FileMode.Create);
            IList<IElement> elements = HtmlConverter.ConvertToElements("<input type='text' name='header' value='test header field value here'/>"
                , converterProperties);
            IList<IElement> footer = HtmlConverter.ConvertToElements("<input type='text' name='footer' value='test footer field value here'/>"
                , converterProperties);
            PdfWriter writer = new PdfWriter(pdfStream);
            PdfDocument pdfDocument = new PdfDocument(writer);
            AbstractPdfDocumentEventHandler handler = new _AbstractPdfDocumentEventHandler_94(elements, footer);
            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, handler);
            pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, handler);
            return pdfDocument;
        }

        private sealed class _AbstractPdfDocumentEventHandler_94 : AbstractPdfDocumentEventHandler {
            public _AbstractPdfDocumentEventHandler_94(IList<IElement> elements, IList<IElement> footer) {
                this.elements = elements;
                this.footer = footer;
            }

            protected override void OnAcceptedEvent(AbstractPdfDocumentEvent @event) {
                if (!(@event is PdfDocumentEvent)) {
                    return;
                }
                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
                PdfPage page = docEvent.GetPage();
                PdfCanvas pdfCanvas = new PdfCanvas(page);
                Rectangle pageSize = page.GetPageSize();
                using (iText.Layout.Canvas canvas = new iText.Layout.Canvas(pdfCanvas, pageSize)) {
                    Paragraph headerP = new Paragraph();
                    foreach (IElement elem in elements) {
                        if (elem is IBlockElement) {
                            headerP.Add((IBlockElement)elem);
                        }
                        else {
                            if (elem is Image) {
                                headerP.Add((Image)elem);
                            }
                        }
                    }
                    Paragraph footerP = new Paragraph();
                    foreach (IElement elem in footer) {
                        if (elem is IBlockElement) {
                            footerP.Add((IBlockElement)elem);
                        }
                        else {
                            if (elem is Image) {
                                footerP.Add((Image)elem);
                            }
                        }
                    }
                    canvas.ShowTextAligned(headerP, pageSize.GetWidth() / 2, pageSize.GetTop() - 30, TextAlignment.LEFT);
                    canvas.ShowTextAligned(footerP, 0, 0, TextAlignment.LEFT);
                }
            }

            private readonly IList<IElement> elements;

            private readonly IList<IElement> footer;
        }
    }
}
