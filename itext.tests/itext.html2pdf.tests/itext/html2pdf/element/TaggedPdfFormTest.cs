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

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTextFieldTagged() {
            RunTest("simpleTextFieldTagged");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTextareaTagged() {
            RunTest("simpleTextareaTagged");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleButtonTagged() {
            RunTest("simpleButtonTagged");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleLabelTagged() {
            RunTest("simpleLabelTagged");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("InputTagWorker ERROR Input type checkbox is not supported")]
        public virtual void SimpleCheckboxTagged() {
            RunTest("simpleCheckboxTagged");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DefaultHtmlProcessor ERROR No worker found for tag select")]
        public virtual void SimpleSelectTagged() {
            RunTest("simpleSelectTagged");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("InputTagWorker ERROR Input type radio is not supported")]
        public virtual void SimpleRadioFormTagged() {
            RunTest("simpleRadioFormTagged");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DefaultHtmlProcessor ERROR No worker found for tag datalist")]
        public virtual void DatalistFormTagged() {
            RunTest("datalistFormTagged");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void FieldsetFormTagged() {
            RunTest("fieldsetFormTagged");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
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
            PdfAcroForm acroForm = PdfAcroForm.GetAcroForm(document, false);
            acroForm.FlattenFields();
            document.Close();
            //compare with cmp
            String compResult1 = new CompareTool().CompareByContent(outTaggedPdfPath, cmpPdfPath, destinationFolder, diff1
                );
            String compResult2 = new CompareTool().CompareByContent(outTaggedPdfPathAcro, cmpPdfPathAcro, destinationFolder
                , diff2);
            String compResult3 = new CompareTool().CompareByContent(outTaggedPdfPathFlatted, cmpPdfPathAcroFlatten, destinationFolder
                , diff3);
            NUnit.Framework.Assert.IsNull(compResult1);
            NUnit.Framework.Assert.IsNull(compResult2);
            NUnit.Framework.Assert.IsNull(compResult3);
        }
    }
}
