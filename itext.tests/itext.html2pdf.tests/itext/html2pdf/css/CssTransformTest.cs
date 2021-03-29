using System;
using iText.Html2pdf;

namespace iText.Html2pdf.Css {
    public class CssTransformTest : ExtendedHtmlConversionITextTest {
        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/CssTransformTest/";

        public static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/CssTransformTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformMatrixTest() {
            ConvertToPdfAndCompare("cssTransformMatrix", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformRotateTest() {
            ConvertToPdfAndCompare("cssTransformRotate", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformTranslateTest() {
            ConvertToPdfAndCompare("cssTransformTranslate", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformTranslateXTest() {
            ConvertToPdfAndCompare("cssTransformTranslateX", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformTranslateXHugeValueTest() {
            ConvertToPdfAndCompare("cssTransformTranslateXHugeValue", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformTranslateYTest() {
            ConvertToPdfAndCompare("cssTransformTranslateY", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformTranslateYHugeValueTest() {
            ConvertToPdfAndCompare("cssTransformTranslateYHugeValue", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformSkewTest() {
            ConvertToPdfAndCompare("cssTransformSkew", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformSkewXTest() {
            ConvertToPdfAndCompare("cssTransformSkewX", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformSkewYTest() {
            ConvertToPdfAndCompare("cssTransformSkewY", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformScaleTest() {
            ConvertToPdfAndCompare("cssTransformScale", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformScaleXTest() {
            ConvertToPdfAndCompare("cssTransformScaleX", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformScaleYTest() {
            ConvertToPdfAndCompare("cssTransformScaleY", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformCombinationTest() {
            ConvertToPdfAndCompare("cssTransformCombination", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CssTransformCellTest() {
            // TODO DEVSIX-2862 layout: improve block's TRANSFORM processing
            ConvertToPdfAndCompare("cssTransformCell", SOURCE_FOLDER, DESTINATION_FOLDER);
        }
    }
}
