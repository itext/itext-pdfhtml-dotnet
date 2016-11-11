using System;
using Org.Jsoup.Helper;
using Org.Jsoup.Nodes;
using Org.Jsoup.Select;

namespace Org.Jsoup.Safety {
    /// <summary>The whitelist based HTML cleaner.</summary>
    /// <remarks>
    /// The whitelist based HTML cleaner. Use to ensure that end-user provided HTML contains only the elements and attributes
    /// that you are expecting; no junk, and no cross-site scripting attacks!
    /// <p>
    /// The HTML cleaner parses the input as HTML and then runs it through a white-list, so the output HTML can only contain
    /// HTML that is allowed by the whitelist.
    /// </p>
    /// <p>
    /// It is assumed that the input HTML is a body fragment; the clean methods only pull from the source's body, and the
    /// canned white-lists only allow body contained tags.
    /// </p>
    /// <p>
    /// Rather than interacting directly with a Cleaner object, generally see the
    /// <c>clean</c>
    /// methods in
    /// <see cref="Org.Jsoup.Jsoup"/>
    /// .
    /// </p>
    /// </remarks>
    public class Cleaner {
        private Whitelist whitelist;

        /// <summary>Create a new cleaner, that sanitizes documents using the supplied whitelist.</summary>
        /// <param name="whitelist">white-list to clean with</param>
        public Cleaner(Whitelist whitelist) {
            Validate.NotNull(whitelist);
            this.whitelist = whitelist;
        }

        /// <summary>Creates a new, clean document, from the original dirty document, containing only elements allowed by the whitelist.
        ///     </summary>
        /// <remarks>
        /// Creates a new, clean document, from the original dirty document, containing only elements allowed by the whitelist.
        /// The original document is not modified. Only elements from the dirt document's <code>body</code> are used.
        /// </remarks>
        /// <param name="dirtyDocument">Untrusted base document to clean.</param>
        /// <returns>cleaned document.</returns>
        public virtual Document Clean(Document dirtyDocument) {
            Validate.NotNull(dirtyDocument);
            Document clean = Document.CreateShell(dirtyDocument.BaseUri());
            if (dirtyDocument.Body() != null) {
                // frameset documents won't have a body. the clean doc will have empty body.
                CopySafeNodes(dirtyDocument.Body(), clean.Body());
            }
            return clean;
        }

        /// <summary>Determines if the input document is valid, against the whitelist.</summary>
        /// <remarks>
        /// Determines if the input document is valid, against the whitelist. It is considered valid if all the tags and attributes
        /// in the input HTML are allowed by the whitelist.
        /// <p>
        /// This method can be used as a validator for user input forms. An invalid document will still be cleaned successfully
        /// using the
        /// <see cref="Clean(Org.Jsoup.Nodes.Document)"/>
        /// document. If using as a validator, it is recommended to still clean the document
        /// to ensure enforced attributes are set correctly, and that the output is tidied.
        /// </p>
        /// </remarks>
        /// <param name="dirtyDocument">document to test</param>
        /// <returns>true if no tags or attributes need to be removed; false if they do</returns>
        public virtual bool IsValid(Document dirtyDocument) {
            Validate.NotNull(dirtyDocument);
            Document clean = Document.CreateShell(dirtyDocument.BaseUri());
            int numDiscarded = CopySafeNodes(dirtyDocument.Body(), clean.Body());
            return numDiscarded == 0;
        }

        /// <summary>Iterates the input and copies trusted nodes (tags, attributes, text) into the destination.</summary>
        private sealed class CleaningVisitor : NodeVisitor {
            internal int numDiscarded = 0;

            internal readonly Element root;

            internal Element destination;

            internal CleaningVisitor(Cleaner _enclosing, Element root, Element destination) {
                this._enclosing = _enclosing;
                // current element to append nodes to
                this.root = root;
                this.destination = destination;
            }

            public void Head(Node source, int depth) {
                if (source is Element) {
                    Element sourceEl = (Element)source;
                    if (this._enclosing.whitelist.IsSafeTag(sourceEl.TagName())) {
                        // safe, clone and copy safe attrs
                        Cleaner.ElementMeta meta = this._enclosing.CreateSafeElement(sourceEl);
                        Element destChild = meta.el;
                        this.destination.AppendChild(destChild);
                        this.numDiscarded += meta.numAttribsDiscarded;
                        this.destination = destChild;
                    }
                    else {
                        if (source != this.root) {
                            // not a safe tag, so don't add. don't count root against discarded.
                            this.numDiscarded++;
                        }
                    }
                }
                else {
                    if (source is TextNode) {
                        TextNode sourceText = (TextNode)source;
                        TextNode destText = new TextNode(sourceText.GetWholeText(), source.BaseUri());
                        this.destination.AppendChild(destText);
                    }
                    else {
                        if (source is DataNode && this._enclosing.whitelist.IsSafeTag(source.Parent().NodeName())) {
                            DataNode sourceData = (DataNode)source;
                            DataNode destData = new DataNode(sourceData.GetWholeData(), source.BaseUri());
                            this.destination.AppendChild(destData);
                        }
                        else {
                            // else, we don't care about comments, xml proc instructions, etc
                            this.numDiscarded++;
                        }
                    }
                }
            }

            public void Tail(Node source, int depth) {
                if (source is Element && this._enclosing.whitelist.IsSafeTag(source.NodeName())) {
                    this.destination = ((Element)this.destination.Parent());
                }
            }

            private readonly Cleaner _enclosing;
            // would have descended, so pop destination stack
        }

        private int CopySafeNodes(Element source, Element dest) {
            Cleaner.CleaningVisitor cleaningVisitor = new Cleaner.CleaningVisitor(this, source, dest);
            NodeTraversor traversor = new NodeTraversor(cleaningVisitor);
            traversor.Traverse(source);
            return cleaningVisitor.numDiscarded;
        }

        private Cleaner.ElementMeta CreateSafeElement(Element sourceEl) {
            String sourceTag = sourceEl.TagName();
            Attributes destAttrs = new Attributes();
            Element dest = new Element(Org.Jsoup.Parser.Tag.ValueOf(sourceTag), sourceEl.BaseUri(), destAttrs);
            int numDiscarded = 0;
            Attributes sourceAttrs = sourceEl.Attributes();
            foreach (Org.Jsoup.Nodes.Attribute sourceAttr in sourceAttrs) {
                if (whitelist.IsSafeAttribute(sourceTag, sourceEl, sourceAttr)) {
                    destAttrs.Put(sourceAttr);
                }
                else {
                    numDiscarded++;
                }
            }
            Attributes enforcedAttrs = whitelist.GetEnforcedAttributes(sourceTag);
            destAttrs.AddAll(enforcedAttrs);
            return new Cleaner.ElementMeta(dest, numDiscarded);
        }

        private class ElementMeta {
            internal Element el;

            internal int numAttribsDiscarded;

            internal ElementMeta(Element el, int numAttribsDiscarded) {
                this.el = el;
                this.numAttribsDiscarded = numAttribsDiscarded;
            }
        }
    }
}
