/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
using System.Collections;
using System.Collections.Generic;
using iText.Commons.Utils;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Css;
using iText.Html2pdf.Logs;
using iText.Kernel.Colors;
using iText.Kernel.Colors.Gradients;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Util;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Apply.Util {
    [NUnit.Framework.Category("UnitTest")]
    public class BackgroundApplierUtilTest : ExtendedITextTest {
        private const double EPS = 0.000001;

        public static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/apply/util/BackgroundApplierUtilTest";

        [NUnit.Framework.Test]
        public virtual void BackgroundColorTest() {
            IPropertyContainer container = new _BodyHtmlStylesContainer_68();
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_COLOR, "red");
            BackgroundApplierUtil.ApplyBackground(props, new ProcessorContext(new ConverterProperties()), container);
        }

        private sealed class _BodyHtmlStylesContainer_68 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_68() {
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
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new _BodyHtmlStylesContainer_87(context, image);
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, image);
            props.Put(CssConstants.FONT_SIZE, "15pt");
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_87 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_87(ProcessorContext context, String image) {
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
                    PdfXObject expectedImage = this.innerContext.GetResourceResolver().RetrieveImage(CssUtils.ExtractUrl(this.
                        innerImage));
                    NUnit.Framework.Assert.IsTrue(expectedImage is PdfImageXObject);
                    NUnit.Framework.Assert.AreEqual(JavaUtil.ArraysToString(((PdfImageXObject)expectedImage).GetImageBytes()), 
                        JavaUtil.ArraysToString(pdfImage.GetImageBytes()));
                }
            }

            private readonly ProcessorContext context;

            private readonly String image;
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.UNABLE_TO_RETRIEVE_IMAGE_WITH_GIVEN_BASE_URI)]
        public virtual void BackgroundInvalidImageTest() {
            String image = "url(img.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new _BodyHtmlStylesContainer_123();
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, image);
            props.Put(CssConstants.FONT_SIZE, "15pt");
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_123 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_123() {
            }

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.Fail();
            }
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageRepeatTest() {
            String image = "url(rock_texture.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new _BodyHtmlStylesContainer_140();
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, image);
            props.Put(CssConstants.BACKGROUND_REPEAT, "no-repeat");
            props.Put(CssConstants.FONT_SIZE, "15pt");
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_140 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_140() {
            }

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND_IMAGE, property);
                NUnit.Framework.Assert.IsTrue(propertyValue is IList);
                IList values = (IList)propertyValue;
                NUnit.Framework.Assert.AreEqual(1, values.Count);
                foreach (Object value in values) {
                    NUnit.Framework.Assert.IsTrue(value is BackgroundImage);
                    BackgroundImage image = (BackgroundImage)value;
                    NUnit.Framework.Assert.AreEqual(BackgroundRepeat.BackgroundRepeatValue.NO_REPEAT, image.GetRepeat().GetXAxisRepeat
                        ());
                    NUnit.Framework.Assert.AreEqual(BackgroundRepeat.BackgroundRepeatValue.NO_REPEAT, image.GetRepeat().GetYAxisRepeat
                        ());
                }
            }
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageInvalidRepeatTest() {
            String image = "url(rock_texture.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new _BodyHtmlStylesContainer_167();
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, image);
            props.Put(CssConstants.BACKGROUND_REPEAT, "j");
            props.Put(CssConstants.FONT_SIZE, "15pt");
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_167 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_167() {
            }

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND_IMAGE, property);
                NUnit.Framework.Assert.IsTrue(propertyValue is IList);
                IList values = (IList)propertyValue;
                NUnit.Framework.Assert.AreEqual(1, values.Count);
                foreach (Object value in values) {
                    NUnit.Framework.Assert.IsTrue(value is BackgroundImage);
                    BackgroundImage image = (BackgroundImage)value;
                    NUnit.Framework.Assert.AreEqual(BackgroundRepeat.BackgroundRepeatValue.REPEAT, image.GetRepeat().GetXAxisRepeat
                        ());
                    NUnit.Framework.Assert.AreEqual(BackgroundRepeat.BackgroundRepeatValue.REPEAT, image.GetRepeat().GetYAxisRepeat
                        ());
                }
            }
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImagesTest() {
            String images = "url(rock_texture.jpg),url(rock_texture2.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new _BodyHtmlStylesContainer_194(context, images);
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, images);
            props.Put(CssConstants.FONT_SIZE, "15pt");
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_194 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_194(ProcessorContext context, String images) {
                this.context = context;
                this.images = images;
                this.innerContext = context;
                this.imagesArray = iText.Commons.Utils.StringUtil.Split(images, ",");
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
                    PdfXObject expectedImage = this.innerContext.GetResourceResolver().RetrieveImage(CssUtils.ExtractUrl(this.
                        imagesArray[i]));
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
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new _BodyHtmlStylesContainer_228(images);
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, images);
            props.Put(CssConstants.BACKGROUND_REPEAT, "no-repeat");
            props.Put(CssConstants.FONT_SIZE, "15pt");
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_228 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_228(String images) {
                this.images = images;
                this.imagesArray = iText.Commons.Utils.StringUtil.Split(images, ",");
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
                    NUnit.Framework.Assert.AreEqual(BackgroundRepeat.BackgroundRepeatValue.NO_REPEAT, image.GetRepeat().GetXAxisRepeat
                        ());
                    NUnit.Framework.Assert.AreEqual(BackgroundRepeat.BackgroundRepeatValue.NO_REPEAT, image.GetRepeat().GetYAxisRepeat
                        ());
                }
            }

            private readonly String images;
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImagesRepeatsTest() {
            String images = "url(rock_texture.jpg),url(rock_texture2.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new _BodyHtmlStylesContainer_257(images);
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, images);
            props.Put(CssConstants.BACKGROUND_REPEAT, "no-repeat,repeat");
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_257 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_257(String images) {
                this.images = images;
                this.imagesArray = iText.Commons.Utils.StringUtil.Split(images, ",");
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
                    if (i == 0) {
                        NUnit.Framework.Assert.AreEqual(BackgroundRepeat.BackgroundRepeatValue.NO_REPEAT, image.GetRepeat().GetXAxisRepeat
                            ());
                        NUnit.Framework.Assert.AreEqual(BackgroundRepeat.BackgroundRepeatValue.NO_REPEAT, image.GetRepeat().GetYAxisRepeat
                            ());
                    }
                    else {
                        NUnit.Framework.Assert.AreEqual(BackgroundRepeat.BackgroundRepeatValue.REPEAT, image.GetRepeat().GetXAxisRepeat
                            ());
                        NUnit.Framework.Assert.AreEqual(BackgroundRepeat.BackgroundRepeatValue.REPEAT, image.GetRepeat().GetYAxisRepeat
                            ());
                    }
                }
            }

            private readonly String images;
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundLinearGradientsTest() {
            String gradients = "linear-gradient(red),linear-gradient(green),linear-gradient(blue)";
            String otterFontSize = "15px";
            IPropertyContainer container = new _BodyHtmlStylesContainer_291(gradients, otterFontSize);
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, gradients);
            props.Put(CssConstants.FONT_SIZE, "15px");
            BackgroundApplierUtil.ApplyBackground(props, new ProcessorContext(new ConverterProperties()), container);
        }

        private sealed class _BodyHtmlStylesContainer_291 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_291(String gradients, String otterFontSize) {
                this.gradients = gradients;
                this.otterFontSize = otterFontSize;
                this.gradientsArray = CssUtils.SplitStringWithComma(gradients);
                this.fontSize = CssDimensionParsingUtils.ParseAbsoluteLength(otterFontSize);
            }

            internal readonly IList<String> gradientsArray;

            internal readonly float fontSize;

            public override void SetProperty(int property, Object value) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND_IMAGE, property);
                NUnit.Framework.Assert.IsTrue(value is IList);
                IList values = (IList)value;
                NUnit.Framework.Assert.AreEqual(this.gradientsArray.Count, values.Count);
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
            IPropertyContainer container = new _BodyHtmlStylesContainer_328(otterGradient, otterFontSize);
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, otterGradient);
            props.Put(CssConstants.FONT_SIZE, "15px");
            BackgroundApplierUtil.ApplyBackground(props, new ProcessorContext(new ConverterProperties()), container);
        }

        private sealed class _BodyHtmlStylesContainer_328 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_328(String otterGradient, String otterFontSize) {
                this.otterGradient = otterGradient;
                this.otterFontSize = otterFontSize;
                this.gradient = otterGradient;
                this.fontSize = CssDimensionParsingUtils.ParseAbsoluteLength(otterFontSize);
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
        public virtual void BackgroundImagePositionTest() {
            String image = "url(rock_texture.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new _BodyHtmlStylesContainer_364();
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, image);
            props.Put(CssConstants.BACKGROUND_POSITION_X, "right");
            props.Put(CssConstants.BACKGROUND_POSITION_Y, "center");
            props.Put(CssConstants.FONT_SIZE, "15pt");
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_364 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_364() {
            }

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND_IMAGE, property);
                NUnit.Framework.Assert.IsTrue(propertyValue is IList);
                IList values = (IList)propertyValue;
                NUnit.Framework.Assert.AreEqual(1, values.Count);
                foreach (Object value in values) {
                    NUnit.Framework.Assert.IsTrue(value is BackgroundImage);
                    BackgroundImage image = (BackgroundImage)value;
                    NUnit.Framework.Assert.AreEqual(BackgroundPosition.PositionX.RIGHT, image.GetBackgroundPosition().GetPositionX
                        ());
                    NUnit.Framework.Assert.AreEqual(BackgroundPosition.PositionY.CENTER, image.GetBackgroundPosition().GetPositionY
                        ());
                }
            }
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageInvalidPositionTest() {
            String image = "url(rock_texture.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new _BodyHtmlStylesContainer_394();
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, image);
            props.Put(CssConstants.BACKGROUND_POSITION_X, "j");
            props.Put(CssConstants.FONT_SIZE, "15pt");
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_394 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_394() {
            }

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND_IMAGE, property);
                NUnit.Framework.Assert.IsTrue(propertyValue is IList);
                IList values = (IList)propertyValue;
                NUnit.Framework.Assert.AreEqual(1, values.Count);
                foreach (Object value in values) {
                    NUnit.Framework.Assert.IsTrue(value is BackgroundImage);
                    BackgroundImage image = (BackgroundImage)value;
                    NUnit.Framework.Assert.AreEqual(BackgroundPosition.PositionX.LEFT, image.GetBackgroundPosition().GetPositionX
                        ());
                    NUnit.Framework.Assert.AreEqual(BackgroundPosition.PositionY.TOP, image.GetBackgroundPosition().GetPositionY
                        ());
                }
            }
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImageEmptyPositionTest() {
            String image = "url(rock_texture.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new _BodyHtmlStylesContainer_422();
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, image);
            props.Put(CssConstants.BACKGROUND_POSITION_X, "");
            props.Put(CssConstants.BACKGROUND_POSITION_Y, "");
            props.Put(CssConstants.FONT_SIZE, "15pt");
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_422 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_422() {
            }

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND_IMAGE, property);
                NUnit.Framework.Assert.IsTrue(propertyValue is IList);
                IList values = (IList)propertyValue;
                NUnit.Framework.Assert.AreEqual(1, values.Count);
                foreach (Object value in values) {
                    NUnit.Framework.Assert.IsTrue(value is BackgroundImage);
                    BackgroundImage image = (BackgroundImage)value;
                    NUnit.Framework.Assert.AreEqual(BackgroundPosition.PositionX.LEFT, image.GetBackgroundPosition().GetPositionX
                        ());
                    NUnit.Framework.Assert.AreEqual(BackgroundPosition.PositionY.TOP, image.GetBackgroundPosition().GetPositionY
                        ());
                }
            }
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImagesLeftBottomPositionTest() {
            String images = "url(rock_texture.jpg),url(rock_texture2.jpg),url(rock_texture.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new _BodyHtmlStylesContainer_451(images);
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, images);
            props.Put(CssConstants.BACKGROUND_POSITION_X, "left");
            props.Put(CssConstants.BACKGROUND_POSITION_Y, "bottom 20pt");
            props.Put(CssConstants.FONT_SIZE, "15pt");
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_451 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_451(String images) {
                this.images = images;
                this.imagesArray = iText.Commons.Utils.StringUtil.Split(images, ",");
            }

            internal readonly String[] imagesArray;

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND_IMAGE, property);
                NUnit.Framework.Assert.IsTrue(propertyValue is IList);
                IList values = (IList)propertyValue;
                NUnit.Framework.Assert.AreEqual(this.imagesArray.Length, values.Count);
                BackgroundPosition position = new BackgroundPosition().SetPositionX(BackgroundPosition.PositionX.LEFT).SetPositionY
                    (BackgroundPosition.PositionY.BOTTOM).SetYShift(UnitValue.CreatePointValue(20));
                foreach (Object value in values) {
                    NUnit.Framework.Assert.IsTrue(value is BackgroundImage);
                    BackgroundImage image = (BackgroundImage)value;
                    NUnit.Framework.Assert.AreEqual(position, image.GetBackgroundPosition());
                }
            }

            private readonly String images;
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImagesRightTopPositionTest() {
            String images = "url(rock_texture.jpg),url(rock_texture2.jpg),url(rock_texture.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new _BodyHtmlStylesContainer_481(images);
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, images);
            props.Put(CssConstants.BACKGROUND_POSITION_X, "right 30pt");
            props.Put(CssConstants.BACKGROUND_POSITION_Y, "top");
            props.Put(CssConstants.FONT_SIZE, "15pt");
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_481 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_481(String images) {
                this.images = images;
                this.imagesArray = iText.Commons.Utils.StringUtil.Split(images, ",");
            }

            internal readonly String[] imagesArray;

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND_IMAGE, property);
                NUnit.Framework.Assert.IsTrue(propertyValue is IList);
                IList values = (IList)propertyValue;
                NUnit.Framework.Assert.AreEqual(this.imagesArray.Length, values.Count);
                BackgroundPosition position = new BackgroundPosition().SetPositionX(BackgroundPosition.PositionX.RIGHT).SetPositionY
                    (BackgroundPosition.PositionY.TOP).SetXShift(UnitValue.CreatePointValue(30));
                foreach (Object value in values) {
                    NUnit.Framework.Assert.IsTrue(value is BackgroundImage);
                    BackgroundImage image = (BackgroundImage)value;
                    NUnit.Framework.Assert.AreEqual(position, image.GetBackgroundPosition());
                }
            }

            private readonly String images;
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImagesCenterCenterPositionTest() {
            String images = "url(rock_texture.jpg),url(rock_texture2.jpg),url(rock_texture.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new _BodyHtmlStylesContainer_511(images);
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, images);
            props.Put(CssConstants.BACKGROUND_POSITION_X, "center");
            props.Put(CssConstants.BACKGROUND_POSITION_Y, "center");
            props.Put(CssConstants.FONT_SIZE, "15pt");
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_511 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_511(String images) {
                this.images = images;
                this.imagesArray = iText.Commons.Utils.StringUtil.Split(images, ",");
            }

            internal readonly String[] imagesArray;

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND_IMAGE, property);
                NUnit.Framework.Assert.IsTrue(propertyValue is IList);
                IList values = (IList)propertyValue;
                NUnit.Framework.Assert.AreEqual(this.imagesArray.Length, values.Count);
                BackgroundPosition position = new BackgroundPosition().SetPositionX(BackgroundPosition.PositionX.CENTER).SetPositionY
                    (BackgroundPosition.PositionY.CENTER);
                foreach (Object value in values) {
                    NUnit.Framework.Assert.IsTrue(value is BackgroundImage);
                    BackgroundImage image = (BackgroundImage)value;
                    NUnit.Framework.Assert.AreEqual(position, image.GetBackgroundPosition());
                }
            }

            private readonly String images;
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImagesPositionMissedTest() {
            String images = "url(rock_texture.jpg),url(rock_texture2.jpg),url(rock_texture.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new _BodyHtmlStylesContainer_541(images);
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, images);
            props.Put(CssConstants.BACKGROUND_POSITION_X, "left, center");
            props.Put(CssConstants.BACKGROUND_POSITION_Y, "center, bottom");
            props.Put(CssConstants.FONT_SIZE, "15pt");
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_541 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_541(String images) {
                this.images = images;
                this.imagesArray = iText.Commons.Utils.StringUtil.Split(images, ",");
            }

            internal readonly String[] imagesArray;

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND_IMAGE, property);
                NUnit.Framework.Assert.IsTrue(propertyValue is IList);
                IList values = (IList)propertyValue;
                NUnit.Framework.Assert.AreEqual(this.imagesArray.Length, values.Count);
                BackgroundPosition[] positions = new BackgroundPosition[] { new BackgroundPosition().SetPositionX(BackgroundPosition.PositionX
                    .LEFT).SetPositionY(BackgroundPosition.PositionY.CENTER), new BackgroundPosition().SetPositionX(BackgroundPosition.PositionX
                    .CENTER).SetPositionY(BackgroundPosition.PositionY.BOTTOM), new BackgroundPosition().SetPositionX(BackgroundPosition.PositionX
                    .LEFT).SetPositionY(BackgroundPosition.PositionY.CENTER) };
                for (int i = 0; i < values.Count; i++) {
                    Object value = values[i];
                    NUnit.Framework.Assert.IsTrue(value is BackgroundImage);
                    BackgroundImage image = (BackgroundImage)value;
                    NUnit.Framework.Assert.AreEqual(positions[i], image.GetBackgroundPosition());
                }
            }

            private readonly String images;
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundImagesPositionsTest() {
            String images = "url(rock_texture.jpg),url(rock_texture2.jpg),url(rock_texture.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new _BodyHtmlStylesContainer_578(images);
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, images);
            props.Put(CssConstants.BACKGROUND_POSITION_X, "left,left,right");
            props.Put(CssConstants.BACKGROUND_POSITION_Y, "top, bottom,top");
            props.Put(CssConstants.FONT_SIZE, "15pt");
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_578 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_578(String images) {
                this.images = images;
                this.imagesArray = iText.Commons.Utils.StringUtil.Split(images, ",");
            }

            internal readonly String[] imagesArray;

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND_IMAGE, property);
                NUnit.Framework.Assert.IsTrue(propertyValue is IList);
                IList values = (IList)propertyValue;
                NUnit.Framework.Assert.AreEqual(this.imagesArray.Length, values.Count);
                BackgroundPosition[] positions = new BackgroundPosition[] { new BackgroundPosition(), new BackgroundPosition
                    ().SetPositionY(BackgroundPosition.PositionY.BOTTOM), new BackgroundPosition().SetPositionX(BackgroundPosition.PositionX
                    .RIGHT) };
                for (int i = 0; i < values.Count; i++) {
                    Object value = values[i];
                    NUnit.Framework.Assert.IsTrue(value is BackgroundImage);
                    BackgroundImage image = (BackgroundImage)value;
                    NUnit.Framework.Assert.AreEqual(positions[i], image.GetBackgroundPosition());
                }
            }

            private readonly String images;
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundClipOriginImageTest() {
            String image = "url(rock_texture.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new _BodyHtmlStylesContainer_612();
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, image);
            props.Put(CssConstants.BACKGROUND_CLIP, CssConstants.CONTENT_BOX);
            props.Put(CssConstants.BACKGROUND_ORIGIN, CssConstants.PADDING_BOX);
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_612 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_612() {
            }

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND_IMAGE, property);
                NUnit.Framework.Assert.IsTrue(propertyValue is IList);
                IList values = (IList)propertyValue;
                NUnit.Framework.Assert.AreEqual(1, values.Count);
                foreach (Object value in values) {
                    NUnit.Framework.Assert.IsTrue(value is BackgroundImage);
                    BackgroundImage image = (BackgroundImage)value;
                    NUnit.Framework.Assert.AreEqual(BackgroundBox.CONTENT_BOX, image.GetBackgroundClip());
                    NUnit.Framework.Assert.AreEqual(BackgroundBox.PADDING_BOX, image.GetBackgroundOrigin());
                }
            }
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundClipColorTest() {
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new _BodyHtmlStylesContainer_638();
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_COLOR, "blue");
            props.Put(CssConstants.BACKGROUND_CLIP, CssConstants.CONTENT_BOX);
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_638 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_638() {
            }

            public override void SetProperty(int property, Object propertyValue) {
                NUnit.Framework.Assert.AreEqual(Property.BACKGROUND, property);
                NUnit.Framework.Assert.IsTrue(propertyValue is Background);
                Background color = (Background)propertyValue;
                NUnit.Framework.Assert.AreEqual(BackgroundBox.CONTENT_BOX, color.GetBackgroundClip());
            }
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundClipOriginImagesTest() {
            String images = "url(rock_texture.jpg),url(rock_texture2.jpg)";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new _BodyHtmlStylesContainer_658(images);
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, images);
            props.Put(CssConstants.BACKGROUND_CLIP, CssConstants.CONTENT_BOX);
            props.Put(CssConstants.BACKGROUND_ORIGIN, CssConstants.BORDER_BOX);
            BackgroundApplierUtil.ApplyBackground(props, context, container);
        }

        private sealed class _BodyHtmlStylesContainer_658 : BodyHtmlStylesContainer {
            public _BodyHtmlStylesContainer_658(String images) {
                this.images = images;
                this.imagesArray = iText.Commons.Utils.StringUtil.Split(images, ",");
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
                    NUnit.Framework.Assert.AreEqual(BackgroundBox.CONTENT_BOX, image.GetBackgroundClip());
                    NUnit.Framework.Assert.AreEqual(BackgroundBox.BORDER_BOX, image.GetBackgroundOrigin());
                }
            }

            private readonly String images;
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundMultipleClipOriginImagesTest() {
            String images = "url(rock_texture.jpg),url(rock_texture2.jpg)";
            String clips = "content-box,padding-box";
            String origins = "border-box,content-box";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new BodyHtmlStylesContainer();
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, images);
            props.Put(CssConstants.BACKGROUND_CLIP, clips);
            props.Put(CssConstants.BACKGROUND_ORIGIN, origins);
            BackgroundApplierUtil.ApplyBackground(props, context, container);
            IList<BackgroundImage> backgroundImages = container.GetProperty<IList<BackgroundImage>>(Property.BACKGROUND_IMAGE
                );
            NUnit.Framework.Assert.IsNotNull(backgroundImages);
            NUnit.Framework.Assert.AreEqual(2, backgroundImages.Count);
            BackgroundImage imageObj1 = backgroundImages[0];
            NUnit.Framework.Assert.IsNotNull(imageObj1);
            NUnit.Framework.Assert.AreEqual(BackgroundBox.CONTENT_BOX, imageObj1.GetBackgroundClip());
            NUnit.Framework.Assert.AreEqual(BackgroundBox.BORDER_BOX, imageObj1.GetBackgroundOrigin());
            BackgroundImage imageObj2 = backgroundImages[1];
            NUnit.Framework.Assert.IsNotNull(imageObj2);
            NUnit.Framework.Assert.AreEqual(BackgroundBox.PADDING_BOX, imageObj2.GetBackgroundClip());
            NUnit.Framework.Assert.AreEqual(BackgroundBox.CONTENT_BOX, imageObj2.GetBackgroundOrigin());
        }

        [NUnit.Framework.Test]
        public virtual void BackgroundClipOriginImagesColorTest() {
            String images = "url(rock_texture.jpg),url(rock_texture2.jpg)";
            String clips = "content-box,padding-box";
            String origins = "border-box,content-box";
            ProcessorContext context = new ProcessorContext(new ConverterProperties().SetBaseUri(SOURCE_FOLDER));
            IPropertyContainer container = new BodyHtmlStylesContainer();
            IDictionary<String, String> props = new Dictionary<String, String>();
            props.Put(CssConstants.BACKGROUND_IMAGE, images);
            props.Put(CssConstants.BACKGROUND_COLOR, "blue");
            props.Put(CssConstants.BACKGROUND_CLIP, clips);
            props.Put(CssConstants.BACKGROUND_ORIGIN, origins);
            BackgroundApplierUtil.ApplyBackground(props, context, container);
            Background background = container.GetProperty<Background>(Property.BACKGROUND);
            NUnit.Framework.Assert.IsNotNull(background);
            NUnit.Framework.Assert.AreEqual(BackgroundBox.PADDING_BOX, background.GetBackgroundClip());
            IList<BackgroundImage> backgroundImages = container.GetProperty<IList<BackgroundImage>>(Property.BACKGROUND_IMAGE
                );
            NUnit.Framework.Assert.IsNotNull(backgroundImages);
            NUnit.Framework.Assert.AreEqual(2, backgroundImages.Count);
            BackgroundImage imageObj1 = backgroundImages[0];
            NUnit.Framework.Assert.IsNotNull(imageObj1);
            NUnit.Framework.Assert.AreEqual(BackgroundBox.CONTENT_BOX, imageObj1.GetBackgroundClip());
            NUnit.Framework.Assert.AreEqual(BackgroundBox.BORDER_BOX, imageObj1.GetBackgroundOrigin());
            BackgroundImage imageObj2 = backgroundImages[1];
            NUnit.Framework.Assert.IsNotNull(imageObj2);
            NUnit.Framework.Assert.AreEqual(BackgroundBox.PADDING_BOX, imageObj2.GetBackgroundClip());
            NUnit.Framework.Assert.AreEqual(BackgroundBox.CONTENT_BOX, imageObj2.GetBackgroundOrigin());
        }
    }
}
