/*
This file is part of the iText (R) project.
Copyright (c) 1998-2020 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.

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

        /// <summary>The counter manager.</summary>
        private CssCounterManager counterManager = new CssCounterManager();

        /// <summary>Indicates if a pages counter or page(s) target-counter is present.</summary>
        private bool pagesCounterOrTargetCounterPresent = false;

        /// <summary>Indicates if a non-page(s) target-counter(s) is present.</summary>
        private bool nonPagesTargetCounterPresent = false;

        /// <summary>The running elements manager.</summary>
        private CssRunningManager runningManager = new CssRunningManager();

        /// <summary>Indicates whether the document shall process target-counter or not.</summary>
        private bool targetCounterEnabled = false;

        /// <summary>Gets the root font size.</summary>
        /// <returns>the root font size in pt</returns>
        public virtual float GetRootFontSize() {
            return rootFontSize;
        }

        /// <summary>Sets the targetCounterEnabled flag.</summary>
        /// <param name="targetCounterEnabled">true if target-counter shall be processed, false otherwise</param>
        /// <returns>
        /// the
        /// <see cref="CssContext"/>
        /// instance
        /// </returns>
        public virtual CssContext SetTargetCounterEnabled(bool targetCounterEnabled) {
            this.targetCounterEnabled = targetCounterEnabled;
            return this;
        }

        /// <summary>Checks if target-counter is enabled.</summary>
        /// <returns>true if target-counter shall be processed, false otherwise</returns>
        public virtual bool IsTargetCounterEnabled() {
            return targetCounterEnabled;
        }

        /// <summary>Sets the root font size.</summary>
        /// <param name="fontSize">the new root font size</param>
        public virtual void SetRootFontSize(float fontSize) {
            this.rootFontSize = fontSize;
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
