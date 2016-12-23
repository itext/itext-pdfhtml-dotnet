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
            table[table.Count - 1].Add(new TableWrapper.CellWrapper(this, col, cell));
        }

        public virtual Table ToTable(WaitingColgroupsHelper colgroupsHelper) {
            UnitValue[] widths = RecalculateWidths(colgroupsHelper);
            Table table;
            if (widths.Length > 0) {
                table = new Table(widths);
            }
            else {
                // if table is empty, create empty table with single column
                table = new Table(1);
            }
            if (headerRows != null) {
                foreach (IList<TableWrapper.CellWrapper> headerRow in headerRows) {
                    foreach (TableWrapper.CellWrapper headerCell in headerRow) {
                        table.AddHeaderCell((Cell)headerCell.cell);
                    }
                }
            }
            if (footerRows != null) {
                foreach (IList<TableWrapper.CellWrapper> footerRow in footerRows) {
                    foreach (TableWrapper.CellWrapper footerCell in footerRow) {
                        table.AddFooterCell((Cell)footerCell.cell);
                    }
                }
            }
            if (rows != null) {
                for (int i = 0; i < rows.Count; i++) {
                    for (int j = 0; j < rows[i].Count; j++) {
                        table.AddCell((Cell)(rows[i][j].cell));
                    }
                    if (i != rows.Count - 1) {
                        table.StartNewRow();
                    }
                }
            }
            return table;
        }

        private UnitValue[] RecalculateWidths(WaitingColgroupsHelper colgroupsHelper) {
            IList<UnitValue> maxAbsoluteWidths = new List<UnitValue>();
            IList<UnitValue> maxPercentageWidths = new List<UnitValue>();
            if (rows != null) {
                CalculateMaxWidths(rows, maxAbsoluteWidths, maxPercentageWidths, colgroupsHelper);
            }
            if (headerRows != null) {
                CalculateMaxWidths(headerRows, maxAbsoluteWidths, maxPercentageWidths, colgroupsHelper);
            }
            if (footerRows != null) {
                CalculateMaxWidths(footerRows, maxAbsoluteWidths, maxPercentageWidths, colgroupsHelper);
            }
            UnitValue[] tableWidths = new UnitValue[maxAbsoluteWidths.Count];
            float totalAbsoluteSum = 0;
            float totalPercentSum = 0;
            float maxTotalWidth = 0;
            int nullWidth = 0;
            UnitValue curAbsWidth;
            UnitValue curPerWidth;
            for (int i = 0; i < tableWidths.Length; ++i) {
                if (maxPercentageWidths[i] != null) {
                    curPerWidth = maxPercentageWidths[i];
                    totalPercentSum += curPerWidth.GetValue();
                    tableWidths[i] = maxPercentageWidths[i];
                    if (maxAbsoluteWidths[i] != null) {
                        curAbsWidth = maxAbsoluteWidths[i];
                        maxTotalWidth = Math.Max(maxTotalWidth, 100 / curPerWidth.GetValue() * curAbsWidth.GetValue());
                    }
                }
                else {
                    if (maxAbsoluteWidths[i] != null) {
                        curAbsWidth = maxAbsoluteWidths[i];
                        totalAbsoluteSum += curAbsWidth.GetValue();
                        tableWidths[i] = curAbsWidth;
                    }
                    else {
                        ++nullWidth;
                    }
                }
            }
            if (totalPercentSum < 100) {
                maxTotalWidth = Math.Max(maxTotalWidth, 100 / (100 - totalPercentSum) * totalAbsoluteSum);
                // TODO: Layout based maxWidth calculations needed here. Currently unsupported.
                for (int i_1 = 0; i_1 < tableWidths.Length; i_1++) {
                    UnitValue width = tableWidths[i_1];
                    if (width == null && nullWidth > 0) {
                        tableWidths[i_1] = UnitValue.CreatePercentValue((100 - totalPercentSum) / nullWidth);
                    }
                }
            }
            else {
                if (nullWidth != 0 && nullWidth < tableWidths.Length) {
                    // TODO: In this case, the columns without percent width should be assigned to min-width. This is currently unsupported.
                    // So we fall back to just division of the available place uniformly.
                    for (int i_1 = 0; i_1 < tableWidths.Length; i_1++) {
                        tableWidths[i_1] = UnitValue.CreatePercentValue(100 / tableWidths.Length);
                    }
                }
            }
            return tableWidths;
        }

        private void CalculateMaxWidths(IList<IList<TableWrapper.CellWrapper>> rows, IList<UnitValue> absoluteMaxWidths
            , IList<UnitValue> percentageMaxWidths, WaitingColgroupsHelper colgroupsHelper) {
            foreach (IList<TableWrapper.CellWrapper> row in rows) {
                foreach (TableWrapper.CellWrapper cellWrapper in row) {
                    int colspan = cellWrapper.cell.GetColspan();
                    UnitValue cellWidth = cellWrapper.cell.GetWidth();
                    UnitValue collWidth = null;
                    if (colspan > 1 && cellWidth != null) {
                        cellWidth = new UnitValue(cellWidth.GetUnitType(), cellWidth.GetValue() / colspan);
                    }
                    if (colgroupsHelper != null && colgroupsHelper.GetColWraper(cellWrapper.col) != null) {
                        collWidth = colgroupsHelper.GetColWraper(cellWrapper.col).GetWidth();
                    }
                    UnitValue absoluteWidth = GetMaxValue(UnitValue.POINT, cellWidth, collWidth);
                    UnitValue percentageWidth = GetMaxValue(UnitValue.PERCENT, cellWidth, collWidth);
                    ApplyNewWidth(absoluteMaxWidths, cellWrapper.col, cellWrapper.col + colspan, absoluteWidth);
                    ApplyNewWidth(percentageMaxWidths, cellWrapper.col, cellWrapper.col + colspan, percentageWidth);
                }
            }
        }

        private void ApplyNewWidth(IList<UnitValue> maxWidths, int start, int end, UnitValue value) {
            UnitValue old;
            while (maxWidths.Count < start) {
                maxWidths.Add(null);
            }
            int middle = Math.Min(maxWidths.Count, end);
            for (int i = start; i < middle; ++i) {
                maxWidths[i] = GetMaxValue(maxWidths[i], value);
            }
            while (maxWidths.Count < end) {
                maxWidths.Add(value);
            }
        }

        private UnitValue GetMaxValue(int unitType, UnitValue first, UnitValue second) {
            if (first != null && first.GetUnitType() != unitType) {
                first = null;
            }
            if (second != null && second.GetUnitType() != unitType) {
                second = null;
            }
            return GetMaxValue(first, second);
        }

        private UnitValue GetMaxValue(UnitValue first, UnitValue second) {
            if (first == null) {
                return second;
            }
            if (second == null) {
                return first;
            }
            return first.GetValue() < second.GetValue() ? second : first;
        }

        private class CellWrapper {
            internal int col;

            internal Cell cell;

            public CellWrapper(TableWrapper _enclosing, int col, Cell cell) {
                this._enclosing = _enclosing;
                this.col = col;
                this.cell = cell;
            }

            private readonly TableWrapper _enclosing;
        }
    }
}
