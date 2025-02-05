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
using iText.Html2pdf.Logs;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class HtmlTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/HtmlTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/HtmlTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Html01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "htmlTest01.html"), new FileInfo(destinationFolder 
                + "htmlTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "htmlTest01.pdf", sourceFolder
                 + "cmp_htmlTest01.pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.NO_WORKER_FOUND_FOR_TAG)]
        public virtual void Html02Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "htmlTest02.html"), new FileInfo(destinationFolder 
                + "htmlTest02.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "htmlTest02.pdf", sourceFolder
                 + "cmp_htmlTest02.pdf", destinationFolder, "diff02_"));
        }

        [NUnit.Framework.Test]
        public virtual void Html03Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "htmlTest03.html"), new FileInfo(destinationFolder 
                + "htmlTest03.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "htmlTest03.pdf", sourceFolder
                 + "cmp_htmlTest03.pdf", destinationFolder, "diff03_"));
        }

        // this test is both for html and body
        [NUnit.Framework.Test]
        public virtual void Html04Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "htmlTest04.html"), new FileInfo(destinationFolder 
                + "htmlTest04.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "htmlTest04.pdf", sourceFolder
                 + "cmp_htmlTest04.pdf", destinationFolder, "diff04_"));
        }

        [NUnit.Framework.Test]
        public virtual void Html05Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "htmlTest05.html"), new FileInfo(destinationFolder 
                + "htmlTest05.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "htmlTest05.pdf", sourceFolder
                 + "cmp_htmlTest05.pdf", destinationFolder, "diff05_"));
        }

        // this test is both for html and body
        [NUnit.Framework.Test]
        public virtual void Html06Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "htmlTest06.html"), new FileInfo(destinationFolder 
                + "htmlTest06.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "htmlTest06.pdf", sourceFolder
                 + "cmp_htmlTest06.pdf", destinationFolder, "diff06_"));
        }

        // this test is both for html and body
        [NUnit.Framework.Test]
        public virtual void Html07Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "htmlTest07.html"), new FileInfo(destinationFolder 
                + "htmlTest07.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "htmlTest07.pdf", sourceFolder
                 + "cmp_htmlTest07.pdf", destinationFolder, "diff07_"));
        }

        [NUnit.Framework.Test]
        public virtual void Html08Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "htmlTest08.html"), new FileInfo(destinationFolder 
                + "htmlTest08.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "htmlTest08.pdf", sourceFolder
                 + "cmp_htmlTest08.pdf", destinationFolder, "diff08_"));
        }

        //TODO replace cmp file when fixing DEVSIX-7303
        [NUnit.Framework.Test]
        public virtual void Html09Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "htmlTest09.html"), new FileInfo(destinationFolder 
                + "htmlTest09.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "htmlTest09.pdf", sourceFolder
                 + "cmp_htmlTest09.pdf", destinationFolder, "diff09_"));
        }
    }
}
