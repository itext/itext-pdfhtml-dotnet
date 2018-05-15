using System;
using iText.Html2pdf;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class FloatAndAlignmentTest : ExtendedHtmlConversionITextTest {
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
            ConvertToPdfAndCompare("singleBlockSingleParagraphRight", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, Count = 2)]
        public virtual void SingleBlockSingleParagraphLeft() {
            //TODO: update test after ticket DEVSIX-1720  fix (WARN Invalid css property declaration: float: initial)
            ConvertToPdfAndCompare("singleBlockSingleParagraphLeft", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, Count = 2)]
        public virtual void SingleBlockSingleParagraphJustify() {
            //TODO: update test after ticket DEVSIX-1720  fix (WARN Invalid css property declaration: float: initial)
            ConvertToPdfAndCompare("singleBlockSingleParagraphJustify", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, Count = 2)]
        public virtual void SingleBlockSingleParagraphCenter() {
            //TODO: update test after ticket DEVSIX-1720  fix (WARN Invalid css property declaration: float: initial)
            ConvertToPdfAndCompare("singleBlockSingleParagraphCenter", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SeveralBlocksSingleParagraph() {
            /* this test shows different combinations of 3 float values blocks and 1 paragraph aligns within div container
            */
            ConvertToPdfAndCompare("severalBlocksSingleParagraph", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BlocksInsideParagraph() {
            /* this test shows different combinations of 3 float values blocks and 1 paragraph aligns within div container
            * now it points not only incorrect alignment vs float positioning, but also incorrect float area
            */
            ConvertToPdfAndCompare("blocksInsideParagraph", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void InlineBlocksInsideParagraph() {
            ConvertToPdfAndCompare("inlineBlocksInsideParagraph", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void InlineFloatsWithTextAlignmentTest01() {
            ConvertToPdfAndCompare("inlineFloatsWithTextAlignmentTest01", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void InlineFloatsWithTextAlignmentTest02() {
            ConvertToPdfAndCompare("inlineFloatsWithTextAlignmentTest02", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void InlineFloatsWithTextAlignmentTest03() {
            ConvertToPdfAndCompare("inlineFloatsWithTextAlignmentTest03", sourceFolder, destinationFolder);
        }
    }
}
