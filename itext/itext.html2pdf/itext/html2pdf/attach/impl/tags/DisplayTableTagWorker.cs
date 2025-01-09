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
using System;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Attach.Wrapelement;
using iText.Html2pdf.Css;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>TagWorker class for a table element.</summary>
    public class DisplayTableTagWorker : ITagWorker {
        /// <summary>The table.</summary>
        private Table table;

        /// <summary>The table wrapper.</summary>
        private TableWrapper tableWrapper = new TableWrapper();

        /// <summary>The helper class for waiting inline elements.</summary>
        private WaitingInlineElementsHelper inlineHelper;

        /// <summary>The cell waiting for flushing.</summary>
        private Cell waitingCell = null;

        /// <summary>The flag which indicates whether.</summary>
        private bool currentRowIsFinished = false;

        /// <summary>
        /// Creates a new
        /// <see cref="DisplayTableTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public DisplayTableTagWorker(IElementNode element, ProcessorContext context) {
            inlineHelper = new WaitingInlineElementsHelper(element.GetStyles().Get(CssConstants.WHITE_SPACE), element.
                GetStyles().Get(CssConstants.TEXT_TRANSFORM));
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            FlushWaitingCell();
            table = tableWrapper.ToTable(null);
            AccessiblePropHelper.TrySetLangAttribute(table, element);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processContent(java.lang.String, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual bool ProcessContent(String content, ProcessorContext context) {
            inlineHelper.Add(content);
            return true;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processTagChild(com.itextpdf.html2pdf.attach.ITagWorker, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            bool displayTableCell = childTagWorker is IDisplayAware && CssConstants.TABLE_CELL.Equals(((IDisplayAware)
                childTagWorker).GetDisplay());
            if (currentRowIsFinished) {
                tableWrapper.NewRow();
            }
            if (childTagWorker is DisplayTableRowTagWorker) {
                FlushWaitingCell();
                if (!currentRowIsFinished) {
                    tableWrapper.NewRow();
                }
                TableRowWrapper wrapper = ((DisplayTableRowTagWorker)childTagWorker).GetTableRowWrapper();
                foreach (Cell cell in wrapper.GetCells()) {
                    tableWrapper.AddCell(cell);
                }
                currentRowIsFinished = true;
                return true;
            }
            else {
                if (childTagWorker.GetElementResult() is IBlockElement) {
                    IBlockElement childResult = (IBlockElement)childTagWorker.GetElementResult();
                    Cell curCell = childResult is Cell ? (Cell)childResult : CreateWrapperCell().Add(childResult);
                    ProcessCell(curCell, displayTableCell);
                    currentRowIsFinished = false;
                    return true;
                }
                else {
                    if (childTagWorker.GetElementResult() is ILeafElement) {
                        inlineHelper.Add((ILeafElement)childTagWorker.GetElementResult());
                        currentRowIsFinished = false;
                        return true;
                    }
                    else {
                        if (childTagWorker is SpanTagWorker) {
                            // the previous one
                            if (displayTableCell) {
                                FlushWaitingCell();
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
                            // the current one
                            if (displayTableCell) {
                                FlushWaitingCell();
                            }
                            currentRowIsFinished = false;
                            return allChildrenProcessed;
                        }
                    }
                }
            }
            return false;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#getElementResult()
        */
        public virtual IPropertyContainer GetElementResult() {
            return table;
        }

        /// <summary>Processes a cell.</summary>
        /// <param name="cell">the cell</param>
        private void ProcessCell(Cell cell, bool displayTableCell) {
            if (displayTableCell) {
                if (waitingCell != cell) {
                    FlushWaitingCell();
                    tableWrapper.AddCell(cell);
                }
                else {
                    if (!cell.IsEmpty()) {
                        tableWrapper.AddCell(cell);
                        waitingCell = null;
                    }
                }
            }
            else {
                FlushInlineElementsToWaitingCell();
                waitingCell.Add(cell);
            }
        }

        /// <summary>Flushes inline elements to the waiting cell.</summary>
        private void FlushInlineElementsToWaitingCell() {
            if (null == waitingCell) {
                waitingCell = CreateWrapperCell();
            }
            inlineHelper.FlushHangingLeaves(waitingCell);
        }

        /// <summary>Flushes the waiting cell.</summary>
        private void FlushWaitingCell() {
            FlushInlineElementsToWaitingCell();
            if (null != waitingCell) {
                ProcessCell(waitingCell, true);
            }
        }

        /// <summary>Creates a wrapper cell.</summary>
        /// <returns>the cell</returns>
        private Cell CreateWrapperCell() {
            return new Cell().SetBorder(Border.NO_BORDER).SetPadding(0);
        }
    }
}
