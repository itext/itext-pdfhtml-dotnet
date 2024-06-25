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
using iText.Html2pdf.Logs;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Grid {
    [NUnit.Framework.Category("IntegrationTest")]
    public class GridAreaTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/grid/GridAreaTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/grid/GridAreaTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BasicGridArea1Test() {
            RunTest("basicGridArea1");
        }

        [NUnit.Framework.Test]
        public virtual void BasicGridArea2Test() {
            RunTest("basicGridArea2");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateAreasBasicTest() {
            RunTest("templateAreasBasic");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateAreasInvalidNameTest() {
            RunTest("templateAreasInvalidName");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateAreasWithDotsTest() {
            RunTest("templateAreasWithDots");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateAreasSwitchedPlacesTest() {
            RunTest("grid-area-switched-places");
        }

        [NUnit.Framework.Test]
        public virtual void DifferentRowSpanTest() {
            // TODO DEVSIX-8387
            RunTest("differentRowSpanTest");
        }

        [NUnit.Framework.Test]
        public virtual void BorderBoxTest() {
            RunTest("borderBoxTest");
        }

        [NUnit.Framework.Test]
        public virtual void DifferentRowSpanOnSplitTest() {
            RunTest("differentRowSpanOnSplitTest");
        }

        [NUnit.Framework.Test]
        public virtual void DifferentRowSpanOnSplitTest2() {
            // TODO DEVSIX-8387
            RunTest("differentRowSpanOnSplitTest2");
        }

        [NUnit.Framework.Test]
        public virtual void DifferentRowSpanWithGaps50OnSplitTest() {
            RunTest("differentRowSpanWithGaps50OnSplitTest");
        }

        [NUnit.Framework.Test]
        public virtual void DifferentRowSpanWithGaps100OnSplitTest() {
            RunTest("differentRowSpanWithGaps100OnSplitTest");
        }

        [NUnit.Framework.Test]
        public virtual void SplitOn2ndRowGapTest() {
            RunTest("splitOn2ndRowGapTest");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.GRID_TEMPLATE_AREAS_IS_INVALID)]
        public virtual void InvalidTemplateAreasTest() {
            RunTest("invalidTemplateAreas");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateAreasStartAutoTest() {
            RunTest("templateAreasStartAuto");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateAreasStartTest() {
            // Here browser result seems strange. We specified only row starts but it somehow applies to column starts also.
            // I'd expect the same result as for templateAreasStartAutoTest
            RunTest("templateAreasStart");
        }

        [NUnit.Framework.Test]
        public virtual void TemplateAreasStartEndTest() {
            RunTest("templateAreasStartEnd");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetBaseUri
                (SOURCE_FOLDER).SetCssGridEnabled(true));
        }
    }
}
