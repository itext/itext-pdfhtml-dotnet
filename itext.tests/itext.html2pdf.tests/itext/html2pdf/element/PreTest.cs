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
using iText.Commons.Utils;
using iText.Html2pdf;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class PreTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/PreTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/PreTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Pre01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "preTest01.html"), new FileInfo(destinationFolder +
                 "preTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "preTest01.pdf", sourceFolder
                 + "cmp_preTest01.pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        public virtual void Pre02Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "preTest02.html"), new FileInfo(destinationFolder +
                 "preTest02.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "preTest02.pdf", sourceFolder
                 + "cmp_preTest02.pdf", destinationFolder, "diff02_"));
        }

        [NUnit.Framework.Test]
        public virtual void Pre03Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "preTest03.html"), new FileInfo(destinationFolder +
                 "preTest03.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "preTest03.pdf", sourceFolder
                 + "cmp_preTest03.pdf", destinationFolder, "diff03_"));
        }

        [NUnit.Framework.Test]
        public virtual void Pre04Test() {
            String html = "<html>\r\n\r\n<body>\r\n    " + "<pre style=\"background-color: mistyrose\">Write your text here and convert it to PDF.\r\n    </pre>"
                 + "\r\n</body>\r\n\r\n</html>";
            HtmlConverter.ConvertToPdf(html, FileUtil.GetFileOutputStream((destinationFolder + "preTest04.pdf")));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "preTest04.pdf", sourceFolder
                 + "cmp_preTest04.pdf", destinationFolder, "diff04_"));
        }
    }
}
