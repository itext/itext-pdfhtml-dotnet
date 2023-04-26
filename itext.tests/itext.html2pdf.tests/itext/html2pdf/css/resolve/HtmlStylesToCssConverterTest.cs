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
using iText.Html2pdf.Html;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Node.Impl.Jsoup.Node;
using iText.Test;

namespace iText.Html2pdf.Css.Resolve {
    [NUnit.Framework.Category("UnitTest")]
    public class HtmlStylesToCssConverterTest : ExtendedITextTest {
        [NUnit.Framework.Test]
        public virtual void TrimSemicolonsForWidthAttributeTest() {
            iText.StyledXmlParser.Jsoup.Nodes.Element element = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf(TagConstants.IMG), "");
            element.Attr("width", "53%;;;");
            JsoupElementNode node = new JsoupElementNode(element);
            IList<CssDeclaration> cssDeclarations = HtmlStylesToCssConverter.Convert(node);
            AssertCssDeclarationListWithOneElement(cssDeclarations, "width", "53%");
        }

        [NUnit.Framework.Test]
        public virtual void WithoutSemicolonsWidthAttributeTest() {
            iText.StyledXmlParser.Jsoup.Nodes.Element element = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf(TagConstants.IMG), "");
            element.Attr("width", "52%");
            JsoupElementNode node = new JsoupElementNode(element);
            IList<CssDeclaration> cssDeclarations = HtmlStylesToCssConverter.Convert(node);
            AssertCssDeclarationListWithOneElement(cssDeclarations, "width", "52%");
        }

        [NUnit.Framework.Test]
        public virtual void TrimSemicolonsForHeightAttributeTest() {
            iText.StyledXmlParser.Jsoup.Nodes.Element element = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf(TagConstants.IMG), "");
            element.Attr("height", "51%;");
            JsoupElementNode node = new JsoupElementNode(element);
            IList<CssDeclaration> cssDeclarations = HtmlStylesToCssConverter.Convert(node);
            AssertCssDeclarationListWithOneElement(cssDeclarations, "height", "51%");
        }

        [NUnit.Framework.Test]
        public virtual void WithoutSemicolonsHeightAttributeTest() {
            iText.StyledXmlParser.Jsoup.Nodes.Element element = new iText.StyledXmlParser.Jsoup.Nodes.Element(iText.StyledXmlParser.Jsoup.Parser.Tag
                .ValueOf(TagConstants.IMG), "");
            element.Attr("height", "50%");
            JsoupElementNode node = new JsoupElementNode(element);
            IList<CssDeclaration> cssDeclarations = HtmlStylesToCssConverter.Convert(node);
            AssertCssDeclarationListWithOneElement(cssDeclarations, "height", "50%");
        }

        private static void AssertCssDeclarationListWithOneElement(IList<CssDeclaration> cssDeclarations, String prop
            , String exp) {
            NUnit.Framework.Assert.AreEqual(1, cssDeclarations.Count);
            NUnit.Framework.Assert.AreEqual(prop, cssDeclarations[0].GetProperty());
            NUnit.Framework.Assert.AreEqual(exp, cssDeclarations[0].GetExpression());
        }
    }
}
