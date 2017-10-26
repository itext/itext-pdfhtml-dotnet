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
using System.Text;
using iText.Html2pdf.Jsoup;
using iText.Html2pdf.Jsoup.Select;

namespace iText.Html2pdf.Jsoup.Nodes {
    /// <summary>Tests Nodes</summary>
    /// <author>Jonathan Hedley, jonathan@hedley.net</author>
    public class NodeTest {
        [NUnit.Framework.Test]
        public virtual void HandlesBaseUri() {
            iText.Html2pdf.Jsoup.Parser.Tag tag = iText.Html2pdf.Jsoup.Parser.Tag.ValueOf("a");
            Attributes attribs = new Attributes();
            attribs.Put("relHref", "/foo");
            attribs.Put("absHref", "http://bar/qux");
            iText.Html2pdf.Jsoup.Nodes.Element noBase = new iText.Html2pdf.Jsoup.Nodes.Element(tag, "", attribs);
            NUnit.Framework.Assert.AreEqual("", noBase.AbsUrl("relHref"));
            // with no base, should NOT fallback to href attrib, whatever it is
            NUnit.Framework.Assert.AreEqual("http://bar/qux", noBase.AbsUrl("absHref"));
            // no base but valid attrib, return attrib
            iText.Html2pdf.Jsoup.Nodes.Element withBase = new iText.Html2pdf.Jsoup.Nodes.Element(tag, "http://foo/", attribs
                );
            NUnit.Framework.Assert.AreEqual("http://foo/foo", withBase.AbsUrl("relHref"));
            // construct abs from base + rel
            NUnit.Framework.Assert.AreEqual("http://bar/qux", withBase.AbsUrl("absHref"));
            // href is abs, so returns that
            NUnit.Framework.Assert.AreEqual("", withBase.AbsUrl("noval"));
            iText.Html2pdf.Jsoup.Nodes.Element dodgyBase = new iText.Html2pdf.Jsoup.Nodes.Element(tag, "wtf://no-such-protocol/"
                , attribs);
            NUnit.Framework.Assert.AreEqual("http://bar/qux", dodgyBase.AbsUrl("absHref"));
        }

        // base fails, but href good, so get that
        // base fails, only rel href, so return nothing
        [NUnit.Framework.Test]
        public virtual void SetBaseUriIsRecursive() {
            Document doc = iText.Html2pdf.Jsoup.Jsoup.Parse("<div><p></p></div>");
            String baseUri = "http://jsoup.org";
            doc.SetBaseUri(baseUri);
            NUnit.Framework.Assert.AreEqual(baseUri, doc.BaseUri());
            NUnit.Framework.Assert.AreEqual(baseUri, doc.Select("div").First().BaseUri());
            NUnit.Framework.Assert.AreEqual(baseUri, doc.Select("p").First().BaseUri());
        }

        [NUnit.Framework.Test]
        public virtual void HandlesAbsPrefix() {
            Document doc = iText.Html2pdf.Jsoup.Jsoup.Parse("<a href=/foo>Hello</a>", "http://jsoup.org/");
            iText.Html2pdf.Jsoup.Nodes.Element a = doc.Select("a").First();
            NUnit.Framework.Assert.AreEqual("/foo", a.Attr("href"));
            NUnit.Framework.Assert.AreEqual("http://jsoup.org/foo", a.Attr("abs:href"));
            NUnit.Framework.Assert.IsTrue(a.HasAttr("abs:href"));
        }

        [NUnit.Framework.Test]
        public virtual void HandlesAbsOnImage() {
            Document doc = iText.Html2pdf.Jsoup.Jsoup.Parse("<p><img src=\"/rez/osi_logo.png\" /></p>", "http://jsoup.org/"
                );
            iText.Html2pdf.Jsoup.Nodes.Element img = doc.Select("img").First();
            NUnit.Framework.Assert.AreEqual("http://jsoup.org/rez/osi_logo.png", img.Attr("abs:src"));
            NUnit.Framework.Assert.AreEqual(img.AbsUrl("src"), img.Attr("abs:src"));
        }

        [NUnit.Framework.Test]
        public virtual void HandlesAbsPrefixOnHasAttr() {
            // 1: no abs url; 2: has abs url
            Document doc = iText.Html2pdf.Jsoup.Jsoup.Parse("<a id=1 href='/foo'>One</a> <a id=2 href='http://jsoup.org/'>Two</a>"
                );
            iText.Html2pdf.Jsoup.Nodes.Element one = doc.Select("#1").First();
            iText.Html2pdf.Jsoup.Nodes.Element two = doc.Select("#2").First();
            NUnit.Framework.Assert.IsFalse(one.HasAttr("abs:href"));
            NUnit.Framework.Assert.IsTrue(one.HasAttr("href"));
            NUnit.Framework.Assert.AreEqual("", one.AbsUrl("href"));
            NUnit.Framework.Assert.IsTrue(two.HasAttr("abs:href"));
            NUnit.Framework.Assert.IsTrue(two.HasAttr("href"));
            NUnit.Framework.Assert.AreEqual("http://jsoup.org/", two.AbsUrl("href"));
        }

