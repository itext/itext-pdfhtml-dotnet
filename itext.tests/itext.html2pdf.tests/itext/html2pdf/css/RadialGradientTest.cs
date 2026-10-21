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
using System.IO;
using iText.Html2pdf;
using iText.Html2pdf.Logs;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class RadialGradientTest : ExtendedHtmlConversionITextTest {
        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/RadialGradientTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/RadialGradientTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundRadialGradientTest() {
            ConvertToPdfAndCompare("background-radial-gradient", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundRadialGradientWithSlashInColorTest() {
            ConvertToPdfAndCompare("background-radial-gradient-with-slash-in-color", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundRadialGradientWithTransformTest() {
            ConvertToPdfAndCompare("background-radial-gradient-with-transform", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageRadialGradientTest() {
            ConvertToPdfAndCompare("background-image-radial-gradient", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, Count = 5, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void BackgroundImageRadialGradientWithMetricsTest() {
            ConvertToPdfAndCompare("background-image-metrics-radial-gradient", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageRadialGradientWithOffsetsTest() {
            ConvertToPdfAndCompare("background-image-offsets-radial-gradient", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundRepeatingRadialGradientTest() {
            ConvertToPdfAndCompare("background-repeating-radial-gradient", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageRepeatingRadialGradientTest() {
            ConvertToPdfAndCompare("background-image-repeating-radial-gradient", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, Count = 5, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void BackgroundImageRepeatingRadialGradientWithMetricsTest() {
            ConvertToPdfAndCompare("background-image-metrics-repeating-radial-gradient", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageRepeatingRadialGradientWithOffsetsTest() {
            ConvertToPdfAndCompare("background-image-offsets-repeating-radial-gradient", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        // Pdf differs from html, same as backgroundImageLinearGradientInBodyTest
        [NUnit.Framework.Test]
        public virtual void BackgroundImageRadialGradientInBodyTest() {
            ConvertToPdfAndCompare("backgroundImageRadialGradientInBody", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        // Pdf differs from html but backgroundImageLinearGradientInSeveralElementsTest is the same
        [NUnit.Framework.Test]
        public virtual void BackgroundImageRadialGradientInSeveralElementsTest() {
            ConvertToPdfAndCompare("backgroundImageRadialGradientInSeveralElements", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageRadialGradientInSpanTest() {
            ConvertToPdfAndCompare("backgroundImageRadialGradientInSpan", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageRadialGradientInTableTest() {
            ConvertToPdfAndCompare("backgroundImageRadialGradientInTable", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageRadialGradientInButtonTest() {
            ConvertToPdfAndCompare("backgroundImageRadialGradientInButton", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RadialGradientExtentKeywordsTest() {
            ConvertToPdfAndCompare("radial-gradient-extent-keywords", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RadialGradientCircleOneLengthTest() {
            ConvertToPdfAndCompare("radial-gradient-circle-one-length", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        // Nothing is renderer in browsers but https://www.w3.org/TR/css-images-4/#radial-size allows it
        [NUnit.Framework.Test]
        public virtual void RadialGradientCirclePercentageSizeTest() {
            ConvertToPdfAndCompare("radial-gradient-circle-percentage-size", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RadialGradientEllipsePercentageSizeTest() {
            ConvertToPdfAndCompare("radial-gradient-ellipse-percentage-size", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RadialGradientPercentagePositionTest() {
            ConvertToPdfAndCompare("radial-gradient-percentage-position", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RadialGradientOffsetPositionTest() {
            ConvertToPdfAndCompare("radial-gradient-offset-position", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RadialGradientExplicitSizeTest() {
            ConvertToPdfAndCompare("radial-gradient-explicit-size", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void RadialGradientShapeGrammarTest() {
            ConvertToPdfAndCompare("radial-gradient-shape", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, Count = 2, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void RadialGradientSizeGrammarTest() {
            ConvertToPdfAndCompare("radial-gradient-size", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, Count = 2, LogLevel = LogLevelConstants
            .WARN)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            Count = 2, LogLevel = LogLevelConstants.WARN)]
        public virtual void RadialGradientPositionGrammarTest() {
            ConvertToPdfAndCompare("radial-gradient-position", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN)]
        [LogMessage(iText.StyledXmlParser.Logs.StyledXmlParserLogMessageConstant.INVALID_CSS_PROPERTY_DECLARATION, 
            LogLevel = LogLevelConstants.WARN)]
        public virtual void RadialGradientInterpolationGrammarTest() {
            ConvertToPdfAndCompare("radial-gradient-interpolation", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, Count = 4, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void RadialGradientColorStopListGrammarTest() {
            ConvertToPdfAndCompare("radial-gradient-color-stop-list", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListStyleImageRadialGradientTest() {
            ConvertToPdfAndCompare("list-style-image-radial-gradient", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ListStyleShorthandRadialGradientTest() {
            ConvertToPdfAndCompare("list-style-shorthand-radial-gradient", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN)]
        public virtual void InvalidRadialGradientKeywordTest() {
            ConvertToPdfAndCompare("invalid-radial-gradient-keyword", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN)]
        public virtual void InvalidColorWithThreeOffsetsValueTest() {
            ConvertToPdfAndCompare("invalid-color-with-three-offsets-radial-gradient", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN)]
        public virtual void InvalidColorOffsetValueTest() {
            ConvertToPdfAndCompare("invalid-color-offset-radial-gradient", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN)]
        public virtual void InvalidMultipleHintsInARowValueTest() {
            ConvertToPdfAndCompare("invalid-multiple-hints-in-a-row-radial-gradient", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN)]
        public virtual void InvalidMultipleHintsInARowWithoutCommaValueTest() {
            ConvertToPdfAndCompare("invalid-multiple-hints-in-a-row-without-comma-radial-gradient", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN)]
        public virtual void InvalidFirstElementIsAHintValueTest() {
            ConvertToPdfAndCompare("invalid-first-element-is-a-hint-radial-gradient", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN)]
        public virtual void InvalidLastElementIsAHintValueTest() {
            ConvertToPdfAndCompare("invalid-last-element-is-a-hint-radial-gradient", SOURCE_FOLDER, DESTINATION_FOLDER
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN)]
        public virtual void InvalidRadialGradientCircleTwoSizesTest() {
            ConvertHtmlWithGradient("radial-gradient(circle 100px 200px, red, blue)");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN)]
        public virtual void InvalidRadialGradientEllipseSingleSizeTest() {
            ConvertHtmlWithGradient("radial-gradient(ellipse 100px, red, blue)");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN)]
        public virtual void InvalidRadialGradientAtWithoutPositionTest() {
            ConvertHtmlWithGradient("radial-gradient(at, red, blue)");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN)]
        public virtual void InvalidRadialGradientMultipleAtTest() {
            ConvertHtmlWithGradient("radial-gradient(circle at center at top, red, blue)");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN)]
        public virtual void InvalidRadialGradientNegativeRadiusTest() {
            ConvertHtmlWithGradient("radial-gradient(-50px, red, blue)");
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN)]
        public virtual void InvalidRadialGradientNoStopsTest() {
            ConvertHtmlWithGradient("radial-gradient(circle)");
        }

        private static void ConvertHtmlWithGradient(String gradient) {
            String html = "<div style=\"background-image: " + gradient + "; width: 500pt; height: 100pt;\"></div>";
            using (Stream outputStream = new MemoryStream()) {
                HtmlConverter.ConvertToPdf(html, outputStream);
            }
        }
    }
}
