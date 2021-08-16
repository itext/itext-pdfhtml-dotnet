using System;
using System.Collections.Generic;
using iText.Html2pdf.Css;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;
using iText.Test;

namespace iText.Html2pdf.Attach.Impl.Tags {
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
