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
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Commons.Utils;
using iText.Html2pdf.Css.Resolve.Func.Counter;
using iText.Html2pdf.Html;
using iText.Html2pdf.Logs;
using iText.IO.Font.Otf;
using iText.Kernel.Font;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout {
//\cond DO_NOT_DOCUMENT
    /// <summary>
    /// <see cref="iText.Layout.Renderer.TextRenderer"/>
    /// implementation for the page target-counter.
    /// </summary>
    internal class PageTargetCountRenderer : TextRenderer {
        private static readonly ILogger LOGGER = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Layout.PageTargetCountRenderer
            ));

        private const String UNDEFINED_VALUE = "0";

        private readonly String target;

        private readonly CounterDigitsGlyphStyle digitsGlyphStyle;

//\cond DO_NOT_DOCUMENT
        /// <summary>
        /// Instantiates a new
        /// <see cref="PageTargetCountRenderer"/>.
        /// </summary>
        /// <param name="textElement">the text element</param>
        internal PageTargetCountRenderer(PageTargetCountElement textElement)
            : base(textElement) {
            digitsGlyphStyle = textElement.GetDigitsGlyphStyle();
            target = textElement.GetTarget();
        }
//\endcond

        protected internal PageTargetCountRenderer(TextRenderer other)
            : base(other) {
            this.digitsGlyphStyle = ((iText.Html2pdf.Attach.Impl.Layout.PageTargetCountRenderer)other).digitsGlyphStyle;
            this.target = ((iText.Html2pdf.Attach.Impl.Layout.PageTargetCountRenderer)other).target;
        }

        /// <summary><inheritDoc/></summary>
        public override LayoutResult Layout(LayoutContext layoutContext) {
            String previousText = GetText().ToString();
            int? page = TargetCounterHandler.GetPageByID(this, target);
            if (page == null) {
                SetText(UNDEFINED_VALUE);
            }
            else {
                SetText(HtmlUtils.ConvertNumberAccordingToGlyphStyle(digitsGlyphStyle, (int)page));
            }
            LayoutResult result = base.Layout(layoutContext);
            SetText(previousText);
            return result;
        }

        /// <summary><inheritDoc/></summary>
        public override void Draw(DrawContext drawContext) {
            if (!TargetCounterHandler.IsValueDefinedForThisId(this, target)) {
                LOGGER.LogWarning(MessageFormatUtil.Format(Html2PdfLogMessageConstant.CANNOT_RESOLVE_TARGET_COUNTER_VALUE, 
                    target));
            }
            base.Draw(drawContext);
        }

        /// <summary><inheritDoc/></summary>
        public override IRenderer GetNextRenderer() {
            if (typeof(iText.Html2pdf.Attach.Impl.Layout.PageTargetCountRenderer) != this.GetType()) {
                ILogger logger = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Layout.PageTargetCountRenderer
                    ));
                logger.LogError(MessageFormatUtil.Format(iText.IO.Logs.IoLogMessageConstant.GET_NEXT_RENDERER_SHOULD_BE_OVERRIDDEN
                    ));
            }
            return new iText.Html2pdf.Attach.Impl.Layout.PageTargetCountRenderer((PageTargetCountElement)modelElement);
        }

        /// <summary><inheritDoc/></summary>
        protected override TextRenderer CreateCopy(GlyphLine gl, PdfFont font) {
            if (typeof(iText.Html2pdf.Attach.Impl.Layout.PageTargetCountRenderer) != this.GetType()) {
                ILogger logger = ITextLogManager.GetLogger(typeof(iText.Html2pdf.Attach.Impl.Layout.PageTargetCountRenderer
                    ));
                logger.LogError(MessageFormatUtil.Format(iText.IO.Logs.IoLogMessageConstant.CREATE_COPY_SHOULD_BE_OVERRIDDEN
                    ));
            }
            iText.Html2pdf.Attach.Impl.Layout.PageTargetCountRenderer copy = new iText.Html2pdf.Attach.Impl.Layout.PageTargetCountRenderer
                (this);
            copy.SetProcessedGlyphLineAndFont(gl, font);
            return copy;
        }

        /// <summary><inheritDoc/></summary>
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
