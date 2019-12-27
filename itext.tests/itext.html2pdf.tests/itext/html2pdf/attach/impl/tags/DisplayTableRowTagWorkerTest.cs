using System;
using System.Collections.Generic;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Jsoup.Nodes;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;
using iText.Test;

namespace iText.Html2pdf.Attach.Impl.Tags {
    public class DisplayTableRowTagWorkerTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void LangAttrInDisplayTableRowForTaggedPdfTest() {
            Attributes attributes = new Attributes();
            attributes.Put(AttributeConstants.LANG, "en");
            iText.StyledXmlParser.Jsoup.Nodes.Element element = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf(TagConstants.DIV), TagConstants.DIV, attributes);
            JsoupElementNode node = new JsoupElementNode(element);
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put(CssConstants.WHITE_SPACE, CssConstants.NORMAL);
            styles.Put(CssConstants.TEXT_TRANSFORM, CssConstants.LOWERCASE);
            styles.Put(CssConstants.DISPLAY, CssConstants.TABLE_ROW);
            node.SetStyles(styles);
            ProcessorContext processorContext = new ProcessorContext(new ConverterProperties());
            DisplayTableRowTagWorker tagWorker = new DisplayTableRowTagWorker(node, processorContext);
            iText.StyledXmlParser.Jsoup.Nodes.Element childElement = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf(TagConstants.TD), TagConstants.TD);
            JsoupElementNode childNode = new JsoupElementNode(childElement);
            styles = new Dictionary<String, String>();
            styles.Put(CssConstants.WHITE_SPACE, CssConstants.NORMAL);
            styles.Put(CssConstants.TEXT_TRANSFORM, CssConstants.LOWERCASE);
            styles.Put(CssConstants.DISPLAY, CssConstants.TABLE_CELL);
            childNode.SetStyles(styles);
            TdTagWorker childTagWorker = new TdTagWorker(childNode, processorContext);
            tagWorker.ProcessTagChild(childTagWorker, processorContext);
            IPropertyContainer propertyContainer = tagWorker.GetElementResult();
            NUnit.Framework.Assert.IsTrue(propertyContainer is Table);
            Cell cell = ((Table)propertyContainer).GetCell(0, 0);
            NUnit.Framework.Assert.IsNotNull(cell);
            NUnit.Framework.Assert.AreEqual("en", cell.GetAccessibilityProperties().GetLanguage());
        }
    }
}
