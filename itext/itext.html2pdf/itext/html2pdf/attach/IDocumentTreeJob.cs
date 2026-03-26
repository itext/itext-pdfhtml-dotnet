/*
This file is part of the iText (R) project.
Copyright (c) 1998-2026 Apryse Group NV
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
using iText.Commons.Utils;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach {
    /// <summary>Interface for document tree jobs.</summary>
    [FunctionalInterfaceAttribute]
    public interface IDocumentTreeJob {
        /// <summary>Processes a node within a document tree structure at a given level.</summary>
        /// <remarks>
        /// Processes a node within a document tree structure at a given level.
        /// <br />
        /// This method is used to perform specific operations on an
        /// <see cref="iText.StyledXmlParser.Node.INode"/>
        /// based on the context of its hierarchical position in the document tree.
        /// </remarks>
        /// <param name="node">the node to process</param>
        /// <param name="level">the hierarchical level of the node in the document tree structure</param>
        void Process(INode node, int level);
    }
}
