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
using iText.Layout;
using iText.Layout.Element;

namespace iText.Html2pdf.Attach.Wrapelement {
    /// <summary>
    /// Wrapper for the
    /// <c>span</c>
    /// element.
    /// </summary>
    public class SpanWrapper : IWrapElement {
        /// <summary>The children of the span element.</summary>
        private IList<Object> children = new List<Object>();

        /// <summary>Adds a child span.</summary>
        /// <param name="span">the span element to add</param>
        public virtual void Add(SpanWrapper span) {
            children.Add(span);
        }

        /// <summary>Adds a child image.</summary>
        /// <param name="img">the img element to add</param>
        public virtual void Add(ILeafElement img) {
            children.Add(img);
        }

        /// <summary>Adds a child block element.</summary>
        /// <param name="block">the block element to add</param>
        public virtual void Add(IBlockElement block) {
            children.Add(block);
        }

        /// <summary>Adds a collection of lead elements as children.</summary>
        /// <param name="collection">the collection to add</param>
        public virtual void AddAll(ICollection<IElement> collection) {
            children.AddAll(collection);
        }

        /// <summary>Gets a list of all the child elements.</summary>
        /// <returns>the child elements</returns>
        public virtual IList<IPropertyContainer> GetElements() {
            IList<IPropertyContainer> leafs = new List<IPropertyContainer>();
            foreach (Object child in children) {
                if (child is IPropertyContainer) {
                    leafs.Add((IPropertyContainer)child);
                }
                else {
                    if (child is SpanWrapper) {
                        leafs.AddAll(((SpanWrapper)child).GetElements());
                    }
                }
            }
            return leafs;
        }
    }
}
