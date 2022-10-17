/*
This file is part of the iText (R) project.
Copyright (c) 1998-2022 iText Group NV
Authors: iText Software.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
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
