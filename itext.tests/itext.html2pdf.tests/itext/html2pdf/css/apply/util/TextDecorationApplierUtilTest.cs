/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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
using iText.Html2pdf.Css;
using iText.StyledXmlParser.Node;
using iText.Test;

namespace iText.Html2pdf.Css.Apply.Util {
    [NUnit.Framework.Category("UnitTest")]
    public class TextDecorationApplierUtilTest : ExtendedITextTest {
        private static IElementNode CreateNewNode(IElementNode parent, String color, String line, String decorationStyle
            ) {
            IElementNode node = new TextDecorationApplierUtilTest.TextDecorationTestElementNode(parent);
            if (color != null) {
                node.GetStyles().Put(CssConstants.TEXT_DECORATION_COLOR, color);
            }
            if (line != null) {
                node.GetStyles().Put(CssConstants.TEXT_DECORATION_LINE, line);
            }
            if (decorationStyle != null) {
                node.GetStyles().Put(CssConstants.TEXT_DECORATION_STYLE, decorationStyle);
            }
            return node;
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationHasNoParentShouldNotAlterStyles() {
            String color = "red";
            String line = "line-through";
            String style = "solid";
            IElementNode node = CreateNewNode(null, color, line, style);
            TextDecorationApplierUtil.PropagateTextDecorationProperties(node);
            NUnit.Framework.Assert.AreEqual(color, node.GetStyles().Get(CssConstants.TEXT_DECORATION_COLOR));
            NUnit.Framework.Assert.AreEqual(line, node.GetStyles().Get(CssConstants.TEXT_DECORATION_LINE));
            NUnit.Framework.Assert.AreEqual(style, node.GetStyles().Get(CssConstants.TEXT_DECORATION_STYLE));
        }

        [NUnit.Framework.Test]
        public virtual void ParentNodeHasNoStyleMapDoesNothing() {
            IElementNode parent = new TextDecorationApplierUtilTest.TextDecorationTestElementNode(null);
            parent.SetStyles(null);
            String colorChild1 = "yellow";
            String lineChild1 = "line-under";
            String styleChild1 = "solid";
            TextDecorationApplierUtil.PropagateTextDecorationProperties(parent);
            IElementNode child1 = CreateNewNode(parent, colorChild1, lineChild1, styleChild1);
            parent.AddChild(child1);
            TextDecorationApplierUtil.PropagateTextDecorationProperties(child1);
            IDictionary<String, String> childStyles = child1.GetStyles();
            NUnit.Framework.Assert.AreEqual(colorChild1, childStyles.Get(CssConstants.TEXT_DECORATION_COLOR));
            NUnit.Framework.Assert.AreEqual(lineChild1, childStyles.Get(CssConstants.TEXT_DECORATION_LINE));
            NUnit.Framework.Assert.AreEqual(styleChild1, childStyles.Get(CssConstants.TEXT_DECORATION_STYLE));
        }

        [NUnit.Framework.Test]
        public virtual void ParentNoStyleAtAllDoesNotImpactChild() {
            IElementNode parent = CreateNewNode(null, null, null, null);
            String colorChild1 = "yellow";
            String lineChild1 = "line-under";
            String styleChild1 = "solid";
            TextDecorationApplierUtil.PropagateTextDecorationProperties(parent);
            IElementNode child1 = CreateNewNode(parent, colorChild1, lineChild1, styleChild1);
            parent.AddChild(child1);
            TextDecorationApplierUtil.PropagateTextDecorationProperties(child1);
            IDictionary<String, String> childStyles = child1.GetStyles();
            NUnit.Framework.Assert.AreEqual(colorChild1, childStyles.Get(CssConstants.TEXT_DECORATION_COLOR));
            NUnit.Framework.Assert.AreEqual(lineChild1, childStyles.Get(CssConstants.TEXT_DECORATION_LINE));
            NUnit.Framework.Assert.AreEqual(styleChild1, childStyles.Get(CssConstants.TEXT_DECORATION_STYLE));
        }

        [NUnit.Framework.Test]
        public virtual void ParentOnlyHasColorChildStylesShouldBeMerged() {
            IElementNode parent = CreateNewNode(null, "red", null, null);
            TextDecorationApplierUtil.PropagateTextDecorationProperties(parent);
            String colorChild1 = "yellow";
            String lineChild1 = "line-under";
            String styleChild1 = "solid";
            IElementNode child1 = CreateNewNode(parent, colorChild1, lineChild1, styleChild1);
            parent.AddChild(child1);
            TextDecorationApplierUtil.PropagateTextDecorationProperties(child1);
            IDictionary<String, String> childStyles = child1.GetStyles();
            // we only expect yellow because the line is ignored
            String expectedColorChild = "yellow";
            String expectedLineChild = "line-under";
            String expectedStyleChild = "solid";
            NUnit.Framework.Assert.AreEqual(expectedColorChild, childStyles.Get(CssConstants.TEXT_DECORATION_COLOR));
            NUnit.Framework.Assert.AreEqual(expectedLineChild, childStyles.Get(CssConstants.TEXT_DECORATION_LINE));
            NUnit.Framework.Assert.AreEqual(expectedStyleChild, childStyles.Get(CssConstants.TEXT_DECORATION_STYLE));
        }

        [NUnit.Framework.Test]
        public virtual void ParentOnlyHasLineOnlyChildStylesShouldBeMerged() {
            IElementNode parent = CreateNewNode(null, null, "line-through", null);
            String colorChild1 = "yellow";
            String lineChild1 = "line-under";
            String styleChild1 = "solid";
            TextDecorationApplierUtil.PropagateTextDecorationProperties(parent);
            IElementNode child1 = CreateNewNode(parent, colorChild1, lineChild1, styleChild1);
            parent.AddChild(child1);
            TextDecorationApplierUtil.PropagateTextDecorationProperties(child1);
            IDictionary<String, String> childStyles = child1.GetStyles();
            String expectedColorChild = "currentcolor yellow";
            String expectedLineChild = "line-through line-under";
            String expectedStyleChild = "solid solid";
            NUnit.Framework.Assert.AreEqual(expectedColorChild, childStyles.Get(CssConstants.TEXT_DECORATION_COLOR));
            NUnit.Framework.Assert.AreEqual(expectedLineChild, childStyles.Get(CssConstants.TEXT_DECORATION_LINE));
            NUnit.Framework.Assert.AreEqual(expectedStyleChild, childStyles.Get(CssConstants.TEXT_DECORATION_STYLE));
        }

        [NUnit.Framework.Test]
        public virtual void ParentOnlyHasDecorationOnlyChildStylesShouldBeMerged() {
            IElementNode parent = CreateNewNode(null, null, null, "solid");
            String colorChild1 = "yellow";
            String lineChild1 = "line-under";
            String styleChild1 = "solid";
            TextDecorationApplierUtil.PropagateTextDecorationProperties(parent);
            IElementNode child1 = CreateNewNode(parent, colorChild1, lineChild1, styleChild1);
            parent.AddChild(child1);
            TextDecorationApplierUtil.PropagateTextDecorationProperties(child1);
            IDictionary<String, String> childStyles = child1.GetStyles();
            //first is ignored because no line style is defined
            String expectedColorChild = "yellow";
            String expectedLineChild = "line-under";
            String expectedStyleChild = "solid";
            NUnit.Framework.Assert.AreEqual(expectedColorChild, childStyles.Get(CssConstants.TEXT_DECORATION_COLOR));
            NUnit.Framework.Assert.AreEqual(expectedLineChild, childStyles.Get(CssConstants.TEXT_DECORATION_LINE));
            NUnit.Framework.Assert.AreEqual(expectedStyleChild, childStyles.Get(CssConstants.TEXT_DECORATION_STYLE));
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationShouldBeAppliedToChild() {
            String colorParent = "red";
            String lineParent = "line-through";
            String styleParent = "solid";
            IElementNode parent = CreateNewNode(null, colorParent, lineParent, styleParent);
            TextDecorationApplierUtil.PropagateTextDecorationProperties(parent);
            String colorChild1 = "yellow";
            String lineChild1 = "line-under";
            String styleChild1 = "solid";
            IElementNode child1 = CreateNewNode(parent, colorChild1, lineChild1, styleChild1);
            parent.AddChild(child1);
            TextDecorationApplierUtil.PropagateTextDecorationProperties(child1);
            IDictionary<String, String> childStyles = child1.GetStyles();
            String expectedColorChild = "red yellow";
            String expectedLineChild = "line-through line-under";
            String expectedStyleChild = "solid solid";
            NUnit.Framework.Assert.AreEqual(expectedColorChild, childStyles.Get(CssConstants.TEXT_DECORATION_COLOR));
            NUnit.Framework.Assert.AreEqual(expectedLineChild, childStyles.Get(CssConstants.TEXT_DECORATION_LINE));
            NUnit.Framework.Assert.AreEqual(expectedStyleChild, childStyles.Get(CssConstants.TEXT_DECORATION_STYLE));
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationShouldBeAppliedToChildWithoutDuplicates() {
            String colorParent = "red";
            String lineParent = "line-through";
            String styleParent = "solid";
            IElementNode parent = CreateNewNode(null, colorParent, lineParent, styleParent);
            IElementNode child1 = CreateNewNode(parent, colorParent, lineParent, styleParent);
            parent.AddChild(child1);
            TextDecorationApplierUtil.PropagateTextDecorationProperties(child1);
            IDictionary<String, String> childStyles = child1.GetStyles();
            NUnit.Framework.Assert.AreEqual(colorParent, childStyles.Get(CssConstants.TEXT_DECORATION_COLOR));
            NUnit.Framework.Assert.AreEqual(lineParent, childStyles.Get(CssConstants.TEXT_DECORATION_LINE));
            NUnit.Framework.Assert.AreEqual(styleParent, childStyles.Get(CssConstants.TEXT_DECORATION_STYLE));
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationOneColor2StylesShouldBeAppliedToChild() {
            String colorParent = "red";
            String lineParent = "line-through line-over";
            String styleParent = "solid";
            IElementNode parent = CreateNewNode(null, colorParent, lineParent, styleParent);
            TextDecorationApplierUtil.PropagateTextDecorationProperties(parent);
            String colorChild1 = "yellow";
            String lineChild1 = "line-under";
            String styleChild1 = "solid";
            IElementNode child1 = CreateNewNode(parent, colorChild1, lineChild1, styleChild1);
            parent.AddChild(child1);
            TextDecorationApplierUtil.PropagateTextDecorationProperties(child1);
            IDictionary<String, String> childStyles = child1.GetStyles();
            String expectedColorChild = "red red yellow";
            String expectedLineChild = "line-through line-over line-under";
            String expectedStyleChild = "solid solid solid";
            NUnit.Framework.Assert.AreEqual(expectedColorChild, childStyles.Get(CssConstants.TEXT_DECORATION_COLOR));
            NUnit.Framework.Assert.AreEqual(expectedLineChild, childStyles.Get(CssConstants.TEXT_DECORATION_LINE));
            NUnit.Framework.Assert.AreEqual(expectedStyleChild, childStyles.Get(CssConstants.TEXT_DECORATION_STYLE));
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationShouldBeAppliedToChildAndSubChild() {
            String colorParent = "red";
            String lineParent = "line-through";
            String styleParent = "solid";
            IElementNode parent = CreateNewNode(null, colorParent, lineParent, styleParent);
            String colorChild1 = "yellow";
            String lineChild1 = "line-under";
            String styleChild1 = "solid";
            IElementNode child1 = CreateNewNode(parent, colorChild1, lineChild1, styleChild1);
            parent.AddChild(child1);
            String colorSubChild1 = "pink";
            String lineSubChild1 = "line-over";
            String styleSubChild1 = "solid";
            IElementNode subChild1 = CreateNewNode(child1, colorSubChild1, lineSubChild1, styleSubChild1);
            child1.AddChild(subChild1);
            TextDecorationApplierUtil.PropagateTextDecorationProperties(child1);
            IDictionary<String, String> child1Styles = child1.GetStyles();
            String expectedColorChild = "red yellow";
            String expectedLineChild = "line-through line-under";
            String expectedStyleChild = "solid solid";
            NUnit.Framework.Assert.AreEqual(expectedColorChild, child1Styles.Get(CssConstants.TEXT_DECORATION_COLOR));
            NUnit.Framework.Assert.AreEqual(expectedLineChild, child1Styles.Get(CssConstants.TEXT_DECORATION_LINE));
            NUnit.Framework.Assert.AreEqual(expectedStyleChild, child1Styles.Get(CssConstants.TEXT_DECORATION_STYLE));
            TextDecorationApplierUtil.PropagateTextDecorationProperties(subChild1);
            child1Styles = subChild1.GetStyles();
            String expectedColorSubChild = "red yellow pink";
            String expectedLineSubChild = "line-through line-under line-over";
            String expectedStyleSubChild = "solid solid solid";
            NUnit.Framework.Assert.AreEqual(expectedColorSubChild, child1Styles.Get(CssConstants.TEXT_DECORATION_COLOR
                ));
            NUnit.Framework.Assert.AreEqual(expectedLineSubChild, child1Styles.Get(CssConstants.TEXT_DECORATION_LINE));
            NUnit.Framework.Assert.AreEqual(expectedStyleSubChild, child1Styles.Get(CssConstants.TEXT_DECORATION_STYLE
                ));
        }

        [NUnit.Framework.Test]
        public virtual void TextDecorationShouldBeAppliedToChildAndSubChildWhenSecondChildDoesntHaveAttributes() {
            String colorParent = "red";
            String lineParent = "line-through";
            String styleParent = "solid";
            IElementNode parent = CreateNewNode(null, colorParent, lineParent, styleParent);
            IElementNode child1 = CreateNewNode(parent, null, null, null);
            parent.AddChild(child1);
            TextDecorationApplierUtil.PropagateTextDecorationProperties(child1);
            IDictionary<String, String> child1Styles = child1.GetStyles();
            String expectedColorChild = "red";
            String expectedLineChild = "line-through";
            String expectedStyleChild = "solid";
            NUnit.Framework.Assert.AreEqual(expectedColorChild, child1Styles.Get(CssConstants.TEXT_DECORATION_COLOR));
            NUnit.Framework.Assert.AreEqual(expectedLineChild, child1Styles.Get(CssConstants.TEXT_DECORATION_LINE));
            NUnit.Framework.Assert.AreEqual(expectedStyleChild, child1Styles.Get(CssConstants.TEXT_DECORATION_STYLE));
            String colorSubChild1 = "pink";
            String lineSubChild1 = "line-over";
            String styleSubChild1 = "solid";
            IElementNode subChild1 = CreateNewNode(child1, colorSubChild1, lineSubChild1, styleSubChild1);
            child1.AddChild(subChild1);
            TextDecorationApplierUtil.PropagateTextDecorationProperties(subChild1);
            child1Styles = subChild1.GetStyles();
            String expectedColorSubChild = "red pink";
            String expectedLineSubChild = "line-through line-over";
            String expectedStyleSubChild = "solid solid";
            NUnit.Framework.Assert.AreEqual(expectedColorSubChild, child1Styles.Get(CssConstants.TEXT_DECORATION_COLOR
                ));
            NUnit.Framework.Assert.AreEqual(expectedLineSubChild, child1Styles.Get(CssConstants.TEXT_DECORATION_LINE));
            NUnit.Framework.Assert.AreEqual(expectedStyleSubChild, child1Styles.Get(CssConstants.TEXT_DECORATION_STYLE
                ));
        }

        internal class TextDecorationTestElementNode : IElementNode {
            private readonly INode parent;

            internal TextDecorationTestElementNode(INode parent) {
                this.parent = parent;
            }

            public virtual String Name() {
                return "testnode";
            }

            public virtual IAttributes GetAttributes() {
                return null;
            }

            public virtual String GetAttribute(String key) {
                return null;
            }

            public virtual IList<IDictionary<String, String>> GetAdditionalHtmlStyles() {
                return null;
            }

            public virtual void AddAdditionalHtmlStyles(IDictionary<String, String> styles) {
            }

            public virtual String GetLang() {
                return "en";
            }

            public virtual IList<INode> ChildNodes() {
                return children;
            }

            internal IList<INode> children = new List<INode>();

            public virtual void AddChild(INode node) {
                children.Add(node);
            }

            public virtual INode ParentNode() {
                return parent;
            }

            public virtual void SetStyles(IDictionary<String, String> stringStringMap) {
            }

            internal Dictionary<String, String> styles = new Dictionary<String, String>();

            public virtual IDictionary<String, String> GetStyles() {
                return styles;
            }
        }
    }
}
