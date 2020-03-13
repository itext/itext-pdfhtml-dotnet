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

namespace iText.Html2pdf.Element {
    public class TaggedPdfFormTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/TaggedPdfFormTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/TaggedPdfFormTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void SimpleTextFieldTagged() {
            RunTest("simpleTextFieldTagged");
        }

        [NUnit.Framework.Test]
        public virtual void SimpleTextareaTagged() {
            RunTest("simpleTextareaTagged");
        }

        [NUnit.Framework.Test]
        public virtual void SimpleButtonTagged() {
            RunTest("simpleButtonTagged");
        }

        [NUnit.Framework.Test]
        public virtual void SimpleLabelTagged() {
            RunTest("simpleLabelTagged");
        }

        [NUnit.Framework.Test]
        public virtual void SimpleCheckboxTagged() {
            RunTest("simpleCheckboxTagged");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1901")]
        public virtual void SimpleSelectTagged() {
            RunTest("simpleSelectTagged");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1901")]
        public virtual void ListBoxSelectTagged() {
            RunTest("listBoxSelectTagged");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1901")]
        public virtual void ListBoxOptGroupSelectTagged() {
            RunTest("listBoxOptGroupSelectTagged");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1901")]
        public virtual void SimpleRadioFormTagged() {
            RunTest("simpleRadioFormTagged");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-980. DefaultHtmlProcessor ERROR No worker found for tag datalist")]
        public virtual void DatalistFormTagged() {
            RunTest("datalistFormTagged");
        }

        [NUnit.Framework.Test]
        public virtual void FieldsetFormTagged() {
            RunTest("fieldsetFormTagged");
        }

        private void RunTest(String name) {
            String htmlPath = sourceFolder + name + ".html";
            String outTaggedPdfPath = destinationFolder + name + ".pdf";
            String outTaggedPdfPathAcro = destinationFolder + name + "_acro.pdf";
            String outTaggedPdfPathFlatted = destinationFolder + name + "_acro_flatten.pdf";
            String cmpPdfPath = sourceFolder + "cmp_" + name + ".pdf";
            String cmpPdfPathAcro = sourceFolder + "cmp_" + name + "_acro.pdf";
            String cmpPdfPathAcroFlatten = sourceFolder + "cmp_" + name + "_acro_flatten.pdf";
            String diff1 = "diff1_" + name;
            String diff2 = "diff2_" + name;
            String diff3 = "diff3_" + name;
            //convert tagged PDF without acroform (from html with form elements)
            PdfWriter taggedWriter = new PdfWriter(outTaggedPdfPath);
            PdfDocument pdfTagged = new PdfDocument(taggedWriter);
            pdfTagged.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(htmlPath, FileMode.Open, FileAccess.Read), pdfTagged);
            //convert tagged PDF with acroform
            PdfWriter taggedWriterAcro = new PdfWriter(outTaggedPdfPathAcro);
            PdfDocument pdfTaggedAcro = new PdfDocument(taggedWriterAcro);
            pdfTaggedAcro.SetTagged();
            ConverterProperties converterPropertiesAcro = new ConverterProperties();
            converterPropertiesAcro.SetCreateAcroForm(true);
            HtmlConverter.ConvertToPdf(new FileStream(htmlPath, FileMode.Open, FileAccess.Read), pdfTaggedAcro, converterPropertiesAcro
                );
            //flatted created tagged PDF with acroform
            PdfDocument document = new PdfDocument(new PdfReader(outTaggedPdfPathAcro), new PdfWriter(outTaggedPdfPathFlatted
                ));
            PdfAcroForm.GetAcroForm(document, false).FlattenFields();
            document.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outTaggedPdfPath, cmpPdfPath, destinationFolder
                , diff1));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outTaggedPdfPathAcro, cmpPdfPathAcro, destinationFolder
                , diff2));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outTaggedPdfPathFlatted, cmpPdfPathAcroFlatten
                , destinationFolder, diff3));
        }
    }
}
