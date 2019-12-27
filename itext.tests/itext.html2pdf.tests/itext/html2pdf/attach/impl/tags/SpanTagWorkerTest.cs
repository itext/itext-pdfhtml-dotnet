using System;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Html;
using iText.Layout;
using iText.Layout.Tagging;
using iText.StyledXmlParser.Jsoup.Nodes;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;
using iText.Test;

namespace iText.Html2pdf.Attach.Impl.Tags {
    public class SpanTagWorkerTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void LangAttrInSpanForTaggedPdfTest() {
            Attributes attributes = new Attributes();
            attributes.Put(AttributeConstants.LANG, "en");
            iText.StyledXmlParser.Jsoup.Nodes.Element element = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf(TagConstants.SPAN), TagConstants.SPAN, attributes);
            JsoupElementNode node = new JsoupElementNode(element);
            ProcessorContext processorContext = new ProcessorContext(new ConverterProperties());
            SpanTagWorker tagWorker = new SpanTagWorker(node, processorContext);
            tagWorker.ProcessContent("Some text", processorContext);
            tagWorker.ProcessEnd(node, processorContext);
            IPropertyContainer propertyContainer = tagWorker.GetAllElements()[0];
            NUnit.Framework.Assert.IsTrue(propertyContainer is IAccessibleElement);
            String lang = ((IAccessibleElement)propertyContainer).GetAccessibilityProperties().GetLanguage();
            NUnit.Framework.Assert.AreEqual("en", lang);
        }
    }
}
