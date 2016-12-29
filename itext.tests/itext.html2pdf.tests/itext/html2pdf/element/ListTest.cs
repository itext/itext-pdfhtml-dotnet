/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2017 iText Group NV
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
    address: sales@itextpdf.com */
using System;
using System.IO;
using iText.Html2pdf;
using iText.Html2pdf.Css.Media;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Versions.Attributes;
using iText.Kernel;
using iText.Pdfa;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    public class ListTest : ExtendedITextTest {
        public static readonly String sourceFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory + "/../../resources/itext/html2pdf/element/ListTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/ListTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
                );
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ListTest01() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "listTest01.html"), new FileInfo(destinationFolder 
                + "listTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "listTest01.pdf", sourceFolder
                 + "cmp_listTest01.pdf", destinationFolder, "diff01_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ListTest02() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "listTest02.html"), new FileInfo(destinationFolder 
                + "listTest02.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "listTest02.pdf", sourceFolder
                 + "cmp_listTest02.pdf", destinationFolder, "diff02_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ListTest03() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "listTest03.html"), new FileInfo(destinationFolder 
                + "listTest03.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "listTest03.pdf", sourceFolder
                 + "cmp_listTest03.pdf", destinationFolder, "diff03_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ListTest04() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "listTest04.html"), new FileInfo(destinationFolder 
                + "listTest04.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "listTest04.pdf", sourceFolder
                 + "cmp_listTest04.pdf", destinationFolder, "diff04_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NOT_SUPPORTED_LIST_STYLE_TYPE, Count = 32)]
        public virtual void ListTest05() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "listTest05.html"), new FileInfo(destinationFolder 
                + "listTest05.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "listTest05.pdf", sourceFolder
                 + "cmp_listTest05.pdf", destinationFolder, "diff05_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ListTest06() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "listTest06.html"), new FileInfo(destinationFolder 
                + "listTest06.pdf"), new ConverterProperties().SetMediaDeviceDescription(new MediaDeviceDescription(MediaType
                .PRINT)));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "listTest06.pdf", sourceFolder
                 + "cmp_listTest06.pdf", destinationFolder, "diff06_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ListTest07() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "listTest07.html"), new FileInfo(destinationFolder 
                + "listTest07.pdf"), new ConverterProperties().SetMediaDeviceDescription(new MediaDeviceDescription(MediaType
                .PRINT)));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "listTest07.pdf", sourceFolder
                 + "cmp_listTest07.pdf", destinationFolder, "diff07_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ListTest08() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "listTest08.html"), new FileInfo(destinationFolder 
                + "listTest08.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "listTest08.pdf", sourceFolder
                 + "cmp_listTest08.pdf", destinationFolder, "diff08_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ListTest09() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "listTest09.html"), new FileInfo(destinationFolder 
                + "listTest09.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "listTest09.pdf", sourceFolder
                 + "cmp_listTest09.pdf", destinationFolder, "diff09_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ListTest10() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "listTest10.html"), new FileInfo(destinationFolder 
                + "listTest10.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "listTest10.pdf", sourceFolder
                 + "cmp_listTest10.pdf", destinationFolder, "diff10_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ListTest11() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "listTest11.html"), new FileInfo(destinationFolder 
                + "listTest11.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "listTest11.pdf", sourceFolder
                 + "cmp_listTest11.pdf", destinationFolder, "diff11_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ListTest12() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "listTest12.html"), new FileInfo(destinationFolder 
                + "listTest12.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "listTest12.pdf", sourceFolder
                 + "cmp_listTest12.pdf", destinationFolder, "diff12_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("Conversion to Pdf/A for lists not supported. DEVSIX-917")]
        public virtual void ListToPdfaTest() {
            Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm", FileMode.Open, FileAccess.Read);
            PdfADocument pdfADocument = new PdfADocument(new PdfWriter(destinationFolder + "listToPdfa.pdf"), PdfAConformanceLevel
                .PDF_A_1B, new PdfOutputIntent("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", @is));
            HtmlConverter.ConvertToPdf(new FileStream(sourceFolder + "listToPdfa.html", FileMode.Open, FileAccess.Read
                ), pdfADocument, new ConverterProperties().SetMediaDeviceDescription(new MediaDeviceDescription(MediaType
                .PRINT)));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "listToPdfa.pdf", sourceFolder
                 + "cmp_listToPdfa.pdf", destinationFolder, "diff99_"));
        }
    }
}
