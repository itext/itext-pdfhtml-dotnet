/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
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
using iText.Layout.Element;

namespace iText.Html2pdf.Attach.Impl.Layout {
    /// <summary>
    /// The HTML implementation of an
    /// <see cref="iText.Layout.Element.AreaBreak"/>.
    /// </summary>
    public class HtmlPageBreak : AreaBreak {
//\cond DO_NOT_DOCUMENT
        /// <summary>
        /// The
        /// <see cref="HtmlPageBreakType"/>.
        /// </summary>
        internal HtmlPageBreakType breakType;
//\endcond

        /// <summary>Instantiates a new html page break.</summary>
        /// <param name="type">the page break type</param>
        public HtmlPageBreak(HtmlPageBreakType type) {
            this.breakType = type;
        }

//\cond DO_NOT_DOCUMENT
        /// <summary>
        /// Gets the
        /// <see cref="HtmlPageBreakType"/>.
        /// </summary>
        /// <returns>the page break type</returns>
        internal virtual HtmlPageBreakType GetBreakType() {
            return breakType;
        }
//\endcond
    }
}
