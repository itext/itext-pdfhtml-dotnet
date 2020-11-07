using System;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Css.Resolve.Func.Counter;
using iText.Html2pdf.Html;
using iText.Layout.Properties;
using iText.StyledXmlParser.Jsoup.Nodes;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;
using iText.Test;

namespace iText.Html2pdf.Attach.Util {
    public class LinkHelperTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void CreateDestinationDestinationTest() {
            ITagWorker worker = new DivTagWorker(new PageTargetCountElementNode(null, ""), null);
            Attributes attributes = new Attributes();
            attributes.Put(AttributeConstants.ID, "some_id");
            attributes.Put(AttributeConstants.HREF, "#some_id");
            JsoupElementNode elementNode = new JsoupElementNode(new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf("a"), "", attributes));
            ProcessorContext context = new ProcessorContext(new ConverterProperties());
            context.GetLinkContext().ScanForIds(elementNode);
            LinkHelper.CreateDestination(worker, elementNode, context);
            NUnit.Framework.Assert.AreEqual("some_id", worker.GetElementResult().GetProperty<String>(Property.DESTINATION
                ));
        }

        [NUnit.Framework.Test]
        public virtual void CreateDestinationIDTest() {
            ITagWorker worker = new DivTagWorker(new PageTargetCountElementNode(null, ""), null);
            Attributes attributes = new Attributes();
            attributes.Put(AttributeConstants.ID, "some_id");
            JsoupElementNode elementNode = new JsoupElementNode(new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf("a"), "", attributes));
            ProcessorContext context = new ProcessorContext(new ConverterProperties());
            LinkHelper.CreateDestination(worker, elementNode, context);
            NUnit.Framework.Assert.AreEqual("some_id", worker.GetElementResult().GetProperty<String>(Property.ID));
        }
    }
}
