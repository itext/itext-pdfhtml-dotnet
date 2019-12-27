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
    public class BodyTagWorkerTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void LangAttrInBodyForTaggedPdfTest() {
            Attributes attributes = new Attributes();
            attributes.Put(AttributeConstants.LANG, "en");
            iText.StyledXmlParser.Jsoup.Nodes.Element element = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf(TagConstants.DIV), TagConstants.DIV, attributes);
            JsoupElementNode node = new JsoupElementNode(element);
            ConverterProperties converterProperties = new ConverterProperties();
            ProcessorContext processorContext = new ProcessorContext(converterProperties);
            BodyTagWorker tagWorker = new BodyTagWorker(node, processorContext);
            IPropertyContainer propertyContainer = tagWorker.GetElementResult();
            NUnit.Framework.Assert.IsTrue(propertyContainer is IAccessibleElement);
            String lang = ((IAccessibleElement)propertyContainer).GetAccessibilityProperties().GetLanguage();
            NUnit.Framework.Assert.AreEqual("en", lang);
        }
    }
}
