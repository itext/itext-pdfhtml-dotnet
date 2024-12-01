/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
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
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Page;
using iText.Html2pdf.Css.Resolve.Func.Counter;
using iText.StyledXmlParser.Css.Resolve;
using iText.StyledXmlParser.Css.Util;

namespace iText.Html2pdf.Css.Resolve {
    /// <summary>Class that bundles all the CSS context properties.</summary>
    public class CssContext : AbstractCssContext {
        /// <summary>The root font size value in pt.</summary>
        private float rootFontSize = CssDimensionParsingUtils.ParseAbsoluteFontSize(CssDefaults.GetDefaultValue(CssConstants
            .FONT_SIZE));

        /// <summary>Current element font size in pt.</summary>
        private float currentFontSize = -1.0F;

        /// <summary>The counter manager.</summary>
        private CssCounterManager counterManager = new CssCounterManager();

        /// <summary>Indicates if a pages counter or page(s) target-counter is present.</summary>
        private bool pagesCounterOrTargetCounterPresent = false;

        /// <summary>Indicates if a non-page(s) target-counter(s) is present.</summary>
        private bool nonPagesTargetCounterPresent = false;

        /// <summary>The running elements manager.</summary>
        private CssRunningManager runningManager = new CssRunningManager();

        /// <summary>Gets the root font size.</summary>
        /// <returns>the root font size in pt</returns>
        public virtual float GetRootFontSize() {
            return rootFontSize;
        }

        /// <summary>Sets the root font size.</summary>
        /// <param name="fontSize">the new root font size</param>
        public virtual void SetRootFontSize(float fontSize) {
            this.rootFontSize = fontSize;
        }

        /// <summary>Gets the current element font size.</summary>
        /// <returns>the current element font size in pt</returns>
        public virtual float GetCurrentFontSize() {
            return currentFontSize;
        }

        /// <summary>Sets the current font size.</summary>
        /// <param name="fontSize">the new current element font size</param>
        public virtual void SetCurrentFontSize(float fontSize) {
            this.currentFontSize = fontSize;
        }

        /// <summary>Sets the root font size.</summary>
        /// <param name="fontSizeStr">the new root font size</param>
        public virtual void SetRootFontSize(String fontSizeStr) {
            this.rootFontSize = CssDimensionParsingUtils.ParseAbsoluteFontSize(fontSizeStr);
        }

        /// <summary>Gets the counter manager.</summary>
        /// <returns>the counter manager</returns>
        public virtual CssCounterManager GetCounterManager() {
            return counterManager;
        }

        /// <summary>Sets the presence of a pages counter or page(s) target counter.</summary>
        /// <param name="pagesCounterOrTargetCounterPresent">the new pages counter or page(s) target-counter present</param>
        public virtual void SetPagesCounterPresent(bool pagesCounterOrTargetCounterPresent) {
            this.pagesCounterOrTargetCounterPresent = pagesCounterOrTargetCounterPresent;
        }

        /// <summary>Checks if a pages counter or page(s) target-counter is present.</summary>
        /// <returns>true, if pages counter or page(s) target-counter present</returns>
        public virtual bool IsPagesCounterPresent() {
            return pagesCounterOrTargetCounterPresent;
        }

        /// <summary>Sets the presence of a non-page(s) target-counter(s).</summary>
        /// <param name="nonPagesTargetCounterPresent">the new non-page(s) target-counter(s) present</param>
        public virtual void SetNonPagesTargetCounterPresent(bool nonPagesTargetCounterPresent) {
            this.nonPagesTargetCounterPresent = nonPagesTargetCounterPresent;
        }

        /// <summary>Checks if a non-page(s) target-counter(s) is present.</summary>
        /// <returns>true, if non-page(s) target-counter(s) present</returns>
        public virtual bool IsNonPagesTargetCounterPresent() {
            return nonPagesTargetCounterPresent;
        }

        public virtual CssRunningManager GetRunningManager() {
            return runningManager;
        }
    }
}
