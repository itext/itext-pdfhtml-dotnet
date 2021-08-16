using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Attach.Impl.Layout {
    public class HtmlDocumentRendererIntegrationTest : ExtendedHtmlConversionITextTest {
        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/attach/impl/layout/HtmlDocumentRendererIntegrationTest/";

        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/attach/impl/layout/HtmlDocumentRendererIntegrationTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmptyPageRelayoutCausedByPagesCounterTest() {
            // HtmlDocumentRenderer#getNextRenderer can be called when currentArea == null
            ConvertToPdfAndCompare("emptyPageRelayoutCausedByPagesCounter", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
