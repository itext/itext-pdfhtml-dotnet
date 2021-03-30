/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
Authors: iText Software.

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
    public class FigureTest : ExtendedITextTest {
        private static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/FigureTest/";

        private static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/FigureTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FigureFileDocumentTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hello_figure_file.html"), new FileInfo(destinationFolder
                 + "hello_figure_file.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hello_figure_file.pdf"
                , sourceFolder + "cmp_hello_figure_file.pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        public virtual void SmallFigureTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "smallFigureTest.html"), new FileInfo(destinationFolder
                 + "smallFigureTest.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "smallFigureTest.pdf"
                , sourceFolder + "cmp_smallFigureTest.pdf", destinationFolder, "diff03_"));
        }

        [NUnit.Framework.Test]
        public virtual void FigureInSpanTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "figureInSpan.html"), new FileInfo(destinationFolder
                 + "figureInSpan.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "figureInSpan.pdf", sourceFolder
                 + "cmp_figureInSpan.pdf", destinationFolder, "diff04_"));
        }
    }
}
