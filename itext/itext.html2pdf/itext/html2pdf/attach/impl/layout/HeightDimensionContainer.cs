using System;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Page;

namespace iText.Html2pdf.Attach.Impl.Layout {
    internal class HeightDimensionContainer : DimensionContainer {
        internal HeightDimensionContainer(CssContextNode pmbcNode, float width, float maxHeight, ProcessorContext 
            context) {
            String height = pmbcNode.GetStyles().Get(CssConstants.HEIGHT);
            if (height != null && !height.Equals("auto")) {
                dimension = ParseDimension(pmbcNode, height, maxHeight);
            }
            minDimension = GetMinHeight(pmbcNode, maxHeight);
            maxDimension = GetMaxHeight(pmbcNode, maxHeight);
            minContentDimension = PageContextProcessor.GetMinContentHeight((PageMarginBoxContextNode)pmbcNode, width, 
                maxHeight, context);
            maxContentDimension = PageContextProcessor.GetMaxContentHeight((PageMarginBoxContextNode)pmbcNode, width, 
                maxHeight, context);
        }

        private float GetMinHeight(CssContextNode node, float maxAvailableHeight) {
            String content = node.GetStyles().Get(CssConstants.MIN_HEIGHT);
            if (content == null) {
                return 0;
            }
            content = content.ToLowerInvariant().Trim();
            if (content.Equals("inherit")) {
                if (node.ParentNode() is CssContextNode) {
                    return GetMinHeight((CssContextNode)node.ParentNode(), maxAvailableHeight);
                }
                return 0;
            }
            return ParseDimension(node, content, maxAvailableHeight);
        }

        private float GetMaxHeight(CssContextNode node, float maxAvailableHeight) {
            String content = node.GetStyles().Get(CssConstants.MAX_HEIGHT);
            if (content == null) {
                return float.MaxValue;
            }
            content = content.ToLowerInvariant().Trim();
            if (content.Equals("inherit")) {
                if (node.ParentNode() is CssContextNode) {
                    return GetMaxHeight((CssContextNode)node.ParentNode(), maxAvailableHeight);
                }
                return float.MaxValue;
            }
            float dim = ParseDimension(node, content, maxAvailableHeight);
            if (dim == 0) {
                return float.MaxValue;
            }
            return dim;
        }
    }
}
