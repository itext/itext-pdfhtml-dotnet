/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 iText Group NV
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
            PdfAcroForm acroForm = PdfAcroForm.GetAcroForm(document, false);
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
