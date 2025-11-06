/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
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
using System.Collections.Generic;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach {
    /// <summary>Utility class for document tree operations.</summary>
    public sealed class DocumentTreeUtil {
        private DocumentTreeUtil() {
        }

        //Utility class should not be instantiated
        /// <summary>Traverses a document tree starting from a specified node and performs a collection of jobs on each node.
        ///     </summary>
        /// <param name="node">node of the document tree to traverse</param>
        /// <param name="jobs">a collection of jobs to be executed on each node during the traversal</param>
        public static void Traverse(INode node, IList<IDocumentTreeJob> jobs) {
            Stack<INode> stk = new Stack<INode>();
            stk.Push(node);
            while (!stk.IsEmpty()) {
                INode n = stk.Pop();
                foreach (IDocumentTreeJob job in jobs) {
                    job.Process(n, stk.Count);
                }
                if (!n.ChildNodes().IsEmpty()) {
                    for (int i = n.ChildNodes().Count - 1; i >= 0; i--) {
                        stk.Push(n.ChildNodes()[i]);
                    }
                }
            }
        }
    }
}
