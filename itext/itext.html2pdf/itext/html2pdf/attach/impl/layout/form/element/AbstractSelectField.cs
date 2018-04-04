using System;
using System.Collections.Generic;
using iText.Layout.Element;

namespace iText.Html2pdf.Attach.Impl.Layout.Form.Element {
    /// <summary>An abstract class for fields that represents a control for selecting one or several of the provided options.
    ///     </summary>
    public abstract class AbstractSelectField : FormField<iText.Html2pdf.Attach.Impl.Layout.Form.Element.AbstractSelectField
        > {
        private IList<IBlockElement> options = new List<IBlockElement>();

        protected internal AbstractSelectField(String id)
            : base(id) {
        }

        /// <summary>Adds a container with option(s).</summary>
        /// <remarks>Adds a container with option(s). This might be a container for options group.</remarks>
        /// <param name="optionElement">a container with option(s)</param>
        public virtual void AddOption(IBlockElement optionElement) {
            options.Add(optionElement);
        }

        /// <summary>Gets a list of containers with option(s).</summary>
        /// <remarks>Gets a list of containers with option(s). Every container might be a container for options group.
        ///     </remarks>
        /// <returns>a list of containers with option(s)</returns>
        public virtual IList<IBlockElement> GetOptions() {
            return options;
        }
    }
}
