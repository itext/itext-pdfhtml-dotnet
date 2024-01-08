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
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;

namespace iText.Html2pdf.Attribute {
    [NUnit.Framework.Category("IntegrationTest")]
    public class AlignAttributeTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/attribute/AlignAttributeTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/attribute/AlignAttributeTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AlignImgTest01() {
            // vertical-alignment values top, middle and bottom are not currently supported for inline-block elements and images
            ConvertToPdfAndCompare("alignImgTest01", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ImgAttrAlignLeftReadOrderPdfTest() {
            String pdfDestinationFile = destinationFolder + "imgAttrAlignLeftReadOrderPdf.pdf";
            String htmlSource = sourceFolder + "imgAttrAlignLeftReadOrderPdf.html";
            String cmpPdfDestinationFile = sourceFolder + "cmp_imgAttrAlignLeftReadOrderPdf.pdf";
            FileStream fileOutputStream = new FileStream(pdfDestinationFile, FileMode.Create);
            WriterProperties writerProperties = new WriterProperties();
            writerProperties.AddXmpMetadata();
            PdfWriter pdfWriter = new PdfWriter(fileOutputStream, writerProperties);
            PdfDocument pdfDocument = new PdfDocument(pdfWriter);
            pdfDocument.GetCatalog().SetLang(new PdfString("en-US"));
            pdfDocument.SetTagged();
            ConverterProperties converterProperties = new ConverterProperties();
            converterProperties.SetBaseUri(sourceFolder);
            FileStream fileInputStream = new FileStream(htmlSource, FileMode.Open, FileAccess.Read);
            HtmlConverter.ConvertToPdf(fileInputStream, pdfDocument, converterProperties);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(pdfDestinationFile, cmpPdfDestinationFile
                , destinationFolder));
        }
    }
}
