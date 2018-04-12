using System;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Css {
    public class HtmlCommentedTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/HtmlCommentedTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/HtmlCommentedTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void InitDestinationFolder() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CommentedHtmlToPdf() {
            String htmlSource = sourceFolder + "commentedHtmlToPdf.html";
            String outPdf = destinationFolder + "commentedHtmlToPdf.pdf";
            String cmpPdf = sourceFolder + "cmp_commentedHtmlToPdf.pdf";
            FileInfo htmlInput = new FileInfo(htmlSource);
            FileInfo pdfOutput = new FileInfo(outPdf);
            HtmlConverter.ConvertToPdf(htmlInput, pdfOutput);
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, "diff_"
                ));
        }
    }
}
