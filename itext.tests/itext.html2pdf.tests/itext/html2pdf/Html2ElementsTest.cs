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
using iText.Commons.Actions;
using iText.Commons.Actions.Sequence;
using iText.Html2pdf.Actions.Events;
using iText.Html2pdf.Attach.Impl;
using iText.Html2pdf.Logs;
using iText.IO.Source;
using iText.IO.Util;
using iText.Kernel.Exceptions;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf {
    [NUnit.Framework.Category("IntegrationTest")]
    public class Html2ElementsTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/Html2ElementsTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/Html2ElementsTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HtmlToElementsTest01() {
            String html = "<p>Hello world!</p>";
            IList<IElement> lst = HtmlConverter.ConvertToElements(html);
            NUnit.Framework.Assert.IsTrue(lst.Count == 1);
            NUnit.Framework.Assert.IsTrue(lst[0] is Paragraph);
            Paragraph p = (Paragraph)lst[0];
            NUnit.Framework.Assert.AreEqual("Hello world!", ((Text)p.GetChildren()[0]).GetText());
            NUnit.Framework.Assert.AreEqual(12f, p.GetProperty<UnitValue>(Property.FONT_SIZE).GetValue(), 1e-10);
        }

        [NUnit.Framework.Test]
        public virtual void HtmlToElementsTest02() {
            String html = "<table style=\"font-size: 2em\"><tr><td>123</td><td><456></td></tr><tr><td>Long cell</td></tr></table>";
            IList<IElement> lst = HtmlConverter.ConvertToElements(html);
            NUnit.Framework.Assert.IsTrue(lst.Count == 1);
            NUnit.Framework.Assert.IsTrue(lst[0] is Table);
            Table t = (Table)lst[0];
            NUnit.Framework.Assert.AreEqual(2, t.GetNumberOfRows());
            NUnit.Framework.Assert.AreEqual("123", ((Text)(((Paragraph)t.GetCell(0, 0).GetChildren()[0]).GetChildren()
                [0])).GetText());
            NUnit.Framework.Assert.AreEqual(24f, t.GetProperty<UnitValue>(Property.FONT_SIZE).GetValue(), 1e-10);
        }

        [NUnit.Framework.Test]
        public virtual void HtmlToElementsTest03() {
            String html = "<p>Hello world!</p><table><tr><td>123</td><td><456></td></tr><tr><td>Long cell</td></tr></table><p>Hello world!</p>";
            IList<IElement> lst = HtmlConverter.ConvertToElements(html);
            NUnit.Framework.Assert.IsTrue(lst.Count == 3);
            NUnit.Framework.Assert.IsTrue(lst[0] is Paragraph);
            NUnit.Framework.Assert.IsTrue(lst[1] is Table);
            NUnit.Framework.Assert.IsTrue(lst[2] is Paragraph);
            NUnit.Framework.Assert.AreEqual("Hello world!", ((Text)((Paragraph)lst[0]).GetChildren()[0]).GetText());
            NUnit.Framework.Assert.AreEqual("123", ((Text)(((Paragraph)((Table)lst[1]).GetCell(0, 0).GetChildren()[0])
                .GetChildren()[0])).GetText());
        }

        [NUnit.Framework.Test]
        public virtual void HtmlToElementsTest04() {
            // Handles malformed html
            String html = "<p>Hello world!<table><td>123";
            IList<IElement> lst = HtmlConverter.ConvertToElements(html);
            NUnit.Framework.Assert.IsTrue(lst.Count == 2);
            NUnit.Framework.Assert.IsTrue(lst[0] is Paragraph);
            NUnit.Framework.Assert.IsTrue(lst[1] is Table);
            NUnit.Framework.Assert.AreEqual("Hello world!", ((Text)((Paragraph)lst[0]).GetChildren()[0]).GetText());
            NUnit.Framework.Assert.AreEqual("123", ((Text)(((Paragraph)((Table)lst[1]).GetCell(0, 0).GetChildren()[0])
                .GetChildren()[0])).GetText());
        }

        [NUnit.Framework.Test]
        public virtual void HtmlToElementsTest05() {
            String html = "123";
            IList<IElement> lst = HtmlConverter.ConvertToElements(html);
            NUnit.Framework.Assert.IsTrue(lst.Count == 1);
        }

        [NUnit.Framework.Test]
        public virtual void HtmlElementsTest06() {
            String html = "<html>Lorem<p>Ipsum</p>Dolor<p>Sit</p></html>";
            IList<IElement> lst = HtmlConverter.ConvertToElements(html);
            NUnit.Framework.Assert.IsTrue(lst.Count == 4);
            for (int i = 0; i < lst.Count; i++) {
                NUnit.Framework.Assert.IsTrue(lst[i] is Paragraph);
            }
        }

        [NUnit.Framework.Test]
        public virtual void HtmlElementsTest07() {
            String html = "<html>Lorem<span>Dolor</span><p>Ipsum</p><p>Sit</p></html>";
            IList<IElement> lst = HtmlConverter.ConvertToElements(html);
            NUnit.Framework.Assert.IsTrue(lst.Count == 3);
            for (int i = 0; i < lst.Count; i++) {
                NUnit.Framework.Assert.IsTrue(lst[i] is Paragraph);
            }
        }

        [NUnit.Framework.Test]
        public virtual void HtmlToElementsTest08() {
            // this test checks whether iText fails to process meta tag inside body section or not
            String html = "<html><p>Hello world!</p><meta name=\"author\" content=\"Bruno\"><table><tr><td>123</td><td><456></td></tr><tr><td>Long cell</td></tr></table><p>Hello world!</p></html>";
            HtmlConverter.ConvertToElements(html);
        }

        [NUnit.Framework.Test]
        public virtual void HtmlToElementsTest09() {
            //Test OutlineHandler exception throwing
            /*
            Outlines require a PdfDocument, and OutlineHandler is based around its availability
            Any convert to elements workflow of course doesn't have a PdfDocument.
            Instead of throwing an NPE when trying it, the OutlineHandler will check for the existence of a pdfDocument
            If no PdfDocument is found, the handler will do nothing silently
            */
            String html = "<html><p>Hello world!</p><meta name=\"author\" content=\"Bruno\"><table><tr><td>123</td><td><456></td></tr><tr><td>Long cell</td></tr></table><p>Hello world!</p></html>";
            ConverterProperties props = new ConverterProperties();
            OutlineHandler outlineHandler = new OutlineHandler();
            outlineHandler.PutTagPriorityMapping("h1", 1);
            outlineHandler.PutTagPriorityMapping("h3", 2);
            outlineHandler.PutTagPriorityMapping("p", 3);
            props.SetOutlineHandler(outlineHandler);
            HtmlConverter.ConvertToElements(html);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.UNABLE_TO_RETRIEVE_STREAM_WITH_GIVEN_BASE_URI
            , Count = 1)]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Count = 1)]
        [LogMessage(Html2PdfLogMessageConstant.PDF_DOCUMENT_NOT_PRESENT, Count = 1)]
        public virtual void HtmlObjectMalformedUrlTest() {
            String html = "<object data ='htt://as' type='image/svg+xml'></object>";
            IList<IElement> lst = HtmlConverter.ConvertToElements(html);
            NUnit.Framework.Assert.IsTrue(lst.Count == 0);
        }

        [NUnit.Framework.Test]
        public virtual void HtmlToElementsVsHtmlToPdfTest() {
            String src = sourceFolder + "basic.html";
            String outConvertToPdf = destinationFolder + "basicCovertToPdfResult.pdf";
            String outConvertToElements = destinationFolder + "basicCovertToElementsResult.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(src), new FileInfo(outConvertToPdf));
            IList<IElement> elements = HtmlConverter.ConvertToElements(new FileStream(src, FileMode.Open, FileAccess.Read
                ));
            Document document = new Document(new PdfDocument(new PdfWriter(outConvertToElements)));
            // In order to collapse margins between the direct children of root element
            // it's required to manually enable collapsing on root element. This is because siblings
            // margins collapsing is controlled by the parent element.
            // This leads to the difference between pure convertToPdf/Document and convertToElements methods.
            document.SetProperty(Property.COLLAPSING_MARGINS, true);
            foreach (IElement elem in elements) {
                if (elem is IBlockElement) {
                    document.Add((IBlockElement)elem);
                }
                else {
                    if (elem is Image) {
                        document.Add((Image)elem);
                    }
                    else {
                        if (elem is AreaBreak) {
                            document.Add((AreaBreak)elem);
                        }
                        else {
                            NUnit.Framework.Assert.Fail("The #convertToElements method gave element which is unsupported as root element, it's unexpected."
                                );
                        }
                    }
                }
            }
            document.Close();
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(src) + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outConvertToElements, outConvertToPdf, destinationFolder
                ));
        }

        [NUnit.Framework.Test]
        public virtual void BodyFontFamilyTest() {
            String html = "<!DOCTYPE html>\n" + "<html>\n" + "<body style=\"font-family: monospace\">\n" + "This text is directly in body and should be monospaced.\n"
                 + "<p>This text is in paragraph and should be monospaced.</p>\n" + "</body>\n" + "</html>";
            IList<IElement> elements = HtmlConverter.ConvertToElements(html);
            NUnit.Framework.Assert.AreEqual(2, elements.Count);
            IElement anonymousParagraph = elements[0];
            NUnit.Framework.Assert.AreEqual(new String[] { "monospace" }, anonymousParagraph.GetProperty<String[]>(Property
                .FONT));
            IElement normalParagraph = elements[1];
            NUnit.Framework.Assert.AreEqual(new String[] { "monospace" }, normalParagraph.GetProperty<String[]>(Property
                .FONT));
        }

        [NUnit.Framework.Test]
        public virtual void LeadingInDefaultRenderingModeTest() {
            String html = "This text is directly in body. It should have the same default LEADING property as everything else.\n"
                 + "<p>This text is in paragraph.</p>";
            IList<IElement> elements = HtmlConverter.ConvertToElements(html);
            NUnit.Framework.Assert.AreEqual(2, elements.Count);
            IElement anonymousParagraph = elements[0];
            // TODO DEVSIX-3873 anonymous paragraph inherited styles should be applied in general way
            NUnit.Framework.Assert.IsNull(anonymousParagraph.GetProperty<Leading>(Property.LEADING));
            IElement normalParagraph = elements[1];
            NUnit.Framework.Assert.AreEqual(new Leading(Leading.MULTIPLIED, 1.2f), normalParagraph.GetProperty<Leading
                >(Property.LEADING));
        }

        [NUnit.Framework.Test]
        public virtual void EventGenerationTest() {
            Html2ElementsTest.StoreEventsHandler handler = new Html2ElementsTest.StoreEventsHandler();
            try {
                EventManager.GetInstance().Register(handler);
                String html = "<table><tr><td>123</td><td><456></td></tr><tr><td>789</td></tr></table><p>Hello world!</p>";
                IList<IElement> elements = HtmlConverter.ConvertToElements(html);
                NUnit.Framework.Assert.AreEqual(1, handler.GetEvents().Count);
                NUnit.Framework.Assert.IsTrue(handler.GetEvents()[0] is PdfHtmlProductEvent);
                SequenceId expectedSequenceId = ((PdfHtmlProductEvent)handler.GetEvents()[0]).GetSequenceId();
                int validationsCount = ValidateSequenceIds(expectedSequenceId, elements);
                // Table                                     1
                //      Cell -> Paragraph -> Text [123]      3
                //      Cell -> Paragraph -> Text [456]      3
                //      Cell -> Paragraph -> Text [789]      3
                // Paragraph -> Text [Hello world!]          2
                //--------------------------------------------
                //                                          12
                NUnit.Framework.Assert.AreEqual(12, validationsCount);
            }
            finally {
                EventManager.GetInstance().Unregister(handler);
            }
        }

        [NUnit.Framework.Test]
        public virtual void ConvertToElementsAndCreateTwoDocumentsTest() {
            String html = "This text is directly in body. It should have the same default LEADING property as everything else.\n"
                 + "<p>This text is in paragraph.</p>";
            IList<IElement> iElementList = HtmlConverter.ConvertToElements(html);
            using (PdfDocument pdfDocument = new PdfDocument(new PdfWriter(new ByteArrayOutputStream()))) {
                using (Document document = new Document(pdfDocument)) {
                    AddElementsToDocument(document, iElementList);
                }
            }
            PdfDocument pdfDocument_1 = new PdfDocument(new PdfWriter(new ByteArrayOutputStream()));
            Document document_1 = new Document(pdfDocument_1);
            AddElementsToDocument(document_1, iElementList);
            // TODO DEVSIX-5753 error should not be thrown here
            Exception e = NUnit.Framework.Assert.Catch(typeof(PdfException), () => document_1.Close());
            NUnit.Framework.Assert.AreEqual(KernelExceptionMessageConstant.PDF_INDIRECT_OBJECT_BELONGS_TO_OTHER_PDF_DOCUMENT
                , e.Message);
        }

        private static void AddElementsToDocument(Document document, IList<IElement> elements) {
            foreach (IElement elem in elements) {
                if (elem is IBlockElement) {
                    document.Add((IBlockElement)elem);
                }
                else {
                    if (elem is Image) {
                        document.Add((Image)elem);
                    }
                    else {
                        if (elem is AreaBreak) {
                            document.Add((AreaBreak)elem);
                        }
                        else {
                            NUnit.Framework.Assert.Fail("The #convertToElements method gave element which is unsupported as root element, it's unexpected."
                                );
                        }
                    }
                }
            }
        }

        private static int ValidateSequenceIds(SequenceId expectedSequenceId, IList<IElement> elements) {
            int validationCount = 0;
            foreach (IElement element in elements) {
                NUnit.Framework.Assert.IsTrue(element is AbstractIdentifiableElement);
                NUnit.Framework.Assert.IsTrue(element is IAbstractElement);
                NUnit.Framework.Assert.AreEqual(expectedSequenceId, SequenceIdManager.GetSequenceId((AbstractIdentifiableElement
                    )element));
                validationCount += 1;
                validationCount += ValidateSequenceIds(expectedSequenceId, ((IAbstractElement)element).GetChildren());
            }
            return validationCount;
        }

        private class StoreEventsHandler : IEventHandler {
            private IList<IEvent> events = new List<IEvent>();

            public virtual IList<IEvent> GetEvents() {
                return events;
            }

            public virtual void OnEvent(IEvent @event) {
                events.Add(@event);
            }
        }
    }
}
