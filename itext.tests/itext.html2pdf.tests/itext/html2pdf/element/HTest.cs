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
using iText.Html2pdf;
using iText.Html2pdf.Attach.Impl;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class HTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/HTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/HTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void H1Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hTest01.html"), new FileInfo(destinationFolder + "hTest01.pdf"
                ), new ConverterProperties().SetOutlineHandler(OutlineHandler.CreateStandardHandler()));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hTest01.pdf", sourceFolder
                 + "cmp_hTest01.pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        public virtual void H2Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hTest02.html"), new FileInfo(destinationFolder + "hTest02.pdf"
                ), new ConverterProperties().SetOutlineHandler(OutlineHandler.CreateStandardHandler()));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hTest02.pdf", sourceFolder
                 + "cmp_hTest02.pdf", destinationFolder, "diff02_"));
        }

        [NUnit.Framework.Test]
        public virtual void H3Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hTest03.html"), new FileInfo(destinationFolder + "hTest03.pdf"
                ), new ConverterProperties().SetOutlineHandler(OutlineHandler.CreateStandardHandler()));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hTest03.pdf", sourceFolder
                 + "cmp_hTest03.pdf", destinationFolder, "diff03_"));
        }

        [NUnit.Framework.Test]
        public virtual void H4Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hTest04.html"), new FileInfo(destinationFolder + "hTest04.pdf"
                ), new ConverterProperties().SetOutlineHandler(OutlineHandler.CreateStandardHandler()));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hTest04.pdf", sourceFolder
                 + "cmp_hTest04.pdf", destinationFolder, "diff04_"));
        }

        [NUnit.Framework.Test]
        public virtual void H5Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hTest05.html"), new FileInfo(destinationFolder + "hTest05.pdf"
                ), new ConverterProperties().SetOutlineHandler(OutlineHandler.CreateStandardHandler()));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hTest05.pdf", sourceFolder
                 + "cmp_hTest05.pdf", destinationFolder, "diff05_"));
        }

        [NUnit.Framework.Test]
        public virtual void HTagRoleTest() {
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationFolder + "hTest06.pdf"));
            pdfDocument.SetTagged();
            HtmlConverter.ConvertToPdf(new FileStream(sourceFolder + "hTest06.html", FileMode.Open, FileAccess.Read), 
                pdfDocument, new ConverterProperties());
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hTest06.pdf", sourceFolder
                 + "cmp_hTest06.pdf", destinationFolder, "diff06_"));
        }
    }
}
