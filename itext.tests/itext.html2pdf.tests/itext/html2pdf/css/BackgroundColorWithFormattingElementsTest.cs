using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css {
    public class BackgroundColorWithFormattingElementsTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css" + "/BackgroundColorWithFormattingElementsTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css" + "/BackgroundColorWithFormattingElementsTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void StrongTagTest() {
            ConvertToPdfAndCompare("strongTag", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BTagTest() {
            ConvertToPdfAndCompare("bTag", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ITagTest() {
            ConvertToPdfAndCompare("iTag", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void EmTagTest() {
            ConvertToPdfAndCompare("emTag", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void MarkTagTest() {
            ConvertToPdfAndCompare("markTag", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SmallTagTest() {
            ConvertToPdfAndCompare("smallTag", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DelTagTest() {
            ConvertToPdfAndCompare("delTag", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void InsTagTest() {
            ConvertToPdfAndCompare("insTag", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SubTagTest() {
            ConvertToPdfAndCompare("subTag", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SupTagTest() {
            ConvertToPdfAndCompare("supTag", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
