/*
This file is part of the iText (R) project.
Copyright (c) 1998-2022 iText Group NV
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
using System.Collections.Generic;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Utils;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Test;

namespace iText.Html2pdf.Events {
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
            IList<IElement> elements = HtmlConverter.ConvertToElements("<input type='text' name='h' value='test header field value here'/>"
                , converterProperties);
            IList<IElement> footer = HtmlConverter.ConvertToElements("<input type='text' name='h' value='test footer field value here'/>"
                , converterProperties);
            PdfWriter writer = new PdfWriter(pdfStream);
            PdfDocument pdfDocument = new PdfDocument(writer);
            iText.Kernel.Events.IEventHandler handler = new _IEventHandler_116(elements, footer);
            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, handler);
            pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, handler);
            return pdfDocument;
        }

        private sealed class _IEventHandler_116 : iText.Kernel.Events.IEventHandler {
            public _IEventHandler_116(IList<IElement> elements, IList<IElement> footer) {
                this.elements = elements;
                this.footer = footer;
            }

            public void HandleEvent(Event @event) {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
                PdfPage page = docEvent.GetPage();
                PdfCanvas pdfCanvas = new PdfCanvas(page);
                Rectangle pageSize = page.GetPageSize();
                iText.Layout.Canvas canvas = new iText.Layout.Canvas(pdfCanvas, pageSize);
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

            private readonly IList<IElement> elements;

            private readonly IList<IElement> footer;
        }
    }
}
