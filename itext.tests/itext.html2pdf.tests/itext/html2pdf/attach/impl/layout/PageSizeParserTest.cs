/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
Authors: Apryse Software.

This program is offered under a commercial and under the AGPL license.
For commercial licensing, contact us at https://itextpdf.com/sales.  For AGPL licensing, see below.

AGPL licensing:
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using iText.Html2pdf.Logs;
using iText.Kernel.Geom;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Attach.Impl.Layout {
    [NUnit.Framework.Category("UnitTest")]
    public class PageSizeParserTest : ExtendedITextTest {
        private const double EPS = 1e-9;

        [NUnit.Framework.Test]
        public virtual void SimpleA4Test() {
            PageSize expected = PageSize.A4;
            PageSize actual = PageSizeParser.FetchPageSize("a4", 10, 10, PageSize.A0);
            AssertSizesAreSame(expected, actual);
        }

        [NUnit.Framework.Test]
        public virtual void SimpleLedgerTest() {
            PageSize expected = PageSize.LEDGER.Rotate();
            PageSize actual = PageSizeParser.FetchPageSize("ledger", 10, 10, PageSize.A0);
            AssertSizesAreSame(expected, actual);
        }

        [NUnit.Framework.Test]
        public virtual void LedgerLandscapeIsSameAsLedgerTest() {
            PageSize expected = PageSize.LEDGER;
            PageSize actual = PageSizeParser.FetchPageSize("ledger landscape", 10, 10, PageSize.A0);
            AssertSizesAreSame(expected, actual);
        }

        [NUnit.Framework.Test]
        public virtual void LedgerPortraitIsRotatedLedgerTest() {
            PageSize expected = PageSize.LEDGER.Rotate();
            PageSize actual = PageSizeParser.FetchPageSize("ledger portrait", 10, 10, PageSize.A0);
            AssertSizesAreSame(expected, actual);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.PAGE_SIZE_VALUE_IS_INVALID, Count = 1)]
        public virtual void IncorrectPageSizeNameTest() {
            PageSize expected = PageSize.LEDGER;
            PageSize actual = PageSizeParser.FetchPageSize("INCORRECT_PAGE_SIZE", 10, 10, PageSize.LEDGER);
            AssertSizesAreSame(expected, actual);
        }

        private void AssertSizesAreSame(PageSize a, PageSize b) {
            NUnit.Framework.Assert.AreEqual(a.GetWidth(), b.GetWidth(), EPS);
            NUnit.Framework.Assert.AreEqual(a.GetHeight(), b.GetHeight(), EPS);
        }
    }
}
