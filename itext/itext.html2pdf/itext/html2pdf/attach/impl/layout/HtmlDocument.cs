using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;

namespace iText.Html2pdf.Attach.Impl.Layout {
    /// <summary>
    /// This class is a wrapper on
    /// <see cref="iText.Layout.Document"/>
    /// , which is the default root element while creating a self-sufficient PDF.
    /// </summary>
    /// <remarks>
    /// This class is a wrapper on
    /// <see cref="iText.Layout.Document"/>
    /// , which is the default root element while creating a self-sufficient PDF.
    /// It contains several html-specific customizations.
    /// </remarks>
    public class HtmlDocument : Document {
        /// <summary>
        /// Creates a html document from a
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// with a manually set
        /// <see cref="iText.Kernel.Geom.PageSize"/>.
        /// </summary>
        /// <param name="pdfDoc">the in-memory representation of the PDF document</param>
        /// <param name="pageSize">the page size</param>
        /// <param name="immediateFlush">
        /// if true, write pages and page-related instructions
        /// to the
        /// <see cref="iText.Kernel.Pdf.PdfDocument"/>
        /// as soon as possible.
        /// </param>
        public HtmlDocument(PdfDocument pdfDoc, PageSize pageSize, bool immediateFlush)
            : base(pdfDoc, pageSize, immediateFlush) {
        }

        /// <summary><inheritDoc/></summary>
        public override void Relayout() {
            if (rootRenderer is HtmlDocumentRenderer) {
                ((HtmlDocumentRenderer)rootRenderer).RemoveEventHandlers();
            }
            base.Relayout();
        }
    }
}
