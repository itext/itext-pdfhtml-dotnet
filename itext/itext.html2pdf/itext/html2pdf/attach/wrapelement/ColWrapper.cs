/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
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
