/*
This file is part of the iText (R) project.
Copyright (c) 1998-2019 iText Group NV
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
using System.Collections.Generic;
using iText.Layout;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout {
    /// <summary>
    /// <see cref="iText.Layout.Renderer.TextRenderer"/>
    /// implementation for the page count.
    /// </summary>
    internal class PageCountRenderer : TextRenderer {
        /// <summary>Instantiates a new page count renderer.</summary>
        /// <param name="textElement">the text element</param>
        internal PageCountRenderer(PageCountElement textElement)
            : base(textElement) {
        }

        /* (non-Javadoc)
        * @see com.itextpdf.layout.renderer.TextRenderer#layout(com.itextpdf.layout.layout.LayoutContext)
        */
        public override LayoutResult Layout(LayoutContext layoutContext) {
            PageCountType pageCountType = (PageCountType)this.GetProperty<PageCountType?>(Html2PdfProperty.PAGE_COUNT_TYPE
                );
            if (pageCountType == PageCountType.CURRENT_PAGE_NUMBER) {
                SetText(layoutContext.GetArea().GetPageNumber().ToString());
            }
            else {
                if (pageCountType == PageCountType.TOTAL_PAGE_COUNT) {
                    IRenderer rootRenderer = this;
                    while (rootRenderer is AbstractRenderer && ((AbstractRenderer)rootRenderer).GetParent() != null) {
                        rootRenderer = ((AbstractRenderer)rootRenderer).GetParent();
                    }
                    if (rootRenderer is HtmlDocumentRenderer && ((HtmlDocumentRenderer)rootRenderer).GetEstimatedNumberOfPages
                        () > 0) {
                        SetText(((HtmlDocumentRenderer)rootRenderer).GetEstimatedNumberOfPages().ToString());
                    }
                    else {
                        if (rootRenderer is DocumentRenderer && rootRenderer.GetModelElement() is Document) {
                            SetText(((Document)rootRenderer.GetModelElement()).GetPdfDocument().GetNumberOfPages().ToString());
                        }
                    }
                }
            }
            return base.Layout(layoutContext);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.layout.renderer.TextRenderer#resolveFonts(java.util.List)
        */
        protected override bool ResolveFonts(IList<IRenderer> addTo) {
            IList<IRenderer> dummyList = new List<IRenderer>();
            base.ResolveFonts(dummyList);
            SetProperty(Property.FONT, dummyList[0].GetProperty<Object>(Property.FONT));
            addTo.Add(this);
            return true;
        }
    }
}
