/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
Authors: Apryse Software.

This program is offered under a commercial and under the AGPL license.
For commercial licensing, contact us at https://itextpdf.com/sales.  For AGPL licensing, see below.

AGPL licensing:
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using iText.Commons.Utils;
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Css;

namespace iText.Html2pdf.Css.Page {
    /// <summary>Class that manages running elements.</summary>
    public class CssRunningManager {
        private IDictionary<String, LinkedHashSet<RunningElementContainer>> runningElements = new Dictionary<String
            , LinkedHashSet<RunningElementContainer>>();

        /// <summary>Registers new running element from HTML document.</summary>
        /// <param name="runningElemName">the name of the new running element.</param>
        /// <param name="container">a wrapper for the running elements taken out of the normal flow.</param>
        public virtual void AddRunningElement(String runningElemName, RunningElementContainer container) {
            LinkedHashSet<RunningElementContainer> runningElems = runningElements.Get(runningElemName);
            if (runningElems == null) {
                runningElems = new LinkedHashSet<RunningElementContainer>();
                runningElements.Put(runningElemName, runningElems);
            }
            runningElems.Add(container);
        }

        /// <summary>
        /// Finds the running element that has particular name and should appear on specific page with given occurrence
        /// options.
        /// </summary>
        /// <remarks>
        /// Finds the running element that has particular name and should appear on specific page with given occurrence
        /// options. This would work only if page content was already layouted and flushed (drawn).
        /// </remarks>
        /// <param name="runningElemName">the running element name.</param>
        /// <param name="occurrenceOption">
        /// an option defining which running element should be chosen in case there are multiple
        /// running elements with the same name on the given page.
        /// </param>
        /// <param name="pageNum">the 1-based index of the page for which running element is requested.</param>
        /// <returns>
        /// 
        /// <see cref="iText.Html2pdf.Attach.Impl.Layout.RunningElementContainer"/>
        /// with corresponding running element, or
        /// <see langword="null"/>
        /// if no running
        /// element should be displayed for the given page with the given name or occurrence option.
        /// </returns>
        public virtual RunningElementContainer GetRunningElement(String runningElemName, String occurrenceOption, 
            int pageNum) {
            LinkedHashSet<RunningElementContainer> runningElementContainers = runningElements.Get(runningElemName);
            if (runningElementContainers == null || runningElementContainers.IsEmpty()) {
                return null;
            }
            bool isLast = CssConstants.LAST.Equals(occurrenceOption);
            bool isFirstExcept = CssConstants.FIRST_EXCEPT.Equals(occurrenceOption);
            bool isStart = CssConstants.START.Equals(occurrenceOption);
            RunningElementContainer runningElementContainer = null;
            foreach (RunningElementContainer container in runningElementContainers) {
                if (container.GetOccurrencePage() == 0 || container.GetOccurrencePage() > pageNum) {
                    // Imagine that floating element is before some normal element, but is drawn on the next page,
                    // while this normal element is drawn on previous page.
                    // This example covers both the cases when zero and bigger page nums are required to be skipped,
                    // rather than breaking here.
                    continue;
                }
                if (container.GetOccurrencePage() < pageNum) {
                    runningElementContainer = container;
                }
                if (container.GetOccurrencePage() == pageNum) {
                    if (isFirstExcept) {
                        return null;
                    }
                    if (!isStart || container.IsFirstOnPage()) {
                        runningElementContainer = container;
                    }
                    if (!isLast) {
                        break;
                    }
                }
            }
            return runningElementContainer;
        }
    }
}
