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
using iText.Html2pdf.Attach.Util;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Attach.Wrapelement {
    public class TableWrapper : IWrapElement {
        private IList<IList<TableWrapper.CellWrapper>> rows;

        private IList<IList<TableWrapper.CellWrapper>> headerRows;

        private IList<IList<TableWrapper.CellWrapper>> footerRows;

        private RowColHelper rowShift = new RowColHelper();

        private RowColHelper headerRowShift = new RowColHelper();

        private RowColHelper footerRowShift = new RowColHelper();

        private int numberOfColumns = 0;

        public virtual int GetRowsSize() {
            return rows.Count;
        }

        public virtual void NewRow() {
            if (rows == null) {
                rows = new List<IList<TableWrapper.CellWrapper>>();
            }
            rowShift.NewRow();
            rows.Add(new List<TableWrapper.CellWrapper>());
        }

        public virtual void NewHeaderRow() {
            if (headerRows == null) {
                headerRows = new List<IList<TableWrapper.CellWrapper>>();
            }
            headerRowShift.NewRow();
            headerRows.Add(new List<TableWrapper.CellWrapper>());
        }

        public virtual void NewFooterRow() {
            if (footerRows == null) {
                footerRows = new List<IList<TableWrapper.CellWrapper>>();
            }
            footerRowShift.NewRow();
            footerRows.Add(new List<TableWrapper.CellWrapper>());
        }

        public virtual void AddHeaderCell(Cell cell) {
            if (headerRows == null) {
                headerRows = new List<IList<TableWrapper.CellWrapper>>();
            }
            if (headerRows.Count == 0) {
                NewHeaderRow();
            }
            AddCellToTable(cell, headerRows, headerRowShift);
        }

        public virtual void AddFooterCell(Cell cell) {
            if (footerRows == null) {
                footerRows = new List<IList<TableWrapper.CellWrapper>>();
            }
            if (footerRows.Count == 0) {
                NewFooterRow();
            }
            AddCellToTable(cell, footerRows, footerRowShift);
        }

        public virtual void AddCell(Cell cell) {
            if (rows == null) {
                rows = new List<IList<TableWrapper.CellWrapper>>();
            }
            if (rows.Count == 0) {
                NewRow();
            }
            AddCellToTable(cell, rows, rowShift);
        }

        private void AddCellToTable(Cell cell, IList<IList<TableWrapper.CellWrapper>> table, RowColHelper tableRowShift
            ) {
            int col = tableRowShift.MoveToNextEmptyCol();
            tableRowShift.UpdateCurrentPosition(cell.GetColspan(), cell.GetRowspan());
            IList<TableWrapper.CellWrapper> currentRow = table[table.Count - 1];
            currentRow.Add(new TableWrapper.CellWrapper(col, cell));
            numberOfColumns = Math.Max(numberOfColumns, col + cell.GetColspan());
        }

        public virtual Table ToTable(WaitingColgroupsHelper colgroupsHelper) {
            Table table;
            if (numberOfColumns > 0) {
                table = new Table(GetColWidths(colgroupsHelper));
            }
            else {
                // if table is empty, create empty table with single column
                table = new Table(1);
            }
            //Workaround to remove default width:100%
            table.DeleteOwnProperty(Property.WIDTH);
            if (headerRows != null) {
                foreach (IList<TableWrapper.CellWrapper> headerRow in headerRows) {
                    foreach (TableWrapper.CellWrapper headerCell in headerRow) {
                        table.AddHeaderCell(headerCell.cell);
                    }
                }
            }
            if (footerRows != null) {
                foreach (IList<TableWrapper.CellWrapper> footerRow in footerRows) {
                    foreach (TableWrapper.CellWrapper footerCell in footerRow) {
                        table.AddFooterCell(footerCell.cell);
                    }
                }
            }
            if (rows != null) {
                for (int i = 0; i < rows.Count; i++) {
                    for (int j = 0; j < rows[i].Count; j++) {
                        table.AddCell(rows[i][j].cell);
                    }
                    if (i != rows.Count - 1) {
                        table.StartNewRow();
                    }
                }
            }
            return table;
        }

        private UnitValue[] GetColWidths(WaitingColgroupsHelper colgroups) {
            UnitValue[] colWidths = new UnitValue[numberOfColumns];
            if (colgroups == null) {
                for (int i = 0; i < numberOfColumns; i++) {
                    colWidths[i] = null;
                }
            }
            else {
                for (int i = 0; i < numberOfColumns; i++) {
                    colWidths[i] = colgroups.GetColWraper(i) != null ? colgroups.GetColWraper(i).GetWidth() : null;
                }
            }
            return colWidths;
        }

        private class CellWrapper {
            internal int col;

            internal Cell cell;

            internal CellWrapper(int col, Cell cell) {
                this.col = col;
                this.cell = cell;
            }
        }
    }
}
