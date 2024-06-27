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
    public class GridTemplateRowTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/grid/GridTemplateRowTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/grid/GridTemplateRowTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowAutoTest() {
            RunTest("template-rows-auto");
        }

        [NUnit.Framework.Test]
        public virtual void AutoRowFixedTest() {
            RunTest("auto-rows-fixed");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowBordersTest() {
            RunTest("template-rows-borders");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowStartEndTest() {
            RunTest("template-rows-start-end");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowWidthUnitsTest() {
            RunTest("template-rows-different-width-units");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowFitContentTest() {
            RunTest("template-rows-fit-content");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowFitContentAutoTest() {
            RunTest("template-rows-fit-content-auto");
        }

        [NUnit.Framework.Test]
        public virtual void RowFitContentPercentTest() {
            RunTest("row-fit-content-percent");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowFrTest() {
            RunTest("template-rows-fr");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowGridGapTest() {
            RunTest("template-rows-grid-gap");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowHeightWidthTest() {
            RunTest("template-rows-height-width");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowMarginTest() {
            RunTest("template-rows-margin");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowMinMaxTest() {
            RunTest("template-rows-minmax");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowMixedTest() {
            RunTest("template-rows-mixed");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowMultiPageTest() {
            // TODO DEVSIX-8331
            RunTest("template-rows-multipage");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowNestedTest() {
            RunTest("template-rows-nested");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowPaddingTest() {
            RunTest("template-rows-padding");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowRepeatTest() {
            RunTest("template-rows-repeat");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowRepeatMinMaxTest() {
            RunTest("template-rows-repeat-minmax");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowRowGapTest() {
            RunTest("template-rows-row-gap");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateRowBasicTest() {
            RunTest("template-rows-without-other-props");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetBaseUri
                (SOURCE_FOLDER).SetCssGridEnabled(true));
        }
    }
}
