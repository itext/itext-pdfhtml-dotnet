using System;
using iText.Html2pdf.Exceptions;
using iText.IO.Util;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;
using iText.Test;

namespace iText.Html2pdf.Css.Apply.Impl {
    public class DefaultCssApplierFactoryTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void CannotGetCssApplierForCustomTagViaReflection() {
            String tag = "custom-tag";
            String className = "iText.Html2pdf.Css.Apply.Impl.TestClass";
            NUnit.Framework.Assert.That(() =>  {
                new TestCssApplierFactory().GetCssApplier(new JsoupElementNode(new iText.StyledXmlParser.Jsoup.Nodes.Element
                    (iText.StyledXmlParser.Jsoup.Parser.Tag.ValueOf("custom-tag"), "")));
            }
            , NUnit.Framework.Throws.InstanceOf<CssApplierInitializationException>().With.Message.EqualTo(MessageFormatUtil.Format(CssApplierInitializationException.REFLECTION_FAILED, className, tag)))
;
        }
    }

    internal class TestCssApplierFactory : DefaultCssApplierFactory {
        public TestCssApplierFactory() {
            defaultMapping.PutMapping("custom-tag", typeof(TestClass));
        }
    }

    internal class TestClass {
    }
}
