using System;
using System.Collections.Generic;
using System.Text;
using Org.Jsoup;
using Org.Jsoup.Helper;
using Org.Jsoup.Select;
using iText.IO.Util;

namespace Org.Jsoup.Nodes {
    /// <summary>The base, abstract Node model.</summary>
    /// <remarks>The base, abstract Node model. Elements, Documents, Comments etc are all Node instances.</remarks>
    /// <author>Jonathan Hedley, jonathan@hedley.net</author>
    public abstract class Node : ICloneable {
        private static readonly IList<Org.Jsoup.Nodes.Node> EMPTY_NODES = JavaCollectionsUtil.EmptyList<Org.Jsoup.Nodes.Node
            >();

        internal Org.Jsoup.Nodes.Node parentNode;

        internal IList<Org.Jsoup.Nodes.Node> childNodes;

        internal Org.Jsoup.Nodes.Attributes attributes;

        internal String baseUri;

        internal int siblingIndex;

        /// <summary>Create a new Node.</summary>
        /// <param name="baseUri">base URI</param>
        /// <param name="attributes">attributes (not null, but may be empty)</param>
        protected internal Node(String baseUri, Org.Jsoup.Nodes.Attributes attributes) {
            Validate.NotNull(baseUri);
            Validate.NotNull(attributes);
            childNodes = EMPTY_NODES;
            this.baseUri = baseUri.Trim();
            this.attributes = attributes;
        }

        protected internal Node(String baseUri)
            : this(baseUri, new Org.Jsoup.Nodes.Attributes()) {
        }

        /// <summary>Default constructor.</summary>
        /// <remarks>Default constructor. Doesn't setup base uri, children, or attributes; use with caution.</remarks>
        protected internal Node() {
            childNodes = EMPTY_NODES;
            attributes = null;
        }

        /// <summary>Get the node name of this node.</summary>
        /// <remarks>Get the node name of this node. Use for debugging purposes and not logic switching (for that, use instanceof).
        ///     </remarks>
        /// <returns>node name</returns>
        public abstract String NodeName();

        /// <summary>Get an attribute's value by its key.</summary>
        /// <remarks>
        /// Get an attribute's value by its key.
        /// <p>
        /// To get an absolute URL from an attribute that may be a relative URL, prefix the key with <code><b>abs</b></code>,
        /// which is a shortcut to the
        /// <see cref="AbsUrl(System.String)"/>
        /// method.
        /// </p>
        /// E.g.:
        /// <blockquote><code>String url = a.attr("abs:href");</code></blockquote>
        /// </remarks>
        /// <param name="attributeKey">The attribute key.</param>
        /// <returns>The attribute, or empty string if not present (to avoid nulls).</returns>
        /// <seealso cref="Attributes()"/>
        /// <seealso cref="HasAttr(System.String)"/>
        /// <seealso cref="AbsUrl(System.String)"/>
        public virtual String Attr(String attributeKey) {
            Validate.NotNull(attributeKey);
            if (attributes.HasKey(attributeKey)) {
                return attributes.Get(attributeKey);
            }
            else {
                if (attributeKey.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("abs:")) {
                    return AbsUrl(attributeKey.Substring("abs:".Length));
                }
                else {
                    return "";
                }
            }
        }

        /// <summary>Get all of the element's attributes.</summary>
        /// <returns>attributes (which implements iterable, in same order as presented in original HTML).</returns>
        public virtual Org.Jsoup.Nodes.Attributes Attributes() {
            return attributes;
        }

        /// <summary>Set an attribute (key=value).</summary>
        /// <remarks>Set an attribute (key=value). If the attribute already exists, it is replaced.</remarks>
        /// <param name="attributeKey">The attribute key.</param>
        /// <param name="attributeValue">The attribute value.</param>
        /// <returns>this (for chaining)</returns>
        public virtual Org.Jsoup.Nodes.Node Attr(String attributeKey, String attributeValue) {
            attributes.Put(attributeKey, attributeValue);
            return this;
        }

