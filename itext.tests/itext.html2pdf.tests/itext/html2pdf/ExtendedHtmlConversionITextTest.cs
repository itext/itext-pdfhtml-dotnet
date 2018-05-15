using System;
using System.IO;
using iText.Forms;
using iText.IO.Util;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Element;
using iText.Test;

namespace iText.Html2pdf {
    /// <summary>
    /// This class is used for testing of pdfHTML conversion cases
    /// extends ExtendedITextTest test class
    /// </summary>
    public abstract class ExtendedHtmlConversionITextTest : ExtendedITextTest {
        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        public virtual void ConvertToPdfAndCompare(String name, String sourceFolder, String destinationFolder) {
            ConvertToPdfAndCompare(name, sourceFolder, destinationFolder, false, sourceFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        public virtual void ConvertToPdfAndCompare(String name, String sourceFolder, String destinationFolder, bool
             tagged) {
            ConvertToPdfAndCompare(name, sourceFolder, destinationFolder, tagged, sourceFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        public virtual void ConvertToPdfAndCompare(String name, String sourceFolder, String destinationFolder, bool
             tagged, String fontsFolder) {
            String sourceHtml = sourceFolder + name + ".html";
            String cmpPdf = sourceFolder + "cmp_" + name + ".pdf";
            String destinationPdf = destinationFolder + name + ".pdf";
            ConverterProperties converterProperties = GetConverterProperties(fontsFolder);
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationPdf));
            if (tagged) {
                pdfDocument.SetTagged();
            }
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, pdfDocument, converterProperties);
            }
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(sourceHtml).AbsolutePath + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationPdf, cmpPdf, destinationFolder
                , "diff_" + name + "_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        public virtual void ConvertToElementsAndCompare(String name, String sourceFolder, String destinationFolder
            ) {
            ConvertToElementsAndCompare(name, sourceFolder, destinationFolder, false, sourceFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        public virtual void ConvertToElementsAndCompare(String name, String sourceFolder, String destinationFolder
            , bool tagged) {
            ConvertToElementsAndCompare(name, sourceFolder, destinationFolder, tagged, sourceFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        public virtual void ConvertToElementsAndCompare(String name, String sourceFolder, String destinationFolder
            , bool tagged, String fontsFolder) {
            String sourceHtml = sourceFolder + name + ".html";
            String cmpPdf = sourceFolder + "cmp_" + name + ".pdf";
            String destinationPdf = destinationFolder + name + ".pdf";
            ConverterProperties converterProperties = GetConverterProperties(fontsFolder);
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationPdf));
            if (tagged) {
                pdfDocument.SetTagged();
            }
            Document document = new Document(pdfDocument);
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                foreach (IElement element in HtmlConverter.ConvertToElements(fileInputStream, converterProperties)) {
                    document.Add((IBlockElement)element);
                }
            }
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(sourceHtml).AbsolutePath + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationPdf, cmpPdf, destinationFolder
                , "diff_" + name + "_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        /// <exception cref="Javax.Xml.Parsers.ParserConfigurationException"/>
        /// <exception cref="Org.Xml.Sax.SAXException"/>
        public virtual void ConvertToPdfAcroformFlattenAndCompare(String name, String sourceFolder, String destinationFolder
            , bool tagged) {
            String sourceHtml = sourceFolder + name + ".html";
            if (tagged) {
                name = name + "Tagged";
            }
            String outPdfPath = destinationFolder + name + ".pdf";
            String outPdfPathAcro = destinationFolder + name + "_acro.pdf";
            String outPdfPathFlatted = destinationFolder + name + "_acro_flatten.pdf";
            String cmpPdfPath = sourceFolder + "cmp_" + name + ".pdf";
            String cmpPdfPathAcro = sourceFolder + "cmp_" + name + "_acro.pdf";
            String cmpPdfPathAcroFlatten = sourceFolder + "cmp_" + name + "_acro_flatten.pdf";
            String diff1 = "diff1_" + name;
            String diff2 = "diff2_" + name;
            String diff3 = "diff3_" + name;
            //convert tagged PDF without acroform (from html with form elements)
            PdfWriter taggedWriter = new PdfWriter(outPdfPath);
            PdfDocument pdfTagged = new PdfDocument(taggedWriter);
            if (tagged) {
                pdfTagged.SetTagged();
            }
            HtmlConverter.ConvertToPdf(new FileStream(sourceHtml, FileMode.Open, FileAccess.Read), pdfTagged, new ConverterProperties
                ().SetBaseUri(sourceFolder));
            //convert PDF with acroform
            PdfWriter writerAcro = new PdfWriter(outPdfPathAcro);
            PdfDocument pdfTaggedAcro = new PdfDocument(writerAcro);
            if (tagged) {
                pdfTaggedAcro.SetTagged();
            }
            ConverterProperties converterPropertiesAcro = new ConverterProperties();
            converterPropertiesAcro.SetBaseUri(sourceFolder);
            converterPropertiesAcro.SetCreateAcroForm(true);
            HtmlConverter.ConvertToPdf(new FileStream(sourceHtml, FileMode.Open, FileAccess.Read), pdfTaggedAcro, converterPropertiesAcro
                );
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(sourceHtml).AbsolutePath + "\n");
            //flatted created tagged PDF with acroform
            PdfDocument document = new PdfDocument(new PdfReader(outPdfPathAcro), new PdfWriter(outPdfPathFlatted));
            PdfAcroForm acroForm = PdfAcroForm.GetAcroForm(document, false);
            acroForm.FlattenFields();
            document.Close();
            //compare with cmp
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdfPath, cmpPdfPath, destinationFolder
                , diff1));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdfPathAcro, cmpPdfPathAcro, destinationFolder
                , diff2));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdfPathFlatted, cmpPdfPathAcroFlatten, 
                destinationFolder, diff3));
            //compare tags structure if tagged
            if (tagged) {
                CompareTagStructure(outPdfPath, cmpPdfPath);
                CompareTagStructure(outPdfPathAcro, cmpPdfPathAcro);
                CompareTagStructure(outPdfPathFlatted, cmpPdfPathAcroFlatten);
            }
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

        private ConverterProperties GetConverterProperties(String fontsFolder) {
            ConverterProperties properties = new ConverterProperties().SetBaseUri(fontsFolder);
            return properties;
        }
    }
}
