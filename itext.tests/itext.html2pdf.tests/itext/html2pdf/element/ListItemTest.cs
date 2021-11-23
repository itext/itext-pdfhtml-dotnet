using System;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Element {
    public class ListItemTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/ListItemTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/ListItemTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 14)]
        public virtual void RtlListItemInsideLtrOrderedListTest() {
            String name = "rtlListItemInsideLtrOrderedListTest";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 16)]
        public virtual void ListItemWithDifferentDirAndPositionInsideTest() {
            String name = "listItemWithDifferentDirAndPositionInsideTest";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 12)]
        public virtual void RtlListItemInsideLtrUnorderedListTest() {
            String name = "rtlListItemInsideLtrUnorderedListTest";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff01_"));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 16)]
        public virtual void DifferentListItemsInsideDifferentListsWithDifferentDirections() {
            String name = "differentListItemsInsideDifferentListsWithDifferentDirections";
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + name + ".html"), new FileInfo(destinationFolder + name
                 + ".pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + name + ".pdf", sourceFolder
                 + "cmp_" + name + ".pdf", destinationFolder, "diff01_"));
        }
    }
}