        /// <summary>Test if this element has an attribute.</summary>
        /// <param name="attributeKey">The attribute key to check.</param>
        /// <returns>true if the attribute exists, false if not.</returns>
        public virtual bool HasAttr(String attributeKey) {
            Validate.NotNull(attributeKey);
            if (attributeKey.StartsWith("abs:")) {
                String key = attributeKey.Substring("abs:".Length);
                if (attributes.HasKey(key) && !AbsUrl(key).Equals("")) {
                    return true;
                }
            }
            return attributes.HasKey(attributeKey);
        }

        /// <summary>Remove an attribute from this element.</summary>
        /// <param name="attributeKey">The attribute to remove.</param>
        /// <returns>this (for chaining)</returns>
        public virtual Org.Jsoup.Nodes.Node RemoveAttr(String attributeKey) {
            Validate.NotNull(attributeKey);
            attributes.Remove(attributeKey);
            return this;
        }

        /// <summary>Get the base URI of this node.</summary>
        /// <returns>base URI</returns>
        public virtual String BaseUri() {
            return baseUri;
        }

        /// <summary>Update the base URI of this node and all of its descendants.</summary>
        /// <param name="baseUri">base URI to set</param>
        public virtual void SetBaseUri(String baseUri) {
            Validate.NotNull(baseUri);
            Traverse(new _NodeVisitor_146(baseUri));
        }

        private sealed class _NodeVisitor_146 : NodeVisitor {
            public _NodeVisitor_146(String baseUri) {
                this.baseUri = baseUri;
            }

            public void Head(Org.Jsoup.Nodes.Node node, int depth) {
                node.baseUri = baseUri;
            }

            public void Tail(Org.Jsoup.Nodes.Node node, int depth) {
            }

            private readonly String baseUri;
        }

        /// <summary>Get an absolute URL from a URL attribute that may be relative (i.e.</summary>
        /// <remarks>
        /// Get an absolute URL from a URL attribute that may be relative (i.e. an <code>&lt;a href&gt;</code> or
        /// <code>&lt;img src&gt;</code>).
        /// <p>
        /// E.g.: <code>String absUrl = linkEl.absUrl("href");</code>
        /// </p>
        /// <p>
        /// If the attribute value is already absolute (i.e. it starts with a protocol, like
        /// <code>http://</code> or <code>https://</code> etc), and it successfully parses as a URL, the attribute is
        /// returned directly. Otherwise, it is treated as a URL relative to the element's
        /// <see cref="baseUri"/>
        /// , and made
        /// absolute using that.
        /// </p>
        /// <p>
        /// As an alternate, you can use the
        /// <see cref="Attr(System.String)"/>
        /// method with the <code>abs:</code> prefix, e.g.:
        /// <code>String absUrl = linkEl.attr("abs:href");</code>
        /// </p>
        /// </remarks>
        /// <param name="attributeKey">The attribute key</param>
        /// <returns>
        /// An absolute URL if one could be made, or an empty string (not null) if the attribute was missing or
        /// could not be made successfully into a URL.
        /// </returns>
        /// <seealso cref="Attr(System.String)"/>
        /// <seealso cref="System.Uri.URL(System.Uri, System.String)"/>
        public virtual String AbsUrl(String attributeKey) {
            Validate.NotEmpty(attributeKey);
            if (!HasAttr(attributeKey)) {
                return "";
            }
            else {
                // nothing to make absolute with
                return Org.Jsoup.Helper.StringUtil.Resolve(baseUri, Attr(attributeKey));
            }
        }

        /// <summary>Get a child node by its 0-based index.</summary>
        /// <param name="index">index of child node</param>
        /// <returns>
        /// the child node at this index. Throws a
        /// <c>IndexOutOfBoundsException</c>
        /// if the index is out of bounds.
        /// </returns>
        public virtual Org.Jsoup.Nodes.Node ChildNode(int index) {
            return childNodes[index];
        }

        /// <summary>Get this node's children.</summary>
        /// <remarks>
        /// Get this node's children. Presented as an unmodifiable list: new children can not be added, but the child nodes
        /// themselves can be manipulated.
        /// </remarks>
        /// <returns>list of children. If no children, returns an empty list.</returns>
        public virtual IList<Org.Jsoup.Nodes.Node> ChildNodes() {
            return JavaCollectionsUtil.UnmodifiableList(childNodes);
        }

