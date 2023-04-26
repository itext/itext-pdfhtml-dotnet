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
namespace iText.Html2pdf.Attach.Impl.Layout {
    /// <summary>Set of constants that will be used as keys to get and set properties.</summary>
    public class Html2PdfProperty {
        /// <summary>The Constant PROPERTY_START.</summary>
        private const int PROPERTY_START = (1 << 20);

        /// <summary>The Constant KEEP_WITH_PREVIOUS works only for top-level elements, i.e. ones that are added to the document directly.
        ///     </summary>
        public const int KEEP_WITH_PREVIOUS = PROPERTY_START + 1;

        /// <summary>The Constant PAGE_COUNT_TYPE.</summary>
        public const int PAGE_COUNT_TYPE = PROPERTY_START + 2;

        /// <summary>The Constant BODY_STYLING.</summary>
        public const int BODY_STYLING = PROPERTY_START + 3;

        /// <summary>The Constant HTML_STYLING.</summary>
        public const int HTML_STYLING = PROPERTY_START + 4;

        /// <summary>The Constant CAPITALIZE_ELEMENT indicates if an inline element needs to be capitalized.</summary>
        public const int CAPITALIZE_ELEMENT = PROPERTY_START + 5;
    }
}
