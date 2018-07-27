/*
This file is part of the iText (R) project.
Copyright (c) 1998-2018 iText Group NV
Authors: iText Software.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
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
