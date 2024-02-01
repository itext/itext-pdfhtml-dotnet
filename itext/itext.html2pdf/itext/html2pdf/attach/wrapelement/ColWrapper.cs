/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
using iText.Layout.Properties;

namespace iText.Html2pdf.Attach.Wrapelement {
    /// <summary>
    /// Wrapper for the
    /// <c>col</c>
    /// element.
    /// </summary>
    public class ColWrapper : IWrapElement {
        /// <summary>The span.</summary>
        private int span;

        /// <summary>The lang attribute value.</summary>
        private String lang;

        /// <summary>The width.</summary>
        private UnitValue width;

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

        /// <summary>
        /// Creates a new
        /// <see cref="ColWrapper"/>
        /// instance.
        /// </summary>
        /// <param name="span">the span</param>
        public ColWrapper(int span) {
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
        /// <see cref="ColWrapper"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.Attach.Wrapelement.ColWrapper SetWidth(UnitValue width) {
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
        /// <see cref="ColWrapper"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.Attach.Wrapelement.ColWrapper SetCellCssProps(IDictionary<String, String> cellCssProps
            ) {
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
        /// <see cref="ColWrapper"/>
        /// instance
        /// </returns>
        public virtual iText.Html2pdf.Attach.Wrapelement.ColWrapper SetOwnCssProps(IDictionary<String, String> ownCssProps
            ) {
            this.ownCssProps = ownCssProps;
            return this;
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
