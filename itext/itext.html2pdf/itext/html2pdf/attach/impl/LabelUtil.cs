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
using iText.Html2pdf.Html;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl {
    /// <summary>Utility class for handling operations related to labels</summary>
    public sealed class LabelUtil {
        private LabelUtil() {
        }

        // Utility class, no instances allowed
        /// <summary>Determines whether the provided element can be labeled.</summary>
        /// <param name="element">element to be checked</param>
        /// <returns>true if the element can be labeled; false otherwise</returns>
        public static bool IsLabelable(INameContainer element) {
            return TagConstants.INPUT.Equals(element.Name()) || TagConstants.TEXTAREA.Equals(element.Name()) || TagConstants
                .SELECT.Equals(element.Name()) || TagConstants.BUTTON.Equals(element.Name());
        }
    }
}
