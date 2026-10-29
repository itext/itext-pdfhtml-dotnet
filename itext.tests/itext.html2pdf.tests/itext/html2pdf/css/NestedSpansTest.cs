using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("IntegrationTest")]
    public class NestedSpansTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/NestedSpansTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/NestedSpansTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NestedSpansDifferentNestingLevelsTest() {
            ConvertToPdfAndCompare("nestedSpansDifferentNestingLevels", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NestedSpansDifferentNestingLevelsNoFloatTest() {
            ConvertToPdfAndCompare("nestedSpansDifferentNestingLevelsNoFloat", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NestedSpansPropertiesLevelsTest() {
            ConvertToPdfAndCompare("nestedSpansPropertiesLevels", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanParentPaddingOverrideTest() {
            ConvertToPdfAndCompare("spanParentPaddingOverrideTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanParentCornerRadiusSplitTest() {
            ConvertToPdfAndCompare("spanParentCornerRadiusSplitTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanParentBorderSidePreserveTest() {
            ConvertToPdfAndCompare("spanParentBorderSidePreserveTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanParentMarginPaddingCombineOverrideTest() {
            ConvertToPdfAndCompare("spanParentMarginPaddingCombineOverrideTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void SpanParentDifferentTopBottomMarginTest() {
            ConvertToPdfAndCompare("spanParentDifferentTopBottomMarginTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ChildSpanMarginDoubledTest() {
            ConvertToPdfAndCompare("childSpanMarginDoubled", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ParentPaddingIncreasesChildBackgroundTest() {
            ConvertToPdfAndCompare("parentPaddingIncreasesChildBackgroundTest", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