        /// <summary>Returns a deep copy of this node's children.</summary>
        /// <remarks>
        /// Returns a deep copy of this node's children. Changes made to these nodes will not be reflected in the original
        /// nodes
        /// </remarks>
        /// <returns>a deep copy of this node's children</returns>
        public virtual IList<Org.Jsoup.Nodes.Node> ChildNodesCopy() {
            IList<Org.Jsoup.Nodes.Node> children = new List<Org.Jsoup.Nodes.Node>(childNodes.Count);
            foreach (Org.Jsoup.Nodes.Node node in childNodes) {
                children.Add((Org.Jsoup.Nodes.Node)node.Clone());
            }
            return children;
        }

        /// <summary>Get the number of child nodes that this node holds.</summary>
        /// <returns>the number of child nodes that this node holds.</returns>
        public int ChildNodeSize() {
            return childNodes.Count;
        }

        protected internal virtual Org.Jsoup.Nodes.Node[] ChildNodesAsArray() {
            return childNodes.ToArray(new Org.Jsoup.Nodes.Node[ChildNodeSize()]);
        }

        /// <summary>Gets this node's parent node.</summary>
        /// <returns>parent node; or null if no parent.</returns>
        public virtual Org.Jsoup.Nodes.Node Parent() {
            return parentNode;
        }

        /// <summary>Gets this node's parent node.</summary>
        /// <remarks>Gets this node's parent node. Node overridable by extending classes, so useful if you really just need the Node type.
        ///     </remarks>
        /// <returns>parent node; or null if no parent.</returns>
        public Org.Jsoup.Nodes.Node ParentNode() {
            return parentNode;
        }

        /// <summary>Gets the Document associated with this Node.</summary>
        /// <returns>the Document associated with this Node, or null if there is no such Document.</returns>
        public virtual Document OwnerDocument() {
            if (this is Document) {
                return (Document)this;
            }
            else {
                if (parentNode == null) {
                    return null;
                }
                else {
                    return parentNode.OwnerDocument();
                }
            }
        }

        /// <summary>Remove (delete) this node from the DOM tree.</summary>
        /// <remarks>Remove (delete) this node from the DOM tree. If this node has children, they are also removed.</remarks>
        public virtual void Remove() {
            Validate.NotNull(parentNode);
            parentNode.RemoveChild(this);
        }

        /// <summary>Insert the specified HTML into the DOM before this node (i.e.</summary>
        /// <remarks>Insert the specified HTML into the DOM before this node (i.e. as a preceding sibling).</remarks>
        /// <param name="html">HTML to add before this node</param>
        /// <returns>this node, for chaining</returns>
        /// <seealso cref="After(System.String)"/>
        public virtual Org.Jsoup.Nodes.Node Before(String html) {
            AddSiblingHtml(siblingIndex, html);
            return this;
        }

        /// <summary>Insert the specified node into the DOM before this node (i.e.</summary>
        /// <remarks>Insert the specified node into the DOM before this node (i.e. as a preceding sibling).</remarks>
        /// <param name="node">to add before this node</param>
        /// <returns>this node, for chaining</returns>
        /// <seealso cref="After(Node)"/>
        public virtual Org.Jsoup.Nodes.Node Before(Org.Jsoup.Nodes.Node node) {
            Validate.NotNull(node);
            Validate.NotNull(parentNode);
            parentNode.AddChildren(siblingIndex, node);
            return this;
        }

        /// <summary>Insert the specified HTML into the DOM after this node (i.e.</summary>
        /// <remarks>Insert the specified HTML into the DOM after this node (i.e. as a following sibling).</remarks>
        /// <param name="html">HTML to add after this node</param>
        /// <returns>this node, for chaining</returns>
        /// <seealso cref="Before(System.String)"/>
        public virtual Org.Jsoup.Nodes.Node After(String html) {
            AddSiblingHtml(siblingIndex + 1, html);
            return this;
        }

