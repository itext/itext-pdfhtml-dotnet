/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
using System;
using System.IO;
using iText.Html2pdf;
using iText.IO.Util;
using iText.Kernel.Utils;
using iText.Test;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class CssCollapsingMarginsTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/CssCollapsingMarginsTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/CssCollapsingMarginsTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingTest01() {
            Test("collapsingTest01.html", "collapsingTest01.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingTest02() {
            Test("collapsingTest02.html", "collapsingTest02.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingTest03() {
            Test("collapsingTest03.html", "collapsingTest03.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingTest04() {
            Test("collapsingTest04.html", "collapsingTest04.pdf", "diff_");
        }

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

        [NUnit.Framework.Test]
        public virtual void CollapsingTest06() {
            Test("collapsingTest06.html", "collapsingTest06.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingTest07() {
            Test("collapsingTest07.html", "collapsingTest07.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingTest07_malformed() {
            Test("collapsingTest07_malformed.html", "collapsingTest07_malformed.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingTest07_malformed2() {
            Test("collapsingTest07_malformed2.html", "collapsingTest07_malformed2.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingTest08() {
            Test("collapsingTest08.html", "collapsingTest08.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingTest09() {
            Test("collapsingTest09.html", "collapsingTest09.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingTest10() {
            Test("collapsingTest10.html", "collapsingTest10.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingTest11() {
            Test("collapsingTest11.html", "collapsingTest11.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingTest12() {
            Test("collapsingTest12.html", "collapsingTest12.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void ElementTableTest() {
            // empty tables don't self-collapse in browsers
            Test("elementTableTest.html", "elementTableTest.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void ElementUlOlLiTest() {
            Test("elementUlOlLiTest.html", "elementUlOlLiTest.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void EmptyInlinesTest01() {
            Test("emptyInlinesTest01.html", "emptyInlinesTest01.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void EmptyInlinesTest02() {
            Test("emptyInlinesTest02.html", "emptyInlinesTest02.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void NegativeMarginsTest01() {
            Test("negativeMarginsTest01.html", "negativeMarginsTest01.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void NegativeMarginsTest02() {
            Test("negativeMarginsTest02.html", "negativeMarginsTest02.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void NegativeMarginsTest03() {
            Test("negativeMarginsTest03.html", "negativeMarginsTest03.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void NotSanitizedTest01() {
            Test("notSanitizedTest01.html", "notSanitizedTest01.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void NotSanitizedTest02() {
            Test("notSanitizedTest02.html", "notSanitizedTest02.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingEmptyInlinesTest01() {
            Test("selfCollapsingEmptyInlinesTest01.html", "selfCollapsingEmptyInlinesTest01.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingEmptyInlinesTest02() {
            Test("selfCollapsingEmptyInlinesTest02.html", "selfCollapsingEmptyInlinesTest02.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingEmptyInlinesTest03() {
            Test("selfCollapsingEmptyInlinesTest03.html", "selfCollapsingEmptyInlinesTest03.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest01() {
            Test("selfCollapsingTest01.html", "selfCollapsingTest01.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest02() {
            Test("selfCollapsingTest02.html", "selfCollapsingTest02.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest03() {
            Test("selfCollapsingTest03.html", "selfCollapsingTest03.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest04() {
            Test("selfCollapsingTest04.html", "selfCollapsingTest04.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest05() {
            Test("selfCollapsingTest05.html", "selfCollapsingTest05.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest06() {
            Test("selfCollapsingTest06.html", "selfCollapsingTest06.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest07() {
            Test("selfCollapsingTest07.html", "selfCollapsingTest07.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest07_zero() {
            Test("selfCollapsingTest07_zero.html", "selfCollapsingTest07_zero.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest08() {
            Test("selfCollapsingTest08.html", "selfCollapsingTest08.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest08_zero() {
            Test("selfCollapsingTest08_zero.html", "selfCollapsingTest08_zero.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest09() {
            Test("selfCollapsingTest09.html", "selfCollapsingTest09.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest10() {
            Test("selfCollapsingTest10.html", "selfCollapsingTest10.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest11() {
            Test("selfCollapsingTest11.html", "selfCollapsingTest11.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest12() {
            Test("selfCollapsingTest12.html", "selfCollapsingTest12.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest13() {
            Test("selfCollapsingTest13.html", "selfCollapsingTest13.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest14() {
            Test("selfCollapsingTest14.html", "selfCollapsingTest14.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest15() {
            Test("selfCollapsingTest15.html", "selfCollapsingTest15.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest16() {
            Test("selfCollapsingTest16.html", "selfCollapsingTest16.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest17() {
            Test("selfCollapsingTest17.html", "selfCollapsingTest17.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingTest19() {
            Test("selfCollapsingTest19.html", "selfCollapsingTest19.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsFloatTest01() {
            Test("collapsingMarginsFloatTest01.html", "collapsingMarginsFloatTest01.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsFloatTest02() {
            Test("collapsingMarginsFloatTest02.html", "collapsingMarginsFloatTest02.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsFloatTest03() {
            Test("collapsingMarginsFloatTest03.html", "collapsingMarginsFloatTest03.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsFloatTest04() {
            Test("collapsingMarginsFloatTest04.html", "collapsingMarginsFloatTest04.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsFloatTest05() {
            Test("collapsingMarginsFloatTest05.html", "collapsingMarginsFloatTest05.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsFloatTest06() {
            Test("collapsingMarginsFloatTest06.html", "collapsingMarginsFloatTest06.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsFloatTest07() {
            Test("collapsingMarginsFloatTest07.html", "collapsingMarginsFloatTest07.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsFloatTest08() {
            // TODO DEVSIX-1820: on floats positioning collapsing margins of parent and first child is not taken into account
            Test("collapsingMarginsFloatTest08.html", "collapsingMarginsFloatTest08.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsFloatTest09() {
            // TODO DEVSIX-1820: on floats positioning collapsing margins of parent and first child is not taken into account
            Test("collapsingMarginsFloatTest09.html", "collapsingMarginsFloatTest09.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsImageTest() {
            Test("collapsingMarginsImage.html", "collapsingMarginsImage.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsImgInNestedDivsTest() {
            Test("collapsingMarginsImgInNestedDivs.html", "collapsingMarginsImgInNestedDivs.pdf", "diff_");
        }

        [NUnit.Framework.Test]
        public virtual void SelfCollapsingWithImageTest() {
            Test("selfCollapsingWithImage.html", "selfCollapsingWithImage.pdf", "diff_");
        }

        private void Test(String @in, String @out, String diff) {
            String outPdf = destinationFolder + @out;
            String cmpPdf = sourceFolder + "cmp_" + @out;
            FileInfo srcFile = new FileInfo(sourceFolder + @in);
            FileInfo destFile = new FileInfo(outPdf);
            HtmlConverter.ConvertToPdf(srcFile, destFile);
            System.Console.Out.WriteLine("html: " + UrlUtil.GetNormalizedFileUriString(srcFile.FullName) + "\n");
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder, diff));
        }
    }
}
