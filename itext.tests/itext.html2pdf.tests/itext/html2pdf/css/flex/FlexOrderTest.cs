/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
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

namespace iText.Html2pdf.Css.Flex {
    [NUnit.Framework.Category("IntegrationTest")]
    public class FlexOrderTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/flex/FlexOrderTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/flex/FlexOrderTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DecimalAlignContentWrapTest() {
            ConvertToPdfAndCompare("decimalAlignContentWrap", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void DecimalAlignContentWrapReverseTest() {
            ConvertToPdfAndCompare("decimalAlignContentWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        [LogMessage(Html2PdfLogMessageConstant.WORKER_UNABLE_TO_PROCESS_OTHER_WORKER)]
        public virtual void DecimalAlignItemsTest() {
            ConvertToPdfAndCompare("decimalAlignItems", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void DecimalAlignSelfTest() {
            ConvertToPdfAndCompare("decimalAlignSelf", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void DecimalsTest() {
            ConvertToPdfAndCompare("decimals", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void DecimalsNestedTest() {
            ConvertToPdfAndCompare("decimalsNested", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void DecimalsDuplicatedValuesTest() {
            ConvertToPdfAndCompare("decimalsDuplicatedValues", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void DecimalsDuplicatedValuesWrapReverseTest() {
            ConvertToPdfAndCompare("decimalsDuplicatedValuesWrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void DirColumnTest() {
            ConvertToPdfAndCompare("dirColumn", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void DirColumnLongTest() {
            ConvertToPdfAndCompare("dirColumnLong", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void DirColumnReverseTest() {
            ConvertToPdfAndCompare("dirColumnReverse", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void DirRowTest() {
            ConvertToPdfAndCompare("dirRow", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void DirRowReverseTest() {
            ConvertToPdfAndCompare("dirRowReverse", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void DirRowWideTest() {
            ConvertToPdfAndCompare("dirRowWide", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void FlexBasisTest() {
            ConvertToPdfAndCompare("flexBasis", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void FlexGrowTest() {
            ConvertToPdfAndCompare("flexGrow", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void FlexShrinkTest() {
            ConvertToPdfAndCompare("flexShrink", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void FlowColumnWrapTest() {
            ConvertToPdfAndCompare("flowColumnWrap", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void FlowRowWrapTest() {
            ConvertToPdfAndCompare("flowRowWrap", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void InheritTest() {
            ConvertToPdfAndCompare("inherit", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void InheritNestedTest() {
            ConvertToPdfAndCompare("inheritNested", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void InitialTest() {
            ConvertToPdfAndCompare("initial", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void InitialAlignContentTest() {
            ConvertToPdfAndCompare("initialAlignContent", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void NegativeTest() {
            ConvertToPdfAndCompare("negative", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void NoWrapTest() {
            ConvertToPdfAndCompare("noWrap", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void RevertTest() {
            ConvertToPdfAndCompare("revert", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.RULE_IS_NOT_SUPPORTED, Count = 2)]
        public virtual void RevertLayerTest() {
            ConvertToPdfAndCompare("revertLayer", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void UnsetTest() {
            ConvertToPdfAndCompare("unset", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void WrapTest() {
            ConvertToPdfAndCompare("wrap", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }

        [NUnit.Framework.Test]
        public virtual void WrapReverseTest() {
            ConvertToPdfAndCompare("wrapReverse", SOURCE_FOLDER, DESTINATION_FOLDER, true);
        }
    }
}
