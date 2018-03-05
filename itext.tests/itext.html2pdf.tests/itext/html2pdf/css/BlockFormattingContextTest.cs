using System;
using System.IO;
using iText.Html2pdf;
using iText.IO.Util;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Css {
    public class BlockFormattingContextTest : ExtendedITextTest {
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
            RunTest("bfcOwnerFloat_floatsAndClear", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BfcOwnerFloat_marginsCollapse() {
            RunTest("bfcOwnerFloat_marginsCollapse", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BfcOwnerAbsolute_floatsAndClear() {
            // Positioning and handling floats and clearance is exactly correct,
            // however TODO absolutely positioned elements shall be drawn on the same z-level as floats.
            RunTest("bfcOwnerAbsolute_floatsAndClear", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BfcOwnerAbsolute_marginsCollapse() {
            // Margins don't collapse here which is correct,
            // however absolute positioning works a bit wrong from css point of view.
            RunTest("bfcOwnerAbsolute_marginsCollapse", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BfcOwnerInlineBlock_floatsAndClear() {
            RunTest("bfcOwnerInlineBlock_floatsAndClear", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BfcOwnerInlineBlock_marginsCollapse() {
            RunTest("bfcOwnerInlineBlock_marginsCollapse", "diff_");
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
            RunTest("bfcOwnerOverflowHidden_floatsAndClear", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void BfcOwnerOverflowHidden_marginsCollapse() {
            RunTest("bfcOwnerOverflowHidden_marginsCollapse", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        private void RunTest(String testName, String diff) {
            String htmlName = sourceFolder + testName + ".html";
            String outFileName = destinationFolder + testName + ".pdf";
            String cmpFileName = sourceFolder + "cmp_" + testName + ".pdf";
            HtmlConverter.ConvertToPdf(new FileInfo(htmlName), new FileInfo(outFileName));
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(htmlName).AbsolutePath + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , diff));
        }
    }
}
