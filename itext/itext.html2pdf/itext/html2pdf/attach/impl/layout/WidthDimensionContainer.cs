using System;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Page;

namespace iText.Html2pdf.Attach.Impl.Layout {
    public class WidthDimensionContainer : DimensionContainer {
        public WidthDimensionContainer(CssContextNode node, float maxWidth, ProcessorContext context) {
            String width = node.GetStyles().Get(CssConstants.WIDTH);
            if (width != null && !width.Equals("auto")) {
                dimension = ParseDimension(node, width, maxWidth);
            }
            minDimension = GetMinWidth(node, maxWidth);
            maxDimension = GetMaxWidth(node, maxWidth);
            minContentDimension = PageContextProcessor.GetMinContentWidth((PageMarginBoxContextNode)node, context);
            maxContentDimension = PageContextProcessor.GetMaxContentWidth((PageMarginBoxContextNode)node, context);
        }

        internal virtual float GetMinWidth(CssContextNode node, float maxAvailableWidth) {
            String content = node.GetStyles().Get(CssConstants.MIN_WIDTH);
            if (content == null) {
                return 0;
            }
            content = content.ToLowerInvariant().Trim();
            if (content.Equals("inherit")) {
                if (node.ParentNode() is CssContextNode) {
                    return GetMinWidth((CssContextNode)node.ParentNode(), maxAvailableWidth);
                }
                return 0;
            }
            return ParseDimension(node, content, maxAvailableWidth);
        }

        internal virtual float GetMaxWidth(CssContextNode node, float maxAvailableWidth) {
            String content = node.GetStyles().Get(CssConstants.MAX_WIDTH);
            if (content == null) {
                return float.MaxValue;
            }
            content = content.ToLowerInvariant().Trim();
            if (content.Equals("inherit")) {
                if (node.ParentNode() is CssContextNode) {
                    return GetMaxWidth((CssContextNode)node.ParentNode(), maxAvailableWidth);
                }
                return float.MaxValue;
            }
            float dim = ParseDimension(node, content, maxAvailableWidth);
            if (dim == 0) {
                return float.MaxValue;
            }
            return dim;
        }
    }
}
