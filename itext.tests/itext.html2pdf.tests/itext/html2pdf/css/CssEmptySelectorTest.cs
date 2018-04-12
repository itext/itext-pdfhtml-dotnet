using System;
using System.IO;
using iText.Html2pdf;
using iText.IO.Util;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Css {
    public class CssEmptySelectorTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/CssEmptySelectorTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/CssEmptySelectorTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CssEmptyNotEmptyNestedNodesTest() {
            RunTest("cssEmptyNotEmptyNestedNodesTest", "diff01_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        private void RunTest(String testName, String diff) {
            String htmlName = sourceFolder + testName + ".html";
            String outFileName = destinationFolder + testName + ".pdf";
            String cmpFileName = sourceFolder + "cmp_" + testName + ".pdf";
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(htmlName).AbsolutePath + "\n");
            HtmlConverter.ConvertToPdf(new FileInfo(htmlName), new FileInfo(outFileName));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , diff));
        }
    }
}
