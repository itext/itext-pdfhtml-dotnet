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
using iText.Html2pdf;
using iText.IO.Util;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Pdfa;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    public class LinkTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/LinkTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/LinkTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void LinkTest01() {
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(sourceFolder + "linkTest01.html"
                ) + "\n");
            PdfDocument outDoc = new PdfDocument(new PdfWriter(destinationFolder + "linkTest01.pdf"));
            outDoc.SetTagged();
            using (FileStream fileInputStream = new FileStream(sourceFolder + "linkTest01.html", FileMode.Open, FileAccess.Read
                )) {
                HtmlConverter.ConvertToPdf(fileInputStream, outDoc);
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "linkTest01.pdf", sourceFolder
                 + "cmp_linkTest01.pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        public virtual void LinkTest02() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "linkTest02.html"), new FileInfo(destinationFolder 
                + "linkTest02.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "linkTest02.pdf", sourceFolder
                 + "cmp_linkTest02.pdf", destinationFolder, "diff02_"));
        }

        [NUnit.Framework.Test]
        public virtual void LinkTest03() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "linkTest03.html"), new FileInfo(destinationFolder 
                + "linkTest03.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "linkTest03.pdf", sourceFolder
                 + "cmp_linkTest03.pdf", destinationFolder, "diff03_"));
        }

        [NUnit.Framework.Test]
        public virtual void LinkTest04() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "linkTest04.html"), new FileInfo(destinationFolder 
                + "linkTest04.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "linkTest04.pdf", sourceFolder
                 + "cmp_linkTest04.pdf", destinationFolder, "diff04_"));
        }

        [NUnit.Framework.Test]
        public virtual void LinkTest05() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "linkTest05.html"), new FileInfo(destinationFolder 
                + "linkTest05.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "linkTest05.pdf", sourceFolder
                 + "cmp_linkTest05.pdf", destinationFolder, "diff05_"));
        }

        [NUnit.Framework.Test]
        public virtual void LinkTest06() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "linkTest06.html"), new FileInfo(destinationFolder 
                + "linkTest06.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "linkTest06.pdf", sourceFolder
                 + "cmp_linkTest06.pdf", destinationFolder, "diff06_"));
        }

        [NUnit.Framework.Test]
        public virtual void LinkTest07() {
            PdfDocument outDoc = new PdfDocument(new PdfWriter(destinationFolder + "linkTest07.pdf"));
            outDoc.SetTagged();
            using (FileStream fileInputStream = new FileStream(sourceFolder + "linkTest07.html", FileMode.Open, FileAccess.Read
                )) {
                HtmlConverter.ConvertToPdf(fileInputStream, outDoc);
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "linkTest07.pdf", sourceFolder
                 + "cmp_linkTest07.pdf", destinationFolder, "diff07_"));
        }

        [NUnit.Framework.Test]
        public virtual void LinkTest09() {
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationFolder + "linkTest09.pdf"));
            pdfDocument.SetTagged();
            using (FileStream fileInputStream = new FileStream(sourceFolder + "linkTest09.html", FileMode.Open, FileAccess.Read
                )) {
                HtmlConverter.ConvertToPdf(fileInputStream, pdfDocument, new ConverterProperties().SetBaseUri(sourceFolder
                    ));
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "linkTest09.pdf", sourceFolder
                 + "cmp_linkTest09.pdf", destinationFolder, "diff09_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(PdfAConformanceLogMessageConstant.CATALOG_SHOULD_CONTAIN_LANG_ENTRY)]
        public virtual void LinkTest10ToPdfa() {
            Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read);
            PdfADocument pdfADocument = new PdfADocument(new PdfWriter(destinationFolder + "linkTest10.pdf"), PdfAConformanceLevel
                .PDF_A_2A, new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", @is));
            pdfADocument.SetTagged();
            using (FileStream fileInputStream = new FileStream(sourceFolder + "linkTest10.html", FileMode.Open, FileAccess.Read
                )) {
                HtmlConverter.ConvertToPdf(fileInputStream, pdfADocument);
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "linkTest10.pdf", sourceFolder
                 + "cmp_linkTest10.pdf", destinationFolder, "diff10_"));
        }

        [NUnit.Framework.Test]
        public virtual void LinkTest11() {
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationFolder + "linkTest11.pdf"));
            pdfDocument.SetTagged();
            using (FileStream fileInputStream = new FileStream(sourceFolder + "linkTest11.html", FileMode.Open, FileAccess.Read
                )) {
                HtmlConverter.ConvertToPdf(fileInputStream, pdfDocument, new ConverterProperties().SetBaseUri(sourceFolder
                    ));
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "linkTest11.pdf", sourceFolder
                 + "cmp_linkTest11.pdf", destinationFolder, "diff11_"));
        }

        [NUnit.Framework.Test]
        public virtual void LinkTest12() {
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationFolder + "linkTest12.pdf"));
            pdfDocument.SetTagged();
            using (FileStream fileInputStream = new FileStream(sourceFolder + "linkTest12.html", FileMode.Open, FileAccess.Read
                )) {
                HtmlConverter.ConvertToPdf(fileInputStream, pdfDocument, new ConverterProperties().SetBaseUri("https://en.wikipedia.org/wiki/"
                    ));
            }
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "linkTest12.pdf", sourceFolder
                 + "cmp_linkTest12.pdf", destinationFolder, "diff12_"));
        }

        [NUnit.Framework.Test]
        public virtual void AnchorLinkToSpanTest01() {
            String fileName = "anchorLinkToSpanTest01";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + fileName + ".html"), new FileInfo(destinationFolder
                 + fileName + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + fileName + ".pdf", sourceFolder
                 + "cmp_" + fileName + ".pdf", destinationFolder));
        }
    }
}
