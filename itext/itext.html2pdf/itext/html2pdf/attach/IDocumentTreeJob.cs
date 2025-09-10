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
