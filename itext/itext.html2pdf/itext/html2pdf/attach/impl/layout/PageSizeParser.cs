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
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Util;
using iText.IO.Log;
using iText.Kernel.Geom;
using iText.Layout.Properties;

namespace iText.Html2pdf.Attach.Impl.Layout {
    internal class PageSizeParser {
        private static IDictionary<String, PageSize> pageSizeConstants = new Dictionary<String, PageSize>();

        static PageSizeParser() {
            pageSizeConstants["a5"] = PageSize.A5;
            pageSizeConstants["a4"] = PageSize.A4;
            pageSizeConstants["a3"] = PageSize.A3;
            pageSizeConstants["b5"] = PageSize.B5;
            pageSizeConstants["b4"] = PageSize.B4;
            pageSizeConstants["jis-b5"] = new PageSize(516, 729);
            pageSizeConstants["jis-b4"] = new PageSize(729, 1032);
            pageSizeConstants["letter"] = PageSize.LETTER;
            pageSizeConstants["legal"] = PageSize.LEGAL;
            pageSizeConstants["ledger"] = PageSize.LEDGER;
        }

        // TODO may be use here TABLOID? based on w3c tests, ledger in html is interpreted as portrait-oriented page
        internal static PageSize FetchPageSize(String pageSizeStr, float em, float rem, PageSize defaultPageSize) {
            PageSize pageSize = ((PageSize)defaultPageSize.Clone());
            if (pageSizeStr == null || CssConstants.AUTO.Equals(pageSizeStr)) {
                return pageSize;
            }
            String[] pageSizeChunks = iText.IO.Util.StringUtil.Split(pageSizeStr, " ");
            String firstChunk = pageSizeChunks[0];
            if (IsLengthValue(firstChunk)) {
                PageSize pageSizeBasedOnLength = ParsePageLengthValue(pageSizeChunks, em, rem);
                if (pageSizeBasedOnLength != null) {
                    pageSize = pageSizeBasedOnLength;
                }
                else {
                    ILogger logger = LoggerFactory.GetLogger(typeof(PageSizeParser));
                    logger.Error(String.Format(iText.Html2pdf.LogMessageConstant.PAGE_SIZE_VALUE_IS_INVALID, pageSizeStr));
                }
            }
            else {
                bool? landscape = null;
                PageSize namedPageSize = null;
                if (IsLandscapePortraitValue(firstChunk)) {
                    landscape = CssConstants.LANDSCAPE.Equals(firstChunk);
                }
                else {
                    namedPageSize = pageSizeConstants.Get(firstChunk);
                }
                if (pageSizeChunks.Length > 1) {
                    String secondChunk = pageSizeChunks[1];
                    if (IsLandscapePortraitValue(secondChunk)) {
                        landscape = CssConstants.LANDSCAPE.Equals(secondChunk);
                    }
                    else {
                        namedPageSize = pageSizeConstants.Get(secondChunk);
                    }
                }
                bool b1 = pageSizeChunks.Length == 1 && (namedPageSize != null || landscape != null);
                // TODO rename propertly?
                bool b2 = namedPageSize != null && landscape != null;
                if (b1 || b2) {
                    if (namedPageSize != null) {
                        pageSize = namedPageSize;
                    }
                    if (true.Equals(landscape)) {
                        pageSize = pageSize.Rotate();
                    }
                }
                else {
                    ILogger logger = LoggerFactory.GetLogger(typeof(PageSizeParser));
                    logger.Error(String.Format(iText.Html2pdf.LogMessageConstant.PAGE_SIZE_VALUE_IS_INVALID, pageSizeStr));
                }
            }
            return pageSize;
        }

        private static PageSize ParsePageLengthValue(String[] pageSizeChunks, float em, float rem) {
            float? width;
            float? height;
            width = TryParsePageLengthValue(pageSizeChunks[0], em, rem);
            if (width == null) {
                return null;
            }
            if (pageSizeChunks.Length > 1) {
                height = TryParsePageLengthValue(pageSizeChunks[1], em, rem);
                if (height == null) {
                    return null;
                }
            }
            else {
                height = width;
            }
            return new PageSize((float)width, (float)height);
        }

        private static float? TryParsePageLengthValue(String valueChunk, float em, float rem) {
            UnitValue unitValue = CssUtils.ParseLengthValueToPt(valueChunk, em, rem);
            if (unitValue == null || unitValue.IsPercentValue()) {
                return null;
            }
            return unitValue.GetValue();
        }

        private static bool IsLengthValue(String pageSizeChunk) {
            return CssUtils.IsMetricValue(pageSizeChunk) || CssUtils.IsRelativeValue(pageSizeChunk);
        }

        private static bool IsLandscapePortraitValue(String pageSizeChunk) {
            return CssConstants.LANDSCAPE.Equals(pageSizeChunk) || CssConstants.PORTRAIT.Equals(pageSizeChunk);
        }
    }
}
