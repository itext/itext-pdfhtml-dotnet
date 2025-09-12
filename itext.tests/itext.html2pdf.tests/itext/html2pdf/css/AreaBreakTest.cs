using System;
using iText.Html2pdf;
using iText.Layout.Logs;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class AreaBreakTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/AreaBreakTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/AreaBreakTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AreaBreakDefaultTest() {
            ConvertToPdfAndCompare("area-break-default-test", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AreaBreakFlexReverseTest() {
            ConvertToPdfAndCompare("area-break-flex-reverse-test", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AreaBreakFlexTest() {
            ConvertToPdfAndCompare("area-break-flex-test", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AreaBreakNestedFlexTest() {
            ConvertToPdfAndCompare("area-break-nested-flex-items", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT)]
        [LogMessage(LayoutLogMessageConstant.AREA_BREAK_UNEXPECTED)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.OCCUPIED_AREA_HAS_NOT_BEEN_INITIALIZED)]
        public virtual void AreaBreakNestedFixedHeightTest() {
            //TODO Change test files after DEVSIX-2024 is fixed
            ConvertToPdfAndCompare("area-break-nested-fixed-height", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
