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
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Commons.Utils;
using iText.Html2pdf.Css;
using iText.Html2pdf.Logs;
using iText.Kernel.Geom;
using iText.Layout.Properties;
using iText.StyledXmlParser.Css.Util;

namespace iText.Html2pdf.Attach.Impl.Layout {
//\cond DO_NOT_DOCUMENT
    /// <summary>The Class PageSizeParser.</summary>
    internal class PageSizeParser {
        /// <summary>A Map mapping page size names to page size values.</summary>
        private static readonly IDictionary<String, PageSize> pageSizeConstants = new Dictionary<String, PageSize>
            ();

        static PageSizeParser() {
            pageSizeConstants.Put("a5", PageSize.A5);
            pageSizeConstants.Put("a4", PageSize.A4);
            pageSizeConstants.Put("a3", PageSize.A3);
            pageSizeConstants.Put("b5", PageSize.B5);
            pageSizeConstants.Put("b4", PageSize.B4);
            pageSizeConstants.Put("jis-b5", new PageSize(516, 729));
            pageSizeConstants.Put("jis-b4", new PageSize(729, 1032));
            pageSizeConstants.Put("letter", PageSize.LETTER);
            pageSizeConstants.Put("legal", PageSize.LEGAL);
            /* according to CSS Paged Media Module Level 3 Editor’s Draft:
            "ledger - Equivalent to the size of North American ledger: 11 inches wide by 17 inches high"
            See https://www.w3.org/TR/css-page-3/
            That makes this <page-size> portrait-oriented, i.e. rotated PageSize.LEDGER.
            */
            pageSizeConstants.Put("ledger", PageSize.LEDGER.Rotate());
        }

//\cond DO_NOT_DOCUMENT
        /// <summary>Fetch the page size.</summary>
        /// <param name="pageSizeStr">the name of the page size ("a4", "letter",...)</param>
        /// <param name="em">the em value</param>
        /// <param name="rem">the root em value</param>
        /// <param name="defaultPageSize">the default page size</param>
        /// <returns>the page size</returns>
        internal static PageSize FetchPageSize(String pageSizeStr, float em, float rem, PageSize defaultPageSize) {
            PageSize pageSize = (PageSize)defaultPageSize.Clone();
            if (pageSizeStr == null || CssConstants.AUTO.Equals(pageSizeStr)) {
                return pageSize;
            }
            String[] pageSizeChunks = iText.Commons.Utils.StringUtil.Split(pageSizeStr, " ");
            String firstChunk = pageSizeChunks[0];
            if (IsLengthValue(firstChunk)) {
                PageSize pageSizeBasedOnLength = ParsePageLengthValue(pageSizeChunks, em, rem);
                if (pageSizeBasedOnLength != null) {
                    pageSize = pageSizeBasedOnLength;
                }
                else {
                    ILogger logger = ITextLogManager.GetLogger(typeof(PageSizeParser));
                    logger.LogError(MessageFormatUtil.Format(Html2PdfLogMessageConstant.PAGE_SIZE_VALUE_IS_INVALID, pageSizeStr
                        ));
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
                bool isValidSingleWordDeclaration = pageSizeChunks.Length == 1 && (namedPageSize != null || landscape != null
                    );
                bool isValidTwoWordDeclaration = namedPageSize != null && landscape != null;
                if (isValidSingleWordDeclaration || isValidTwoWordDeclaration) {
                    if (namedPageSize != null) {
                        pageSize = namedPageSize;
                    }
                    bool landscapeRequestedAndNeedRotation = true.Equals(landscape) && pageSize.GetWidth() < pageSize.GetHeight
                        ();
                    bool portraitRequestedAndNeedRotation = false.Equals(landscape) && pageSize.GetHeight() < pageSize.GetWidth
                        ();
                    if (landscapeRequestedAndNeedRotation || portraitRequestedAndNeedRotation) {
                        pageSize = pageSize.Rotate();
                    }
                }
                else {
                    ILogger logger = ITextLogManager.GetLogger(typeof(PageSizeParser));
                    logger.LogError(MessageFormatUtil.Format(Html2PdfLogMessageConstant.PAGE_SIZE_VALUE_IS_INVALID, pageSizeStr
                        ));
                }
            }
            return pageSize;
        }
//\endcond

        /// <summary>Parses a page length value into a page size.</summary>
        /// <param name="pageSizeChunks">array of string values that represent the page size</param>
        /// <param name="em">the em value</param>
        /// <param name="rem">the root em value</param>
        /// <returns>the page size</returns>
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

        /// <summary>Try to parse a page length value.</summary>
        /// <param name="valueChunk">a string containing a value</param>
        /// <param name="em">the em value</param>
        /// <param name="rem">the root em value</param>
        /// <returns>the value as a float</returns>
        private static float? TryParsePageLengthValue(String valueChunk, float em, float rem) {
            UnitValue unitValue = CssDimensionParsingUtils.ParseLengthValueToPt(valueChunk, em, rem);
            if (unitValue == null || unitValue.IsPercentValue()) {
                return null;
            }
            return unitValue.GetValue();
        }

        /// <summary>Checks if a string represents length value.</summary>
        /// <param name="pageSizeChunk">the string that possibly represents a length value</param>
        /// <returns>true, if the string represents a length value</returns>
        private static bool IsLengthValue(String pageSizeChunk) {
            return CssTypesValidationUtils.IsMetricValue(pageSizeChunk) || CssTypesValidationUtils.IsRelativeValue(pageSizeChunk
                );
        }

        /// <summary>Checks if a string represents the CSS value for landscape or portrait orientation.</summary>
        /// <param name="pageSizeChunk">the string that possibly represents a landscape or portrait value</param>
        /// <returns>true, if the string represents a landscape or portrait value</returns>
        private static bool IsLandscapePortraitValue(String pageSizeChunk) {
            return CssConstants.LANDSCAPE.Equals(pageSizeChunk) || CssConstants.PORTRAIT.Equals(pageSizeChunk);
        }
    }
//\endcond
}
