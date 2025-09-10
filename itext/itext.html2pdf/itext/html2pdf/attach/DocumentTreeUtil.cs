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
