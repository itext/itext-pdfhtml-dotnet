using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Element {
    public class OptionTest : ExtendedHtmlConversionITextTest {
        private static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/element/OptionTest/";

        private static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/element/OptionTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptionBasicTest01() {
            ConvertToPdfAndCompare("optionBasicTest01", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptionBasicTest02() {
            ConvertToPdfAndCompare("optionBasicTest02", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptionEmptyTest01() {
            ConvertToPdfAndCompare("optionEmptyTest01", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptionLabelValueTest01() {
            ConvertToPdfAndCompare("optionLabelValueTest01", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptionStylesTest01() {
            ConvertToPdfAndCompare("optionStylesTest01", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptionStylesTest02() {
            ConvertToPdfAndCompare("optionStylesTest02", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptionHeightTest01() {
            ConvertToPdfAndCompare("optionHeightTest01", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptionWidthTest01() {
            ConvertToPdfAndCompare("optionWidthTest01", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptionOverflowTest01() {
            ConvertToPdfAndCompare("optionOverflowTest01", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptionOverflowTest02() {
            ConvertToPdfAndCompare("optionOverflowTest02", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptionPseudoTest01() {
            ConvertToPdfAndCompare("optionPseudoTest01", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void OptionPseudoTest02() {
            ConvertToPdfAndCompare("optionPseudoTest02", sourceFolder, destinationFolder);
        }
    }
}
