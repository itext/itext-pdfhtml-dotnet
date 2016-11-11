using System;
using System.Collections.Generic;
using System.Text;
using Org.Jsoup;
using Org.Jsoup.Select;

namespace Org.Jsoup.Nodes {
    /// <summary>Tests Nodes</summary>
    /// <author>Jonathan Hedley, jonathan@hedley.net</author>
    public class NodeTest {
        [NUnit.Framework.Test]
        public virtual void HandlesBaseUri() {
            Org.Jsoup.Parser.Tag tag = Org.Jsoup.Parser.Tag.ValueOf("a");
            Attributes attribs = new Attributes();
            attribs.Put("relHref", "/foo");
            attribs.Put("absHref", "http://bar/qux");
            Element noBase = new Element(tag, "", attribs);
            NUnit.Framework.Assert.AreEqual("", noBase.AbsUrl("relHref"));
            // with no base, should NOT fallback to href attrib, whatever it is
            NUnit.Framework.Assert.AreEqual("http://bar/qux", noBase.AbsUrl("absHref"));
            // no base but valid attrib, return attrib
            Element withBase = new Element(tag, "http://foo/", attribs);
            NUnit.Framework.Assert.AreEqual("http://foo/foo", withBase.AbsUrl("relHref"));
            // construct abs from base + rel
            NUnit.Framework.Assert.AreEqual("http://bar/qux", withBase.AbsUrl("absHref"));
            // href is abs, so returns that
            NUnit.Framework.Assert.AreEqual("", withBase.AbsUrl("noval"));
            Element dodgyBase = new Element(tag, "wtf://no-such-protocol/", attribs);
            NUnit.Framework.Assert.AreEqual("http://bar/qux", dodgyBase.AbsUrl("absHref"));
        }

        // base fails, but href good, so get that
        // base fails, only rel href, so return nothing
        [NUnit.Framework.Test]
        public virtual void SetBaseUriIsRecursive() {
            Document doc = Org.Jsoup.Jsoup.Parse("<div><p></p></div>");
            String baseUri = "http://jsoup.org";
            doc.SetBaseUri(baseUri);
            NUnit.Framework.Assert.AreEqual(baseUri, doc.BaseUri());
            NUnit.Framework.Assert.AreEqual(baseUri, doc.Select("div").First().BaseUri());
            NUnit.Framework.Assert.AreEqual(baseUri, doc.Select("p").First().BaseUri());
        }

        [NUnit.Framework.Test]
        public virtual void HandlesAbsPrefix() {
            Document doc = Org.Jsoup.Jsoup.Parse("<a href=/foo>Hello</a>", "http://jsoup.org/");
            Element a = doc.Select("a").First();
            NUnit.Framework.Assert.AreEqual("/foo", a.Attr("href"));
            NUnit.Framework.Assert.AreEqual("http://jsoup.org/foo", a.Attr("abs:href"));
            NUnit.Framework.Assert.IsTrue(a.HasAttr("abs:href"));
        }

        [NUnit.Framework.Test]
        public virtual void HandlesAbsOnImage() {
            Document doc = Org.Jsoup.Jsoup.Parse("<p><img src=\"/rez/osi_logo.png\" /></p>", "http://jsoup.org/");
            Element img = doc.Select("img").First();
            NUnit.Framework.Assert.AreEqual("http://jsoup.org/rez/osi_logo.png", img.Attr("abs:src"));
            NUnit.Framework.Assert.AreEqual(img.AbsUrl("src"), img.Attr("abs:src"));
        }

        [NUnit.Framework.Test]
        public virtual void HandlesAbsPrefixOnHasAttr() {
            // 1: no abs url; 2: has abs url
            Document doc = Org.Jsoup.Jsoup.Parse("<a id=1 href='/foo'>One</a> <a id=2 href='http://jsoup.org/'>Two</a>"
                );
            Element one = doc.Select("#1").First();
            Element two = doc.Select("#2").First();
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
            Document doc = Org.Jsoup.Jsoup.Parse("<a abs:href='odd'>One</a>");
            Element el = doc.Select("a").First();
            NUnit.Framework.Assert.IsTrue(el.HasAttr("abs:href"));
            NUnit.Framework.Assert.AreEqual("odd", el.Attr("abs:href"));
        }

