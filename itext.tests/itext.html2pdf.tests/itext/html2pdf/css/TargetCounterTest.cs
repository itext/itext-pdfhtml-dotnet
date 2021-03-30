/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
Authors: iText Software.

This program is offered under a commercial and under the AGPL license.
For commercial licensing, contact us at https://itextpdf.com/sales.  For AGPL licensing, see below.

AGPL licensing:
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.IO;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl;
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Html;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Renderer;
using iText.StyledXmlParser;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class TargetCounterTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/TargetCounterTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/TargetCounterTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterPageUrlNameTest() {
            ConvertToPdfAndCompare("targetCounterPageUrlName");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterPageUrlIdTest() {
            ConvertToPdfAndCompare("targetCounterPageUrlId");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.EXCEEDED_THE_MAXIMUM_NUMBER_OF_RELAYOUTS)]
        public virtual void TargetCounterManyRelayoutsTest() {
            ConvertToPdfAndCompare("targetCounterManyRelayouts");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterPageBigElementTest() {
            ConvertToPdfAndCompare("targetCounterPageBigElement");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterPageAllTagsTest() {
            ConvertToPdfAndCompare("targetCounterPageAllTags");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.CANNOT_RESOLVE_TARGET_COUNTER_VALUE, Count = 2)]
        public virtual void TargetCounterNotExistingTargetTest() {
            ConvertToPdfAndCompare("targetCounterNotExistingTarget");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION)]
        public virtual void PageTargetCounterTestWithLogMessageTest() {
            ConvertToPdfAndCompare("pageTargetCounterTestWithLogMessage");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, Count = 2)]
        public virtual void NonPageTargetCounterTestWithLogMessageTest() {
            // There should be only one log message here, but we have two because we resolve css styles twice.
            ConvertToPdfAndCompare("nonPageTargetCounterTestWithLogMessage");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterSeveralCountersTest() {
            ConvertToPdfAndCompare("targetCounterSeveralCounters");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterIDNotExistTest() {
            ConvertToPdfAndCompare("targetCounterIDNotExist");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterNotCounterElementTest() {
            ConvertToPdfAndCompare("targetCounterNotCounterElement");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterNestedCountersTest() {
            ConvertToPdfAndCompare("targetCounterNestedCounters");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterUnusualStylesTest() {
            ConvertToPdfAndCompare("targetCounterUnusualStyles");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCountersNestedCountersTest() {
            ConvertToPdfAndCompare("targetCountersNestedCounters");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterNotDefaultStyleTest() {
            // TODO DEVSIX-4789 Armenian and Georgian symbols are not drawn, but there is no log message.
            ConvertToPdfAndCompare("targetCounterNotDefaultStyle");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.EXCEEDED_THE_MAXIMUM_NUMBER_OF_RELAYOUTS)]
        public virtual void TargetCounterCannotBeResolvedTest() {
            ConvertToPdfAndCompare("targetCounterCannotBeResolved");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.CUSTOM_RENDERER_IS_SET_FOR_HTML_DOCUMENT)]
        public virtual void CustomRendererAndPageTargetCounterTest() {
            ConvertToPdfWithCustomRendererAndCompare("customRendererAndPageTargetCounter");
        }

        [NUnit.Framework.Test]
        public virtual void CustomRendererAndNonPageTargetCounterTest() {
            ConvertToPdfWithCustomRendererAndCompare("customRendererAndNonPageTargetCounter");
        }

        private void ConvertToPdfWithCustomRendererAndCompare(String name) {
            ConverterProperties properties = new ConverterProperties().SetTagWorkerFactory(new _DefaultTagWorkerFactory_166
                ());
            DefaultHtmlProcessor processor = new DefaultHtmlProcessor(properties);
            IXmlParser parser = new JsoupHtmlParser();
            String outPdfPath = destinationFolder + name + ".pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outPdfPath));
            IDocumentNode doc = parser.Parse(new FileStream(sourceFolder + name + ".html", FileMode.Open, FileAccess.Read
                ), properties.GetCharset());
            Document document = processor.ProcessDocument(doc, pdfDocument);
            document.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdfPath, sourceFolder + "cmp_" + name 
                + ".pdf", destinationFolder));
        }

        private sealed class _DefaultTagWorkerFactory_166 : DefaultTagWorkerFactory {
            public _DefaultTagWorkerFactory_166() {
            }

            public override ITagWorker GetCustomTagWorker(IElementNode tag, ProcessorContext context) {
                if (TagConstants.HTML.Equals(tag.Name())) {
                    return new _HtmlTagWorker_170(tag, context);
                }
                return null;
            }

            private sealed class _HtmlTagWorker_170 : HtmlTagWorker {
                public _HtmlTagWorker_170(IElementNode baseArg1, ProcessorContext baseArg2)
                    : base(baseArg1, baseArg2) {
                }

                public override IPropertyContainer GetElementResult() {
                    Document document = (Document)base.GetElementResult();
                    document.SetRenderer(new DocumentRenderer(document));
                    return document;
                }
            }
        }

        private void ConvertToPdfAndCompare(String name) {
            ConvertToPdfAndCompare(name, sourceFolder, destinationFolder);
        }
    }
}
