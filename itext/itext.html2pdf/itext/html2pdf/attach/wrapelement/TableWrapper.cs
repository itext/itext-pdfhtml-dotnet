/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2017 iText Group NV
    Authors: iText Software.

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
    address: sales@itextpdf.com */
using System;
using System.Collections.Generic;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Attach.Wrapelement {
    public class TableWrapper : IWrapElement {
        private IList<IList<Cell>> rows;

        private IList<IList<Cell>> headerRows;

        private IList<IList<Cell>> footerRows;

        public virtual int GetRowsSize() {
            return rows.Count;
        }

        public virtual void NewRow() {
            if (rows == null) {
                rows = new List<IList<Cell>>();
            }
            rows.Add(new List<Cell>());
        }

        public virtual void NewHeaderRow() {
            if (headerRows == null) {
                headerRows = new List<IList<Cell>>();
            }
            headerRows.Add(new List<Cell>());
        }

        public virtual void NewFooterRow() {
            if (footerRows == null) {
                footerRows = new List<IList<Cell>>();
            }
            footerRows.Add(new List<Cell>());
        }

        public virtual void AddHeaderCell(Cell cell) {
            if (headerRows == null) {
                headerRows = new List<IList<Cell>>();
            }
            if (headerRows.Count == 0) {
                NewHeaderRow();
            }
            headerRows[headerRows.Count - 1].Add(cell);
        }

        public virtual void AddFooterCell(Cell cell) {
            if (footerRows == null) {
                footerRows = new List<IList<Cell>>();
            }
            if (footerRows.Count == 0) {
                NewFooterRow();
            }
            footerRows[footerRows.Count - 1].Add(cell);
        }

        public virtual void AddCell(Cell cell) {
            if (rows == null) {
                rows = new List<IList<Cell>>();
            }
            if (rows.Count == 0) {
                NewRow();
            }
            rows[rows.Count - 1].Add(cell);
        }

        public virtual Table ToTable() {
            IList<UnitValue> maxWidths = new List<UnitValue>();
            if (rows != null) {
                CalculateMaxWidths(rows, maxWidths);
            }
            if (headerRows != null) {
                CalculateMaxWidths(headerRows, maxWidths);
            }
            if (footerRows != null) {
                CalculateMaxWidths(footerRows, maxWidths);
            }
            UnitValue[] arr = new UnitValue[maxWidths.Count];
            int nullWidth = 0;
            foreach (UnitValue width in maxWidths) {
                if (width == null) {
                    nullWidth++;
                }
            }
            for (int k = 0; k < maxWidths.Count; k++) {
                UnitValue width_1 = maxWidths[k];
                if (width_1 == null && nullWidth > 0) {
                    width_1 = UnitValue.CreatePercentValue(100 / nullWidth);
                }
                arr[k] = width_1;
            }
            Table table;
            if (arr.Length > 0) {
                table = new Table(arr);
            }
            else {
                // if table is empty, create empty table with single column
                table = new Table(1);
            }
            if (headerRows != null) {
                foreach (IList<Cell> headerRow in headerRows) {
                    foreach (Cell headerCell in headerRow) {
                        table.AddHeaderCell((Cell)headerCell);
                    }
                }
            }
            if (footerRows != null) {
                foreach (IList<Cell> footerRow in footerRows) {
                    foreach (Cell footerCell in footerRow) {
                        table.AddFooterCell((Cell)footerCell);
                    }
                }
            }
            if (rows != null) {
                for (int i = 0; i < rows.Count; i++) {
                    for (int j = 0; j < rows[i].Count; j++) {
                        table.AddCell((Cell)(rows[i][j]));
                    }
                    if (i != rows.Count - 1) {
                        table.StartNewRow();
                    }
                }
            }
            return table;
        }

        private void CalculateMaxWidths(IList<IList<Cell>> rows, IList<UnitValue> maxWidths) {
            int maxRowSize = 1;
            foreach (IList<Cell> row in rows) {
                maxRowSize = Math.Max(maxRowSize, row.Count);
                for (int j = 0; j < row.Count; j++) {
                    if (maxWidths.Count <= j) {
                        UnitValue width = row[j].GetWidth();
                        if (width == null) {
                            maxWidths.Add(null);
                        }
                        else {
                            maxWidths.Add(row[j].GetWidth());
                        }
                    }
                    else {
                        UnitValue maxWidth = maxWidths[j];
                        UnitValue width = row[j].GetWidth();
                        if (width != null && (maxWidth == null || width.GetValue() > maxWidth.GetValue())) {
                            maxWidths[j] = width;
                        }
                    }
                }
            }
        }
    }
}
