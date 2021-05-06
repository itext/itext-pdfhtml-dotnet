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
using iText.Html2pdf.Actions.Data;
using iText.Kernel.Actions;
using iText.Kernel.Actions.Events;
using iText.Kernel.Actions.Sequence;
using iText.Kernel.Counter.Event;

namespace iText.Html2pdf.Actions.Events {
    /// <summary>Class represents events registered in iText pdfHTML module.</summary>
    public class PdfHtmlProductEvent : AbstractITextProductEvent {
        /// <summary>Convert elements event type.</summary>
        public const String CONVERT_ELEMENTS = "convert-elements-event";

        private readonly String eventType;

        /// <summary>Creates an event associated with a general identifier and additional meta data.</summary>
        /// <param name="sequenceId">is an identifier associated with the event</param>
        /// <param name="metaInfo">is an additional meta info</param>
        /// <param name="eventType">is a string description of the event</param>
        public PdfHtmlProductEvent(SequenceId sequenceId, IMetaInfo metaInfo, String eventType)
            : base(sequenceId, PdfHtmlProductData.GetInstance(), metaInfo) {
            this.eventType = eventType;
        }

        public override String GetProductName() {
            return ProductNameConstant.PDF_HTML;
        }

        public override String GetEventType() {
            return eventType;
        }
    }
}