        /// <summary>Insert the specified node into the DOM after this node (i.e.</summary>
        /// <remarks>Insert the specified node into the DOM after this node (i.e. as a following sibling).</remarks>
        /// <param name="node">to add after this node</param>
        /// <returns>this node, for chaining</returns>
        /// <seealso cref="Before(Node)"/>
        public virtual Org.Jsoup.Nodes.Node After(Org.Jsoup.Nodes.Node node) {
            Validate.NotNull(node);
            Validate.NotNull(parentNode);
            parentNode.AddChildren(siblingIndex + 1, node);
            return this;
        }

        private void AddSiblingHtml(int index, String html) {
            Validate.NotNull(html);
            Validate.NotNull(parentNode);
            Element context = Parent() is Element ? (Element)Parent() : null;
            IList<Org.Jsoup.Nodes.Node> nodes = Org.Jsoup.Parser.Parser.ParseFragment(html, context, BaseUri());
            parentNode.AddChildren(index, nodes.ToArray(new Org.Jsoup.Nodes.Node[nodes.Count]));
        }

        /// <summary>Wrap the supplied HTML around this node.</summary>
        /// <param name="html">
        /// HTML to wrap around this element, e.g.
        /// <c>&lt;div class="head"&gt;&lt;/div&gt;</c>
        /// . Can be arbitrarily deep.
        /// </param>
        /// <returns>this node, for chaining.</returns>
        public virtual Org.Jsoup.Nodes.Node Wrap(String html) {
            Validate.NotEmpty(html);
            Element context = Parent() is Element ? (Element)Parent() : null;
            IList<Org.Jsoup.Nodes.Node> wrapChildren = Org.Jsoup.Parser.Parser.ParseFragment(html, context, BaseUri());
            Org.Jsoup.Nodes.Node wrapNode = wrapChildren[0];
            if (wrapNode == null || !(wrapNode is Element)) {
                // nothing to wrap with; noop
                return null;
            }
            Element wrap = (Element)wrapNode;
            Element deepest = GetDeepChild(wrap);
            parentNode.ReplaceChild(this, wrap);
            deepest.AddChildren(this);
            // remainder (unbalanced wrap, like <div></div><p></p> -- The <p> is remainder
            if (wrapChildren.Count > 0) {
                for (int i = 0; i < wrapChildren.Count; i++) {
                    Org.Jsoup.Nodes.Node remainder = wrapChildren[i];
                    remainder.parentNode.RemoveChild(remainder);
                    wrap.AppendChild(remainder);
                }
            }
            return this;
        }

        /// <summary>Removes this node from the DOM, and moves its children up into the node's parent.</summary>
        /// <remarks>
        /// Removes this node from the DOM, and moves its children up into the node's parent. This has the effect of dropping
        /// the node but keeping its children.
        /// <p>
        /// For example, with the input html:
        /// </p>
        /// <p>
        /// <c>&lt;div&gt;One &lt;span&gt;Two &lt;b&gt;Three&lt;/b&gt;&lt;/span&gt;&lt;/div&gt;</c>
        /// </p>
        /// Calling
        /// <c>element.unwrap()</c>
        /// on the
        /// <c>span</c>
        /// element will result in the html:
        /// <p>
        /// <c>&lt;div&gt;One Two &lt;b&gt;Three&lt;/b&gt;&lt;/div&gt;</c>
        /// </p>
        /// and the
        /// <c>"Two "</c>
        /// 
        /// <see cref="TextNode"/>
        /// being returned.
        /// </remarks>
        /// <returns>the first child of this node, after the node has been unwrapped. Null if the node had no children.
        ///     </returns>
        /// <seealso cref="Remove()"/>
        /// <seealso cref="Wrap(System.String)"/>
        public virtual Org.Jsoup.Nodes.Node Unwrap() {
            Validate.NotNull(parentNode);
            Org.Jsoup.Nodes.Node firstChild = childNodes.Count > 0 ? childNodes[0] : null;
            parentNode.AddChildren(siblingIndex, this.ChildNodesAsArray());
            this.Remove();
            return firstChild;
        }

