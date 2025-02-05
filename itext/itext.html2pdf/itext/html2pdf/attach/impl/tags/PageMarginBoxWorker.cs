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
using iText.Kernel.Pdf.Tagging;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Tagging;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>TagWorker class for the page margin box.</summary>
    public class PageMarginBoxWorker : DivTagWorker {
        private Div wrappedElementResult;

        /// <summary>
        /// Creates a new
        /// <see cref="PageMarginBoxWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public PageMarginBoxWorker(IElementNode element, ProcessorContext context)
            : base(element, context) {
        }

        public override void ProcessEnd(IElementNode element, ProcessorContext context) {
            base.ProcessEnd(element, context);
            GetElementResult().SetProperty(Property.COLLAPSING_MARGINS, true);
            if (base.GetElementResult() is IBlockElement) {
                wrappedElementResult = new Div().Add((IBlockElement)base.GetElementResult());
                wrappedElementResult.SetProperty(Property.COLLAPSING_MARGINS, false);
            }
            if (GetElementResult() is IAccessibleElement) {
                ((IAccessibleElement)GetElementResult()).GetAccessibilityProperties().SetRole(StandardRoles.ARTIFACT);
            }
        }

        public override IPropertyContainer GetElementResult() {
            return wrappedElementResult == null ? base.GetElementResult() : wrappedElementResult;
        }
    }
}
