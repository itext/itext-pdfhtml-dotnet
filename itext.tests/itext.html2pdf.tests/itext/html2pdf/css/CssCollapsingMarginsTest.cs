/*
This file is part of the iText (R) project.
Copyright (c) 1998-2019 iText Group NV
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
using System;
using System.IO;
using iText.Html2pdf;
using iText.IO.Util;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class CssCollapsingMarginsTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/CssCollapsingMarginsTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/CssCollapsingMarginsTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingTest01() {
            Test("collapsingTest01.html", "collapsingTest01.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingTest02() {
            Test("collapsingTest02.html", "collapsingTest02.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingTest03() {
            Test("collapsingTest03.html", "collapsingTest03.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingTest04() {
            Test("collapsingTest04.html", "collapsingTest04.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingTest05() {
            // "min-height" property affects margins collapse differently in chrome and mozilla. While in chrome, this property
            // seems to not have any effect on collapsing margins at all (child margins collapse with parent margins even if
            // there is a considerable space between them due to the min-height property on parent), mozilla behaves better
            // and collapse happens only in case min-height of parent is less than actual height of the content and therefore
            // collapse really should happen. However even in mozilla, if parent has min-height which is a little bigger then
            // it's content actual height, margin collapse doesn't happen, however child margin doesn't have any effect either.
            Test("collapsingTest05.html", "collapsingTest05.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingTest06() {
            Test("collapsingTest06.html", "collapsingTest06.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingTest07() {
            Test("collapsingTest07.html", "collapsingTest07.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingTest07_malformed() {
            Test("collapsingTest07_malformed.html", "collapsingTest07_malformed.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingTest07_malformed2() {
            Test("collapsingTest07_malformed2.html", "collapsingTest07_malformed2.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingTest08() {
            Test("collapsingTest08.html", "collapsingTest08.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingTest09() {
            Test("collapsingTest09.html", "collapsingTest09.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingTest10() {
            Test("collapsingTest10.html", "collapsingTest10.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingTest11() {
            Test("collapsingTest11.html", "collapsingTest11.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingTest12() {
            Test("collapsingTest12.html", "collapsingTest12.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ElementTableTest() {
            // empty tables don't self-collapse in browsers
            Test("elementTableTest.html", "elementTableTest.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.RECTANGLE_HAS_NEGATIVE_OR_ZERO_SIZES, Count = 2)]
        public virtual void ElementUlOlLiTest() {
            Test("elementUlOlLiTest.html", "elementUlOlLiTest.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void EmptyInlinesTest01() {
            Test("emptyInlinesTest01.html", "emptyInlinesTest01.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void EmptyInlinesTest02() {
            Test("emptyInlinesTest02.html", "emptyInlinesTest02.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.RECTANGLE_HAS_NEGATIVE_OR_ZERO_SIZES)]
        public virtual void NegativeMarginsTest01() {
            Test("negativeMarginsTest01.html", "negativeMarginsTest01.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void NegativeMarginsTest02() {
            Test("negativeMarginsTest02.html", "negativeMarginsTest02.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.RECTANGLE_HAS_NEGATIVE_OR_ZERO_SIZES)]
        public virtual void NegativeMarginsTest03() {
            Test("negativeMarginsTest03.html", "negativeMarginsTest03.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void NotSanitizedTest01() {
            Test("notSanitizedTest01.html", "notSanitizedTest01.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void NotSanitizedTest02() {
            Test("notSanitizedTest02.html", "notSanitizedTest02.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingEmptyInlinesTest01() {
            Test("selfCollapsingEmptyInlinesTest01.html", "selfCollapsingEmptyInlinesTest01.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingEmptyInlinesTest02() {
            Test("selfCollapsingEmptyInlinesTest02.html", "selfCollapsingEmptyInlinesTest02.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingEmptyInlinesTest03() {
            Test("selfCollapsingEmptyInlinesTest03.html", "selfCollapsingEmptyInlinesTest03.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest01() {
            Test("selfCollapsingTest01.html", "selfCollapsingTest01.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest02() {
            Test("selfCollapsingTest02.html", "selfCollapsingTest02.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest03() {
            Test("selfCollapsingTest03.html", "selfCollapsingTest03.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest04() {
            Test("selfCollapsingTest04.html", "selfCollapsingTest04.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest05() {
            Test("selfCollapsingTest05.html", "selfCollapsingTest05.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest06() {
            Test("selfCollapsingTest06.html", "selfCollapsingTest06.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest07() {
            Test("selfCollapsingTest07.html", "selfCollapsingTest07.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest07_zero() {
            Test("selfCollapsingTest07_zero.html", "selfCollapsingTest07_zero.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest08() {
            Test("selfCollapsingTest08.html", "selfCollapsingTest08.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest08_zero() {
            Test("selfCollapsingTest08_zero.html", "selfCollapsingTest08_zero.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.RECTANGLE_HAS_NEGATIVE_OR_ZERO_SIZES)]
        public virtual void SelfCollapsingTest09() {
            Test("selfCollapsingTest09.html", "selfCollapsingTest09.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest10() {
            Test("selfCollapsingTest10.html", "selfCollapsingTest10.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest11() {
            Test("selfCollapsingTest11.html", "selfCollapsingTest11.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest12() {
            Test("selfCollapsingTest12.html", "selfCollapsingTest12.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest13() {
            Test("selfCollapsingTest13.html", "selfCollapsingTest13.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.RECTANGLE_HAS_NEGATIVE_OR_ZERO_SIZES)]
        public virtual void SelfCollapsingTest14() {
            Test("selfCollapsingTest14.html", "selfCollapsingTest14.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest15() {
            Test("selfCollapsingTest15.html", "selfCollapsingTest15.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest16() {
            Test("selfCollapsingTest16.html", "selfCollapsingTest16.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest17() {
            Test("selfCollapsingTest17.html", "selfCollapsingTest17.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest19() {
            Test("selfCollapsingTest19.html", "selfCollapsingTest19.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsFloatTest01() {
            Test("collapsingMarginsFloatTest01.html", "collapsingMarginsFloatTest01.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsFloatTest02() {
            Test("collapsingMarginsFloatTest02.html", "collapsingMarginsFloatTest02.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsFloatTest03() {
            Test("collapsingMarginsFloatTest03.html", "collapsingMarginsFloatTest03.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsFloatTest04() {
            Test("collapsingMarginsFloatTest04.html", "collapsingMarginsFloatTest04.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsFloatTest05() {
            Test("collapsingMarginsFloatTest05.html", "collapsingMarginsFloatTest05.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsFloatTest06() {
            Test("collapsingMarginsFloatTest06.html", "collapsingMarginsFloatTest06.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsFloatTest07() {
            Test("collapsingMarginsFloatTest07.html", "collapsingMarginsFloatTest07.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsFloatTest08() {
            // TODO DEVSIX-1820: on floats positioning collapsing margins of parent and first child is not taken into account
            Test("collapsingMarginsFloatTest08.html", "collapsingMarginsFloatTest08.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsFloatTest09() {
            // TODO DEVSIX-1820: on floats positioning collapsing margins of parent and first child is not taken into account
            Test("collapsingMarginsFloatTest09.html", "collapsingMarginsFloatTest09.pdf", "diff_");
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        private void Test(String @in, String @out, String diff) {
            String outPdf = destinationFolder + @out;
            String cmpPdf = sourceFolder + "cmp_" + @out;
            FileInfo srcFile = new FileInfo(sourceFolder + @in);
            FileInfo destFile = new FileInfo(outPdf);
            HtmlConverter.ConvertToPdf(srcFile, destFile);
            System.Console.Out.WriteLine("html: file:///" + UrlUtil.ToNormalizedURI(srcFile).AbsolutePath + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, diff));
        }
    }
}
