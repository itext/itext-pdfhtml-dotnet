/*
This file is part of the iText (R) project.
Copyright (c) 1998-2022 iText Group NV
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
using System.Collections.Generic;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Logs;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Apply.Util {
    [NUnit.Framework.Category("UnitTest")]
    public class FlexApplierUtilTest : ExtendedITextTest {
        private const float EPS = 1e-6f;

        [NUnit.Framework.Test]
        public virtual void ApplyFlexGrowTest() {
            ProcessorContext context = new ProcessorContext(new ConverterProperties());
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.FLEX_GROW, "20.568");
            IElement element = new Div();
            FlexApplierUtil.ApplyFlexItemProperties(cssProps, context, element);
            float? flexGrow = element.GetProperty<float?>(Property.FLEX_GROW);
            NUnit.Framework.Assert.AreEqual(20.568f, (float)flexGrow, EPS);
        }

        [NUnit.Framework.Test]
        public virtual void ApplyFlexShrinkTest() {
            ProcessorContext context = new ProcessorContext(new ConverterProperties());
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.FLEX_SHRINK, "182.1932");
            IElement element = new Div();
            FlexApplierUtil.ApplyFlexItemProperties(cssProps, context, element);
            float? flexShrink = element.GetProperty<float?>(Property.FLEX_SHRINK);
            NUnit.Framework.Assert.AreEqual(182.1932f, (float)flexShrink, EPS);
        }

        [NUnit.Framework.Test]
        public virtual void ApplyFlexBasisNullTest() {
            ProcessorContext context = new ProcessorContext(new ConverterProperties());
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.FLEX_BASIS, null);
            IElement element = new Div();
            FlexApplierUtil.ApplyFlexItemProperties(cssProps, context, element);
            NUnit.Framework.Assert.IsNull(element.GetProperty<UnitValue>(Property.FLEX_BASIS));
        }

        [NUnit.Framework.Test]
        public virtual void ApplyFlexBasisNullWidthTest() {
            ProcessorContext context = new ProcessorContext(new ConverterProperties());
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.FLEX_BASIS, null);
            cssProps.Put(CssConstants.WIDTH, "20.45pt");
            cssProps.Put(CssConstants.FONT_SIZE, "0");
            IElement element = new Div();
            FlexApplierUtil.ApplyFlexItemProperties(cssProps, context, element);
            NUnit.Framework.Assert.AreEqual(UnitValue.CreatePointValue(20.45f), element.GetProperty<UnitValue>(Property
                .FLEX_BASIS));
        }

        [NUnit.Framework.Test]
        public virtual void ApplyFlexBasisAutoTest() {
            ProcessorContext context = new ProcessorContext(new ConverterProperties());
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.FLEX_BASIS, CssConstants.AUTO);
            IElement element = new Div();
            FlexApplierUtil.ApplyFlexItemProperties(cssProps, context, element);
            NUnit.Framework.Assert.IsNull(element.GetProperty<UnitValue>(Property.FLEX_BASIS));
        }

        [NUnit.Framework.Test]
        public virtual void ApplyFlexBasisAutoWidthTest() {
            ProcessorContext context = new ProcessorContext(new ConverterProperties());
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.FLEX_BASIS, CssConstants.AUTO);
            cssProps.Put(CssConstants.WIDTH, "20.45pt");
            cssProps.Put(CssConstants.FONT_SIZE, "0");
            IElement element = new Div();
            FlexApplierUtil.ApplyFlexItemProperties(cssProps, context, element);
            NUnit.Framework.Assert.AreEqual(UnitValue.CreatePointValue(20.45f), element.GetProperty<UnitValue>(Property
                .FLEX_BASIS));
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void ApplyFlexBasisContentWidthTest() {
            ProcessorContext context = new ProcessorContext(new ConverterProperties());
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.FLEX_BASIS, CssConstants.CONTENT);
            IElement element = new Div();
            FlexApplierUtil.ApplyFlexItemProperties(cssProps, context, element);
            NUnit.Framework.Assert.IsFalse(element.HasProperty(Property.FLEX_BASIS));
            NUnit.Framework.Assert.IsNull(element.GetProperty<UnitValue>(Property.FLEX_BASIS));
        }

        [NUnit.Framework.Test]
        public virtual void ApplyFlexBasisAbsoluteValueTest() {
            ProcessorContext context = new ProcessorContext(new ConverterProperties());
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.FLEX_BASIS, "20.45pt");
            cssProps.Put(CssConstants.FONT_SIZE, "0");
            IElement element = new Div();
            FlexApplierUtil.ApplyFlexItemProperties(cssProps, context, element);
            NUnit.Framework.Assert.AreEqual(UnitValue.CreatePointValue(20.45f), element.GetProperty<UnitValue>(Property
                .FLEX_BASIS));
            NUnit.Framework.Assert.AreEqual(UnitValue.CreatePointValue(20.45f), element.GetProperty<UnitValue>(Property
                .FLEX_BASIS));
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void ApplyAlignItemsTest() {
            String[] alignItemsStrings = new String[] { CssConstants.START, CssConstants.END, CssConstants.CENTER, CssConstants
                .FLEX_START, CssConstants.FLEX_END, CssConstants.SELF_START, CssConstants.SELF_END, CssConstants.BASELINE
                , CssConstants.STRETCH, CssConstants.NORMAL };
            AlignmentPropertyValue[] alignItemsValues = new AlignmentPropertyValue[] { AlignmentPropertyValue.START, AlignmentPropertyValue
                .END, AlignmentPropertyValue.CENTER, AlignmentPropertyValue.FLEX_START, AlignmentPropertyValue.FLEX_END
                , AlignmentPropertyValue.SELF_START, AlignmentPropertyValue.SELF_END, AlignmentPropertyValue.STRETCH, 
                AlignmentPropertyValue.STRETCH, AlignmentPropertyValue.NORMAL };
            for (int i = 0; i < alignItemsStrings.Length; ++i) {
                IDictionary<String, String> cssProps = new Dictionary<String, String>();
                cssProps.Put(CssConstants.ALIGN_ITEMS, alignItemsStrings[i]);
                IElement element = new Div();
                FlexApplierUtil.ApplyFlexContainerProperties(cssProps, element);
                NUnit.Framework.Assert.AreEqual(alignItemsValues[i], (AlignmentPropertyValue)element.GetProperty<AlignmentPropertyValue?
                    >(Property.ALIGN_ITEMS));
            }
        }

        [NUnit.Framework.Test]
        public virtual void ApplyJustifyContentTest() {
            String[] justifyContentStrings = new String[] { CssConstants.START, CssConstants.END, CssConstants.CENTER, 
                CssConstants.FLEX_START, CssConstants.FLEX_END, CssConstants.SELF_START, CssConstants.SELF_END, CssConstants
                .LEFT, CssConstants.RIGHT, CssConstants.NORMAL, CssConstants.STRETCH };
            JustifyContent[] justifyContentValues = new JustifyContent[] { JustifyContent.START, JustifyContent.END, JustifyContent
                .CENTER, JustifyContent.FLEX_START, JustifyContent.FLEX_END, JustifyContent.SELF_START, JustifyContent
                .SELF_END, JustifyContent.LEFT, JustifyContent.RIGHT, JustifyContent.NORMAL, JustifyContent.STRETCH };
            for (int i = 0; i < justifyContentStrings.Length; ++i) {
                IDictionary<String, String> cssProps = new Dictionary<String, String>();
                cssProps.Put(CssConstants.JUSTIFY_CONTENT, justifyContentStrings[i]);
                IElement element = new Div();
                FlexApplierUtil.ApplyFlexContainerProperties(cssProps, element);
                NUnit.Framework.Assert.AreEqual(justifyContentValues[i], (JustifyContent)element.GetProperty<JustifyContent?
                    >(Property.JUSTIFY_CONTENT));
            }
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void ApplyAlignItemsUnsupportedValuesTest() {
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CommonCssConstants.ALIGN_ITEMS, CssConstants.SAFE + " " + CommonCssConstants.FLEX_END);
            IElement element = new Div();
            FlexApplierUtil.ApplyFlexContainerProperties(cssProps, element);
            NUnit.Framework.Assert.AreEqual(AlignmentPropertyValue.STRETCH, (AlignmentPropertyValue)element.GetProperty
                <AlignmentPropertyValue?>(Property.ALIGN_ITEMS));
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET)]
        public virtual void ApplyJustifyContentUnsupportedValuesTest() {
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.JUSTIFY_CONTENT, CommonCssConstants.SPACE_BETWEEN);
            IElement element = new Div();
            FlexApplierUtil.ApplyFlexContainerProperties(cssProps, element);
            NUnit.Framework.Assert.AreEqual(JustifyContent.FLEX_START, (JustifyContent)element.GetProperty<JustifyContent?
                >(Property.JUSTIFY_CONTENT));
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 5)]
        public virtual void ApplyFlexContainerUnsupportedPropertiesUnsupportedValuesTest() {
            String[] unsupportedProperties = new String[] { CssConstants.FLEX_WRAP, CssConstants.FLEX_DIRECTION, CssConstants
                .ROW_GAP, CssConstants.COLUMN_GAP, CssConstants.ALIGN_CONTENT };
            String[] unsupportedValues = new String[] { CssConstants.WRAP_REVERSE, CssConstants.COLUMN, "20px", "10em"
                , CssConstants.SPACE_AROUND };
            for (int i = 0; i < unsupportedValues.Length; ++i) {
                IDictionary<String, String> cssProps = new Dictionary<String, String>();
                cssProps.Put(unsupportedProperties[i], unsupportedValues[i]);
                IElement element = new Div();
                FlexApplierUtil.ApplyFlexContainerProperties(cssProps, element);
            }
            // This test checks that there are log messages so assertions are not required
            NUnit.Framework.Assert.IsTrue(true);
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.FLEX_PROPERTY_IS_NOT_SUPPORTED_YET, Count = 2)]
        public virtual void ApplyFlexItemUnsupportedPropertiesUnsupportedValuesTest() {
            ProcessorContext context = new ProcessorContext(new ConverterProperties());
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.ORDER, "1");
            cssProps.Put(CssConstants.ALIGN_SELF, CssConstants.STRETCH);
            IElement element = new Div();
            FlexApplierUtil.ApplyFlexItemProperties(cssProps, context, element);
            // This test checks that there are log messages so assertions are not required
            NUnit.Framework.Assert.IsTrue(true);
        }

        [NUnit.Framework.Test]
        public virtual void ApplyFlexContainerUnsupportedPropertiesSupportedValuesTest() {
            String[] unsupportedProperties = new String[] { CssConstants.FLEX_WRAP, CssConstants.FLEX_DIRECTION, CssConstants
                .ALIGN_CONTENT };
            String[] supportedValues = new String[] { CssConstants.NOWRAP, CssConstants.ROW, CssConstants.STRETCH };
            for (int i = 0; i < supportedValues.Length; ++i) {
                IDictionary<String, String> cssProps = new Dictionary<String, String>();
                cssProps.Put(unsupportedProperties[i], supportedValues[i]);
                IElement element = new Div();
                FlexApplierUtil.ApplyFlexContainerProperties(cssProps, element);
            }
            // This test checks that there are no log messages so assertions are not required
            NUnit.Framework.Assert.IsTrue(true);
        }

        [NUnit.Framework.Test]
        public virtual void ApplyFlexItemUnsupportedPropertiesSupportedValuesTest() {
            ProcessorContext context = new ProcessorContext(new ConverterProperties());
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.ALIGN_SELF, CssConstants.AUTO);
            IElement element = new Div();
            FlexApplierUtil.ApplyFlexItemProperties(cssProps, context, element);
            // This test checks that there are no log messages so assertions are not required
            NUnit.Framework.Assert.IsTrue(true);
        }
    }
}
