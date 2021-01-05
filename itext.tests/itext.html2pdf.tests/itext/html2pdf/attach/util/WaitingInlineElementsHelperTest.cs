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
using System.Text;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout.Element;
using iText.Test;

namespace iText.Html2pdf.Attach.Util {
    public class WaitingInlineElementsHelperTest : ExtendedITextTest {
        private readonly String capitalizeStyle = "capitalize";

        private WaitingInlineElementsHelper inlineHelper;

        [NUnit.Framework.Test]
        public virtual void CapitalizeLeafTest() {
            inlineHelper = new WaitingInlineElementsHelper(null, capitalizeStyle);
            inlineHelper.Add("one");
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("One", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeLeafWithTruePropertyTest() {
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("one", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("One", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeWithEmptyTextLeafTest() {
            inlineHelper = new WaitingInlineElementsHelper(null, capitalizeStyle);
            inlineHelper.Add(new Text("one"));
            inlineHelper.Add("");
            inlineHelper.Add(new Text("two"));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("Onetwo", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeTwoLeavesWithSpaceBeforeSecondTest() {
            inlineHelper = new WaitingInlineElementsHelper(null, capitalizeStyle);
            inlineHelper.Add(new Text("one"));
            inlineHelper.Add(new Text(" two"));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("One Two", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void OneLeafWithFalsePropertyAndCapitalizeStyleTest() {
            inlineHelper = new WaitingInlineElementsHelper(null, capitalizeStyle);
            inlineHelper.Add(CreateText("one", false));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("one", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeWithTabBetweenLeavesTest() {
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("one", true));
            inlineHelper.Add(new Tab());
            inlineHelper.Add(CreateText("two", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("OneTwo", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeWithLineSeparatorBetweenLeavesTest() {
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("one", true));
            inlineHelper.Add(new LineSeparator(new SolidLine()));
            inlineHelper.Add(CreateText("two", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("OneTwo", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeWithDivBetweenLeavesTest() {
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("one", true));
            inlineHelper.Add(new Div());
            inlineHelper.Add(CreateText("two", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("OneTwo", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeTest01() {
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("one", false));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("one", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeTest02() {
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("one", true));
            inlineHelper.Add("\n");
            inlineHelper.Add(CreateText("two", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("One Two", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeTest03() {
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("one ", true));
            inlineHelper.Add(new Text(" two"));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("One two", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeTest04() {
            inlineHelper = new WaitingInlineElementsHelper(null, capitalizeStyle);
            inlineHelper.Add(new Text("one "));
            inlineHelper.Add(CreateText(" two ", false));
            inlineHelper.Add(new Text(" three"));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("One two Three", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeTest05() {
            inlineHelper = new WaitingInlineElementsHelper(null, capitalizeStyle);
            inlineHelper.Add(new Text("one two"));
            inlineHelper.Add(new Text("three four"));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("One Twothree Four", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeTest06() {
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(new Text("one"));
            inlineHelper.Add(CreateText("two three", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("onetwo Three", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeTest07() {
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("one two", true));
            inlineHelper.Add(CreateText("three", false));
            inlineHelper.Add(CreateText("four five", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("One Twothreefour Five", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeTest08() {
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("one ", true));
            inlineHelper.Add(CreateText("two", false));
            inlineHelper.Add(CreateText("three", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("One twothree", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeTest09() {
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("one ", false));
            inlineHelper.Add(CreateText("two", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("one Two", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeAfterUnderScoreTest() {
            //TODO: replace assertNotEquals with assertEquals DEVSIX-4414
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("( one_", true));
            inlineHelper.Add(CreateText("two) ", true));
            inlineHelper.Add(CreateText("( _one", true));
            inlineHelper.Add(CreateText("_two)", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreNotEqual("( One_two) ( _one_two)", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeAfterDigitsTest() {
            //TODO: replace assertNotEquals with assertEquals DEVSIX-4414
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("( one2", true));
            inlineHelper.Add(CreateText("two) ", true));
            inlineHelper.Add(CreateText("( one ", true));
            inlineHelper.Add(CreateText("2two) ", true));
            inlineHelper.Add(CreateText("( one-", true));
            inlineHelper.Add(CreateText("2two) ", true));
            inlineHelper.Add(CreateText("one_", true));
            inlineHelper.Add(CreateText("2two", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreNotEqual("( One2two) ( One 2two) ( One-2two) ( One_2two)", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeAfterColonTest() {
            //TODO: replace assertNotEquals with assertEquals DEVSIX-4414
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("one:", true));
            inlineHelper.Add(CreateText("two", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreNotEqual("One:two", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeTest10() {
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("(one/", true));
            inlineHelper.Add(CreateText("two) ", true));
            inlineHelper.Add(CreateText("(one-", true));
            inlineHelper.Add(CreateText("two) ", true));
            inlineHelper.Add(CreateText("(one&", true));
            inlineHelper.Add(CreateText("two)", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("(One/Two) (One-Two) (One&Two)", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeTest11() {
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("(one: ", true));
            inlineHelper.Add(CreateText("two) ", true));
            inlineHelper.Add(CreateText("(one;", true));
            inlineHelper.Add(CreateText("two) ", true));
            inlineHelper.Add(CreateText("(one?", true));
            inlineHelper.Add(CreateText("two)", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("(One: Two) (One;Two) (One?Two)", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeTest12() {
            //TODO: replace assertNotEquals with assertEquals DEVSIX-4414
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("one@", true));
            inlineHelper.Add(CreateText("2two", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreNotEqual("One@2two", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeTest13() {
            //TODO: replace assertNotEquals with assertEquals DEVSIX-4414
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("_one", true));
            inlineHelper.Add(CreateText("_@two", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreNotEqual("_one_@Two", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeTest14() {
            //TODO: replace assertNotEquals with assertEquals DEVSIX-4414
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("one'", true));
            inlineHelper.Add(CreateText("two'", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreNotEqual("One'two'", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeTest15() {
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("( 4'", true));
            inlineHelper.Add(CreateText("two') ", true));
            inlineHelper.Add(CreateText("( one2(", true));
            inlineHelper.Add(CreateText("two))", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("( 4'Two') ( One2(Two))", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeTest16() {
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("(!one", true));
            inlineHelper.Add(CreateText("!two) ", true));
            inlineHelper.Add(CreateText("( one:", true));
            inlineHelper.Add(CreateText(":two) ", true));
            inlineHelper.Add(CreateText("( one:", true));
            inlineHelper.Add(CreateText("-two)", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("(!One!Two) ( One::Two) ( One:-Two)", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeTest17() {
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("( one:'", true));
            inlineHelper.Add(CreateText("two') ", true));
            inlineHelper.Add(CreateText("( one(", true));
            inlineHelper.Add(CreateText("two))", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("( One:'Two') ( One(Two))", lineResult);
        }

        [NUnit.Framework.Test]
        public virtual void CapitalizeTest18() {
            inlineHelper = new WaitingInlineElementsHelper(null, null);
            inlineHelper.Add(CreateText("( one,", true));
            inlineHelper.Add(CreateText("two) ", true));
            inlineHelper.Add(CreateText("( one", true));
            inlineHelper.Add(CreateText("~two)", true));
            Div div = new Div();
            inlineHelper.FlushHangingLeaves(div);
            String lineResult = GetLine(div);
            NUnit.Framework.Assert.AreEqual("( One,Two) ( One~Two)", lineResult);
        }

        private Text CreateText(String text, bool capitalizeProperty) {
            Text element = new Text(text);
            element.SetProperty(Html2PdfProperty.CAPITALIZE_ELEMENT, capitalizeProperty);
            return element;
        }

        private String GetLine(Div div) {
            IList<IElement> elementList = div.GetChildren();
            Paragraph paragraph = (Paragraph)elementList[0];
            IList<IElement> paragraphChildren = paragraph.GetChildren();
            StringBuilder line = new StringBuilder();
            foreach (IElement paragraphChild in paragraphChildren) {
                if (paragraphChild is iText.Layout.Element.Text) {
                    line.Append(((iText.Layout.Element.Text)paragraphChild).GetText());
                }
            }
            return line.ToString();
        }
    }
}
