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
    public class GridTemplateColumnTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/grid/GridTemplateColumnTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/grid/GridTemplateColumnTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnBordersTest() {
            RunTest("template-cols-borders");
        }

        [NUnit.Framework.Test]
        public virtual void AutoRowFixedTest() {
            RunTest("auto-cols-fixed");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnStartEndTest() {
            RunTest("template-cols-column-start-end");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnWidthUnitsTest() {
            RunTest("template-cols-different-width-units");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnFitContentTest() {
            // TODO DEVSIX-8383 (assign to the ticket about minmax&repeat&fit-content support)
            RunTest("template-cols-fit-content");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnFitContentAutoTest() {
            // TODO DEVSIX-8383 (assign to the ticket about minmax&repeat&fit-content support)
            RunTest("template-cols-fit-content-auto");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnFrTest() {
            RunTest("template-cols-fr");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnGridGapTest() {
            RunTest("template-cols-grid-gap");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnHeightWidthTest() {
            RunTest("template-cols-height-width");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnMarginTest() {
            RunTest("template-cols-margin");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnMinMaxTest() {
            // TODO DEVSIX-8383 (assign to the ticket about minmax&repeat&fit-content support)
            RunTest("template-cols-minmax");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnMixedTest() {
            RunTest("template-cols-mixed");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnMultiPageTest() {
            RunTest("template-cols-enormous-size");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnNestedTest() {
            RunTest("template-cols-nested");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnPaddingTest() {
            RunTest("template-cols-padding");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnRepeatTest() {
            // TODO DEVSIX-8383 (assign to the ticket about minmax&repeat&fit-content support)
            RunTest("template-cols-repeat");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnRepeatMinMaxTest() {
            // TODO DEVSIX-8383 (assign to the ticket about minmax&repeat&fit-content support)
            RunTest("template-cols-repeat-minmax");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnColumnGapTest() {
            RunTest("template-cols-column-gap");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnBasicTest() {
            RunTest("template-cols-without-other-props");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateColumnBasicTest2() {
            RunTest("template-cols-without-other-props-2");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetBaseUri
                (SOURCE_FOLDER).SetCssGridEnabled(true));
        }
    }
}
