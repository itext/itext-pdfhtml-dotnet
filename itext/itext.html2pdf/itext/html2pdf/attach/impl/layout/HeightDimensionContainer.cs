using System;
using iText.Html2pdf.Css;
using iText.Kernel.Geom;
using iText.Layout.Layout;
using iText.Layout.Renderer;
using iText.StyledXmlParser.Css;

namespace iText.Html2pdf.Attach.Impl.Layout {
    internal class HeightDimensionContainer : DimensionContainer {
        internal HeightDimensionContainer(CssContextNode pmbcNode, float width, float maxHeight, IRenderer renderer
            , float additionalWidthFix) {
            String height = pmbcNode.GetStyles().Get(CssConstants.HEIGHT);
            if (height != null && !height.Equals("auto")) {
                dimension = ParseDimension(pmbcNode, height, maxHeight, additionalWidthFix);
            }
            minDimension = GetMinHeight(pmbcNode, maxHeight, additionalWidthFix);
            maxDimension = GetMaxHeight(pmbcNode, maxHeight, additionalWidthFix);
            LayoutArea layoutArea = new LayoutArea(1, new Rectangle(0, 0, width, maxHeight));
            LayoutContext minimalContext = new LayoutContext(layoutArea);
            LayoutResult quickLayout = renderer.Layout(minimalContext);
            maxContentDimension = quickLayout.GetOccupiedArea().GetBBox().GetHeight();
            minContentDimension = maxContentDimension;
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
