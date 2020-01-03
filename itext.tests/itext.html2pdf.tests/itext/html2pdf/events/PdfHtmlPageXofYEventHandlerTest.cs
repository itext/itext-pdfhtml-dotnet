/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
Authors: iText Software.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
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
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(src).AbsolutePath + "\n");
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
        protected internal class PageXofY : IEventHandler {
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
                iText.Layout.Canvas canvas = new iText.Layout.Canvas(pdfCanvas, pdf, pageSize);
                Paragraph p = new Paragraph().Add("Page ").Add(pageNumber.ToString()).Add(" of");
                canvas.ShowTextAligned(p, this.x, this.y, TextAlignment.RIGHT);
                pdfCanvas.AddXObject(this.placeholder, this.x + this.space, this.y - this.descent);
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
