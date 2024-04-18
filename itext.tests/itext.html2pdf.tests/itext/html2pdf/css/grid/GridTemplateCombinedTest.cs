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
    public class GridTemplateCombinedTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/grid/GridTemplateCombinedTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/grid/GridTemplateCombinedTest/";

        //TODO DEVSIX-3340 change cmp files when GRID LAYOUT is supported
        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void TemplateCombinedBordersTest() {
            RunTest("template-combined-borders");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateCombinedGridColAndRowGapTest() {
            RunTest("template-combined-grid-row-col-gap");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateCombinedGridStartEndTest() {
            RunTest("template-combined-grid-start-end");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateCoombinedMarginsPaddingsTest() {
            RunTest("template-combined-margins-paddings");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateCombinedMinMaxTest() {
            RunTest("template-combined-minmax");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateCombinedMixedTest() {
            RunTest("template-combined-mixed");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateCombinedMultiPageTest() {
            RunTest("template-combined-multipage");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateCombinedNestedTest() {
            RunTest("template-combined-nested");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateCombinedRepeatMinMaxTest() {
            RunTest("template-combined-repeat-minmax");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateCombinedRowsAutoTest() {
            RunTest("template-combined-rows-auto");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateCombinedRowsFitContentAutoTest() {
            RunTest("template-combined-rows-fit-content-auto");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateCombinedNoOtherPropertiesTest() {
            RunTest("template-combined-without-other-props");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetBaseUri
                (SOURCE_FOLDER));
        }
    }
}
