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

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class FlexRowReverseTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FlexRowReverseTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FlexRowReverseTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 51)]
        public virtual void FlexDirRowReverseAlignContentBaselineTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-content-baseline", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowReverseAlignContentCenterTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-content-center", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void FlexDirRowReverseAlignContentEndTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-content-end", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void FlexDirRowReverseAlignContentFirstBaselineTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-content-first-baseline", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowReverseAlignContentFlexEndTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-content-flex-end", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowReverseAlignContentFlexStartTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-content-flex-start", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowReverseAlignContentNormalTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-content-normal", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowReverseAlignContentSpaceAroundTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-content-space-around", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowReverseAlignContentSpaceBetweenTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-content-space-between", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowReverseAlignContentSpaceEvenlyTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-content-space-evenly", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void FlexDirRowReverseAlignContentStartTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-content-start", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowReverseAlignContentStretchTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-content-stretch", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 51)]
        public virtual void FlexDirRowReverseAlignItemsBaselineTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-items-baseline", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowReverseAlignItemsCenterTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-items-center", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowReverseAlignItemsEndTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-items-end", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowReverseAlignItemsFlexEndTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-items-flex-end", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowReverseAlignItemsFlexStartTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-items-flex-start", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowReverseAlignItemsSelfEndTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-items-self-end", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowReverseAlignItemsSelfStartTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-items-self-start", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowReverseAlignItemsStartTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-items-start", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexDirRowReverseAlignItemsStretchTest() {
            ConvertToPdfAndCompare("flex-dir-row-reverse-align-items-stretch", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
