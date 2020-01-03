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
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Element {
    public class BodyTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/BodyTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/BodyTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Body01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "bodyTest01.html"), new FileInfo(destinationFolder 
                + "bodyTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "bodyTest01.pdf", sourceFolder
                 + "cmp_bodyTest01.pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        public virtual void Body02Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "bodyTest02.html"), new FileInfo(destinationFolder 
                + "bodyTest02.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "bodyTest02.pdf", sourceFolder
                 + "cmp_bodyTest02.pdf", destinationFolder, "diff02_"));
        }

        [NUnit.Framework.Test]
        public virtual void Body03Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "bodyTest03.html"), new FileInfo(destinationFolder 
                + "bodyTest03.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "bodyTest03.pdf", sourceFolder
                 + "cmp_bodyTest03.pdf", destinationFolder, "diff03_"));
        }

        [NUnit.Framework.Test]
        public virtual void Body04Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "bodyTest04.html"), new FileInfo(destinationFolder 
                + "bodyTest04.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "bodyTest04.pdf", sourceFolder
                 + "cmp_bodyTest04.pdf", destinationFolder, "diff04_"));
        }

        [NUnit.Framework.Test]
        public virtual void Body05Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "bodyTest05.html"), new FileInfo(destinationFolder 
                + "bodyTest05.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "bodyTest05.pdf", sourceFolder
                 + "cmp_bodyTest05.pdf", destinationFolder, "diff05_"));
        }

        [NUnit.Framework.Test]
        public virtual void Body06Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "bodyTest06.html"), new FileInfo(destinationFolder 
                + "bodyTest06.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "bodyTest06.pdf", sourceFolder
                 + "cmp_bodyTest06.pdf", destinationFolder, "diff06_"));
        }

        // this test is both for html and body
        [NUnit.Framework.Test]
        public virtual void Body07Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "bodyTest07.html"), new FileInfo(destinationFolder 
                + "bodyTest07.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "bodyTest07.pdf", sourceFolder
                 + "cmp_bodyTest07.pdf", destinationFolder, "diff07_"));
        }

        // this test is both for html and body
        [NUnit.Framework.Test]
        public virtual void Body08Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "bodyTest08.html"), new FileInfo(destinationFolder 
                + "bodyTest08.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "bodyTest08.pdf", sourceFolder
                 + "cmp_bodyTest08.pdf", destinationFolder, "diff08_"));
        }

        // this test is both for html and body
        [NUnit.Framework.Test]
        public virtual void Body09Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "bodyTest09.html"), new FileInfo(destinationFolder 
                + "bodyTest09.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "bodyTest09.pdf", sourceFolder
                 + "cmp_bodyTest09.pdf", destinationFolder, "diff09_"));
        }
    }
}