        private Element GetDeepChild(Element el) {
            IList<Element> children = el.Children();
            if (children.Count > 0) {
                return GetDeepChild(children[0]);
            }
            else {
                return el;
            }
        }

        /// <summary>Replace this node in the DOM with the supplied node.</summary>
        /// <param name="in">the node that will will replace the existing node.</param>
        public virtual void ReplaceWith(Org.Jsoup.Nodes.Node @in) {
            Validate.NotNull(@in);
            Validate.NotNull(parentNode);
            parentNode.ReplaceChild(this, @in);
        }

        protected internal virtual void SetParentNode(Org.Jsoup.Nodes.Node parentNode) {
            if (this.parentNode != null) {
                this.parentNode.RemoveChild(this);
            }
            this.parentNode = parentNode;
        }

        protected internal virtual void ReplaceChild(Org.Jsoup.Nodes.Node @out, Org.Jsoup.Nodes.Node @in) {
            Validate.IsTrue(@out.parentNode == this);
            Validate.NotNull(@in);
            if (@in.parentNode != null) {
                @in.parentNode.RemoveChild(@in);
            }
            int index = @out.siblingIndex;
            childNodes[index] = @in;
            @in.parentNode = this;
            @in.SetSiblingIndex(index);
            @out.parentNode = null;
        }

        protected internal virtual void RemoveChild(Org.Jsoup.Nodes.Node @out) {
            Validate.IsTrue(@out.parentNode == this);
            int index = @out.siblingIndex;
            childNodes.JRemoveAt(index);
            ReindexChildren(index);
            @out.parentNode = null;
        }

        protected internal virtual void AddChildren(params Org.Jsoup.Nodes.Node[] children) {
            //most used. short circuit addChildren(int), which hits reindex children and array copy
            foreach (Org.Jsoup.Nodes.Node child in children) {
                ReparentChild(child);
                EnsureChildNodes();
                childNodes.Add(child);
                child.SetSiblingIndex(childNodes.Count - 1);
            }
        }

        protected internal virtual void AddChildren(int index, params Org.Jsoup.Nodes.Node[] children) {
            Validate.NoNullElements(children);
            EnsureChildNodes();
            for (int i = children.Length - 1; i >= 0; i--) {
                Org.Jsoup.Nodes.Node @in = children[i];
                ReparentChild(@in);
                childNodes.Add(index, @in);
                ReindexChildren(index);
            }
        }

        protected internal virtual void EnsureChildNodes() {
            if (childNodes == EMPTY_NODES) {
                childNodes = new List<Org.Jsoup.Nodes.Node>(4);
            }
        }

        protected internal virtual void ReparentChild(Org.Jsoup.Nodes.Node child) {
            if (child.parentNode != null) {
                child.parentNode.RemoveChild(child);
            }
            child.SetParentNode(this);
        }

        private void ReindexChildren(int start) {
            for (int i = start; i < childNodes.Count; i++) {
                childNodes[i].SetSiblingIndex(i);
            }
        }

        /// <summary>Retrieves this node's sibling nodes.</summary>
        /// <remarks>
        /// Retrieves this node's sibling nodes. Similar to
        /// <see cref="ChildNodes()">node.parent.childNodes()</see>
        /// , but does not
        /// include this node (a node is not a sibling of itself).
        /// </remarks>
        /// <returns>node siblings. If the node has no parent, returns an empty list.</returns>
        public virtual IList<Org.Jsoup.Nodes.Node> SiblingNodes() {
            if (parentNode == null) {
                return JavaCollectionsUtil.EmptyList<Org.Jsoup.Nodes.Node>();
            }
            IList<Org.Jsoup.Nodes.Node> nodes = parentNode.childNodes;
            IList<Org.Jsoup.Nodes.Node> siblings = new List<Org.Jsoup.Nodes.Node>(nodes.Count - 1);
            foreach (Org.Jsoup.Nodes.Node node in nodes) {
                if (node != this) {
                    siblings.Add(node);
                }
            }
            return siblings;
        }

