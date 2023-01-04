/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 iText Group NV
Authors: iText Software.

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
using iText.Html2pdf.Css;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;
using iText.Test;

namespace iText.Html2pdf.Attach.Impl.Tags {
    [NUnit.Framework.Category("UnitTest")]
    public class PreTagWorkerTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void ProcessContentWithoutRNTest() {
            IElementNode elementNode = new JsoupElementNode(new iText.StyledXmlParser.Jsoup.Nodes.Element("not empty")
                );
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put(CssConstants.WHITE_SPACE, CssConstants.PRE);
            elementNode.SetStyles(styles);
            PreTagWorker preTagWorker = new PreTagWorker(elementNode, null);
            preTagWorker.ProcessContent("content", null);
            preTagWorker.ProcessContent("content", null);
            preTagWorker.ProcessContent("content", null);
            preTagWorker.PostProcessInlineGroup();
            Div div = (Div)preTagWorker.GetElementResult();
            IList<IElement> children = ((Paragraph)div.GetChildren()[0]).GetChildren();
            foreach (IElement child in children) {
                NUnit.Framework.Assert.IsTrue(child is Text);
                NUnit.Framework.Assert.AreEqual("\u200Dcontent", ((Text)child).GetText());
            }
        }

        [NUnit.Framework.Test]
        public virtual void ProcessContentWithNTest() {
            IElementNode elementNode = new JsoupElementNode(new iText.StyledXmlParser.Jsoup.Nodes.Element("not empty")
                );
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put(CssConstants.WHITE_SPACE, CssConstants.PRE);
            elementNode.SetStyles(styles);
            PreTagWorker preTagWorker = new PreTagWorker(elementNode, null);
            preTagWorker.ProcessContent("\ncontent", null);
            preTagWorker.ProcessContent("\ncontent", null);
            preTagWorker.ProcessContent("\ncontent", null);
            preTagWorker.PostProcessInlineGroup();
            Div div = (Div)preTagWorker.GetElementResult();
            IList<IElement> children = ((Paragraph)div.GetChildren()[0]).GetChildren();
            foreach (IElement child in children) {
                NUnit.Framework.Assert.IsTrue(child is Text);
                NUnit.Framework.Assert.AreEqual("\u200D\n\u200Dcontent", ((Text)child).GetText());
            }
        }

        [NUnit.Framework.Test]
        public virtual void ProcessContentWithRNTest() {
            IElementNode elementNode = new JsoupElementNode(new iText.StyledXmlParser.Jsoup.Nodes.Element("not empty")
                );
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put(CssConstants.WHITE_SPACE, CssConstants.PRE);
            elementNode.SetStyles(styles);
            PreTagWorker preTagWorker = new PreTagWorker(elementNode, null);
            preTagWorker.ProcessContent("\r\ncontent", null);
            preTagWorker.ProcessContent("\r\ncontent", null);
            preTagWorker.ProcessContent("\r\ncontent", null);
            preTagWorker.PostProcessInlineGroup();
            Div div = (Div)preTagWorker.GetElementResult();
            IList<IElement> children = ((Paragraph)div.GetChildren()[0]).GetChildren();
            for (int i = 0; i < children.Count; i++) {
                IElement child = children[i];
                NUnit.Framework.Assert.IsTrue(child is Text);
                if (i == 0) {
                    NUnit.Framework.Assert.AreEqual("\u200Dcontent", ((Text)child).GetText());
                }
                else {
                    NUnit.Framework.Assert.AreEqual("\u200D\r\n\u200Dcontent", ((Text)child).GetText());
                }
            }
        }
    }
}
