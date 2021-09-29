/*
This file is part of the iText (R) project.
Copyright (c) 1998-2021 iText Group NV
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
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Resolve.Func.Counter;
using iText.Html2pdf.Html;
using iText.Html2pdf.Util;
using iText.StyledXmlParser.Css.Page;
using iText.StyledXmlParser.Css.Pseudo;

namespace iText.Html2pdf.Css.Apply.Impl {
    /// <summary>Class that contains the default mapping between CSS keys and CSS appliers.</summary>
    internal class DefaultTagCssApplierMapping {
        /// <summary>
        /// Creates a new
        /// <see cref="DefaultTagCssApplierMapping"/>
        /// instance.
        /// </summary>
        internal DefaultTagCssApplierMapping() {
        }

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

        /// <summary>Gets the default CSS applier mapping.</summary>
        /// <returns>the default CSS applier mapping</returns>
        internal virtual TagProcessorMapping<DefaultTagCssApplierMapping.ICssApplierCreator> GetDefaultCssApplierMapping
            () {
            return mapping;
        }

        internal delegate ICssApplier ICssApplierCreator();
    }
}
