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
using iText.Html2pdf.Attach.Impl.Layout;
using iText.Html2pdf.Css.Resolve.Func.Counter;
using iText.Layout;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>TagWorker class for the page count.</summary>
    public class PageCountWorker : SpanTagWorker {
        private IPropertyContainer pageCountElement;

        /// <summary>
        /// Creates a new
        /// <see cref="PageCountWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public PageCountWorker(IElementNode element, ProcessorContext context)
            : base(element, context) {
            if (element is PageCountElementNode) {
                CounterDigitsGlyphStyle digitsStyle = ((PageCountElementNode)element).GetDigitsGlyphStyle();
                if (element is PageTargetCountElementNode) {
                    pageCountElement = new PageTargetCountElement(((PageTargetCountElementNode)element).GetTarget(), digitsStyle
                        );
                }
                else {
                    bool totalPageCount = ((PageCountElementNode)element).IsTotalPageCount();
                    pageCountElement = new PageCountElement(digitsStyle);
                    pageCountElement.SetProperty(Html2PdfProperty.PAGE_COUNT_TYPE, totalPageCount ? PageCountType.TOTAL_PAGE_COUNT
                         : PageCountType.CURRENT_PAGE_NUMBER);
                }
            }
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.tags.SpanTagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public override void ProcessEnd(IElementNode element, ProcessorContext context) {
            base.ProcessTagChild(this, context);
            base.ProcessEnd(element, context);
        }

        public override IPropertyContainer GetElementResult() {
            return pageCountElement;
        }
    }
}
