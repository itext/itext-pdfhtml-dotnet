/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
Authors: iText Software.

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
