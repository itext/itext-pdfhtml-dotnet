/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
using iText.Layout.Minmaxwidth;
using iText.Layout.Renderer;
using iText.StyledXmlParser.Css;

namespace iText.Html2pdf.Attach.Impl.Layout {
//\cond DO_NOT_DOCUMENT
    internal class WidthDimensionContainer : DimensionContainer {
        public WidthDimensionContainer(CssContextNode node, float maxWidth, IRenderer renderer, float additionalWidthFix
            ) {
            String width = node.GetStyles().Get(CssConstants.WIDTH);
            if (width != null && !CssConstants.AUTO.Equals(width)) {
                dimension = ParseDimension(node, width, maxWidth, additionalWidthFix);
            }
            minDimension = GetMinWidth(node, maxWidth, additionalWidthFix);
            maxDimension = GetMaxWidth(node, maxWidth, additionalWidthFix);
            if (!IsAutoDimension()) {
                // According to point 3 of paragraph "5.3.2.3. Handling min-width and max-width" of the specification
                // maxContentDimension and minContentDimension will always be equal
                maxContentDimension = dimension;
                minContentDimension = dimension;
            }
            else {
                if (renderer is BlockRenderer) {
                    MinMaxWidth minMaxWidth = ((BlockRenderer)renderer).GetMinMaxWidth();
                    maxContentDimension = minMaxWidth.GetMaxWidth();
                    minContentDimension = minMaxWidth.GetMinWidth();
                }
            }
        }

        private float GetMinWidth(CssContextNode node, float maxAvailableWidth, float additionalWidthFix) {
            String content = node.GetStyles().Get(CssConstants.MIN_WIDTH);
            if (content == null) {
                return 0;
            }
            return ParseDimension(node, content, maxAvailableWidth, additionalWidthFix);
        }

        private float GetMaxWidth(CssContextNode node, float maxAvailableWidth, float additionalWidthFix) {
            String content = node.GetStyles().Get(CssConstants.MAX_WIDTH);
            if (content == null) {
                return float.MaxValue;
            }
            float dim = ParseDimension(node, content, maxAvailableWidth, additionalWidthFix);
            if (dim == 0) {
                return float.MaxValue;
            }
            return dim;
        }
    }
//\endcond
}
