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
using iText.Layout.Logs;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Grid {
    [NUnit.Framework.Category("IntegrationTest")]
    public class GridTemplateElementPropertyContainerTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/grid/GridTemplateElementPropertyContainerTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/grid/GridTemplateElementPropertyContainerTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundItemsOnTopOfBackgroundGridTest() {
            RunTest("backgroundItemsOnTopOfBackgroundGrid");
        }

        [NUnit.Framework.Test]
        public virtual void PaddingAllSidesTest() {
            RunTest("paddingAll");
        }

        [NUnit.Framework.Test]
        public virtual void PaddingAllSidesEmptyDivTest() {
            RunTest("padding_all_with_empty_div");
        }

        [NUnit.Framework.Test]
        [LogMessage(LayoutLogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA, Count = 1)]
        public virtual void PaddingAllToBigForWidthTest() {
            RunTest("paddingAllToBigForWidth");
        }

        [NUnit.Framework.Test]
        [NUnit.Framework.Ignore("DEVSIX-TODO")]
        public virtual void PaddingOverflowX() {
            RunTest("padding_overflow_x");
        }

        [NUnit.Framework.Test]
        public virtual void BasicBorderTest() {
            RunTest("basic_border");
        }

        [NUnit.Framework.Test]
        public virtual void BorderBigTest() {
            RunTest("border_big");
        }

        [NUnit.Framework.Test]
        public virtual void BoderWithOverflowXTest() {
            RunTest("border_big_with_overflow_x");
        }

        [NUnit.Framework.Test]
        public virtual void MarginAllTest() {
            RunTest("margin_all");
        }

        [NUnit.Framework.Test]
        public virtual void MarginAllEmptyTest() {
            RunTest("margin_all_empty");
        }

        [NUnit.Framework.Test]
        public virtual void MarginAllWithEmptyDIV() {
            RunTest("margin_all_with_empty_div");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetBaseUri
                (SOURCE_FOLDER).SetCssGridEnabled(true));
        }
    }
}
