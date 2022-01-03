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
using iText.Html2pdf.Attach.Wrapelement;
using iText.Html2pdf.Html;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Util {
    /// <summary>Helper class for waiting column groups.</summary>
    public class WaitingColgroupsHelper {
        /// <summary>The table element.</summary>
        private IElementNode tableElement;

        /// <summary>The column groups.</summary>
        private List<ColgroupWrapper> colgroups = new List<ColgroupWrapper>();

        /// <summary>The maximum value of the index.</summary>
        private int maxIndex = -1;

        /// <summary>The index to column group mapping.</summary>
        private int[] indexToColgroupMapping;

        /// <summary>The shift values for the columns.</summary>
        private int[] shiftCol;

        /// <summary>
        /// Creates a new
        /// <see cref="WaitingColgroupsHelper"/>
        /// instance.
        /// </summary>
        /// <param name="tableElement">the table element</param>
        public WaitingColgroupsHelper(IElementNode tableElement) {
            this.tableElement = tableElement;
        }

        /// <summary>Adds a column group.</summary>
        /// <param name="colgroup">the column group</param>
        public virtual void Add(ColgroupWrapper colgroup) {
            colgroups.Add(colgroup);
        }

        /// <summary>Applies column styles.</summary>
        public virtual void ApplyColStyles() {
            if (colgroups.IsEmpty() || maxIndex != -1) {
                return;
            }
            FinalizeColgroups();
            RowColHelper tableRowColHelper = new RowColHelper();
            RowColHelper headerRowColHelper = new RowColHelper();
            RowColHelper footerRowColHelper = new RowColHelper();
            IElementNode element;
            foreach (INode child in tableElement.ChildNodes()) {
                if (child is IElementNode) {
                    element = (IElementNode)child;
                    if (TagConstants.THEAD.Equals(element.Name())) {
                        ApplyColStyles(element, headerRowColHelper);
                    }
                    else {
                        if (TagConstants.TFOOT.Equals(element.Name())) {
                            ApplyColStyles(element, footerRowColHelper);
                        }
                        else {
                            ApplyColStyles(element, tableRowColHelper);
                        }
                    }
                }
            }
        }

        /// <summary>Gets a specific column.</summary>
        /// <param name="index">the index of the column</param>
        /// <returns>the column</returns>
        public virtual ColWrapper GetColWrapper(int index) {
            if (index > maxIndex) {
                return null;
            }
            return colgroups[indexToColgroupMapping[index]].GetColumnByIndex(index - shiftCol[indexToColgroupMapping[index
                ]]);
        }

        /// <summary>Applies column styles.</summary>
        /// <param name="node">the node</param>
        /// <param name="rowColHelper">the helper class to keep track of the position inside the table</param>
        private void ApplyColStyles(INode node, RowColHelper rowColHelper) {
            int col;
            IElementNode element;
            foreach (INode child in node.ChildNodes()) {
                if (child is IElementNode) {
                    element = (IElementNode)child;
                    if (TagConstants.TR.Equals(element.Name())) {
                        ApplyColStyles(element, rowColHelper);
                        rowColHelper.NewRow();
                    }
                    else {
                        if (TagConstants.TH.Equals(element.Name()) || TagConstants.TD.Equals(element.Name())) {
                            int? colspan = CssDimensionParsingUtils.ParseInteger(element.GetAttribute(AttributeConstants.COLSPAN));
                            int? rowspan = CssDimensionParsingUtils.ParseInteger(element.GetAttribute(AttributeConstants.ROWSPAN));
                            colspan = colspan != null ? colspan : 1;
                            rowspan = rowspan != null ? rowspan : 1;
                            col = rowColHelper.MoveToNextEmptyCol();
                            if (GetColWrapper(col) != null) {
                                ColWrapper colWrapper = GetColWrapper(col);
                                if (colWrapper.GetCellCssProps() != null) {
                                    element.AddAdditionalHtmlStyles(colWrapper.GetCellCssProps());
                                }
                                String elemLang = element.GetAttribute(AttributeConstants.LANG);
                                String trLang = null;
                                if (node is IElementNode) {
                                    trLang = ((IElementNode)node).GetAttribute(AttributeConstants.LANG);
                                }
                                if (trLang == null && colWrapper.GetLang() != null && elemLang == null) {
                                    element.GetAttributes().SetAttribute(AttributeConstants.LANG, colWrapper.GetLang());
                                }
                            }
                            rowColHelper.UpdateCurrentPosition((int)colspan, (int)rowspan);
                        }
                        else {
                            ApplyColStyles(child, rowColHelper);
                        }
                    }
                }
            }
        }

        /// <summary>Finalizes the column groups.</summary>
        private void FinalizeColgroups() {
            int shift = 0;
            shiftCol = new int[colgroups.Count];
            for (int i = 0; i < colgroups.Count; ++i) {
                shiftCol[i] = shift;
                shift += colgroups[i].GetSpan();
            }
            maxIndex = shift - 1;
            indexToColgroupMapping = new int[shift];
            for (int i = 0; i < colgroups.Count; ++i) {
                for (int j = 0; j < colgroups[i].GetSpan(); ++j) {
                    indexToColgroupMapping[j + shiftCol[i]] = i;
                }
            }
            colgroups.TrimExcess();
        }
    }
}
