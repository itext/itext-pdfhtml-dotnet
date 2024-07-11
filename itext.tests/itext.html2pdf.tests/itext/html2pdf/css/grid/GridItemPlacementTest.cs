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

namespace iText.Html2pdf.Css.Grid {
    [NUnit.Framework.Category("IntegrationTest")]
    public class GridItemPlacementTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/grid/GridItemPlacementTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/grid/GridItemPlacementTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ColStartEnd1Test() {
            RunTest("colStartEnd1");
        }

        [NUnit.Framework.Test]
        public virtual void ColStartEnd2Test() {
            RunTest("colStartEnd2");
        }

        [NUnit.Framework.Test]
        public virtual void ColStartEnd3Test() {
            RunTest("colStartEnd3");
        }

        [NUnit.Framework.Test]
        public virtual void ColStartEnd4Test() {
            RunTest("colStartEnd4");
        }

        [NUnit.Framework.Test]
        public virtual void ColStartEnd5Test() {
            RunTest("colStartEnd5");
        }

        [NUnit.Framework.Test]
        public virtual void FewCellsPlacement1Test() {
            RunTest("fewCellsPlacement1");
        }

        [NUnit.Framework.Test]
        public virtual void FewCellsPlacement2Test() {
            RunTest("fewCellsPlacement2");
        }

        [NUnit.Framework.Test]
        public virtual void FewCellsPlacement3Test() {
            RunTest("fewCellsPlacement3");
        }

        [NUnit.Framework.Test]
        public virtual void FewCellsPlacement4Test() {
            RunTest("fewCellsPlacement4");
        }

        [NUnit.Framework.Test]
        public virtual void FewCellsPlacement5Test() {
            RunTest("fewCellsPlacement5");
        }

        [NUnit.Framework.Test]
        public virtual void FewCellsPlacement6Test() {
            RunTest("fewCellsPlacement6");
        }

        [NUnit.Framework.Test]
        public virtual void FewCellsPlacement7Test() {
            RunTest("fewCellsPlacement7");
        }

        [NUnit.Framework.Test]
        public virtual void RowStartEnd1Test() {
            RunTest("rowStartEnd1");
        }

        [NUnit.Framework.Test]
        public virtual void RowStartEnd2Test() {
            RunTest("rowStartEnd2");
        }

        [NUnit.Framework.Test]
        public virtual void RowStartEnd3Test() {
            RunTest("rowStartEnd3");
        }

        [NUnit.Framework.Test]
        public virtual void RowStartEnd4Test() {
            RunTest("rowStartEnd4");
        }

        [NUnit.Framework.Test]
        public virtual void TwoColumnSpans1Test() {
            RunTest("twoColumnSpans1");
        }

        [NUnit.Framework.Test]
        public virtual void TwoColumnSpans2Test() {
            RunTest("twoColumnSpans2");
        }

        [NUnit.Framework.Test]
        public virtual void TwoColumnSpans3Test() {
            RunTest("twoColumnSpans3");
        }

        [NUnit.Framework.Test]
        public virtual void TwoRowSpans1Test() {
            RunTest("twoRowSpans1");
        }

        [NUnit.Framework.Test]
        public virtual void TwoRowSpans2Test() {
            RunTest("twoRowSpans2");
        }

        [NUnit.Framework.Test]
        public virtual void TwoRowSpans3Test() {
            RunTest("twoRowSpans3");
        }

        [NUnit.Framework.Test]
        public virtual void SpanToNegativeStartTest() {
            RunTest("spanToNegativeStartTest");
        }

        [NUnit.Framework.Test]
        public virtual void SpanToNegativeStartWithExplicitTemplatesTest() {
            RunTest("spanToNegativeStartWithExplicitTemplatesTest");
        }

        [NUnit.Framework.Test]
        public virtual void SpanToNegativeStartWithoutTemplatesTest() {
            RunTest("spanToNegativeStartWithoutTemplatesTest");
        }

        [NUnit.Framework.Test]
        public virtual void SpanToNegativeStartWithoutTemplatesTest2() {
            RunTest("spanToNegativeStartWithoutTemplatesTest2");
        }

        [NUnit.Framework.Test]
        public virtual void SpanToNegativeStartWithSingleTemplateTest() {
            RunTest("spanToNegativeStartWithSingleTemplateTest");
        }

        [NUnit.Framework.Test]
        public virtual void ColumnSpanExpandsStartToNegativeTest() {
            RunTest("columnSpanExpandsStartToNegativeTest");
        }

        [NUnit.Framework.Test]
        public virtual void NegativeIndexOutOfTemplateTest() {
            RunTest("negativeIndexOutOfTemplateTest");
        }

        [NUnit.Framework.Test]
        public virtual void NegativeIndexWithImplicitLinesTest() {
            RunTest("negativeIndexWithImplicitLinesTest");
        }

        [NUnit.Framework.Test]
        public virtual void NegativeIndexWithoutTemplateTest() {
            RunTest("negativeIndexWithoutTemplateTest");
        }

        [NUnit.Framework.Test]
        public virtual void NegativeIndexShorthandTest() {
            RunTest("negativeIndexShorthandTest");
        }

        [NUnit.Framework.Test]
        public virtual void NegativeAndPositiveIndexShorthandTest() {
            RunTest("negativeAndPositiveIndexShorthandTest");
        }

        [NUnit.Framework.Test]
        public virtual void SpanToNegativeIndexWithoutTemplateTest() {
            RunTest("spanToNegativeIndexWithoutTemplateTest");
        }

        [NUnit.Framework.Test]
        public virtual void NoTemplate1Test() {
            RunTest("noTemplate1");
        }

        [NUnit.Framework.Test]
        public virtual void NoTemplate2Test() {
            RunTest("noTemplate2");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetBaseUri
                (SOURCE_FOLDER));
        }
    }
}
