/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
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
using System.IO;
using System.Text;
using iText.Commons.Utils;
using iText.Html2pdf.Css.Resolve;
using iText.Html2pdf.Html;
using iText.StyledXmlParser;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Media;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup;
using iText.StyledXmlParser.Resolver.Resource;
using iText.Test;

namespace iText.Html2pdf.Css {
    [NUnit.Framework.Category("UnitTest")]
    public class CssStylesResolvingTest : ExtendedITextTest {
        private static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/CssElementStylesResolvingTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
        }

        [NUnit.Framework.Test]
        public virtual void CollectStylesDeclarationsTest01() {
            Test("collectStylesDeclarationsTest01.html", "html body p", "color: red", "text-align: center", "font-size: 11.25pt"
                , "margin-bottom: 1em", "margin-left: 0", "margin-right: 0", "margin-top: 1em", "display: block", "font-family: times"
                );
        }

        [NUnit.Framework.Test]
        public virtual void CollectStylesDeclarationsTest02() {
            Test("collectStylesDeclarationsTest02.html", "html body p", "color: blue", "text-align: center", "font-style: italic"
                , "font-size: 11.25pt", "margin-bottom: 1em", "margin-left: 0", "margin-right: 0", "margin-top: 1em", 
                "display: block", "font-family: times");
        }

        [NUnit.Framework.Test]
        public virtual void CollectStylesDeclarationsTest03() {
            Test("collectStylesDeclarationsTest03.html", "html body p", "color: red", "text-align: right", "font-size: 7.5pt"
                , "margin-bottom: 1em", "margin-left: 0", "margin-right: 0", "margin-top: 1em", "display: block", "font-family: times"
                );
        }

        [NUnit.Framework.Test]
        public virtual void StylesInheritanceTest01() {
            Test("stylesInheritanceTest01.html", "html body p span", "color: blue", "text-align: center", "font-style: italic"
                , "font-size: 11.25pt", "font-family: times");
        }

        [NUnit.Framework.Test]
        public virtual void StylesInheritanceTest02() {
            Test("stylesInheritanceTest02.html", "html body p span", "color: black", "text-align: center", "font-style: italic"
                , "font-size: 11.25pt", "font-family: times");
        }

        [NUnit.Framework.Test]
        public virtual void StylesInheritanceTest03() {
            Test("stylesInheritanceTest03.html", "html body p span", "color: green", "font-size: 12.0pt", "font-family: times"
                );
        }

        [NUnit.Framework.Test]
        public virtual void StylesInheritanceTest04() {
            Test("stylesInheritanceTest04.html", "html body p span", "color: blue", "font-size: 12.0pt", "font-family: times"
                );
        }

        [NUnit.Framework.Test]
        public virtual void StylesInheritanceTest05() {
            Test("stylesInheritanceTest05.html", "html body p span", "color: black", "font-size: 12.0pt", "font-family: times"
                );
        }

        [NUnit.Framework.Test]
        public virtual void StylesInheritanceTest06() {
            Test("stylesInheritanceTest06.html", "html body p span", "margin-left: 20px", "margin-right: 0", "background-color: yellow"
                , "font-size: 12.0pt", "font-family: times");
        }

        [NUnit.Framework.Test]
        public virtual void StylesInheritanceTest07() {
            Test("stylesInheritanceTest07.html", "html body div p span", "margin-left: 0", "padding-top: 10px", "background-color: yellow"
                , "font-size: 12.0pt", "font-family: times");
        }

