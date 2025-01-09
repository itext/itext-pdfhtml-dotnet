/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
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
using iText.Forms.Logs;
using iText.Html2pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class BrTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/BrTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/BrTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Br01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "brTest01.html"), new FileInfo(destinationFolder + 
                "brTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "brTest01.pdf", sourceFolder
                 + "cmp_brTest01.pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        public virtual void Br02Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "brTest02.html"), new FileInfo(destinationFolder + 
                "brTest02.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "brTest02.pdf", sourceFolder
                 + "cmp_brTest02.pdf", destinationFolder, "diff02_"));
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-1655")]
        public virtual void Br03Test() {
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(destinationFolder + "brTest03.pdf"));
            pdfDoc.SetDefaultPageSize(new PageSize(72, 72));
            HtmlConverter.ConvertToPdf(new FileStream(sourceFolder + "brTest03.html", FileMode.Open, FileAccess.Read), 
                pdfDoc, new ConverterProperties());
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "brTest03.pdf", sourceFolder
                 + "cmp_brTest03.pdf", destinationFolder, "diff03_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(FormsLogMessageConstants.DUPLICATE_EXPORT_VALUE, Count = 1)]
        public virtual void BrInsideDifferentTagsTest01() {
            // TODO DEVSIX-2092
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "brInsideDifferentTagsTest01.html"), new FileInfo(destinationFolder
                 + "brInsideDifferentTagsTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "brInsideDifferentTagsTest01.pdf"
                , sourceFolder + "cmp_brInsideDifferentTagsTest01.pdf", destinationFolder, "diff04_"));
        }

        [NUnit.Framework.Test]
        public virtual void BrSmallFontSizeTest() {
            // TODO DEVSIX-6070 <br> tag create too much space with a small font size
            ConvertToPdfAndCompare("brSmallFontSize", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BrClearNoneTest() {
            ConvertToPdfAndCompare("brClearNone", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TaggedBrTest() {
            // TODO: DEVSIX-8698 creates an empty tag for the br tag
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationFolder + "taggedBr.pdf"));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(sourceFolder + "taggedBr.html", FileMode.Open, FileAccess.Read), 
                pdfDocument, new ConverterProperties());
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "taggedBr.pdf", sourceFolder
                 + "cmp_taggedBr.pdf", destinationFolder, "diff05_"));
        }
    }
}
