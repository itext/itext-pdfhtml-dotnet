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
