using System;
using iText.Html2pdf.Attach;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl {
    /// <summary>
    /// <see cref="ClassOutlineMarkExtractor"/>
    /// class is used to get class of element as a mark for
    /// <see cref="OutlineHandler"/>
    /// </summary>
    public class ClassOutlineMarkExtractor : IOutlineMarkExtractor {
        public virtual String GetMark(IElementNode element) {
            return element.GetAttribute("class");
        }
    }
}
