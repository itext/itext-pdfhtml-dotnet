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
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class TitleTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/TitleTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/TitleTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Title01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "titleTest01.html"), new FileInfo(destinationFolder
                 + "titleTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "titleTest01.pdf", sourceFolder
                 + "cmp_titleTest01.pdf", destinationFolder, "diff01_"));
            NUnit.Framework.Assert.AreEqual("Best title!", new PdfDocument(new PdfReader(destinationFolder + "titleTest01.pdf"
                )).GetDocumentInfo().GetTitle());
        }

        [NUnit.Framework.Test]
        public virtual void Title02Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "titleTest02.html"), new FileInfo(destinationFolder
                 + "titleTest02.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "titleTest02.pdf", sourceFolder
                 + "cmp_titleTest02.pdf", destinationFolder, "diff02_"));
            NUnit.Framework.Assert.AreEqual("Best title!", new PdfDocument(new PdfReader(destinationFolder + "titleTest02.pdf"
                )).GetDocumentInfo().GetTitle());
        }
    }
}
