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
