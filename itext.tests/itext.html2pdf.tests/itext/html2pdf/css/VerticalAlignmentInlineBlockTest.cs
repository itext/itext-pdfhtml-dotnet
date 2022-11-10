using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class VerticalAlignmentInlineBlockTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/VerticalAlignmentInlineBlockTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/VerticalAlignmentInlineBlockTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckBaselineAlignmentTest() {
            ConvertToPdfAndCompare("baseline", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckBaselineAlignmentWitWrapTest() {
            ConvertToPdfAndCompare("baseline-wrap", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckBottomAlignmentTest() {
            ConvertToPdfAndCompare("bottom", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckBottomAlignmentWitWrapTest() {
            ConvertToPdfAndCompare("bottom-wrap", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckMiddleAlignmentTest() {
            ConvertToPdfAndCompare("middle", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckMiddleAlignmentWitWrapTest() {
            ConvertToPdfAndCompare("middle-wrap", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckTopAlignmentTest() {
            ConvertToPdfAndCompare("top", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckTopAlignmentWitWrapTest() {
            ConvertToPdfAndCompare("top-wrap", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckMixedAlignmentTest() {
            ConvertToPdfAndCompare("mixed", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckMixedAlignment2Test() {
            // TODO DEVSIX-3757 Update reference doc if the result matched the expected result
            ConvertToPdfAndCompare("mixed2", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckMixedAlignment3Test() {
            // TODO DEVSIX-3757 Update reference doc if the result matched the expected result
            ConvertToPdfAndCompare("mixed3", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckCustomerExampleTest() {
            ConvertToPdfAndCompare("customerExample", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckSingleImageTest() {
            ConvertToPdfAndCompare("singleimage", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckElementsInDivAlignmentTest() {
            ConvertToPdfAndCompare("ElementsInDiv", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckSpanAlignmentTest() {
            ConvertToPdfAndCompare("span", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckStyledElementsAlignmentTest() {
            // TODO DEVSIX-3757 Update reference doc if the result matched the expected result
            ConvertToPdfAndCompare("styleAlignment", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CheckUnorderedListAlignmentTest() {
            ConvertToPdfAndCompare("unorderedList", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OrderedListAlignmentTest() {
            ConvertToPdfAndCompare("orderedList", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TableAlignmentTest() {
            ConvertToPdfAndCompare("table", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ButtonAlignmentTest() {
            ConvertToPdfAndCompare("button", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void FormAlignmentTest() {
            // TODO DEVSIX-3757 Update reference doc if the result matched the expected result
            ConvertToPdfAndCompare("form", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void HeaderAlignmentTest() {
            ConvertToPdfAndCompare("header", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphAlignmentTest() {
            ConvertToPdfAndCompare("paragraph", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void AllStylesTest() {
            // TODO DEVSIX-3757 Update reference doc if the result matched the expected result
            ConvertToPdfAndCompare("AllStyles", sourceFolder, destinationFolder);
        }
    }
}
