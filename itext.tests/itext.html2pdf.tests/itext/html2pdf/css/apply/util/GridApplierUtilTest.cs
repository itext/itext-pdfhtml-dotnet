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
            IElement element = new Div();
            GridApplierUtil.ApplyGridItemProperties(cssProps, CreateStylesContainer(), element);
            int? columnStart = element.GetProperty<int?>(Property.GRID_COLUMN_START);
            NUnit.Framework.Assert.IsNull(columnStart);
        }

        [NUnit.Framework.Test]
        public virtual void ApplyGridContainerPropertiesTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_TEMPLATE_COLUMNS, "100px 100px");
            cssProps.Put(CssConstants.GRID_TEMPLATE_ROWS, "100pt 100pt");
            IElement element = new Div();
            GridApplierUtil.ApplyGridContainerProperties(cssProps, new ProcessorContext(new ConverterProperties()), element
                );
            IList<UnitValue> expectedResult = new List<UnitValue>();
            expectedResult.Add(UnitValue.CreatePointValue(75));
            expectedResult.Add(UnitValue.CreatePointValue(75));
            NUnit.Framework.Assert.AreEqual(expectedResult, element.GetProperty<IList<UnitValue>>(Property.GRID_TEMPLATE_COLUMNS
                ));
            expectedResult.Clear();
            expectedResult.Add(UnitValue.CreatePointValue(100));
            expectedResult.Add(UnitValue.CreatePointValue(100));
            NUnit.Framework.Assert.AreEqual(expectedResult, element.GetProperty<IList<UnitValue>>(Property.GRID_TEMPLATE_ROWS
                ));
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
            NUnit.Framework.Assert.AreEqual(2, element.GetProperty<int?>(Property.GRID_COLUMN_START));
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
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_ROW_START));
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
            IElement element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_AREAS, CommonCssConstants.NONE);
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void ApplyGridTemplateAreas1Test() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AREA, "somename1");
            IElement element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_AREAS, "\"somename1 Somename1\" ' somename1     Somename1' ' somename1     Somename1'"
                );
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
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
            IElement element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_AREAS, "\"somename1 Somename1\" ' somename1     Somename1' ' somename1     Somename1'"
                );
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
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
            IElement element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_AREAS, "\"somename1 Somename1\" ' somename1     Somename1' ' somename1     Somename1'"
                );
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(3, element.GetProperty<int?>(Property.GRID_ROW_END));
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
            IElement element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_AREAS, "'a b' 'b a'");
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
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
            IElement element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_AREAS, "'a b a' 'a b a'");
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.AreEqual(1, element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.AreEqual(3, element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.AreEqual(2, element.GetProperty<int?>(Property.GRID_COLUMN_END));
        }

        [NUnit.Framework.Test]
        public virtual void ApplyGridTemplateAreasWithDotsTest() {
            IDictionary<String, String> cssProps = new LinkedDictionary<String, String>();
            cssProps.Put(CssConstants.GRID_AREA, ".");
            IElement element = new Div();
            IElementNode stylesContainer = CreateStylesContainer();
            IDictionary<String, String> parentStyles = ((JsoupElementNode)stylesContainer.ParentNode()).GetStyles();
            parentStyles.Put(CssConstants.GRID_TEMPLATE_AREAS, "'. . a' '. . a'");
            GridApplierUtil.ApplyGridItemProperties(cssProps, stylesContainer, element);
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_ROW_START));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_COLUMN_START));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_ROW_END));
            NUnit.Framework.Assert.IsNull(element.GetProperty<int?>(Property.GRID_COLUMN_END));
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
