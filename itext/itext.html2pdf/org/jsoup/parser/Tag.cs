using System;
using System.Collections.Generic;
using Org.Jsoup.Helper;

namespace Org.Jsoup.Parser {
    /// <summary>HTML Tag capabilities.</summary>
    /// <author>Jonathan Hedley, jonathan@hedley.net</author>
    public class Tag {
        private static readonly IDictionary<String, Org.Jsoup.Parser.Tag> tags = new Dictionary<String, Org.Jsoup.Parser.Tag
            >();

        private String tagName;

        private bool isBlock = true;

        private bool formatAsBlock = true;

        private bool canContainBlock = true;

        private bool canContainInline = true;

        private bool empty = false;

        private bool selfClosing = false;

        private bool preserveWhitespace = false;

        private bool formList = false;

        private bool formSubmit = false;

        private Tag(String tagName) {
            // map of known tags
            // block or inline
            // should be formatted as a block
            // Can this tag hold block level tags?
            // only pcdata if not
            // can hold nothing; e.g. img
            // can self close (<foo />). used for unknown tags that self close, without forcing them as empty.
            // for pre, textarea, script etc
            // a control that appears in forms: input, textarea, output etc
            // a control that can be submitted in a form: input etc
            this.tagName = tagName.ToLower(System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>Get this tag's name.</summary>
        /// <returns>the tag's name</returns>
        public virtual String GetName() {
            return tagName;
        }

        /// <summary>Get a Tag by name.</summary>
        /// <remarks>
        /// Get a Tag by name. If not previously defined (unknown), returns a new generic tag, that can do anything.
        /// <p>
        /// Pre-defined tags (P, DIV etc) will be ==, but unknown tags are not registered and will only .equals().
        /// </p>
        /// </remarks>
        /// <param name="tagName">Name of tag, e.g. "p". Case insensitive.</param>
        /// <returns>The tag, either defined or new generic.</returns>
        public static Org.Jsoup.Parser.Tag ValueOf(String tagName) {
            Validate.NotNull(tagName);
            Org.Jsoup.Parser.Tag tag = tags.Get(tagName);
            if (tag == null) {
                tagName = tagName.Trim().ToLower(System.Globalization.CultureInfo.InvariantCulture);
                Validate.NotEmpty(tagName);
                tag = tags.Get(tagName);
                if (tag == null) {
                    // not defined: create default; go anywhere, do anything! (incl be inside a <p>)
                    tag = new Org.Jsoup.Parser.Tag(tagName);
                    tag.isBlock = false;
                    tag.canContainBlock = true;
                }
            }
            return tag;
        }

        /// <summary>Gets if this is a block tag.</summary>
        /// <returns>if block tag</returns>
        public virtual bool IsBlock() {
            return isBlock;
        }

        /// <summary>Gets if this tag should be formatted as a block (or as inline)</summary>
        /// <returns>if should be formatted as block or inline</returns>
        public virtual bool FormatAsBlock() {
            return formatAsBlock;
        }

        /// <summary>Gets if this tag can contain block tags.</summary>
        /// <returns>if tag can contain block tags</returns>
        public virtual bool CanContainBlock() {
            return canContainBlock;
        }

        /// <summary>Gets if this tag is an inline tag.</summary>
        /// <returns>if this tag is an inline tag.</returns>
        public virtual bool IsInline() {
            return !isBlock;
        }

        /// <summary>Gets if this tag is a data only tag.</summary>
        /// <returns>if this tag is a data only tag</returns>
        public virtual bool IsData() {
            return !canContainInline && !IsEmpty();
        }

        /// <summary>Get if this is an empty tag</summary>
        /// <returns>if this is an empty tag</returns>
        public virtual bool IsEmpty() {
            return empty;
        }

        /// <summary>Get if this tag is self closing.</summary>
        /// <returns>if this tag should be output as self closing.</returns>
        public virtual bool IsSelfClosing() {
            return empty || selfClosing;
        }

        /// <summary>Get if this is a pre-defined tag, or was auto created on parsing.</summary>
        /// <returns>if a known tag</returns>
        public virtual bool IsKnownTag() {
            return tags.ContainsKey(tagName);
        }

        /// <summary>Check if this tagname is a known tag.</summary>
        /// <param name="tagName">name of tag</param>
        /// <returns>if known HTML tag</returns>
        public static bool IsKnownTag(String tagName) {
            return tags.ContainsKey(tagName);
        }

        /// <summary>Get if this tag should preserve whitespace within child text nodes.</summary>
        /// <returns>if preserve whitepace</returns>
        public virtual bool PreserveWhitespace() {
            return preserveWhitespace;
        }

        /// <summary>Get if this tag represents a control associated with a form.</summary>
        /// <remarks>Get if this tag represents a control associated with a form. E.g. input, textarea, output</remarks>
        /// <returns>if associated with a form</returns>
        public virtual bool IsFormListed() {
            return formList;
        }

        /// <summary>Get if this tag represents an element that should be submitted with a form.</summary>
        /// <remarks>Get if this tag represents an element that should be submitted with a form. E.g. input, option</remarks>
        /// <returns>if submittable with a form</returns>
        public virtual bool IsFormSubmittable() {
            return formSubmit;
        }

        internal virtual Org.Jsoup.Parser.Tag SetSelfClosing() {
            selfClosing = true;
            return this;
        }

        public override bool Equals(Object o) {
            if (this == o) {
                return true;
            }
            if (!(o is Org.Jsoup.Parser.Tag)) {
                return false;
            }
            Org.Jsoup.Parser.Tag tag = (Org.Jsoup.Parser.Tag)o;
            if (!tagName.Equals(tag.tagName)) {
                return false;
            }
            if (canContainBlock != tag.canContainBlock) {
                return false;
            }
            if (canContainInline != tag.canContainInline) {
                return false;
            }
            if (empty != tag.empty) {
                return false;
            }
            if (formatAsBlock != tag.formatAsBlock) {
                return false;
            }
            if (isBlock != tag.isBlock) {
                return false;
            }
            if (preserveWhitespace != tag.preserveWhitespace) {
                return false;
            }
            if (selfClosing != tag.selfClosing) {
                return false;
            }
            if (formList != tag.formList) {
                return false;
            }
            return formSubmit == tag.formSubmit;
        }

        public override int GetHashCode() {
            int result = tagName.GetHashCode();
            result = 31 * result + (isBlock ? 1 : 0);
            result = 31 * result + (formatAsBlock ? 1 : 0);
            result = 31 * result + (canContainBlock ? 1 : 0);
            result = 31 * result + (canContainInline ? 1 : 0);
            result = 31 * result + (empty ? 1 : 0);
            result = 31 * result + (selfClosing ? 1 : 0);
            result = 31 * result + (preserveWhitespace ? 1 : 0);
            result = 31 * result + (formList ? 1 : 0);
            result = 31 * result + (formSubmit ? 1 : 0);
            return result;
        }

        public override String ToString() {
            return tagName;
        }

        private static readonly String[] blockTags = new String[] { "html", "head", "body", "frameset", "script", 
            "noscript", "style", "meta", "link", "title", "frame", "noframes", "section", "nav", "aside", "hgroup"
            , "header", "footer", "p", "h1", "h2", "h3", "h4", "h5", "h6", "ul", "ol", "pre", "div", "blockquote", 
            "hr", "address", "figure", "figcaption", "form", "fieldset", "ins", "del", "s", "dl", "dt", "dd", "li"
            , "table", "caption", "thead", "tfoot", "tbody", "colgroup", "col", "tr", "th", "td", "video", "audio"
            , "canvas", "details", "menu", "plaintext", "template", "article", "main", "svg", "math" };

        private static readonly String[] inlineTags = new String[] { "object", "base", "font", "tt", "i", "b", "u"
            , "big", "small", "em", "strong", "dfn", "code", "samp", "kbd", "var", "cite", "abbr", "time", "acronym"
            , "mark", "ruby", "rt", "rp", "a", "img", "br", "wbr", "map", "q", "sub", "sup", "bdo", "iframe", "embed"
            , "span", "input", "select", "textarea", "label", "button", "optgroup", "option", "legend", "datalist"
            , "keygen", "output", "progress", "meter", "area", "param", "source", "track", "summary", "command", "device"
            , "area", "basefont", "bgsound", "menuitem", "param", "source", "track", "data", "bdi" };

        private static readonly String[] emptyTags = new String[] { "meta", "link", "base", "frame", "img", "br", 
            "wbr", "embed", "hr", "input", "keygen", "col", "command", "device", "area", "basefont", "bgsound", "menuitem"
            , "param", "source", "track" };

        private static readonly String[] formatAsInlineTags = new String[] { "title", "a", "p", "h1", "h2", "h3", 
            "h4", "h5", "h6", "pre", "address", "li", "th", "td", "script", "style", "ins", "del", "s" };

        private static readonly String[] preserveWhitespaceTags = new String[] { "pre", "plaintext", "title", "textarea"
             };

        private static readonly String[] formListedTags = new String[] { "button", "fieldset", "input", "keygen", 
            "object", "output", "select", "textarea" };

        private static readonly String[] formSubmitTags = new String[] { "input", "keygen", "object", "select", "textarea"
             };

        static Tag() {
            // internal static initialisers:
            // prepped from http://www.w3.org/TR/REC-html40/sgml/dtd.html and other sources
            // script is not here as it is a data node, which always preserve whitespace
            // todo: I think we just need submit tags, and can scrub listed
            // creates
            foreach (String tagName in blockTags) {
                Org.Jsoup.Parser.Tag tag = new Org.Jsoup.Parser.Tag(tagName);
                Register(tag);
            }
            foreach (String tagName_1 in inlineTags) {
                Org.Jsoup.Parser.Tag tag = new Org.Jsoup.Parser.Tag(tagName_1);
                tag.isBlock = false;
                tag.canContainBlock = false;
                tag.formatAsBlock = false;
                Register(tag);
            }
            // mods:
            foreach (String tagName_2 in emptyTags) {
                Org.Jsoup.Parser.Tag tag = tags.Get(tagName_2);
                Validate.NotNull(tag);
                tag.canContainBlock = false;
                tag.canContainInline = false;
                tag.empty = true;
            }
            foreach (String tagName_3 in formatAsInlineTags) {
                Org.Jsoup.Parser.Tag tag = tags.Get(tagName_3);
                Validate.NotNull(tag);
                tag.formatAsBlock = false;
            }
            foreach (String tagName_4 in preserveWhitespaceTags) {
                Org.Jsoup.Parser.Tag tag = tags.Get(tagName_4);
                Validate.NotNull(tag);
                tag.preserveWhitespace = true;
            }
            foreach (String tagName_5 in formListedTags) {
                Org.Jsoup.Parser.Tag tag = tags.Get(tagName_5);
                Validate.NotNull(tag);
                tag.formList = true;
            }
            foreach (String tagName_6 in formSubmitTags) {
                Org.Jsoup.Parser.Tag tag = tags.Get(tagName_6);
                Validate.NotNull(tag);
                tag.formSubmit = true;
            }
        }

        private static void Register(Org.Jsoup.Parser.Tag tag) {
            tags[tag.tagName] = tag;
        }
    }
}
