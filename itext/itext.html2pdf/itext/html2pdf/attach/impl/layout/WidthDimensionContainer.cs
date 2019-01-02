using System;
using iText.Html2pdf.Css;
using iText.Layout.Minmaxwidth;
using iText.Layout.Renderer;
using iText.StyledXmlParser.Css;

namespace iText.Html2pdf.Attach.Impl.Layout {
    internal class WidthDimensionContainer : DimensionContainer {
        public WidthDimensionContainer(CssContextNode node, float maxWidth, IRenderer renderer, float additionalWidthFix
            ) {
            String width = node.GetStyles().Get(CssConstants.WIDTH);
            if (width != null && !width.Equals("auto")) {
                dimension = ParseDimension(node, width, maxWidth, additionalWidthFix);
            }
            minDimension = GetMinWidth(node, maxWidth, additionalWidthFix);
            maxDimension = GetMaxWidth(node, maxWidth, additionalWidthFix);
            MinMaxWidth minMaxWidth = null;
            if (renderer is BlockRenderer) {
                minMaxWidth = ((BlockRenderer)renderer).GetMinMaxWidth();
                maxContentDimension = minMaxWidth.GetMaxWidth();
                minContentDimension = minMaxWidth.GetMinWidth();
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
}
