/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2017 iText Group NV
    Authors: iText Software.

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
    address: sales@itextpdf.com */
using System;
using System.Collections.Generic;
using iText.Html2pdf.Css.Apply.Impl;
using iText.Html2pdf.Html;
using iText.IO.Util;

namespace iText.Html2pdf.Css.Apply {
    public class DefaultTagCssApplierMapping {
        private static IDictionary<String, Type> mapping;

        public static IDictionary<String, Type> GetDefaultCssApplierMapping() {
            if (mapping == null) {
                IDictionary<String, Type> buildMap = new Dictionary<String, Type>();
                buildMap[TagConstants.A] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.ABBR] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.ADDRESS] = typeof(BlockCssApplier);
                buildMap[TagConstants.ARTICLE] = typeof(BlockCssApplier);
                buildMap[TagConstants.ASIDE] = typeof(BlockCssApplier);
                buildMap[TagConstants.B] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.BDI] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.BDO] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.BLOCKQUOTE] = typeof(BlockCssApplier);
                buildMap[TagConstants.BODY] = typeof(BodyTagCssApplier);
                //buildMap.put(TagConstants.CAPTION,SpanTagCssApplier.class);
                buildMap[TagConstants.CENTER] = typeof(BlockCssApplier);
                buildMap[TagConstants.CITE] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.CODE] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.EM] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.DEL] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.DFN] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.DT] = typeof(BlockCssApplier);
                buildMap[TagConstants.DD] = typeof(BlockCssApplier);
                buildMap[TagConstants.DIV] = typeof(BlockCssApplier);
                buildMap[TagConstants.FONT] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.FOOTER] = typeof(BlockCssApplier);
                buildMap[TagConstants.FIGCAPTION] = typeof(BlockCssApplier);
                buildMap[TagConstants.FIGURE] = typeof(BlockCssApplier);
                buildMap[TagConstants.H1] = typeof(BlockCssApplier);
                buildMap[TagConstants.H2] = typeof(BlockCssApplier);
                buildMap[TagConstants.H3] = typeof(BlockCssApplier);
                buildMap[TagConstants.H4] = typeof(BlockCssApplier);
                buildMap[TagConstants.H5] = typeof(BlockCssApplier);
                buildMap[TagConstants.H6] = typeof(BlockCssApplier);
                buildMap[TagConstants.HEADER] = typeof(BlockCssApplier);
                buildMap[TagConstants.HR] = typeof(BlockCssApplier);
                buildMap[TagConstants.IMG] = typeof(BlockCssApplier);
                buildMap[TagConstants.MAIN] = typeof(BlockCssApplier);
                buildMap[TagConstants.NAV] = typeof(BlockCssApplier);
                buildMap[TagConstants.P] = typeof(BlockCssApplier);
                buildMap[TagConstants.SECTION] = typeof(BlockCssApplier);
                buildMap[TagConstants.TABLE] = typeof(BlockCssApplier);
                buildMap[TagConstants.TFOOT] = typeof(BlockCssApplier);
                buildMap[TagConstants.THEAD] = typeof(BlockCssApplier);
                buildMap[TagConstants.DL] = typeof(DlTagCssApplier);
                buildMap[TagConstants.HTML] = typeof(HtmlTagCssApplier);
                buildMap[TagConstants.I] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.INS] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.KBD] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.LI] = typeof(LiTagCssApplier);
                buildMap[TagConstants.MARK] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.OL] = typeof(UlOlTagCssApplier);
                buildMap[TagConstants.PRE] = typeof(BlockCssApplier);
                buildMap[TagConstants.Q] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.S] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.SAMP] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.SMALL] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.SPAN] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.STRIKE] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.STRONG] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.SUB] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.SUP] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.TD] = typeof(TdTagCssApplier);
                buildMap[TagConstants.TH] = typeof(TdTagCssApplier);
                buildMap[TagConstants.TIME] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.U] = typeof(SpanTagCssApplier);
                buildMap[TagConstants.UL] = typeof(UlOlTagCssApplier);
                buildMap[TagConstants.VAR] = typeof(SpanTagCssApplier);
                mapping = JavaCollectionsUtil.UnmodifiableMap(buildMap);
            }
            return mapping;
        }
    }
}
