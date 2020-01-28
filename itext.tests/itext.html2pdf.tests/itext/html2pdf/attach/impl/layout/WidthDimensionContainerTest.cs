using System;
using System.Collections.Generic;
using iText.Layout.Element;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Node;
using iText.Test;

namespace iText.Html2pdf.Attach.Impl.Layout {
    public class WidthDimensionContainerTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void MinFixContentDimensionTest() {
            INode iNode = null;
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put("width", "20pt");
            CssContextNode cssContextNode = new _CssContextNode_24(iNode);
            cssContextNode.SetStyles(styles);
            Paragraph paragraph = new Paragraph("Paragraph");
            WidthDimensionContainer widthDimensionContainer = new WidthDimensionContainer(cssContextNode, 500, paragraph
                .CreateRendererSubTree(), 0);
            NUnit.Framework.Assert.AreEqual(widthDimensionContainer.minContentDimension, 20, 0.0);
        }

        private sealed class _CssContextNode_24 : CssContextNode {
            public _CssContextNode_24(INode baseArg1)
                : base(baseArg1) {
            }

            public override INode ParentNode() {
                return base.ParentNode();
            }
        }
    }
}
