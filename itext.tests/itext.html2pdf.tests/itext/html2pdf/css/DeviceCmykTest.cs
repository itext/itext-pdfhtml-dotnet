using System;
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.StyledXmlParser.Css.Validate;
using iText.StyledXmlParser.Css.Validate.Impl;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("Integration test")]
    public class DeviceCmykTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css" + "/DeviceCmykTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css" + "/DeviceCmykTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
            CssDeclarationValidationMaster.SetValidator(new CssDeviceCmykAwareValidator());
        }

        [NUnit.Framework.OneTimeTearDown]
        public static void After() {
            CssDeclarationValidationMaster.SetValidator(new CssDefaultValidator());
        }

        [NUnit.Framework.Test]
        public virtual void BodyBgColorTest() {
            ConvertToPdfAndCompare("bodyBgColor", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanColorTest() {
            ConvertToPdfAndCompare("spanColor", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanBgColorTest() {
            ConvertToPdfAndCompare("spanBgColor", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DivColorTest() {
            ConvertToPdfAndCompare("divColor", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DivBgColorTest() {
            ConvertToPdfAndCompare("divBgColor", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HeaderColorTest() {
            ConvertToPdfAndCompare("headerColor", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void HeaderBgColorTest() {
            ConvertToPdfAndCompare("headerBgColor", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphColorTest() {
            ConvertToPdfAndCompare("paragraphColor", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ParagraphBgColorTest() {
            ConvertToPdfAndCompare("paragraphBgColor", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BorderColorTest() {
            ConvertToPdfAndCompare("borderColor", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ELEMENT_DOES_NOT_FIT_CURRENT_AREA)]
        public virtual void SimpleSvgColorTest() {
            ConvertToPdfAndCompare("simpleSvgColor", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
