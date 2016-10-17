using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Org.Jsoup;
using Org.Jsoup.Helper;
using iText.IO.Util;

namespace Org.Jsoup.Nodes {
    /// <summary>The attributes of an Element.</summary>
    /// <remarks>
    /// The attributes of an Element.
    /// <p>
    /// Attributes are treated as a map: there can be only one value associated with an attribute key.
    /// </p>
    /// <p>
    /// Attribute key and value comparisons are done case insensitively, and keys are normalised to
    /// lower-case.
    /// </p>
    /// </remarks>
    /// <author>Jonathan Hedley, jonathan@hedley.net</author>
    public class Attributes : IEnumerable<Org.Jsoup.Nodes.Attribute>, ICloneable {
        protected internal const String dataPrefix = "data-";

        private LinkedDictionary<String, Org.Jsoup.Nodes.Attribute> attributes = null;

        // linked hash map to preserve insertion order.
        // null be default as so many elements have no attributes -- saves a good chunk of memory
        /// <summary>Get an attribute value by key.</summary>
        /// <param name="key">the attribute key</param>
        /// <returns>the attribute value if set; or empty string if not set.</returns>
        /// <seealso cref="HasKey(System.String)"/>
        public virtual String Get(String key) {
            Validate.NotEmpty(key);
            if (attributes == null) {
                return "";
            }
            Org.Jsoup.Nodes.Attribute attr = attributes.Get(key.ToLower(System.Globalization.CultureInfo.InvariantCulture
                ));
            return attr != null ? attr.Value : "";
        }

        /// <summary>Set a new attribute, or replace an existing one by key.</summary>
        /// <param name="key">attribute key</param>
        /// <param name="value">attribute value</param>
        public virtual void Put(String key, String value) {
            Org.Jsoup.Nodes.Attribute attr = new Org.Jsoup.Nodes.Attribute(key, value);
            Put(attr);
        }

        /// <summary>Set a new boolean attribute, remove attribute if value is false.</summary>
        /// <param name="key">attribute key</param>
        /// <param name="value">attribute value</param>
        public virtual void Put(String key, bool value) {
            if (value) {
                Put(new BooleanAttribute(key));
            }
            else {
                Remove(key);
            }
        }

        /// <summary>Set a new attribute, or replace an existing one by key.</summary>
        /// <param name="attribute">attribute</param>
        public virtual void Put(Org.Jsoup.Nodes.Attribute attribute) {
            Validate.NotNull(attribute);
            if (attributes == null) {
                attributes = new LinkedDictionary<String, Org.Jsoup.Nodes.Attribute>(2);
            }
            attributes[attribute.Key] = attribute;
        }

        /// <summary>Remove an attribute by key.</summary>
        /// <param name="key">attribute key to remove</param>
        public virtual void Remove(String key) {
            Validate.NotEmpty(key);
            if (attributes == null) {
                return;
            }
            attributes.JRemove(key.ToLower(System.Globalization.CultureInfo.InvariantCulture));
        }

        /// <summary>Tests if these attributes contain an attribute with this key.</summary>
        /// <param name="key">key to check for</param>
        /// <returns>true if key exists, false otherwise</returns>
        public virtual bool HasKey(String key) {
            return attributes != null && attributes.Contains(key.ToLower(System.Globalization.CultureInfo.InvariantCulture
                ));
        }

        /// <summary>Get the number of attributes in this set.</summary>
        /// <returns>size</returns>
        public virtual int Size() {
            if (attributes == null) {
                return 0;
            }
            return attributes.Count;
        }

        /// <summary>Add all the attributes from the incoming set to this set.</summary>
        /// <param name="incoming">attributes to add to these attributes.</param>
        public virtual void AddAll(Attributes incoming) {
            if (incoming.Size() == 0) {
                return;
            }
            if (attributes == null) {
                attributes = new LinkedDictionary<String, Org.Jsoup.Nodes.Attribute>(incoming.Size());
            }
            attributes.AddAll(incoming.attributes);
        }

        public virtual IEnumerator<Org.Jsoup.Nodes.Attribute> Iterator() {
            return AsList().Iterator();
        }

        /// <summary>Get the attributes as a List, for iteration.</summary>
        /// <remarks>
        /// Get the attributes as a List, for iteration. Do not modify the keys of the attributes via this view, as changes
        /// to keys will not be recognised in the containing set.
        /// </remarks>
        /// <returns>an view of the attributes as a List.</returns>
        public virtual IList<Org.Jsoup.Nodes.Attribute> AsList() {
            if (attributes == null) {
                return JavaCollectionsUtil.EmptyList();
            }
            IList<Org.Jsoup.Nodes.Attribute> list = new List<Org.Jsoup.Nodes.Attribute>(attributes.Count);
            foreach (KeyValuePair<String, Org.Jsoup.Nodes.Attribute> entry in attributes) {
                list.Add(entry.Value);
            }
            return JavaCollectionsUtil.UnmodifiableList(list);
        }

        /// <summary>
        /// Retrieves a filtered view of attributes that are HTML5 custom data attributes; that is, attributes with keys
        /// starting with
        /// <c>data-</c>
        /// .
        /// </summary>
        /// <returns>map of custom data attributes.</returns>
        public virtual IDictionary<String, String> Dataset() {
            return new Attributes.Dataset(this);
        }

