/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
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
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Test;

namespace iText.Html2pdf.Css.Apply.Util {
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
        }
    }
}
