using System;
using System.Collections.Generic;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.Layout;
using iText.Layout.Tagging;
using iText.StyledXmlParser.Jsoup.Nodes;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;
using iText.Test;

namespace iText.Html2pdf.Attach.Impl.Tags {
    public class PTagWorkerTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void LangAttrInPForTaggedPdfTest() {
            Attributes attributes = new Attributes();
            attributes.Put(AttributeConstants.LANG, "en");
            iText.StyledXmlParser.Jsoup.Nodes.Element element = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf(TagConstants.P), TagConstants.P, attributes);
            JsoupElementNode node = new JsoupElementNode(element);
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put(CssConstants.WHITE_SPACE, CssConstants.NORMAL);
            styles.Put(CssConstants.TEXT_TRANSFORM, CssConstants.LOWERCASE);
            node.SetStyles(styles);
            ProcessorContext processorContext = new ProcessorContext(new ConverterProperties());
            PTagWorker tagWorker = new PTagWorker(node, processorContext);
            tagWorker.ProcessEnd(node, processorContext);
            IPropertyContainer propertyContainer = tagWorker.GetElementResult();
            NUnit.Framework.Assert.IsTrue(propertyContainer is IAccessibleElement);
            String lang = ((IAccessibleElement)propertyContainer).GetAccessibilityProperties().GetLanguage();
            NUnit.Framework.Assert.AreEqual("en", lang);
        }
    }
}
