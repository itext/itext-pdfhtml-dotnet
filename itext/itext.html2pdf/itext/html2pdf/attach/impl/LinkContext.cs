/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
Authors: Apryse Software.

This program is offered under a commercial and under the AGPL license.
For commercial licensing, contact us at https://itextpdf.com/sales.  For AGPL licensing, see below.

AGPL licensing:
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using iText.Html2pdf.Html;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl {
    /// <summary>This class keeps track of information regarding link (destinations) that occur in the document.</summary>
    /// <remarks>
    /// This class keeps track of information regarding link (destinations) that occur in the document.
    /// Doing so enables us to drastically trim the amount of PdfDestinations that will end up being included in the document.
    /// For performance reasons it was decided to scan the DOM tree only once and store the result in a separate object
    /// (this object) in the ProcessorContext.
    /// <para />
    /// This class is not reusable and a new instance shall be created for every new conversion process.
    /// </remarks>
    public class LinkContext {
        /// <summary>the ids currently in use as valid link destinations</summary>
        private ICollection<String> linkDestinations = new HashSet<String>();

        /// <summary>Construct an (empty) LinkContext</summary>
        public LinkContext() {
        }

        /// <summary>Scan the DOM tree for all (internal) link targets</summary>
        /// <param name="root">the DOM tree root node</param>
        /// <returns>this LinkContext</returns>
        public virtual iText.Html2pdf.Attach.Impl.LinkContext ScanForIds(INode root) {
            // clear previous
            linkDestinations.Clear();
            // expensive scan operation
            while (root.ParentNode() != null) {
                root = root.ParentNode();
            }
            Stack<INode> stk = new Stack<INode>();
            stk.Push(root);
            while (!stk.IsEmpty()) {
                INode n = stk.Pop();
                if (n is IElementNode) {
                    IElementNode elem = (IElementNode)n;
                    if (TagConstants.A.Equals(elem.Name())) {
                        String href = elem.GetAttribute(AttributeConstants.HREF);
                        if (href != null && href.StartsWith("#")) {
                            linkDestinations.Add(href.Substring(1));
                        }
                    }
                }
                if (!n.ChildNodes().IsEmpty()) {
                    stk.AddAll(n.ChildNodes());
                }
            }
            return this;
        }

        /// <summary>Returns whether a given (internal) link destination is used by at least one href element in the document
        ///     </summary>
        /// <param name="linkDestination">link destination</param>
        /// <returns>whether a given (internal) link destination is used by at least one href element in the document</returns>
        public virtual bool IsUsedLinkDestination(String linkDestination) {
            return linkDestinations.Contains(linkDestination);
        }
    }
}
