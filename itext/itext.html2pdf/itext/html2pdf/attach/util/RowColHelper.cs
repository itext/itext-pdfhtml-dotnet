/*
This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
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
        public virtual bool CanPutCell(int col) {
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
