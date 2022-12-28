/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 iText Group NV
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
