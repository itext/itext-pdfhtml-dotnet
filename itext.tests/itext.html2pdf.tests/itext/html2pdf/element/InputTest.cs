/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
Authors: Apryse Software.

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
using iText.Forms.Form.Element;
using iText.Forms.Logs;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl;
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Logs;
using iText.IO.Util;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Logs;
using iText.StyledXmlParser.Node;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class InputTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/InputTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/InputTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Input01Test() {
            RunTest("inputTest01");
        }

        [NUnit.Framework.Test]
        public virtual void Input02Test() {
            RunTest("inputTest02");
        }

        [NUnit.Framework.Test]
        public virtual void Input03Test() {
            RunTest("inputTest03");
        }

        [NUnit.Framework.Test]
        public virtual void Input04Test() {
            RunTest("inputTest04");
        }

        [NUnit.Framework.Test]
        public virtual void Input05Test() {
            RunTest("inputTest05");
        }

        [NUnit.Framework.Test]
        [LogMessage(FormsLogMessageConstants.INPUT_FIELD_DOES_NOT_FIT, Ignore = true)]
        public virtual void Input06Test() {
            String htmlPath = sourceFolder + "inputTest06.html";
            String outPdfPath = destinationFolder + "inputTest06.pdf";
            String cmpPdfPath = sourceFolder + "cmp_" + "inputTest06.pdf";
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(htmlPath) + "\n");
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outPdfPath));
            pdfDoc.SetDefaultPageSize(PageSize.A8);
            HtmlConverter.ConvertToPdf(new FileStream(htmlPath, FileMode.Open, FileAccess.Read), pdfDoc, new ConverterProperties
                ().SetCreateAcroForm(true));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdfPath, cmpPdfPath, destinationFolder
                , "diff_inputTest06_"));
        }

        [NUnit.Framework.Test]
        public virtual void Input07Test() {
            // TODO DEVSIX-1777: if not explicitly specified, <input> border default value should be different from the one
            // specified in user agent css. Also user agent css should not specify default color
            // and should use 'initial' instead.
            RunTest("inputTest07");
        }

        [NUnit.Framework.Test]
        public virtual void Input08Test() {
            RunTest("inputTest08");
        }

        [NUnit.Framework.Test]
        public virtual void Input09Test() {
            RunTest("inputTest09");
        }

        [NUnit.Framework.Test]
        public virtual void Input10Test() {
            RunTest("inputTest10");
        }

        [NUnit.Framework.Test]
        public virtual void TextareaRowsHeightTest() {
            RunTest("textareaRowsHeight");
        }

        [NUnit.Framework.Test]
        public virtual void BlockHeightTest() {
            RunTest("blockHeightTest");
        }

        [NUnit.Framework.Test]
        public virtual void SmallPercentWidthTest() {
            RunTest("smallPercentWidth");
        }

        [NUnit.Framework.Test]
        public virtual void Button01Test() {
            RunTest("buttonTest01");
        }

        [NUnit.Framework.Test]
        public virtual void Button02Test() {
            RunTest("buttonTest02");
        }

        [NUnit.Framework.Test]
        public virtual void FlexWithButtonsTest() {
            RunTest("flexWithButtons");
        }

        [NUnit.Framework.Test]
        public virtual void ButtonWithDisplayBlockTest() {
            RunTest("buttonWithDisplayBlock");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INPUT_TYPE_IS_INVALID)]
        public virtual void InputDefaultTest01() {
            RunTest("inputDefaultTest01");
        }

        [NUnit.Framework.Test]
        public virtual void PlaceholderTest01() {
            RunTest("placeholderTest01");
        }

        [NUnit.Framework.Test]
        public virtual void PlaceholderTest02() {
            RunTest("placeholderTest02");
        }

        [NUnit.Framework.Test]
        public virtual void PlaceholderTest02A() {
            RunTest("placeholderTest02A");
        }

        [NUnit.Framework.Test]
        public virtual void PlaceholderTest03() {
            RunTest("placeholderTest03");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INPUT_TYPE_IS_NOT_SUPPORTED, Ignore = true)]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER, Ignore = true)]
        public virtual void PlaceholderTest04() {
            RunTest("placeholderTest04");
        }

        [NUnit.Framework.Test]
        public virtual void InputDisabled01AcroTest() {
            String htmlPath = sourceFolder + "inputDisabled01Test.html";
            String outPdfPath = destinationFolder + "inputDisabled01AcroTest.pdf";
            String cmpPdfPath = sourceFolder + "cmp_" + "inputDisabled01AcroTest.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(outPdfPath), new ConverterProperties().SetCreateAcroForm
                (true));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdfPath, cmpPdfPath, destinationFolder
                ));
        }

        [NUnit.Framework.Test]
        public virtual void InputDisabled01Test() {
            RunTest("inputDisabled01Test");
        }

        [NUnit.Framework.Test]
        public virtual void PlaceholderTest05() {
            String htmlPath = sourceFolder + "placeholderTest05.html";
            String outPdfPath = destinationFolder + "placeholderTest05.pdf";
            String cmpPdfPath = sourceFolder + "cmp_" + "placeholderTest05.pdf";
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(htmlPath) + "\n");
            IList<IElement> elements = HtmlConverter.ConvertToElements(new FileStream(htmlPath, FileMode.Open, FileAccess.Read
                ));
            Paragraph placeholderToBeSet = new Paragraph("bazinga").SetBackgroundColor(ColorConstants.RED).SetFontColor
                (ColorConstants.YELLOW);
            IList<IElement> children = ((Paragraph)elements[0]).GetChildren();
            foreach (IElement child in children) {
                if (child is InputField) {
                    ((InputField)child).SetPlaceholder(placeholderToBeSet);
                }
            }
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(outPdfPath));
            Document doc = new Document(pdfDoc);
            foreach (IElement child in elements) {
                if (child is IBlockElement) {
                    doc.Add((IBlockElement)child);
                }
            }
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdfPath, cmpPdfPath, destinationFolder
                , "diff_placeholderTest05_"));
        }

        [NUnit.Framework.Test]
        public virtual void PlaceholderTest05A() {
            RunTest("placeholderTest05A");
        }

        [NUnit.Framework.Test]
        public virtual void CheckboxTaggingTest() {
            // TODO fix after DEVSIX-3461 is done
            ConvertToPdfAndCompare("checkboxTagging", sourceFolder, destinationFolder, true);
        }

        [NUnit.Framework.Test]
        public virtual void CheckboxInTableTest() {
            RunTest("checkboxInTable");
        }

        [NUnit.Framework.Test]
        public virtual void CheckboxDisplayBlockInTableTest() {
            RunTest("checkboxDisplayBlockInTable");
        }

        [NUnit.Framework.Test]
        public virtual void CheckboxFullWidthDisplayBlockInTableTest() {
            RunTest("checkboxFullWidthDisplayBlockInTable");
        }

        [NUnit.Framework.Test]
        public virtual void CheckboxDiffWidthDisplayBlockInTableTest() {
            RunTest("checkboxDiffWidthDisplayBlockInTable");
        }

        [NUnit.Framework.Test]
        public virtual void CheckboxTest() {
            RunTest("checkbox");
        }

        [NUnit.Framework.Test]
        public virtual void CheckboxDisplayBlockTest() {
            RunTest("checkboxDisplayBlock");
        }

        [NUnit.Framework.Test]
        public virtual void CheckboxFullWidthDisplayBlockTest() {
            RunTest("checkboxFullWidthDisplayBlock");
        }

        [NUnit.Framework.Test]
        public virtual void CheckboxDiffWidthDisplayBlockTest() {
            RunTest("checkboxDiffWidthDisplayBlock");
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Ignore = true)]
        public virtual void LongInputValueCausesNothingTest() {
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetTagWorkerFactory(new InputTest.CustomTextInputTagWorkerFactory());
            ConvertToPdfAndCompare("longInputValueCausesNothingTest", sourceFolder, destinationFolder, false, converterProperties
                );
        }

        [NUnit.Framework.Test]
        public virtual void InputMinWidthTest() {
            RunTest("inputMinWidth");
        }

        private class CustomTextInputTagWorkerFactory : DefaultTagWorkerFactory {
            public override ITagWorker GetCustomTagWorker(IElementNode tag, ProcessorContext context) {
                switch (tag.Name().ToLowerInvariant()) {
                    case "input": {
                        switch (tag.GetAttribute("type").ToLowerInvariant()) {
                            case "text": {
                                IDictionary<String, String> map = new Dictionary<String, String>();
                                map.Put("page-break-inside", "avoid");
                                tag.AddAdditionalHtmlStyles(map);
                                return new InputTest.CustomInputDivTagWorker(tag, context);
                            }
                        }
                        break;
                    }
                }
                return null;
            }
        }

        private class CustomInputDivTagWorker : DivTagWorker {
            public CustomInputDivTagWorker(IElementNode element, ProcessorContext context)
                : base(element, context) {
                String value = element.GetAttribute("value");
                ProcessContent(value, context);
            }
        }

        private void RunTest(String name) {
            ConvertToPdfAndCompare(name, sourceFolder, destinationFolder);
        }
    }
}
