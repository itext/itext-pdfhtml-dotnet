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
        private DefaultTagCssApplierMapping() {
        }

        /// <summary>The default mapping.</summary>
        private static TagProcessorMapping mapping;

        static DefaultTagCssApplierMapping() {
            mapping = new TagProcessorMapping();
            mapping.PutMapping(TagConstants.A, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.ABBR, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.ADDRESS, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.ARTICLE, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.ASIDE, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.B, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.BDI, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.BDO, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.BLOCKQUOTE, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.BODY, typeof(BodyTagCssApplier));
            mapping.PutMapping(TagConstants.BUTTON, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.CAPTION, typeof(CaptionCssApplier));
            mapping.PutMapping(TagConstants.CENTER, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.CITE, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.CODE, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.COL, typeof(ColTagCssApplier));
            mapping.PutMapping(TagConstants.COLGROUP, typeof(ColgroupTagCssApplier));
            mapping.PutMapping(TagConstants.DD, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.DEL, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.DFN, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.DIV, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.DL, typeof(DlTagCssApplier));
            mapping.PutMapping(TagConstants.DT, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.EM, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.FIELDSET, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.FIGCAPTION, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.FIGURE, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.FONT, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.FOOTER, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.FORM, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.H1, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.H2, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.H3, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.H4, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.H5, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.H6, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.HEADER, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.HR, typeof(HrTagCssApplier));
            mapping.PutMapping(TagConstants.HTML, typeof(HtmlTagCssApplier));
            mapping.PutMapping(TagConstants.I, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.IMG, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.INPUT, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.INS, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.KBD, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.LABEL, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.LEGEND, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.LI, typeof(LiTagCssApplier));
            mapping.PutMapping(TagConstants.MAIN, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.MARK, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.NAV, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.OBJECT, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.OL, typeof(UlOlTagCssApplier));
            mapping.PutMapping(TagConstants.OPTGROUP, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.OPTION, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.P, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.PRE, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.Q, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.S, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.SAMP, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.SECTION, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.SELECT, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.SMALL, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.SPAN, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.STRIKE, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.STRONG, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.SUB, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.SUP, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.SVG, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.TABLE, typeof(TableTagCssApplier));
            mapping.PutMapping(TagConstants.TEXTAREA, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.TD, typeof(TdTagCssApplier));
            mapping.PutMapping(TagConstants.TFOOT, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.TH, typeof(TdTagCssApplier));
            mapping.PutMapping(TagConstants.THEAD, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.TIME, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.TR, typeof(TrTagCssApplier));
            mapping.PutMapping(TagConstants.TT, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.U, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.UL, typeof(UlOlTagCssApplier));
            mapping.PutMapping(TagConstants.VAR, typeof(SpanTagCssApplier));
            String placeholderPseudoElemName = CssPseudoElementUtil.CreatePseudoElementTagName(CssConstants.PLACEHOLDER
                );
            mapping.PutMapping(placeholderPseudoElemName, typeof(PlaceholderCssApplier));
            mapping.PutMapping(TagConstants.DIV, CssConstants.INLINE, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.UL, CssConstants.INLINE, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.LI, CssConstants.INLINE, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.LI, CssConstants.INLINE_BLOCK, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.DD, CssConstants.INLINE, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.DT, CssConstants.INLINE, typeof(SpanTagCssApplier));
            mapping.PutMapping(TagConstants.SPAN, CssConstants.BLOCK, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.SPAN, CssConstants.INLINE_BLOCK, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.A, CssConstants.INLINE_BLOCK, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.A, CssConstants.BLOCK, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.A, CssConstants.TABLE_CELL, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.LABEL, CssConstants.BLOCK, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.LABEL, CssConstants.INLINE_BLOCK, typeof(BlockCssApplier));
            mapping.PutMapping(TagConstants.DIV, CssConstants.TABLE, typeof(TableTagCssApplier));
            mapping.PutMapping(TagConstants.DIV, CssConstants.TABLE_CELL, typeof(TdTagCssApplier));
            mapping.PutMapping(TagConstants.DIV, CssConstants.TABLE_ROW, typeof(DisplayTableRowTagCssApplier));
            mapping.PutMapping(TagConstants.DIV, CssConstants.FLEX, typeof(DisplayFlexTagCssApplier));
            mapping.PutMapping(TagConstants.SPAN, CssConstants.FLEX, typeof(DisplayFlexTagCssApplier));
            // pseudo elements mapping
            String beforePseudoElemName = CssPseudoElementUtil.CreatePseudoElementTagName(CssConstants.BEFORE);
            String afterPseudoElemName = CssPseudoElementUtil.CreatePseudoElementTagName(CssConstants.AFTER);
            mapping.PutMapping(beforePseudoElemName, typeof(SpanTagCssApplier));
            mapping.PutMapping(afterPseudoElemName, typeof(SpanTagCssApplier));
            mapping.PutMapping(beforePseudoElemName, CssConstants.INLINE_BLOCK, typeof(BlockCssApplier));
            mapping.PutMapping(afterPseudoElemName, CssConstants.INLINE_BLOCK, typeof(BlockCssApplier));
            mapping.PutMapping(beforePseudoElemName, CssConstants.BLOCK, typeof(BlockCssApplier));
            mapping.PutMapping(afterPseudoElemName, CssConstants.BLOCK, typeof(BlockCssApplier));
            // For now behaving like display:block in display:table case is sufficient
            mapping.PutMapping(beforePseudoElemName, CssConstants.TABLE, typeof(BlockCssApplier));
            mapping.PutMapping(afterPseudoElemName, CssConstants.TABLE, typeof(BlockCssApplier));
            mapping.PutMapping(CssPseudoElementUtil.CreatePseudoElementTagName(TagConstants.IMG), typeof(BlockCssApplier
                ));
            mapping.PutMapping(CssPseudoElementUtil.CreatePseudoElementTagName(TagConstants.DIV), typeof(CssContentLinearGradientApplier
                ));
            // custom elements mapping, implementation-specific
            mapping.PutMapping(PageCountElementNode.PAGE_COUNTER_TAG, typeof(SpanTagCssApplier));
            mapping.PutMapping(PageMarginBoxContextNode.PAGE_MARGIN_BOX_TAG, typeof(PageMarginBoxCssApplier));
        }

        /// <summary>Gets the default CSS applier mapping.</summary>
        /// <returns>the default CSS applier mapping</returns>
        internal static TagProcessorMapping GetDefaultCssApplierMapping() {
            return mapping;
        }
    }
}