        /// <summary>Get this node's next sibling.</summary>
        /// <returns>next sibling, or null if this is the last sibling</returns>
        public virtual Org.Jsoup.Nodes.Node NextSibling() {
            if (parentNode == null) {
                return null;
            }
            // root
            IList<Org.Jsoup.Nodes.Node> siblings = parentNode.childNodes;
            int index = siblingIndex + 1;
            if (siblings.Count > index) {
                return siblings[index];
            }
            else {
                return null;
            }
        }

        /// <summary>Get this node's previous sibling.</summary>
        /// <returns>the previous sibling, or null if this is the first sibling</returns>
        public virtual Org.Jsoup.Nodes.Node PreviousSibling() {
            if (parentNode == null) {
                return null;
            }
            // root
            if (siblingIndex > 0) {
                return parentNode.childNodes[siblingIndex - 1];
            }
            else {
                return null;
            }
        }

        /// <summary>Get the list index of this node in its node sibling list.</summary>
        /// <remarks>
        /// Get the list index of this node in its node sibling list. I.e. if this is the first node
        /// sibling, returns 0.
        /// </remarks>
        /// <returns>position in node sibling list</returns>
        /// <seealso cref="Element.ElementSiblingIndex()"/>
        public virtual int SiblingIndex() {
            return siblingIndex;
        }

        protected internal virtual void SetSiblingIndex(int siblingIndex) {
            this.siblingIndex = siblingIndex;
        }

        /// <summary>Perform a depth-first traversal through this node and its descendants.</summary>
        /// <param name="nodeVisitor">the visitor callbacks to perform on each node</param>
        /// <returns>this node, for chaining</returns>
        public virtual Org.Jsoup.Nodes.Node Traverse(NodeVisitor nodeVisitor) {
            Validate.NotNull(nodeVisitor);
            NodeTraversor traversor = new NodeTraversor(nodeVisitor);
            traversor.Traverse(this);
            return this;
        }

        /// <summary>Get the outer HTML of this node.</summary>
        /// <returns>HTML</returns>
        public virtual String OuterHtml() {
            StringBuilder accum = new StringBuilder(128);
            OuterHtml(accum);
            return accum.ToString();
        }

        protected internal virtual void OuterHtml(StringBuilder accum) {
            new NodeTraversor(new Node.OuterHtmlVisitor(accum, GetOutputSettings())).Traverse(this);
        }

        // if this node has no document (or parent), retrieve the default output settings
        internal virtual OutputSettings GetOutputSettings() {
            return OwnerDocument() != null ? OwnerDocument().OutputSettings() : (new Document("")).OutputSettings();
        }

        /// <summary>Get the outer HTML of this node.</summary>
        /// <param name="accum">accumulator to place HTML into</param>
        /// <exception cref="System.IO.IOException">if appending to the given accumulator fails.</exception>
        internal abstract void OuterHtmlHead(StringBuilder accum, int depth, OutputSettings @out);

        /// <exception cref="System.IO.IOException"/>
        internal abstract void OuterHtmlTail(StringBuilder accum, int depth, OutputSettings @out);

        /// <summary>
        /// Write this node and its children to the given
        /// <see cref="System.Text.StringBuilder"/>
        /// .
        /// </summary>
        /// <param name="appendable">
        /// the
        /// <see cref="System.Text.StringBuilder"/>
        /// to write to.
        /// </param>
        /// <returns>
        /// the supplied
        /// <see cref="System.Text.StringBuilder"/>
        /// , for chaining.
        /// </returns>
        public virtual T Html<T>(T appendable)
            where T : StringBuilder {
            OuterHtml(appendable);
            return appendable;
        }

        public override String ToString() {
            return OuterHtml();
        }

        /// <exception cref="System.IO.IOException"/>
        protected internal virtual void Indent(StringBuilder accum, int depth, OutputSettings @out) {
            accum.Append("\n").Append(Org.Jsoup.Helper.StringUtil.Padding(depth * @out.IndentAmount()));
        }

