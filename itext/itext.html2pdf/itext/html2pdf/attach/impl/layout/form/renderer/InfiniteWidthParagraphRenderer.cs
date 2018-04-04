using System;
using iText.Layout.Element;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout.Form.Renderer {
    /// <summary>
    /// TODO
    /// Is required for layouting all the options text in one line.
    /// </summary>
    /// <remarks>
    /// TODO
    /// Is required for layouting all the options text in one line.
    /// Will be redundant when no-wrap value for white-space property will be supported
    /// </remarks>
    internal class InfiniteWidthParagraphRenderer : ParagraphRenderer {
        public InfiniteWidthParagraphRenderer(ParagraphRenderer renderer)
            : base((Paragraph)renderer.GetModelElement()) {
            // TODO make a block with paragraph as child
            childRenderers.AddAll(renderer.GetChildRenderers());
            SetParent(renderer.GetParent());
            SetProperty(Property.OVERFLOW_X, OverflowPropertyValue.VISIBLE);
            SetProperty(Property.OVERFLOW_Y, OverflowPropertyValue.VISIBLE);
        }

        private InfiniteWidthParagraphRenderer(Paragraph p)
            : base(p) {
        }

        //        setProperty(Property.SPLIT_CHARACTERS, new ISplitCharacters() {
        //            @Override
        //            public boolean isSplitCharacter(GlyphLine text, int glyphPos) {
        //                return false;
        //            }
        //        });
        public override LayoutResult Layout(LayoutContext layoutContext) {
            LayoutArea area = layoutContext.GetArea().Clone();
            area.GetBBox().SetWidth(INF);
            LayoutResult layoutResult = base.Layout(new LayoutContext(area, layoutContext.GetMarginsCollapseInfo(), layoutContext
                .GetFloatRendererAreas(), layoutContext.IsClippedHeight()));
            // TODO result not FULL
            LayoutArea resultOccArea = layoutResult.GetOccupiedArea().Clone();
            float maxLineWidth = float.MinValue;
            foreach (LineRenderer line in GetLines()) {
                // TODO no lines? null lines?
                maxLineWidth = Math.Max(line.GetOccupiedArea().GetBBox().GetHeight(), maxLineWidth);
            }
            resultOccArea.GetBBox().SetWidth(maxLineWidth);
            LayoutResult adjustedResult;
            if (layoutResult is MinMaxWidthLayoutResult) {
                // TODO probably not required if won't extend ParagraphRenderer
                adjustedResult = new MinMaxWidthLayoutResult(layoutResult.GetStatus(), resultOccArea, layoutResult.GetSplitRenderer
                    (), layoutResult.GetOverflowRenderer(), layoutResult.GetCauseOfNothing());
                ((MinMaxWidthLayoutResult)adjustedResult).SetMinMaxWidth(((MinMaxWidthLayoutResult)layoutResult).GetMinMaxWidth
                    ());
            }
            else {
                adjustedResult = new LayoutResult(layoutResult.GetStatus(), resultOccArea, layoutResult.GetSplitRenderer()
                    , layoutResult.GetOverflowRenderer(), layoutResult.GetCauseOfNothing());
            }
            return adjustedResult;
        }

        public override IRenderer GetNextRenderer() {
            return new iText.Html2pdf.Attach.Impl.Layout.Form.Renderer.InfiniteWidthParagraphRenderer((Paragraph)modelElement
                );
        }
    }
}
