/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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
using System.Collections.Generic;
using iText.Commons.Utils;
using iText.Layout.Properties;

namespace iText.Html2pdf.Attach.Wrapelement {
    /// <summary>
    /// Wrapper for the
    /// <c>colgroup</c>
    /// element.
    /// </summary>
    public class ColgroupWrapper : IWrapElement {
        /// <summary>The span.</summary>
        private int span;

        /// <summary>The lang attribute value.</summary>
        private String lang;

        /// <summary>The width.</summary>
        private UnitValue width;

        /// <summary>The index to column mapping.</summary>
        private int[] indexToColMapping;

        /// <summary>The cell CSS properties.</summary>
        /// <remarks>
        /// The cell CSS properties.
        /// These properties should be inherited from &lt;colgroup&gt; to &lt;col&gt;
        /// and are eventually applied to &lt;td&gt; or &lt;th&gt;.
        /// </remarks>
        private IDictionary<String, String> cellCssProps;

        /// <summary>The own CSS properties.</summary>
        /// <remarks>
        /// The own CSS properties.
        /// These properties shouldn't be applied to &lt;td&gt; or &lt;th&gt;.
        /// </remarks>
        private IDictionary<String, String> ownCssProps;

        /// <summary>A list of column wrappers.</summary>
        private IList<ColWrapper> columns = new List<ColWrapper>();

        /// <summary>
        /// Creates a new
        /// <see cref="ColgroupWrapper"/>
        /// instance.
        /// </summary>
        /// <param name="span">the span</param>
        public ColgroupWrapper(int span) {
            this.span = span;
        }

        /// <summary>Gets the span.</summary>
        /// <returns>the span</returns>
        public virtual int GetSpan() {
            return span;
        }

        /// <summary>Gets the width.</summary>
        /// <returns>the width</returns>
        public virtual UnitValue GetWidth() {
            return width;
        }

        /// <summary>Sets the width.</summary>
        /// <param name="width">the width</param>
        /// <returns>
        /// this
        /// <see cref="ColgroupWrapper"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.Attach.Wrapelement.ColgroupWrapper SetWidth(UnitValue width) {
            this.width = width;
            return this;
        }

        /// <summary>Gets the cell CSS properties.</summary>
        /// <returns>the cell CSS properties</returns>
        public virtual IDictionary<String, String> GetCellCssProps() {
            return cellCssProps;
        }

        /// <summary>Sets the cell CSS properties.</summary>
        /// <param name="cellCssProps">the cell CSS properties</param>
        /// <returns>
        /// this
        /// <see cref="ColgroupWrapper"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.Attach.Wrapelement.ColgroupWrapper SetCellCssProps(IDictionary<String, String
            > cellCssProps) {
            this.cellCssProps = cellCssProps;
            return this;
        }

        /// <summary>Gets the own CSS properties.</summary>
        /// <returns>the own CSS properties</returns>
        public virtual IDictionary<String, String> GetOwnCssProps() {
            return ownCssProps;
        }

        /// <summary>Sets the own CSS properties.</summary>
        /// <param name="ownCssProps">the own CSS properties</param>
        /// <returns>
        /// this
        /// <see cref="ColgroupWrapper"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.Attach.Wrapelement.ColgroupWrapper SetOwnCssProps(IDictionary<String, String
            > ownCssProps) {
            this.ownCssProps = ownCssProps;
            return this;
        }

        /// <summary>Gets the columns.</summary>
        /// <returns>the columns</returns>
        public virtual IList<ColWrapper> GetColumns() {
            return columns;
        }

        /// <summary>Finalize the columns.</summary>
        /// <returns>
        /// this
        /// <see cref="ColgroupWrapper"/>
        /// instance
        /// </returns>
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
                if (lang != null) {
                    foreach (ColWrapper col in columns) {
                        if (col.GetLang() == null) {
                            col.SetLang(lang);
                        }
                    }
                }
            }
            columns = JavaCollectionsUtil.UnmodifiableList(columns);
            int ncol = 0;
            foreach (ColWrapper col in columns) {
                ncol += col.GetSpan();
            }
            indexToColMapping = new int[ncol];
            span = 0;
            for (int i = 0; i < columns.Count; ++i) {
                int colSpan = columns[i].GetSpan();
                for (int j = 0; j < colSpan; ++j) {
                    indexToColMapping[span + j] = i;
                }
                span += colSpan;
            }
            return this;
        }

        /// <summary>Gets the column by index.</summary>
        /// <param name="index">the index</param>
        /// <returns>the column corresponding with the index</returns>
        public virtual ColWrapper GetColumnByIndex(int index) {
            return columns[indexToColMapping[index]];
        }

        /// <summary>Sets the language attribute.</summary>
        /// <param name="lang">the lang attribute</param>
        public virtual void SetLang(String lang) {
            this.lang = lang;
        }

        /// <summary>Gets the language attribute.</summary>
        /// <returns>the lang attribute</returns>
        public virtual String GetLang() {
            return lang;
        }
    }
}