        /// <summary>Check if this node is the same instance of another (object identity test).</summary>
        /// <param name="o">other object to compare to</param>
        /// <returns>true if the content of this node is the same as the other</returns>
        /// <seealso cref="HasSameValue(System.Object)">to compare nodes by their value</seealso>
        public override bool Equals(Object o) {
            // implemented just so that javadoc is clear this is an identity test
            return this == o;
        }

        /// <summary>Check if this node is has the same content as another node.</summary>
        /// <remarks>
        /// Check if this node is has the same content as another node. A node is considered the same if its name, attributes and content match the
        /// other node; particularly its position in the tree does not influence its similarity.
        /// </remarks>
        /// <param name="o">other object to compare to</param>
        /// <returns>true if the content of this node is the same as the other</returns>
        public virtual bool HasSameValue(Object o) {
            if (this == o) {
                return true;
            }
            if (o == null || GetType() != o.GetType()) {
                return false;
            }
            return this.OuterHtml().Equals(((Org.Jsoup.Nodes.Node)o).OuterHtml());
        }

        /// <summary>Create a stand-alone, deep copy of this node, and all of its children.</summary>
        /// <remarks>
        /// Create a stand-alone, deep copy of this node, and all of its children. The cloned node will have no siblings or
        /// parent node. As a stand-alone object, any changes made to the clone or any of its children will not impact the
        /// original node.
        /// <p>
        /// The cloned node may be adopted into another Document or node structure using
        /// <see cref="Element.AppendChild(Node)"/>
        /// .
        /// </remarks>
        /// <returns>stand-alone cloned node</returns>
        public virtual Object Clone() {
            Org.Jsoup.Nodes.Node thisClone = DoClone(null);
            // splits for orphan
            // Queue up nodes that need their children cloned (BFS).
            LinkedList<Org.Jsoup.Nodes.Node> nodesToProcess = new LinkedList<Org.Jsoup.Nodes.Node>();
            nodesToProcess.Add(thisClone);
            while (!nodesToProcess.IsEmpty()) {
                Org.Jsoup.Nodes.Node currParent = nodesToProcess.JRemove();
                for (int i = 0; i < currParent.childNodes.Count; i++) {
                    Org.Jsoup.Nodes.Node childClone = currParent.childNodes[i].DoClone(currParent);
                    currParent.childNodes[i] = childClone;
                    nodesToProcess.Add(childClone);
                }
            }
            return thisClone;
        }

        /*
        * Return a clone of the node using the given parent (which can be null).
        * Not a deep copy of children.
        */
        protected internal virtual Org.Jsoup.Nodes.Node DoClone(Org.Jsoup.Nodes.Node parent) {
            Org.Jsoup.Nodes.Node clone;
            clone = (Org.Jsoup.Nodes.Node)MemberwiseClone();
            clone.parentNode = parent;
            // can be null, to create an orphan split
            clone.siblingIndex = parent == null ? 0 : siblingIndex;
            clone.attributes = attributes != null ? (Org.Jsoup.Nodes.Attributes)attributes.Clone() : null;
            clone.baseUri = baseUri;
            clone.childNodes = new List<Org.Jsoup.Nodes.Node>(childNodes.Count);
            foreach (Org.Jsoup.Nodes.Node child in childNodes) {
                clone.childNodes.Add(child);
            }
            return clone;
        }

        private class OuterHtmlVisitor : NodeVisitor {
            private StringBuilder accum;

            private OutputSettings @out;

            internal OuterHtmlVisitor(StringBuilder accum, OutputSettings @out) {
                this.accum = accum;
                this.@out = @out;
            }

            public virtual void Head(Node node, int depth) {
                try {
                    node.OuterHtmlHead(accum, depth, @out);
                }
                catch (System.IO.IOException exception) {
                    throw new SerializationException(exception);
                }
            }

            public virtual void Tail(Node node, int depth) {
                if (!node.NodeName().Equals("#text")) {
                    // saves a void hit.
                    try {
                        node.OuterHtmlTail(accum, depth, @out);
                    }
                    catch (System.IO.IOException exception) {
                        throw new SerializationException(exception);
                    }
                }
            }
        }
    }
}
