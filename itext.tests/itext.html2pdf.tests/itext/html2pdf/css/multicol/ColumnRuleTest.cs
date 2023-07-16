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
    public class ColumnRuleTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/multicol/ColumnRuleTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/multicol/ColumnRuleTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertRuleStyleNoneTest() {
            RunTest("ruleStyleNoneTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertRuleStyleDottedTest() {
            RunTest("ruleStyleDottedTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertRuleStyleSolidTest() {
            RunTest("ruleStyleSolidTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertRuleStyleDoubleTest() {
            RunTest("ruleStyleDoubleTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertRuleStyleRidgeTest() {
            RunTest("ruleStyleRidgeTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertRuleStyleManyColumnsTest() {
            RunTest("ruleStyleManyColumnsTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertRuleStyleMultipageColumnsTest() {
            RunTest("ruleStyleMultipageColumnsTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertRuleWidthThinTest() {
            RunTest("ruleWidthThinTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertRuleWidthMediumTest() {
            RunTest("ruleWidthMediumTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertRuleWidthThickTest() {
            RunTest("ruleWidthThickTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertRuleWidthDifferentWidthValuesTest() {
            RunTest("ruleWidthDifferentWidthValuesTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertRuleWidthHugeColumnsTest() {
            RunTest("ruleWidthHugeColumnsTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertRuleWidthIncorrectValuesTest() {
            RunTest("ruleWidthIncorrectValuesTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertRuleColorTest() {
            RunTest("ruleColorTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertRuleColorRgbTest() {
            RunTest("ruleColorRgbTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertRuleColorHslaTest() {
            RunTest("ruleColorHslaTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertRuleColorCurrentColorTest() {
            RunTest("ruleColorCurrentColorTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertRuleShorthandTest() {
            RunTest("ruleShorthandTest");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetBaseUri
                (SOURCE_FOLDER));
        }
    }
}
