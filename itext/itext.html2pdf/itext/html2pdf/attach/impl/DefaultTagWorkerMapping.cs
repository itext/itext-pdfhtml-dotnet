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
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Resolve.Func.Counter;
using iText.Html2pdf.Html;
using iText.Html2pdf.Util;
using iText.StyledXmlParser.Css.Page;
using iText.StyledXmlParser.Css.Pseudo;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl {
//\cond DO_NOT_DOCUMENT
    /// <summary>
    /// Contains the actual mapping of the
    /// <see cref="DefaultTagWorkerFactory"/>.
    /// </summary>
    internal class DefaultTagWorkerMapping {
        /// <summary>The worker mapping.</summary>
        private static TagProcessorMapping<DefaultTagWorkerMapping.ITagWorkerCreator> workerMapping;

        static DefaultTagWorkerMapping() {
            workerMapping = new TagProcessorMapping<DefaultTagWorkerMapping.ITagWorkerCreator>();
            workerMapping.PutMapping(TagConstants.A, (lhs, rhs) => new ATagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.ABBR, (lhs, rhs) => new AbbrTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.ADDRESS, (lhs, rhs) => new DivTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.ARTICLE, (lhs, rhs) => new DivTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.ASIDE, (lhs, rhs) => new DivTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.B, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.BDI, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.BDO, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.BLOCKQUOTE, (lhs, rhs) => new DivTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.BODY, (lhs, rhs) => new BodyTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.BR, (lhs, rhs) => new BrTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.BUTTON, (lhs, rhs) => new ButtonTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.CAPTION, (lhs, rhs) => new CaptionTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.CENTER, (lhs, rhs) => new DivTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.CITE, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.CODE, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.COL, (lhs, rhs) => new ColTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.COLGROUP, (lhs, rhs) => new ColgroupTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.DD, (lhs, rhs) => new LiTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.DEL, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.DFN, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.DIV, (lhs, rhs) => new DivTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.DL, (lhs, rhs) => new UlOlTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.DT, (lhs, rhs) => new LiTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.EM, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.FIELDSET, (lhs, rhs) => new DivTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.FIGCAPTION, (lhs, rhs) => new DivTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.FIGURE, (lhs, rhs) => new DivTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.FONT, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.FOOTER, (lhs, rhs) => new DivTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.FORM, (lhs, rhs) => new DivTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.H1, (lhs, rhs) => new HTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.H2, (lhs, rhs) => new HTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.H3, (lhs, rhs) => new HTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.H4, (lhs, rhs) => new HTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.H5, (lhs, rhs) => new HTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.H6, (lhs, rhs) => new HTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.HEADER, (lhs, rhs) => new DivTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.HR, (lhs, rhs) => new HrTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.HTML, (lhs, rhs) => new HtmlTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.I, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.IMG, (lhs, rhs) => new ImgTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.INPUT, (lhs, rhs) => new InputTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.INS, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.KBD, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.LABEL, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.LEGEND, (lhs, rhs) => new DivTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.LI, (lhs, rhs) => new LiTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.LINK, (lhs, rhs) => new LinkTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.MAIN, (lhs, rhs) => new DivTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.MARK, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.META, (lhs, rhs) => new MetaTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.NAV, (lhs, rhs) => new DivTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.OBJECT, (lhs, rhs) => new ObjectTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.OL, (lhs, rhs) => new UlOlTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.OPTGROUP, (lhs, rhs) => new OptGroupTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.OPTION, (lhs, rhs) => new OptionTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.P, (lhs, rhs) => new PTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.PRE, (lhs, rhs) => new PreTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.Q, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.S, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.SAMP, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.SECTION, (lhs, rhs) => new DivTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.SELECT, (lhs, rhs) => new SelectTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.SMALL, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.SPAN, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.STRIKE, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.STRONG, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.SUB, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.SUP, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.SVG, (lhs, rhs) => new SvgTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.TABLE, (lhs, rhs) => new TableTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.TD, (lhs, rhs) => new TdTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.TEXTAREA, (lhs, rhs) => new TextAreaTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.TFOOT, (lhs, rhs) => new TableFooterTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.TH, (lhs, rhs) => new ThTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.THEAD, (lhs, rhs) => new TableHeaderTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.TIME, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.TITLE, (lhs, rhs) => new TitleTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.TR, (lhs, rhs) => new TrTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.TT, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.U, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.UL, (lhs, rhs) => new UlOlTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.VAR, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            String placeholderPseudoElemName = CssPseudoElementUtil.CreatePseudoElementTagName(CssConstants.PLACEHOLDER
                );
            workerMapping.PutMapping(placeholderPseudoElemName, (lhs, rhs) => new PlaceholderTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.UL, CssConstants.INLINE, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.LI, CssConstants.INLINE, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.LI, CssConstants.INLINE_BLOCK, (lhs, rhs) => new DivTagWorker(lhs, rhs
                ));
            workerMapping.PutMapping(TagConstants.LI, CssConstants.BLOCK, (lhs, rhs) => new DivTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.DD, CssConstants.INLINE, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.DT, CssConstants.INLINE, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.SPAN, CssConstants.BLOCK, (lhs, rhs) => new DivTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.SPAN, CssConstants.INLINE_BLOCK, (lhs, rhs) => new DivTagWorker(lhs, 
                rhs));
            workerMapping.PutMapping(TagConstants.A, CssConstants.BLOCK, (lhs, rhs) => new ABlockTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.A, CssConstants.INLINE_BLOCK, (lhs, rhs) => new ABlockTagWorker(lhs, 
                rhs));
            workerMapping.PutMapping(TagConstants.A, CssConstants.TABLE_CELL, (lhs, rhs) => new ABlockTagWorker(lhs, rhs
                ));
            workerMapping.PutMapping(TagConstants.LABEL, CssConstants.BLOCK, (lhs, rhs) => new DivTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.LABEL, CssConstants.INLINE_BLOCK, (lhs, rhs) => new DivTagWorker(lhs
                , rhs));
            workerMapping.PutMapping(TagConstants.DIV, CssConstants.TABLE, (lhs, rhs) => new DisplayTableTagWorker(lhs
                , rhs));
            workerMapping.PutMapping(TagConstants.DIV, CssConstants.TABLE_ROW, (lhs, rhs) => new DisplayTableRowTagWorker
                (lhs, rhs));
            workerMapping.PutMapping(TagConstants.DIV, CssConstants.INLINE, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(TagConstants.DIV, CssConstants.INLINE_TABLE, (lhs, rhs) => new DisplayTableTagWorker
                (lhs, rhs));
            workerMapping.PutMapping(TagConstants.DIV, CssConstants.TABLE_CELL, (lhs, rhs) => new TdTagWorker(lhs, rhs
                ));
            workerMapping.PutMapping(TagConstants.DIV, CssConstants.FLEX, (lhs, rhs) => new DisplayFlexTagWorker(lhs, 
                rhs));
            workerMapping.PutMapping(TagConstants.SPAN, CssConstants.FLEX, (lhs, rhs) => new DisplayFlexTagWorker(lhs, 
                rhs));
            //TODO DEVSIX-8335 remove check for css grid enabled logic
            workerMapping.PutMapping(TagConstants.DIV, CssConstants.GRID, (lhs, rhs) => rhs.IsCssGridEnabled() ? new DisplayGridTagWorker
                (lhs, rhs) : new DivTagWorker(lhs, rhs));
            // pseudo elements mapping
            String beforePseudoElemName = CssPseudoElementUtil.CreatePseudoElementTagName(CssConstants.BEFORE);
            String afterPseudoElemName = CssPseudoElementUtil.CreatePseudoElementTagName(CssConstants.AFTER);
            workerMapping.PutMapping(beforePseudoElemName, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(afterPseudoElemName, (lhs, rhs) => new SpanTagWorker(lhs, rhs));
            workerMapping.PutMapping(beforePseudoElemName, CssConstants.INLINE_BLOCK, (lhs, rhs) => new DivTagWorker(lhs
                , rhs));
            workerMapping.PutMapping(afterPseudoElemName, CssConstants.INLINE_BLOCK, (lhs, rhs) => new DivTagWorker(lhs
                , rhs));
            workerMapping.PutMapping(beforePseudoElemName, CssConstants.BLOCK, (lhs, rhs) => new DivTagWorker(lhs, rhs
                ));
            workerMapping.PutMapping(afterPseudoElemName, CssConstants.BLOCK, (lhs, rhs) => new DivTagWorker(lhs, rhs)
                );
            // For now behaving like display:block in display:table case is sufficient
            workerMapping.PutMapping(beforePseudoElemName, CssConstants.TABLE, (lhs, rhs) => new DivTagWorker(lhs, rhs
                ));
            workerMapping.PutMapping(afterPseudoElemName, CssConstants.TABLE, (lhs, rhs) => new DivTagWorker(lhs, rhs)
                );
            workerMapping.PutMapping(CssPseudoElementUtil.CreatePseudoElementTagName(TagConstants.IMG), (lhs, rhs) => 
                new ImgTagWorker(lhs, rhs));
            workerMapping.PutMapping(CssPseudoElementUtil.CreatePseudoElementTagName(TagConstants.DIV), (lhs, rhs) => 
                new DivTagWorker(lhs, rhs));
            // custom elements mapping, implementation-specific
            workerMapping.PutMapping(PageCountElementNode.PAGE_COUNTER_TAG, (lhs, rhs) => new PageCountWorker(lhs, rhs
                ));
            workerMapping.PutMapping(PageMarginBoxContextNode.PAGE_MARGIN_BOX_TAG, (lhs, rhs) => new PageMarginBoxWorker
                (lhs, rhs));
        }

//\cond DO_NOT_DOCUMENT
        /// <summary>Gets the default tag worker mapping.</summary>
        /// <returns>the default mapping</returns>
        internal virtual TagProcessorMapping<DefaultTagWorkerMapping.ITagWorkerCreator> GetDefaultTagWorkerMapping
            () {
            return workerMapping;
        }
//\endcond

//\cond DO_NOT_DOCUMENT
        /// <summary>
        /// Instantiates a new
        /// <see cref="DefaultTagWorkerMapping"/>
        /// instance.
        /// </summary>
        internal DefaultTagWorkerMapping() {
        }
//\endcond

        public delegate ITagWorker ITagWorkerCreator(IElementNode elementNode, ProcessorContext processorContext);
    }
//\endcond
}
