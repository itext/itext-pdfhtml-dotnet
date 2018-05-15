using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css {
    public class HtmlCommentedTest : ExtendedHtmlConversionITextTest {
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
            ConvertToPdfAndCompare("commentedHtmlToPdf", sourceFolder, destinationFolder);
        }
    }
}
