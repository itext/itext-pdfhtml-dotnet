using System;
using System.IO;
using iText.Forms;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Element {
    public class TagsInsideButtonTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/TagsInsideButtonTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/TagsInsideButtonTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ButtonWithImageInside() {
            RunConversionAcroformAndFlatten("buttonWithImageInside");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        /// <exception cref="Javax.Xml.Parsers.ParserConfigurationException"/>
        /// <exception cref="Org.Xml.Sax.SAXException"/>
        [NUnit.Framework.Test]
        public virtual void ButtonWithImageInsideTagged() {
            RunTaggedConversionAcroformAndFlatten("buttonWithImageInside");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ButtonWithPInside() {
            RunConversionAcroformAndFlatten("buttonWithPInside");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        /// <exception cref="Javax.Xml.Parsers.ParserConfigurationException"/>
        /// <exception cref="Org.Xml.Sax.SAXException"/>
        [NUnit.Framework.Test]
        public virtual void ButtonWithPInsideTagged() {
            RunTaggedConversionAcroformAndFlatten("buttonWithPInside");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        private void RunConversionAcroformAndFlatten(String name) {
            String htmlPath = sourceFolder + name + ".html";
            String outPdfPath = destinationFolder + name + ".pdf";
            String outAcroPdfPath = destinationFolder + name + "_acro.pdf";
            String outAcroFlattenPdfPath = destinationFolder + name + "_acro_flatten.pdf";
            String cmpPdfPath = sourceFolder + "cmp_" + name + ".pdf";
            String cmpAcroPdfPath = sourceFolder + "cmp_" + name + "_acro.pdf";
            String cmpAcroFlattenPdfPath = sourceFolder + "cmp_" + name + "_acro_flatten.pdf";
            String diff = "diff_" + name + "_";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(outPdfPath), new ConverterProperties().SetBaseUri
                (sourceFolder));
            HtmlConverter.ConvertToPdf(new FileInfo(htmlPath), new FileInfo(outAcroPdfPath), new ConverterProperties()
                .SetCreateAcroForm(true).SetBaseUri(sourceFolder));
            PdfDocument document = new PdfDocument(new PdfReader(outAcroPdfPath), new PdfWriter(outAcroFlattenPdfPath)
                );
            PdfAcroForm acroForm = PdfAcroForm.GetAcroForm(document, false);
            acroForm.FlattenFields();
            document.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdfPath, cmpPdfPath, destinationFolder
                , diff));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outAcroPdfPath, cmpAcroPdfPath, destinationFolder
                , diff));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outAcroFlattenPdfPath, cmpAcroFlattenPdfPath
                , destinationFolder, diff));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        /// <exception cref="Javax.Xml.Parsers.ParserConfigurationException"/>
        /// <exception cref="Org.Xml.Sax.SAXException"/>
        private void RunTaggedConversionAcroformAndFlatten(String name) {
            String htmlPath = sourceFolder + name + ".html";
            name = name + "Tagged";
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
            HtmlConverter.ConvertToPdf(new FileStream(htmlPath, FileMode.Open, FileAccess.Read), pdfTagged, new ConverterProperties
                ().SetBaseUri(sourceFolder));
            //convert tagged PDF with acroform
            PdfWriter taggedWriterAcro = new PdfWriter(outTaggedPdfPathAcro);
            PdfDocument pdfTaggedAcro = new PdfDocument(taggedWriterAcro);
            pdfTaggedAcro.SetTagged();
            ConverterProperties converterPropertiesAcro = new ConverterProperties();
            converterPropertiesAcro.SetBaseUri(sourceFolder);
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
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outTaggedPdfPath, cmpPdfPath, destinationFolder
                , diff1));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outTaggedPdfPathAcro, cmpPdfPathAcro, destinationFolder
                , diff2));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outTaggedPdfPathFlatted, cmpPdfPathAcroFlatten
                , destinationFolder, diff3));
            //compare tags structure
            CompareTagStructure(outTaggedPdfPath, cmpPdfPath);
            CompareTagStructure(outTaggedPdfPathAcro, cmpPdfPathAcro);
            CompareTagStructure(outTaggedPdfPathFlatted, cmpPdfPathAcroFlatten);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        /// <exception cref="Javax.Xml.Parsers.ParserConfigurationException"/>
        /// <exception cref="Org.Xml.Sax.SAXException"/>
        private void CompareTagStructure(String outPath, String cmpPath) {
            CompareTool compareTool = new CompareTool();
            String tagStructureErrors = compareTool.CompareTagStructures(outPath, cmpPath);
            String resultMessage = "";
            if (tagStructureErrors != null) {
                resultMessage += tagStructureErrors + "\n";
            }
            NUnit.Framework.Assert.IsTrue(tagStructureErrors == null, resultMessage);
        }
    }
}
