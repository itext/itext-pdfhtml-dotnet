/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
Authors: Apryse Software.

This program is offered under a commercial and under the AGPL license.
For commercial licensing, contact us at https://itextpdf.com/sales.  For AGPL licensing, see below.

AGPL licensing:
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using iText.Kernel.Pdf.Tagging;
using iText.Layout.Element;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout {
    /// <summary>
    /// An
    /// <see cref="iText.Layout.Element.IElement"/>
    /// that serves as a placeholder for removed running element
    /// from the normal flow.
    /// </summary>
    /// <remarks>
    /// An
    /// <see cref="iText.Layout.Element.IElement"/>
    /// that serves as a placeholder for removed running element
    /// from the normal flow. This element is designed to register where particular running element would have been placed.
    /// </remarks>
    public class RunningElement : Div {
        private RunningElementContainer runningElementContainer;

        /// <summary>
        /// Creates a new instance of
        /// <see cref="RunningElement"/>.
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
                // LineRenderer uses html logic only if there is at least one child renderer in html
                // mode. So the case when the line contains only running elements should be
                // processed in the default mode, since for this line the line-height should not be calculated.
                SetProperty(Property.RENDERING_MODE, RenderingMode.DEFAULT_LAYOUT_MODE);
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
