/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class HyphenateTest : ExtendedITextTest {
        private static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/HyphenateTest/";

        private static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/HyphenateTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void Test01() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hyphenateTest01.html"), new FileInfo(destinationFolder
                 + "hyphenateTest01.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hyphenateTest01.pdf"
                , sourceFolder + "cmp_hyphenateTest01.pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        public virtual void Test02() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hyphenateTest02.html"), new FileInfo(destinationFolder
                 + "hyphenateTest02.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hyphenateTest02.pdf"
                , sourceFolder + "cmp_hyphenateTest02.pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        public virtual void Test03() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hyphenateTest03.html"), new FileInfo(destinationFolder
                 + "hyphenateTest03.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hyphenateTest03.pdf"
                , sourceFolder + "cmp_hyphenateTest03.pdf", destinationFolder, "diff03_"));
        }

        [NUnit.Framework.Test]
        public virtual void Test04() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hyphenateTest04.html"), new FileInfo(destinationFolder
                 + "hyphenateTest04.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hyphenateTest04.pdf"
                , sourceFolder + "cmp_hyphenateTest04.pdf", destinationFolder, "diff04_"));
        }

        [NUnit.Framework.Test]
        public virtual void Test05() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hyphenateTest05.html"), new FileInfo(destinationFolder
                 + "hyphenateTest05.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hyphenateTest05.pdf"
                , sourceFolder + "cmp_hyphenateTest05.pdf", destinationFolder, "diff05_"));
        }

        [NUnit.Framework.Test]
        public virtual void Test06() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hyphenateTest06.html"), new FileInfo(destinationFolder
                 + "hyphenateTest06.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hyphenateTest06.pdf"
                , sourceFolder + "cmp_hyphenateTest06.pdf", destinationFolder, "diff06_"));
        }

        [NUnit.Framework.Test]
        public virtual void Test07Ru() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hyphenateTest07Ru.html"), new FileInfo(destinationFolder
                 + "hyphenateTest07Ru.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hyphenateTest07Ru.pdf"
                , sourceFolder + "cmp_hyphenateTest07Ru.pdf", destinationFolder, "diff07Ru_"));
        }

        [NUnit.Framework.Test]
        public virtual void Test08De() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hyphenateTest08De.html"), new FileInfo(destinationFolder
                 + "hyphenateTest08De.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hyphenateTest08De.pdf"
                , sourceFolder + "cmp_hyphenateTest08De.pdf", destinationFolder, "diff08De_"));
        }

        [NUnit.Framework.Test]
        public virtual void Test09NonBreakingHyphen() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hyphenateTest09NonBreakingHyphen.html"), new FileInfo
                (destinationFolder + "hyphenateTest09NonBreakingHyphen.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hyphenateTest09NonBreakingHyphen.pdf"
                , sourceFolder + "cmp_hyphenateTest09NonBreakingHyphen.pdf", destinationFolder, "diff09NonBreakingHyphen_"
                ));
        }
    }
}
