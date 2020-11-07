using System;
using iText.Layout.Element;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout {
    /// <summary>
    /// <see cref="iText.Layout.Element.Text"/>
    /// implementation to be used for the page target-counter.
    /// </summary>
    public class PageTargetCountElement : Text {
        private readonly String target;

        /// <summary>
        /// Instantiates a new
        /// <see cref="PageTargetCountElement"/>.
        /// </summary>
        /// <param name="target">name of the corresponding target</param>
        public PageTargetCountElement(String target)
            : base("1234567890") {
            this.target = target.Replace("'", "").Replace("#", "");
        }

        /// <summary>Gets element's target.</summary>
        /// <returns>target which was specified for this element.</returns>
        public virtual String GetTarget() {
            return target;
        }

        /// <summary><inheritDoc/></summary>
        protected override IRenderer MakeNewRenderer() {
            return new PageTargetCountRenderer(this);
        }
    }
}
