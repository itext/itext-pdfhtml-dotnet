/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 Apryse Group NV
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
using iText.Html2pdf.Css.Resolve.Func.Counter;
using iText.Html2pdf.Html;
using iText.Layout.Element;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout {
    /// <summary>
    /// <see cref="iText.Layout.Element.Text"/>
    /// implementation to be used for the page count.
    /// </summary>
    public class PageCountElement : Text {
        private readonly CounterDigitsGlyphStyle digitsGlyphStyle;

        /// <summary>
        /// Instantiates a new
        /// <see cref="PageCountElement"/>.
        /// </summary>
        public PageCountElement()
            : base(
                        // Workaround to match correct font containing number glyphs
                        "1234567890") {
            digitsGlyphStyle = CounterDigitsGlyphStyle.DEFAULT;
        }

        /// <summary>
        /// Instantiates a new
        /// <see cref="PageCountElement"/>.
        /// </summary>
        /// <param name="digitsGlyphStyle">digits glyph style</param>
        public PageCountElement(CounterDigitsGlyphStyle digitsGlyphStyle)
            : base(HtmlUtils
                        // Workaround to match correct font containing number glyphs
                        .GetAllNumberGlyphsForStyle(digitsGlyphStyle)) {
            this.digitsGlyphStyle = digitsGlyphStyle;
        }

        /// <summary>Gets glyph style for digits.</summary>
        /// <returns>name of the glyph style</returns>
        public virtual CounterDigitsGlyphStyle GetDigitsGlyphStyle() {
            return digitsGlyphStyle;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.layout.element.Text#makeNewRenderer()
        */
        protected override IRenderer MakeNewRenderer() {
            return new PageCountRenderer(this);
        }
    }
}
