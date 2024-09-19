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

namespace iText.Html2pdf.Css.Multicol {
    [NUnit.Framework.Category("IntegrationTest")]
    public class ColumnGapTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/multicol/ColumnGapTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/multicol/ColumnGapTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ConvertBasicTest() {
            RunTest("basicTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertLargeColumnGapValueTest() {
            RunTest("largeColumnGapValueTest");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 2)]
        public virtual void ConvertNegativeColumnGapValueTest() {
            RunTest("negativeColumnGapValueTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertSmallColumnGapValueTest() {
            // TODO DEVSIX-7631 Provide advanced support of percentage values for column-gap\width
            RunTest("smallColumnGapValueTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertFloatColumnGapValueTest() {
            // TODO DEVSIX-7631 Provide advanced support of percentage values for column-gap\width
            RunTest("floatColumnGapValueTest");
        }

        //TODO: DEVSIX-3596 add support of relative units that currently are not supported
        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 2)]
        public virtual void ConvertDifferentUnitsTest() {
            RunTest("differentUnitsTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertColumnsAndGapTest() {
            RunTest("columnsAndGapTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertMarginTest() {
            RunTest("marginTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertPaddingTest() {
            RunTest("paddingTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertMixedElementsTest() {
            RunTest("mixedElementsTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertNestedElementsTest() {
            RunTest("nestedElementsTest");
        }

        [NUnit.Framework.Test]
        public virtual void ConvertGapShorthandTest() {
            RunTest("gapShorthandTest");
        }

        private void RunTest(String testName) {
            ConvertToPdfAndCompare(testName, SOURCE_FOLDER, DESTINATION_FOLDER, false, new ConverterProperties().SetBaseUri
                (SOURCE_FOLDER));
        }
    }
}
