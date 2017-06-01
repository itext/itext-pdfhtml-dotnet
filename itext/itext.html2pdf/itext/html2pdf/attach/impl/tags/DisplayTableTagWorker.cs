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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html.Node;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Attach.Impl.Tags {
    public class DisplayTableTagWorker : ITagWorker {
        private IList<IList<Cell>> columns = new List<IList<Cell>>();

        private IPropertyContainer table;

        private WaitingInlineElementsHelper inlineHelper;

        public DisplayTableTagWorker(IElementNode element, ProcessorContext context) {
            columns.Add(new List<Cell>());
            inlineHelper = new WaitingInlineElementsHelper(element.GetStyles().Get(CssConstants.WHITE_SPACE), element.
                GetStyles().Get(CssConstants.TEXT_TRANSFORM));
        }

        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            FlushInlineElements();
            int maxRowSize = 0;
            for (int i = 0; i < columns.Count; i++) {
                maxRowSize = Math.Max(maxRowSize, columns[i].Count);
            }
            if (maxRowSize == 0) {
                // Workaround because there are problems with empty table
                table = new Div();
            }
            else {
                float[] columnsWidths = new float[columns.Count];
                for (int i = 0; i < columnsWidths.Length; i++) {
                    columnsWidths[i] = -1;
                }
                table = new Table(UnitValue.CreatePointArray(columnsWidths));
                for (int i = 0; i < maxRowSize; i++) {
                    for (int j = 0; j < columns.Count; j++) {
                        Cell cell = i < columns[j].Count ? columns[j][i] : null;
                        if (cell == null) {
                            cell = CreateWrapperCell();
                        }
                        ((Table)table).AddCell(cell);
                    }
                }
            }
        }

        public virtual bool ProcessContent(String content, ProcessorContext context) {
            inlineHelper.Add(content);
            return true;
        }

        public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            bool displayTableCell = childTagWorker is IDisplayAware && CssConstants.TABLE_CELL.Equals(((IDisplayAware)
                childTagWorker).GetDisplay());
            if (childTagWorker.GetElementResult() is IBlockElement) {
                IBlockElement childResult = (IBlockElement)childTagWorker.GetElementResult();
                Cell curCell = childResult is Cell ? (Cell)childResult : CreateWrapperCell().Add(childResult);
                ProcessCell(curCell, displayTableCell);
                return true;
            }
            else {
                if (childTagWorker.GetElementResult() is ILeafElement) {
                    inlineHelper.Add((ILeafElement)childTagWorker.GetElementResult());
                    return true;
                }
                else {
                    if (childTagWorker is SpanTagWorker) {
                        if (displayTableCell) {
                            FlushInlineElements();
                        }
                        bool allChildrenProcessed = true;
                        foreach (IPropertyContainer propertyContainer in ((SpanTagWorker)childTagWorker).GetAllElements()) {
                            if (propertyContainer is ILeafElement) {
                                inlineHelper.Add((ILeafElement)propertyContainer);
                            }
                            else {
                                allChildrenProcessed = false;
                            }
                        }
                        if (displayTableCell) {
                            Cell cell = CreateWrapperCell();
                            inlineHelper.FlushHangingLeaves(cell);
                            ProcessCell(cell, displayTableCell);
                        }
                        return allChildrenProcessed;
                    }
                }
            }
            return false;
        }

        public virtual IPropertyContainer GetElementResult() {
            return table;
        }

        private void ProcessCell(Cell cell, bool displayTableCell) {
            FlushInlineElements();
            if (displayTableCell) {
                IList<Cell> curCol = new List<Cell>();
                curCol.Add(cell);
                if (columns[columns.Count - 1].Count == 0) {
                    columns.JRemoveAt(columns.Count - 1);
                }
                columns.Add(curCol);
                columns.Add(new List<Cell>());
            }
            else {
                columns[columns.Count - 1].Add(cell);
            }
        }

        private void FlushInlineElements() {
            if (inlineHelper.GetSanitizedWaitingLeaves().Count > 0) {
                Cell waitingLavesCell = CreateWrapperCell();
                inlineHelper.FlushHangingLeaves(waitingLavesCell);
                ProcessCell(waitingLavesCell, false);
            }
        }

        private Cell CreateWrapperCell() {
            return new Cell().SetBorder(Border.NO_BORDER).SetPadding(0);
        }
    }
}
