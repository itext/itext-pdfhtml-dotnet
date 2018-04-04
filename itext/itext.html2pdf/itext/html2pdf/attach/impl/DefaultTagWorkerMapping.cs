/*
This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
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
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Page;
using iText.Html2pdf.Css.Pseudo;
using iText.Html2pdf.Css.Resolve.Func.Counter;
using iText.Html2pdf.Html;
using iText.Html2pdf.Util;

namespace iText.Html2pdf.Attach.Impl {
    /// <summary>
    /// Contains the actual mapping of the
    /// <see cref="DefaultTagWorkerFactory"/>
    /// .
    /// </summary>
    internal class DefaultTagWorkerMapping {
        /// <summary>
        /// Instantiates a new
        /// <see cref="DefaultTagWorkerMapping"/>
        /// instance.
        /// </summary>
        private DefaultTagWorkerMapping() {
        }

        /// <summary>The worker mapping.</summary>
        private static TagProcessorMapping workerMapping;

        static DefaultTagWorkerMapping() {
            workerMapping = new TagProcessorMapping();
            workerMapping.PutMapping(TagConstants.A, typeof(ATagWorker));
            workerMapping.PutMapping(TagConstants.ABBR, typeof(AbbrTagWorker));
            workerMapping.PutMapping(TagConstants.ADDRESS, typeof(DivTagWorker));
            workerMapping.PutMapping(TagConstants.ARTICLE, typeof(DivTagWorker));
            workerMapping.PutMapping(TagConstants.ASIDE, typeof(DivTagWorker));
            workerMapping.PutMapping(TagConstants.B, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.BDI, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.BDO, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.BLOCKQUOTE, typeof(DivTagWorker));
            workerMapping.PutMapping(TagConstants.BODY, typeof(BodyTagWorker));
            workerMapping.PutMapping(TagConstants.BR, typeof(BrTagWorker));
            workerMapping.PutMapping(TagConstants.BUTTON, typeof(ButtonTagWorker));
            workerMapping.PutMapping(TagConstants.CENTER, typeof(DivTagWorker));
            workerMapping.PutMapping(TagConstants.CITE, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.CODE, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.COL, typeof(ColTagWorker));
            workerMapping.PutMapping(TagConstants.COLGROUP, typeof(ColgroupTagWorker));
            workerMapping.PutMapping(TagConstants.DD, typeof(LiTagWorker));
            workerMapping.PutMapping(TagConstants.DEL, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.DFN, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.DIV, typeof(DivTagWorker));
            workerMapping.PutMapping(TagConstants.DL, typeof(UlOlTagWorker));
            workerMapping.PutMapping(TagConstants.DT, typeof(LiTagWorker));
            workerMapping.PutMapping(TagConstants.EM, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.FIELDSET, typeof(DivTagWorker));
            workerMapping.PutMapping(TagConstants.FIGCAPTION, typeof(DivTagWorker));
            workerMapping.PutMapping(TagConstants.FIGURE, typeof(DivTagWorker));
            workerMapping.PutMapping(TagConstants.FONT, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.FOOTER, typeof(DivTagWorker));
            workerMapping.PutMapping(TagConstants.FORM, typeof(DivTagWorker));
            workerMapping.PutMapping(TagConstants.H1, typeof(HTagWorker));
            workerMapping.PutMapping(TagConstants.H2, typeof(HTagWorker));
            workerMapping.PutMapping(TagConstants.H3, typeof(HTagWorker));
            workerMapping.PutMapping(TagConstants.H4, typeof(HTagWorker));
            workerMapping.PutMapping(TagConstants.H5, typeof(HTagWorker));
            workerMapping.PutMapping(TagConstants.H6, typeof(HTagWorker));
            workerMapping.PutMapping(TagConstants.HEADER, typeof(DivTagWorker));
            workerMapping.PutMapping(TagConstants.HR, typeof(HrTagWorker));
            workerMapping.PutMapping(TagConstants.HTML, typeof(HtmlTagWorker));
            workerMapping.PutMapping(TagConstants.I, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.IMG, typeof(ImgTagWorker));
            workerMapping.PutMapping(TagConstants.INPUT, typeof(InputTagWorker));
            workerMapping.PutMapping(TagConstants.INS, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.KBD, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.LABEL, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.LEGEND, typeof(DivTagWorker));
            workerMapping.PutMapping(TagConstants.LI, typeof(LiTagWorker));
            workerMapping.PutMapping(TagConstants.LINK, typeof(LinkTagWorker));
            workerMapping.PutMapping(TagConstants.MAIN, typeof(DivTagWorker));
            workerMapping.PutMapping(TagConstants.MARK, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.META, typeof(MetaTagWorker));
            workerMapping.PutMapping(TagConstants.NAV, typeof(DivTagWorker));
            workerMapping.PutMapping(TagConstants.OL, typeof(UlOlTagWorker));
            workerMapping.PutMapping(TagConstants.P, typeof(PTagWorker));
            workerMapping.PutMapping(TagConstants.PRE, typeof(PreTagWorker));
            workerMapping.PutMapping(TagConstants.Q, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.S, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.SAMP, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.SECTION, typeof(DivTagWorker));
            workerMapping.PutMapping(TagConstants.SMALL, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.SPAN, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.STRIKE, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.STRONG, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.SUB, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.SUP, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.TABLE, typeof(TableTagWorker));
            workerMapping.PutMapping(TagConstants.TD, typeof(TdTagWorker));
            workerMapping.PutMapping(TagConstants.TEXTAREA, typeof(TextAreaTagWorker));
            workerMapping.PutMapping(TagConstants.TFOOT, typeof(TableFooterTagWorker));
            workerMapping.PutMapping(TagConstants.TH, typeof(ThTagWorker));
            workerMapping.PutMapping(TagConstants.THEAD, typeof(TableHeaderTagWorker));
            workerMapping.PutMapping(TagConstants.TIME, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.TITLE, typeof(TitleTagWorker));
            workerMapping.PutMapping(TagConstants.TR, typeof(TrTagWorker));
            workerMapping.PutMapping(TagConstants.TT, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.U, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.UL, typeof(UlOlTagWorker));
            workerMapping.PutMapping(TagConstants.VAR, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.UL, CssConstants.INLINE, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.LI, CssConstants.INLINE, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.LI, CssConstants.INLINE_BLOCK, typeof(DivTagWorker));
            workerMapping.PutMapping(TagConstants.DD, CssConstants.INLINE, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.DT, CssConstants.INLINE, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.SPAN, CssConstants.BLOCK, typeof(DivTagWorker));
            workerMapping.PutMapping(TagConstants.SPAN, CssConstants.INLINE_BLOCK, typeof(DivTagWorker));
            workerMapping.PutMapping(TagConstants.A, CssConstants.BLOCK, typeof(ABlockTagWorker));
            workerMapping.PutMapping(TagConstants.A, CssConstants.INLINE_BLOCK, typeof(ABlockTagWorker));
            workerMapping.PutMapping(TagConstants.A, CssConstants.TABLE_CELL, typeof(ABlockTagWorker));
            workerMapping.PutMapping(TagConstants.DIV, CssConstants.TABLE, typeof(DisplayTableTagWorker));
            workerMapping.PutMapping(TagConstants.DIV, CssConstants.TABLE_ROW, typeof(DisplayTableRowTagWorker));
            workerMapping.PutMapping(TagConstants.DIV, CssConstants.INLINE, typeof(SpanTagWorker));
            workerMapping.PutMapping(TagConstants.DIV, CssConstants.INLINE_TABLE, typeof(DisplayTableTagWorker));
            workerMapping.PutMapping(TagConstants.DIV, CssConstants.TABLE_CELL, typeof(TdTagWorker));
            // pseudo elements mapping
            String beforePseudoElemName = CssPseudoElementUtil.CreatePseudoElementTagName(CssConstants.BEFORE);
            String afterPseudoElemName = CssPseudoElementUtil.CreatePseudoElementTagName(CssConstants.AFTER);
            workerMapping.PutMapping(beforePseudoElemName, typeof(SpanTagWorker));
            workerMapping.PutMapping(afterPseudoElemName, typeof(SpanTagWorker));
            workerMapping.PutMapping(beforePseudoElemName, CssConstants.INLINE_BLOCK, typeof(DivTagWorker));
            workerMapping.PutMapping(afterPseudoElemName, CssConstants.INLINE_BLOCK, typeof(DivTagWorker));
            workerMapping.PutMapping(beforePseudoElemName, CssConstants.BLOCK, typeof(DivTagWorker));
            workerMapping.PutMapping(afterPseudoElemName, CssConstants.BLOCK, typeof(DivTagWorker));
            // For now behaving like display:block in display:table case is sufficient
            workerMapping.PutMapping(beforePseudoElemName, CssConstants.TABLE, typeof(DivTagWorker));
            workerMapping.PutMapping(afterPseudoElemName, CssConstants.TABLE, typeof(DivTagWorker));
            workerMapping.PutMapping(CssPseudoElementUtil.CreatePseudoElementTagName(TagConstants.IMG), typeof(ImgTagWorker
                ));
            // custom elements mapping, implementation-specific
            workerMapping.PutMapping(PageCountElementNode.PAGE_COUNTER_TAG, typeof(PageCountWorker));
            workerMapping.PutMapping(PageMarginBoxContextNode.PAGE_MARGIN_BOX_TAG, typeof(PageMarginBoxWorker));
        }

        /// <summary>Gets the default tag worker mapping.</summary>
        /// <returns>the default mapping</returns>
        internal static TagProcessorMapping GetDefaultTagWorkerMapping() {
            return workerMapping;
        }
    }
}
