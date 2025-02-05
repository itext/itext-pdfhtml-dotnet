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
using iText.Layout;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach {
    /// <summary>Interface for all the tag worker implementations.</summary>
    public interface ITagWorker {
        /// <summary>Placeholder for what needs to be done after the content of a tag has been processed.</summary>
        /// <param name="element">the element node</param>
        /// <param name="context">the processor context</param>
        void ProcessEnd(IElementNode element, ProcessorContext context);

        /// <summary>Placeholder for what needs to be done while the content of a tag is being processed.</summary>
        /// <param name="content">the content of a node</param>
        /// <param name="context">the processor context</param>
        /// <returns>true, if content was successfully processed, otherwise false.</returns>
        bool ProcessContent(String content, ProcessorContext context);

        /// <summary>Placeholder for what needs to be done when a child node is being processed.</summary>
        /// <param name="childTagWorker">the tag worker of the child node</param>
        /// <param name="context">the processor context</param>
        /// <returns>true, if child was successfully processed, otherwise false.</returns>
        bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context);

        /// <summary>
        /// Gets a processed object if it can be expressed as an
        /// <see cref="iText.Layout.IPropertyContainer"/>
        /// instance.
        /// </summary>
        /// <returns>
        /// the same object on every call.
        /// Might return null either if result is not yet produced or if this particular
        /// tag worker doesn't produce result in a form of
        /// <see cref="iText.Layout.IPropertyContainer"/>.
        /// </returns>
        IPropertyContainer GetElementResult();
    }
}
