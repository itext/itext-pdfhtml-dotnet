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
using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css.Multicol {
    [NUnit.Framework.Category("IntegrationTest")]
    public class ColumnWidthTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/multicol/ColumnWidthTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/multicol/ColumnWidthTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertSimpleDivTest() {
            RunTest("simpleDivTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertColumnWidthAutoTest() {
            RunTest("columnWidthAutoTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertDifferentUnitsTest() {
            RunTest("differentUnitsTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertParagraphsInsideContainerTest() {
            RunTest("paragraphsInsideContainer");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertOneParagraphSpecifiedWithDifferentWidthTest() {
            RunTest("oneParagraphSpecifiedWithDifferentWidthTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertMixedElementsInContainerTest() {
            RunTest("mixedElementsInContainer");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertNarrowColumnsTest() {
            RunTest("narrowColumns");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertLargeColumnsTest() {
            RunTest("largeColumns");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicArticleTest() {
            RunTest("basicArticleTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicDivMultiPageTest() {
            RunTest("basicDivMultiPageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicDivTest() {
            RunTest("basicDivTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertNestedElementsTest() {
            RunTest("nestedElementsTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicDisplayPropertyTest() {
            RunTest("basicDisplayPropertyTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertDisplayPropertyWithNestedColumnsTest() {
            RunTest("displayPropertyWithNestedColumnsTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicDivWithImageTest() {
            RunTest("basicDivWithImageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertOverlaidContentInDivWithImageTest() {
            RunTest("overlaidContentInDivWithImageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicFlexPropertyTest() {
            RunTest("basicFlexPropertyTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertOverlaidFlexContentInColumnContainerTest() {
            RunTest("overlaidFlexContentInColumnContainerTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicFloatPropertyTest() {
            RunTest("basicFloatPropertyTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicFormTest() {
            RunTest("basicFormTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertFormWithNestedElementsTest() {
            RunTest("formWithNestedElementsTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertFormMultiPageTest() {
            RunTest("formMultiPageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertFormWithNestedElementsMultiPageTest() {
            RunTest("formWithNestedElementsMultiPageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicOlTest() {
            RunTest("basicOlTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertOlWithTestedElementsTest() {
            RunTest("olWithNestedElementsTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicSectionTest() {
            RunTest("basicSectionTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertColumnizedShortParagraphsInTableCellTest() {
            RunTest("columnizedShortParagraphsInTableCellTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertColumnizedShortPInTableCellWithHeightTest() {
            RunTest("columnizedShortPInTableCellWithHeightTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertColumnizedSpanInTableCellTest() {
            RunTest("columnizedSpanInTableCellTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertColumnizedContentInTableTest() {
            RunTest("columnizedContentInTableTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicUlTest() {
            RunTest("basicUlTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertUlWithNestedElementsTest() {
            RunTest("ulWithNestedElementsTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertImagesTest() {
            RunTest("imagesTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertImagesWithDifferentHeightsTest() {
            RunTest("imagesWithDifferentHeightsTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertColumnWidthEqualsImagesTest() {
            RunTest("columnWidthEqualsImagesTest");
        }

        [NUnit.Framework.Test]
        public virtual void DiffElementsInsidePTest() {
            RunTest("diffElementsInsidePTest");
        }

        [NUnit.Framework.Test]
        public virtual void TableColspanTest() {
            RunTest("tableColspanTest");
        }

        [NUnit.Framework.Test]
        public virtual void TableRowspanTest() {
            RunTest("tableRowspanTest");
        }

        [NUnit.Framework.Test]
        public virtual void TableColspanRowspanTest() {
            RunTest("tableColspanRowspanTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicHiTest() {
            RunTest("basicHiTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicFooterHeaderTest() {
            RunTest("basicFooterHeaderTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicDlTest() {
            RunTest("basicDlTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicInlineElementsTest() {
            RunTest("basicInlineElementsTest");
        }

        [NUnit.Framework.Test]
        public virtual void BasicBlockquoteTest() {
            RunTest("basicBlockquoteTest");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetMulticolEnabled
                (true).SetBaseUri(SOURCE_FOLDER));
        }
    }
}
