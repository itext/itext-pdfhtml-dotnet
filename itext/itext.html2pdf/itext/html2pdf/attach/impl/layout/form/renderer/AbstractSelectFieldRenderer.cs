using System;
using System.Collections.Generic;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
using iText.Kernel.Geom;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout.Form.Renderer {
    /// <summary>
    /// Abstract
    /// <see cref="iText.Layout.Renderer.BlockRenderer"/>
    /// for select form fields.
    /// </summary>
    public abstract class AbstractSelectFieldRenderer : BlockRenderer {
        /// <summary>
        /// Creates a new
        /// <see cref="AbstractSelectFieldRenderer"/>
        /// instance.
        /// </summary>
        /// <param name="modelElement">the model element</param>
        public AbstractSelectFieldRenderer(AbstractSelectField modelElement)
            : base(modelElement) {
            AddChild(CreateFlatRenderer());
        }

        public override LayoutResult Layout(LayoutContext layoutContext) {
            // Resolve width here in case it's relative, while parent width is still intact.
            // If it's inline-block context, relative width is already resolved.
            float? width = RetrieveWidth(layoutContext.GetArea().GetBBox().GetWidth());
            if (width != null) {
                UpdateWidth(UnitValue.CreatePointValue((float)width));
            }
            float childrenMaxWidth = GetMinMaxWidth().GetMaxWidth();
            LayoutArea area = layoutContext.GetArea().Clone();
            area.GetBBox().MoveDown(INF - area.GetBBox().GetHeight()).SetHeight(INF).SetWidth(childrenMaxWidth + EPS);
            LayoutResult layoutResult = base.Layout(new LayoutContext(area, layoutContext.GetMarginsCollapseInfo(), layoutContext
                .GetFloatRendererAreas(), layoutContext.IsClippedHeight()));
            if (layoutResult.GetStatus() != LayoutResult.FULL) {
                if (true.Equals(GetPropertyAsBoolean(Property.FORCED_PLACEMENT))) {
                    layoutResult = MakeLayoutResultFull(layoutContext.GetArea(), layoutResult);
                }
                else {
                    return new LayoutResult(LayoutResult.NOTHING, null, null, this, this);
                }
            }
            float availableHeight = layoutContext.GetArea().GetBBox().GetHeight();
            bool isClippedHeight = layoutContext.IsClippedHeight();
            Rectangle dummy = new Rectangle(0, 0);
            ApplyMargins(dummy, true);
            ApplyBorderBox(dummy, true);
            ApplyPaddings(dummy, true);
            float additionalHeight = dummy.GetHeight();
            availableHeight -= additionalHeight;
            availableHeight = Math.Max(availableHeight, 0);
            float actualHeight = GetOccupiedArea().GetBBox().GetHeight() - additionalHeight;
            float finalSelectFieldHeight = GetFinalSelectFieldHeight(availableHeight, actualHeight, isClippedHeight);
            if (finalSelectFieldHeight < 0) {
                return new LayoutResult(LayoutResult.NOTHING, null, null, this, this);
            }
            float delta = finalSelectFieldHeight - actualHeight;
            if (Math.Abs(delta) > EPS) {
                GetOccupiedArea().GetBBox().IncreaseHeight(delta).MoveDown(delta);
            }
            return layoutResult;
        }

        public override void DrawChildren(DrawContext drawContext) {
            if (true) {
                // TODO isFlatten
                base.DrawChildren(drawContext);
            }
            else {
                ApplyAcroField(drawContext);
            }
        }

        protected internal abstract IRenderer CreateFlatRenderer();

        protected internal abstract void ApplyAcroField(DrawContext drawContext);

        protected internal virtual float GetFinalSelectFieldHeight(float availableHeight, float actualHeight, bool
             isClippedHeight) {
            bool isForcedPlacement = true.Equals(GetPropertyAsBoolean(Property.FORCED_PLACEMENT));
            if (!isClippedHeight && actualHeight > availableHeight) {
                if (isForcedPlacement) {
                    return availableHeight;
                }
                return -1;
            }
            return actualHeight;
        }

        protected internal virtual IList<IRenderer> GetOptionsMarkedSelected(IRenderer optionsSubTree) {
            IList<IRenderer> selectedOptions = new List<IRenderer>();
            foreach (IRenderer option in optionsSubTree.GetChildRenderers()) {
                if (!IsOptionRenderer(option)) {
                    IList<IRenderer> subSelectedOptions = GetOptionsMarkedSelected(option);
                    selectedOptions.AddAll(subSelectedOptions);
                }
                else {
                    if (true.Equals(option.GetProperty<bool?>(Html2PdfProperty.FORM_FIELD_SELECTED))) {
                        selectedOptions.Add(option);
                    }
                }
            }
            return selectedOptions;
        }

        private LayoutResult MakeLayoutResultFull(LayoutArea layoutArea, LayoutResult layoutResult) {
            IRenderer splitRenderer = layoutResult.GetSplitRenderer() != null ? layoutResult.GetSplitRenderer() : this;
            if (occupiedArea == null) {
                occupiedArea = new LayoutArea(layoutArea.GetPageNumber(), new Rectangle(layoutArea.GetBBox().GetLeft(), layoutArea
                    .GetBBox().GetTop(), 0, 0));
            }
            layoutResult = new LayoutResult(LayoutResult.FULL, occupiedArea, splitRenderer, null);
            return layoutResult;
        }

        internal static bool IsOptGroupRenderer(IRenderer renderer) {
            return renderer.HasProperty(Html2PdfProperty.FORM_FIELD_LABEL) && !renderer.HasProperty(Html2PdfProperty.FORM_FIELD_SELECTED
                );
        }

        internal static bool IsOptionRenderer(IRenderer child) {
            return child.HasProperty(Html2PdfProperty.FORM_FIELD_SELECTED);
        }

        internal static void ReplaceParagraphRenderers(IRenderer rendererSubTree) {
            // TODO does inf width messes up the layout?
            // TODO may be better run it in constructor
            for (int i = 0; i < rendererSubTree.GetChildRenderers().Count; ++i) {
                IRenderer renderer = rendererSubTree.GetChildRenderers()[i];
                if (renderer is ParagraphRenderer) {
                    // TODO may be better to introduce intermediate parent
                    InfiniteWidthParagraphRenderer newRenderer = new InfiniteWidthParagraphRenderer((ParagraphRenderer)renderer
                        );
                    rendererSubTree.GetChildRenderers()[i] = newRenderer;
                }
                ReplaceParagraphRenderers(renderer);
            }
        }
    }
}
