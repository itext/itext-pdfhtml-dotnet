using System;
using iText.Html2pdf.Attach;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl {
    /// <summary>
    /// <see cref="TagOutlineMarkExtractor"/>
    /// class is used to get tag of element as a mark for
    /// <see cref="OutlineHandler"/>
    /// </summary>
    public class TagOutlineMarkExtractor : IOutlineMarkExtractor {
        public virtual String GetMark(IElementNode element) {
            return element.Name();
        }
    }
}
