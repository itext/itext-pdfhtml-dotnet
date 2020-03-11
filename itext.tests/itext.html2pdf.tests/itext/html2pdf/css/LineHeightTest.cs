/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
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
using System.Collections.Generic;
using System.IO;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl;
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Html;
using iText.IO.Util;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Font;
using iText.Layout.Properties;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css {
    public class LineHeightTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/LineHeightTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/LineHeightTest/";

        private static readonly String RESOURCES = SOURCE_FOLDER + "fonts/";

        private static ConverterProperties converterProperties = new ConverterProperties();

        [NUnit.Framework.OneTimeSetUp]
        public static void Init() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
            InitConverterProperties();
        }

        [NUnit.Framework.Test]
        public virtual void LineHeightFreeSansFontLengthTest() {
            TestLineHeight("lineHeightFreeSansFontLengthTest");
        }

        [NUnit.Framework.Test]
        public virtual void LineHeightFreeSansFontMaxCoeffTest() {
            TestLineHeight("lineHeightFreeSansFontMaxCoeffTest");
        }

        [NUnit.Framework.Test]
        public virtual void LineHeightFreeSansFontNormalTest() {
            // differences with HTML due to default coefficient (see LineHeightHelper#DEFAULT_LINE_HEIGHT_COEFF)
            TestLineHeight("lineHeightFreeSansFontNormalTest");
        }

        [NUnit.Framework.Test]
        public virtual void LineHeightFreeSansFontNumberTest() {
            TestLineHeight("lineHeightFreeSansFontNumberTest");
        }

        [NUnit.Framework.Test]
        public virtual void LineHeightFreeSansFontPercentageTest() {
            TestLineHeight("lineHeightFreeSansFontPercentageTest");
        }

        [NUnit.Framework.Test]
        public virtual void LineHeightNotoSansFontMaxCoeffTest() {
            TestLineHeight("lineHeightNotoSansFontMaxCoeffTest");
        }

        [NUnit.Framework.Test]
        public virtual void LineHeightNotoSansFontNormalTest() {
            TestLineHeight("lineHeightNotoSansFontNormalTest");
        }

        [NUnit.Framework.Test]
        public virtual void GetMaxLineHeightWhenThereAreFewInlineElementsInTheBoxTest() {
            TestLineHeight("getMaxLineHeightWhenThereAreFewInlineElementsInTheBoxTest");
        }

        [NUnit.Framework.Test]
        public virtual void LineHeightWithDiffFontsTest() {
            TestLineHeight("lineHeightWithDiffFontsTest");
        }

        [NUnit.Framework.Test]
        public virtual void LineHeightStratFontTest() {
            TestLineHeight("lineHeightStratFontTest");
        }

        [NUnit.Framework.Test]
        public virtual void ImageLineHeightNormalTest() {
            TestLineHeight("imageLineHeightNormalTest");
        }

        [NUnit.Framework.Test]
        public virtual void ImageLineHeightTest() {
            TestLineHeight("imageLineHeightTest");
        }

        [NUnit.Framework.Test]
        public virtual void ImageLineHeightZeroTest() {
            TestLineHeight("imageLineHeightZeroTest");
        }

        [NUnit.Framework.Test]
        public virtual void ImageAscenderLineHeightTest() {
            TestLineHeight("imageAscenderLineHeightTest");
        }

        [NUnit.Framework.Test]
        public virtual void InputLineHeightTest() {
            TestLineHeight("inputLineHeightTest");
        }

        [NUnit.Framework.Test]
        public virtual void TextAreaLineHeightNormalTest() {
            TestLineHeight("textAreaLineHeightNormalTest");
        }

        [NUnit.Framework.Test]
        public virtual void TextAreaLineHeightTest() {
            TestLineHeight("textAreaLineHeightTest");
        }

        [NUnit.Framework.Test]
        public virtual void TextAreaLineHeightShortTest() {
            TestLineHeight("textAreaLineHeightShortTest");
        }

        [NUnit.Framework.Test]
        public virtual void InlineElementLineHeightTest() {
            // TODO DEVSIX-2485 change cmp after fixing the ticket
            TestLineHeight("inlineElementLineHeightTest");
        }

        [NUnit.Framework.Test]
        public virtual void InlineBlockElementLineHeightTest() {
            TestLineHeight("inlineBlockElementLineHeightTest");
        }

        [NUnit.Framework.Test]
        public virtual void BlockElementLineHeightTest() {
            TestLineHeight("blockElementLineHeightTest");
        }

        [NUnit.Framework.Test]
        public virtual void DefaultLineHeightTest() {
            IList<IElement> elements = HtmlConverter.ConvertToElements("<p>Lorem Ipsum</p>");
            NUnit.Framework.Assert.AreEqual(1.2f, elements[0].GetProperty<Leading>(Property.LEADING).GetValue(), 1e-10
                );
        }

        internal virtual void TestLineHeight(String name) {
            String sourceHtml = SOURCE_FOLDER + name + ".html";
            String destinationPdf = DESTINATION_FOLDER + name + ".pdf";
            String cmpPdf = SOURCE_FOLDER + "cmp_" + name + ".pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationPdf));
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, pdfDocument, converterProperties);
            }
            System.Console.Out.WriteLine("html: file://" + UrlUtil.ToNormalizedURI(sourceHtml).AbsolutePath + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationPdf, cmpPdf, DESTINATION_FOLDER
                , "diff_" + name + "_"));
        }

        internal static void InitConverterProperties() {
            converterProperties.SetBaseUri(SOURCE_FOLDER);
            FontProvider fontProvider = new FontProvider();
            fontProvider.AddDirectory(RESOURCES);
            fontProvider.AddStandardPdfFonts();
            converterProperties.SetFontProvider(fontProvider);
            DefaultTagWorkerFactory tagWorkerFactory = new LineHeightTest.HtmlModeTagWorkerFactory();
            converterProperties.SetTagWorkerFactory(tagWorkerFactory);
        }

        internal class HtmlModeTagWorkerFactory : DefaultTagWorkerFactory {
            public override ITagWorker GetCustomTagWorker(IElementNode tag, ProcessorContext context) {
                if (TagConstants.BODY.Equals(tag.Name())) {
                    return new LineHeightTest.HtmlModeBodyTagWorker(tag, context);
                }
                return null;
            }
        }

        internal class HtmlModeBodyTagWorker : BodyTagWorker {
            internal HtmlModeBodyTagWorker(IElementNode element, ProcessorContext context)
                : base(element, context) {
            }

            public override IPropertyContainer GetElementResult() {
                IPropertyContainer result = base.GetElementResult();
                result.SetProperty(Property.RENDERING_MODE, RenderingMode.HTML_MODE);
                return result;
            }
        }
    }
}
