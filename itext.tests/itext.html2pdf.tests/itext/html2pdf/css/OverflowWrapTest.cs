using System;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class OverflowWrapTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/OverflowWrapTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/OverflowWrapTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OverflowWrapCommonScenarioTest() {
            // TODO: update cmp file after implementing DEVSIX-1438
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "overflowWrapCommonScenario.html"), new FileInfo(destinationFolder
                 + "overflowWrapCommonScenario.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "overflowWrapCommonScenario.pdf"
                , sourceFolder + "cmp_overflowWrapCommonScenario.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void OverflowXOverflowWrapTest() {
            // TODO: update cmp file after implementing DEVSIX-1438
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "overflowXOverflowWrap.html"), new FileInfo(destinationFolder
                 + "overflowXOverflowWrap.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "overflowXOverflowWrap.pdf"
                , sourceFolder + "cmp_overflowXOverflowWrap.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void WhiteSpaceAndOverflowWrapTest() {
            // TODO: update cmp file after implementing DEVSIX-1438
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "whiteSpaceAndOverflowWrap.html"), new FileInfo(destinationFolder
                 + "whiteSpaceAndOverflowWrap.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "whiteSpaceAndOverflowWrap.pdf"
                , sourceFolder + "cmp_whiteSpaceAndOverflowWrap.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        public virtual void OverflowWrapAndFloatTest() {
            // TODO: update cmp file after implementing DEVSIX-1438
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "overflowWrapAndFloat.html"), new FileInfo(destinationFolder
                 + "overflowWrapAndFloat.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "overflowWrapAndFloat.pdf"
                , sourceFolder + "cmp_overflowWrapAndFloat.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH, Count = 3)]
        public virtual void OverflowWrapTableScenarioTest() {
            // TODO: update cmp file after implementing DEVSIX-1438
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "overflowWrapTableScenario.html"), new FileInfo(destinationFolder
                 + "overflowWrapTableScenario.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "overflowWrapTableScenario.pdf"
                , sourceFolder + "cmp_overflowWrapTableScenario.pdf", destinationFolder));
        }
    }
}