        [NUnit.Framework.Test]
        public virtual void LiteralAbsPrefix() {
            // if there is a literal attribute "abs:xxx", don't try and make absolute.
            Document doc = iText.Html2pdf.Jsoup.Jsoup.Parse("<a abs:href='odd'>One</a>");
            iText.Html2pdf.Jsoup.Nodes.Element el = doc.Select("a").First();
            NUnit.Framework.Assert.IsTrue(el.HasAttr("abs:href"));
            NUnit.Framework.Assert.AreEqual("odd", el.Attr("abs:href"));
        }

        [NUnit.Framework.Test]
        public virtual void HandleAbsOnLocalhostFileUris() {
            Document doc = iText.Html2pdf.Jsoup.Jsoup.Parse("<a href='password'>One/a><a href='/var/log/messages'>Two</a>"
                , "file://localhost/etc/");
            iText.Html2pdf.Jsoup.Nodes.Element one = doc.Select("a").First();
            NUnit.Framework.Assert.AreEqual("file://localhost/etc/password", one.AbsUrl("href"));
        }

        [NUnit.Framework.Test]
        public virtual void HandlesAbsOnProtocolessAbsoluteUris() {
            Document doc1 = iText.Html2pdf.Jsoup.Jsoup.Parse("<a href='//example.net/foo'>One</a>", "http://example.com/"
                );
            Document doc2 = iText.Html2pdf.Jsoup.Jsoup.Parse("<a href='//example.net/foo'>One</a>", "https://example.com/"
                );
            iText.Html2pdf.Jsoup.Nodes.Element one = doc1.Select("a").First();
            iText.Html2pdf.Jsoup.Nodes.Element two = doc2.Select("a").First();
            NUnit.Framework.Assert.AreEqual("http://example.net/foo", one.AbsUrl("href"));
            NUnit.Framework.Assert.AreEqual("https://example.net/foo", two.AbsUrl("href"));
            Document doc3 = iText.Html2pdf.Jsoup.Jsoup.Parse("<img src=//www.google.com/images/errors/logo_sm.gif alt=Google>"
                , "https://google.com");
            NUnit.Framework.Assert.AreEqual("https://www.google.com/images/errors/logo_sm.gif", doc3.Select("img").Attr
                ("abs:src"));
        }

        /*
        Test for an issue with Java's abs URL handler.
        */
        [NUnit.Framework.Test]
        public virtual void AbsHandlesRelativeQuery() {
            Document doc = iText.Html2pdf.Jsoup.Jsoup.Parse("<a href='?foo'>One</a> <a href='bar.html?foo'>Two</a>", "http://jsoup.org/path/file?bar"
                );
            iText.Html2pdf.Jsoup.Nodes.Element a1 = doc.Select("a").First();
            NUnit.Framework.Assert.AreEqual("http://jsoup.org/path/file?foo", a1.AbsUrl("href"));
            iText.Html2pdf.Jsoup.Nodes.Element a2 = doc.Select("a")[1];
            NUnit.Framework.Assert.AreEqual("http://jsoup.org/path/bar.html?foo", a2.AbsUrl("href"));
        }

        [NUnit.Framework.Test]
        public virtual void AbsHandlesDotFromIndex() {
            Document doc = iText.Html2pdf.Jsoup.Jsoup.Parse("<a href='./one/two.html'>One</a>", "http://example.com");
            iText.Html2pdf.Jsoup.Nodes.Element a1 = doc.Select("a").First();
            NUnit.Framework.Assert.AreEqual("http://example.com/one/two.html", a1.AbsUrl("href"));
        }

        [NUnit.Framework.Test]
        public virtual void TestRemove() {
            Document doc = iText.Html2pdf.Jsoup.Jsoup.Parse("<p>One <span>two</span> three</p>");
            iText.Html2pdf.Jsoup.Nodes.Element p = doc.Select("p").First();
            p.ChildNode(0).Remove();
            NUnit.Framework.Assert.AreEqual("two three", p.Text());
            NUnit.Framework.Assert.AreEqual("<span>two</span> three", TextUtil.StripNewlines(p.Html()));
        }

