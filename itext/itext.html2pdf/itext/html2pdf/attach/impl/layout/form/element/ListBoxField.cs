using System;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Attach.Impl.Layout.Form.Renderer;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout.Form.Element {
    /// <summary>A field that represents a control for selecting one or several of the provided options.</summary>
    public class ListBoxField : AbstractSelectField {
        /// <summary>Creates a new list box field.</summary>
        /// <param name="size">the size of the list box, which will define the height of visible properties, shall be greater than zero
        ///     </param>
        /// <param name="allowMultipleSelection">a boolean flag that defines whether multiple options are allowed to be selected at once
        ///     </param>
        /// <param name="id">the id</param>
        public ListBoxField(String id, int size, bool allowMultipleSelection)
            : base(id) {
            SetProperty(Html2PdfProperty.FORM_FIELD_SIZE, size);
            SetProperty(Html2PdfProperty.FORM_FIELD_MULTIPLE, allowMultipleSelection);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.layout.form.element.FormField#getDefaultProperty(int)
        */
        public override T1 GetDefaultProperty<T1>(int property) {
            switch (property) {
                case Html2PdfProperty.FORM_FIELD_MULTIPLE: {
                    return (T1)(Object)false;
                }

                case Html2PdfProperty.FORM_FIELD_SIZE: {
                    return (T1)(Object)4;
                }

                default: {
                    return base.GetDefaultProperty<T1>(property);
                }
            }
        }

        protected override IRenderer MakeNewRenderer() {
            return new SelectFieldListBoxRenderer(this);
        }
    }
}
