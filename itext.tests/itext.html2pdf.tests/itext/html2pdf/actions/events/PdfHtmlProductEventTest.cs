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
using iText.Html2pdf.Actions.Data;
using iText.Kernel.Actions;
using iText.Kernel.Actions.Events;
using iText.Kernel.Actions.Sequence;
using iText.Test;

namespace iText.Html2pdf.Actions.Events {
    public class PdfHtmlProductEventTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void ConvertElementsEventTest() {
            SequenceId sequenceId = new SequenceId();
            PdfHtmlProductEvent @event = PdfHtmlProductEvent.CreateConvertHtmlEvent(sequenceId, new PdfHtmlTestMetaInfo
                ("meta data"));
            NUnit.Framework.Assert.AreEqual(PdfHtmlProductEvent.CONVERT_HTML, @event.GetEventType());
            NUnit.Framework.Assert.AreEqual(ProductNameConstant.PDF_HTML, @event.GetProductName());
            NUnit.Framework.Assert.AreEqual(EventConfirmationType.ON_CLOSE, @event.GetConfirmationType());
            NUnit.Framework.Assert.AreEqual(sequenceId, @event.GetSequenceId());
            NUnit.Framework.Assert.AreEqual(PdfHtmlProductData.GetInstance().GetPublicProductName(), @event.GetProductData
                ().GetPublicProductName());
            NUnit.Framework.Assert.AreEqual(PdfHtmlProductData.GetInstance().GetProductName(), @event.GetProductData()
                .GetProductName());
            NUnit.Framework.Assert.AreEqual(PdfHtmlProductData.GetInstance().GetVersion(), @event.GetProductData().GetVersion
                ());
            NUnit.Framework.Assert.AreEqual(PdfHtmlProductData.GetInstance().GetSinceCopyrightYear(), @event.GetProductData
                ().GetSinceCopyrightYear());
            NUnit.Framework.Assert.AreEqual(PdfHtmlProductData.GetInstance().GetToCopyrightYear(), @event.GetProductData
                ().GetToCopyrightYear());
        }
    }
}
