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
using System.Collections.Generic;
using iText.Commons.Utils;
using iText.Layout.Element;

namespace iText.Html2pdf.Attach.Wrapelement {
    /// <summary>
    /// Wrapper for the
    /// <c>tr</c>
    /// element.
    /// </summary>
    public class TableRowWrapper : IWrapElement {
        /// <summary>The cells in the row.</summary>
        private IList<Cell> cells = new List<Cell>();

        /// <summary>Adds a cell to the row.</summary>
        /// <param name="cell">the cell</param>
        public virtual void AddCell(Cell cell) {
            cells.Add(cell);
        }

        /// <summary>Gets the cells of the row.</summary>
        /// <returns>the cells</returns>
        public virtual IList<Cell> GetCells() {
            return JavaCollectionsUtil.UnmodifiableList(cells);
        }
    }
}
