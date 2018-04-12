using System;
using iText.Forms;
using iText.Forms.Fields;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Annot;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout.Form.Renderer {
    /// <summary>
    /// The
    /// <see cref="AbstractOneLineTextFieldRenderer"/>
    /// implementation for buttons with kids.
    /// </summary>
    [System.ObsoleteAttribute(@"Will be renamed to ButtonRenderer in next major release.")]
    public class ButtonContainerRenderer : BlockRenderer {
        private const float DEFAULT_FONT_SIZE = 12f;

        public ButtonContainerRenderer(ButtonContainer modelElement)
            : base(modelElement) {
        }

        public override void Draw(DrawContext drawContext) {
            base.Draw(drawContext);
            if (!IsFlatten()) {
                String value = GetDefaultValue();
                String name = GetModelId();
                UnitValue fontSize = (UnitValue)this.GetPropertyAsUnitValue(Property.FONT_SIZE);
                if (!fontSize.IsPointValue()) {
                    fontSize = UnitValue.CreatePointValue(DEFAULT_FONT_SIZE);
                }
                PdfDocument doc = drawContext.GetDocument();
                Rectangle area = GetOccupiedArea().GetBBox().Clone();
                ApplyMargins(area, false);
                PdfPage page = doc.GetPage(occupiedArea.GetPageNumber());
                PdfButtonFormField button = PdfFormField.CreatePushButton(doc, area, name, value, doc.GetDefaultFont(), fontSize
                    .GetValue());
                button.GetWidgets()[0].SetHighlightMode(PdfAnnotation.HIGHLIGHT_NONE);
                button.SetBorderWidth(0);
                button.SetBackgroundColor(null);
                TransparentColor color = GetPropertyAsTransparentColor(Property.FONT_COLOR);
                if (color != null) {
                    button.SetColor(color.GetColor());
                }
                PdfAcroForm forms = PdfAcroForm.GetAcroForm(doc, true);
                //Add fields only if it isn't already added. This can happen on split.
                if (forms.GetField(name) == null) {
                    forms.AddField(button, page);
                }
            }
        }

        protected override float? GetLastYLineRecursively() {
            return base.GetFirstYLineRecursively();
        }

        public override IRenderer GetNextRenderer() {
            return new iText.Html2pdf.Attach.Impl.Layout.Form.Renderer.ButtonContainerRenderer((ButtonContainer)modelElement
                );
        }

        //NOTE: Duplicates methods from AbstractFormFieldRenderer should be changed in next major version
        /// <summary>Gets the model id.</summary>
        /// <returns>the model id</returns>
        protected internal virtual String GetModelId() {
            return ((IFormField)GetModelElement()).GetId();
        }

        /// <summary>Checks if form fields need to be flattened.</summary>
        /// <returns>true, if fields need to be flattened</returns>
        public virtual bool IsFlatten() {
            bool? flatten = GetPropertyAsBoolean(Html2PdfProperty.FORM_FIELD_FLATTEN);
            return flatten != null ? (bool)flatten : (bool)modelElement.GetDefaultProperty<bool>(Html2PdfProperty.FORM_FIELD_FLATTEN
                );
        }

        /// <summary>Gets the default value of the form field.</summary>
        /// <returns>the default value of the form field</returns>
        public virtual String GetDefaultValue() {
            String defaultValue = this.GetProperty<String>(Html2PdfProperty.FORM_FIELD_VALUE);
            return defaultValue != null ? defaultValue : modelElement.GetDefaultProperty<String>(Html2PdfProperty.FORM_FIELD_VALUE
                );
        }
    }
}
