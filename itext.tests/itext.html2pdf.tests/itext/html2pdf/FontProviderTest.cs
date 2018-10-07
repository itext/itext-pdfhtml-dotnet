using System;
using System.IO;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf {
    public class FontProviderTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/FontProviderTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/FontProviderTest/";

        private const String TYPOGRAPHY_WARNING = "Cannot find pdfCalligraph module, which was implicitly required by one of the layout properties";

        // Actually the results are invalid because there is no pdfCalligraph.
        // But we'd like to test how Free Sans works for a specific scripts.
        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(TYPOGRAPHY_WARNING, Count = 14)]
        public virtual void HebrewTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "hebrew.html"), new FileInfo(destinationFolder + "hebrew.pdf"
                ));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "hebrew.pdf", sourceFolder
                 + "cmp_hebrew.pdf", destinationFolder, "diffHebrew_"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DevanagariTest() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "devanagari.html"), new FileInfo(destinationFolder 
                + "devanagari.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "devanagari.pdf", sourceFolder
                 + "cmp_devanagari.pdf", destinationFolder, "diffDevanagari_"));
        }
    }
}
