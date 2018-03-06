using System;
using iText.Html2pdf.Attach.Impl.Layout.Form.Renderer;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout.Form.Element {
    /// <summary>
    /// Extension of the
    /// <see cref="FormField{T}"/>
    /// class representing a button in html
    /// </summary>
    [System.ObsoleteAttribute(@"Will be renamed to Button in next major release")]
    public class ButtonContainer : FormField<iText.Html2pdf.Attach.Impl.Layout.Form.Element.ButtonContainer> {
        public ButtonContainer(String id)
            : base(id) {
        }

        protected override IRenderer MakeNewRenderer() {
            return new ButtonContainerRenderer(this);
        }

        /// <summary>Adds any block element to the div's contents.</summary>
        /// <param name="element">
        /// a
        /// <see cref="iText.Layout.Element.BlockElement{T}"/>
        /// </param>
        /// <returns>this Element</returns>
        public virtual iText.Html2pdf.Attach.Impl.Layout.Form.Element.ButtonContainer Add(IBlockElement element) {
            childElements.Add(element);
            return this;
        }

        /// <summary>Adds an image to the div's contents.</summary>
        /// <param name="element">
        /// an
        /// <see cref="iText.Layout.Element.Image"/>
        /// </param>
        /// <returns>this Element</returns>
        public virtual iText.Html2pdf.Attach.Impl.Layout.Form.Element.ButtonContainer Add(Image element) {
            childElements.Add(element);
            return this;
        }

        public override T1 GetDefaultProperty<T1>(int property) {
            if (property == Property.KEEP_TOGETHER) {
                return (T1)(Object)true;
            }
            return base.GetDefaultProperty<T1>(property);
        }
    }
}
