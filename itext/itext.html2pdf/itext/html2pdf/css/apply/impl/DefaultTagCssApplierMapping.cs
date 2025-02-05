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
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Resolve.Func.Counter;
using iText.Html2pdf.Html;
using iText.Html2pdf.Util;
using iText.StyledXmlParser.Css.Page;
using iText.StyledXmlParser.Css.Pseudo;

namespace iText.Html2pdf.Css.Apply.Impl {
//\cond DO_NOT_DOCUMENT
    /// <summary>Class that contains the default mapping between CSS keys and CSS appliers.</summary>
    internal class DefaultTagCssApplierMapping {
//\cond DO_NOT_DOCUMENT
        /// <summary>
        /// Creates a new
        /// <see cref="DefaultTagCssApplierMapping"/>
        /// instance.
        /// </summary>
        internal DefaultTagCssApplierMapping() {
        }
//\endcond

        /// <summary>The default mapping.</summary>
        private static TagProcessorMapping<DefaultTagCssApplierMapping.ICssApplierCreator> mapping;

        static DefaultTagCssApplierMapping() {
            mapping = new TagProcessorMapping<DefaultTagCssApplierMapping.ICssApplierCreator>();
            mapping.PutMapping(TagConstants.A, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.ABBR, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.ADDRESS, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.ARTICLE, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.ASIDE, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.B, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.BDI, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.BDO, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.BLOCKQUOTE, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.BODY, () => new BodyTagCssApplier());
            mapping.PutMapping(TagConstants.BUTTON, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.CAPTION, () => new CaptionCssApplier());
            mapping.PutMapping(TagConstants.CENTER, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.CITE, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.CODE, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.COL, () => new ColTagCssApplier());
            mapping.PutMapping(TagConstants.COLGROUP, () => new ColgroupTagCssApplier());
            mapping.PutMapping(TagConstants.DD, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.DEL, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.DFN, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.DIV, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.DL, () => new DlTagCssApplier());
            mapping.PutMapping(TagConstants.DT, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.EM, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.FIELDSET, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.FIGCAPTION, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.FIGURE, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.FONT, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.FOOTER, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.FORM, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.H1, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.H2, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.H3, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.H4, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.H5, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.H6, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.HEADER, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.HR, () => new HrTagCssApplier());
            mapping.PutMapping(TagConstants.HTML, () => new HtmlTagCssApplier());
            mapping.PutMapping(TagConstants.I, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.IMG, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.INPUT, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.INS, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.KBD, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.LABEL, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.LEGEND, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.LI, () => new LiTagCssApplier());
            mapping.PutMapping(TagConstants.MAIN, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.MARK, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.NAV, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.OBJECT, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.OL, () => new UlOlTagCssApplier());
            mapping.PutMapping(TagConstants.OPTGROUP, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.OPTION, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.P, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.PRE, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.Q, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.S, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.SAMP, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.SECTION, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.SELECT, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.SMALL, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.SPAN, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.STRIKE, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.STRONG, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.SUB, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.SUP, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.SVG, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.TABLE, () => new TableTagCssApplier());
            mapping.PutMapping(TagConstants.TEXTAREA, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.TD, () => new TdTagCssApplier());
            mapping.PutMapping(TagConstants.TFOOT, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.TH, () => new TdTagCssApplier());
            mapping.PutMapping(TagConstants.THEAD, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.TIME, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.TR, () => new TrTagCssApplier());
            mapping.PutMapping(TagConstants.TT, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.U, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.UL, () => new UlOlTagCssApplier());
            mapping.PutMapping(TagConstants.VAR, () => new SpanTagCssApplier());
            String placeholderPseudoElemName = CssPseudoElementUtil.CreatePseudoElementTagName(CssConstants.PLACEHOLDER
                );
            mapping.PutMapping(placeholderPseudoElemName, () => new PlaceholderCssApplier());
            mapping.PutMapping(TagConstants.DIV, CssConstants.INLINE, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.UL, CssConstants.INLINE, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.LI, CssConstants.INLINE, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.LI, CssConstants.INLINE_BLOCK, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.LI, CssConstants.BLOCK, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.DD, CssConstants.INLINE, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.DT, CssConstants.INLINE, () => new SpanTagCssApplier());
            mapping.PutMapping(TagConstants.SPAN, CssConstants.BLOCK, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.SPAN, CssConstants.INLINE_BLOCK, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.A, CssConstants.INLINE_BLOCK, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.A, CssConstants.BLOCK, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.A, CssConstants.TABLE_CELL, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.LABEL, CssConstants.BLOCK, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.LABEL, CssConstants.INLINE_BLOCK, () => new BlockCssApplier());
            mapping.PutMapping(TagConstants.DIV, CssConstants.TABLE, () => new TableTagCssApplier());
            mapping.PutMapping(TagConstants.DIV, CssConstants.TABLE_CELL, () => new TdTagCssApplier());
            mapping.PutMapping(TagConstants.DIV, CssConstants.TABLE_ROW, () => new DisplayTableRowTagCssApplier());
            mapping.PutMapping(TagConstants.DIV, CssConstants.FLEX, () => new DisplayFlexTagCssApplier());
            mapping.PutMapping(TagConstants.SPAN, CssConstants.FLEX, () => new DisplayFlexTagCssApplier());
            mapping.PutMapping(TagConstants.DIV, CssConstants.GRID, () => new DisplayGridTagCssApplier());
            // pseudo elements mapping
            String beforePseudoElemName = CssPseudoElementUtil.CreatePseudoElementTagName(CssConstants.BEFORE);
            String afterPseudoElemName = CssPseudoElementUtil.CreatePseudoElementTagName(CssConstants.AFTER);
            mapping.PutMapping(beforePseudoElemName, () => new SpanTagCssApplier());
            mapping.PutMapping(afterPseudoElemName, () => new SpanTagCssApplier());
            mapping.PutMapping(beforePseudoElemName, CssConstants.INLINE_BLOCK, () => new BlockCssApplier());
            mapping.PutMapping(afterPseudoElemName, CssConstants.INLINE_BLOCK, () => new BlockCssApplier());
            mapping.PutMapping(beforePseudoElemName, CssConstants.BLOCK, () => new BlockCssApplier());
            mapping.PutMapping(afterPseudoElemName, CssConstants.BLOCK, () => new BlockCssApplier());
            // For now behaving like display:block in display:table case is sufficient
            mapping.PutMapping(beforePseudoElemName, CssConstants.TABLE, () => new BlockCssApplier());
            mapping.PutMapping(afterPseudoElemName, CssConstants.TABLE, () => new BlockCssApplier());
            mapping.PutMapping(CssPseudoElementUtil.CreatePseudoElementTagName(TagConstants.IMG), () => new BlockCssApplier
                ());
            mapping.PutMapping(CssPseudoElementUtil.CreatePseudoElementTagName(TagConstants.DIV), () => new CssContentLinearGradientApplier
                ());
            // custom elements mapping, implementation-specific
            mapping.PutMapping(PageCountElementNode.PAGE_COUNTER_TAG, () => new SpanTagCssApplier());
            mapping.PutMapping(PageMarginBoxContextNode.PAGE_MARGIN_BOX_TAG, () => new PageMarginBoxCssApplier());
        }

//\cond DO_NOT_DOCUMENT
        /// <summary>Gets the default CSS applier mapping.</summary>
        /// <returns>the default CSS applier mapping</returns>
        internal virtual TagProcessorMapping<DefaultTagCssApplierMapping.ICssApplierCreator> GetDefaultCssApplierMapping
            () {
            return mapping;
        }
//\endcond

        public delegate ICssApplier ICssApplierCreator();
    }
//\endcond
}
