using System.Collections.Generic;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Layout.Element;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout.Form.Renderer {
    /// <summary>
    /// The
    /// <see cref="SelectFieldListBoxRenderer"/>
    /// implementation for select field renderer.
    /// </summary>
    public class SelectFieldListBoxRenderer : AbstractSelectFieldRenderer {
        /// <summary>
        /// Creates a new
        /// <see cref="SelectFieldListBoxRenderer"/>
        /// instance.
        /// </summary>
        /// <param name="modelElement">the model element</param>
        public SelectFieldListBoxRenderer(AbstractSelectField modelElement)
            : base(modelElement) {
        }

        public override IRenderer GetNextRenderer() {
            return new iText.Html2pdf.Attach.Impl.Layout.Form.Renderer.SelectFieldListBoxRenderer((AbstractSelectField
                )modelElement);
        }

        public override LayoutResult Layout(LayoutContext layoutContext) {
            LayoutResult layoutResult = base.Layout(layoutContext);
            // options container is the only kid of the select field renderer by design
            IRenderer optionsContainer = childRenderers.Count == 1 ? childRenderers[0] : null;
            if (!IsFlatten() || layoutResult.GetStatus() != LayoutResult.FULL || optionsContainer == null || optionsContainer
                .GetOccupiedArea() == null) {
                return layoutResult;
            }
            if (IsOverflowProperty(OverflowPropertyValue.HIDDEN, this, Property.OVERFLOW_Y)) {
                IList<IRenderer> selectedOptions = GetSelectedOptions(this);
                IRenderer firstSelectedOption;
                if (!selectedOptions.IsEmpty() && (firstSelectedOption = selectedOptions[0]).GetOccupiedArea() != null) {
                    Rectangle borderAreaBBox = GetBorderAreaBBox();
                    Rectangle optionBBox = firstSelectedOption.GetOccupiedArea().GetBBox().Clone();
                    if (firstSelectedOption is AbstractRenderer) {
                        ((AbstractRenderer)firstSelectedOption).ApplyMargins(optionBBox, false);
                    }
                    if (optionBBox.GetHeight() < borderAreaBBox.GetHeight()) {
                        float selectedBottom = optionBBox.GetBottom();
                        float borderAreaBBoxBottom = borderAreaBBox.GetBottom();
                        if (selectedBottom < borderAreaBBoxBottom) {
                            optionsContainer.Move(0, borderAreaBBoxBottom - selectedBottom);
                        }
                    }
                    else {
                        optionsContainer.Move(0, borderAreaBBox.GetTop() - optionBBox.GetTop());
                    }
                }
            }
            return layoutResult;
        }

        protected override bool AllowLastYLineRecursiveExtraction() {
            return false;
        }

        protected internal override IRenderer CreateFlatRenderer() {
            AbstractSelectField selectField = (AbstractSelectField)modelElement;
            IList<IBlockElement> options = selectField.GetOptions();
            Div optionsContainer = new Div();
            foreach (IBlockElement option in options) {
                optionsContainer.Add(option);
            }
            IRenderer rendererSubTree;
            if (optionsContainer.GetChildren().IsEmpty()) {
                Paragraph pStub = new Paragraph("\u00A0").SetMargin(0);
                pStub.SetProperty(Property.OVERFLOW_X, OverflowPropertyValue.VISIBLE);
                pStub.SetProperty(Property.OVERFLOW_Y, OverflowPropertyValue.VISIBLE);
                pStub.SetProperty(Html2PdfProperty.FORM_FIELD_SELECTED, false);
                // applying this property for the sake of finding this element as option
                optionsContainer.Add(pStub);
                rendererSubTree = optionsContainer.CreateRendererSubTree();
            }
            else {
                rendererSubTree = optionsContainer.CreateRendererSubTree();
                IList<IRenderer> selectedOptions = GetSelectedOptions(rendererSubTree);
                foreach (IRenderer selectedOption in selectedOptions) {
                    ApplySelectedStyle(selectedOption);
                }
            }
            return rendererSubTree;
        }

        protected internal override float GetFinalSelectFieldHeight(float availableHeight, float actualHeight, bool
             isClippedHeight) {
            float? height = RetrieveHeight();
            float calculatedHeight;
            if (height == null) {
                calculatedHeight = GetCalculatedHeight(this);
                float? maxHeight = RetrieveMaxHeight();
                if (maxHeight != null && maxHeight < calculatedHeight) {
                    calculatedHeight = (float)maxHeight;
                }
                float? minHeight = RetrieveMinHeight();
                if (minHeight != null && minHeight > calculatedHeight) {
                    calculatedHeight = (float)minHeight;
                }
            }
            else {
                calculatedHeight = actualHeight;
            }
            return base.GetFinalSelectFieldHeight(availableHeight, calculatedHeight, isClippedHeight);
        }

        protected internal override void ApplyAcroField(DrawContext drawContext) {
        }

        // TODO DEVSIX-1901
        private float GetCalculatedHeight(IRenderer flatRenderer) {
            int? sizeProp = this.GetProperty<int?>(Html2PdfProperty.FORM_FIELD_SIZE);
            int size;
            if (sizeProp == null || sizeProp <= 0) {
                // Ensure height will not be negative or zero.
                // There is no particular reason for setting specifically 4.
                size = 4;
            }
            else {
                size = (int)sizeProp;
            }
            float maxOptionActualHeight = GetMaxOptionActualHeight(flatRenderer);
            if (maxOptionActualHeight == float.MinValue) {
                UnitValue fontSize = flatRenderer.GetProperty<UnitValue>(Property.FONT_SIZE);
                if (fontSize != null && fontSize.IsPointValue()) {
                    maxOptionActualHeight = fontSize.GetValue() * 1.2f;
                }
                else {
                    // according to default styles for options (min-height: 1.2em)
                    maxOptionActualHeight = 0;
                }
            }
            return size * maxOptionActualHeight;
        }

        private float GetMaxOptionActualHeight(IRenderer flatRenderer) {
            float maxActualHeight = float.MinValue;
            foreach (IRenderer child in flatRenderer.GetChildRenderers()) {
                if (IsOptionRenderer(child)) {
                    float childHeight;
                    if (child is AbstractRenderer) {
                        AbstractRenderer abstractChild = (AbstractRenderer)child;
                        childHeight = abstractChild.ApplyMargins(abstractChild.GetOccupiedAreaBBox(), false).GetHeight();
                    }
                    else {
                        childHeight = child.GetOccupiedArea().GetBBox().GetHeight();
                    }
                    if (childHeight > maxActualHeight) {
                        maxActualHeight = childHeight;
                    }
                }
                else {
                    float maxNestedHeight = GetMaxOptionActualHeight(child);
                    if (maxNestedHeight > maxActualHeight) {
                        maxActualHeight = maxNestedHeight;
                    }
                }
            }
            return maxActualHeight;
        }

        private IList<IRenderer> GetSelectedOptions(IRenderer rendererSubTree) {
            IList<IRenderer> selectedOptions = new List<IRenderer>();
            IList<IRenderer> optionsWhichMarkedSelected = GetOptionsMarkedSelected(rendererSubTree);
            if (!optionsWhichMarkedSelected.IsEmpty()) {
                if (IsMultiple()) {
                    selectedOptions.AddAll(optionsWhichMarkedSelected);
                }
                else {
                    selectedOptions.Add(optionsWhichMarkedSelected[optionsWhichMarkedSelected.Count - 1]);
                }
            }
            return selectedOptions;
        }

        private bool IsMultiple() {
            bool? propertyAsBoolean = GetPropertyAsBoolean(Html2PdfProperty.FORM_FIELD_MULTIPLE);
            return propertyAsBoolean != null && (bool)propertyAsBoolean;
        }

        private void ApplySelectedStyle(IRenderer selectedOption) {
            selectedOption.SetProperty(Property.BACKGROUND, new Background(new DeviceRgb(0, 120, 215)));
            SetFontColorRecursively(selectedOption);
        }

        /// <summary>
        /// The `select` tag has default color css property, therefore it makes sense to explicitly override this property to all children,
        /// otherwise it will be not applied due to the css resolving mechanism.
        /// </summary>
        private void SetFontColorRecursively(IRenderer selectedOption) {
            selectedOption.SetProperty(Property.FONT_COLOR, new TransparentColor(ColorConstants.WHITE));
            foreach (IRenderer renderer in selectedOption.GetChildRenderers()) {
                SetFontColorRecursively(renderer);
            }
        }
    }
}
