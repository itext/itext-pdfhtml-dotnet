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
using System.IO;
using iText.Html2pdf;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Attach.Impl.Layout.Form.Element;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Css {
    public class DisplayFlexTest : ExtendedHtmlConversionITextTest {
        private const float EPS = 1e-6f;

        private static readonly String SOURCE_FOLDER = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/DisplayFlexTest/";

        private static readonly String DESTINATION_FOLDER = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/css/DisplayFlexTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void DisplayFlexCommonTest() {
            String name = "displayFlexCommon";
            IList<IElement> elements = ConvertToElements(name);
            IElement flexContainer = elements[0];
            NUnit.Framework.Assert.IsTrue(flexContainer is FlexContainer);
            IList<IElement> flexContainerChildren = ((FlexContainer)flexContainer).GetChildren();
            NUnit.Framework.Assert.AreEqual(11, flexContainerChildren.Count);
            IElement element0 = flexContainerChildren[0];
            AssertDiv(element0, "block");
            IElement element1 = flexContainerChildren[1];
            AssertDiv(element1, "div with display inline");
            IElement element2 = flexContainerChildren[2];
            AssertDiv(element2, "float");
            IElement element3 = flexContainerChildren[3];
            AssertDiv(element3, "anonymous item");
            IElement element4 = flexContainerChildren[4];
            AssertDiv(element4, "span");
            IElement element5 = flexContainerChildren[5];
            NUnit.Framework.Assert.IsTrue(element5 is Image);
            IElement element6 = flexContainerChildren[6];
            NUnit.Framework.Assert.IsTrue(element6 is Div);
            NUnit.Framework.Assert.AreEqual(1, ((Div)element6).GetChildren().Count);
            NUnit.Framework.Assert.IsTrue(((Div)element6).GetChildren()[0] is Paragraph);
            NUnit.Framework.Assert.AreEqual(3, ((Paragraph)((Div)element6).GetChildren()[0]).GetChildren().Count);
            NUnit.Framework.Assert.IsTrue(((Paragraph)((Div)element6).GetChildren()[0]).GetChildren()[0] is Text);
            NUnit.Framework.Assert.IsTrue(((Paragraph)((Div)element6).GetChildren()[0]).GetChildren()[1] is Text);
            NUnit.Framework.Assert.IsTrue(((Paragraph)((Div)element6).GetChildren()[0]).GetChildren()[2] is Text);
            NUnit.Framework.Assert.AreEqual("text with", ((Text)((Paragraph)((Div)element6).GetChildren()[0]).GetChildren
                ()[0]).GetText());
            NUnit.Framework.Assert.AreEqual("\n", ((Text)((Paragraph)((Div)element6).GetChildren()[0]).GetChildren()[1
                ]).GetText());
            NUnit.Framework.Assert.AreEqual("br tag", ((Text)((Paragraph)((Div)element6).GetChildren()[0]).GetChildren
                ()[2]).GetText());
            IElement element7 = flexContainerChildren[7];
            NUnit.Framework.Assert.IsTrue(element7 is Image);
            IElement element8 = flexContainerChildren[8];
            AssertDiv(element8, "div with page break");
            IElement element9 = flexContainerChildren[9];
            NUnit.Framework.Assert.IsTrue(element9 is HtmlPageBreak);
            IElement element10 = flexContainerChildren[10];
            NUnit.Framework.Assert.IsTrue(element10 is TextArea);
        }

        [NUnit.Framework.Test]
        public virtual void NestedDivTest() {
            String name = "nestedDiv";
            String sourceHtml = SOURCE_FOLDER + name + ".html";
            ConverterProperties converterProperties = new ConverterProperties().SetBaseUri(SOURCE_FOLDER);
            IList<IElement> elements;
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                elements = HtmlConverter.ConvertToElements(fileInputStream, converterProperties);
            }
            IElement flexContainer = elements[0];
            NUnit.Framework.Assert.IsTrue(flexContainer is FlexContainer);
            NUnit.Framework.Assert.AreEqual(1, ((FlexContainer)flexContainer).GetChildren().Count);
            IElement element = ((FlexContainer)flexContainer).GetChildren()[0];
            NUnit.Framework.Assert.IsTrue(element is Div);
            NUnit.Framework.Assert.AreEqual(3, ((Div)element).GetChildren().Count);
            NUnit.Framework.Assert.IsTrue(((Div)element).GetChildren()[0] is Paragraph);
            NUnit.Framework.Assert.IsTrue(((Div)element).GetChildren()[1] is Div);
            NUnit.Framework.Assert.IsTrue(((Div)element).GetChildren()[2] is Paragraph);
        }

        [NUnit.Framework.Test]
        public virtual void FlexItemWhiteSpacePreTest() {
            String name = "flexItemWhiteSpacePre";
            String sourceHtml = SOURCE_FOLDER + name + ".html";
            ConverterProperties converterProperties = new ConverterProperties().SetBaseUri(SOURCE_FOLDER);
            IList<IElement> elements;
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                elements = HtmlConverter.ConvertToElements(fileInputStream, converterProperties);
            }
            IElement flexContainer = elements[0];
            NUnit.Framework.Assert.IsTrue(flexContainer is FlexContainer);
            NUnit.Framework.Assert.AreEqual(1, ((FlexContainer)flexContainer).GetChildren().Count);
            IElement element = ((FlexContainer)flexContainer).GetChildren()[0];
            AssertDiv(element, "\u200Dthe best   world");
        }

        [NUnit.Framework.Test]
        public virtual void AnonymousBlockInTheEndTest() {
            String name = "anonymousBlockInTheEnd";
            String sourceHtml = SOURCE_FOLDER + name + ".html";
            ConverterProperties converterProperties = new ConverterProperties().SetBaseUri(SOURCE_FOLDER);
            IList<IElement> elements;
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                elements = HtmlConverter.ConvertToElements(fileInputStream, converterProperties);
            }
            IElement flexContainer = elements[0];
            NUnit.Framework.Assert.IsTrue(flexContainer is FlexContainer);
            NUnit.Framework.Assert.AreEqual(2, ((FlexContainer)flexContainer).GetChildren().Count);
            NUnit.Framework.Assert.IsTrue(((FlexContainer)flexContainer).GetChildren()[0] is Div);
            IElement element = ((FlexContainer)flexContainer).GetChildren()[1];
            AssertDiv(element, "anonymous block");
        }

        [NUnit.Framework.Test]
        public virtual void BrTagTest() {
            String name = "brTag";
            String sourceHtml = SOURCE_FOLDER + name + ".html";
            ConverterProperties converterProperties = new ConverterProperties().SetBaseUri(SOURCE_FOLDER);
            IList<IElement> elements;
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                elements = HtmlConverter.ConvertToElements(fileInputStream, converterProperties);
            }
            IElement flexContainer = elements[0];
            NUnit.Framework.Assert.IsTrue(flexContainer is FlexContainer);
            NUnit.Framework.Assert.AreEqual(1, ((FlexContainer)flexContainer).GetChildren().Count);
            IElement element = ((FlexContainer)flexContainer).GetChildren()[0];
            AssertDiv(element, "hello");
        }

        [NUnit.Framework.Test]
        public virtual void FlexWrapTest() {
            //TODO DEVSIX-5086 change this test when working on the ticket
            String name = "flexWrap";
            String sourceHtml = SOURCE_FOLDER + name + ".html";
            ConverterProperties converterProperties = new ConverterProperties().SetBaseUri(SOURCE_FOLDER);
            IList<IElement> elements;
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                elements = HtmlConverter.ConvertToElements(fileInputStream, converterProperties);
            }
            IElement flexContainer = elements[0];
            NUnit.Framework.Assert.IsTrue(flexContainer is FlexContainer);
            NUnit.Framework.Assert.IsFalse(flexContainer.HasProperty(Property.FLEX_WRAP));
        }

        [NUnit.Framework.Test]
        public virtual void FloatAtFlexContainerTest() {
            //TODO DEVSIX-5087 remove this test when working on the ticket
            String name = "floatAtFlexContainer";
            String sourceHtml = SOURCE_FOLDER + name + ".html";
            ConverterProperties converterProperties = new ConverterProperties().SetBaseUri(SOURCE_FOLDER);
            IList<IElement> elements;
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                elements = HtmlConverter.ConvertToElements(fileInputStream, converterProperties);
            }
            IElement flexContainer = elements[0];
            NUnit.Framework.Assert.IsTrue(flexContainer is FlexContainer);
            NUnit.Framework.Assert.IsFalse(flexContainer.HasProperty(Property.FLOAT));
            NUnit.Framework.Assert.IsFalse(flexContainer.HasProperty(Property.CLEAR));
        }

        [NUnit.Framework.Test]
        public virtual void OverflowAtFlexContainerTest() {
            //TODO DEVSIX-5087 remove this test when working on the ticket
            String name = "overflowAtFlexContainer";
            String sourceHtml = SOURCE_FOLDER + name + ".html";
            ConverterProperties converterProperties = new ConverterProperties().SetBaseUri(SOURCE_FOLDER);
            IList<IElement> elements;
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                elements = HtmlConverter.ConvertToElements(fileInputStream, converterProperties);
            }
            IElement flexContainer = elements[0];
            NUnit.Framework.Assert.IsTrue(flexContainer is FlexContainer);
            NUnit.Framework.Assert.IsFalse(flexContainer.HasProperty(Property.OVERFLOW_X));
            NUnit.Framework.Assert.IsFalse(flexContainer.HasProperty(Property.OVERFLOW_Y));
        }

        [NUnit.Framework.Test]
        public virtual void CollapsingMarginsAtFlexContainerTest() {
            String name = "collapsingMarginsAtFlexContainer";
            String sourceHtml = SOURCE_FOLDER + name + ".html";
            ConverterProperties converterProperties = new ConverterProperties().SetBaseUri(SOURCE_FOLDER);
            IList<IElement> elements;
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                elements = HtmlConverter.ConvertToElements(fileInputStream, converterProperties);
            }
            IElement flexContainer = elements[0];
            NUnit.Framework.Assert.IsTrue(flexContainer is FlexContainer);
            NUnit.Framework.Assert.IsTrue(flexContainer.HasProperty(Property.COLLAPSING_MARGINS));
        }

        [NUnit.Framework.Test]
        public virtual void OverflowAtFlexItemTest() {
            String name = "overflowAtFlexItem";
            String sourceHtml = SOURCE_FOLDER + name + ".html";
            ConverterProperties converterProperties = new ConverterProperties().SetBaseUri(SOURCE_FOLDER);
            IList<IElement> elements;
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                elements = HtmlConverter.ConvertToElements(fileInputStream, converterProperties);
            }
            IElement flexContainer = elements[0];
            NUnit.Framework.Assert.IsTrue(flexContainer is FlexContainer);
            NUnit.Framework.Assert.AreEqual(1, ((FlexContainer)flexContainer).GetChildren().Count);
            NUnit.Framework.Assert.IsTrue(((FlexContainer)flexContainer).GetChildren()[0].HasProperty(Property.OVERFLOW_X
                ));
            NUnit.Framework.Assert.IsTrue(((FlexContainer)flexContainer).GetChildren()[0].HasProperty(Property.OVERFLOW_Y
                ));
        }

        [NUnit.Framework.Test]
        public virtual void DisplayFlexSpanContainerTest() {
            String name = "displayFlexSpanContainer";
            String sourceHtml = SOURCE_FOLDER + name + ".html";
            ConverterProperties converterProperties = new ConverterProperties().SetBaseUri(SOURCE_FOLDER);
            IList<IElement> elements;
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                elements = HtmlConverter.ConvertToElements(fileInputStream, converterProperties);
            }
            IElement flexContainer = elements[0];
            NUnit.Framework.Assert.IsTrue(flexContainer is FlexContainer);
        }

        [NUnit.Framework.Test]
        public virtual void TempDisablePropertiesTest() {
            //TODO DEVSIX-5087 remove this test when working on the ticket
            IList<IElement> elements = ConvertToElements("tempDisableProperties");
            NUnit.Framework.Assert.AreEqual(1, elements.Count);
            NUnit.Framework.Assert.IsTrue(elements[0] is FlexContainer);
            NUnit.Framework.Assert.IsFalse(elements[0].HasProperty(Property.OVERFLOW_X));
            NUnit.Framework.Assert.IsFalse(elements[0].HasProperty(Property.OVERFLOW_Y));
            NUnit.Framework.Assert.IsFalse(elements[0].HasProperty(Property.FLOAT));
            NUnit.Framework.Assert.IsFalse(elements[0].HasProperty(Property.CLEAR));
            NUnit.Framework.Assert.IsTrue(elements[0].HasProperty(Property.COLLAPSING_MARGINS));
            NUnit.Framework.Assert.IsNull(elements[0].GetProperty<Object>(Property.COLLAPSING_MARGINS));
        }

        [NUnit.Framework.Test]
        public virtual void DisableFlexItemPropertiesTest() {
            IList<IElement> elements = ConvertToElements("disableFlexItemProperties");
            IElement flexItem = ((FlexContainer)elements[0]).GetChildren()[0];
            NUnit.Framework.Assert.IsFalse(flexItem.HasProperty(Property.FLOAT));
            NUnit.Framework.Assert.IsFalse(flexItem.HasProperty(Property.CLEAR));
            NUnit.Framework.Assert.IsFalse(flexItem.HasProperty(Property.VERTICAL_ALIGNMENT));
        }

        [NUnit.Framework.Test]
        public virtual void FlexItemPropertiesTest() {
            String name = "flexItemProperties";
            String sourceHtml = SOURCE_FOLDER + name + ".html";
            ConverterProperties converterProperties = new ConverterProperties().SetBaseUri(SOURCE_FOLDER);
            IList<IElement> elements;
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                elements = HtmlConverter.ConvertToElements(fileInputStream, converterProperties);
            }
            IElement flexContainer = elements[0];
            NUnit.Framework.Assert.IsTrue(flexContainer is FlexContainer);
            NUnit.Framework.Assert.AreEqual(1, ((FlexContainer)flexContainer).GetChildren().Count);
            IElement flexItem = ((FlexContainer)flexContainer).GetChildren()[0];
            float? flexGrow = flexItem.GetProperty<float?>(Property.FLEX_GROW);
            float? flexShrink = flexItem.GetProperty<float?>(Property.FLEX_SHRINK);
            NUnit.Framework.Assert.AreEqual(2f, (float)flexGrow, EPS);
            NUnit.Framework.Assert.AreEqual(3f, (float)flexShrink, EPS);
            NUnit.Framework.Assert.AreEqual(UnitValue.CreatePointValue(200.338f), flexItem.GetProperty<UnitValue>(Property
                .FLEX_BASIS));
        }

        [NUnit.Framework.Test]
        public virtual void FlexGrowTest() {
            // TODO DEVSIX-5137	flex: support margin collapse
            String name = "flexGrow";
            ConvertToPdfAndCompare(name, SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexShrinkTest() {
            String name = "flexShrink";
            ConvertToPdfAndCompare(name, SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexBasisAuto() {
            ConvertToPdfAndCompare("flexBasisAuto", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FloatAtFlexItemTest() {
            ConvertToPdfAndCompare("floatAtFlexItem", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void ClearAtFlexItemTest() {
            ConvertToPdfAndCompare("clearAtFlexItem", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FloatAtFlexItemNestedTest() {
            ConvertToPdfAndCompare("floatAtFlexItemNested", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexContainerHeightTest() {
            // TODO DEVSIX-5135 flex item with nested floating element processed incorrectly
            ConvertToPdfAndCompare("flexContainerHeight", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void NestedFlexContainerTest() {
            ConvertToPdfAndCompare("nestedFlexContainer", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void CheckboxTest() {
            // TODO DEVSIX-5137	flex: support margin collapse
            ConvertToPdfAndCompare("checkbox", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexItemHeightTest() {
            // TODO DEVSIX-5040 change cmp after finishing this ticket
            ConvertToPdfAndCompare("flexItemHeight", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexItemContentTest() {
            //TODO DEVSIX-5149 Fix indent in element nested to the flex item
            ConvertToPdfAndCompare("flexItemContent", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexItemMinWidthTest() {
            //TODO DEVSIX-5150 Fix processing of min-width at flex item element
            ConvertToPdfAndCompare("flexItemMinWidth", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexItemEmptyTest() {
            //TODO DEVSIX-5155 Fix processing of empty flex item
            ConvertToPdfAndCompare("flexItemEmpty", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        [NUnit.Framework.Test]
        public virtual void FlexItemEmptyFlexBasisTest() {
            //TODO DEVSIX-5155 Fix processing of empty flex item
            ConvertToPdfAndCompare("flexItemEmptyFlexBasis", SOURCE_FOLDER, DESTINATION_FOLDER);
        }

        private static void AssertDiv(IElement element, String text) {
            NUnit.Framework.Assert.IsTrue(element is Div);
            NUnit.Framework.Assert.AreEqual(1, ((Div)element).GetChildren().Count);
            NUnit.Framework.Assert.IsTrue(((Div)element).GetChildren()[0] is Paragraph);
            NUnit.Framework.Assert.AreEqual(1, ((Paragraph)((Div)element).GetChildren()[0]).GetChildren().Count);
            NUnit.Framework.Assert.IsTrue(((Paragraph)((Div)element).GetChildren()[0]).GetChildren()[0] is Text);
            NUnit.Framework.Assert.AreEqual(text, ((Text)((Paragraph)((Div)element).GetChildren()[0]).GetChildren()[0]
                ).GetText());
        }

        private static IList<IElement> ConvertToElements(String name) {
            String sourceHtml = SOURCE_FOLDER + name + ".html";
            ConverterProperties converterProperties = new ConverterProperties().SetBaseUri(SOURCE_FOLDER);
            IList<IElement> elements;
            using (FileStream fileInputStream = new FileStream(sourceHtml, FileMode.Open, FileAccess.Read)) {
                elements = HtmlConverter.ConvertToElements(fileInputStream, converterProperties);
            }
            return elements;
        }
    }
}
