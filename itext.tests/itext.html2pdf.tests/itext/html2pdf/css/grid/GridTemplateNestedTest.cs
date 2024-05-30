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
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Grid {
    [NUnit.Framework.Category("IntegrationTest")]
    public class GridTemplateNestedTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/grid" + "/GridTemplateNestedTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/grid" + "/GridTemplateNestedTest/";

        //TODO DEVSIX-3340 change cmp files when GRID LAYOUT is supported
        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TemplateNestedAreasTest() {
            RunTest("grid-nested-areas");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateNestedArticlesTest() {
            RunTest("grid-nested-articles");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateNestedFormsTest() {
            RunTest("grid-nested-forms");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateNestedGridTest() {
            RunTest("grid-nested-grid");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateNestedListsTest() {
            RunTest("grid-nested-lists");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateNestedListsOddEvenTest() {
            RunTest("grid-nested-lists-odd-even");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateNestedMixedContentTest() {
            RunTest("grid-nested-mixed-content");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateNestedParagraphsTest() {
            RunTest("grid-nested-paragraphs");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateNestedImagesTest() {
            RunTest("grid-nested-images");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateNestedTableTest() {
            RunTest("grid-nested-table");
        }

        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TABLE_WIDTH_IS_MORE_THAN_EXPECTED_DUE_TO_MIN_WIDTH, Count = 
            2)]
        [NUnit.Framework.Test]
        public virtual void TemplateNestedTableNestedGridTest() {
            RunTest("grid-nested-table-nested-grid");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateNestedTableMixedContentTest() {
            RunTest("grid-nested-table-with-mixed-content");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateNested2LevelsWithAreasTest() {
            RunTest("grid-nested-2-levels-areas");
        }

        [LogMessage(iText.IO.Logs.IoLogMessageConstant.CLIP_ELEMENT, Count = 5)]
        [NUnit.Framework.Test]
        public virtual void TemplateNested3LevelsFormsTest() {
            RunTest("grid-nested-3-forms");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateNested3LevelsTest() {
            RunTest("grid-nested-3-levels");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateNested3LevelsMultipleTest() {
            RunTest("grid-nested-3-levels-multiple");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateNested3LevelsTablesTest() {
            RunTest("grid-nested-3-levels-tables");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetBaseUri
                (SOURCE_FOLDER).SetCssGridEnabled(true));
        }
    }
}
