using System;

namespace Org.Jsoup.Nodes {
    /// <summary>A boolean attribute that is written out without any value.</summary>
    public class BooleanAttribute : Org.Jsoup.Nodes.Attribute {
        /// <summary>Create a new boolean attribute from unencoded (raw) key.</summary>
        /// <param name="key">attribute key</param>
        public BooleanAttribute(String key)
            : base(key, "") {
        }

        protected internal override bool IsBooleanAttribute() {
            return true;
        }
    }
}
