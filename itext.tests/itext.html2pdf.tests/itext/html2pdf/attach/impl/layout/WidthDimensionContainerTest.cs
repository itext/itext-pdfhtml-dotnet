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
using System.Collections.Generic;
using iText.Layout.Element;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Node;
using iText.Test;

namespace iText.Html2pdf.Attach.Impl.Layout {
    [NUnit.Framework.Category("UnitTest")]
    public class WidthDimensionContainerTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void MinFixContentDimensionTest() {
            INode iNode = null;
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put("width", "20pt");
            CssContextNode cssContextNode = new _CssContextNode_46(iNode);
            cssContextNode.SetStyles(styles);
            Paragraph paragraph = new Paragraph("Paragraph");
            WidthDimensionContainer widthDimensionContainer = new WidthDimensionContainer(cssContextNode, 500, paragraph
                .CreateRendererSubTree(), 0);
            NUnit.Framework.Assert.AreEqual(widthDimensionContainer.minContentDimension, 20, 0.0);
        }

        private sealed class _CssContextNode_46 : CssContextNode {
            public _CssContextNode_46(INode baseArg1)
                : base(baseArg1) {
            }

            public override INode ParentNode() {
                return base.ParentNode();
            }
        }
    }
}
