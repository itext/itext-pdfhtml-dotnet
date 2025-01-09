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
using iText.Html2pdf.Attach;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// <see cref="iText.Html2pdf.Attach.ITagWorker"/>
    /// implementation for elements with
    /// <c>display: grid</c>.
    /// </summary>
    public class DisplayGridTagWorker : DivTagWorker {
        /// <summary>
        /// Creates a new
        /// <see cref="DisplayGridTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public DisplayGridTagWorker(IElementNode element, ProcessorContext context)
            : base(element, context, new GridContainer()) {
        }

        /// <summary><inheritDoc/></summary>
        public override bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            IPropertyContainer element = childTagWorker.GetElementResult();
            if (childTagWorker is BrTagWorker) {
                return base.ProcessTagChild(childTagWorker, context);
            }
            else {
                return AddBlockChild((IElement)element);
            }
        }
    }
}
