/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 iText Group NV
Authors: iText Software.

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
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Css.Resolve.Func.Counter {
    /// <summary>
    /// <see cref="iText.StyledXmlParser.Node.Impl.Jsoup.Node.JsoupElementNode"/>
    /// implementation for page target-counters.
    /// </summary>
    public class PageTargetCountElementNode : PageCountElementNode {
        /// <summary>The target from which page will be taken.</summary>
        private readonly String target;

        /// <summary>
        /// Creates a new
        /// <see cref="PageTargetCountElementNode"/>
        /// instance.
        /// </summary>
        /// <param name="parent">the parent node</param>
        /// <param name="target">the target from which page will be taken.</param>
        public PageTargetCountElementNode(INode parent, String target)
            : base(false, parent) {
            this.target = target;
        }

        /// <summary>Checks if the node represents the total page count.</summary>
        /// <returns>true, if the node represents the total page count</returns>
        public virtual String GetTarget() {
            return target;
        }
    }
}
