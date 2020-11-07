using System;
using System.IO;
using iText.Html2pdf;
using iText.IO.Util;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class TargetCounterTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/TargetCounterTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/TargetCounterTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterPageUrlNameTest() {
            ConvertToPdfWithTargetCounterEnabledAndCompare("targetCounterPageUrlName");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterPageUrlIdTest() {
            ConvertToPdfWithTargetCounterEnabledAndCompare("targetCounterPageUrlId");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.EXCEEDED_THE_MAXIMUM_NUMBER_OF_RELAYOUTS)]
        public virtual void TargetCounterManyRelayoutsTest() {
            ConvertToPdfWithTargetCounterEnabledAndCompare("targetCounterManyRelayouts");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterPageBigElementTest() {
            ConvertToPdfWithTargetCounterEnabledAndCompare("targetCounterPageBigElement");
        }

        [NUnit.Framework.Test]
        public virtual void TargetCounterPageAllTagsTest() {
            ConvertToPdfWithTargetCounterEnabledAndCompare("targetCounterPageAllTags");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.CANNOT_RESOLVE_TARGET_COUNTER_VALUE, Count = 2)]
        public virtual void TargetCounterNotExistingTargetTest() {
            ConvertToPdfWithTargetCounterEnabledAndCompare("targetCounterNotExistingTarget");
        }

        private void ConvertToPdfWithTargetCounterEnabledAndCompare(String name) {
            String sourceHtml = sourceFolder + name + ".html";
            String cmpPdf = sourceFolder + "cmp_" + name + ".pdf";
            String destinationPdf = destinationFolder + name + ".pdf";
            ConverterProperties converterProperties = new ConverterProperties().SetBaseUri(sourceFolder).SetTargetCounterEnabled
                (true);
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(destinationPdf));
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                HtmlConverter.ConvertToPdf(fileInputStream, pdfDocument, converterProperties);
            }
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(sourceHtml) + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationPdf, cmpPdf, destinationFolder
                , "diff_" + name + "_"));
        }
    }
}
