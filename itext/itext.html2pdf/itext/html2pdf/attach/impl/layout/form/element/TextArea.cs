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
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Attach.Impl.Layout.Form.Renderer;
using iText.Layout.Element;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout.Form.Element {
    /// <summary>
    /// Extension of the
    /// <see cref="FormField{T}"/>
    /// class representing a button so that
    /// a
    /// <see cref="iText.Html2pdf.Attach.Impl.Layout.Form.Renderer.TextAreaRenderer"/>
    /// is used instead of the default renderer for fields.
    /// </summary>
    public class TextArea : FormField<iText.Html2pdf.Attach.Impl.Layout.Form.Element.TextArea>, IPlaceholderable {
        /// <summary>The placeholder paragraph.</summary>
        private Paragraph placeholder;

        /// <summary>
        /// Creates a new
        /// <see cref="TextArea"/>
        /// instance.
        /// </summary>
        /// <param name="id">the id</param>
        public TextArea(String id)
            : base(id) {
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.layout.form.element.FormField#getDefaultProperty(int)
        */
        public override T1 GetDefaultProperty<T1>(int property) {
            switch (property) {
                case Html2PdfProperty.FORM_FIELD_ROWS: {
                    return (T1)(Object)2;
                }

                case Html2PdfProperty.FORM_FIELD_COLS: {
                    return (T1)(Object)20;
                }

                default: {
                    return base.GetDefaultProperty<T1>(property);
                }
            }
        }

        /// <summary><inheritDoc/></summary>
        public virtual Paragraph GetPlaceholder() {
            return placeholder;
        }

        /// <summary><inheritDoc/></summary>
        public virtual void SetPlaceholder(Paragraph placeholder) {
            this.placeholder = placeholder;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.layout.element.AbstractElement#makeNewRenderer()
        */
        protected override IRenderer MakeNewRenderer() {
            return new TextAreaRenderer(this);
        }
    }
}
