using System;
using System.Collections;
using System.Collections.Generic;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Css;
using iText.IO.Util;
using iText.Kernel.Colors;
using iText.Kernel.Colors.Gradients;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Util;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Apply.Util {
    public class BackgroundApplierUtilTest : ExtendedITextTest {
        private const double EPS = 0.000001;

        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/apply/util/BackgroundApplierUtilTest";

        [NUnit.Framework.Test]
        public virtual void BackgroundColorTest() {
            IPropertyContainer container = new _BodyHtmlStylesContainer_43();
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_COLOR, "red");
            BackgroundApplierUtil.ApplyBackground(props, new ProcessorContext(new ConverterProperties()), container);
        }

        private sealed class _BodyHtmlStylesContainer_43 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_43() {
            }

            public override void SetProperty(int property, Object value) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND, property);
                NUnit.Framework.Assert.IsTrue(value is Background);
                Background backgroundValue = (Background)value;
                NUnit.Framework.Assert.AreEqual(new DeviceRgb(1.0f, 0.0f, 0.0f), backgroundValue.GetColor());
                NUnit.Framework.Assert.AreEqual(1.0f, backgroundValue.GetOpacity(), BackgroundApplierUtilTest.EPS);
            }
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageTest() {
            String image = "url(rock_texture.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(sourceFolder));
            IPropertyContainer container = new _BodyHtmlStylesContainer_62(context, image);
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, image);
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_62 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_62(ProcessorContext context, String image) {
                this.context = context;
                this.image = image;
                this.innerContext = context;
                this.innerImage = image;
            }

            internal readonly ProcessorContext innerContext;

            internal readonly String innerImage;

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND_IMAGE, property);
                NUnit.Framework.Assert.IsTrue(propertyValue is IList);
                IList values = (IList)propertyValue;
                NUnit.Framework.Assert.AreEqual(1, values.Count);
                foreach (Object value in values) {
                    NUnit.Framework.Assert.IsTrue(value is BackgroundImage);
                    BackgroundImage image = (BackgroundImage)value;
                    PdfImageXObject pdfImage = image.GetImage();
                    NUnit.Framework.Assert.IsNotNull(pdfImage);
                    PdfXObject expectedImage = this.innerContext.GetResourceResolver().RetrieveImageExtended(CssUtils.ExtractUrl
                        (this.innerImage));
                    NUnit.Framework.Assert.IsTrue(expectedImage is PdfImageXObject);
                    NUnit.Framework.Assert.AreEqual(JavaUtil.ArraysToString(((PdfImageXObject)expectedImage).GetImageBytes()), 
                        JavaUtil.ArraysToString(pdfImage.GetImageBytes()));
                }
            }

            private readonly ProcessorContext context;

            private readonly String image;
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.Html2pdf.LogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI)]
        public virtual void BackgroundInvalidImageTest() {
            String image = "url(img.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(sourceFolder));
            IPropertyContainer container = new _BodyHtmlStylesContainer_97();
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, image);
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_97 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_97() {
            }

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.Fail();
            }
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageRepeatTest() {
            String image = "url(rock_texture.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(sourceFolder));
            IPropertyContainer container = new _BodyHtmlStylesContainer_113();
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, image);
            props.Put(CssConstants.BACKGROUND_REPEAT, "no-repeat");
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_113 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_113() {
            }

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND_IMAGE, property);
                NUnit.Framework.Assert.IsTrue(propertyValue is IList);
                IList values = (IList)propertyValue;
                NUnit.Framework.Assert.AreEqual(1, values.Count);
                foreach (Object value in values) {
                    NUnit.Framework.Assert.IsTrue(value is BackgroundImage);
                    BackgroundImage image = (BackgroundImage)value;
                    NUnit.Framework.Assert.IsFalse(image.IsRepeatX());
                    NUnit.Framework.Assert.IsFalse(image.IsRepeatY());
                }
            }
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImagesTest() {
            String images = "url(rock_texture.jpg),url(rock_texture2.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(sourceFolder));
            IPropertyContainer container = new _BodyHtmlStylesContainer_139(context, images);
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, images);
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_139 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_139(ProcessorContext context, String images) {
                this.context = context;
                this.images = images;
                this.innerContext = context;
                this.imagesArray = iText.IO.Util.StringUtil.Split(images, ",");
            }

            internal readonly ProcessorContext innerContext;

            internal readonly String[] imagesArray;

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND_IMAGE, property);
                NUnit.Framework.Assert.IsTrue(propertyValue is IList);
                IList values = (IList)propertyValue;
                NUnit.Framework.Assert.AreEqual(this.imagesArray.Length, values.Count);
                for (int i = 0; i < values.Count; i++) {
                    Object value = values[i];
                    NUnit.Framework.Assert.IsTrue(value is BackgroundImage);
                    BackgroundImage image = (BackgroundImage)value;
                    PdfImageXObject pdfImage = image.GetImage();
                    NUnit.Framework.Assert.IsNotNull(pdfImage);
                    PdfXObject expectedImage = this.innerContext.GetResourceResolver().RetrieveImageExtended(CssUtils.ExtractUrl
                        (this.imagesArray[i]));
                    NUnit.Framework.Assert.IsTrue(expectedImage is PdfImageXObject);
                    NUnit.Framework.Assert.AreEqual(JavaUtil.ArraysToString(((PdfImageXObject)expectedImage).GetImageBytes()), 
                        JavaUtil.ArraysToString(pdfImage.GetImageBytes()));
                }
            }

            private readonly ProcessorContext context;

            private readonly String images;
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImagesRepeatTest() {
            String images = "url(rock_texture.jpg),url(rock_texture2.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(sourceFolder));
            IPropertyContainer container = new _BodyHtmlStylesContainer_172(images);
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, images);
            props.Put(CssConstants.BACKGROUND_REPEAT, "no-repeat");
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_172 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_172(String images) {
                this.images = images;
                this.imagesArray = iText.IO.Util.StringUtil.Split(images, ",");
            }

            internal readonly String[] imagesArray;

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND_IMAGE, property);
                NUnit.Framework.Assert.IsTrue(propertyValue is IList);
                IList values = (IList)propertyValue;
                NUnit.Framework.Assert.AreEqual(this.imagesArray.Length, values.Count);
                for (int i = 0; i < values.Count; i++) {
                    Object value = values[i];
                    NUnit.Framework.Assert.IsTrue(value is BackgroundImage);
                    BackgroundImage image = (BackgroundImage)value;
                    NUnit.Framework.Assert.IsFalse(image.IsRepeatX());
                    NUnit.Framework.Assert.IsFalse(image.IsRepeatY());
                }
            }

            private readonly String images;
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImagesRepeatsTest() {
            String images = "url(rock_texture.jpg),url(rock_texture2.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(sourceFolder));
            IPropertyContainer container = new _BodyHtmlStylesContainer_200(images);
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, images);
            props.Put(CssConstants.BACKGROUND_REPEAT, "no-repeat,repeat");
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_200 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_200(String images) {
                this.images = images;
                this.imagesArray = iText.IO.Util.StringUtil.Split(images, ",");
            }

            internal readonly String[] imagesArray;

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND_IMAGE, property);
                NUnit.Framework.Assert.IsTrue(propertyValue is IList);
                IList values = (IList)propertyValue;
                NUnit.Framework.Assert.AreEqual(this.imagesArray.Length, values.Count);
                for (int i = 0; i < values.Count; i++) {
                    Object value = values[i];
                    NUnit.Framework.Assert.IsTrue(value is BackgroundImage);
                    BackgroundImage image = (BackgroundImage)value;
                    NUnit.Framework.Assert.AreNotEqual((i == 0), image.IsRepeatX());
                    NUnit.Framework.Assert.AreNotEqual((i == 0), image.IsRepeatY());
                }
            }

            private readonly String images;
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundLinearGradientsTest() {
            String gradients = "linear-gradient(red),linear-gradient(green),linear-gradient(blue)";
            String otterFontSize = "15px";
            IPropertyContainer container = new _BodyHtmlStylesContainer_228(gradients, otterFontSize);
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, gradients);
            props.Put(CssConstants.FONT_SIZE, "15px");
            BackgroundApplierUtil.ApplyBackground(props, new ProcessorContext(new ConverterProperties()), container);
        }

        private sealed class _BodyHtmlStylesContainer_228 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_228(String gradients, String otterFontSize) {
                this.gradients = gradients;
                this.otterFontSize = otterFontSize;
                this.gradientsArray = BackgroundApplierUtil.SplitStringWithComma(gradients);
                this.fontSize = CssUtils.ParseAbsoluteLength(otterFontSize);
            }

            internal readonly String[] gradientsArray;

            internal readonly float fontSize;

            public override void SetProperty(int property, Object value) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND_IMAGE, property);
                NUnit.Framework.Assert.IsTrue(value is IList);
                IList values = (IList)value;
                NUnit.Framework.Assert.AreEqual(this.gradientsArray.Length, values.Count);
                for (int i = 0; i < values.Count; ++i) {
                    NUnit.Framework.Assert.IsTrue(values[i] is BackgroundImage);
                    AbstractLinearGradientBuilder builder = ((BackgroundImage)values[i]).GetLinearGradientBuilder();
                    NUnit.Framework.Assert.IsTrue(builder is StrategyBasedLinearGradientBuilder);
                    StrategyBasedLinearGradientBuilder expectedGradientBuilder = CssGradientUtil.ParseCssLinearGradient(this.gradientsArray
                        [i], this.fontSize, this.fontSize);
                    NUnit.Framework.Assert.IsNotNull(expectedGradientBuilder);
                    StrategyBasedLinearGradientBuilder actualGradientBuilder = (StrategyBasedLinearGradientBuilder)builder;
                    NUnit.Framework.Assert.AreEqual(expectedGradientBuilder.GetSpreadMethod(), actualGradientBuilder.GetSpreadMethod
                        ());
                    NUnit.Framework.Assert.AreEqual(expectedGradientBuilder.GetColorStops(), actualGradientBuilder.GetColorStops
                        ());
                }
            }

            private readonly String gradients;

            private readonly String otterFontSize;
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundLinearGradientTest() {
            String otterGradient = "linear-gradient(red)";
            String otterFontSize = "15px";
            IPropertyContainer container = new _BodyHtmlStylesContainer_265(otterGradient, otterFontSize);
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, otterGradient);
            props.Put(CssConstants.FONT_SIZE, "15px");
            BackgroundApplierUtil.ApplyBackground(props, new ProcessorContext(new ConverterProperties()), container);
        }

        private sealed class _BodyHtmlStylesContainer_265 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_265(String otterGradient, String otterFontSize) {
                this.otterGradient = otterGradient;
                this.otterFontSize = otterFontSize;
                this.gradient = otterGradient;
                this.fontSize = CssUtils.ParseAbsoluteLength(otterFontSize);
            }

            internal readonly String gradient;

            internal readonly float fontSize;

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND_IMAGE, property);
                NUnit.Framework.Assert.IsTrue(propertyValue is IList);
                IList values = (IList)propertyValue;
                NUnit.Framework.Assert.AreEqual(1, values.Count);
                foreach (Object value in values) {
                    NUnit.Framework.Assert.IsTrue(value is BackgroundImage);
                    AbstractLinearGradientBuilder builder = ((BackgroundImage)value).GetLinearGradientBuilder();
                    NUnit.Framework.Assert.IsTrue(builder is StrategyBasedLinearGradientBuilder);
                    StrategyBasedLinearGradientBuilder expectedGradientBuilder = CssGradientUtil.ParseCssLinearGradient(this.gradient
                        , this.fontSize, this.fontSize);
                    NUnit.Framework.Assert.IsNotNull(expectedGradientBuilder);
                    StrategyBasedLinearGradientBuilder actualGradientBuilder = (StrategyBasedLinearGradientBuilder)builder;
                    NUnit.Framework.Assert.AreEqual(expectedGradientBuilder.GetSpreadMethod(), actualGradientBuilder.GetSpreadMethod
                        ());
                    NUnit.Framework.Assert.AreEqual(expectedGradientBuilder.GetColorStops(), actualGradientBuilder.GetColorStops
                        ());
                }
            }

            private readonly String otterGradient;

            private readonly String otterFontSize;
        }

        [NUnit.Framework.Test]
        public virtual void SplitStringWithCommaTest() {
            NUnit.Framework.Assert.AreEqual(new List<String>(), JavaUtil.ArraysAsList(BackgroundApplierUtil.SplitStringWithComma
                (null)));
            NUnit.Framework.Assert.AreEqual(JavaUtil.ArraysAsList("value1", "value2", "value3"), JavaUtil.ArraysAsList
                (BackgroundApplierUtil.SplitStringWithComma("value1,value2,value3")));
            NUnit.Framework.Assert.AreEqual(JavaUtil.ArraysAsList("value1", " value2", " value3"), JavaUtil.ArraysAsList
                (BackgroundApplierUtil.SplitStringWithComma("value1, value2, value3")));
            NUnit.Framework.Assert.AreEqual(JavaUtil.ArraysAsList("value1", "(value,with,comma)", "value3"), JavaUtil.ArraysAsList
                (BackgroundApplierUtil.SplitStringWithComma("value1,(value,with,comma),value3")));
            NUnit.Framework.Assert.AreEqual(JavaUtil.ArraysAsList("value1", "(val(ue,with,comma),value3"), JavaUtil.ArraysAsList
                (BackgroundApplierUtil.SplitStringWithComma("value1,(val(ue,with,comma),value3")));
            NUnit.Framework.Assert.AreEqual(JavaUtil.ArraysAsList("value1", "(value,with)", "comma)", "value3"), JavaUtil.ArraysAsList
                (BackgroundApplierUtil.SplitStringWithComma("value1,(value,with),comma),value3")));
            NUnit.Framework.Assert.AreEqual(JavaUtil.ArraysAsList("value1", "( v2,v3)", "(v4, v5)", "value3"), JavaUtil.ArraysAsList
                (BackgroundApplierUtil.SplitStringWithComma("value1,( v2,v3),(v4, v5),value3")));
            NUnit.Framework.Assert.AreEqual(JavaUtil.ArraysAsList("v.al*ue1\"", "( v2,v3)", "\"(v4,v5;);", "value3"), 
                JavaUtil.ArraysAsList(BackgroundApplierUtil.SplitStringWithComma("v.al*ue1\",( v2,v3),\"(v4,v5;);,value3"
                )));
        }
    }
}