        [NUnit.Framework.Test]
        public virtual void StylesShorthandsTest01() {
            Test("stylesShorthandsTest01.html", "html body p", "border-bottom-style: dashed", "border-bottom-width: 5px"
                , "border-left-style: dashed", "border-left-width: 5px", "border-right-style: dashed", "border-right-width: 5px"
                , "border-top-style: dashed", "border-top-width: 5px", "border-bottom-color: red", "border-left-color: red"
                , "border-right-color: red", "border-top-color: red", "font-size: 12.0pt", "margin-bottom: 1em", "margin-left: 0"
                , "margin-right: 0", "margin-top: 1em", "display: block", "font-family: times");
        }

        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest01() {
            Test("htmlStylesConvertingTest01.html", "html body b p", "font-weight: bold", "font-size: 12.0pt", "margin-bottom: 1em"
                , "margin-left: 0", "margin-right: 0", "margin-top: 1em", "display: block", "font-family: times");
        }

        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest02() {
            Test("htmlStylesConvertingTest01.html", "html body b i p", "font-weight: bold", "font-style: italic", "font-size: 12.0pt"
                , "margin-bottom: 1em", "margin-left: 0", "margin-right: 0", "margin-top: 1em", "display: block", "font-family: times"
                );
        }

        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest03() {
            Test("htmlStylesConvertingTest01.html", "html body i p", "font-style: italic", "font-size: 12.0pt", "margin-bottom: 1em"
                , "margin-left: 0", "margin-right: 0", "margin-top: 1em", "display: block", "font-family: times");
        }

        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest04() {
            Test("htmlStylesConvertingTest01.html", "html body i center p", "font-style: italic", "text-align: center"
                , "font-size: 12.0pt", "margin-bottom: 1em", "margin-left: 0", "margin-right: 0", "margin-top: 1em", "display: block"
                , "font-family: times");
        }

        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest05() {
            Test("htmlStylesConvertingTest05.html", "html body table", "border-bottom-style: solid", "border-left-style: solid"
                , "border-right-style: solid", "border-top-style: solid", "border-bottom-width: 2px", "border-left-width: 2px"
                , "border-right-width: 2px", "border-top-width: 2px", "font-size: 12.0pt", "margin-bottom: 0", "margin-left: 0"
                , "margin-right: 0", "margin-top: 0", "text-indent: 0", "display: table", "border-spacing: 2px", "font-family: times"
                , "border-bottom-color: currentcolor", "border-left-color: currentcolor", "border-right-color: currentcolor"
                , "border-top-color: currentcolor");
        }

        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest06() {
            Test("htmlStylesConvertingTest05.html", "html body table tbody tr", "background-color: yellow", "font-size: 12.0pt"
                , "text-indent: 0", "vertical-align: middle", "display: table-row", "border-spacing: 2px", "font-family: times"
                );
        }

        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest07() {
            Test("htmlStylesConvertingTest07.html", "html body p font span", "font-size: 18.0pt", "font-family: verdana"
                , "color: blue");
        }

        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest08() {
            Test("htmlStylesConvertingTest08.html", "html body p font span", "font-size: 18.0pt", "font-family: verdana"
                , "color: blue");
        }

        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest09() {
            Test("htmlStylesConvertingTest08.html", "html body div center", "text-align: center", "display: block", "font-size: 12.0pt"
                , "font-family: times");
        }

        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest10() {
            Test("htmlStylesConvertingTest10.html", "html body p font span", "font-size: 7.5pt", "font-family: verdana"
                , "color: blue");
        }

        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest11() {
            Test("htmlStylesConvertingTest10.html", "html body", "background-color: yellow", "font-size: 12.0pt", "display: block"
                , "font-family: times");
        }

        private void ResolveStylesForTree(INode node, ICssResolver cssResolver, CssContext context) {
            if (node is IElementNode) {
                IElementNode element = (IElementNode)node;
                element.SetStyles(cssResolver.ResolveStyles((IElementNode)node, context));
                if (TagConstants.HTML.Equals(element.Name())) {
                    context.SetRootFontSize(element.GetStyles().Get(CssConstants.FONT_SIZE));
                }
            }
            foreach (INode child in node.ChildNodes()) {
                ResolveStylesForTree(child, cssResolver, context);
            }
        }

        private void Test(String fileName, String elementPath, params String[] expectedStyles) {
            String filePath = sourceFolder + fileName;
            IXmlParser parser = new JsoupHtmlParser();
            IDocumentNode document = parser.Parse(new FileStream(filePath, FileMode.Open, FileAccess.Read), "UTF-8");
            ICssResolver cssResolver = new DefaultCssResolver(document, MediaDeviceDescription.CreateDefault(), new ResourceResolver
                (""));
            CssContext context = new CssContext();
            ResolveStylesForTree(document, cssResolver, context);
            IElementNode element = FindElement(document, elementPath);
            if (element == null) {
                NUnit.Framework.Assert.Fail(MessageFormatUtil.Format("Element at path \"{0}\" was not found.", elementPath
                    ));
            }
            IDictionary<String, String> elementStyles = element.GetStyles();
            ICollection<String> expectedStylesSet = new HashSet<String>(JavaUtil.ArraysAsList(expectedStyles));
            ICollection<String> actualStylesSet = StylesMapToHashSet(elementStyles);
            NUnit.Framework.Assert.IsTrue(SetsAreEqual(expectedStylesSet, actualStylesSet), GetDifferencesMessage(expectedStylesSet
                , actualStylesSet));
        }

