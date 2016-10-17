using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Javax.Xml.Parsers;
using Javax.Xml.Transform;
using Javax.Xml.Transform.Dom;
using Javax.Xml.Transform.Stream;
using Org.Jsoup.Nodes;
using Org.Jsoup.Select;
using Org.W3c.Dom;

namespace Org.Jsoup.Helper {
    /// <summary>
    /// Helper class to transform a
    /// <see cref="Org.Jsoup.Nodes.Document"/>
    /// to a
    /// <see cref="System.Xml.XmlDocument">XmlDocument</see>
    /// ,
    /// for integration with toolsets that use the W3C DOM.
    /// <p>
    /// This class is currently <b>experimental</b>, please provide feedback on utility and any problems experienced.
    /// </p>
    /// </summary>
    public class W3CDom {
        protected internal DocumentBuilderFactory factory = DocumentBuilderFactory.NewInstance();

        /// <summary>Convert a jsoup Document to a W3C Document.</summary>
        /// <param name="in">jsoup doc</param>
        /// <returns>w3c doc</returns>
        public virtual XmlDocument FromJsoup(Document @in) {
            Validate.NotNull(@in);
            DocumentBuilder builder;
            try {
                //set the factory to be namespace-aware
                factory.SetNamespaceAware(true);
                builder = factory.NewDocumentBuilder();
                XmlDocument @out = builder.NewDocument();
                Convert(@in, @out);
                return @out;
            }
            catch (ParserConfigurationException e) {
                throw new InvalidOperationException(e);
            }
        }

        /// <summary>Converts a jsoup document into the provided W3C Document.</summary>
        /// <remarks>
        /// Converts a jsoup document into the provided W3C Document. If required, you can set options on the output document
        /// before converting.
        /// </remarks>
        /// <param name="in">jsoup doc</param>
        /// <param name="out">w3c doc</param>
        /// <seealso cref="FromJsoup(Org.Jsoup.Nodes.Document)"/>
        public virtual void Convert(Document @in, XmlDocument @out) {
            if (!Org.Jsoup.Helper.StringUtil.IsBlank(@in.Location())) {
                @out.SetDocumentURI(@in.Location());
            }
            Element rootEl = @in.Child(0);
            // skip the #root node
            NodeTraversor traversor = new NodeTraversor(new W3CDom.W3CBuilder(@out));
            traversor.Traverse(rootEl);
        }

        /// <summary>Implements the conversion by walking the input.</summary>
        protected internal class W3CBuilder : NodeVisitor {
            private const String xmlnsKey = "xmlns";

            private const String xmlnsPrefix = "xmlns:";

            private readonly XmlDocument doc;

            private readonly Dictionary<String, String> namespaces = new Dictionary<String, String>();

            private XmlElement dest;

            public W3CBuilder(XmlDocument doc) {
                // prefix => urn
                this.doc = doc;
            }

            public virtual void Head(Node source, int depth) {
                if (source is Element) {
                    Element sourceEl = (Element)source;
                    String prefix = UpdateNamespaces(sourceEl);
                    String @namespace = namespaces.Get(prefix);
                    XmlElement el = doc.CreateElementNS(@namespace, sourceEl.TagName());
                    CopyAttributes(sourceEl, el);
                    if (dest == null) {
                        // sets up the root
                        doc.AppendChild(el);
                    }
                    else {
                        dest.AppendChild(el);
                    }
                    dest = el;
                }
                else {
                    // descend
                    if (source is TextNode) {
                        TextNode sourceText = (TextNode)source;
                        Text text = doc.CreateTextNode(sourceText.GetWholeText());
                        dest.AppendChild(text);
                    }
                    else {
                        if (source is Comment) {
                            Comment sourceComment = (Comment)source;
                            Comment comment = doc.CreateComment(sourceComment.GetData());
                            dest.AppendChild(comment);
                        }
                        else {
                            if (source is DataNode) {
                                DataNode sourceData = (DataNode)source;
                                Text node = doc.CreateTextNode(sourceData.GetWholeData());
                                dest.AppendChild(node);
                            }
                        }
                    }
                }
            }

            // unhandled
            public virtual void Tail(Node source, int depth) {
                if (source is Element && dest.GetParentNode() is XmlElement) {
                    dest = (XmlElement)dest.GetParentNode();
                }
            }

            // undescend. cromulent.
            private void CopyAttributes(Node source, XmlElement el) {
                foreach (Org.Jsoup.Nodes.Attribute attribute in source.Attributes()) {
                    el.SetAttribute(attribute.Key, attribute.Value);
                }
            }

            /// <summary>Finds any namespaces defined in this element.</summary>
            /// <remarks>Finds any namespaces defined in this element. Returns any tag prefix.</remarks>
            private String UpdateNamespaces(Element el) {
                // scan the element for namespace declarations
                // like: xmlns="blah" or xmlns:prefix="blah"
                Attributes attributes = el.Attributes();
                foreach (Org.Jsoup.Nodes.Attribute attr in attributes) {
                    String key = attr.Key;
                    String prefix;
                    if (key.Equals(xmlnsKey)) {
                        prefix = "";
                    }
                    else {
                        if (key.StartsWith(xmlnsPrefix)) {
                            prefix = key.Substring(xmlnsPrefix.Length);
                        }
                        else {
                            continue;
                        }
                    }
                    namespaces[prefix] = attr.Value;
                }
                // get the element prefix if any
                int pos = el.TagName().IndexOf(":");
                return pos > 0 ? el.TagName().JSubstring(0, pos) : "";
            }
        }

        /// <summary>Serialize a W3C document to a String.</summary>
        /// <param name="doc">Document</param>
        /// <returns>Document as string</returns>
        public virtual String AsString(XmlDocument doc) {
            try {
                DOMSource domSource = new DOMSource(doc);
                StringWriter writer = new StringWriter();
                StreamResult result = new StreamResult(writer);
                TransformerFactory tf = TransformerFactory.NewInstance();
                Transformer transformer = tf.NewTransformer();
                transformer.Transform(domSource, result);
                return writer.ToString();
            }
            catch (TransformerException e) {
                throw new InvalidOperationException(e);
            }
        }
    }
}