        [NUnit.Framework.Test]
        public virtual void HandleAbsOnLocalhostFileUris() {
            Document doc = Org.Jsoup.Jsoup.Parse("<a href='password'>One/a><a href='/var/log/messages'>Two</a>", "file://localhost/etc/"
                );
            Element one = doc.Select("a").First();
            NUnit.Framework.Assert.AreEqual("file://localhost/etc/password", one.AbsUrl("href"));
        }

        [NUnit.Framework.Test]
        public virtual void HandlesAbsOnProtocolessAbsoluteUris() {
            Document doc1 = Org.Jsoup.Jsoup.Parse("<a href='//example.net/foo'>One</a>", "http://example.com/");
            Document doc2 = Org.Jsoup.Jsoup.Parse("<a href='//example.net/foo'>One</a>", "https://example.com/");
            Element one = doc1.Select("a").First();
            Element two = doc2.Select("a").First();
            NUnit.Framework.Assert.AreEqual("http://example.net/foo", one.AbsUrl("href"));
            NUnit.Framework.Assert.AreEqual("https://example.net/foo", two.AbsUrl("href"));
            Document doc3 = Org.Jsoup.Jsoup.Parse("<img src=//www.google.com/images/errors/logo_sm.gif alt=Google>", "https://google.com"
                );
            NUnit.Framework.Assert.AreEqual("https://www.google.com/images/errors/logo_sm.gif", doc3.Select("img").Attr
                ("abs:src"));
        }

        /*
        Test for an issue with Java's abs URL handler.
        */
        [NUnit.Framework.Test]
        public virtual void AbsHandlesRelativeQuery() {
            Document doc = Org.Jsoup.Jsoup.Parse("<a href='?foo'>One</a> <a href='bar.html?foo'>Two</a>", "http://jsoup.org/path/file?bar"
                );
            Element a1 = doc.Select("a").First();
            NUnit.Framework.Assert.AreEqual("http://jsoup.org/path/file?foo", a1.AbsUrl("href"));
            Element a2 = doc.Select("a")[1];
            NUnit.Framework.Assert.AreEqual("http://jsoup.org/path/bar.html?foo", a2.AbsUrl("href"));
        }

        [NUnit.Framework.Test]
        public virtual void AbsHandlesDotFromIndex() {
            Document doc = Org.Jsoup.Jsoup.Parse("<a href='./one/two.html'>One</a>", "http://example.com");
            Element a1 = doc.Select("a").First();
            NUnit.Framework.Assert.AreEqual("http://example.com/one/two.html", a1.AbsUrl("href"));
        }

        [NUnit.Framework.Test]
        public virtual void TestRemove() {
            Document doc = Org.Jsoup.Jsoup.Parse("<p>One <span>two</span> three</p>");
            Element p = doc.Select("p").First();
            p.ChildNode(0).Remove();
            NUnit.Framework.Assert.AreEqual("two three", p.Text());
            NUnit.Framework.Assert.AreEqual("<span>two</span> three", TextUtil.StripNewlines(p.Html()));
        }

        [NUnit.Framework.Test]
        public virtual void TestReplace() {
            Document doc = Org.Jsoup.Jsoup.Parse("<p>One <span>two</span> three</p>");
            Element p = doc.Select("p").First();
            Element insert = doc.CreateElement("em").Text("foo");
            p.ChildNode(1).ReplaceWith(insert);
            NUnit.Framework.Assert.AreEqual("One <em>foo</em> three", p.Html());
        }

        [NUnit.Framework.Test]
        public virtual void OwnerDocument() {
            Document doc = Org.Jsoup.Jsoup.Parse("<p>Hello");
            Element p = doc.Select("p").First();
            NUnit.Framework.Assert.IsTrue(p.OwnerDocument() == doc);
            NUnit.Framework.Assert.IsTrue(doc.OwnerDocument() == doc);
            NUnit.Framework.Assert.IsNull(((Element)doc.Parent()));
        }

