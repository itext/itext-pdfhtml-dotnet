using System;
using System.IO;
using iText.Html2pdf;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css {
    public class LinearGradientTest : ExtendedHtmlConversionITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/LinearGradientTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/LinearGradientTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundLinearGradientTest() {
            ConvertToPdfAndCompare("background-linear-gradient", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundLinearGradientWithTransformTest() {
            ConvertToPdfAndCompare("background-linear-gradient-with-transform", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageLinearGradientTest() {
            ConvertToPdfAndCompare("background-image-linear-gradient", sourceFolder, destinationFolder);
        }

        // TODO: DEVSIX-3595 update cmp_ after fix and remove log message expectation
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, Count = 3, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void BackgroundImageLinearGradientWithAnglesTest() {
            ConvertToPdfAndCompare("background-image-angles-linear-gradient", sourceFolder, destinationFolder);
        }

        // TODO: DEVSIX-3596 update cmp_ after fix and remove log message expectation
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, Count = 5, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void BackgroundImageLinearGradientWithMetricsTest() {
            ConvertToPdfAndCompare("background-image-metrics-linear-gradient", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageLinearGradientWithOffsetsTest() {
            ConvertToPdfAndCompare("background-image-offsets-linear-gradient", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundRepeatingLinearGradientTest() {
            ConvertToPdfAndCompare("background-repeating-linear-gradient", sourceFolder, destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageRepeatingLinearGradientTest() {
            ConvertToPdfAndCompare("background-image-repeating-linear-gradient", sourceFolder, destinationFolder);
        }

        // TODO: DEVSIX-3595 update cmp_ after fix and remove log message expectation
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, Count = 3, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void BackgroundImageRepeatingLinearGradientWithAnglesTest() {
            ConvertToPdfAndCompare("background-image-angles-repeating-linear-gradient", sourceFolder, destinationFolder
                );
        }

        // TODO: DEVSIX-3596 update cmp_ after fix and remove log message expectation
        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, Count = 5, LogLevel = LogLevelConstants
            .WARN)]
        public virtual void BackgroundImageRepeatingLinearGradientWithMetricsTest() {
            ConvertToPdfAndCompare("background-image-metrics-repeating-linear-gradient", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageRepeatingLinearGradientWithOffsetsTest() {
            ConvertToPdfAndCompare("background-image-offsets-repeating-linear-gradient", sourceFolder, destinationFolder
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN
            )]
        public virtual void InvalidFirstArgumentTest() {
            ConvertHtmlWithGradient("linear-gradient(not-angle-or-color, orange 100pt, red 150pt, green 200pt, blue 250pt)"
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN
            )]
        public virtual void InvalidToSideTest0() {
            ConvertHtmlWithGradient("linear-gradient(to , orange 100pt, red 150pt, green 200pt, blue 250pt)");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN
            )]
        public virtual void InvalidToSideTest1() {
            ConvertHtmlWithGradient("linear-gradient(to, orange 100pt, red 150pt, green 200pt, blue 250pt)");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN
            )]
        public virtual void InvalidToSideTest2() {
            ConvertHtmlWithGradient("linear-gradient(to left left, orange 100pt, red 150pt, green 200pt, blue 250pt)");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN
            )]
        public virtual void InvalidToSideTest3() {
            ConvertHtmlWithGradient("linear-gradient(to bottom top, orange 100pt, red 150pt, green 200pt, blue 250pt)"
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN
            )]
        public virtual void InvalidToSideTest4() {
            ConvertHtmlWithGradient("linear-gradient(to left right, orange 100pt, red 150pt, green 200pt, blue 250pt)"
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN
            )]
        public virtual void InvalidToSideTest5() {
            ConvertHtmlWithGradient("linear-gradient(to top right right, orange 100pt, red 150pt, green 200pt, blue 250pt)"
                );
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN
            )]
        public virtual void InvalidColorWithThreeOffsetsValueTest() {
            ConvertHtmlWithGradient("linear-gradient(red, orange 20pt 30pt 100pt, green 200pt, blue 250pt)");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN
            )]
        public virtual void InvalidColorOffsetValueTest() {
            ConvertHtmlWithGradient("linear-gradient(red, orange 20, green 200pt, blue 250pt)");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN
            )]
        public virtual void InvalidMultipleHintsInARowValueTest() {
            ConvertHtmlWithGradient("linear-gradient(red, orange, 20%, 30%, green 200pt, blue 250pt)");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN
            )]
        public virtual void InvalidMultipleHintsInARowWithoutCommaValueTest() {
            ConvertHtmlWithGradient("linear-gradient(red, orange, 20% 30%, green 200pt, blue 250pt)");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN
            )]
        public virtual void InvalidFirstElementIsAHintValueTest() {
            ConvertHtmlWithGradient("linear-gradient(5%, red, orange, 30%, green 200pt, blue 250pt)");
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.INVALID_GRADIENT_DECLARATION, LogLevel = LogLevelConstants.WARN
            )]
        public virtual void InvalidLastElementIsAHintValueTest() {
            ConvertHtmlWithGradient("linear-gradient(red, orange, 30%, green 200pt, blue 250pt, 120%)");
        }

        private void ConvertHtmlWithGradient(String gradientString) {
            String html = "<!DOCTYPE html><html lang=\"en\">" + "<head><meta charset=\"UTF-8\"></head><body><div style=\"background-image: "
                 + gradientString + ";\"></div></body></html>";
            using (Stream os = new MemoryStream()) {
                HtmlConverter.ConvertToPdf(html, os);
            }
        }
    }
}
