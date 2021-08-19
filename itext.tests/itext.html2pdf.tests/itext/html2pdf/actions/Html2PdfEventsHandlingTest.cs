/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
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
using System.Collections.Generic;
using System.IO;
using iText.Commons.Actions;
using iText.Commons.Actions.Confirmations;
using iText.Commons.Actions.Contexts;
using iText.Commons.Actions.Processors;
using iText.Commons.Actions.Producer;
using iText.Commons.Actions.Sequence;
using iText.Commons.Utils;
using iText.Html2pdf;
using iText.Html2pdf.Actions.Events;
using iText.IO.Source;
using iText.Kernel.Counter.Event;
using iText.Kernel.Logs;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Actions {
    public class Html2PdfEventsHandlingTest : ExtendedITextTest {
        private static readonly TestConfigurationEvent CONFIGURATION_ACCESS = new TestConfigurationEvent();

        private static Html2PdfEventsHandlingTest.StoreEventsHandler handler;

        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/actions/Html2PdfEventsHandlingTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/actions/Html2PdfEventsHandlingTest/";

        [NUnit.Framework.SetUp]
        public virtual void SetUpHandler() {
            handler = new Html2PdfEventsHandlingTest.StoreEventsHandler();
            EventManager.GetInstance().Register(handler);
        }

        [NUnit.Framework.TearDown]
        public virtual void ResetHandler() {
            EventManager.GetInstance().Unregister(handler);
            handler = null;
        }

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TwoDifferentElementsToOneDocumentTest() {
            ByteArrayOutputStream baos = new ByteArrayOutputStream();
            SequenceId docSequenceId;
            using (PdfDocument pdfDocument = new PdfDocument(new PdfWriter(baos))) {
                using (Document document = new Document(pdfDocument)) {
                    docSequenceId = pdfDocument.GetDocumentIdWrapper();
                    String firstHtml = "<p>Hello world first!</p>";
                    IList<IElement> lstFirst = HtmlConverter.ConvertToElements(firstHtml);
                    AddElementsToDocument(document, lstFirst);
                    String secondHtml = "<p>Hello world second!</p>";
                    IList<IElement> lstSecond = HtmlConverter.ConvertToElements(secondHtml);
                    AddElementsToDocument(document, lstSecond);
                }
            }
            IList<AbstractProductProcessITextEvent> events = CONFIGURATION_ACCESS.GetPublicEvents(docSequenceId);
            // Confirmed 3 events, but only 2 events (1 core + 1 pdfHtml) will be reported because
            // ReportingHandler don't report similar events for one sequenceId
            NUnit.Framework.Assert.AreEqual(3, events.Count);
            NUnit.Framework.Assert.IsTrue(events[0] is ConfirmedEventWrapper);
            ConfirmedEventWrapper confirmedEventWrapper = (ConfirmedEventWrapper)events[0];
            NUnit.Framework.Assert.IsTrue(confirmedEventWrapper.GetEvent() is ITextCoreEvent);
            NUnit.Framework.Assert.AreEqual(ITextCoreEvent.PROCESS_PDF, confirmedEventWrapper.GetEvent().GetEventType(
                ));
            NUnit.Framework.Assert.IsTrue(events[1] is ConfirmedEventWrapper);
            confirmedEventWrapper = (ConfirmedEventWrapper)events[1];
            NUnit.Framework.Assert.IsTrue(confirmedEventWrapper.GetEvent() is PdfHtmlProductEvent);
            NUnit.Framework.Assert.AreEqual(PdfHtmlProductEvent.CONVERT_HTML, confirmedEventWrapper.GetEvent().GetEventType
                ());
            NUnit.Framework.Assert.IsTrue(events[2] is ConfirmedEventWrapper);
            confirmedEventWrapper = (ConfirmedEventWrapper)events[2];
            NUnit.Framework.Assert.IsTrue(confirmedEventWrapper.GetEvent() is PdfHtmlProductEvent);
            NUnit.Framework.Assert.AreEqual(PdfHtmlProductEvent.CONVERT_HTML, confirmedEventWrapper.GetEvent().GetEventType
                ());
            using (PdfDocument pdfDocument_1 = new PdfDocument(new PdfReader(new MemoryStream(baos.ToArray())))) {
                String expectedProdLine = CreateExpectedProducerLine(new ConfirmedEventWrapper[] { GetCoreEvent(), GetPdfHtmlEvent
                    () });
                NUnit.Framework.Assert.AreEqual(expectedProdLine, pdfDocument_1.GetDocumentInfo().GetProducer());
            }
        }

        [NUnit.Framework.Test]
        public virtual void SetOfElementsToOneDocumentTest() {
            ByteArrayOutputStream baos = new ByteArrayOutputStream();
            SequenceId docSequenceId;
            using (PdfDocument pdfDocument = new PdfDocument(new PdfWriter(baos))) {
                using (Document document = new Document(pdfDocument)) {
                    docSequenceId = pdfDocument.GetDocumentIdWrapper();
                    String html = "<p>Hello world first!</p><span>Some text</span><p>Some second text</p>";
                    IList<IElement> lstFirst = HtmlConverter.ConvertToElements(html);
                    NUnit.Framework.Assert.AreEqual(3, lstFirst.Count);
                    AddElementsToDocument(document, lstFirst);
                }
            }
            IList<AbstractProductProcessITextEvent> events = CONFIGURATION_ACCESS.GetPublicEvents(docSequenceId);
            NUnit.Framework.Assert.AreEqual(2, events.Count);
            NUnit.Framework.Assert.IsTrue(events[0] is ConfirmedEventWrapper);
            ConfirmedEventWrapper confirmedEventWrapper = (ConfirmedEventWrapper)events[0];
            NUnit.Framework.Assert.IsTrue(confirmedEventWrapper.GetEvent() is ITextCoreEvent);
            NUnit.Framework.Assert.AreEqual(ITextCoreEvent.PROCESS_PDF, confirmedEventWrapper.GetEvent().GetEventType(
                ));
            NUnit.Framework.Assert.IsTrue(events[1] is ConfirmedEventWrapper);
            confirmedEventWrapper = (ConfirmedEventWrapper)events[1];
            NUnit.Framework.Assert.IsTrue(confirmedEventWrapper.GetEvent() is PdfHtmlProductEvent);
            NUnit.Framework.Assert.AreEqual(PdfHtmlProductEvent.CONVERT_HTML, confirmedEventWrapper.GetEvent().GetEventType
                ());
            using (PdfDocument pdfDocument_1 = new PdfDocument(new PdfReader(new MemoryStream(baos.ToArray())))) {
                String expectedProdLine = CreateExpectedProducerLine(new ConfirmedEventWrapper[] { GetCoreEvent(), GetPdfHtmlEvent
                    () });
                NUnit.Framework.Assert.AreEqual(expectedProdLine, pdfDocument_1.GetDocumentInfo().GetProducer());
            }
        }

        [NUnit.Framework.Test]
        public virtual void ConvertHtmlToDocumentTest() {
            String outFileName = "helloWorld_to_doc.pdf";
            SequenceId docId;
            using (Document document = HtmlConverter.ConvertToDocument(SOURCE_FOLDER + "helloWorld.html", new PdfWriter
                (DESTINATION_FOLDER + outFileName))) {
                docId = document.GetPdfDocument().GetDocumentIdWrapper();
            }
            IList<AbstractProductProcessITextEvent> events = CONFIGURATION_ACCESS.GetPublicEvents(docId);
            NUnit.Framework.Assert.AreEqual(2, events.Count);
            NUnit.Framework.Assert.IsTrue(events[0] is ConfirmedEventWrapper);
            ConfirmedEventWrapper confirmedEventWrapper = (ConfirmedEventWrapper)events[0];
            NUnit.Framework.Assert.IsTrue(confirmedEventWrapper.GetEvent() is ITextCoreEvent);
            NUnit.Framework.Assert.AreEqual(ITextCoreEvent.PROCESS_PDF, confirmedEventWrapper.GetEvent().GetEventType(
                ));
            NUnit.Framework.Assert.IsTrue(events[1] is ConfirmedEventWrapper);
            confirmedEventWrapper = (ConfirmedEventWrapper)events[1];
            NUnit.Framework.Assert.IsTrue(confirmedEventWrapper.GetEvent() is PdfHtmlProductEvent);
            NUnit.Framework.Assert.AreEqual(PdfHtmlProductEvent.CONVERT_HTML, confirmedEventWrapper.GetEvent().GetEventType
                ());
            using (PdfDocument pdfDocument = new PdfDocument(new PdfReader(DESTINATION_FOLDER + outFileName))) {
                String expectedProdLine = CreateExpectedProducerLine(new ConfirmedEventWrapper[] { GetCoreEvent(), GetPdfHtmlEvent
                    () });
                NUnit.Framework.Assert.AreEqual(expectedProdLine, pdfDocument.GetDocumentInfo().GetProducer());
            }
        }

        [NUnit.Framework.Test]
        public virtual void ConvertHtmlToDocAndAddElementsToDocTest() {
            String outFileName = "helloWorld_to_doc_add_elem.pdf";
            SequenceId docId;
            using (Document document = HtmlConverter.ConvertToDocument(SOURCE_FOLDER + "helloWorld.html", new PdfWriter
                (DESTINATION_FOLDER + outFileName))) {
                docId = document.GetPdfDocument().GetDocumentIdWrapper();
                String html = "<p>Hello world first!</p><span>Some text</span><p>Some second text</p>";
                IList<IElement> lstFirst = HtmlConverter.ConvertToElements(html);
                NUnit.Framework.Assert.AreEqual(3, lstFirst.Count);
                AddElementsToDocument(document, lstFirst);
            }
            IList<AbstractProductProcessITextEvent> events = CONFIGURATION_ACCESS.GetPublicEvents(docId);
            // Confirmed 3 events, but only 2 events (1 core + 1 pdfHtml) will be reported because
            // ReportingHandler don't report similar events for one sequenceId
            NUnit.Framework.Assert.AreEqual(3, events.Count);
            NUnit.Framework.Assert.IsTrue(events[0] is ConfirmedEventWrapper);
            ConfirmedEventWrapper confirmedEventWrapper = (ConfirmedEventWrapper)events[0];
            NUnit.Framework.Assert.IsTrue(confirmedEventWrapper.GetEvent() is ITextCoreEvent);
            NUnit.Framework.Assert.AreEqual(ITextCoreEvent.PROCESS_PDF, confirmedEventWrapper.GetEvent().GetEventType(
                ));
            NUnit.Framework.Assert.IsTrue(events[1] is ConfirmedEventWrapper);
            confirmedEventWrapper = (ConfirmedEventWrapper)events[1];
            NUnit.Framework.Assert.IsTrue(confirmedEventWrapper.GetEvent() is PdfHtmlProductEvent);
            NUnit.Framework.Assert.AreEqual(PdfHtmlProductEvent.CONVERT_HTML, confirmedEventWrapper.GetEvent().GetEventType
                ());
            NUnit.Framework.Assert.IsTrue(events[2] is ConfirmedEventWrapper);
            confirmedEventWrapper = (ConfirmedEventWrapper)events[2];
            NUnit.Framework.Assert.IsTrue(confirmedEventWrapper.GetEvent() is PdfHtmlProductEvent);
            NUnit.Framework.Assert.AreEqual(PdfHtmlProductEvent.CONVERT_HTML, confirmedEventWrapper.GetEvent().GetEventType
                ());
            using (PdfDocument pdfDocument = new PdfDocument(new PdfReader(DESTINATION_FOLDER + outFileName))) {
                String expectedProdLine = CreateExpectedProducerLine(new ConfirmedEventWrapper[] { GetCoreEvent(), GetPdfHtmlEvent
                    () });
                NUnit.Framework.Assert.AreEqual(expectedProdLine, pdfDocument.GetDocumentInfo().GetProducer());
            }
        }

        [NUnit.Framework.Test]
        public virtual void ConvertHtmlToPdfTest() {
            String outFileName = "helloWorld_to_pdf.pdf";
            HtmlConverter.ConvertToPdf(SOURCE_FOLDER + "helloWorld.html", new PdfWriter(DESTINATION_FOLDER + outFileName
                ));
            IList<ConfirmEvent> events = handler.GetEvents();
            NUnit.Framework.Assert.AreEqual(1, events.Count);
            AbstractProductProcessITextEvent @event = events[0].GetConfirmedEvent();
            NUnit.Framework.Assert.AreEqual(PdfHtmlProductEvent.CONVERT_HTML, @event.GetEventType());
            using (PdfDocument pdfDocument = new PdfDocument(new PdfReader(DESTINATION_FOLDER + outFileName))) {
                String expectedProdLine = CreateExpectedProducerLine(new ConfirmedEventWrapper[] { GetPdfHtmlEvent() });
                NUnit.Framework.Assert.AreEqual(expectedProdLine, pdfDocument.GetDocumentInfo().GetProducer());
            }
        }

        [NUnit.Framework.Test]
        public virtual void ConvertHtmlToPdfWithExistPdfTest() {
            String outFileName = "helloWorld_to_pdf.pdf";
            using (PdfDocument pdfDocument = new PdfDocument(new PdfWriter(DESTINATION_FOLDER + outFileName))) {
                HtmlConverter.ConvertToPdf(SOURCE_FOLDER + "helloWorld.html", pdfDocument, new ConverterProperties());
            }
            IList<ConfirmEvent> events = handler.GetEvents();
            NUnit.Framework.Assert.AreEqual(2, events.Count);
            AbstractProductProcessITextEvent @event = events[0].GetConfirmedEvent();
            NUnit.Framework.Assert.AreEqual(ITextCoreEvent.PROCESS_PDF, @event.GetEventType());
            @event = events[1].GetConfirmedEvent();
            NUnit.Framework.Assert.AreEqual(PdfHtmlProductEvent.CONVERT_HTML, @event.GetEventType());
            using (PdfDocument pdfDocument_1 = new PdfDocument(new PdfReader(DESTINATION_FOLDER + outFileName))) {
                String expectedProdLine = CreateExpectedProducerLine(new ConfirmedEventWrapper[] { GetCoreEvent(), GetPdfHtmlEvent
                    () });
                NUnit.Framework.Assert.AreEqual(expectedProdLine, pdfDocument_1.GetDocumentInfo().GetProducer());
            }
        }

        [NUnit.Framework.Test]
        public virtual void NestedElementToDocumentTest() {
            ByteArrayOutputStream baos = new ByteArrayOutputStream();
            SequenceId docSequenceId;
            using (PdfDocument pdfDocument = new PdfDocument(new PdfWriter(baos))) {
                using (Document document = new Document(pdfDocument)) {
                    docSequenceId = pdfDocument.GetDocumentIdWrapper();
                    String firstHtml = "<p>Hello world first!</p>";
                    Paragraph pWithId = (Paragraph)HtmlConverter.ConvertToElements(firstHtml)[0];
                    Paragraph pWithoutId = new Paragraph("");
                    pWithoutId.Add(pWithId);
                    Div divWithoutId = new Div();
                    divWithoutId.Add(pWithoutId);
                    document.Add(divWithoutId);
                }
            }
            IList<AbstractProductProcessITextEvent> events = CONFIGURATION_ACCESS.GetPublicEvents(docSequenceId);
            NUnit.Framework.Assert.AreEqual(2, events.Count);
            NUnit.Framework.Assert.IsTrue(events[0] is ConfirmedEventWrapper);
            ConfirmedEventWrapper confirmedEventWrapper = (ConfirmedEventWrapper)events[0];
            NUnit.Framework.Assert.IsTrue(confirmedEventWrapper.GetEvent() is ITextCoreEvent);
            NUnit.Framework.Assert.AreEqual(ITextCoreEvent.PROCESS_PDF, confirmedEventWrapper.GetEvent().GetEventType(
                ));
            NUnit.Framework.Assert.IsTrue(events[1] is ConfirmedEventWrapper);
            confirmedEventWrapper = (ConfirmedEventWrapper)events[1];
            NUnit.Framework.Assert.IsTrue(confirmedEventWrapper.GetEvent() is PdfHtmlProductEvent);
            NUnit.Framework.Assert.AreEqual(PdfHtmlProductEvent.CONVERT_HTML, confirmedEventWrapper.GetEvent().GetEventType
                ());
            using (PdfDocument pdfDocument_1 = new PdfDocument(new PdfReader(new MemoryStream(baos.ToArray())))) {
                String expectedProdLine = CreateExpectedProducerLine(new ConfirmedEventWrapper[] { GetCoreEvent(), GetPdfHtmlEvent
                    () });
                NUnit.Framework.Assert.AreEqual(expectedProdLine, pdfDocument_1.GetDocumentInfo().GetProducer());
            }
        }

        [NUnit.Framework.Test]
        [LogMessage(KernelLogMessageConstant.UNCONFIRMED_EVENT)]
        public virtual void UnreportedCoreEventTest() {
            String outFileName = DESTINATION_FOLDER + "unreportedCoreEvent.pdf";
            String html = "<html><head></head>" + "<body style=\"font-size:12.0pt; font-family:Arial\">" + "<p>Text in paragraph</p>"
                 + "</body></html>";
            using (PdfDocument document = new PdfDocument(new PdfWriter(outFileName), new DocumentProperties().SetEventCountingMetaInfo
                (new Html2PdfEventsHandlingTest.HtmlTestMetaInfo()))) {
                document.AddNewPage();
                HtmlConverter.ConvertToDocument(html, document, new ConverterProperties());
                ITextCoreEvent coreEvent = ITextCoreEvent.CreateProcessPdfEvent(document.GetDocumentIdWrapper(), null, EventConfirmationType
                    .ON_DEMAND);
                EventManager.GetInstance().OnEvent(coreEvent);
            }
            String expectedProdLine = CreateExpectedProducerLine(new ConfirmedEventWrapper[] { GetPdfHtmlEvent() });
            ValidatePdfProducerLine(outFileName, expectedProdLine);
        }

        [NUnit.Framework.Test]
        [LogMessage(KernelLogMessageConstant.UNCONFIRMED_EVENT)]
        public virtual void UnreportedPdfHtmlEventTest() {
            String outFileName = DESTINATION_FOLDER + "unreportedPdfHtmlEvent.pdf";
            using (PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFileName))) {
                pdfDocument.AddNewPage();
                PdfHtmlTestProductEvent @event = new PdfHtmlTestProductEvent(pdfDocument.GetDocumentIdWrapper(), "event data"
                    , EventConfirmationType.ON_DEMAND);
                EventManager.GetInstance().OnEvent(@event);
            }
            String expectedProdLine = CreateExpectedProducerLine(new ConfirmedEventWrapper[] { GetCoreEvent() });
            ValidatePdfProducerLine(outFileName, expectedProdLine);
        }

        private class HtmlTestMetaInfo : IMetaInfo {
        }

        private void ValidatePdfProducerLine(String filePath, String expected) {
            using (PdfDocument pdfDocument = new PdfDocument(new PdfReader(filePath))) {
                NUnit.Framework.Assert.AreEqual(expected, pdfDocument.GetDocumentInfo().GetProducer());
            }
        }

        private static String CreateExpectedProducerLine(ConfirmedEventWrapper[] expectedEvents) {
            IList<ConfirmedEventWrapper> listEvents = JavaUtil.ArraysAsList(expectedEvents);
            return ProducerBuilder.ModifyProducer(listEvents, null);
        }

        private static ConfirmedEventWrapper GetPdfHtmlEvent() {
            DefaultITextProductEventProcessor processor = new DefaultITextProductEventProcessor(ProductNameConstant.PDF_HTML
                );
            return new ConfirmedEventWrapper(PdfHtmlProductEvent.CreateConvertHtmlEvent(new SequenceId(), null), processor
                .GetUsageType(), processor.GetProducer());
        }

        private static ConfirmedEventWrapper GetCoreEvent() {
            DefaultITextProductEventProcessor processor = new DefaultITextProductEventProcessor(ProductNameConstant.ITEXT_CORE
                );
            return new ConfirmedEventWrapper(ITextCoreEvent.CreateProcessPdfEvent(new SequenceId(), null, EventConfirmationType
                .ON_CLOSE), processor.GetUsageType(), processor.GetProducer());
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

        private class StoreEventsHandler : IBaseEventHandler {
            private IList<ConfirmEvent> events = new List<ConfirmEvent>();

            public virtual IList<ConfirmEvent> GetEvents() {
                return events;
            }

            public virtual void OnEvent(IBaseEvent @event) {
                if (@event is ConfirmEvent) {
                    events.Add((ConfirmEvent)@event);
                }
            }
        }
    }
}
