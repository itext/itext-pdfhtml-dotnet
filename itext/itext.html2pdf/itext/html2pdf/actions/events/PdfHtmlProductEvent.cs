/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 iText Group NV
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
using iText.Commons.Actions;
using iText.Commons.Actions.Confirmations;
using iText.Commons.Actions.Contexts;
using iText.Commons.Actions.Sequence;
using iText.Html2pdf.Actions.Data;

namespace iText.Html2pdf.Actions.Events {
    /// <summary>Class represents events registered in iText pdfHTML module.</summary>
    public sealed class PdfHtmlProductEvent : AbstractProductProcessITextEvent {
        /// <summary>Convert html event type.</summary>
        public const String CONVERT_HTML = "convert-html";

        private readonly String eventType;

        /// <summary>Creates an event associated with a general identifier and additional meta data.</summary>
        /// <param name="sequenceId">is an identifier associated with the event</param>
        /// <param name="metaInfo">is an additional meta info</param>
        /// <param name="eventType">is a string description of the event</param>
        private PdfHtmlProductEvent(SequenceId sequenceId, IMetaInfo metaInfo, String eventType)
            : base(sequenceId, PdfHtmlProductData.GetInstance(), metaInfo, EventConfirmationType.ON_CLOSE) {
            this.eventType = eventType;
        }

        /// <summary>Creates a convert html event which associated with a general identifier and additional meta data.
        ///     </summary>
        /// <param name="sequenceId">is an identifier associated with the event</param>
        /// <param name="metaInfo">is an additional meta info</param>
        /// <returns>the convert html event</returns>
        public static iText.Html2pdf.Actions.Events.PdfHtmlProductEvent CreateConvertHtmlEvent(SequenceId sequenceId
            , IMetaInfo metaInfo) {
            return new iText.Html2pdf.Actions.Events.PdfHtmlProductEvent(sequenceId, metaInfo, CONVERT_HTML);
        }

        public override String GetEventType() {
            return eventType;
        }
    }
}
