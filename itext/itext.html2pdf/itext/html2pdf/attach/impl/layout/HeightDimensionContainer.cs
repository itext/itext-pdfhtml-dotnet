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
using System;
using iText.Html2pdf.Css;
using iText.Kernel.Geom;
using iText.Layout.Layout;
using iText.Layout.Renderer;
using iText.StyledXmlParser.Css;

namespace iText.Html2pdf.Attach.Impl.Layout {
    internal class HeightDimensionContainer : DimensionContainer {
        private const float infHeight = 1e6f;

        internal HeightDimensionContainer(CssContextNode pmbcNode, float width, float maxHeight, IRenderer renderer
            , float additionalWidthFix) {
            String height = pmbcNode.GetStyles().Get(CssConstants.HEIGHT);
            if (height != null && !CssConstants.AUTO.Equals(height)) {
                dimension = ParseDimension(pmbcNode, height, maxHeight, additionalWidthFix);
            }
            minDimension = GetMinHeight(pmbcNode, maxHeight, additionalWidthFix);
            maxDimension = GetMaxHeight(pmbcNode, maxHeight, additionalWidthFix);
            if (!IsAutoDimension()) {
                maxContentDimension = dimension;
                maxContentDimension = dimension;
            }
            else {
                LayoutArea layoutArea = new LayoutArea(1, new Rectangle(0, 0, width, infHeight));
                LayoutContext minimalContext = new LayoutContext(layoutArea);
                LayoutResult quickLayout = renderer.Layout(minimalContext);
                if (quickLayout.GetStatus() != LayoutResult.NOTHING) {
                    maxContentDimension = quickLayout.GetOccupiedArea().GetBBox().GetHeight();
                    minContentDimension = maxContentDimension;
                }
            }
        }

        private float GetMinHeight(CssContextNode node, float maxAvailableHeight, float additionalWidthFix) {
            String content = node.GetStyles().Get(CssConstants.MIN_HEIGHT);
            if (content == null) {
                return 0;
            }
            return ParseDimension(node, content, maxAvailableHeight, additionalWidthFix);
        }

        private float GetMaxHeight(CssContextNode node, float maxAvailableHeight, float additionalWidthFix) {
            String content = node.GetStyles().Get(CssConstants.MAX_HEIGHT);
            if (content == null) {
                return float.MaxValue;
            }
            float dim = ParseDimension(node, content, maxAvailableHeight, additionalWidthFix);
            if (dim == 0) {
                return float.MaxValue;
            }
            return dim;
        }
    }
}
