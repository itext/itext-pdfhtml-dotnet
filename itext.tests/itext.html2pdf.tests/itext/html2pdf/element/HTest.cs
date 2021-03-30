/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
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
using iText.Html2pdf;
using iText.Html2pdf.Attach.Impl;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Element {
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