        [NUnit.Framework.Test]
        public virtual void Before() {
            Document doc = Org.Jsoup.Jsoup.Parse("<p>One <b>two</b> three</p>");
            Element newNode = new Element(Org.Jsoup.Parser.Tag.ValueOf("em"), "");
            newNode.AppendText("four");
            doc.Select("b").First().Before(newNode);
            NUnit.Framework.Assert.AreEqual("<p>One <em>four</em><b>two</b> three</p>", doc.Body().Html());
            doc.Select("b").First().Before("<i>five</i>");
            NUnit.Framework.Assert.AreEqual("<p>One <em>four</em><i>five</i><b>two</b> three</p>", doc.Body().Html());
        }

        [NUnit.Framework.Test]
        public virtual void After() {
            Document doc = Org.Jsoup.Jsoup.Parse("<p>One <b>two</b> three</p>");
            Element newNode = new Element(Org.Jsoup.Parser.Tag.ValueOf("em"), "");
            newNode.AppendText("four");
            doc.Select("b").First().After(newNode);
            NUnit.Framework.Assert.AreEqual("<p>One <b>two</b><em>four</em> three</p>", doc.Body().Html());
            doc.Select("b").First().After("<i>five</i>");
            NUnit.Framework.Assert.AreEqual("<p>One <b>two</b><i>five</i><em>four</em> three</p>", doc.Body().Html());
        }

        [NUnit.Framework.Test]
        public virtual void Unwrap() {
            Document doc = Org.Jsoup.Jsoup.Parse("<div>One <span>Two <b>Three</b></span> Four</div>");
            Element span = doc.Select("span").First();
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
            Document doc = Org.Jsoup.Jsoup.Parse("<div>One <span></span> Two</div>");
            Element span = doc.Select("span").First();
            Node node = span.Unwrap();
            NUnit.Framework.Assert.AreEqual("<div>One  Two</div>", TextUtil.StripNewlines(doc.Body().Html()));
            NUnit.Framework.Assert.IsTrue(node == null);
        }

        [NUnit.Framework.Test]
        public virtual void Traverse() {
            Document doc = Org.Jsoup.Jsoup.Parse("<div><p>Hello</p></div><div>There</div>");
            StringBuilder accum = new StringBuilder();
            doc.Select("div").First().Traverse(new _NodeVisitor_221(accum));
            NUnit.Framework.Assert.AreEqual("<div><p><#text></#text></p></div>", accum.ToString());
        }

        private sealed class _NodeVisitor_221 : NodeVisitor {
            public _NodeVisitor_221(StringBuilder accum) {
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
            Node node = new Element(Org.Jsoup.Parser.Tag.ValueOf("p"), "");
            Element el = new Element(Org.Jsoup.Parser.Tag.ValueOf("p"), "");
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
            Document doc = Org.Jsoup.Jsoup.Parse("<div><p>One<p>Two<p>Three</div>");
            Element p2 = doc.Select("p")[1];
            NUnit.Framework.Assert.AreEqual("Two", p2.Text());
            IList<Node> nodes = p2.SiblingNodes();
            NUnit.Framework.Assert.AreEqual(2, nodes.Count);
            NUnit.Framework.Assert.AreEqual("<p>One</p>", nodes[0].OuterHtml());
            NUnit.Framework.Assert.AreEqual("<p>Three</p>", nodes[1].OuterHtml());
        }

        [NUnit.Framework.Test]
        public virtual void ChildNodesCopy() {
            Document doc = Org.Jsoup.Jsoup.Parse("<div id=1>Text 1 <p>One</p> Text 2 <p>Two<p>Three</div><div id=2>");
            Element div1 = doc.Select("#1").First();
            Element div2 = doc.Select("#2").First();
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
            Document doc = Org.Jsoup.Jsoup.Parse("<div class=foo>Text</div>");
            Element el = doc.Select("div").First();
            NUnit.Framework.Assert.IsTrue(el.HasClass("foo"));
            Element elClone = ((Document)doc.Clone()).Select("div").First();
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
