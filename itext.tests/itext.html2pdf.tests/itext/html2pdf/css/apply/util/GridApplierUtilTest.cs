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
using System.Collections.Generic;
using iText.Commons.Utils;
using iText.Html2pdf;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css;
using iText.Html2pdf.Logs;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Properties.Grid;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf.Css.Apply.Util {
    [NUnit.Framework.Category("UnitTest")]
    public class GridApplierUtilTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void ApplyColumnStartTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_COLUMN_START, "2");
            IElement element = new Div();
            GridApplierUtil.ApplyGridItemProperties(cssProps, CreateStylesContainer(), element);
            int? columnStart = element.GetProperty<int?>(Property.GRID_COLUMN_START);
            NUnit.Framework.Assert.AreEqual(2, columnStart);
        }

        [NUnit.Framework.Test]
        public virtual void ApplyColumnEndTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_COLUMN_END, "4");
            IElement element = new Div();
            GridApplierUtil.ApplyGridItemProperties(cssProps, CreateStylesContainer(), element);
            int? columnStart = element.GetProperty<int?>(Property.GRID_COLUMN_END);
            NUnit.Framework.Assert.AreEqual(4, columnStart);
        }

        [NUnit.Framework.Test]
        public virtual void ApplyRowStartTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_ROW_START, "3");
            IElement element = new Div();
            GridApplierUtil.ApplyGridItemProperties(cssProps, CreateStylesContainer(), element);
            int? columnStart = element.GetProperty<int?>(Property.GRID_ROW_START);
            NUnit.Framework.Assert.AreEqual(3, columnStart);
        }

        [NUnit.Framework.Test]
        public virtual void ApplyRowStartSpanTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_ROW_START, "span  3");
            IElement element = new Div();
            GridApplierUtil.ApplyGridItemProperties(cssProps, CreateStylesContainer(), element);
            int? rowSpan = element.GetProperty<int?>(Property.GRID_ROW_SPAN);
            NUnit.Framework.Assert.AreEqual(3, rowSpan);
        }

        [NUnit.Framework.Test]
        public virtual void ApplyRowEndTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_ROW_END, "11");
            IElement element = new Div();
            GridApplierUtil.ApplyGridItemProperties(cssProps, CreateStylesContainer(), element);
            int? columnStart = element.GetProperty<int?>(Property.GRID_ROW_END);
            NUnit.Framework.Assert.AreEqual(11, columnStart);
        }

        [NUnit.Framework.Test]
        public virtual void ApplyInvalidColumnStartTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_COLUMN_START, CssConstants.AUTO);
            Div element = new Div();
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, CreateStylesContainer(), element);
            GridApplierUtil.ApplyGridContainerProperties(new Dictionary<String, String>(), grid, new ProcessorContext(
                new ConverterProperties()));
            int? columnStart = element.GetProperty<int?>(Property.GRID_COLUMN_START);
            NUnit.Framework.Assert.IsNull(columnStart);
        }

        [NUnit.Framework.Test]
        public virtual void ApplyGridAreaBasicTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AREA, "1 / 2 /  3  / 4");
            IElement element = new Div();
            GridApplierUtil.ApplyGridItemProperties(cssProps, CreateStylesContainer(), element);
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(2, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(3, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(4, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void ApplyGridAreaAutoTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AREA, "auto / auto / auto  / auto");
            IElement element = new Div();
            GridApplierUtil.ApplyGridItemProperties(cssProps, CreateStylesContainer(), element);
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void ApplyGridAreaOrderTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AREA, "1 / 2 /  3  / 4");
            cssProps.Put(CssConstants.GRID_COLUMN_START, "1");
            IElement element = new Div();
            GridApplierUtil.ApplyGridItemProperties(cssProps, CreateStylesContainer(), element);
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(3, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(4, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void ApplyGridAreaOrder2Test() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_COLUMN_START, "1");
            cssProps.Put(CssConstants.GRID_AREA, "1 / 2 /  3  / 4");
            IElement element = new Div();
            GridApplierUtil.ApplyGridItemProperties(cssProps, CreateStylesContainer(), element);
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(3, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(4, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void ApplyGridAreaOrder3Test() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AREA, "auto / 2 /  3  / 4");
            cssProps.Put(CssConstants.GRID_ROW_START, "1");
            IElement element = new Div();
            GridApplierUtil.ApplyGridItemProperties(cssProps, CreateStylesContainer(), element);
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(2, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(3, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(4, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void ApplyGridAreaOrder4Test() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_ROW_START, "1");
            cssProps.Put(CssConstants.GRID_AREA, "auto  / 2 /  3  / 4");
            IElement element = new Div();
            GridApplierUtil.ApplyGridItemProperties(cssProps, CreateStylesContainer(), element);
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(2, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(3, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(4, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void ApplyGridPropertiesToNotGrid() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AREA, "1 / 2 /  3  / 4");
            IElement element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.JRemove(CssConstants.DISPLAY);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void ApplyNoneGridTemplateAreasTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AREA, CommonCssConstants.NONE);
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_AREAS, CommonCssConstants.NONE);
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(new Dictionary<String, String>(), grid, new ProcessorContext(
                new ConverterProperties()));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void ApplyGridTemplateAreas1Test() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AREA, "somename1");
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_AREAS, "\"somename1 Somename1\" ' somename1     Somename1' ' somename1     Somename1'"
                );
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(parentStyles, grid, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(4, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(2, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void ApplyGridTemplateAreasOrder1Test() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_ROW_END, "3");
            cssProps.Put(CssConstants.GRID_AREA, "somename1");
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_AREAS, "\"somename1 Somename1\" ' somename1     Somename1' ' somename1     Somename1'"
                );
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(parentStyles, grid, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(4, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(2, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void ApplyGridTemplateAreasOrder2Test() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AREA, "somename1");
            cssProps.Put(CssConstants.GRID_ROW_END, "3");
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_AREAS, "\"somename1 Somename1\" ' somename1     Somename1' ' somename1     Somename1'"
                );
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(parentStyles, grid, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(4, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(2, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void ApplyGridTemplateAreasInvalidNameTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AREA, "1");
            IElement element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_AREAS, "'a b' '1 1'");
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.GRID_TEMPLATE_AREAS_IS_INVALID, Count = 2)]
        public virtual void ApplyInvalidGridTemplateAreas1Test() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AREA, "b");
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_AREAS, "'a b' 'b a'");
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(parentStyles, grid, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(2, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(2, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(3, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.GRID_TEMPLATE_AREAS_IS_INVALID, Count = 2)]
        public virtual void ApplyInvalidGridTemplateAreas2Test() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AREA, "a");
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_AREAS, "'a b a' 'a b a'");
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(parentStyles, grid, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(3, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(2, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void ApplyGridTemplateAreasWithDotsTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AREA, ".");
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_AREAS, "'. . a' '. . a'");
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(new Dictionary<String, String>(), grid, new ProcessorContext(
                new ConverterProperties()));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        // ------------------ Grid container tests ------------------
        [NUnit.Framework.Test]
        public virtual void ContainerAutoValuesTest() {
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AUTO_COLUMNS, "11px");
            cssProps.Put(CssConstants.GRID_AUTO_ROWS, "30%");
            IElement element = new Div();
            GridApplierUtil.ApplyGridContainerProperties(cssProps, element, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(8.25f, element.GetProperty<LengthValue>(Property.GRID_AUTO_COLUMNS).GetValue
                ());
            NUnit.Framework.Assert.AreEqual(30.0f, element.GetProperty<LengthValue>(Property.GRID_AUTO_ROWS).GetValue(
                ));
        }

        [NUnit.Framework.Test]
        public virtual void ContainerTemplateValuesTest() {
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.GRID_TEMPLATE_COLUMNS, "min-content 1.5fr auto 2fr 100px 20%");
            cssProps.Put(CssConstants.GRID_TEMPLATE_ROWS, "10px 20pt 3em 5rem");
            IElement element = new Div();
            GridApplierUtil.ApplyGridContainerProperties(cssProps, element, new ProcessorContext(new ConverterProperties
                ()));
            IList<TemplateValue> actualColValues = element.GetProperty<IList<TemplateValue>>(Property.GRID_TEMPLATE_COLUMNS
                );
            NUnit.Framework.Assert.AreEqual(6, actualColValues.Count);
            NUnit.Framework.Assert.AreEqual(actualColValues[0].GetType(), TemplateValue.ValueType.MIN_CONTENT);
            NUnit.Framework.Assert.AreEqual(actualColValues[1].GetType(), TemplateValue.ValueType.FLEX);
            NUnit.Framework.Assert.AreEqual(actualColValues[2].GetType(), TemplateValue.ValueType.AUTO);
            NUnit.Framework.Assert.AreEqual(actualColValues[3].GetType(), TemplateValue.ValueType.FLEX);
            NUnit.Framework.Assert.AreEqual(75.0f, ((PointValue)actualColValues[4]).GetValue());
            NUnit.Framework.Assert.AreEqual(20.0f, ((PercentValue)actualColValues[5]).GetValue());
            IList<TemplateValue> actualRowValues = element.GetProperty<IList<TemplateValue>>(Property.GRID_TEMPLATE_ROWS
                );
            NUnit.Framework.Assert.AreEqual(4, actualRowValues.Count);
            NUnit.Framework.Assert.AreEqual(7.5f, ((PointValue)actualRowValues[0]).GetValue());
            NUnit.Framework.Assert.AreEqual(20.0f, ((PointValue)actualRowValues[1]).GetValue());
            NUnit.Framework.Assert.AreEqual(0.0f, ((PointValue)actualRowValues[2]).GetValue());
            NUnit.Framework.Assert.AreEqual(60.0f, ((PointValue)actualRowValues[3]).GetValue());
        }

        [NUnit.Framework.Test]
        public virtual void ContainerComplexTemplateValuesTest() {
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.GRID_TEMPLATE_COLUMNS, "minmax(min-content, 1fr) fit-content(40%) fit-content(20px) repeat(2, fit-content(200px))"
                );
            cssProps.Put(CssConstants.GRID_TEMPLATE_ROWS, "repeat(3, 100px) repeat(auto-fit, minmax(100px, auto))");
            IElement element = new Div();
            GridApplierUtil.ApplyGridContainerProperties(cssProps, element, new ProcessorContext(new ConverterProperties
                ()));
            IList<TemplateValue> actualColValues = element.GetProperty<IList<TemplateValue>>(Property.GRID_TEMPLATE_COLUMNS
                );
            NUnit.Framework.Assert.AreEqual(4, actualColValues.Count);
            NUnit.Framework.Assert.AreEqual(TemplateValue.ValueType.MINMAX, actualColValues[0].GetType());
            NUnit.Framework.Assert.AreEqual(TemplateValue.ValueType.MIN_CONTENT, ((MinMaxValue)actualColValues[0]).GetMin
                ().GetType());
            NUnit.Framework.Assert.AreEqual(TemplateValue.ValueType.FLEX, ((MinMaxValue)actualColValues[0]).GetMax().GetType
                ());
            NUnit.Framework.Assert.AreEqual(TemplateValue.ValueType.FIT_CONTENT, actualColValues[1].GetType());
            NUnit.Framework.Assert.AreEqual(TemplateValue.ValueType.PERCENT, ((FitContentValue)actualColValues[1]).GetLength
                ().GetType());
            NUnit.Framework.Assert.AreEqual(TemplateValue.ValueType.FIT_CONTENT, actualColValues[2].GetType());
            NUnit.Framework.Assert.AreEqual(TemplateValue.ValueType.POINT, ((FitContentValue)actualColValues[2]).GetLength
                ().GetType());
            NUnit.Framework.Assert.AreEqual(TemplateValue.ValueType.FIXED_REPEAT, actualColValues[3].GetType());
            NUnit.Framework.Assert.AreEqual(TemplateValue.ValueType.FIT_CONTENT, ((FixedRepeatValue)actualColValues[3]
                ).GetValues()[0].GetType());
            IList<TemplateValue> actualRowValues = element.GetProperty<IList<TemplateValue>>(Property.GRID_TEMPLATE_ROWS
                );
            NUnit.Framework.Assert.AreEqual(2, actualRowValues.Count);
            NUnit.Framework.Assert.AreEqual(TemplateValue.ValueType.FIXED_REPEAT, actualRowValues[0].GetType());
            NUnit.Framework.Assert.AreEqual(TemplateValue.ValueType.POINT, ((FixedRepeatValue)actualRowValues[0]).GetValues
                ()[0].GetType());
            NUnit.Framework.Assert.AreEqual(TemplateValue.ValueType.AUTO_REPEAT, actualRowValues[1].GetType());
            NUnit.Framework.Assert.AreEqual(TemplateValue.ValueType.MINMAX, ((AutoRepeatValue)actualRowValues[1]).GetValues
                ()[0].GetType());
        }

        [NUnit.Framework.Test]
        public virtual void ContainerGapValuesTest() {
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.COLUMN_GAP, "11px");
            cssProps.Put(CssConstants.ROW_GAP, "30%");
            IElement element = new Div();
            GridApplierUtil.ApplyGridContainerProperties(cssProps, element, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(8.25f, element.GetProperty<float?>(Property.COLUMN_GAP));
            NUnit.Framework.Assert.AreEqual(30, element.GetProperty<float?>(Property.ROW_GAP));
        }

        [NUnit.Framework.Test]
        public virtual void ColumnFlowTest() {
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AUTO_FLOW, CommonCssConstants.COLUMN);
            IElement element = new Div();
            GridApplierUtil.ApplyGridContainerProperties(cssProps, element, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(GridFlow.COLUMN, element.GetProperty<GridFlow?>(Property.GRID_FLOW));
        }

        [NUnit.Framework.Test]
        public virtual void NullFlowTest() {
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            IElement element = new Div();
            GridApplierUtil.ApplyGridContainerProperties(cssProps, element, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(GridFlow.ROW, element.GetProperty<GridFlow?>(Property.GRID_FLOW));
        }

        [NUnit.Framework.Test]
        public virtual void DenseFlowTest() {
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AUTO_FLOW, CssConstants.DENSE);
            IElement element = new Div();
            GridApplierUtil.ApplyGridContainerProperties(cssProps, element, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(GridFlow.ROW_DENSE, element.GetProperty<GridFlow?>(Property.GRID_FLOW));
        }

        [NUnit.Framework.Test]
        public virtual void ColumnDenseFlowTest() {
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AUTO_FLOW, CommonCssConstants.COLUMN + " " + CssConstants.DENSE);
            IElement element = new Div();
            GridApplierUtil.ApplyGridContainerProperties(cssProps, element, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(GridFlow.COLUMN_DENSE, element.GetProperty<GridFlow?>(Property.GRID_FLOW));
        }

        [NUnit.Framework.Test]
        public virtual void InvalidFlowTest() {
            IDictionary<String, String> cssProps = new Dictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AUTO_FLOW, "some text");
            IElement element = new Div();
            GridApplierUtil.ApplyGridContainerProperties(cssProps, element, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(GridFlow.ROW, element.GetProperty<GridFlow?>(Property.GRID_FLOW));
        }

        [NUnit.Framework.Test]
        public virtual void CustomIndentTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_COLUMN_START, " a ");
            cssProps.Put(CssConstants.GRID_COLUMN_END, "c");
            cssProps.Put(CssConstants.GRID_ROW_START, " a ");
            cssProps.Put(CssConstants.GRID_ROW_END, "c");
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_COLUMNS, "[a] 10px [ b  c  d  ] 10px [e f]");
            parentStyles.Put(CssConstants.GRID_TEMPLATE_ROWS, "[f] 10px [ e  d  c  ] 10px [b a]");
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(parentStyles, grid, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(3, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(2, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(2, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void CustomIndentNthTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_COLUMN_START, "2 a");
            cssProps.Put(CssConstants.GRID_COLUMN_END, "3 a");
            cssProps.Put(CssConstants.GRID_ROW_START, "2 c");
            cssProps.Put(CssConstants.GRID_ROW_END, "d");
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_COLUMNS, "[a] 10px [b] 10px [a] 10px [a]");
            parentStyles.Put(CssConstants.GRID_TEMPLATE_ROWS, "[c] 10px [c] 10px [c]");
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(parentStyles, grid, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(2, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(3, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(4, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void CustomIndentNegativeTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_COLUMN_START, "-3 a");
            cssProps.Put(CssConstants.GRID_COLUMN_END, "-1 a");
            cssProps.Put(CssConstants.GRID_ROW_START, "-3 c");
            cssProps.Put(CssConstants.GRID_ROW_END, "-1 c");
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_COLUMNS, "[a] 10px [b] 10px [a] 10px [a]");
            parentStyles.Put(CssConstants.GRID_TEMPLATE_ROWS, "[c] 10px [c] 10px [c]");
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(parentStyles, grid, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(3, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(4, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.ADDING_GRID_LINES_TO_THE_LEFT_OR_TOP_IS_NOT_SUPPORTED)]
        public virtual void CustomIndentOutOfBoundsTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_COLUMN_START, "a");
            cssProps.Put(CssConstants.GRID_COLUMN_END, "5 a");
            cssProps.Put(CssConstants.GRID_ROW_START, "-5 c");
            cssProps.Put(CssConstants.GRID_ROW_END, "-1 c");
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_COLUMNS, "[a] 10px [b] 10px [a] 10px [a]");
            parentStyles.Put(CssConstants.GRID_TEMPLATE_ROWS, "[c] 10px [c] 10px [c]");
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(parentStyles, grid, new ProcessorContext(new ConverterProperties
                ()));
            // Null for row start as we don't support negative starts
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(3, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(6, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void CustomIndentOutOfBounds2Test() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_COLUMN_START, "a");
            cssProps.Put(CssConstants.GRID_COLUMN_END, "5 a");
            cssProps.Put(CssConstants.GRID_ROW_START, "c");
            cssProps.Put(CssConstants.GRID_ROW_END, "2 c");
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_COLUMNS, "[a] 10px 10px [a] 10px [b]");
            parentStyles.Put(CssConstants.GRID_TEMPLATE_ROWS, "10px 10px [c] 10px [a]");
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(parentStyles, grid, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(3, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(5, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(7, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void CustomIndentGridAreaTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AREA, "c / a 2 / c -2 / 5 a");
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_COLUMNS, "[a] 10px [b] 10px [a] 10px [a]");
            parentStyles.Put(CssConstants.GRID_TEMPLATE_ROWS, "[c] 10px [c] 10px [c]");
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(parentStyles, grid, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(3, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(2, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(6, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void CustomIndentSpan1Test() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_COLUMN_START, "a");
            cssProps.Put(CssConstants.GRID_COLUMN_END, "span 2 a");
            cssProps.Put(CssConstants.GRID_ROW_START, "span c");
            cssProps.Put(CssConstants.GRID_ROW_END, "4");
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_COLUMNS, "[a] 10px 10px [a] 10px [b]");
            parentStyles.Put(CssConstants.GRID_TEMPLATE_ROWS, "10px 10px [c] 10px [a]");
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(parentStyles, grid, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(3, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(4, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(5, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void CustomIndentSpan2Test() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_COLUMN_START, "2 a");
            cssProps.Put(CssConstants.GRID_COLUMN_END, "span 2 a");
            cssProps.Put(CssConstants.GRID_ROW_START, "span 2 c");
            cssProps.Put(CssConstants.GRID_ROW_END, "4");
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_COLUMNS, "[a] 10px 10px [a] 10px [b]");
            parentStyles.Put(CssConstants.GRID_TEMPLATE_ROWS, "10px 10px [c] 10px [a]");
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(parentStyles, grid, new ProcessorContext(new ConverterProperties
                ()));
            // Null for row start as we don't support negative starts
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(3, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(4, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(6, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void CustomIndentSpan3Test() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_COLUMN_START, " 2  a-a");
            cssProps.Put(CssConstants.GRID_COLUMN_END, "span  2  a-a");
            cssProps.Put(CssConstants.GRID_ROW_START, "span  2  c");
            cssProps.Put(CssConstants.GRID_ROW_END, "4");
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_COLUMNS, "[a-a] 10px 10px [a-a] 10px [b-a]");
            parentStyles.Put(CssConstants.GRID_TEMPLATE_ROWS, "10px 10px [c] 10px [a]");
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(parentStyles, grid, new ProcessorContext(new ConverterProperties
                ()));
            // Null for row start as we don't support negative starts
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(3, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(4, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(6, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void GridAreaLinenamesTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_COLUMN_START, "a-start");
            cssProps.Put(CssConstants.GRID_COLUMN_END, "span c-end 2");
            cssProps.Put(CssConstants.GRID_ROW_START, "span a-start");
            cssProps.Put(CssConstants.GRID_ROW_END, "c-end -1");
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_AREAS, "'a a a b b c c c' 'a a a b b c c c' 'a a a b b c c c'"
                );
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(parentStyles, grid, new ProcessorContext(new ConverterProperties
                ()));
            // Null for row start as we don't support negative starts
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(4, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(10, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void LineNamesInRepeatTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_COLUMN_START, "4 a");
            cssProps.Put(CssConstants.GRID_COLUMN_END, "b 4");
            cssProps.Put(CssConstants.GRID_ROW_START, "span start");
            cssProps.Put(CssConstants.GRID_ROW_END, "c 2");
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_COLUMNS, "[a] repeat(3, [a st] 10px 10px [a] 10px [b])");
            parentStyles.Put(CssConstants.GRID_TEMPLATE_ROWS, "[start] repeat(2, 10px 10px [c] 10px [a])");
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(parentStyles, grid, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(6, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(6, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(11, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void LineNamesInRepeat2Test() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_COLUMN_START, "3 a");
            cssProps.Put(CssConstants.GRID_COLUMN_END, "span nd 3");
            cssProps.Put(CssConstants.GRID_ROW_START, "span c");
            cssProps.Put(CssConstants.GRID_ROW_END, "a 8");
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_COLUMNS, "[a] 10px repeat( 2, 10px 10px [a] 10px [b]) repeat(3, [nd] auto)"
                );
            parentStyles.Put(CssConstants.GRID_TEMPLATE_ROWS, "[start] 10px repeat( 5, 10px 10px [c] 10px [a]) auto [a] auto [a]"
                );
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(parentStyles, grid, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(16, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(7, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(20, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(10, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        [LogMessage(Html2PdfLogMessageConstant.LINENAMES_ARE_NOT_SUPPORTED_WITHIN_AUTO_REPEAT, Count = 2)]
        public virtual void LineNamesInAutoRepeatTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_COLUMN_START, "a");
            cssProps.Put(CssConstants.GRID_COLUMN_END, "c");
            Div element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_COLUMNS, "repeat(auto-fill, [a] 10%) [b] repeat(auto-fit, 1fr) [c]"
                );
            GridContainer grid = new GridContainer();
            grid.Add(element);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            GridApplierUtil.ApplyGridContainerProperties(parentStyles, grid, new ProcessorContext(new ConverterProperties
                ()));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(3, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        private IElementNode CreateStylesContainer() {
            iText.StyledXmlParser.Jsoup.Nodes.Element element = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf("div"), "");
            iText.StyledXmlParser.Jsoup.Nodes.Element parentElement = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf("div"), "");
            parentElement.AppendChild(element);
            IElementNode node = new JsoupElementNode(element);
            IElementNode parentNode = new JsoupElementNode(parentElement);
            parentNode.AddChild(node);
            IDictionary<String, String> styles = new Dictionary<String, String>();
            styles.Put(CssConstants.DISPLAY, CssConstants.GRID);
            parentNode.SetStyles(styles);
            return node;
        }
    }
}
