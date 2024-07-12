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
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Commons.Utils;
using iText.Html2pdf.Css.Resolve.Func.Counter;
using iText.Html2pdf.Html;
using iText.IO.Font.Otf;
using iText.Kernel.Font;
using iText.Layout;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout {
//\cond DO_NOT_DOCUMENT
    /// <summary>
    /// <see cref="iText.Layout.Renderer.TextRenderer"/>
    /// implementation for the page count.
    /// </summary>
    internal class PageCountRenderer : TextRenderer {
        private readonly CounterDigitsGlyphStyle digitsGlyphStyle;

//\cond DO_NOT_DOCUMENT
        /// <summary>Instantiates a new page count renderer.</summary>
        /// <param name="textElement">the text element</param>
        internal PageCountRenderer(PageCountElement textElement)
            : base(textElement) {
            this.digitsGlyphStyle = textElement.GetDigitsGlyphStyle();
        }
//\endcond

        protected internal PageCountRenderer(TextRenderer other)
            : base(other) {
            this.digitsGlyphStyle = ((iText.Html2pdf.Attach.Impl.Layout.PageCountRenderer)other).digitsGlyphStyle;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.layout.renderer.TextRenderer#layout(com.itextpdf.layout.layout.LayoutContext)
        */
        public override LayoutResult Layout(LayoutContext layoutContext) {
            PageCountType pageCountType = (PageCountType)this.GetProperty<PageCountType?>(Html2PdfProperty.PAGE_COUNT_TYPE
                );
            String previousText = GetText().ToString();
            // If typography is enabled and the page counter element has a non-default direction,
            // iText processes its content (see LineRenderer#updateBidiLevels) before layouting it.
            // This might result in an ArrayIndexOutOfBounds exception, because currently iText updates the page counter's content on layout.
            // To solve this, this workaround has been implemented: the renderer's strToBeConverted shouldn't be updated by layout.
            bool textHasBeenReplaced = false;
            if (pageCountType == PageCountType.CURRENT_PAGE_NUMBER) {
                SetText(HtmlUtils.ConvertNumberAccordingToGlyphStyle(digitsGlyphStyle, layoutContext.GetArea().GetPageNumber
                    ()));
                textHasBeenReplaced = true;
            }
            else {
                if (pageCountType == PageCountType.TOTAL_PAGE_COUNT) {
                    IRenderer rootRenderer = this;
                    while (rootRenderer is AbstractRenderer && ((AbstractRenderer)rootRenderer).GetParent() != null) {
                        rootRenderer = ((AbstractRenderer)rootRenderer).GetParent();
                    }
                    if (rootRenderer is HtmlDocumentRenderer && ((HtmlDocumentRenderer)rootRenderer).GetEstimatedNumberOfPages
                        () > 0) {
                        SetText(HtmlUtils.ConvertNumberAccordingToGlyphStyle(digitsGlyphStyle, ((HtmlDocumentRenderer)rootRenderer
                            ).GetEstimatedNumberOfPages()));
                        textHasBeenReplaced = true;
                    }
                    else {
                        if (rootRenderer is DocumentRenderer && rootRenderer.GetModelElement() is Document) {
                            SetText(HtmlUtils.ConvertNumberAccordingToGlyphStyle(digitsGlyphStyle, ((Document)rootRenderer.GetModelElement
                                ()).GetPdfDocument().GetNumberOfPages()));
                            textHasBeenReplaced = true;
                        }
                    }
                }
            }
            LayoutResult result = base.Layout(layoutContext);
            if (textHasBeenReplaced) {
                SetText(previousText);
            }
            return result;
        }

        /// <summary><inheritDoc/></summary>
        public override IRenderer GetNextRenderer() {
            if (typeof(iText.Html2pdf.Attach.Impl.Layout.PageCountRenderer) != this.GetType()) {
                ILogger logger = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Layout.PageCountRenderer));
                logger.LogError(MessageFormatUtil.Format(iText.IO.Logs.IoLogMessageConstant.GET_NEXT_RENDERER_SHOULD_BE_OVERRIDDEN
                    ));
            }
            return new iText.Html2pdf.Attach.Impl.Layout.PageCountRenderer((PageCountElement)modelElement);
        }

        /// <summary><inheritDoc/></summary>
        protected override TextRenderer CreateCopy(GlyphLine gl, PdfFont font) {
            if (typeof(iText.Html2pdf.Attach.Impl.Layout.PageCountRenderer) != this.GetType()) {
                ILogger logger = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Layout.PageCountRenderer));
                logger.LogError(MessageFormatUtil.Format(iText.IO.Logs.IoLogMessageConstant.CREATE_COPY_SHOULD_BE_OVERRIDDEN
                    ));
            }
            iText.Html2pdf.Attach.Impl.Layout.PageCountRenderer copy = new iText.Html2pdf.Attach.Impl.Layout.PageCountRenderer
                (this);
            copy.SetProcessedGlyphLineAndFont(gl, font);
            return copy;
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
//\endcond
}
