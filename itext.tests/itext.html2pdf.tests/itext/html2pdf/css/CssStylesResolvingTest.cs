/*
This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
Authors: iText Software.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using iText.Html2pdf.Css.Media;
using iText.Html2pdf.Css.Resolve;
using iText.Html2pdf.Css.Util;
using iText.Html2pdf.Html;
using iText.Html2pdf.Html.Impl.Jsoup;
using iText.Html2pdf.Html.Node;
using iText.Html2pdf.Resolver.Resource;
using iText.IO.Util;
using iText.Test;

namespace iText.Html2pdf.Css {
    public class CssStylesResolvingTest : ExtendedITextTest {
        private static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/css/CssElementStylesResolvingTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void CollectStylesDeclarationsTest01() {
            Test("collectStylesDeclarationsTest01.html", "html body p", "color: red", "text-align: center", "font-size: 11.25pt"
                , "margin-bottom: 1em", "margin-left: 0", "margin-right: 0", "margin-top: 1em", "display: block", "font-family: times-roman"
                );
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void CollectStylesDeclarationsTest02() {
            Test("collectStylesDeclarationsTest02.html", "html body p", "color: blue", "text-align: center", "font-style: italic"
                , "font-size: 11.25pt", "margin-bottom: 1em", "margin-left: 0", "margin-right: 0", "margin-top: 1em", 
                "display: block", "font-family: times-roman");
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void CollectStylesDeclarationsTest03() {
            Test("collectStylesDeclarationsTest03.html", "html body p", "color: red", "text-align: right", "font-size: 7.5pt"
                , "margin-bottom: 1em", "margin-left: 0", "margin-right: 0", "margin-top: 1em", "display: block", "font-family: times-roman"
                );
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void StylesInheritanceTest01() {
            Test("stylesInheritanceTest01.html", "html body p span", "color: blue", "text-align: center", "font-style: italic"
                , "font-size: 11.25pt", "font-family: times-roman");
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void StylesInheritanceTest02() {
            Test("stylesInheritanceTest02.html", "html body p span", "color: black", "text-align: center", "font-style: italic"
                , "font-size: 11.25pt", "font-family: times-roman");
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void StylesInheritanceTest03() {
            Test("stylesInheritanceTest03.html", "html body p span", "color: green", "font-size: 12.0pt", "font-family: times-roman"
                );
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void StylesInheritanceTest04() {
            Test("stylesInheritanceTest04.html", "html body p span", "color: blue", "font-size: 12.0pt", "font-family: times-roman"
                );
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void StylesInheritanceTest05() {
            Test("stylesInheritanceTest05.html", "html body p span", "color: black", "font-size: 12.0pt", "font-family: times-roman"
                );
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void StylesInheritanceTest06() {
            Test("stylesInheritanceTest06.html", "html body p span", "margin-left: 20px", "margin-right: 0", "background-color: yellow"
                , "font-size: 12.0pt", "font-family: times-roman");
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void StylesInheritanceTest07() {
            Test("stylesInheritanceTest07.html", "html body div p span", "margin-left: 0", "padding-top: 10px", "background-color: yellow"
                , "font-size: 12.0pt", "font-family: times-roman");
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void StylesShorthandsTest01() {
            Test("stylesShorthandsTest01.html", "html body p", "border-bottom-style: dashed", "border-bottom-width: 5px"
                , "border-left-style: dashed", "border-left-width: 5px", "border-right-style: dashed", "border-right-width: 5px"
                , "border-top-style: dashed", "border-top-width: 5px", "border-bottom-color: red", "border-left-color: red"
                , "border-right-color: red", "border-top-color: red", "font-size: 12.0pt", "margin-bottom: 1em", "margin-left: 0"
                , "margin-right: 0", "margin-top: 1em", "display: block", "font-family: times-roman");
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest01() {
            Test("htmlStylesConvertingTest01.html", "html body b p", "font-weight: bold", "font-size: 12.0pt", "margin-bottom: 1em"
                , "margin-left: 0", "margin-right: 0", "margin-top: 1em", "display: block", "font-family: times-roman"
                );
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest02() {
            Test("htmlStylesConvertingTest01.html", "html body b i p", "font-weight: bold", "font-style: italic", "font-size: 12.0pt"
                , "margin-bottom: 1em", "margin-left: 0", "margin-right: 0", "margin-top: 1em", "display: block", "font-family: times-roman"
                );
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest03() {
            Test("htmlStylesConvertingTest01.html", "html body i p", "font-style: italic", "font-size: 12.0pt", "margin-bottom: 1em"
                , "margin-left: 0", "margin-right: 0", "margin-top: 1em", "display: block", "font-family: times-roman"
                );
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest04() {
            Test("htmlStylesConvertingTest01.html", "html body i center p", "font-style: italic", "text-align: center"
                , "font-size: 12.0pt", "margin-bottom: 1em", "margin-left: 0", "margin-right: 0", "margin-top: 1em", "display: block"
                , "font-family: times-roman");
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest05() {
            Test("htmlStylesConvertingTest05.html", "html body table", "border-bottom-style: solid", "border-left-style: solid"
                , "border-right-style: solid", "border-top-style: solid", "border-bottom-width: 2px", "border-left-width: 2px"
                , "border-right-width: 2px", "border-top-width: 2px", "font-size: 12.0pt", "margin-bottom: 0", "margin-left: 0"
                , "margin-right: 0", "margin-top: 0", "text-indent: 0", "display: table", "border-spacing: 2px", "font-family: times-roman"
                );
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest06() {
            Test("htmlStylesConvertingTest05.html", "html body table tbody tr", "background-color: yellow", "font-size: 12.0pt"
                , "text-indent: 0", "vertical-align: middle", "display: table-row", "border-spacing: 2px", "font-family: times-roman"
                );
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest07() {
            Test("htmlStylesConvertingTest07.html", "html body p font span", "font-size: 18.0pt", "font-family: verdana"
                , "color: blue");
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest08() {
            Test("htmlStylesConvertingTest08.html", "html body p font span", "font-size: 18.0pt", "font-family: verdana"
                , "color: blue");
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest09() {
            Test("htmlStylesConvertingTest08.html", "html body div center", "text-align: center", "display: block", "font-size: 12.0pt"
                , "font-family: times-roman");
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest10() {
            Test("htmlStylesConvertingTest10.html", "html body p font span", "font-size: 7.5pt", "font-family: verdana"
                , "color: blue");
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void HtmlStylesConvertingTest11() {
            Test("htmlStylesConvertingTest10.html", "html body", "background-color: yellow", "font-size: 12.0pt", "margin-bottom: 10%"
                , "margin-left: 10%", "margin-right: 10%", "margin-top: 10%", "display: block", "font-family: times-roman"
                );
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

        /// <exception cref="System.IO.IOException"/>
        private void Test(String fileName, String elementPath, params String[] expectedStyles) {
            String filePath = sourceFolder + fileName;
            IHtmlParser parser = new JsoupHtmlParser();
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
            ICollection<String> expectedStylesSet = new HashSet<String>(iText.IO.Util.JavaUtil.ArraysAsList(expectedStyles
                ));
            ICollection<String> actualStylesSet = StylesMapToHashSet(elementStyles);
            NUnit.Framework.Assert.IsTrue(SetsAreEqual(expectedStylesSet, actualStylesSet), GetDifferencesMessage(expectedStylesSet
                , actualStylesSet));
        }

        private IElementNode FindElement(INode root, String ancestryPath) {
            INode currElement = root;
            String[] ancestors = iText.IO.Util.StringUtil.Split(ancestryPath, " ");
            int ancestorPathIndex = 0;
            bool foundElement = false;
            while (ancestorPathIndex < ancestors.Length) {
                String ancestor = ancestors[ancestorPathIndex];
                int ancestorIndex = 0;
                int dash = ancestor.IndexOf('-');
                if (dash > 0) {
                    ancestorIndex = System.Convert.ToInt32(ancestor.JSubstring(dash + 1, ancestor.Length));
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
                float actual = CssUtils.ParseAbsoluteLength(containsActual, CssConstants.PT);
                float expected = CssUtils.ParseAbsoluteLength(containsExpected, CssConstants.PT);
                return Math.Abs(actual - expected) < 0.0001;
            }
            else {
                return false;
            }
        }
    }
}
