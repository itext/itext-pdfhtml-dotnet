/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
Authors: iText Software.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using System.IO;
using iText.Forms;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
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
        [LogMessage(iText.IO.LogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Count = 2)]
        public virtual void ButtonSplit02Test() {
            RunTest("buttonSplit02");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Count = 2)]
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
        [LogMessage(iText.Html2pdf.LogMessageConstant.ACROFORM_NOT_SUPPORTED_FOR_SELECT, Count = 2)]
        public virtual void SelectTest01() {
            RunTest("select01", false);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.ACROFORM_NOT_SUPPORTED_FOR_SELECT, Count = 3)]
        public virtual void SelectTest02() {
            RunTest("select02", false);
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
