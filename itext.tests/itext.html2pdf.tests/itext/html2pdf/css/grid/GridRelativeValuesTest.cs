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
using iText.Html2pdf;
using iText.Layout.Exceptions;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Grid {
    [NUnit.Framework.Category("IntegrationTest")]
    public class GridRelativeValuesTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/grid/GridRelativeValuesTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/grid/GridRelativeValuesTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis1Test() {
            RunTest("bothAxis1");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis2Test() {
            RunTest("bothAxis2");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis3Test() {
            RunTest("bothAxis3");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis4Test() {
            RunTest("bothAxis4");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis5Test() {
            RunTest("bothAxis5");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis6Test() {
            RunTest("bothAxis6");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis7Test() {
            RunTest("bothAxis7");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis8Test() {
            RunTest("bothAxis8");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis9Test() {
            RunTest("bothAxis9");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis10Test() {
            RunTest("bothAxis10");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis11Test() {
            RunTest("bothAxis11");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis12Test() {
            RunTest("bothAxis12");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis13Test() {
            RunTest("bothAxis13");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis14Test() {
            RunTest("bothAxis14");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis15Test() {
            RunTest("bothAxis15");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis16Test() {
            RunTest("bothAxis16");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis17Test() {
            RunTest("bothAxis17");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis18Test() {
            RunTest("bothAxis18");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis19Test() {
            RunTest("bothAxis19");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis20Test() {
            RunTest("bothAxis20");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxis21Test() {
            RunTest("bothAxis21");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxisOnlyFr1Test() {
            RunTest("bothAxisOnlyFr1");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxisOnlyFr2Test() {
            RunTest("bothAxisOnlyFr2");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxisOnlyFr3Test() {
            RunTest("bothAxisOnlyFr3");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxisOnlyFr4Test() {
            RunTest("bothAxisOnlyFr4");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxisOnlyFr5Test() {
            RunTest("bothAxisOnlyFr5");
        }

        [NUnit.Framework.Test]
        public virtual void BothAxisOnlyFr6Test() {
            RunTest("bothAxisOnlyFr6");
        }

        [NUnit.Framework.Test]
        public virtual void ColAxis1Test() {
            RunTest("colAxis1");
        }

        [NUnit.Framework.Test]
        public virtual void ColAxis2Test() {
            RunTest("colAxis2");
        }

        [NUnit.Framework.Test]
        public virtual void ColAxis3Test() {
            RunTest("colAxis3");
        }

        [NUnit.Framework.Test]
        public virtual void ColAxis4Test() {
            RunTest("colAxis4");
        }

        [NUnit.Framework.Test]
        public virtual void ColAxis5Test() {
            RunTest("colAxis5");
        }

        [NUnit.Framework.Test]
        public virtual void ColAxis6Test() {
            RunTest("colAxis6");
        }

        [NUnit.Framework.Test]
        public virtual void ColAxis7Test() {
            RunTest("colAxis7");
        }

        [NUnit.Framework.Test]
        public virtual void ColAxis8Test() {
            RunTest("colAxis8");
        }

        [NUnit.Framework.Test]
        public virtual void ColAxis9Test() {
            RunTest("colAxis9");
        }

        [NUnit.Framework.Test]
        public virtual void ColAxis10Test() {
            RunTest("colAxis10");
        }

        [NUnit.Framework.Test]
        public virtual void ColAxis11Test() {
            RunTest("colAxis11");
        }

        [NUnit.Framework.Test]
        public virtual void RowAxis1Test() {
            RunTest("rowAxis1");
        }

        [NUnit.Framework.Test]
        public virtual void RowAxis2Test() {
            RunTest("rowAxis2");
        }

        [NUnit.Framework.Test]
        public virtual void RowAxis3Test() {
            RunTest("rowAxis3");
        }

        [NUnit.Framework.Test]
        public virtual void RowAxis4Test() {
            RunTest("rowAxis4");
        }

        [NUnit.Framework.Test]
        public virtual void RowAxis5Test() {
            RunTest("rowAxis5");
        }

        [NUnit.Framework.Test]
        public virtual void RowAxis6Test() {
            RunTest("rowAxis6");
        }

        [NUnit.Framework.Test]
        public virtual void RowAxis7Test() {
            RunTest("rowAxis7");
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutExceptionMessageConstant.GRID_AUTO_REPEAT_CANNOT_BE_COMBINED_WITH_INDEFINITE_SIZES, Count
             = 2)]
        public virtual void MinmaxAutoRepeat1Test() {
            RunTest("minmaxAutoRepeat1");
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutExceptionMessageConstant.GRID_AUTO_REPEAT_CANNOT_BE_COMBINED_WITH_INDEFINITE_SIZES, Count
             = 2)]
        public virtual void MinmaxAutoRepeat2Test() {
            RunTest("minmaxAutoRepeat2");
        }

        [NUnit.Framework.Test]
        public virtual void MinmaxFitContent1Test() {
            RunTest("minmaxFitContent1");
        }

        [NUnit.Framework.Test]
        public virtual void MinmaxFitContent2Test() {
            RunTest("minmaxFitContent2");
        }

        [NUnit.Framework.Test]
        public virtual void MinmaxFitContentAutoRepeat1Test() {
            RunTest("minmaxFitContentAutoRepeat1");
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutExceptionMessageConstant.GRID_AUTO_REPEAT_CANNOT_BE_COMBINED_WITH_INDEFINITE_SIZES)]
        public virtual void MinmaxFitContentAutoRepeat2Test() {
            RunTest("minmaxFitContentAutoRepeat2");
        }

        [NUnit.Framework.Test]
        public virtual void MinmaxWithBothAxisSpan1Test() {
            RunTest("minmaxWithBothAxisSpan1");
        }

        [NUnit.Framework.Test]
        public virtual void MinmaxWithBothAxisSpan2Test() {
            RunTest("minmaxWithBothAxisSpan2");
        }

        [NUnit.Framework.Test]
        public virtual void MinmaxWithBothAxisSpan3Test() {
            RunTest("minmaxWithBothAxisSpan3");
        }

        [NUnit.Framework.Test]
        public virtual void MinmaxWithContentAndFrTest() {
            RunTest("minmaxWithContentAndFr");
        }

        [NUnit.Framework.Test]
        public virtual void MinmaxWithSpan1Test() {
            RunTest("minmaxWithSpan1");
        }

        [NUnit.Framework.Test]
        public virtual void MinmaxWithSpan2Test() {
            RunTest("minmaxWithSpan2");
        }

        [NUnit.Framework.Test]
        public virtual void MinmaxWithSpan3Test() {
            RunTest("minmaxWithSpan3");
        }

        [NUnit.Framework.Test]
        public virtual void MinmaxWithSpan4Test() {
            RunTest("minmaxWithSpan4");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetBaseUri
                (SOURCE_FOLDER));
        }
    }
}
