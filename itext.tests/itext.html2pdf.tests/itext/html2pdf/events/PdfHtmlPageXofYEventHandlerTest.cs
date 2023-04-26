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
using System;
using System.IO;
using iText.Html2pdf;
using iText.IO.Util;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Test;

namespace iText.Html2pdf.Events {
    [NUnit.Framework.Category("IntegrationTest")]
    public class PdfHtmlPageXofYEventHandlerTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/events/PdfHtmlPageXofYEventHandlerTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/events/PdfHtmlPageXofYEventHandlerTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void PageXofYHtmlTest() {
            String filename = "pageXofY";
            String src = sourceFolder + filename + ".html";
            String dest = destinationFolder + filename + ".pdf";
            String cmp = sourceFolder + "cmp_" + filename + ".pdf";
            new PdfHtmlPageXofYEventHandlerTest().ParseWithFooter(src, dest, sourceFolder);
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(src) + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(dest, cmp, destinationFolder, "diff_XofY_"
                ));
        }

        public virtual void ParseWithFooter(String htmlSource, String pdfDest, String resoureLoc) {
            //Create Document
            PdfWriter writer = new PdfWriter(pdfDest);
            PdfDocument pdfDocument = new PdfDocument(writer);
            //Create event-handlers
            PdfHtmlPageXofYEventHandlerTest.PageXofY footerHandler = new PdfHtmlPageXofYEventHandlerTest.PageXofY(this
                , pdfDocument);
            //Assign event-handlers
            pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, footerHandler);
            //Convert
            ConverterProperties converterProperties = new ConverterProperties().SetBaseUri(resoureLoc);
            Document doc = HtmlConverter.ConvertToDocument(new FileStream(new FileInfo(htmlSource).FullName, FileMode.Open
                , FileAccess.Read), pdfDocument, converterProperties);
            //Write the total number of pages to the placeholder
            doc.Flush();
            footerHandler.WriteTotal(pdfDocument);
            doc.Close();
        }

        //page X of Y
        protected internal class PageXofY : iText.Kernel.Events.IEventHandler {
            protected internal PdfFormXObject placeholder;

            protected internal float side = 20;

            protected internal float x = 300;

            protected internal float y = 25;

            protected internal float space = 4.5f;

            protected internal float descent = 3;

            public PageXofY(PdfHtmlPageXofYEventHandlerTest _enclosing, PdfDocument pdf) {
                this._enclosing = _enclosing;
                this.placeholder = new PdfFormXObject(new Rectangle(0, 0, this.side, this.side));
            }

            public virtual void HandleEvent(Event @event) {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
                PdfDocument pdf = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();
                int pageNumber = pdf.GetPageNumber(page);
                Rectangle pageSize = page.GetPageSize();
                PdfCanvas pdfCanvas = new PdfCanvas(page.GetLastContentStream(), page.GetResources(), pdf);
                iText.Layout.Canvas canvas = new iText.Layout.Canvas(pdfCanvas, pageSize);
                Paragraph p = new Paragraph().Add("Page ").Add(pageNumber.ToString()).Add(" of");
                canvas.ShowTextAligned(p, this.x, this.y, TextAlignment.RIGHT);
                pdfCanvas.AddXObjectAt(this.placeholder, this.x + this.space, this.y - this.descent);
                canvas.Close();
            }

            public virtual void WriteTotal(PdfDocument pdf) {
                iText.Layout.Canvas canvas = new iText.Layout.Canvas(this.placeholder, pdf);
                canvas.ShowTextAligned(pdf.GetNumberOfPages().ToString(), 0, this.descent, TextAlignment.LEFT);
            }

            private readonly PdfHtmlPageXofYEventHandlerTest _enclosing;
        }
    }
}
