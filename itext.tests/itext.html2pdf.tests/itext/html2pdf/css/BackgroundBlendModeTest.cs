/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
Authors: iText Software.

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
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class BackgroundBlendModeTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/BackgroundBlendModeTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/BackgroundBlendModeTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundBlendModeColorTest() {
            ConvertToPdfAndCompare("backgroundBlendModeColor", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundBlendModeColorBurnTest() {
            ConvertToPdfAndCompare("backgroundBlendModeColorBurn", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundBlendModeColorDodgeTest() {
            ConvertToPdfAndCompare("backgroundBlendModeColorDodge", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundBlendModeDarkenTest() {
            ConvertToPdfAndCompare("backgroundBlendModeDarken", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundBlendModeDifferenceTest() {
            ConvertToPdfAndCompare("backgroundBlendModeDifference", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundBlendModeExclusionTest() {
            ConvertToPdfAndCompare("backgroundBlendModeExclusion", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundBlendModeHardLightTest() {
            ConvertToPdfAndCompare("backgroundBlendModeHardLight", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundBlendModeHueTest() {
            ConvertToPdfAndCompare("backgroundBlendModeHue", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundBlendModeLightenTest() {
            ConvertToPdfAndCompare("backgroundBlendModeLighten", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundBlendModeLuminosityTest() {
            ConvertToPdfAndCompare("backgroundBlendModeLuminosity", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundBlendModeMultiplyTest() {
            ConvertToPdfAndCompare("backgroundBlendModeMultiply", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundBlendModeNormalTest() {
            ConvertToPdfAndCompare("backgroundBlendModeNormal", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundBlendModeOverlayTest() {
            ConvertToPdfAndCompare("backgroundBlendModeOverlay", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundBlendModeSaturationTest() {
            ConvertToPdfAndCompare("backgroundBlendModeSaturation", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundBlendModeScreenTest() {
            ConvertToPdfAndCompare("backgroundBlendModeScreen", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundBlendModeSoftLightTest() {
            ConvertToPdfAndCompare("backgroundBlendModeSoftLight", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void OneImageTwoBackgroundBlendModesTest() {
            ConvertToPdfAndCompare("oneImageTwoBackgroundBlendModes", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void TwoImagesOneBackgroundBlendModeTest() {
            ConvertToPdfAndCompare("twoImagesOneBackgroundBlendMode", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ImageAndGradientWithBackgroundBlendModeTest() {
            ConvertToPdfAndCompare("imageAndGradientWithBackgroundBlendMode", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void PlacementOrderOfLayersAndBackgroundBlendModeTest() {
            ConvertToPdfAndCompare("placementOrderOfLayersAndBackgroundBlendMode", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundBlendModeTwoGradientsSaturationLuminosityTest() {
            ConvertToPdfAndCompare("backgroundBlendModeTwoGradientsSaturationLuminosity", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void ThreeBackgroundBlendModesLastNormalTest() {
            ConvertToPdfAndCompare("threeBackgroundBlendModesLastNormal", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void ThreeBackgroundBlendModesSecondNormalTest() {
            ConvertToPdfAndCompare("threeBackgroundBlendModesSecondNormal", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundBlendModeLetterCaseTest() {
            ConvertToPdfAndCompare("backgroundBlendModeLetterCase", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, LogLevel = LogLevelConstants
            .WARN, Count = 3)]
        public virtual void InvalidBackgroundBlendModeTest() {
            ConvertToPdfAndCompare("invalidBackgroundBlendMode", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageLinearGradientTest() {
            ConvertToPdfAndCompare("background-blend-mode-two-gradients-darken-lighten", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageLinearGradientTest2() {
            ConvertToPdfAndCompare("background-blend-mode-two-gradients-normal-darken", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.StyledXmlParser.LogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void InvalidBackgroundBlendModeValueTest() {
            ConvertToPdfAndCompare("background-blend-mode-invalid", sourceFolder, destinationFolder);
        }
    }
}