        [NUnit.Framework.Test]
        public virtual void TestReplace() {
            Document doc = iText.Html2pdf.Jsoup.Jsoup.Parse("<p>One <span>two</span> three</p>");
            iText.Html2pdf.Jsoup.Nodes.Element p = doc.Select("p").First();
            iText.Html2pdf.Jsoup.Nodes.Element insert = doc.CreateElement("em").Text("foo");
            p.ChildNode(1).ReplaceWith(insert);
            NUnit.Framework.Assert.AreEqual("One <em>foo</em> three", p.Html());
        }

        [NUnit.Framework.Test]
        public virtual void OwnerDocument() {
            Document doc = iText.Html2pdf.Jsoup.Jsoup.Parse("<p>Hello");
            iText.Html2pdf.Jsoup.Nodes.Element p = doc.Select("p").First();
            NUnit.Framework.Assert.IsTrue(p.OwnerDocument() == doc);
            NUnit.Framework.Assert.IsTrue(doc.OwnerDocument() == doc);
            NUnit.Framework.Assert.IsNull(((iText.Html2pdf.Jsoup.Nodes.Element)doc.Parent()));
        }

        [NUnit.Framework.Test]
        public virtual void Before() {
            Document doc = iText.Html2pdf.Jsoup.Jsoup.Parse("<p>One <b>two</b> three</p>");
            iText.Html2pdf.Jsoup.Nodes.Element newNode = new iText.Html2pdf.Jsoup.Nodes.Element(iText.Html2pdf.Jsoup.Parser.Tag
                .ValueOf("em"), "");
            newNode.AppendText("four");
            doc.Select("b").First().Before(newNode);
            NUnit.Framework.Assert.AreEqual("<p>One <em>four</em><b>two</b> three</p>", doc.Body().Html());
            doc.Select("b").First().Before("<i>five</i>");
            NUnit.Framework.Assert.AreEqual("<p>One <em>four</em><i>five</i><b>two</b> three</p>", doc.Body().Html());
        }

        [NUnit.Framework.Test]
        public virtual void After() {
            Document doc = iText.Html2pdf.Jsoup.Jsoup.Parse("<p>One <b>two</b> three</p>");
            iText.Html2pdf.Jsoup.Nodes.Element newNode = new iText.Html2pdf.Jsoup.Nodes.Element(iText.Html2pdf.Jsoup.Parser.Tag
                .ValueOf("em"), "");
            newNode.AppendText("four");
            doc.Select("b").First().After(newNode);
            NUnit.Framework.Assert.AreEqual("<p>One <b>two</b><em>four</em> three</p>", doc.Body().Html());
            doc.Select("b").First().After("<i>five</i>");
            NUnit.Framework.Assert.AreEqual("<p>One <b>two</b><i>five</i><em>four</em> three</p>", doc.Body().Html());
        }

        [NUnit.Framework.Test]
        public virtual void Unwrap() {
            Document doc = iText.Html2pdf.Jsoup.Jsoup.Parse("<div>One <span>Two <b>Three</b></span> Four</div>");
            iText.Html2pdf.Jsoup.Nodes.Element span = doc.Select("span").First();
            Node twoText = span.ChildNode(0);
            Node node = span.Unwrap();
            NUnit.Framework.Assert.AreEqual("<div>One Two <b>Three</b> Four</div>", TextUtil.StripNewlines(doc.Body().
                Html()));
            NUnit.Framework.Assert.IsTrue(node is TextNode);
            NUnit.Framework.Assert.AreEqual("Two ", ((TextNode)node).Text());
            NUnit.Framework.Assert.AreEqual(node, twoText);
            NUnit.Framework.Assert.AreEqual(node.Parent(), doc.Select("div").First());
        }

        [NUnit.Framework.Test]
        public virtual void UnwrapNoChildren() {
            Document doc = iText.Html2pdf.Jsoup.Jsoup.Parse("<div>One <span></span> Two</div>");
            iText.Html2pdf.Jsoup.Nodes.Element span = doc.Select("span").First();
            Node node = span.Unwrap();
            NUnit.Framework.Assert.AreEqual("<div>One  Two</div>", TextUtil.StripNewlines(doc.Body().Html()));
            NUnit.Framework.Assert.IsTrue(node == null);
        }

        [NUnit.Framework.Test]
        public virtual void Traverse() {
            Document doc = iText.Html2pdf.Jsoup.Jsoup.Parse("<div><p>Hello</p></div><div>There</div>");
            StringBuilder accum = new StringBuilder();
            doc.Select("div").First().Traverse(new _NodeVisitor_264(accum));
            NUnit.Framework.Assert.AreEqual("<div><p><#text></#text></p></div>", accum.ToString());
        }

