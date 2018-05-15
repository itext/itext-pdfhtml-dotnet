using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css {
    public class BlockFormattingContextTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/BlockFormattingContextTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/BlockFormattingContextTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BfcOwnerFloat_floatsAndClear() {
            ConvertToPdfAndCompare("bfcOwnerFloat_floatsAndClear", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BfcOwnerFloat_marginsCollapse() {
            ConvertToPdfAndCompare("bfcOwnerFloat_marginsCollapse", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BfcOwnerAbsolute_floatsAndClear() {
            // Positioning and handling floats and clearance is exactly correct,
            // however TODO absolutely positioned elements shall be drawn on the same z-level as floats.
            ConvertToPdfAndCompare("bfcOwnerAbsolute_floatsAndClear", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BfcOwnerAbsolute_marginsCollapse() {
            // Margins don't collapse here which is correct,
            // however absolute positioning works a bit wrong from css point of view.
            ConvertToPdfAndCompare("bfcOwnerAbsolute_marginsCollapse", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BfcOwnerInlineBlock_floatsAndClear() {
            ConvertToPdfAndCompare("bfcOwnerInlineBlock_floatsAndClear", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BfcOwnerInlineBlock_marginsCollapse() {
            ConvertToPdfAndCompare("bfcOwnerInlineBlock_marginsCollapse", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BfcOwnerOverflowHidden_floatsAndClear() {
            // TODO overflow:hidden with display:block behaves curiously: it completely moves away from float horizontally.
            // We don't handle it in such way and it's unclear right now, based on what it behaves like this.
            // How would it behave if it would have 100% width or width:auto?
            //
            // Now, we basically working incorrectly, since overflow:hidden requires it's inner floats to be placed
            // not taking into account any other floats outside parent. However right now this would result in
            // content overlap if we would behave like this.
            ConvertToPdfAndCompare("bfcOwnerOverflowHidden_floatsAndClear", sourceFolder, destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BfcOwnerOverflowHidden_marginsCollapse() {
            ConvertToPdfAndCompare("bfcOwnerOverflowHidden_marginsCollapse", sourceFolder, destinationFolder);
        }
    }
}
