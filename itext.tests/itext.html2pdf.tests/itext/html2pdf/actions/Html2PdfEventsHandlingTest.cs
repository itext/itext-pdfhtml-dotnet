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
using iText.Html2pdf;
using iText.Html2pdf.Actions.Events;
using iText.IO.Source;
using iText.Kernel.Actions;
using iText.Kernel.Actions.Events;
using iText.Kernel.Actions.Sequence;
using iText.Kernel.Counter.Event;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Test;

namespace iText.Html2pdf.Actions {
    public class Html2PdfEventsHandlingTest : ExtendedITextTest {
        private static readonly TestConfigurationEvent CONFIGURATION_ACCESS = new TestConfigurationEvent();

        [NUnit.Framework.Test]
        public virtual void TwoSetOfElementsToOneDocumentTest() {
            ByteArrayOutputStream baos = new ByteArrayOutputStream();
            SequenceId docSequenceId;
            using (PdfDocument pdfDocument = new PdfDocument(new PdfWriter(baos))) {
                using (Document document = new Document(pdfDocument)) {
                    docSequenceId = pdfDocument.GetDocumentIdWrapper();
                    String firstHtml = "<p>Hello world first!</p>";
                    IList<IElement> lstFirst = HtmlConverter.ConvertToElements(firstHtml);
                    AddElementsToDocument(document, lstFirst);
                    AbstractIdentifiableElement identifiableElement = (AbstractIdentifiableElement)lstFirst[0];
                    // TODO DEVSIX-5304 remove LinkDocumentIdEvent after adding support linking in the scope of layout module
                    EventManager.GetInstance().OnEvent(new LinkDocumentIdEvent(pdfDocument, SequenceIdManager.GetSequenceId(identifiableElement
                        )));
                    String secondHtml = "<p>Hello world second!</p>";
                    IList<IElement> lstSecond = HtmlConverter.ConvertToElements(secondHtml);
                    AddElementsToDocument(document, lstSecond);
                    identifiableElement = (AbstractIdentifiableElement)lstSecond[0];
                    // TODO DEVSIX-5304 remove LinkDocumentIdEvent after adding support linking in the scope of layout module
                    EventManager.GetInstance().OnEvent(new LinkDocumentIdEvent(pdfDocument, SequenceIdManager.GetSequenceId(identifiableElement
                        )));
                }
            }
            IList<AbstractProductProcessITextEvent> events = CONFIGURATION_ACCESS.GetPublicEvents(docSequenceId);
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
                String producerLine = pdfDocument_1.GetDocumentInfo().GetProducer();
                // TODO DEVSIX-5304 improve producer line check to check via some template
                NUnit.Framework.Assert.AreNotEqual(-1, producerLine.IndexOf("pdfHTML", StringComparison.Ordinal));
                NUnit.Framework.Assert.AreEqual(producerLine.IndexOf("pdfHTML", StringComparison.Ordinal), producerLine.LastIndexOf
                    ("pdfHTML"));
                NUnit.Framework.Assert.AreNotEqual(-1, producerLine.IndexOf("Core", StringComparison.Ordinal));
            }
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
    }
}
