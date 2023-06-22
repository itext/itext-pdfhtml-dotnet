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
    public class ColumnCountTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/multicol/ColumnCountTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/multicol/ColumnCountTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicArticleTest() {
            RunTest("basicArticleTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicDivTest() {
            RunTest("basicDivTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicDivWithImageTest() {
            RunTest("basicDivWithImageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicPTest() {
            RunTest("basicPTest");
        }

        //TODO: DEVSIX-7592 add support for forms
        [NUnit.Framework.Test]
        public virtual void ConvertBasicFormTest() {
            RunTest("basicFormTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicUlTest() {
            RunTest("basicUlTest");
        }

        //TODO: DEVSIX-7591
        [NUnit.Framework.Test]
        public virtual void ConvertBasicOlTest() {
            RunTest("basicOlTest");
        }

        //TODO: DEVSIX-7592
        [NUnit.Framework.Test]
        public virtual void ConvertBasicTableTest() {
            RunTest("basicTableTest");
        }

        //TODO: DEVSIX-7584 add multipage support
        [NUnit.Framework.Test]
        public virtual void ConvertBasicSectionTest() {
            RunTest("basicSectionTest");
        }

        //TODO: DEVSIX-7584 add multipage support
        [NUnit.Framework.Test]
        public virtual void ConvertBasicDivMultiPageDocumentsTest() {
            RunTest("basicDivMultiPageTest");
        }

        //TODO: DEVSIX-7592 add support for forms
        [NUnit.Framework.Test]
        public virtual void ConvertBasicFormMultiPageDocumentsTest() {
            RunTest("basicFormMultiPageTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicDisplayPropertyTest() {
            RunTest("basicDisplayPropertyTest");
        }

        //TODO: DEVSIX-7591
        [NUnit.Framework.Test]
        public virtual void ConvertBasicDisplayPropertyWithNestedColumnsTest() {
            RunTest("basicDisplayPropertyWithNestedColumnsTest");
        }

        //TODO: DEVSIX-7556
        [NUnit.Framework.Test]
        public virtual void ConvertBasicFloatPropertyTest() {
            RunTest("basicFloatPropertyTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicFlexPropertyTest() {
            RunTest("basicFlexPropertyTest");
        }

        //TODO: DEVSIX-7587 adjust approximate height calculation
        [NUnit.Framework.Test]
        public virtual void ConvertImagesWithDifferentColValuesTest() {
            RunTest("imagesWithDifferentColValuesTest");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetMulticolEnabled
                (true).SetBaseUri(SOURCE_FOLDER));
        }

        [NUnit.Framework.Test]
        public virtual void PaddingsMarginsBorderBackgrounds() {
            ConvertToPdfAndCompare("paddingsMarginsBorderBackgrounds", SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties
                ().SetMulticolEnabled(true));
        }

        [NUnit.Framework.Test]
        public virtual void BorderOnlyTest() {
            ConvertToPdfAndCompare("borderOnly", SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetMulticolEnabled
                (true));
        }

        [NUnit.Framework.Test]
        public virtual void PaddingOnlyTest() {
            ConvertToPdfAndCompare("paddingOnly", SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().
                SetMulticolEnabled(true));
        }

        [NUnit.Framework.Test]
        public virtual void MarginOnlyTest() {
            ConvertToPdfAndCompare("marginOnly", SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetMulticolEnabled
                (true));
        }

        [NUnit.Framework.Test]
        public virtual void SplitInnerParagraphBetweenColumns() {
            ConvertToPdfAndCompare("splitInnerParagraphBetweenColumns", SOURCE_FOLDER, DESTINATION_FOLDER, false, new 
                ConverterProperties().SetMulticolEnabled(true));
        }

        [NUnit.Framework.Test]
        public virtual void SplitInnerParagraphWithoutMarginBetweenColumns() {
            ConvertToPdfAndCompare("splitInnerParagraphWithoutMarginBetweenColumns", SOURCE_FOLDER, DESTINATION_FOLDER
                , false, new ConverterProperties().SetMulticolEnabled(true));
        }

        [NUnit.Framework.Test]
        public virtual void SplitEmptyBlockElementsBetweenColumns() {
            ConvertToPdfAndCompare("splitEmptyBlockElementsBetweenColumns", SOURCE_FOLDER, DESTINATION_FOLDER, false, 
                new ConverterProperties().SetMulticolEnabled(true));
        }

        [NUnit.Framework.Test]
        public virtual void SplitEmptyParagraphElementsBetweenColumns() {
            ConvertToPdfAndCompare("splitEmptyParagraphElementsBetweenColumns", SOURCE_FOLDER, DESTINATION_FOLDER, false
                , new ConverterProperties().SetMulticolEnabled(true));
        }

        [NUnit.Framework.Test]
        public virtual void SplitEmptyContinuousBlockElementBetweenColumns() {
            ConvertToPdfAndCompare("splitEmptyContinuousBlockElementBetweenColumns", SOURCE_FOLDER, DESTINATION_FOLDER
                , false, new ConverterProperties().SetMulticolEnabled(true));
        }
    }
}