        private IElementNode FindElement(INode root, String ancestryPath) {
            INode currElement = root;
            String[] ancestors = iText.Commons.Utils.StringUtil.Split(ancestryPath, " ");
            int ancestorPathIndex = 0;
            bool foundElement = false;
            while (ancestorPathIndex < ancestors.Length) {
                String ancestor = ancestors[ancestorPathIndex];
                int ancestorIndex = 0;
                int dash = ancestor.IndexOf('-');
                if (dash > 0) {
                    ancestorIndex = Convert.ToInt32(ancestor.JSubstring(dash + 1, ancestor.Length), System.Globalization.CultureInfo.InvariantCulture
                        );
                    ancestor = ancestor.JSubstring(0, dash);
                }
                int sameNameInd = 0;
                foundElement = false;
                foreach (INode nextKid in currElement.ChildNodes()) {
                    if (nextKid is IElementNode && ((IElementNode)nextKid).Name().Equals(ancestor) && sameNameInd++ == ancestorIndex
                        ) {
                        currElement = nextKid;
                        foundElement = true;
                        break;
                    }
                }
                if (foundElement) {
                    ++ancestorPathIndex;
                }
                else {
                    break;
                }
            }
            return foundElement ? (IElementNode)currElement : null;
        }

        private ICollection<String> StylesMapToHashSet(IDictionary<String, String> stylesMap) {
            ICollection<String> stylesSet = new HashSet<String>();
            foreach (KeyValuePair<String, String> entry in stylesMap) {
                stylesSet.Add(entry.Key + ": " + entry.Value);
            }
            return stylesSet;
        }

        private String GetDifferencesMessage(ICollection<String> expectedStyles, ICollection<String> actualStyles) {
            StringBuilder sb = new StringBuilder("Resolved styles are different from expected!");
            ICollection<String> expCopy = new SortedSet<String>(expectedStyles);
            ICollection<String> actCopy = new SortedSet<String>(actualStyles);
            expCopy.RemoveAll(actualStyles);
            actCopy.RemoveAll(expectedStyles);
            if (!expCopy.IsEmpty()) {
                sb.Append("\nExpected but not found properties:\n");
                foreach (String expProp in expCopy) {
                    sb.Append(expProp).Append('\n');
                }
            }
            if (!actCopy.IsEmpty()) {
                sb.Append("\nNot expected but found properties:\n");
                foreach (String actProp in actCopy) {
                    sb.Append(actProp).Append('\n');
                }
            }
            return sb.ToString();
        }

        private bool SetsAreEqual(ICollection<String> expectedStyles, ICollection<String> actualStyles) {
            bool sizesEqual = expectedStyles.Count == actualStyles.Count;
            bool elementsEqual = true;
            foreach (String str in actualStyles) {
                if (str.StartsWith("font-size")) {
                    if (!CompareFloatProperty(expectedStyles, actualStyles, "font-size")) {
                        elementsEqual = false;
                        break;
                    }
                }
                else {
                    if (!expectedStyles.Contains(str)) {
                        elementsEqual = false;
                        break;
                    }
                }
            }
            return sizesEqual && elementsEqual;
        }

        private bool CompareFloatProperty(ICollection<String> expectedStyles, ICollection<String> actualStyles, String
             propertyName) {
            String containsExpected = null;
            foreach (String str in expectedStyles) {
                if (str.StartsWith(propertyName)) {
                    containsExpected = str;
                }
            }
            String containsActual = null;
            foreach (String str in actualStyles) {
                if (str.StartsWith(propertyName)) {
                    containsActual = str;
                }
            }
            if (containsActual == null && containsExpected == null) {
                return true;
            }
            if (containsActual != null && containsExpected != null) {
                containsActual = containsActual.Substring(propertyName.Length + 1).Trim();
                containsExpected = containsExpected.Substring(propertyName.Length + 1).Trim();
                float actual = CssDimensionParsingUtils.ParseAbsoluteLength(containsActual, CssConstants.PT);
                float expected = CssDimensionParsingUtils.ParseAbsoluteLength(containsExpected, CssConstants.PT);
                return Math.Abs(actual - expected) < 0.0001;
            }
            else {
                return false;
            }
        }
    }
}
