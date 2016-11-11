using System;
using System.Text;

namespace Org.Jsoup.Nodes {
    /// <summary>
    /// A
    /// <c>&lt;!DOCTYPE&gt;</c>
    /// node.
    /// </summary>
    public class DocumentType : Node {
        private const String NAME = "name";

        private const String PUBLIC_ID = "publicId";

        private const String SYSTEM_ID = "systemId";

        /// <summary>Create a new doctype element.</summary>
        /// <param name="name">the doctype's name</param>
        /// <param name="publicId">the doctype's public ID</param>
        /// <param name="systemId">the doctype's system ID</param>
        /// <param name="baseUri">the doctype's base URI</param>
        public DocumentType(String name, String publicId, String systemId, String baseUri)
            : base(baseUri) {
            // todo: quirk mode from publicId and systemId
            Attr(NAME, name);
            Attr(PUBLIC_ID, publicId);
            Attr(SYSTEM_ID, systemId);
        }

        public override String NodeName() {
            return "#doctype";
        }

        /// <exception cref="System.IO.IOException"/>
        internal override void OuterHtmlHead(StringBuilder accum, int depth, OutputSettings @out) {
            if (@out.Syntax() == Syntax.html && !Has(PUBLIC_ID) && !Has(SYSTEM_ID)) {
                // looks like a html5 doctype, go lowercase for aesthetics
                accum.Append("<!doctype");
            }
            else {
                accum.Append("<!DOCTYPE");
            }
            if (Has(NAME)) {
                accum.Append(" ").Append(Attr(NAME));
            }
            if (Has(PUBLIC_ID)) {
                accum.Append(" PUBLIC \"").Append(Attr(PUBLIC_ID)).Append('"');
            }
            if (Has(SYSTEM_ID)) {
                accum.Append(" \"").Append(Attr(SYSTEM_ID)).Append('"');
            }
            accum.Append('>');
        }

        internal override void OuterHtmlTail(StringBuilder accum, int depth, OutputSettings @out) {
        }

        private bool Has(String attribute) {
            return !Org.Jsoup.Helper.StringUtil.IsBlank(Attr(attribute));
        }
    }
}
