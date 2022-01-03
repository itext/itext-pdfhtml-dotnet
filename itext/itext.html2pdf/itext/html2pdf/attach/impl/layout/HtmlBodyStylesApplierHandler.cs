/*
This file is part of the iText (R) project.
Copyright (c) 1998-2022 iText Group NV
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
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Tagging;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace iText.Html2pdf.Attach.Impl.Layout {
    /// <summary>This handler draws backgrounds and borders for html, body and page-annotation styles.</summary>
    internal class HtmlBodyStylesApplierHandler : iText.Kernel.Events.IEventHandler {
        private readonly HtmlDocumentRenderer htmlDocumentRenderer;

        private readonly IDictionary<int, HtmlBodyStylesApplierHandler.PageStylesProperties> pageStylesPropertiesMap;

        private PdfCanvas pdfCanvas;

        /// <summary>Constructor for HtmlBodyStylesApplierHandler.</summary>
        /// <param name="htmlDocumentRenderer">
        /// 
        /// <see cref="HtmlDocumentRenderer"/>
        /// </param>
        /// <param name="pageStylesPropertiesMap">
        /// 
        /// <see cref="PageStylesProperties"/>
        /// map that contains styles for html and body elements
        /// </param>
        public HtmlBodyStylesApplierHandler(HtmlDocumentRenderer htmlDocumentRenderer, IDictionary<int, HtmlBodyStylesApplierHandler.PageStylesProperties
            > pageStylesPropertiesMap) {
            this.htmlDocumentRenderer = htmlDocumentRenderer;
            this.pageStylesPropertiesMap = pageStylesPropertiesMap;
        }

        public virtual void HandleEvent(iText.Kernel.Events.Event @event) {
            if (!(@event is PdfDocumentEvent)) {
                return;
            }
            PdfPage page = ((PdfDocumentEvent)@event).GetPage();
            int pageNumber = ((PdfDocumentEvent)@event).GetDocument().GetPageNumber(page);
            ProcessPage(page, pageNumber);
        }

        internal virtual void ProcessPage(PdfPage page, int pageNumber) {
            HtmlBodyStylesApplierHandler.PageStylesProperties pageProperties = pageStylesPropertiesMap.Get(pageNumber);
            if (pageProperties == null) {
                return;
            }
            PageContextProcessor contextProcessor = htmlDocumentRenderer.GetPageProcessor(pageNumber);
            pdfCanvas = contextProcessor.DrawPageBackground(page);
            ApplyHtmlBodyStyles(page, contextProcessor.ComputeLayoutMargins(), pageProperties.styles, pageNumber);
            pdfCanvas = null;
        }

        private void ApplyHtmlBodyStyles(PdfPage page, float[] margins, BodyHtmlStylesContainer[] styles, int pageNumber
            ) {
            int firstBackground = ApplyFirstBackground(page, margins, styles, pageNumber);
            bool htmlHasBackground = false;
            for (int i = 0; i < 2; i++) {
                if (styles[i] != null) {
                    if (i == 1) {
                        htmlHasBackground = styles[0] != null && (styles[0].HasOwnProperty(Property.BACKGROUND) || styles[0].HasOwnProperty
                            (Property.BACKGROUND_IMAGE));
                    }
                    if (styles[i].HasContentToDraw()) {
                        DrawSimulatedDiv(page, styles[i].properties, margins, firstBackground != i, pageNumber, htmlHasBackground);
                    }
                    for (int j = 0; j < 4; j++) {
                        margins[j] += styles[i].GetTotalWidth()[j];
                    }
                }
            }
        }

        /// <summary>If html doesn't have a background and the body has, then body background must be drawn on the whole page.
        ///     </summary>
        /// <remarks>
        /// If html doesn't have a background and the body has, then body background must be drawn on the whole page.
        /// Also for case when html doesn't have a background, but has borders,
        /// then body's background must be under the html's borders.
        /// </remarks>
        private int ApplyFirstBackground(PdfPage page, float[] margins, BodyHtmlStylesContainer[] styles, int pageNumber
            ) {
            int firstBackground = -1;
            if (styles[0] != null && (styles[0].GetOwnProperty<Background>(Property.BACKGROUND) != null || styles[0].GetOwnProperty
                <Object>(Property.BACKGROUND_IMAGE) != null)) {
                firstBackground = 0;
            }
            else {
                if (styles[1] != null && (styles[1].GetOwnProperty<Background>(Property.BACKGROUND) != null || styles[1].GetOwnProperty
                    <Object>(Property.BACKGROUND_IMAGE) != null)) {
                    firstBackground = 1;
                }
            }
            if (firstBackground != -1) {
                Dictionary<int, Object> background = new Dictionary<int, Object>();
                background.Put(Property.BACKGROUND, styles[firstBackground].GetProperty<Background>(Property.BACKGROUND));
                background.Put(Property.BACKGROUND_IMAGE, styles[firstBackground].GetProperty<Object>(Property.BACKGROUND_IMAGE
                    ));
                DrawSimulatedDiv(page, background, margins, true, pageNumber, false);
            }
            return firstBackground;
        }

        private void DrawSimulatedDiv(PdfPage page, IDictionary<int, Object> styles, float[] margins, bool drawBackground
            , int pageNumber, bool recalculateBodyAreaForContentSize) {
            Div pageBordersSimulation = new Div().SetFillAvailableArea(true);
            foreach (KeyValuePair<int, Object> entry in styles) {
                if ((entry.Key == Property.BACKGROUND || entry.Key == Property.BACKGROUND_IMAGE) && !drawBackground) {
                    continue;
                }
                pageBordersSimulation.SetProperty(entry.Key, entry.Value);
            }
            pageBordersSimulation.GetAccessibilityProperties().SetRole(StandardRoles.ARTIFACT);
            Rectangle backgroundArea = new Rectangle(page.GetTrimBox()).ApplyMargins(margins[0], margins[1], margins[2
                ], margins[3], false);
            if (recalculateBodyAreaForContentSize) {
                if (pageStylesPropertiesMap.Get(pageNumber).lowestAndHighest == null) {
                    return;
                }
                HtmlBodyStylesApplierHandler.LowestAndHighest lowestAndHighest = pageStylesPropertiesMap.Get(pageNumber).lowestAndHighest;
                RecalculateBackgroundAreaForBody(backgroundArea, pageBordersSimulation, lowestAndHighest);
            }
            if (pdfCanvas == null) {
                pdfCanvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), page.GetDocument());
            }
            iText.Layout.Canvas canvas = new iText.Layout.Canvas(pdfCanvas, backgroundArea);
            canvas.EnableAutoTagging(page);
            canvas.Add(pageBordersSimulation);
            canvas.Close();
        }

        private void RecalculateBackgroundAreaForBody(Rectangle backgroundArea, Div pageBordersSimulation, HtmlBodyStylesApplierHandler.LowestAndHighest
             lowestAndHighest) {
            UnitValue marginTop = pageBordersSimulation.GetOwnProperty<UnitValue>(Property.MARGIN_TOP);
            UnitValue marginBottom = pageBordersSimulation.GetOwnProperty<UnitValue>(Property.MARGIN_BOTTOM);
            float marginTopWidth = marginTop == null ? 0 : marginTop.GetValue();
            float marginBottomWidth = marginBottom == null ? 0 : marginBottom.GetValue();
            Border borderTop = pageBordersSimulation.GetOwnProperty<Border>(Property.BORDER_TOP);
            Border borderBottom = pageBordersSimulation.GetOwnProperty<Border>(Property.BORDER_BOTTOM);
            float borderTopWidth = borderTop == null ? 0 : borderTop.GetWidth();
            float borderBottomWidth = borderBottom == null ? 0 : borderBottom.GetWidth();
            UnitValue paddingTop = pageBordersSimulation.GetOwnProperty<UnitValue>(Property.PADDING_TOP);
            UnitValue paddingBottom = pageBordersSimulation.GetOwnProperty<UnitValue>(Property.PADDING_BOTTOM);
            float paddingTopWidth = paddingTop == null ? 0 : paddingTop.GetValue();
            float paddingBottomWidth = paddingBottom == null ? 0 : paddingBottom.GetValue();
            float oldHighest = backgroundArea.GetY() + backgroundArea.GetHeight();
            if (lowestAndHighest.lowest >= backgroundArea.GetY()) {
                backgroundArea.SetY(lowestAndHighest.lowest - paddingBottomWidth - borderBottomWidth - marginBottomWidth);
            }
            float newHighest = lowestAndHighest.highest - lowestAndHighest.lowest + paddingTopWidth + paddingBottomWidth
                 + borderTopWidth + borderBottomWidth + marginTopWidth + marginBottomWidth + backgroundArea.GetY();
            if (newHighest <= oldHighest) {
                backgroundArea.SetHeight(newHighest - backgroundArea.GetY());
            }
        }

        internal class LowestAndHighest {
            internal float lowest;

            internal float highest;

            public LowestAndHighest(float lowest, float highest) {
                this.lowest = lowest;
                this.highest = highest;
            }
        }

        internal class PageStylesProperties {
            internal BodyHtmlStylesContainer[] styles;

            internal HtmlBodyStylesApplierHandler.LowestAndHighest lowestAndHighest;

            public PageStylesProperties(BodyHtmlStylesContainer[] styles) {
                this.styles = styles;
            }
        }
    }
}
