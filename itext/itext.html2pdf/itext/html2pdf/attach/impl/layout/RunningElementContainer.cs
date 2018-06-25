using iText.Html2pdf.Attach;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Layout {
    /// <summary>A wrapper for the running elements taken out of the normal flow.</summary>
    public class RunningElementContainer {
        private IElementNode runningElement;

        private ITagWorker processedElementWorker;

        private int pageNum;

        private bool firstOnPage;

        /// <summary>
        /// Initializes a new instance of
        /// <see cref="RunningElementContainer"/>
        /// that contains
        /// given running element
        /// <see cref="iText.StyledXmlParser.Node.IElementNode"/>
        /// and
        /// <see cref="iText.Html2pdf.Attach.ITagWorker"/>
        /// instances.
        /// </summary>
        /// <param name="runningElement">
        /// the
        /// <see cref="iText.StyledXmlParser.Node.IElementNode"/>
        /// of the running element.
        /// </param>
        /// <param name="processedElementWorker">
        /// the
        /// <see cref="iText.Html2pdf.Attach.ITagWorker"/>
        /// that was created for the running element
        /// and have been already completely processed (with all running element children).
        /// </param>
        public RunningElementContainer(IElementNode runningElement, ITagWorker processedElementWorker) {
            this.runningElement = runningElement;
            this.processedElementWorker = processedElementWorker;
        }

        /// <summary>Sets the page on which underlying running element was to be placed.</summary>
        /// <param name="pageNum">the 1-based index of the page on which running element was to be placed.</param>
        /// <param name="firstOnPage">specifies if the given running element would have placed as the first element on the page or not.
        ///     </param>
        public virtual void SetOccurrencePage(int pageNum, bool firstOnPage) {
            this.pageNum = pageNum;
            this.firstOnPage = firstOnPage;
        }

        /// <summary>Gets the page on which underlying running element was to be placed.</summary>
        /// <returns>the 1-based index of the page or 0 if element page is not yet defined.</returns>
        public virtual int GetOccurrencePage() {
            return this.pageNum;
        }

        /// <summary>Specifies if the given running element would have placed as the first element on the page or not.
        ///     </summary>
        /// <remarks>
        /// Specifies if the given running element would have placed as the first element on the page or not.
        /// Returned value only makes sense if
        /// <see cref="GetOccurrencePage()"/>
        /// returns value greater than 0.
        /// </remarks>
        /// <returns>true if it would be the first element on the page, otherwise false.</returns>
        public virtual bool IsFirstOnPage() {
            return this.firstOnPage;
        }

        internal virtual IElementNode GetRunningElement() {
            return runningElement;
        }

        internal virtual ITagWorker GetProcessedElementWorker() {
            return processedElementWorker;
        }
    }
}
