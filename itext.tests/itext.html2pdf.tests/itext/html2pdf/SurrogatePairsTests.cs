using System;
using System.IO;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf {
    public class SurrogatePairsTests : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/SurrogatePairsTests/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/SurrogatePairsTests/";

        private const String TYPOGRAPHY_WARNING = "Cannot find pdfCalligraph module, which was implicitly required by one of the layout properties";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(TYPOGRAPHY_WARNING, Count = 2)]
        public virtual void SurrogatePairFrom2Chars() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "surrogatePairFrom2Chars.html"), new FileInfo(destinationFolder
                 + "surrogatePairFrom2Chars.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "surrogatePairFrom2Chars.pdf"
                , sourceFolder + "cmp_surrogatePairFrom2Chars.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        [LogMessage(TYPOGRAPHY_WARNING, Count = 2)]
        public virtual void SurrogatePair2Pairs() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "surrogatePair2Pairs.html"), new FileInfo(destinationFolder
                 + "surrogatePair2Pairs.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "surrogatePair2Pairs.pdf"
                , sourceFolder + "cmp_surrogatePair2Pairs.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        [LogMessage(TYPOGRAPHY_WARNING, Count = 2)]
        public virtual void SurrogatePairFullCharacter() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "surrogatePairFullCharacter.html"), new FileInfo(destinationFolder
                 + "surrogatePairFullCharacter.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "surrogatePairFullCharacter.pdf"
                , sourceFolder + "cmp_surrogatePairFullCharacter.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        [LogMessage(TYPOGRAPHY_WARNING, Count = 2)]
        [LogMessage(iText.IO.LogMessageConstant.FONT_SUBSET_ISSUE)]
        public virtual void SurrogatePairCombingFullSurrs() {
            //TODO DEVSIX-3307
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "surrogatePairCombingFullSurrs.html"), new FileInfo
                (destinationFolder + "surrogatePairCombingFullSurrs.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "surrogatePairCombingFullSurrs.pdf"
                , sourceFolder + "cmp_surrogatePairCombingFullSurrs.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        [LogMessage(TYPOGRAPHY_WARNING, Count = 2)]
        [LogMessage(iText.IO.LogMessageConstant.FONT_SUBSET_ISSUE)]
        public virtual void SurrogatePairCombingFullSurrsWithNoSurrs() {
            //TODO DEVSIX-3307
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "surrogatePairCombingFullSurrsWithNoSurrs.html"), new 
                FileInfo(destinationFolder + "surrogatePairCombingFullSurrsWithNoSurrs.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "surrogatePairCombingFullSurrsWithNoSurrs.pdf"
                , sourceFolder + "cmp_surrogatePairCombingFullSurrsWithNoSurrs.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        [LogMessage(TYPOGRAPHY_WARNING, Count = 2)]
        public virtual void SurrogatePairCombinationOf3TypesPairs() {
            //TODO DEVSIX-3307
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "surrogatePairCombinationOf3TypesPairs.html"), new 
                FileInfo(destinationFolder + "surrogatePairCombinationOf3TypesPairs.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "surrogatePairCombinationOf3TypesPairs.pdf"
                , sourceFolder + "cmp_surrogatePairCombinationOf3TypesPairs.pdf", destinationFolder));
        }
    }
}
