using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Html;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Attach.Impl.Tags {
    public class SvgTagWorkerTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.UNABLE_TO_PROCESS_SVG_ELEMENT, LogLevel = LogLevelConstants.
            ERROR)]
        public virtual void NoSvgRootTest() {
            iText.StyledXmlParser.Jsoup.Nodes.Element element = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf(TagConstants.FIGURE), TagConstants.FIGURE);
            IElementNode elementNode = new JsoupElementNode(element);
            ConverterProperties properties = new ConverterProperties();
            ProcessorContext context = new ProcessorContext(properties);
            SvgTagWorker svgTagWorker = new SvgTagWorker(elementNode, context);
            NUnit.Framework.Assert.IsNull(svgTagWorker.GetElementResult());
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.UNABLE_TO_PROCESS_SVG_ELEMENT, LogLevel = LogLevelConstants.
            ERROR)]
        public virtual void NullElementTest() {
            ConverterProperties properties = new ConverterProperties();
            ProcessorContext context = new ProcessorContext(properties);
            SvgTagWorker svgTagWorker = new SvgTagWorker(null, context);
            NUnit.Framework.Assert.IsNull(svgTagWorker.GetElementResult());
        }
    }
}
