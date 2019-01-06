/*
This file is part of the iText (R) project.
Copyright (c) 1998-2019 iText Group NV
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
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    public class HtmlTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/HtmlTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/HtmlTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Html01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "htmlTest01.html"), new FileInfo(destinationFolder 
                + "htmlTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "htmlTest01.pdf", sourceFolder
                 + "cmp_htmlTest01.pdf", destinationFolder, "diff01_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.NO_WORKER_FOUND_FOR_TAG)]
        public virtual void Html02Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "htmlTest02.html"), new FileInfo(destinationFolder 
                + "htmlTest02.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "htmlTest02.pdf", sourceFolder
                 + "cmp_htmlTest02.pdf", destinationFolder, "diff02_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Html03Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "htmlTest03.html"), new FileInfo(destinationFolder 
                + "htmlTest03.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "htmlTest03.pdf", sourceFolder
                 + "cmp_htmlTest03.pdf", destinationFolder, "diff03_"));
        }

        // this test is both for html and body
        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Html04Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "htmlTest04.html"), new FileInfo(destinationFolder 
                + "htmlTest04.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "htmlTest04.pdf", sourceFolder
                 + "cmp_htmlTest04.pdf", destinationFolder, "diff04_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Html05Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "htmlTest05.html"), new FileInfo(destinationFolder 
                + "htmlTest05.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "htmlTest05.pdf", sourceFolder
                 + "cmp_htmlTest05.pdf", destinationFolder, "diff05_"));
        }

        // this test is both for html and body
        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Html06Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "htmlTest06.html"), new FileInfo(destinationFolder 
                + "htmlTest06.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "htmlTest06.pdf", sourceFolder
                 + "cmp_htmlTest06.pdf", destinationFolder, "diff06_"));
        }

        // this test is both for html and body
        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Html07Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "htmlTest07.html"), new FileInfo(destinationFolder 
                + "htmlTest07.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "htmlTest07.pdf", sourceFolder
                 + "cmp_htmlTest07.pdf", destinationFolder, "diff07_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Html08Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "htmlTest08.html"), new FileInfo(destinationFolder 
                + "htmlTest08.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "htmlTest08.pdf", sourceFolder
                 + "cmp_htmlTest08.pdf", destinationFolder, "diff08_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void Html09Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "htmlTest09.html"), new FileInfo(destinationFolder 
                + "htmlTest09.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "htmlTest09.pdf", sourceFolder
                 + "cmp_htmlTest09.pdf", destinationFolder, "diff09_"));
        }
    }
}
