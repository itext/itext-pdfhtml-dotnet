using System;
using iText.Html2pdf.Attach.Impl.Layout.Form.Renderer;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout.Form.Element {
    /// <summary>A field that represents a control for selecting one of the provided options.</summary>
    public class ComboBoxField : AbstractSelectField {
        /// <summary>Creates a new select field box.</summary>
        /// <param name="id">the id</param>
        public ComboBoxField(String id)
            : base(id) {
        }

        protected override IRenderer MakeNewRenderer() {
            return new SelectFieldComboBoxRenderer(this);
        }
    }
}