        private sealed class _NodeVisitor_264 : NodeVisitor {
            public _NodeVisitor_264(StringBuilder accum) {
                this.accum = accum;
            }

            public void Head(Node node, int depth) {
                accum.Append("<" + node.NodeName() + ">");
            }

            public void Tail(Node node, int depth) {
                accum.Append("</" + node.NodeName() + ">");
            }

            private readonly StringBuilder accum;
        }

        [NUnit.Framework.Test]
        public virtual void OrphanNodeReturnsNullForSiblingElements() {
            Node node = new iText.Html2pdf.Jsoup.Nodes.Element(iText.Html2pdf.Jsoup.Parser.Tag.ValueOf("p"), "");
            iText.Html2pdf.Jsoup.Nodes.Element el = new iText.Html2pdf.Jsoup.Nodes.Element(iText.Html2pdf.Jsoup.Parser.Tag
                .ValueOf("p"), "");
            NUnit.Framework.Assert.AreEqual(0, node.SiblingIndex());
            NUnit.Framework.Assert.AreEqual(0, node.SiblingNodes().Count);
            NUnit.Framework.Assert.IsNull(node.PreviousSibling());
            NUnit.Framework.Assert.IsNull(node.NextSibling());
            NUnit.Framework.Assert.AreEqual(0, el.SiblingElements().Count);
            NUnit.Framework.Assert.IsNull(el.PreviousElementSibling());
            NUnit.Framework.Assert.IsNull(el.NextElementSibling());
        }

        [NUnit.Framework.Test]
        public virtual void NodeIsNotASiblingOfItself() {
            Document doc = iText.Html2pdf.Jsoup.Jsoup.Parse("<div><p>One<p>Two<p>Three</div>");
            iText.Html2pdf.Jsoup.Nodes.Element p2 = doc.Select("p")[1];
            NUnit.Framework.Assert.AreEqual("Two", p2.Text());
            IList<Node> nodes = p2.SiblingNodes();
            NUnit.Framework.Assert.AreEqual(2, nodes.Count);
            NUnit.Framework.Assert.AreEqual("<p>One</p>", nodes[0].OuterHtml());
            NUnit.Framework.Assert.AreEqual("<p>Three</p>", nodes[1].OuterHtml());
        }

        [NUnit.Framework.Test]
        public virtual void ChildNodesCopy() {
            Document doc = iText.Html2pdf.Jsoup.Jsoup.Parse("<div id=1>Text 1 <p>One</p> Text 2 <p>Two<p>Three</div><div id=2>"
                );
            iText.Html2pdf.Jsoup.Nodes.Element div1 = doc.Select("#1").First();
            iText.Html2pdf.Jsoup.Nodes.Element div2 = doc.Select("#2").First();
            IList<Node> divChildren = div1.ChildNodesCopy();
            NUnit.Framework.Assert.AreEqual(5, divChildren.Count);
            TextNode tn1 = (TextNode)div1.ChildNode(0);
            TextNode tn2 = (TextNode)divChildren[0];
            tn2.Text("Text 1 updated");
            NUnit.Framework.Assert.AreEqual("Text 1 ", tn1.Text());
            div2.InsertChildren(-1, divChildren);
            NUnit.Framework.Assert.AreEqual("<div id=\"1\">Text 1 <p>One</p> Text 2 <p>Two</p><p>Three</p></div><div id=\"2\">Text 1 updated"
                 + "<p>One</p> Text 2 <p>Two</p><p>Three</p></div>", TextUtil.StripNewlines(doc.Body().Html()));
        }

        [NUnit.Framework.Test]
        public virtual void SupportsClone() {
            Document doc = iText.Html2pdf.Jsoup.Jsoup.Parse("<div class=foo>Text</div>");
            iText.Html2pdf.Jsoup.Nodes.Element el = doc.Select("div").First();
            NUnit.Framework.Assert.IsTrue(el.HasClass("foo"));
            iText.Html2pdf.Jsoup.Nodes.Element elClone = ((Document)doc.Clone()).Select("div").First();
            NUnit.Framework.Assert.IsTrue(elClone.HasClass("foo"));
            NUnit.Framework.Assert.IsTrue(elClone.Text().Equals("Text"));
            el.RemoveClass("foo");
            el.Text("None");
            NUnit.Framework.Assert.IsFalse(el.HasClass("foo"));
            NUnit.Framework.Assert.IsTrue(elClone.HasClass("foo"));
            NUnit.Framework.Assert.IsTrue(el.Text().Equals("None"));
            NUnit.Framework.Assert.IsTrue(elClone.Text().Equals("Text"));
        }
    }
}
