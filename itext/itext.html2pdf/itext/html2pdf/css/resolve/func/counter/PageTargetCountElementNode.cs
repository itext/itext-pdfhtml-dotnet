using System;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Resolve.Func.Counter {
    /// <summary>
    /// <see cref="iText.StyledXmlParser.Node.Impl.Jsoup.Node.JsoupElementNode"/>
    /// implementation for page target-counters.
    /// </summary>
    public class PageTargetCountElementNode : PageCountElementNode {
        /// <summary>The target from which page will be taken.</summary>
        private readonly String target;

        /// <summary>
        /// Creates a new
        /// <see cref="PageTargetCountElementNode"/>
        /// instance.
        /// </summary>
        /// <param name="parent">the parent node</param>
        /// <param name="target">the target from which page will be taken.</param>
        public PageTargetCountElementNode(INode parent, String target)
            : base(false, parent) {
            this.target = target;
        }

        /// <summary>Checks if the node represents the total page count.</summary>
        /// <returns>true, if the node represents the total page count</returns>
        public virtual String GetTarget() {
            return target;
        }
    }
}