        /// <summary>Get the HTML representation of these attributes.</summary>
        /// <returns>HTML</returns>
        /// <exception cref="Org.Jsoup.SerializationException">if the HTML representation of the attributes cannot be constructed.
        ///     </exception>
        public virtual String Html() {
            StringBuilder accum = new StringBuilder();
            try {
                Html(accum, (new Document("")).OutputSettings());
            }
            catch (System.IO.IOException e) {
                // output settings a bit funky, but this html() seldom used
                // ought never happen
                throw new SerializationException(e);
            }
            return accum.ToString();
        }

        /// <exception cref="System.IO.IOException"/>
        internal virtual void Html(StringBuilder accum, OutputSettings @out) {
            if (attributes == null) {
                return;
            }
            foreach (KeyValuePair<String, Org.Jsoup.Nodes.Attribute> entry in attributes) {
                Org.Jsoup.Nodes.Attribute attribute = entry.Value;
                accum.Append(" ");
                attribute.Html(accum, @out);
            }
        }

        public override String ToString() {
            return Html();
        }

        /// <summary>Checks if these attributes are equal to another set of attributes, by comparing the two sets</summary>
        /// <param name="o">attributes to compare with</param>
        /// <returns>if both sets of attributes have the same content</returns>
        public override bool Equals(Object o) {
            if (this == o) {
                return true;
            }
            if (!(o is Attributes)) {
                return false;
            }
            Attributes that = (Attributes)o;
            return !(attributes != null ? !attributes.Equals(that.attributes) : that.attributes != null);
        }

        /// <summary>Calculates the hashcode of these attributes, by iterating all attributes and summing their hashcodes.
        ///     </summary>
        /// <returns>calculated hashcode</returns>
        public override int GetHashCode() {
            return attributes != null ? attributes.GetHashCode() : 0;
        }

        public virtual Object Clone() {
            if (attributes == null) {
                return new Attributes();
            }
            Attributes clone;
            clone = (Attributes)base.Clone();
            clone.attributes = new LinkedDictionary<String, Org.Jsoup.Nodes.Attribute>(attributes.Count);
            foreach (Org.Jsoup.Nodes.Attribute attribute in this) {
                clone.attributes[attribute.Key] = (Org.Jsoup.Nodes.Attribute)attribute.Clone();
            }
            return clone;
        }

        private class Dataset : AbstractMap<String, String> {
            private Dataset(Attributes _enclosing) {
                this._enclosing = _enclosing;
                if (this._enclosing.attributes == null) {
                    this._enclosing.attributes = new LinkedDictionary<String, Org.Jsoup.Nodes.Attribute>(2);
                }
            }

            public override ICollection<KeyValuePair<String, String>> EntrySet() {
                return new Attributes.Dataset.EntrySet(this);
            }

            public override String Put(String key, String value) {
                String dataKey = Attributes.DataKey(key);
                String oldValue = this._enclosing.HasKey(dataKey) ? this._enclosing.attributes.Get(dataKey).Value : null;
                Org.Jsoup.Nodes.Attribute attr = new Org.Jsoup.Nodes.Attribute(dataKey, value);
                this._enclosing.attributes[dataKey] = attr;
                return oldValue;
            }

            private class EntrySet : AbstractSet<KeyValuePair<String, String>> {
                public override IEnumerator<KeyValuePair<String, String>> Iterator() {
                    return new Attributes.Dataset.DatasetIterator(this);
                }

                public override int Count {
                    get {
                        int count = 0;
                        IEnumerator iter = new Attributes.Dataset.DatasetIterator(this);
                        while (iter.HasNext()) {
                            count++;
                        }
                        return count;
                    }
                }

                internal EntrySet(Dataset _enclosing) {
                    this._enclosing = _enclosing;
                }

                private readonly Dataset _enclosing;
            }

            private class DatasetIterator : IEnumerator<KeyValuePair<String, String>> {
                private IEnumerator<Org.Jsoup.Nodes.Attribute> attrIter = this._enclosing._enclosing.attributes.Values.Iterator
                    ();

                private Org.Jsoup.Nodes.Attribute attr;

                public virtual bool HasNext() {
                    while (this.attrIter.HasNext()) {
                        this.attr = this.attrIter.Next();
                        if (this.attr.IsDataAttribute()) {
                            return true;
                        }
                    }
                    return false;
                }

                public virtual KeyValuePair<String, String> Next() {
                    return new Org.Jsoup.Nodes.Attribute(this.attr.Key.Substring(Attributes.dataPrefix.Length), this.attr.Value
                        );
                }

                public virtual void Remove() {
                    this._enclosing._enclosing.attributes.JRemove(this.attr.Key);
                }

                public void Dispose() {
                    
                }
                
                public bool MoveNext() {
                    if (!HasNext()) {
                        return false;
                    }
                    
                    Current = Next();
                    return true;
                }
                
                public void Reset() {
                    throw new System.NotSupportedException();
                }
                
                public KeyValuePair Current { get; private set; }
                
                object IEnumerator.Current {
                    get { return Current; }
                }

                internal DatasetIterator(Dataset _enclosing) {
                    this._enclosing = _enclosing;
                }

                private readonly Dataset _enclosing;
            }

            private readonly Attributes _enclosing;
        }

        private static String DataKey(String key) {
            return dataPrefix + key;
        }
    }
}
