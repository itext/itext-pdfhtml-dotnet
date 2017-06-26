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
using System.Collections.Generic;

namespace iText.Html2pdf.Css.Media {
    /// <summary>
    /// Utilities class that parses <code>String</code> values into
    /// <see cref="MediaQuery"/>
    /// or
    /// <see cref="MediaExpression"/>
    /// values.
    /// </summary>
    public sealed class MediaQueryParser {
        /// <summary>Creates a <code>MediaQueryParse</code> instance.</summary>
        private MediaQueryParser() {
        }

        /// <summary>
        /// Parses a <code>String</code> into a list of
        /// <see>MediaQuery) values.</see>
        /// </summary>
        /// <param name="mediaQueriesStr">the media queries in the form of a <code>String</code></param>
        /// <returns>
        /// the resulting list of
        /// <see>MediaQuery) values</see>
        /// </returns>
        public static IList<MediaQuery> ParseMediaQueries(String mediaQueriesStr) {
            String[] mediaQueryStrs = iText.IO.Util.StringUtil.Split(mediaQueriesStr, ",");
            IList<MediaQuery> mediaQueries = new List<MediaQuery>();
            foreach (String mediaQueryStr in mediaQueryStrs) {
                MediaQuery mediaQuery = ParseMediaQuery(mediaQueryStr);
                if (mediaQuery != null) {
                    mediaQueries.Add(mediaQuery);
                }
            }
            return mediaQueries;
        }

        /// <summary>
        /// Parses a <code>String</code> into a
        /// <see>MediaQuery) value.</see>
        /// </summary>
        /// <param name="mediaQueryStr">the media query in the form of a <code>String</code></param>
        /// <returns>
        /// the resulting
        /// <see>MediaQuery) value</see>
        /// </returns>
        public static MediaQuery ParseMediaQuery(String mediaQueryStr) {
            mediaQueryStr = mediaQueryStr.Trim().ToLowerInvariant();
            bool only = false;
            bool not = false;
            if (mediaQueryStr.StartsWith(MediaRuleConstants.ONLY)) {
                only = true;
                mediaQueryStr = mediaQueryStr.Substring(MediaRuleConstants.ONLY.Length).Trim();
            }
            else {
                if (mediaQueryStr.StartsWith(MediaRuleConstants.NOT)) {
                    not = true;
                    mediaQueryStr = mediaQueryStr.Substring(MediaRuleConstants.NOT.Length).Trim();
                }
            }
            int indexOfSpace = mediaQueryStr.IndexOf(' ');
            String firstWord = indexOfSpace != -1 ? mediaQueryStr.JSubstring(0, indexOfSpace) : mediaQueryStr;
            String mediaType = null;
            IList<MediaExpression> mediaExpressions = null;
            if (only || not || MediaType.IsValidMediaType(firstWord)) {
                mediaType = firstWord;
                mediaExpressions = ParseMediaExpressions(mediaQueryStr.Substring(firstWord.Length), true);
            }
            else {
                mediaExpressions = ParseMediaExpressions(mediaQueryStr, false);
            }
            return new MediaQuery(mediaType, mediaExpressions, only, not);
        }

        /// <summary>
        /// Parses a <code>String</code> into a list of
        /// <see>MediaExpression) values.</see>
        /// </summary>
        /// <param name="mediaExpressionsStr">the media expressions in the form of a <code>String</code></param>
        /// <param name="shallStartWithAnd">indicates if the media expression shall start with "and"</param>
        /// <returns>
        /// the resulting list of
        /// <see>MediaExpression) values</see>
        /// </returns>
        private static IList<MediaExpression> ParseMediaExpressions(String mediaExpressionsStr, bool shallStartWithAnd
            ) {
            mediaExpressionsStr = mediaExpressionsStr.Trim();
            bool startsWithEnd = mediaExpressionsStr.StartsWith(MediaRuleConstants.AND);
            bool firstExpression = true;
            String[] mediaExpressionStrs = iText.IO.Util.StringUtil.Split(mediaExpressionsStr, MediaRuleConstants.AND);
            IList<MediaExpression> expressions = new List<MediaExpression>();
            foreach (String mediaExpressionStr in mediaExpressionStrs) {
                MediaExpression expression = ParseMediaExpression(mediaExpressionStr);
                if (expression != null) {
                    if (firstExpression) {
                        if (shallStartWithAnd && !startsWithEnd) {
                            throw new InvalidOperationException("Expected 'and' while parsing media expression");
                        }
                    }
                    firstExpression = false;
                    expressions.Add(expression);
                }
            }
            return expressions;
        }

        /// <summary>
        /// Parses a <code>String</code> into a
        /// <see>MediaExpression) value.</see>
        /// </summary>
        /// <param name="mediaExpressionStr">the media expression in the form of a <code>String</code></param>
        /// <returns>
        /// the resulting
        /// <see>MediaExpression) value</see>
        /// </returns>
        private static MediaExpression ParseMediaExpression(String mediaExpressionStr) {
            mediaExpressionStr = mediaExpressionStr.Trim();
            if (!mediaExpressionStr.StartsWith("(") || !mediaExpressionStr.EndsWith(")")) {
                return null;
            }
            mediaExpressionStr = mediaExpressionStr.JSubstring(1, mediaExpressionStr.Length - 1);
            if (mediaExpressionStr.Length == 0) {
                return null;
            }
            int colonPos = mediaExpressionStr.IndexOf(':');
            String mediaFeature;
            String value = null;
            if (colonPos == -1) {
                mediaFeature = mediaExpressionStr;
            }
            else {
                mediaFeature = mediaExpressionStr.JSubstring(0, colonPos).Trim();
                value = mediaExpressionStr.Substring(colonPos + 1).Trim();
            }
            return new MediaExpression(mediaFeature, value);
        }
    }
}
