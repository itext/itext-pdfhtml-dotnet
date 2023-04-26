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
using System;
using System.Collections.Generic;

namespace iText.Html2pdf.Attach.Util {
    /// <summary>Helper class to keep track of the current column / row position in a table.</summary>
    public class RowColHelper {
        /// <summary>The last empty row.</summary>
        private List<int> lastEmptyRow = new List<int>();

        /// <summary>The current row index.</summary>
        private int currRow = -1;

        /// <summary>The current column index.</summary>
        private int currCol = 0;

        /// <summary>Move to a new row.</summary>
        public virtual void NewRow() {
            ++currRow;
            currCol = 0;
        }

        /// <summary>Update current position based on a colspan and a rowspan.</summary>
        /// <param name="colspan">the colspan</param>
        /// <param name="rowspan">the rowspan</param>
        public virtual void UpdateCurrentPosition(int colspan, int rowspan) {
            EnsureRowIsStarted();
            while (lastEmptyRow.Count < currCol) {
                lastEmptyRow.Add(currRow);
            }
            int value = currRow + rowspan;
            int end = currCol + colspan;
            int middle = Math.Min(lastEmptyRow.Count, end);
            for (int i = currCol; i < middle; ++i) {
                lastEmptyRow[i] = Math.Max(value, lastEmptyRow[i]);
            }
            while (lastEmptyRow.Count < end) {
                lastEmptyRow.Add(value);
            }
            currCol = end;
        }

        /// <summary>Move to next empty column.</summary>
        /// <returns>the current column position</returns>
        public virtual int MoveToNextEmptyCol() {
            EnsureRowIsStarted();
            while (!CanPutCell(currCol)) {
                ++currCol;
            }
            return currCol;
        }

        /// <summary>Checks if we can put a new cell in the column.</summary>
        /// <param name="col">the column index</param>
        /// <returns>true, if successful</returns>
        private bool CanPutCell(int col) {
            EnsureRowIsStarted();
            if (col >= lastEmptyRow.Count) {
                return true;
            }
            else {
                return lastEmptyRow[col] <= currRow;
            }
        }

        /// <summary>Ensure that a row is started.</summary>
        private void EnsureRowIsStarted() {
            if (currRow == -1) {
                NewRow();
            }
        }
    }
}
