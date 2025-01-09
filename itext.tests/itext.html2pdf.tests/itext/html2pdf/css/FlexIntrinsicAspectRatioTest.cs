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
    public class FlexIntrinsicAspectRatioTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/FlexIntrinsicAspectRatioTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/FlexIntrinsicAspectRatioTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void AutoFixedHeightFixedWidthIndefiniteContainerTest() {
            ConvertToPdfAndCompare("autoFixedHeightFixedWidthIndefiniteContainer", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void AutoFixedHeightUnfixedWidthDefiniteContainerTest() {
            ConvertToPdfAndCompare("autoFixedHeightUnfixedWidthDefiniteContainer", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void ContentFixedHeightFixedWidthIndefiniteContainerTest() {
            ConvertToPdfAndCompare("contentFixedHeightFixedWidthIndefiniteContainer", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void ContentFixedHeightUnfixedWidthIndefiniteContainerTest() {
            ConvertToPdfAndCompare("contentFixedHeightUnfixedWidthIndefiniteContainer", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void ContentUnfixedHeightUnfixedWidthDefiniteContainerStartTest() {
            ConvertToPdfAndCompare("contentUnfixedHeightUnfixedWidthDefiniteContainerStart", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void ContentUnfixedHeightUnfixedWidthDefiniteContainerStretchTest() {
            // Both firefox and chrome work incorrectly in this case.
            // Paragraph https://www.w3.org/TR/css-flexbox-1/#algo-stretch from the specification explicitly says,
            // that stretch does not affect the main size of the flex item, even if it has an intrinsic aspect ratio.
            ConvertToPdfAndCompare("contentUnfixedHeightUnfixedWidthDefiniteContainerStretch", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void AutoPercentageWidthHeightContainerMinHeightTest() {
            // Firefox works incorrectly in this case
            ConvertToPdfAndCompare("autoPercentageWidthHeightContainerMinHeight", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void InlineSvgImageTest() {
            ConvertToPdfAndCompare("inlineSvgImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ExternalSvgImageTest() {
            ConvertToPdfAndCompare("externalSvgImage", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
