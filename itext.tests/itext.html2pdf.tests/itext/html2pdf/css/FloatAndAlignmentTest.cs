using System;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class FloatAndAlignmentTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FloatAndAlignmentTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FloatAndAlignmentTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, Count = 2)]
        public virtual void SingleBlockSingleParagraphRight() {
            /* this test shows different combinations of float values blocks and  paragraph align RIGHT within div container
            */
            //TODO: update test after ticket DEVSIX-1720  fix (WARN Invalid css property declaration: float: initial)
            //TODO: update cmp file after ticket DEVSIX-1268 fix (Float property...)
            RunTest("singleBlockSingleParagraphRight", "diffRight01_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, Count = 2)]
        public virtual void SingleBlockSingleParagraphLeft() {
            //TODO: update test after ticket DEVSIX-1720  fix (WARN Invalid css property declaration: float: initial)
            //TODO: update cmp file after ticket DEVSIX-1268 fix (Float property...)
            RunTest("singleBlockSingleParagraphLeft", "diffLeft01_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, Count = 2)]
        public virtual void SingleBlockSingleParagraphJustify() {
            //TODO: update test after ticket DEVSIX-1720  fix (WARN Invalid css property declaration: float: initial)
            //TODO: update cmp file after ticket DEVSIX-1268 fix (Float property...)
            RunTest("singleBlockSingleParagraphJustify", "diffJust01_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, Count = 2)]
        public virtual void SingleBlockSingleParagraphCenter() {
            //TODO: update test after ticket DEVSIX-1720  fix (WARN Invalid css property declaration: float: initial)
            //TODO: update cmp file after ticket DEVSIX-1268 fix (Float property...)
            RunTest("singleBlockSingleParagraphCenter", "diffCent01_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SeveralBlocksSingleParagraph() {
            /* this test shows different combinations of 3 float values blocks and 1 paragraph aligns within div container
            */
            //TODO: update cmp file after ticket DEVSIX-1268 fix (Float property...)
            RunTest("severalBlocksSingleParagraph", "diffSev01_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BlocksInsideParagraph() {
            /* this test shows different combinations of 3 float values blocks and 1 paragraph aligns within div container
            * now it points not only incorrect alignment vs float positioning, but also incorrect float area
            */
            //TODO: update cmp file after ticket DEVSIX-1268 fix (Float property...)
            //TODO: update after DEVSIX-1437 fix (Fix edge cases for floats splitting)
            RunTest("blocksInsideParagraph", "diffInside01_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        private void RunTest(String testName, String diff) {
            String htmlName = sourceFolder + testName + ".html";
            String outFileName = destinationFolder + testName + ".pdf";
            String cmpFileName = sourceFolder + "cmp_" + testName + ".pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlName), new FileInfo(outFileName));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , diff));
        }
    }
}
