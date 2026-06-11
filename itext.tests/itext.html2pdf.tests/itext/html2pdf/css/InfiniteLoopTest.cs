using System;
using iText.Commons.Utils;
using iText.Html2pdf;
using iText.Kernel.Exceptions;
using iText.Kernel.Pdf;
using iText.Layout.Utils;
using iText.Test;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class InfiniteLoopTest : ExtendedITextTest {
        private static readonly String SRC = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/InfiniteLoopTest/";

        private static readonly String DEST = NUnit.Framework.TestContext.CurrentContext.TestDirectory + "/test/itext/html2pdf/css/InfiniteLoopTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DEST);
        }

        [NUnit.Framework.Test]
        public virtual void TooBigHtmlThrowsTest() {
            String testName = "tooBigHtml";
            DocumentProperties documentProperties = new DocumentProperties();
            documentProperties.RegisterDependency(typeof(LayoutInfiniteLoopResolver), () => new LayoutInfiniteLoopResolver
                (9));
            NUnit.Framework.Assert.Catch(typeof(PdfException), () => HtmlConverter.ConvertToPdf(FileUtil.GetInputStreamForFile
                (SRC + testName + ".html"), new PdfDocument(new PdfWriter(DEST + testName + ".pdf"), documentProperties
                )));
        }
    }
}
