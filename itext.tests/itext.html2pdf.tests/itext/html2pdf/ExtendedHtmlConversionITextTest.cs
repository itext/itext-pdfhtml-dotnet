/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
using iText.Forms.Fields;
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
        public virtual void ConvertToPdfAndCompare(String name, String sourceFolder, String destinationFolder) {
            ConvertToPdfAndCompare(name, sourceFolder, destinationFolder, false, sourceFolder);
        }

        public virtual void ConvertToPdfAndCompare(String name, String sourceFolder, String destinationFolder, bool
             tagged) {
            ConvertToPdfAndCompare(name, sourceFolder, destinationFolder, tagged, sourceFolder);
        }

        public virtual void ConvertToPdfAndCompare(String name, String sourceFolder, String destinationFolder, bool
             tagged, String fontsFolder) {
            ConverterProperties converterProperties = GetConverterProperties(fontsFolder);
            ConvertToPdfAndCompare(name, sourceFolder, destinationFolder, tagged, converterProperties);
        }

        public virtual void ConvertToPdfAndCompare(String name, String sourceFolder, String destinationFolder, bool
             tagged, ConverterProperties converterProperties) {
            String sourceHtml = sourceFolder + name + ".html";
            String cmpPdf = sourceFolder + "cmp_" + name + ".pdf";
            String destinationPdf = destinationFolder + name + ".pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationPdf));
            if (tagged) {
                pdfDocument.SetTagged();
            }
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, pdfDocument, null == converterProperties ? new ConverterProperties
                    () : converterProperties);
            }
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(sourceHtml) + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationPdf, cmpPdf, destinationFolder
                , "diff_" + name + "_"));
        }

        public virtual void ConvertToElementsAndCompare(String name, String sourceFolder, String destinationFolder
            ) {
            ConvertToElementsAndCompare(name, sourceFolder, destinationFolder, false, sourceFolder);
        }

        public virtual void ConvertToElementsAndCompare(String name, String sourceFolder, String destinationFolder
            , bool tagged) {
            ConvertToElementsAndCompare(name, sourceFolder, destinationFolder, tagged, sourceFolder);
        }

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
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(sourceHtml) + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationPdf, cmpPdf, destinationFolder
                , "diff_" + name + "_"));
        }

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
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(sourceHtml) + "\n");
            //flatted created tagged PDF with acroform
            PdfDocument document = new PdfDocument(new PdfReader(outPdfPathAcro), new PdfWriter(outPdfPathFlatted));
            PdfAcroForm acroForm = PdfFormCreator.GetAcroForm(document, false);
            acroForm.FlattenFields();
            document.Close();
            //compare with cmp
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdfPath, cmpPdfPath, destinationFolder
                ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdfPathAcro, cmpPdfPathAcro, destinationFolder
                ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdfPathFlatted, cmpPdfPathAcroFlatten, 
                destinationFolder));
            //compare tags structure if tagged
            if (tagged) {
                CompareTagStructure(outPdfPath, cmpPdfPath);
                CompareTagStructure(outPdfPathAcro, cmpPdfPathAcro);
                CompareTagStructure(outPdfPathFlatted, cmpPdfPathAcroFlatten);
            }
        }

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
