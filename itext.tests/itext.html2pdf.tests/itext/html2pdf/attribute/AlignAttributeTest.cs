using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Attribute {
    public class AlignAttributeTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/attribute/AlignAttributeTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/attribute/AlignAttributeTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void AlignImgTest01() {
            // vertical-alignment values top, middle and bottom are not currently supported for inline-block elements and images
            ConvertToPdfAndCompare("alignImgTest01", sourceFolder, destinationFolder);
        }
    }
}
