using System;
using System.Collections.Generic;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
using iText.Layout.Element;
using iText.Layout.Minmaxwidth;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout.Form.Renderer {
    /// <summary>
    /// The
    /// <see cref="SelectFieldComboBoxRenderer"/>
    /// implementation for select field renderer.
    /// </summary>
    public class SelectFieldComboBoxRenderer : AbstractSelectFieldRenderer {
        private IRenderer minMaxWidthRenderer;

        /// <summary>
        /// Creates a new
        /// <see cref="SelectFieldComboBoxRenderer"/>
        /// instance.
        /// </summary>
        /// <param name="modelElement">the model element</param>
        public SelectFieldComboBoxRenderer(AbstractSelectField modelElement)
            : base(modelElement) {
            SetProperty(Property.VERTICAL_ALIGNMENT, VerticalAlignment.MIDDLE);
            SetProperty(Property.OVERFLOW_X, OverflowPropertyValue.HIDDEN);
            SetProperty(Property.OVERFLOW_Y, OverflowPropertyValue.HIDDEN);
            minMaxWidthRenderer = CreateFlatRenderer(true);
        }

        public override IRenderer GetNextRenderer() {
            return new iText.Html2pdf.Attach.Impl.Layout.Form.Renderer.SelectFieldComboBoxRenderer((AbstractSelectField
                )modelElement);
        }

        protected override MinMaxWidth GetMinMaxWidth() {
            IList<IRenderer> realChildRenderers = childRenderers;
            childRenderers = new List<IRenderer>();
            childRenderers.Add(minMaxWidthRenderer);
            MinMaxWidth minMaxWidth = base.GetMinMaxWidth();
            childRenderers = realChildRenderers;
            return minMaxWidth;
        }

        protected override bool AllowLastYLineRecursiveExtraction() {
            return true;
        }

        protected internal override IRenderer CreateFlatRenderer() {
            return CreateFlatRenderer(false);
        }

        protected internal override void ApplyAcroField(DrawContext drawContext) {
        }

        // TODO
        private IRenderer CreateFlatRenderer(bool addAllOptionsToChildren) {
            AbstractSelectField selectField = (AbstractSelectField)modelElement;
            IList<IBlockElement> options = selectField.GetOptions();
            Div pseudoContainer = new Div();
            foreach (IBlockElement option in options) {
                pseudoContainer.Add(option);
            }
            IList<Paragraph> allOptions;
            IRenderer pseudoRendererSubTree = pseudoContainer.CreateRendererSubTree();
            if (addAllOptionsToChildren) {
                allOptions = GetAllOptionsFlatElements(pseudoRendererSubTree);
            }
            else {
                allOptions = GetSingleSelectedOptionFlatRenderer(pseudoRendererSubTree);
            }
            if (allOptions.IsEmpty()) {
                allOptions.Add(CreateComboBoxOptionFlatElement());
            }
            pseudoContainer.GetChildren().Clear();
            foreach (Paragraph option in allOptions) {
                pseudoContainer.Add(option);
            }
            IRenderer rendererSubTree = pseudoContainer.CreateRendererSubTree();
            ReplaceParagraphRenderers(rendererSubTree);
            return rendererSubTree;
        }

        private IList<Paragraph> GetSingleSelectedOptionFlatRenderer(IRenderer optionsSubTree) {
            IList<Paragraph> selectedOptionFlatRendererList = new List<Paragraph>();
            IList<IRenderer> selectedOptions = GetOptionsMarkedSelected(optionsSubTree);
            IRenderer selectedOption;
            if (selectedOptions.IsEmpty()) {
                selectedOption = GetFirstOption(optionsSubTree);
            }
            else {
                selectedOption = selectedOptions[selectedOptions.Count - 1];
            }
            if (selectedOption != null) {
                String label = selectedOption.GetProperty<String>(Html2PdfProperty.FORM_FIELD_LABEL);
                selectedOptionFlatRendererList.Add(CreateComboBoxOptionFlatElement(label, false));
            }
            return selectedOptionFlatRendererList;
        }

        private IRenderer GetFirstOption(IRenderer renderer) {
            IRenderer firstOption = null;
            foreach (IRenderer child in renderer.GetChildRenderers()) {
                if (IsOptionRenderer(child)) {
                    firstOption = child;
                    break;
                }
                firstOption = GetFirstOption(child);
                if (firstOption != null) {
                    break;
                }
            }
            return firstOption;
        }

        private IList<Paragraph> GetAllOptionsFlatElements(IRenderer renderer) {
            return GetAllOptionsFlatElements(renderer, false);
        }

        private IList<Paragraph> GetAllOptionsFlatElements(IRenderer renderer, bool isInOptGroup) {
            IList<Paragraph> options = new List<Paragraph>();
            foreach (IRenderer child in renderer.GetChildRenderers()) {
                if (IsOptionRenderer(child)) {
                    String label = child.GetProperty<String>(Html2PdfProperty.FORM_FIELD_LABEL);
                    Paragraph optionFlatElement = CreateComboBoxOptionFlatElement(label, isInOptGroup);
                    options.Add(optionFlatElement);
                }
                else {
                    options.AddAll(GetAllOptionsFlatElements(child, isInOptGroup || IsOptGroupRenderer(child)));
                }
            }
            return options;
        }

        private static Paragraph CreateComboBoxOptionFlatElement() {
            return CreateComboBoxOptionFlatElement(null, false);
        }

        private static Paragraph CreateComboBoxOptionFlatElement(String label, bool simulateOptGroupMargin) {
            Paragraph paragraph = new Paragraph().SetMargin(0);
            if (simulateOptGroupMargin) {
                paragraph.Add("\u200d    ");
            }
            if (label == null || String.IsNullOrEmpty(label)) {
                label = "\u00A0";
            }
            paragraph.Add(label);
            paragraph.SetProperty(Property.OVERFLOW_X, OverflowPropertyValue.VISIBLE);
            paragraph.SetProperty(Property.OVERFLOW_Y, OverflowPropertyValue.VISIBLE);
            // These constants are defined according to values in default.css.
            // At least in Chrome paddings of options in comboboxes cannot be altered through css styles.
            float leftRightPaddingVal = 2 * 0.75f;
            float bottomPaddingVal = 0.75f;
            float topPaddingVal = 0;
            paragraph.SetPaddings(topPaddingVal, leftRightPaddingVal, bottomPaddingVal, leftRightPaddingVal);
            return paragraph;
        }
    }
}
