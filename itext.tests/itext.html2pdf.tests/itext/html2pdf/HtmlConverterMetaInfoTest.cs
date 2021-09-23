using System.IO;
using iText.Commons.Actions.Contexts;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl;
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Html;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using iText.StyledXmlParser.Node;
using iText.Test;

namespace iText.Html2pdf {
    public class HtmlConverterMetaInfoTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void MetaInfoShouldBePresentTest() {
            IMetaInfo o = new _IMetaInfo_34();
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetEventMetaInfo(o);
            HtmlConverterMetaInfoTest.InvocationAssert invocationAssert = new HtmlConverterMetaInfoTest.InvocationAssert
                ();
            converterProperties.SetTagWorkerFactory(new HtmlConverterMetaInfoTest.AssertMetaInfoTagWorkerFactory(invocationAssert
                ));
            Document document = HtmlConverter.ConvertToDocument("<!DOCTYPE html>\n" + "<html>\n" + "\n" + "<body>\n" +
                 "<div>\n" + "The content of the div\n" + "</div>\n" + "</body>\n" + "\n" + "</html>\n", new PdfDocument
                (new PdfWriter(new MemoryStream())), converterProperties);
            document.Close();
            NUnit.Framework.Assert.IsTrue(invocationAssert.IsInvoked());
        }

        private sealed class _IMetaInfo_34 : IMetaInfo {
            public _IMetaInfo_34() {
            }
        }

        private class AssertMetaInfoTagWorkerFactory : DefaultTagWorkerFactory {
            private readonly HtmlConverterMetaInfoTest.InvocationAssert invocationAssert;

            public AssertMetaInfoTagWorkerFactory(HtmlConverterMetaInfoTest.InvocationAssert invocationAssert) {
                this.invocationAssert = invocationAssert;
            }

            public override ITagWorker GetCustomTagWorker(IElementNode tag, ProcessorContext context) {
                if (TagConstants.DIV.Equals(tag.Name())) {
                    return new HtmlConverterMetaInfoTest.AssertMetaInfoDivTagWorker(tag, context, invocationAssert);
                }
                return base.GetCustomTagWorker(tag, context);
            }
        }

        private class AssertMetaInfoDivTagWorker : DivTagWorker {
            private readonly HtmlConverterMetaInfoTest.InvocationAssert invocationAssert;

            public AssertMetaInfoDivTagWorker(IElementNode element, ProcessorContext context, HtmlConverterMetaInfoTest.InvocationAssert
                 invocationAssert)
                : base(element, context) {
                this.invocationAssert = invocationAssert;
            }

            public override IPropertyContainer GetElementResult() {
                Div result = (Div)base.GetElementResult();
                result.SetNextRenderer(new HtmlConverterMetaInfoTest.AssertMetaInfoDivTagRenderer(result, invocationAssert
                    ));
                return result;
            }
        }

        private class AssertMetaInfoDivTagRenderer : DivRenderer {
            private readonly HtmlConverterMetaInfoTest.InvocationAssert invocationAssert;

            public AssertMetaInfoDivTagRenderer(Div modelElement, HtmlConverterMetaInfoTest.InvocationAssert invocationAssert
                )
                : base(modelElement) {
                this.invocationAssert = invocationAssert;
            }

            public override LayoutResult Layout(LayoutContext layoutContext) {
                NUnit.Framework.Assert.IsNotNull(this.GetProperty<MetaInfoContainer>(Property.META_INFO));
                invocationAssert.SetInvoked(true);
                return base.Layout(layoutContext);
            }

            public override IRenderer GetNextRenderer() {
                return new HtmlConverterMetaInfoTest.AssertMetaInfoDivTagRenderer((Div)modelElement, invocationAssert);
            }
        }

        private class InvocationAssert {
            private bool invoked;

            public virtual void SetInvoked(bool invoked) {
                this.invoked = invoked;
            }

            public virtual bool IsInvoked() {
                return invoked;
            }
        }
    }
}
