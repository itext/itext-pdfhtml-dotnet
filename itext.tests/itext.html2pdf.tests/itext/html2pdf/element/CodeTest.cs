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
using iText.Test;

namespace iText.Html2pdf.Element {
    [NUnit.Framework.Category("IntegrationTest")]
    public class CodeTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/CodeTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/CodeTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Code01Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "codeTest01.html"), new FileInfo(destinationFolder 
                + "codeTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "codeTest01.pdf", sourceFolder
                 + "cmp_codeTest01.pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        public virtual void Code02Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "codeTest02.html"), new FileInfo(destinationFolder 
                + "codeTest02.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "codeTest02.pdf", sourceFolder
                 + "cmp_codeTest02.pdf", destinationFolder, "diff02_"));
        }

        [NUnit.Framework.Test]
        public virtual void Code03Test() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "codeTest03.html"), new FileInfo(destinationFolder 
                + "codeTest03.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "codeTest03.pdf", sourceFolder
                 + "cmp_codeTest03.pdf", destinationFolder, "diff02_"));
        }
    }
}
