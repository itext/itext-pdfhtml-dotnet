using System;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach {
    /// <summary>
    /// <see cref="IOutlineMarkExtractor"/>
    /// interface is used to control what part of element will be a mark
    /// witch will be used to create outline in
    /// <see cref="iText.Html2pdf.Attach.Impl.OutlineHandler"/>
    /// </summary>
    public interface IOutlineMarkExtractor {
        /// <summary>Get element mark.</summary>
        /// <param name="element">the element</param>
        /// <returns>returns string mark of the element</returns>
        String GetMark(IElementNode element);
    }
}
