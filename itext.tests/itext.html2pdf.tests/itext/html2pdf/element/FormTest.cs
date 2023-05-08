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
using System.IO;
using iText.Forms;
using iText.Forms.Logs;
using iText.Html2pdf;
using iText.IO.Util;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout.Logs;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class FormTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/FormTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/FormTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void SimpleTextFieldTest() {
            RunTest("simpleTextField");
        }

        [NUnit.Framework.Test]
        public virtual void SplitTextFieldTest() {
            RunTest("splitTextField");
        }

        [NUnit.Framework.Test]
        public virtual void ForcedSplitTextFieldTest() {
            RunTest("forcedSplitTextField");
        }

        [NUnit.Framework.Test]
        public virtual void TextFieldHeight1Test() {
            RunTest("textFieldHeight1");
        }

        [NUnit.Framework.Test]
        public virtual void TextFieldHeight2Test() {
            RunTest("textFieldHeight2");
        }

        [NUnit.Framework.Test]
        public virtual void TextFieldHeight3Test() {
            RunTest("textFieldHeight3");
        }

        [NUnit.Framework.Test]
        public virtual void SimpleButtonTest() {
            RunTest("simpleButton");
        }

        [NUnit.Framework.Test]
        public virtual void FieldsetTest() {
            RunTest("fieldset");
        }

        [NUnit.Framework.Test]
        public virtual void FieldsetLegendTest() {
            RunTest("fieldsetLegend");
        }

        [NUnit.Framework.Test]
        public virtual void LabelTest() {
            RunTest("label");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1316")]
        public virtual void FieldInTablePercent() {
            RunTest("fieldInTablePercent");
        }

        [NUnit.Framework.Test]
        public virtual void InputDisplayTest() {
            RunTest("inputDisplay");
        }

        [NUnit.Framework.Test]
        public virtual void TextareaDisplayTest() {
            RunTest("textareaDisplay");
        }

        [NUnit.Framework.Test]
        public virtual void ColsAttributeInTextareaTest() {
            RunTest("colsTextArea01");
        }

        [NUnit.Framework.Test]
        public virtual void ColsAttributeWithBigValueInTextareaTest() {
            RunTest("colsTextArea02");
        }

        [NUnit.Framework.Test]
        public virtual void ColsAttributeWithSmallValueInTextareaTest() {
            RunTest("colsTextArea03");
        }

        [NUnit.Framework.Test]
        public virtual void Checkbox1Test() {
            RunTest("checkbox1");
        }

        [NUnit.Framework.Test]
        public virtual void ButtonWithChildrenTest() {
            RunTest("buttonWithChildren");
        }

        [NUnit.Framework.Test]
        public virtual void ButtonSplit01Test() {
            RunTest("buttonSplit01");
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Count = 4)]
        [LogMessage(FormsLogMessageConstants.INPUT_FIELD_DOES_NOT_FIT, Count = 2)]
        public virtual void ButtonSplit02Test() {
            RunTest("buttonSplit02");
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Count = 2)]
        [LogMessage(FormsLogMessageConstants.INPUT_FIELD_DOES_NOT_FIT, Count = 2)]
        public virtual void ButtonSplit03Test() {
            RunTest("buttonSplit03");
        }

        [NUnit.Framework.Test]
        public virtual void Radiobox1Test() {
            RunTest("radiobox1");
        }

        [NUnit.Framework.Test]
        public virtual void Radiobox2Test() {
            RunTest("radiobox2");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.MULTIPLE_VALUES_ON_A_NON_MULTISELECT_FIELD)]
        public virtual void SelectTest01() {
            RunTest("select01", true);
        }

        [NUnit.Framework.Test]
        public virtual void SelectTest02() {
            RunTest("select02", true);
        }

        [NUnit.Framework.Test]
        public virtual void RadioButtonWithPageCounterAtBottonTest() {
            //TODO: update cmpfile after DEVSIX-4772 will be fixed
            String html = sourceFolder + "radioButtonWithPageCounterAtBotton.html";
            String pdf = destinationFolder + "radioButtonWithPageCounterAtBotton.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(html), new FileInfo(pdf), new ConverterProperties().SetCreateAcroForm
                (true));
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(html) + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(pdf, sourceFolder + "cmp_radioButtonWithPageCounterAtBotton.pdf"
                , destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void RadioButtonWithPageCounterOnTopTest() {
            //TODO: update cmpfile after DEVSIX-4772 will be fixed
            String html = sourceFolder + "radioButtonWithPageCounterOnTop.html";
            String pdf = destinationFolder + "radioButtonWithPageCounterOnTop.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(html), new FileInfo(pdf), new ConverterProperties().SetCreateAcroForm
                (true));
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(html) + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(pdf, sourceFolder + "cmp_radioButtonWithPageCounterOnTop.pdf"
                , destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void RadioButtonNoPageCounterTest() {
            String html = sourceFolder + "radioButtonNoPageCounter.html";
            String pdf = destinationFolder + "radioButtonNoPageCounter.pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(html), new FileInfo(pdf), new ConverterProperties().SetCreateAcroForm
                (true));
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(html) + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(pdf, sourceFolder + "cmp_radioButtonNoPageCounter.pdf"
                , destinationFolder));
        }

        private void RunTest(String name) {
            RunTest(name, true);
        }

        private void RunTest(String name, bool flattenPdfAcroFormFields) {
            String htmlPath = sourceFolder + name + ".html";
            String outPdfPath = destinationFolder + name + ".pdf";
            String outAcroPdfPath = destinationFolder + name + "_acro.pdf";
            String outAcroFlattenPdfPath = destinationFolder + name + "_acro_flatten.pdf";
            String cmpPdfPath = sourceFolder + "cmp_" + name + ".pdf";
            String cmpAcroPdfPath = sourceFolder + "cmp_" + name + "_acro.pdf";
            String cmpAcroFlattenPdfPath = sourceFolder + "cmp_" + name + "_acro_flatten.pdf";
            String diff = "diff_" + name + "_";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(outPdfPath));
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(outAcroPdfPath), new ConverterProperties()
                .SetCreateAcroForm(true));
            if (flattenPdfAcroFormFields) {
                PdfDocument document = new PdfDocument(new PdfReader(outAcroPdfPath), new PdfWriter(outAcroFlattenPdfPath)
                    );
                PdfAcroForm acroForm = PdfAcroForm.GetAcroForm(document, false);
                acroForm.FlattenFields();
                document.Close();
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdfPath, cmpPdfPath, destinationFolder
                , diff));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outAcroPdfPath, cmpAcroPdfPath, destinationFolder
                , diff));
            if (flattenPdfAcroFormFields) {
                NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outAcroFlattenPdfPath, cmpAcroFlattenPdfPath
                    , destinationFolder, diff));
            }
        }
    }
}
