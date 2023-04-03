/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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
using iText.Kernel.Utils;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class BodyTest : ExtendedHtmlConversionITextTest {
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

        [NUnit.Framework.Test]
        public virtual void HelloMalformedDocumentTest() {
            ConvertToPdfAndCompare("hello_malformed", sourceFolder, destinationFolder);
        }
    }
}
