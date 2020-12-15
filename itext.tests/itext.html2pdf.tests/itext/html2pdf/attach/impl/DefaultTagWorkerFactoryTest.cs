using System;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Exceptions;
using iText.IO.Util;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;
using iText.Test;

namespace iText.Html2pdf.Attach.Impl {
    public class DefaultTagWorkerFactoryTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void CannotGetTagWorkerForCustomTagViaReflection() {
            String tag = "custom-tag";
            String className = "iText.Html2pdf.Attach.Impl.TestClass";
            NUnit.Framework.Assert.That(() =>  {
                new TestTagWorkerFactory().GetTagWorker(new JsoupElementNode(new iText.StyledXmlParser.Jsoup.Nodes.Element
                    (iText.StyledXmlParser.Jsoup.Parser.Tag.ValueOf("custom-tag"), "")), new ProcessorContext(new ConverterProperties
                    ()));
            }
            , NUnit.Framework.Throws.InstanceOf<TagWorkerInitializationException>().With.Message.EqualTo(MessageFormatUtil.Format(TagWorkerInitializationException.REFLECTION_IN_TAG_WORKER_FACTORY_IMPLEMENTATION_FAILED, className, tag)))
;
        }
    }

    internal class TestTagWorkerFactory : DefaultTagWorkerFactory {
        public TestTagWorkerFactory() {
            defaultMapping.PutMapping("custom-tag", typeof(TestClass));
        }
    }

    internal class TestClass {
    }
}
