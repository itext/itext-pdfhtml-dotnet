using iText.Kernel.Geom;

namespace iText.Html2pdf.Attach.Impl.Layout {
    public class PageSizeParserTest {
        private const double EPS = 1e-9;

        [NUnit.Framework.Test]
        public virtual void SimpleA4Test() {
            PageSize expected = PageSize.A4;
            PageSize actual = PageSizeParser.FetchPageSize("a4", 10, 10, PageSize.A0);
            AssertSizesAreSame(expected, actual);
        }

        [NUnit.Framework.Test]
        public virtual void SimpleLedgerTest() {
            PageSize expected = PageSize.LEDGER;
            PageSize actual = PageSizeParser.FetchPageSize("ledger", 10, 10, PageSize.A0);
            AssertSizesAreSame(expected, actual);
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("To be fixed in DEVSIX-3072")]
        public virtual void LedgerLandscapeIsSameAsLedgerTest() {
            PageSize expected = PageSize.LEDGER;
            PageSize actual = PageSizeParser.FetchPageSize("ledger landscape", 10, 10, PageSize.A0);
            AssertSizesAreSame(expected, actual);
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("To be fixed in DEVSIX-3072")]
        public virtual void LedgerPortraitIsRotatedLedgerTest() {
            PageSize expected = PageSize.LEDGER.Rotate();
            PageSize actual = PageSizeParser.FetchPageSize("ledger portrait", 10, 10, PageSize.A0);
            AssertSizesAreSame(expected, actual);
        }

        private void AssertSizesAreSame(PageSize a, PageSize b) {
            NUnit.Framework.Assert.AreEqual(a.GetWidth(), b.GetWidth(), EPS);
            NUnit.Framework.Assert.AreEqual(a.GetHeight(), b.GetHeight(), EPS);
        }
    }
}
