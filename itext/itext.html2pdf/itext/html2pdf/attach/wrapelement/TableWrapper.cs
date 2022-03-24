/*
This file is part of the iText (R) project.
Copyright (c) 1998-2022 iText Group NV
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
using iText.Commons.Utils;
using iText.Html2pdf.Attach.Util;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Attach.Wrapelement {
    /// <summary>
    /// Wrapper for the
    /// <c>table</c>
    /// element.
    /// </summary>
    public class TableWrapper : IWrapElement {
        /// <summary>The body rows of the table.</summary>
        private IList<IList<TableWrapper.CellWrapper>> rows;

        /// <summary>The header rows.</summary>
        private IList<IList<TableWrapper.CellWrapper>> headerRows;

        /// <summary>The footer rows.</summary>
        private IList<IList<TableWrapper.CellWrapper>> footerRows;

        /// <summary>The current position in the body of the table (row / column).</summary>
        private RowColHelper rowShift = new RowColHelper();

        /// <summary>The current position in the header of the table (row / column).</summary>
        private RowColHelper headerRowShift = new RowColHelper();

        /// <summary>The current position in the footer of the table (row / column).</summary>
        private RowColHelper footerRowShift = new RowColHelper();

        /// <summary>The number of columns.</summary>
        private int numberOfColumns = 0;

        /// <summary>The direction value.</summary>
        private bool isRtl = false;

        /// <summary>The caption value.</summary>
        private Div caption = null;

        /// <summary>The lang attribute value.</summary>
        private String lang;

        /// <summary>The footer lang attribute value.</summary>
        private String footerLang;

        /// <summary>The header lang attribute value.</summary>
        private String headerLang;

        public TableWrapper() {
        }

        public TableWrapper(bool isRtl) {
            this.isRtl = isRtl;
        }

        /// <summary>Gets the number of rows.</summary>
        /// <returns>the number of rows</returns>
        public virtual int GetRowsSize() {
            return rows.Count;
        }

        /// <summary>Adds a new body row.</summary>
        public virtual void NewRow() {
            if (rows == null) {
                rows = new List<IList<TableWrapper.CellWrapper>>();
            }
            rowShift.NewRow();
            rows.Add(new List<TableWrapper.CellWrapper>());
        }

        /// <summary>Adds a new header row.</summary>
        public virtual void NewHeaderRow() {
            if (headerRows == null) {
                headerRows = new List<IList<TableWrapper.CellWrapper>>();
            }
            headerRowShift.NewRow();
            headerRows.Add(new List<TableWrapper.CellWrapper>());
        }

        /// <summary>Adds a new footer row.</summary>
        public virtual void NewFooterRow() {
            if (footerRows == null) {
                footerRows = new List<IList<TableWrapper.CellWrapper>>();
            }
            footerRowShift.NewRow();
            footerRows.Add(new List<TableWrapper.CellWrapper>());
        }

        /// <summary>Adds a new cell to the header rows.</summary>
        /// <param name="cell">the cell</param>
        public virtual void AddHeaderCell(Cell cell) {
            if (headerRows == null) {
                headerRows = new List<IList<TableWrapper.CellWrapper>>();
            }
            if (headerRows.Count == 0) {
                NewHeaderRow();
            }
            AddCellToTable(cell, headerRows, headerRowShift);
        }

        /// <summary>Adds a new cell to the footer rows.</summary>
        /// <param name="cell">the cell</param>
        public virtual void AddFooterCell(Cell cell) {
            if (footerRows == null) {
                footerRows = new List<IList<TableWrapper.CellWrapper>>();
            }
            if (footerRows.Count == 0) {
                NewFooterRow();
            }
            AddCellToTable(cell, footerRows, footerRowShift);
        }

        /// <summary>Adds a new cell to the body rows.</summary>
        /// <param name="cell">the cell</param>
        public virtual void AddCell(Cell cell) {
            if (rows == null) {
                rows = new List<IList<TableWrapper.CellWrapper>>();
            }
            if (rows.Count == 0) {
                NewRow();
            }
            AddCellToTable(cell, rows, rowShift);
        }

        public virtual void SetLang(String lang) {
            this.lang = lang;
        }

        public virtual void SetFooterLang(String footerLang) {
            this.footerLang = footerLang;
        }

        public virtual void SetHeaderLang(String headerLang) {
            this.headerLang = headerLang;
        }

        /// <summary>Adds a cell to a table.</summary>
        /// <param name="cell">the cell</param>
        /// <param name="table">the table</param>
        /// <param name="tableRowShift">the applicable table row shift (current col / row position).</param>
        private void AddCellToTable(Cell cell, IList<IList<TableWrapper.CellWrapper>> table, RowColHelper tableRowShift
            ) {
            int col = tableRowShift.MoveToNextEmptyCol();
            tableRowShift.UpdateCurrentPosition(cell.GetColspan(), cell.GetRowspan());
            IList<TableWrapper.CellWrapper> currentRow = table[table.Count - 1];
            currentRow.Add(new TableWrapper.CellWrapper(col, cell));
            numberOfColumns = Math.Max(numberOfColumns, col + cell.GetColspan());
        }

        /// <summary>Sets the table's caption.</summary>
        /// <param name="caption">the caption to be set</param>
        public virtual void SetCaption(Div caption) {
            this.caption = caption;
        }

        /// <summary>
        /// Renders all the rows to a
        /// <see cref="iText.Layout.Element.Table"/>
        /// object.
        /// </summary>
        /// <param name="colgroupsHelper">the colgroups helper class</param>
        /// <returns>the table</returns>
        public virtual Table ToTable(WaitingColgroupsHelper colgroupsHelper) {
            Table table;
            if (numberOfColumns > 0) {
                table = new Table(GetColWidths(colgroupsHelper));
            }
            else {
                // if table is empty, create empty table with single column
                table = new Table(1);
            }
            AccessiblePropHelper.TrySetLangAttribute(table, lang);
            if (headerRows != null) {
                for (int i = 0; i < headerRows.Count; i++) {
                    if (isRtl) {
                        JavaCollectionsUtil.Reverse(headerRows[i]);
                    }
                    for (int j = 0; j < headerRows[i].Count; j++) {
                        Cell cell = headerRows[i][j].cell;
                        ColWrapper colWrapper = colgroupsHelper.GetColWrapper(j);
                        if (colWrapper != null) {
                            if (headerLang == null && cell.GetAccessibilityProperties().GetLanguage() == null) {
                                if (colWrapper.GetLang() != null) {
                                    cell.GetAccessibilityProperties().SetLanguage(colWrapper.GetLang());
                                }
                            }
                        }
                        table.AddHeaderCell(cell);
                    }
                    if (i != headerRows.Count - 1) {
                        table.GetHeader().StartNewRow();
                    }
                }
                AccessiblePropHelper.TrySetLangAttribute(table.GetHeader(), headerLang);
            }
            if (footerRows != null) {
                for (int i = 0; i < footerRows.Count; i++) {
                    if (isRtl) {
                        JavaCollectionsUtil.Reverse(footerRows[i]);
                    }
                    for (int j = 0; j < footerRows[i].Count; j++) {
                        Cell cell = footerRows[i][j].cell;
                        ColWrapper colWrapper = colgroupsHelper.GetColWrapper(j);
                        if (colWrapper != null) {
                            if (footerLang == null && cell.GetAccessibilityProperties().GetLanguage() == null) {
                                if (colWrapper.GetLang() != null) {
                                    cell.GetAccessibilityProperties().SetLanguage(colWrapper.GetLang());
                                }
                            }
                        }
                        table.AddFooterCell(cell);
                    }
                    if (i != footerRows.Count - 1) {
                        table.GetFooter().StartNewRow();
                    }
                }
                AccessiblePropHelper.TrySetLangAttribute(table.GetFooter(), footerLang);
            }
            if (rows != null) {
                for (int i = 0; i < rows.Count; i++) {
                    if (isRtl) {
                        JavaCollectionsUtil.Reverse(rows[i]);
                    }
                    for (int j = 0; j < rows[i].Count; j++) {
                        table.AddCell(rows[i][j].cell);
                    }
                    if (i != rows.Count - 1) {
                        table.StartNewRow();
                    }
                }
            }
            if (caption != null) {
                table.SetCaption(caption);
            }
            return table;
        }

        /// <summary>Gets the column widths.</summary>
        /// <param name="colgroups">the colgroups helper class</param>
        /// <returns>the column widths</returns>
        private UnitValue[] GetColWidths(WaitingColgroupsHelper colgroups) {
            UnitValue[] colWidths = new UnitValue[numberOfColumns];
            if (colgroups == null) {
                for (int i = 0; i < numberOfColumns; i++) {
                    colWidths[i] = null;
                }
            }
            else {
                for (int i = 0; i < numberOfColumns; i++) {
                    colWidths[i] = colgroups.GetColWrapper(i) != null ? colgroups.GetColWrapper(i).GetWidth() : null;
                }
            }
            return colWidths;
        }

        /// <summary>
        /// Wrapper for the
        /// <c>td</c>
        /// /
        /// <c>th</c>
        /// element.
        /// </summary>
        private class CellWrapper {
            /// <summary>The column index.</summary>
            internal int col;

            /// <summary>The cell.</summary>
            internal Cell cell;

            /// <summary>
            /// Creates a new
            /// <see cref="CellWrapper"/>
            /// instance.
            /// </summary>
            /// <param name="col">the column index</param>
            /// <param name="cell">the cell</param>
            internal CellWrapper(int col, Cell cell) {
                this.col = col;
                this.cell = cell;
            }
        }
    }
}
