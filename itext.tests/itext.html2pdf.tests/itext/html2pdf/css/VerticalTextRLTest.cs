/*
This file is part of the iText (R) project.
Copyright (c) 1998-2026 Apryse Group NV
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

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class VerticalTextRLTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/VerticalTextRLTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/VerticalTextRLTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.CSS_PROPERTY_IN_PERCENTS_NOT_SUPPORTED)]
        public virtual void VertRlAbsolutePositioningTest() {
            ConvertToPdfAndCompare("vertRlAbsolutePositioning", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlBackgroundDecorationTest() {
            ConvertToPdfAndCompare("vertRlBackgroundDecoration", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlBasicTest() {
            ConvertToPdfAndCompare("vertRlBasic", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlBlockquoteTest() {
            ConvertToPdfAndCompare("vertRlBlockquote", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlCjkTextTest() {
            ConvertToPdfAndCompare("vertRlCjkText", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlComboComplexTest() {
            ConvertToPdfAndCompare("vertRlComboComplex", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlComboFlexMixedTest() {
            ConvertToPdfAndCompare("vertRlComboFlexMixed", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlComboSpacingDecorationOverflowTest() {
            ConvertToPdfAndCompare("vertRlComboSpacingDecorationOverflow", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlComboWideDecoratedTest() {
            ConvertToPdfAndCompare("vertRlComboWideDecorated", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlCssColumnsTest() {
            ConvertToPdfAndCompare("vertRlCssColumns", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlFlexMinMaxTest() {
            ConvertToPdfAndCompare("vertRlFlexMinMax", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlFloatTest() {
            ConvertToPdfAndCompare("vertRlFloat", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlHeadingsTest() {
            ConvertToPdfAndCompare("vertRlHeadings", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlImageInlineBlockTest() {
            ConvertToPdfAndCompare("vertRlImageInlineBlock", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlLetterSpacingTest() {
            ConvertToPdfAndCompare("vertRlLetterSpacing", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlLineHeightTest() {
            ConvertToPdfAndCompare("vertRlLineHeight", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlListsTest() {
            ConvertToPdfAndCompare("vertRlLists", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlLongContainerTest() {
            ConvertToPdfAndCompare("vertRlLongContainer", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlLongTextTest() {
            ConvertToPdfAndCompare("vertRlLongText", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlMixedFontsTest() {
            ConvertToPdfAndCompare("vertRlMixedFonts", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlNoSoftWrapTest() {
            ConvertToPdfAndCompare("vertRlNoSoftWrap", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlOverflowTest() {
            ConvertToPdfAndCompare("vertRlOverflow", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlSpacingRatioTest() {
            ConvertToPdfAndCompare("vertRlSpacingRatio", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlTableCellTest() {
            ConvertToPdfAndCompare("vertRlTableCell", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlTextAlignTest() {
            ConvertToPdfAndCompare("vertRlTextAlign", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlTextCombineUprightTest() {
            ConvertToPdfAndCompare("vertRlTextCombineUpright", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlTextDecorationTest() {
            ConvertToPdfAndCompare("vertRlTextDecoration", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlUnderlinePositionTest() {
            ConvertToPdfAndCompare("vertRlUnderlinePosition", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlVerticalAlignTest() {
            ConvertToPdfAndCompare("vertRlVerticalAlign", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlWideMulticolumnTest() {
            ConvertToPdfAndCompare("vertRlWideMulticolumn", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlWordSpacingTest() {
            ConvertToPdfAndCompare("vertRlWordSpacing", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void VertRlZeroNegativeDimensionsTest() {
            ConvertToPdfAndCompare("vertRlZeroNegativeDimensions", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
