using iText.Kernel.Pdf.Tagging;
using iText.Layout.Element;
using iText.Layout.Layout;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout {
    /// <summary>
    /// An
    /// <see cref="iText.Layout.Element.IElement"/>
    /// that serves as a placeholder for removed running element
    /// from the normal flow. This element is designed to register where particular running element would have been placed.
    /// </summary>
    public class RunningElement : Div {
        private RunningElementContainer runningElementContainer;

        /// <summary>
        /// Creates a new instance of
        /// <see cref="RunningElement"/>
        /// .
        /// </summary>
        /// <param name="runningElementContainer">a container for the actual running element removed from the normal flow.
        ///     </param>
        public RunningElement(RunningElementContainer runningElementContainer) {
            this.runningElementContainer = runningElementContainer;
            GetAccessibilityProperties().SetRole(StandardRoles.ARTIFACT);
        }

        protected override IRenderer MakeNewRenderer() {
            return new RunningElement.RunningElementRenderer(this, runningElementContainer);
        }

        /// <summary>It's an empty div so it's not expected to be ever split between areas.</summary>
        internal class RunningElementRenderer : DivRenderer {
            private RunningElementContainer runningElementContainer;

            private bool isFirstOnRootArea;

            public RunningElementRenderer(Div modelElement, RunningElementContainer runningElementContainer)
                : base(modelElement) {
                this.runningElementContainer = runningElementContainer;
            }

            public override LayoutResult Layout(LayoutContext layoutContext) {
                this.isFirstOnRootArea = IsFirstOnRootArea();
                return base.Layout(layoutContext);
            }

            public override void Draw(DrawContext drawContext) {
                runningElementContainer.SetOccurrencePage(GetOccupiedArea().GetPageNumber(), isFirstOnRootArea);
                base.Draw(drawContext);
            }
        }
    }
}
