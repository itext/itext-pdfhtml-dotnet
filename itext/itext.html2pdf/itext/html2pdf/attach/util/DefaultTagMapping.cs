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
using System.Collections.Concurrent;
using System.Collections.Generic;
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Html;

namespace iText.Html2pdf.Attach.Util {
    /// <summary>Created by SamuelHuylebroeck on 11/30/2016.</summary>
    public class DefaultTagMapping {
        public static IDictionary<String, String> GetDefaultTagWorkerMapping() {
            IDictionary<String, String> mapping = new ConcurrentDictionary<String, String>();
            mapping[TagConstants.A] = typeof(ATagWorker).FullName;
            mapping[TagConstants.ARTICLE] = typeof(DivTagWorker).FullName;
            mapping[TagConstants.ASIDE] = typeof(DivTagWorker).FullName;
            mapping[TagConstants.B] = typeof(SpanTagWorker).FullName;
            mapping[TagConstants.BLOCKQUOTE] = typeof(DivTagWorker).FullName;
            mapping[TagConstants.BODY] = typeof(BodyTagWorker).FullName;
            mapping[TagConstants.BR] = typeof(BrTagWorker).FullName;
            mapping[TagConstants.CITE] = typeof(SpanTagWorker).FullName;
            mapping[TagConstants.CODE] = typeof(SpanTagWorker).FullName;
            mapping[TagConstants.DIV] = typeof(DivTagWorker).FullName;
            mapping[TagConstants.DD] = typeof(DdTagWorker).FullName;
            mapping[TagConstants.DL] = typeof(DlTagWorker).FullName;
            mapping[TagConstants.DT] = typeof(DtTagWorker).FullName;
            mapping[TagConstants.EM] = typeof(SpanTagWorker).FullName;
            mapping[TagConstants.FOOTER] = typeof(DivTagWorker).FullName;
            mapping[TagConstants.HEADER] = typeof(DivTagWorker).FullName;
            mapping[TagConstants.HR] = typeof(HrTagWorker).FullName;
            mapping[TagConstants.HTML] = typeof(HtmlTagWorker).FullName;
            mapping[TagConstants.I] = typeof(SpanTagWorker).FullName;
            mapping[TagConstants.IMG] = typeof(ImgTagWorker).FullName;
            mapping[TagConstants.LI] = typeof(LiTagWorker).FullName;
            mapping[TagConstants.MAIN] = typeof(DivTagWorker).FullName;
            mapping[TagConstants.META] = typeof(MetaTagWorker).FullName;
            mapping[TagConstants.NAV] = typeof(DivTagWorker).FullName;
            mapping[TagConstants.OL] = typeof(UlOlTagWorker).FullName;
            mapping[TagConstants.H1] = typeof(PTagWorker).FullName;
            mapping[TagConstants.H2] = typeof(PTagWorker).FullName;
            mapping[TagConstants.H3] = typeof(PTagWorker).FullName;
            mapping[TagConstants.H4] = typeof(PTagWorker).FullName;
            mapping[TagConstants.H5] = typeof(PTagWorker).FullName;
            mapping[TagConstants.H6] = typeof(PTagWorker).FullName;
            mapping[TagConstants.P] = typeof(PTagWorker).FullName;
            mapping[TagConstants.PRE] = typeof(PTagWorker).FullName;
            mapping[TagConstants.Q] = typeof(SpanTagWorker).FullName;
            mapping[TagConstants.SECTION] = typeof(DivTagWorker).FullName;
            mapping[TagConstants.SMALL] = typeof(SpanTagWorker).FullName;
            mapping[TagConstants.SPAN] = typeof(SpanTagWorker).FullName;
            mapping[TagConstants.STRIKE] = typeof(SpanTagWorker).FullName;
            mapping[TagConstants.STRONG] = typeof(SpanTagWorker).FullName;
            mapping[TagConstants.SUB] = typeof(SpanTagWorker).FullName;
            mapping[TagConstants.SUP] = typeof(SpanTagWorker).FullName;
            mapping[TagConstants.TABLE] = typeof(TableTagWorker).FullName;
            mapping[TagConstants.TFOOT] = typeof(TableFooterTagWorker).FullName;
            mapping[TagConstants.THEAD] = typeof(TableHeaderTagWorker).FullName;
            mapping[TagConstants.TIME] = typeof(SpanTagWorker).FullName;
            mapping[TagConstants.TITLE] = typeof(TitleTagWorker).FullName;
            mapping[TagConstants.TD] = typeof(TdTagWorker).FullName;
            mapping[TagConstants.TH] = typeof(TdTagWorker).FullName;
            mapping[TagConstants.TR] = typeof(TrTagWorker).FullName;
            mapping[TagConstants.U] = typeof(SpanTagWorker).FullName;
            mapping[TagConstants.UL] = typeof(UlOlTagWorker).FullName;
            return mapping;
        }
    }
}
