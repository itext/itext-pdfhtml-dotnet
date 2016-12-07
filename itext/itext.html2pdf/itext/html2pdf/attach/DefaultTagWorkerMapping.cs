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
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Html;
using iText.IO.Util;

namespace iText.Html2pdf.Attach {
    /// <summary>Created by SamuelHuylebroeck on 11/30/2016.</summary>
    public class DefaultTagWorkerMapping {
        private static IDictionary<String, Type> mapping;

        public static IDictionary<String, Type> GetDefaultTagWorkerMapping() {
            if (mapping == null) {
                IDictionary<String, Type> buildMap = new Dictionary<String, Type>();
                buildMap[TagConstants.A] = typeof(ATagWorker);
                buildMap[TagConstants.ARTICLE] = typeof(DivTagWorker);
                buildMap[TagConstants.ASIDE] = typeof(DivTagWorker);
                buildMap[TagConstants.B] = typeof(SpanTagWorker);
                buildMap[TagConstants.BLOCKQUOTE] = typeof(DivTagWorker);
                buildMap[TagConstants.BODY] = typeof(BodyTagWorker);
                buildMap[TagConstants.BR] = typeof(BrTagWorker);
                buildMap[TagConstants.CITE] = typeof(SpanTagWorker);
                buildMap[TagConstants.CODE] = typeof(SpanTagWorker);
                buildMap[TagConstants.DIV] = typeof(DivTagWorker);
                buildMap[TagConstants.DD] = typeof(DdTagWorker);
                buildMap[TagConstants.DL] = typeof(DlTagWorker);
                buildMap[TagConstants.DT] = typeof(DtTagWorker);
                buildMap[TagConstants.EM] = typeof(SpanTagWorker);
                buildMap[TagConstants.FOOTER] = typeof(DivTagWorker);
                buildMap[TagConstants.HEADER] = typeof(DivTagWorker);
                buildMap[TagConstants.HR] = typeof(HrTagWorker);
                buildMap[TagConstants.HTML] = typeof(HtmlTagWorker);
                buildMap[TagConstants.I] = typeof(SpanTagWorker);
                buildMap[TagConstants.IMG] = typeof(ImgTagWorker);
                buildMap[TagConstants.LI] = typeof(LiTagWorker);
                buildMap[TagConstants.MAIN] = typeof(DivTagWorker);
                buildMap[TagConstants.META] = typeof(MetaTagWorker);
                buildMap[TagConstants.NAV] = typeof(DivTagWorker);
                buildMap[TagConstants.OL] = typeof(UlOlTagWorker);
                buildMap[TagConstants.H1] = typeof(PTagWorker);
                buildMap[TagConstants.H2] = typeof(PTagWorker);
                buildMap[TagConstants.H3] = typeof(PTagWorker);
                buildMap[TagConstants.H4] = typeof(PTagWorker);
                buildMap[TagConstants.H5] = typeof(PTagWorker);
                buildMap[TagConstants.H6] = typeof(PTagWorker);
                buildMap[TagConstants.P] = typeof(PTagWorker);
                buildMap[TagConstants.PRE] = typeof(PTagWorker);
                buildMap[TagConstants.Q] = typeof(SpanTagWorker);
                buildMap[TagConstants.SECTION] = typeof(DivTagWorker);
                buildMap[TagConstants.SMALL] = typeof(SpanTagWorker);
                buildMap[TagConstants.SPAN] = typeof(SpanTagWorker);
                buildMap[TagConstants.STRIKE] = typeof(SpanTagWorker);
                buildMap[TagConstants.STRONG] = typeof(SpanTagWorker);
                buildMap[TagConstants.SUB] = typeof(SpanTagWorker);
                buildMap[TagConstants.SUP] = typeof(SpanTagWorker);
                buildMap[TagConstants.TABLE] = typeof(TableTagWorker);
                buildMap[TagConstants.TFOOT] = typeof(TableFooterTagWorker);
                buildMap[TagConstants.THEAD] = typeof(TableHeaderTagWorker);
                buildMap[TagConstants.TIME] = typeof(SpanTagWorker);
                buildMap[TagConstants.TITLE] = typeof(TitleTagWorker);
                buildMap[TagConstants.TD] = typeof(TdTagWorker);
                buildMap[TagConstants.TH] = typeof(TdTagWorker);
                buildMap[TagConstants.TR] = typeof(TrTagWorker);
                buildMap[TagConstants.U] = typeof(SpanTagWorker);
                buildMap[TagConstants.UL] = typeof(UlOlTagWorker);
                mapping = JavaCollectionsUtil.UnmodifiableMap(buildMap);
            }
            return mapping;
        }
    }
}
