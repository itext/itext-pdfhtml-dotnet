using System;
using System.Collections.Generic;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Page {
    /// <summary>
    /// Wrapper
    /// <see cref="iText.StyledXmlParser.Node.INode"/>
    /// serving as a placeholder for running element.
    /// </summary>
    public class PageMarginRunningElementNode : INode {
        private String runningElementName;

        private String runningElementOccurrence;

        public PageMarginRunningElementNode(String runningElementName, String runningElementOccurrence) {
            this.runningElementName = runningElementName;
            this.runningElementOccurrence = runningElementOccurrence;
        }

        public virtual IList<INode> ChildNodes() {
            throw new NotSupportedException();
        }

        public virtual void AddChild(INode node) {
            throw new NotSupportedException();
        }

        public virtual INode ParentNode() {
            throw new NotSupportedException();
        }

        public virtual String GetRunningElementName() {
            return runningElementName;
        }

        public virtual String GetRunningElementOccurrence() {
            return runningElementOccurrence;
        }
    }
}
