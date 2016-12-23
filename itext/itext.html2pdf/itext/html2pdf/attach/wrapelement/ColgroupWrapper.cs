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
using iText.IO.Util;
using iText.Layout.Properties;

namespace iText.Html2pdf.Attach.Wrapelement {
    public class ColgroupWrapper : IWrapElement {
        private int span;

        private UnitValue width;

        private int[] indexToColMapping;

        private IDictionary<String, String> cellCssProps;

        private IDictionary<String, String> ownCssProps;

        private IList<ColWrapper> columns = new List<ColWrapper>();

        public ColgroupWrapper(int span) {
            //Those properties should be inherited from <colgroup> to <col> and are eventually applied to <td> or <th>
            //Those properties shouldn't be applied to <td> or <th>private Map<String, String> ownCssProps;
            this.span = span;
        }

        public virtual int GetSpan() {
            return span;
        }

        public virtual UnitValue GetWidth() {
            return width;
        }

        public virtual iText.Html2pdf.Attach.Wrapelement.ColgroupWrapper SetWidth(UnitValue width) {
            this.width = width;
            return this;
        }

        public virtual IDictionary<String, String> GetCellCssProps() {
            return cellCssProps;
        }

        public virtual iText.Html2pdf.Attach.Wrapelement.ColgroupWrapper SetCellCssProps(IDictionary<String, String
            > cellCssProps) {
            this.cellCssProps = cellCssProps;
            return this;
        }

        public virtual IDictionary<String, String> GetOwnCssProps() {
            return ownCssProps;
        }

        public virtual iText.Html2pdf.Attach.Wrapelement.ColgroupWrapper SetOwnCssProps(IDictionary<String, String
            > ownCssProps) {
            this.ownCssProps = ownCssProps;
            return this;
        }

        public virtual IList<ColWrapper> GetColumns() {
            return columns;
        }

        public virtual iText.Html2pdf.Attach.Wrapelement.ColgroupWrapper FinalizeCols() {
            if (indexToColMapping != null) {
                return this;
            }
            if (columns.IsEmpty()) {
                columns.Add(new ColWrapper(span).SetCellCssProps(cellCssProps).SetWidth(width));
            }
            else {
                if (cellCssProps != null) {
                    foreach (ColWrapper col in columns) {
                        IDictionary<String, String> colStyles = new Dictionary<String, String>(cellCssProps);
                        if (col.GetCellCssProps() != null) {
                            colStyles.AddAll(col.GetCellCssProps());
                        }
                        if (colStyles.Count > 0) {
                            col.SetCellCssProps(colStyles);
                        }
                        if (col.GetWidth() == null) {
                            col.SetWidth(width);
                        }
                    }
                }
            }
            columns = JavaCollectionsUtil.UnmodifiableList(columns);
            int ncol = 0;
            foreach (ColWrapper col_1 in columns) {
                ncol += col_1.GetSpan();
            }
            indexToColMapping = new int[ncol];
            int shift = 0;
            for (int i = 0; i < columns.Count; ++i) {
                int span = columns[i].GetSpan();
                for (int j = 0; j < span; ++j) {
                    indexToColMapping[shift + j] = i;
                }
                shift += span;
            }
            span = shift;
            return this;
        }

        public virtual ColWrapper GetColumnByIndex(int index) {
            return columns[indexToColMapping[index]];
        }
    }
}
